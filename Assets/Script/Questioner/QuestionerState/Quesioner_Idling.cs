using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTLGeek.Manager;


namespace BTLGeek.State
{
    public class Quesioner_Idling : DesignPattern.State<Questioner>
    {
        /*---- メンバ ----*/
        // 現在の移動回数を持っているコンポーネント

        /// <summary>
        /// 食べ物のマネージャー(判定してもらうよう)
        /// </summary>
        private BurgerManager burgerManager_ = null;

        /// <summary>
        /// 前フレームの移動回数
        /// </summary>
        private int prevMoveCount = 0;

		/// <summary>
		/// スコア加算エフェクト再生用のアニメーター
		/// </summary>
		private Animator animator_ = null;

		/// <summary>
		/// 移動回数管理クラス
		/// </summary>
		private MoveCountManager moveCountManager_ = null;



		/*---- メソッド ----*/
		/// <summary>
		/// 
		/// </summary>
		/// <param name="owner"></param>
		public override void Start(Questioner owner)
		{
			// 現在の移動回数を持っているコンポーネント取得
			moveCountManager_ = MoveCountManager.Instance;
			// 移動回数の取得
			prevMoveCount = moveCountManager_.MoveCount;
			// 食べ物マネージャー取得
			burgerManager_ = BurgerManager.Instance;
			// スコア加算エフェクトのアニメーション再生用のアニメーター取得
			animator_ = GameObject.Find("Directing").GetComponent<Animator>( );
			if (null == animator_) {
				Debug.LogError("クラス名：Questioner_Start\n関数名：Start\n-- 詳細--\n開始アニメーションのアニメーターの取得に失敗しました。");
			}
		}

		public override void Finish(Questioner owner)
		{
			// 手放す(無くてもよかった気がするけど丁寧に)
			burgerManager_ = null;
		}

		public override void Update(Questioner owner)
		{
			// 移動回数に変動があったら判定する
			if (prevMoveCount != moveCountManager_.MoveCount) {
				// クリア判定実施
				BurgerManager.EVALUATION evaluation = burgerManager_.ClearCheck();

				switch (evaluation) {
					case BurgerManager.EVALUATION.PERFECT:
						// スコア加算

						// スコア加算エフェクト再生
						animator_.Play("Parfects");
						// ステートを出題に変える
						owner.stateMachine_.ChangeState<Questioner_Do>( );
						break;

					case BurgerManager.EVALUATION.GOOD:
					case BurgerManager.EVALUATION.BAD:
						// 移動回数が0か判定
						if (moveCountManager_.MoveCount == 0) {
							// 0の場合スコア加算

							// スコア加算エフェクト再生
							animator_.Play(evaluation.ToString( ));
							//ステートを出題に変える
							owner.stateMachine_.ChangeState<Questioner_Do>( );
						}
						else {
							// 0出ない場合何もしない
						}
						break;

					default:
						Debug.LogError("クリア判定のありえないルート\nクラス名：Quesioner_Idling\n関数名：Update");
						break;
				}

				// 前フレームの移動回数に、現在の値をセット
				//prevMoveNum = ;
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
