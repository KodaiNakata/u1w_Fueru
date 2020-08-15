using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/**
 * @file ScoreController.cs
 * @brief スコアの操作スクリプトのファイル
 * @author Kodai Nakata
 */

/**
 * @class ScoreController
 * @brief スコアの操作用のクラス
 */
public class ScoreController : MonoBehaviour
{
    //! 秒
    private float second;
    //! スコア
    private int score;
    //! スコア表示用テキスト
    private TextMeshProUGUI scoreText;
    //! スコアの最大値
    private const int MAX_SCORE = 9999;

    /**
     * @brief オブジェクトを有効にした直後に呼び出される関数
     */
    void OnEnable()
    {
        second = 0f;
        score = 0;
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = "SCORE 0000";
    }

    /**
     * @briref 1フレームごとに呼び出される関数
     */
    void Update()
    {
        UpdateScore();// タイマーを更新する
        ScoreManager.GetInstance().SetScore(score);// スコアの管理用も設定する
    }

    /**
     * @brief スコアを更新する
     */
    private void UpdateScore()
    {
        second += Time.deltaTime;

        // 1秒以上経ったとき
        if (second >= 1f)
        {
            score++;// スコアを1増やす
            if (MAX_SCORE < score)
            {
                score = MAX_SCORE;
            }
            scoreText.text = "SCORE " + score.ToString("0000");// スコアを更新
            second = 0f;// 秒数をリセット
        }
    }
}
