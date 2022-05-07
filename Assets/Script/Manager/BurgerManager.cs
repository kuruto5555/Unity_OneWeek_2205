using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BTLGeek.Manager
{
    public class BurgerManager : SingletonMonoBehaviour<BurgerManager>
    {
        /// <summary>
        /// 判定の評価（仮で定義。スコアに定義してもらいたい。）
        /// </summary>
        [System.Serializable]
        public enum EVALUATION
		{
            PERFECT,    // 完全一致
            GOOD,       // ５割り正解
            BAD,        // ５割り未満
		}


        /// <summary>
        /// アイテムの種類
        /// </summary>
        [System.Serializable]
        public enum ITEM_INDEX
		{
            HAMBURGER       =0, // ハンバーガー
            DOUBLE_BURGER,      // ダブルバーガー
            POTATO,             // ポテト
            JUICE,              // ジュース
            NUGGET,             // ナゲット
            APPLE_PIE,          // アップルパイ

            NONE,               // 無し(最大数)
		}

        /// <summary>
        /// アイテムのリスト
        /// </summary>
        public List<List<ITEM_INDEX>> Table_Frame_Item { get; private set; }


        /// <summary>
        /// インスタンス生成時に呼ばれる
        /// </summary>
        void Awake()
        {
            Table_Frame_Item = new List<List<ITEM_INDEX>>( );
        }


		void TebleClear()
		{
            //データがあるかチェック
            if (Table_Frame_Item.Count != 0) {
                //リストの中は開放する
                foreach (var frameItem in Table_Frame_Item)
                    frameItem.Clear( );
                //0～カウント数分のリストを削除(全削除)
                Table_Frame_Item.RemoveRange(0, (Table_Frame_Item.Count));
            }
        }

        /// <summary>
        /// 最初のアイテムを配置
        /// </summary>
        /// <param name="itemNum">アイテムの数</param>
        /// <param name="tableNum">テーブルの数(2以上)</param>
        /// <param name="efforts">最短手数</param>
		public void SetBurger(int itemNum, int frameNum, int tableNum, int efforts)
		{
            //テーブルの初期化
            TebleClear( );

            // テーブルリスト作成
            for (int i = 0; i<tableNum; i++) {
                Table_Frame_Item.Add(new List<ITEM_INDEX>( ));
			}

            // 引数のアイテムの数が、定義以上か判定
            if ((int)ITEM_INDEX.NONE <= itemNum) {
                //大きい場合、種類の最大数に設定
                itemNum = (int)ITEM_INDEX.NONE;
			}

            // 引数のアイテムの数が、引数の枠の数より大きいか判定
            if(frameNum < itemNum) {
                // 大きい場合、アイテムの数を枠の数と同じにする
                itemNum = frameNum;
			}

            // 抽出用アイテムリスト
            List<int> burger = new List<int>();
            // フレームの数だけ、バーガーの生成
            for (int i = 0; i < frameNum; i++) {
                //現在のフレームインデックスが生成するアイテムの数未満だったら、アイテムを生成
                if(i < itemNum) {
                    //リストの中が0だったらリストに種類を入れる
                    if (burger.Count == 0) {
                        for (int j = 0; j < (int)ITEM_INDEX.NONE; j++) {
                            burger.Add(j);
                        }
                    }

                    // Randでアイテムを抽出し、抽出した物をリストから排除する
                    int select = burger[Random.Range(0, (burger.Count)-1)];
                    foreach(var Frame in Table_Frame_Item) {
                        Frame.Add((ITEM_INDEX)select);
				    }
                    burger.Remove(select);
                }
                // アイテムの数異常だったら、空きにする
                else {
                    foreach (var Frame in Table_Frame_Item) {
                        Frame.Add(ITEM_INDEX.NONE);
                    }
                }
            }

            // 手数分のシャッフルを実行
            Shuffle(efforts, frameNum, tableNum);
		}

        /// <summary>
        /// シャッフル
        /// </summary>
        /// <param name="efforts">最短手数</param>
        /// <param name="frameNum">枠の数(片側の数)</param>
        void Shuffle(int efforts, int frameNum, int tableNum)
		{
            // 手数分実行
            for(int i = 0; i < efforts; i++) {
                // 移動元の選択
                // テーブルを選択
                int movingSource_tableIndex = Random.Range(0, (tableNum-1));
                // どの位置のアイテムか、Randで選択
                int movingSource_frameIndex = Random.Range(0, (frameNum-1));
                int destination_tableIndex;
                int destination_frameIndex;
                do {
                    // 移動先の選択
                    // 右か左かRandで選択(0:右　1:左)
                    destination_tableIndex = Random.Range(0, (tableNum-1));
                    // どの位置のアイテムか、Randで選択
                    destination_frameIndex = Random.Range(0, (frameNum-1));

                } while (movingSource_tableIndex == destination_tableIndex && movingSource_frameIndex == destination_frameIndex);

                // 並び変え
                Move(movingSource_tableIndex, movingSource_frameIndex, destination_tableIndex, destination_frameIndex);
            }
        }


        /// <summary>
        /// バーガーの移動処理
        /// </summary>
        /// <param name="movingSource_tableIndex">移動元のテーブルインデックス</param>
        /// <param name="movingSource_frameIndex">移動元のフレームインデックス</param>
        /// <param name="destination_tableIndex">移動先のテーブルインデックス</param>
        /// <param name="destination_frameIndex">移動先のフレームインデックス</param>
		public void Move(int movingSource_tableIndex, int movingSource_frameIndex, int destination_tableIndex, int destination_frameIndex)
		{
            ITEM_INDEX a = Table_Frame_Item[movingSource_tableIndex][movingSource_frameIndex];
            Table_Frame_Item[movingSource_tableIndex][movingSource_frameIndex] = Table_Frame_Item[destination_tableIndex][destination_frameIndex];
            Table_Frame_Item[destination_tableIndex][destination_frameIndex] = a;
        }

		/// <summary>
		/// クリアしているかチェックする
		/// </summary>
		/// <returns>クリアしているかどうか。true：クリアしている　false：不一致</returns>
		public EVALUATION ClearCheck()
		{
            // ローカル変数
            int matchNum = 0; // 一致しているテーブルの数
            EVALUATION evaluation; // 戻り値
            int frameHalfNum = (int)(Table_Frame_Item[0].Count * 0.5f);
            // 一致しているか判定
            bool isMatch = true;

            // フレームの数分判定
            for (int i = 0; i < Table_Frame_Item[0].Count; i++) {
                // テーブル同士判定
                for(int j = 0; j < (Table_Frame_Item.Count-1); j++) {
                    if (Table_Frame_Item[j][i] != Table_Frame_Item[j+1][i]) {
                        isMatch = false;
                        break;
                    }
				}
                // マッチフラグがtrueだったらカウント
                if (true == isMatch) {
                    matchNum++; // ここに来れば一致しているのでカウント
                }
			}

            // 評価判定
            if (matchNum == Table_Frame_Item[0].Count) {
                evaluation = EVALUATION.PERFECT;
			}
            else if (matchNum >= frameHalfNum) { 
                evaluation = EVALUATION.GOOD;
            }
			else {
                evaluation = EVALUATION.BAD;
			}

            return evaluation;
		}
    }

}