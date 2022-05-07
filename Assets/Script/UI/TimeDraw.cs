using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BTLGeek.UI
{
    public class TimeDraw : MonoBehaviour
    {
        [field: Header("アタッチするインスタンス")]
        /// <summary>
        /// スコアを表示するテキストコンポーネント
        /// </summary>
        [field: Tooltip("残り時間を表示するテキストコンポーネントをアタッチする。")]
        [field: SerializeField]
        Text timeText = null;

        /// <summary>
        /// 残り時間を所持しているコンポーネント
        /// </summary>
//        [field: Tooltip("残り時間を所持しているコンポーネントをアタッチする。")]
//        [field: SerializeField]
//        Timer timer = null;

        /// <summary>
        /// 前フレームの残り時間(s)
        /// </summary>
        int prevTime;

        // Start is called before the first frame update
        void Start()
        {
            // エラーチェック
            if (timeText == null) {
                Debug.LogError("残り移動回数を表示するテキストコンポーネントがアタッチされていません。\nインスペクター上からアタッチしてください。");
            }
//            if (timer==null) {
//                Debug.LogError("残り移動回数を所持しているコンポーネントがアタッチされていません。\nインスペクター上からアタッチしてください。");
//            }

            // 前フレームのスコアを初期
            prevTime = 0;
        }

        // Update is called once per frame
        void Update()
        {
            // 前フレームと比較して、変更があればテキスト更新
//            if (prevTime == ) {
//                timeText.text = .ToString();
//			}
        }
    }
}
