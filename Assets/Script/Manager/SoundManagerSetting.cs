using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BTLGeek.Manager
{
    /// <summary>
    /// サウンド設定ファイル
    /// </summary>
    public class SoundManagerSetting : ScriptableObject
    {
        private static SoundManagerSetting instance_ = null;
        public static SoundManagerSetting Instance
        {
            get
            {
                if(instance_ == null)
                {
                    instance_ = Resources.Load<SoundManagerSetting>("SoundManagerSetting");
                }
                return instance_;
            }
        }

        //==============================================================================
        // ボリューム系
        //==============================================================================
        public struct SoundVolumeData
		{
            public float volume_;       // メインボリューム
            public float bgm_volume_;   // BGMボリューム
            public float se_volume_;    // SEボリューム
        }

        [SerializeField]
        private float volume_ = 1f, bgm_volume_ = 1f, se_volume_ = 1f;

        public float Volume => volume_;
        public float BGMVolume => bgm_volume_;
        public float SEVolume => se_volume_;

        /// <summary>
        /// サウンド設定
        /// </summary>
        /// <param name="data"></param>
        public void ValueSetting(SoundVolumeData data)
        {
            volume_ = data.volume_;
            bgm_volume_ = data.bgm_volume_;
            se_volume_ = data.se_volume_;
        }

        public SoundVolumeData GetSoundVolumeData()
        {
            SoundVolumeData data = new SoundVolumeData();
            var sm = SoundManager.Instance;

            data.volume_ = sm.Volume;
            data.bgm_volume_ = sm.BgmVolume;
            data.se_volume_ = sm.SeVolume;

            return data;
        }
        /// <summary>
        /// アプリ終了時呼ぶ出す
        /// </summary>
        public void ApplicationQuit()
        {
            // サウンドの設定を保存
            var sm = SoundManager.Instance;
            volume_ = sm.Volume;
            bgm_volume_ = sm.BgmVolume;
            se_volume_ = sm.SeVolume;
        }
    }
}
