using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @file MouseController.cs
 * @brief マウス操作スクリプトのファイル
 * @author Kodai Nakata
 */

/**
 * @class MouseController
 * @brief マウス操作用のクラス
 */

public class MouseController : MonoBehaviour
{
    /**
     * @briref 1フレームごとに呼び出される関数
     */
    void Update()
    {
        GameSceneDirector.GameStatus status = GameSceneDirector.GetInstance().GetCurrentStatus();

        // スタート状態のとき
        if (status == GameSceneDirector.GameStatus.START)
        {
            ControlStartScene();// スタート画面を操作する
        }
        // プレイ状態のとき
        else if (status == GameSceneDirector.GameStatus.PLAY)
        {
            PlayerController.GetInstance().Control();// プレイヤーを操作する
        }
        // ゲームオーバー状態のとき
        else
        {
            ControlGameOverScene();// ゲームオーバー画面を操作する
        }
    }

    /**
     * @brief スタート画面を操作する
     */
    private void ControlStartScene()
    {
        // 左クリックしたとき
        if (Input.GetMouseButtonDown(0))
        {
            GameSceneDirector.GetInstance().ChangeStatus(GameSceneDirector.GameStatus.PLAY);// プレイ状態へ遷移する
        }
    }

    /**
     * @brief ゲームオーバー画面を操作する
     */
    private void ControlGameOverScene()
    {
        // 左クリックしたとき
        if (MouseManager.GetInstance().GetControl() && Input.GetMouseButtonDown(0))
        {
            GameObject returnObject = GameObject.Find("Return");// 戻るオブジェクトを取得
            Animator returnAnim = returnObject.GetComponent<Animator>();// 戻るのアニメーションを取得
            returnAnim.SetBool("flash", false);// 透明アニメーションへ移行
            GameSceneDirector.GetInstance().ChangeStatus(GameSceneDirector.GameStatus.START);// スタート状態へ遷移する
        }
    }
}
