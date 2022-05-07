using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTLGeek.Manager;
using BTLGeek.Constants;


namespace BTLGeek.State
{
	public class Questioner_Start : DesignPattern.State<Questioner>
	{
		/*---- メンバ ----*/
		/// <summary>
		/// 開始演出を持つアニメーター
		/// </summary>
		private Animator animator_ = null;

		/// <summary>
		/// サウンドを再生するかどうか
		/// </summary>
		bool isSE_Play = true;


		/*---- メソッド ----*/
		/// <summary>
		/// ステート生成時に呼ばれる
		/// </summary>
		public override void Start(Questioner owner)
		{
			// 開始演出のアニメーターを取得
			animator_ = GameObject.Find("Directing").GetComponent<Animator>( );
			if(null == animator_) {
				Debug.LogError("クラス名：Questioner_Start\n関数名：Start\n-- 詳細--\n開始アニメーションのアニメーターの取得に失敗しました。");
			}

			// 開始演出再生
			animator_.Play("Start");

			// BGM再生
			SoundManager.Instance.PlayBgmByName(BGMPath.GAME_SCENE, true);
		}

		public override void Finish(Questioner owner)
		{
			animator_ = null;
		}

		public override void Update(Questioner owner)
		{
			if (animator_.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.33f && isSE_Play) {
				switch(Random.Range(1, 3)) {
					case 1:
						SoundManager.Instance.PlaySeByName(SEPath.START01);
						break;

					case 2:
						SoundManager.Instance.PlaySeByName(SEPath.START02);
						break;

					case 3:
						SoundManager.Instance.PlaySeByName(SEPath.START03);
						break;
				}
				isSE_Play = false;
			}
			// 開始演出終了待ち
			else if (animator_.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f) {
			// 終了後タイマースタート

			// ステートをゲーム中に遷移
				owner.stateMachine_.ChangeState<Questioner_Do>( );
			}
		}

		public override void FixedUpdate(Questioner owner)
		{// 処理なし
		}

		public override void LateUpdate(Questioner owner)
		{ // 処理無し
		}


	}
}
