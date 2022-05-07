using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BTLGeek.State
{
	public class Questioner_Start : DesignPattern.State<Questioner>
	{
		/*---- メンバ ----*/
		/// <summary>
		/// 開始演出を持つアニメーター
		/// </summary>
		private Animator animator_ = null;



		/*---- メソッド ----*/
		/// <summary>
		/// ステート生成時に呼ばれる
		/// </summary>
		public override void Start(Questioner owner)
		{
			// 開始演出のアニメーターを取得

			// 開始演出再生

		}

		public override void Finish(Questioner owner)
		{
			animator_ = null;
		}

		public override void Update(Questioner owner)
		{
			// 開始演出終了待ち

			// 終了後タイマースタート

			// ステートをゲーム中に遷移

		}

		public override void FixedUpdate(Questioner owner)
		{// 処理なし
		}

		public override void LateUpdate(Questioner owner)
		{ // 処理無し
		}
	}
}
