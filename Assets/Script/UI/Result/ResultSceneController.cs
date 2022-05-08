using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BTLGeek.Manager;
using BTLGeek.Constants;

namespace BTLGeek.UI
{
    public enum FADE_TYPE
	{
        FADE_IN  = 0,
        FADE_OUT = 1,
	}


    public class ResultSceneController : SingletonMonoBehaviour<ResultSceneController>
    {
        [field: Header("アタッチするインスタンス")]
        /// <summary>
        /// スコアを表示するテキストコンポーネント
        /// </summary>
        [field: Tooltip("スコアを表示するテキストコンポーネントをアタッチする。")]
        [field: SerializeField]
        Text scoreText = null;

        [field: Header("フェード関連")]
        [field: Tooltip("フェード用のImageをアタッチしてください。")]
        [field: SerializeField]
        private Image fadeImage_ = null;

        [field: Tooltip("フェードにかける時間")]
        [field: SerializeField, Range(0.1f, 5f)]
        private float fadeTime_ = 1.0f;

        /// <summary>
        /// 表示するスコアを所持しているコンポーネント
        /// </summary>
        ApplicationManager applicationManager_ = null;



        // Start is called before the first frame update
        void Start()
        {
            // アプリケーションマネージャー取得
            applicationManager_ = ApplicationManager.Instance;

            // エラーチェック
            if (null == scoreText) {
                Debug.LogError("残り移動回数を表示するテキストコンポーネントがアタッチされていません。\nインスペクター上からアタッチしてください。");
            }
            if (null == applicationManager_) {
                Debug.LogError("残り移動回数を所持しているコンポーネントがアタッチされていません。\nインスペクター上からアタッチしてください。");
            }

            // テキストにスコアを設定
            scoreText.text = applicationManager_.Score.ToString();
            // スコアを初期化
            applicationManager_.Score = 0;
            // BGM再生
            SoundManager.Instance.PlayBgmByName(BGMPath.RESULT_SCENE, true);
            // フェードイン
            FadeStart(null, FADE_TYPE.FADE_IN);
        }

        // フェード開始
        public void FadeStart(string changeSceneName, FADE_TYPE fadeType)
		{
            fadeImage_.StopAllCoroutines( );
            fadeImage_.StartCoroutine(FadeUpdate(fadeImage_, fadeTime_, fadeType, changeSceneName));
        }

        public IEnumerator FadeUpdate(Image fadeImage, float fadeTime, FADE_TYPE fadeType, string changeSceneName)
		{
            // 変更後のカラーを作成
            Color aftorColor = fadeImage.color;
            aftorColor.a = (int)fadeType;
            float time = 0f;
            float updateTime = 1f / fadeTime_;

            // フェード完了までループ
            do {
                fadeImage.color = Color.Lerp(fadeImage.color, aftorColor, time);
                time += updateTime * Time.deltaTime;
                yield return null;
            } while (fadeImage.color.a != aftorColor.a);

            if(null != changeSceneName) {
                ApplicationManager.Instance.LoadScene(changeSceneName);
			}
		}
    }


}
