using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/**
 * @file TimerController.cs
 * @brief タイマーの操作スクリプトのファイル
 * @author Kodai Nakata
 */

/**
 * @class TimerController
 * @brief タイマーの操作用のクラス
 */
public class TimerController : MonoBehaviour
{
    //! 時
    private int hour;
    //! 分
    private int minute;
    //! 秒
    private float second;
    //! 前の秒数
    private float oldSecond;
    //! タイマー表示用テキスト
    private TextMeshProUGUI timerText;

    /**
     * @brief オブジェクトを有効にした直後に呼び出される関数
     */
    void OnEnable()
    {
        hour = 0;
        minute = 0;
        second = 0f;
        oldSecond = 0f;
        timerText = GetComponent<TextMeshProUGUI>();
        timerText.text = "TIME 00:00:00";
    }

    /**
     * @briref 1フレームごとに呼び出される関数
     */
    void Update()
    {
        UpdateTimer();// タイマーを更新する
        TimerManager.GetInstance().SetTime(hour, minute, second);// タイマーの管理用の時間も設定する
    }

    /**
     * @brief タイマーを更新する
     */
    private void UpdateTimer()
    {
        second += Time.deltaTime;

        // 60秒以上のとき
        if (second >= 60f)
        {
            minute++;// 1分増やす
            second = 0f;// 秒数をリセット
        }
        // 60分以上のとき
        if (minute >= 60)
        {
            hour++;// 1時間増やす
            minute = 0;// 分をリセット
        }
        // 更新前の秒数と更新後の秒数が違うとき
        if ((int)second != (int)oldSecond)
        {
            timerText.text = "TIME " + hour.ToString("00") + ":" + minute.ToString("00") + ":" + ((int)second).ToString("00");// タイマーを更新
        }
        oldSecond = second;// 更新前の秒数に格納
    }
}
