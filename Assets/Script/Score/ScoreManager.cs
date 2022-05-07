using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    /* ↓それぞれエディタで任意の値を設定する↓ */
    public int perfectScore;    // Perfectのスコア
    public int goodScore;       // Goodのスコア
    public int badScore;        // Badのスコア


    private int totalScore;     // スコアの合計値

    public int testHensu;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreCalculator(testHensu);
        scoreText.text = totalScore.ToString();

    }

    // 判定結果をもとに指定のスコアを加算する
    public void scoreCalculator(int judgement)
    {
        switch (judgement)
        {
            // 判定結果が「Perfect」の時
            case 1:
                totalScore += perfectScore;
                break;

            // 判定結果が「Good」の時
            case 2:
                totalScore += goodScore;
                break;

            // 判定結果が「Bad」の時
            case 3:
                totalScore += badScore;
                break;
        }

    }
}
