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

        if (seconds <= 0)
        {
            countActiveFlag = false;
        }

    }

}
