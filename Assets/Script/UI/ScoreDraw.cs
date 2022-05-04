using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BTLGeek.Manager;

namespace BTLGeek.UI
{
    public class ScoreDraw : MonoBehaviour
    {
        [field: Header("アタッチするインスタンス")]
        /// <summary>
        /// スコアを表示するテキストコンポーネント
        /// </summary>
        [field: Tooltip("スコアを表示するテキストコンポーネントをアタッチする。")]
        [field: SerializeField]
        Text scoreText = null;

        /// <summary>
        /// 表示するスコアを所持しているコンポーネント
        /// </summary>
//        [field: Tooltip("表示するスコアを所持しているコンポーネントをアタッチする。")]
//        [field: SerializeField]
//        ScoreManager scoreManager = null;


        int prevScore;

        // Start is called before the first frame update
        void Start()
        {
            // エラーチェック
            if (scoreText == null) {
                Debug.LogError("残り移動回数を表示するテキストコンポーネントがアタッチされていません。\nインスペクター上からアタッチしてください。");
            }
//            if (scoreManager==null) {
//                Debug.LogError("残り移動回数を所持しているコンポーネントがアタッチされていません。\nインスペクター上からアタッチしてください。");
//            }

            // 前フレームのスコアを初期
            prevScore = 0;
        }

        // Update is called once per frame
        void Update()
        {
            // 前フレームと比較して、変更があればテキスト更新
//            if (prevScore == ) {
//                scoreText.text = .ToString();
//			}
        }
    }


}
