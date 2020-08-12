using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/**
 * @file ResultTimer.cs
 * @brief 時間の結果スクリプトのファイル
 * @author Kodai Nakata
 */

/**
 * @class ResultTimer
 * @brief 時間の結果用のクラス
 */
public class ResultTimer : MonoBehaviour
{
    //! タイマー表示用テキスト
    private TextMeshProUGUI timerText;

    /**
     * @brief 最初のフレームに入る前に呼び出される関数
     */
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    /**
     * @brief 1フレームごとに呼び出される関数
     */
    void Update()
    {
        timerText.text = "Result " + TimerManager.GetInstance().GetResultOfTime();
    }
}
