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
        public enum EVALUATION
		{
            PERFECT,    // 完全一致
            GOOD,       // ５割り正解
            BAD,        // ５割り未満
		}


        /// <summary>
        /// アイテムの種類
        /// </summary>
        public enum ITEM_INDEX
		{
            NONE = 0,       // 無し
            HAMBURGER,      // 普通のハンバーガー
            CHEESE_BURGER,  // チーズバーガー
            BTL_BURGER,     // BTLバーガー
            MAX             // 最大値(異常)
		}

        /// <summary>
        /// アイテムのリスト
        /// </summary>
        public List<List<ITEM_INDEX>> Table_Frame_Item { get; private set; }


        // Start is called before the first frame update
        void Start()
        {
            Table_Frame_Item = new List<List<ITEM_INDEX>>( );
            TebleClear( );
        }


        void TebleClear()
		{
            foreach (var FrameItem in Table_Frame_Item)
                FrameItem.Clear( );
            Table_Frame_Item.Clear( );
        }

        /// <summary>
        /// 最初のアイテムを配置
        /// </summary>
        /// <param name="itemNum">アイテムの数</param>
        /// <param name="frameNum">配置できる枠の数(片側の数)</param>
        /// <param name="tableNum">テーブルの数(2以上)</param>
        /// <param name="frameNum">最短手数</param>
		public void SetItem(int itemNum, int frameNum, int tableNum, int efforts)
		{
            //テーブルの初期化
            TebleClear( );

            // テーブルリスト作成
            for(int i = 0; i<tableNum; i++) {
                Table_Frame_Item.Add(new List<ITEM_INDEX>( ));
			}

            // 引数のアイテムの数が、定義以上か判定
            if ((int)ITEM_INDEX.MAX <= itemNum) {
                //大きい場合、種類の最大数に設定
                itemNum = (int)ITEM_INDEX.MAX;
			}

            // 引数のアイテムの数が、引数の枠の数より大きいか判定
            if(frameNum < itemNum) {
                // 大きい場合、アイテムの数を枠の数と同じにする
                itemNum = frameNum;
			}

            // アイテムの生成
            for (int i = 0; i < itemNum; i++) {
                // 抽出用アイテムリスト生成
                List<int> item = new List<int>();
                for (int j = 0; j < (int)ITEM_INDEX.MAX; j++) {
                    item.Add(j);
				}

                // Randでアイテムを抽出し、抽出した物をリストから排除する
                int select = item[Random.Range(0, (item.Count)-1)];
                foreach(var Frame in Table_Frame_Item) {
                    Frame.Add((ITEM_INDEX)select);
				}
                item.Remove(select);
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
                int movingSource_tableIndex = Random.Range(0, tableNum);
                // どの位置のアイテムか、Randで選択
                int movingSource_frameIndex = Random.Range(0, frameNum);
                int destination_tableIndex;
                int destination_frameIndex;
                do {
                    // 移動先の選択
                    // 右か左かRandで選択(0:右　1:左)
                    destination_tableIndex = Random.Range(0, 1);
                    // どの位置のアイテムか、Randで選択
                    destination_frameIndex = Random.Range(0, frameNum);

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
		EVALUATION ClearCheck()
		{
            //ローカル変数
            int matchNum = 0; // 一致しているテーブルの数
            EVALUATION evaluation; // 戻り値
            int frameHalfNum = (int)(Table_Frame_Item[0].Count * 0.5f);

            // フレームの数分判定
            for (int i = 0; i <= Table_Frame_Item[0].Count; i++) {
                // テーブル同士判定
                for(int j = 0; j <= (Table_Frame_Item.Count-1); j++) {
                    if (Table_Frame_Item[j][i] != Table_Frame_Item[j+1][i])
                        break;
				}
                matchNum++; // ここに来れば一致しているのでカウント
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