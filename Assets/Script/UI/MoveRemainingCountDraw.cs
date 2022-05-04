using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        private GameObject gameObject = null;


        private int prevCount;

        // Start is called before the first frame update
        void Start()
        {
            // エラーチェック
            if (countText == null) {
                Debug.LogError("残り移動回数を表示するテキストコンポーネントがアタッチされていません。\nインスペクター上からアタッチしてください。");
			}
			if (gameObject==null) {
                Debug.LogError("残り移動回数を所持しているコンポーネントがアタッチされていません。\nインスペクター上からアタッチしてください。");
			}

            // 前フレーム残り移動回数を初期
            prevCount = 0;
        }

        // Update is called once per frame
        void Update()
        {
            // 前フレームと比較して、変更があればテキスト更新
//            if (prevCount == ) {
//                countText.text = .ToString();
//			}
        }
    }

}