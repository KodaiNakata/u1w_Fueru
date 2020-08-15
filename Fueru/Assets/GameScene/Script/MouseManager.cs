using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
/**
 * @file MouseManager.cs
 * @brief マウスの管理スクリプトのファイル
 * @author Kodai Nakata
 */

/**
 * @class MouseManager
 * @brief マウスの管理用のクラス
 */
public class MouseManager
{
    //! 自クラスのインスタンス
    private static MouseManager instance;
    //! 操作できるか
    private bool canControl;

    /**
     * @brief 自クラスのインスタンスを取得する
     * @return インスタンス
     */
    public static MouseManager GetInstance()
    {
        if (instance == null)
        {
            instance = new MouseManager();
        }
        return instance;
    }

    /**
     * @brief 操作を設定する
     * @param[in] control
     */
    public void SetControl(bool control)
    {
        canControl = control;
    }

    /**
     * @brief 操作可能かを取得する
     * @return true 操作可能
     *         false 操作不可能
     */
    public bool GetControl()
    {
        return canControl;
    }
}
