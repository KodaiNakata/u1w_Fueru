using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
/**
 * @file GameSceneDirector.cs
 * @brief ゲーム画面の監督スクリプトのファイル
 * @author Kodai Nakata
 */
/**
 * @class GameSceneDirector
 * @brief ゲーム画面の監督スクリプト用のクラス
 */
public class GameSceneDirector : MonoBehaviour
{
    /**
     * @enum GameStatus 
     * @brief ゲームの状態の列挙体
     */
    public enum GameStatus
    {
        //! スタート
        START,
        //! プレイ
        PLAY,
        //! ゲームオーバー
        GAME_OVER
    }
    //! ゲームオブジェクト
    [SerializeField]
    private GameObject[] gameObjects = default;
    //! 現在のゲームの状態
    private GameStatus currentStatus;
    //! 自クラスのインスタンス
    private static GameSceneDirector instance;
    /**
     * @brief Start関数の前およびプレハブのインスタンス化直後に呼び出される関数
     */
    void Awake()
    {
        instance = GetComponent<GameSceneDirector>();
        ChangeStatus(GameStatus.START);// スタート状態にする
    }

    /**
     * @brief 自クラスのインスタンスを取得する
     * @return インスタンス
     */
    public static GameSceneDirector GetInstance()
    {
        return instance;
    }

    /**
     * @brief 現在のゲーム状態を取得する
     * @return 現在のゲームの状態
     */
    public GameStatus GetCurrentStatus()
    {
        return currentStatus;
    }

    /**
     * @brief 現在のゲームの状態を変更する
     * @param[in] status 変更する状態
     */
    public void ChangeStatus(GameStatus status)
    {
        currentStatus = status;
        // スタート状態のとき
        if (status == GameStatus.START)
        {
            SetActiveStatusOfStart(true);// スタート状態をアクティブにする
            SetActiveStatusOfPlay(false);// プレイ状態を非アクティブにする
            SetActiveStatusOfGameOver(false);// ゲームオーバー状態を非アクティブにする
        }
        // プレイ状態のとき
        else if (status == GameStatus.PLAY)
        {
            SetActiveStatusOfPlay(true);// プレイ状態をアクティブにする
            SetActiveStatusOfStart(false);// スタート状態を非アクティブにする
        }
        // ゲームオーバー状態のとき
        else
        {
            SetActiveStatusOfGameOver(true);// ゲームオーバー状態をアクティブにする
            SetActiveStatusOfPlay(false);// プレイ状態を非アクティブにする
        }
    }

    /**
     * @brief スタート状態をアクティブに設定する
     * @param[in] isActive アクティブにするか
     */
    private void SetActiveStatusOfStart(bool isActive)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            // タイトルまたはスタートのオブジェクトのとき
            if (gameObject.name.Equals("Title") || gameObject.name.Equals("Start"))
            {
                gameObject.SetActive(isActive);// アクティブにするか設定する
            }
        }
    }

    /**
     * @brief プレイ状態をアクティブに設定する
     * @param[in] isActive アクティブにするか
     */
    private void SetActiveStatusOfPlay(bool isActive)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            // プレイヤーまたは弾の管理またはタイマーのオブジェクトのとき
            if (gameObject.name.Equals("Player") || gameObject.name.Equals("BulletManager") || gameObject.name.Equals("Timer"))
            {
                gameObject.SetActive(isActive);// アクティブにするか設定する
            }
        }
    }

    /**
     * @brief ゲームオーバー状態をアクティブに設定する
     * @param[in] isActive アクティブにするか
     */
    private void SetActiveStatusOfGameOver(bool isActive)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            // ゲームオーバーまたは結果のオブジェクトのとき
            if (gameObject.name.Equals("GameOver") || gameObject.name.Equals("Result"))
            {
                gameObject.SetActive(isActive);// アクティブにするか設定する
            }
        }
    }
}
