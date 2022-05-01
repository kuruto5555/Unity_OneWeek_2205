using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BTLGeek.Manager
{
    /// <summary>
    /// ゲーム見守り続けるマネージャー
    /// 別のシーンに持ち越したい値を作って使えればいいですね。（同一シーン内でしか使わない値は別のマネージャーを作ってね。）
    /// </summary>
    public class ApplicationManager : SingletonMonoBehaviour<ApplicationManager>
    {
        /// <summary>
        /// 起動時に実行される
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            DontDestroyOnLoad(new GameObject("ApplicationManager", typeof(ApplicationManager)));
        }


        /// <summary>
        /// シーンの読み込み
        /// </summary>
        /// <param name="scene_name"></param>
        public void LoadScene(string scene_name)
        {
            //BGM,SEの停止
            SoundManager.Instance.StopBgm();
            SoundManager.Instance.StopSe();
            //シーンの読み込み
            SceneManager.LoadScene(scene_name);
        }
    }
}