using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace BTLGeek.Manager
{

    /// <summary>
    /// サウンドマネージャー
    /// </summary>
    public class SoundManager : SingletonMonoBehaviour<SoundManager>
    {
        [Header("マスタ音量")]
        [SerializeField, Range(0f, 1f)]
        private float volume_ = 1f;

        [Header("BGM音量")]
        [SerializeField, Range(0f, 1f)]
        private float bgm_volume_ = 1f;

        [Header("SE音量")]
        [SerializeField, Range(0f, 1f)]
        private float se_volume_ = 1f;

        private AudioClip[] bgm_;
        private AudioClip[] se_;

        private Dictionary<string, int> bgm_index_ = new Dictionary<string, int>();
        private Dictionary<string, int> se_index_ = new Dictionary<string, int>();

        private AudioSource bgm_audio_source_;
        private AudioSource se_audio_source_;

        public float Volume
        {
            set
            {
                volume_ = Mathf.Clamp01(value);
                bgm_audio_source_.volume = bgm_volume_ * volume_;
                se_audio_source_.volume = se_volume_ * volume_;
            }
            get
            {
                return volume_;
            }
        }

        public float BgmVolume
        {
            set
            {
                bgm_volume_ = Mathf.Clamp01(value);
                bgm_audio_source_.volume = bgm_volume_ * volume_;
            }
            get
            {
                return bgm_volume_;
            }
        }

        public float SeVolume
        {
            set
            {
                se_volume_ = Mathf.Clamp01(value);
                se_audio_source_.volume = se_volume_ * volume_;
            }
            get
            {
                return se_volume_;
            }
        }

        /// <summary>
        /// 起動時に実行される
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            DontDestroyOnLoad(new GameObject("SoundManager", typeof(SoundManager)));
            Instance.Init();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        private void Init()
        {
            bgm_audio_source_ = gameObject.AddComponent<AudioSource>();
            se_audio_source_ = gameObject.AddComponent<AudioSource>();

            // 音量設定
            var sms = SoundManagerSetting.Instance;
            Volume = sms.Volume;
            BgmVolume = sms.BGMVolume;
            SeVolume = sms.SEVolume;

            bgm_ = Resources.LoadAll<AudioClip>("BGM");
            se_ = Resources.LoadAll<AudioClip>("SE");

            for (int i = 0; i < bgm_.Length; i++)
            {
                bgm_index_.Add("BGM/" + bgm_[i].name, i);
            }

            for (int i = 0; i < se_.Length; i++)
            {
                se_index_.Add("SE/" + se_[i].name, i);
            }
        }

        public int GetBgmIndex(string name)
        {
            if (bgm_index_.ContainsKey(name))
            {
                return bgm_index_[name];
            }
            else
            {
                Debug.LogError("指定された" + name + "名前のBGMファイルが存在しません。");
                return 0;
            }
        }

        public int GetSeIndex(string name)
        {
            if (se_index_.ContainsKey(name))
            {
                return se_index_[name];
            }
            else
            {
                Debug.LogError("指定された" + name + "の名前のSEファイルが存在しません。");
                return 0;
            }
        }

        //BGM再生
        public void PlayBgm(int index, bool is_loop)
        {
            index = Mathf.Clamp(index, 0, bgm_.Length);

            bgm_audio_source_.clip = bgm_[index];
            bgm_audio_source_.loop = is_loop;
            bgm_audio_source_.volume = BgmVolume * Volume;
            bgm_audio_source_.Play();
        }

        public void PlayBgmByName(string name, bool is_loop = true)
        {
            PlayBgm(GetBgmIndex(name), is_loop);
        }

        public void StopBgm()
        {
            bgm_audio_source_.Stop();
            bgm_audio_source_.clip = null;
        }

        //SE再生
        public void PlaySe(int index)
        {
            index = Mathf.Clamp(index, 0, se_.Length);

            se_audio_source_.PlayOneShot(se_[index], SeVolume * Volume);
        }

        public void PlaySeByName(string name)
        {
            PlaySe(GetSeIndex(name));
        }

        public void StopSe()
        {
            se_audio_source_.Stop();
            se_audio_source_.clip = null;
        }

        /// <summary>
        /// アプリ終了時呼ばれる
        /// </summary>
        private void OnApplicationQuit()
        {
//            SoundManagerSetting.Instance.ApplicationQuit();
        }
    }
}