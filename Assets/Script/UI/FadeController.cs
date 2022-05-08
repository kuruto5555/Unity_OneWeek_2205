using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using BTLGeek.Manager;
using BTLGeek.Constants;

public class FadeController : MonoBehaviour
{
    [Header("フェードアウトが完了するまでの時間")]
    [SerializeField] float FadeTime = 1.0f;

    private Image _fadeImage = null;

    private bool _isFadeStart = false;

    private float _nowTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _fadeImage = GetComponent<Image>();

        _isFadeStart = false;

        _nowTime = 0f;

        SoundManager.Instance.PlayBgmByName(BGMPath.TITLE_SCENE);
    }

    // Update is called once per frame
    void Update()
    {
        SceneChangeUpdate();
    }

    public void FadeOutStart()
    {
        _isFadeStart = true;
        _fadeImage.DOFade(1, FadeTime);
        SoundManager.Instance.PlaySeByName(SEPath.DECISION5);
    }

    private void SceneChangeUpdate()
    {
        if (!_isFadeStart) return;

        _nowTime += Time.deltaTime;

        if(FadeTime < _nowTime)
        {
            ApplicationManager.Instance.LoadScene(SceneName.GAME_SCENE);
        }

    }
}
