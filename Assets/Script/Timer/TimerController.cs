using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    public Text timerText;
    public float timeLimit;  // 制限時間
    float timeCounter;       // 内部処理用
    int seconds;
    bool countActiveFlag;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = timeLimit;
        countActiveFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (countActiveFlag == true)
        {
            timeCounter -= Time.deltaTime;
            seconds = (int)timeCounter;
            timerText.text = seconds.ToString();
        }

    }

    // 時間切れ判定
    void TimeOverChecker()
    {
        if (seconds <= 0)
        {

        }
    }

}
