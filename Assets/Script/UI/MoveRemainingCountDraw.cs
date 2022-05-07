using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BTLGeek.Manager;

namespace BTLGeek.UI
{
    public class MoveRemainingCountDraw : MonoBehaviour
    {
        [field: Header("アタッチするインスタンス")]
        /// <summary>
        /// 残り移動回数を表示するテキストコンポーネント
        /// </summary>
        [field: Tooltip("残り移動回数を表示するテキストコンポーネントをアタッチ")]
        [field: SerializeField]
        private Text countText = null;

        /// <summary>
        /// 残り移動回数を所持しているコンポーネント
        /// </summary>
        [field: Tooltip("残り移動回数を所持しているコンポーネントをアタッチ")]
        [field: SerializeField]
        private MoveCountManager moveCountManager = null;


        private int prevMoveCount;

        // Start is called before the first frame update
        void Start()
        {
            // ムーブカウントマネージャーの取得
            moveCountManager = MoveCountManager.Instance;

            // エラーチェック
            if (countText == null) {
                Debug.LogError("残り移動回数を表示するテキストコンポーネントがアタッチされていません。\nインスペクター上からアタッチしてください。");
			}
			if (moveCountManager == null) {
                Debug.LogError("残り移動回数を所持しているコンポーネントがアタッチされていません。\nインスペクター上からアタッチしてください。");
			}

            // 前フレーム残り移動回数を初期化
            prevMoveCount = moveCountManager.MoveCount;
        }

        // Update is called once per frame
        void Update()
        {
            // 前フレームと比較して、変更があればテキスト更新
            if (prevMoveCount != moveCountManager.MoveCount) {
                // 文字更新
                countText.text = moveCountManager.MoveCount.ToString();
                // 次フレームの比較用に、1フレーム前の値を更新
                prevMoveCount = moveCountManager.MoveCount;
            }
        }
    }

}