using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTLGeek.Manager;
using BTLGeek.Constants;


namespace BTLGeek.State
{
	public class Questioner_TimeIsUp : DesignPattern.State<Questioner>
	{
		/*---- メンバ ----*/
		/// <summary>
		/// アプリケーションマネージャー
		/// </summary>
		ApplicationManager apManager_ = null;

		/// <summary>
		/// スコア加算エフェクト再生用のアニメーター
		/// </summary>
		private Animator animator_ = null;



		/*---- メソッド ----*/
		public override void Start(Questioner owner)
		{
			// アプリケーションマネージャーの取得
			apManager_ = ApplicationManager.Instance;
			// スコア加算エフェクトのアニメーション再生用のアニメーター取得
			animator_ = GameObject.Find("Directing").GetComponent<Animator>( );
			if (null == animator_) {
				Debug.LogError("クラス名：Questioner_Start\n関数名：Start\n-- 詳細--\n開始アニメーションのアニメーターの取得に失敗しました。");
			}
			// タイムアップアニメーション再生
			animator_?.Play("TimeIsUp");
			// タイムアップのSE再生
			SoundManager.Instance.PlaySeByName(SEPath.TIME_IS_OUT);
		}

		public override void Finish(Questioner owner)
		{
			animator_ = null;
		}

		public override void Update(Questioner owner)
		{
			// アニメーションの再生終了待ち
			if(animator_.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f) {
				// 現在のスコアをアプリケーションマネージャーに登録

				// Resultシーンへ遷移
				apManager_.LoadScene(SceneName.RESULT_SCENE);
			}
		}

		public override void FixedUpdate(Questioner owner)
		{
		}

		public override void LateUpdate(Questioner owner)
		{
		}
	}
}
