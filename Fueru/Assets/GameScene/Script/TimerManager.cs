using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
/**
 * @file TimerManager.cs
 * @brief タイマーの管理スクリプトのファイル
 * @author Kodai Nakata
 */

/**
 * @class TimerManager
 * @brief タイマーの管理用のクラス
 */
public class TimerManager : MonoBehaviour
{
    //! 自クラスのインスタンス
    private static TimerManager instance;
    //! 時
    private int hour;
    //! 分
    private int minute;
    //! 秒
    private float second;

    /**
     * @brief 最初のフレームに入る前に呼び出される関数
     */
    void Start()
    {
        hour = 0;
        minute = 0;
        second = 0f;
        instance = GetComponent<TimerManager>();
    }

    /**
     * @brief 自クラスのインスタンスを取得する
     * @return インスタンス
     */
    public static TimerManager GetInstance()
    {
        return instance;
    }

    /**
     * @brief 時間を設定する
     * @param[in] hour 時
     * @param[in] minute 分
     * @param[in] second 秒
     */
    public void SetTime(int hour, int minute, float second)
    {
        this.hour = hour;
        this.minute = minute;
        this.second = second;
    }

    /**
     * @brief 時間の結果を取得する
     * @return 時間の結果
     */
    public string GetResultOfTime()
    {
        return hour.ToString("00") + ":" + minute.ToString("00") + ":" + ((int)second).ToString("00");
    }
}
