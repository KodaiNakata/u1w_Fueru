using DG.Tweening;
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
    //! 時
    private int hour;
    //! 分
    private int minute;
    //! 秒
    private float second;

    /**
     * @brief オブジェクトを有効にした直後に呼び出される関数
     */
    void OnEnable()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        timerText.text = "Result 00:00:00";
        int nowHour = 0;
        int nowMinute = 0;
        int nowSecond = 0;
        int hour = TimerManager.GetInstance().GetHour();
        int minute = TimerManager.GetInstance().GetMinute();
        int second = (int)TimerManager.GetInstance().GetSecond();

        Sequence sequence = DOTween.Sequence();
        sequence.Append(DOTween.To(() => nowSecond, (n) => nowSecond = n, second, 1)// 秒のアニメーション
                        .OnUpdate(() => timerText.text = "Result 00:00:" + nowSecond.ToString("00")))
                .Append(DOTween.To(() => nowMinute, (n) => nowMinute = n, minute, 1)// 分のアニメーション
                        .OnUpdate(() => timerText.text = "Result 00:" + nowMinute.ToString("00") + ":" + second.ToString("00")))
                .Append(DOTween.To(() => nowHour, (n) => nowHour = n, hour, 1)// 時のアニメーション
                        .OnUpdate(() => timerText.text = "Result " + nowHour.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00")));
    }

    /**
     * @brief 1フレームごとに呼び出される関数
     */
    void Update()
    {
    }
}
