using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTLGeek.Manager;

namespace BTLGeek
{
    public class Frame : MonoBehaviour
    {
        /*---- メンバ ----*/
        [field: Header("プレハブ")]
        /// <summary>
		/// 食べ物のプレハブリスト
		/// </summary>
		[field: Tooltip("食べ物のプレハブを、ITEM_LISTのインデックスに合わせてアタッチしてください。\nDictionaryがシリアライズ化できないから、こんなことになるんだ！！！")]
        [field: SerializeField]
        List<GameObject> foodPrefabList_ = null;


        [field: Header("デバッグ時に見るよう")]
        /// <summary>
        /// テーブルインデックス
        /// </summary>
        public int TableIndex = 0;

        /// <summary>
        /// フレームのインデックス
        /// </summary>
        public int FrameIndex = 0;



        /*---- メソッド ----*/
        /// <summary>
        /// 食べ物のセット
        /// </summary>
        public void SetFood()
		{
            // BurgerManagerの取得
            BurgerManager bm = BurgerManager.Instance;

            // 種類の取得
            BurgerManager.ITEM_INDEX itemIndex = bm.Table_Frame_Item[TableIndex][FrameIndex];

            if (BurgerManager.ITEM_INDEX.NONE != itemIndex) {
                // 食べ物の生成
                Instantiate(foodPrefabList_[(int)itemIndex], transform);
            }
		}
	}
}