using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BTLGeek.Manager;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    /* ↓それぞれエディタで任意の値を設定する↓ */
    [SerializeField] private int perfectScore;    // Perfectのスコア
    [SerializeField] private int goodScore;       // Goodのスコア
    [SerializeField] private int badScore;        // Badのスコア

    public int totalScore { get; private set; }    // 画面に表示する秒数     // スコアの合計値


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = totalScore.ToString();

    }

    // 判定結果をもとに指定のスコアを加算する
    public void scoreCalculator(BurgerManager.EVALUATION judgement)
    {
        

        switch (judgement)
        {
            // 判定結果が「Perfect」の時
            case BurgerManager.EVALUATION.Perfect:
                totalScore += perfectScore;
                break;

            // 判定結果が「Good」の時
            case BurgerManager.EVALUATION.Good:
                totalScore += goodScore;
                break;

            // 判定結果が「Bad」の時
            case BurgerManager.EVALUATION.Bad:
                totalScore += badScore;
                break;
        }

    }

}
