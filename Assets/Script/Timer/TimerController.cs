using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TimerController : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private float timeLimit;   // 制限時間
    private float timeCounter;                  // 内部処理用
    public int seconds { get; private set; }    // 画面に表示する秒数
    private bool countActiveFlag;


    // Start is called before the first frame update
    void Start()
    {
        timeCounter = timeLimit;
        seconds = (int)timeLimit;
        countActiveFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        // カウントが有効か判定
        if (countActiveFlag == true)
        {
            // 有効であれば時間を更新
            timeCounter -= Time.deltaTime;
            seconds = (int)timeCounter;
            // テキストに値を反映
            timerText.text = seconds.ToString();

            // もしカウントが0になっていたら、カウントを無効にする
            if (timeCounter <= 0f) CountStop( );
        }
    }

    /// <summary>
    /// タイマーカウント開始
    /// </summary>
    public void CountStart()
	{
        countActiveFlag = true;
	}

    /// <summary>
    /// タイマーカウントストップ
    /// </summary>
    public void CountStop()
	{
        countActiveFlag = false;
	}
}
