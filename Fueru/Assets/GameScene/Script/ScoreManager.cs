using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
/**
 * @file ScoreManager.cs
 * @brief スコアの管理スクリプトのファイル
 * @author Kodai Nakata
 */

/**
 * @class ScoreManager
 * @brief スコアの管理用のクラス
 */
public class ScoreManager
{
    //! 自クラスのインスタンス
    private static ScoreManager instance;
    //! スコア
    private int score;

    /**
     * @brief 自クラスのインスタンスを取得する
     * @return インスタンス
     */
    public static ScoreManager GetInstance()
    {
        if (instance == null)
        {
            instance = new ScoreManager();
        }
        return instance;
    }

    /**
     * @brief スコアを設定する
     * @param[in] score スコア
     */
    public void SetScore(int score)
    {
        this.score = score;
    }

    /**
     * @brief スコアを取得する
     * @return スコア
     */
    public int GetScore()
    {
        return score;
    }
}
