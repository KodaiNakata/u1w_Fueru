using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @file PlayerController.cs
 * @brief プレイヤーの操作スクリプトのファイル
 * @author Kodai Nakata
 */

/**
 * @class PlayerController
 * @brief プレイヤーの操作用のクラス
 */
public class PlayerController : MonoBehaviour
{
    //! プレイヤーの位置
    private Vector3 playerPos;
    //! x座標の最小値
    private float minPosX;
    //! y座標の最小値
    private float minPosY;
    //! x座標の最大値
    private float maxPosX;
    //! y座標の最大値
    private float maxPosY;
    /**
     * @brief 最初のフレームに入る前に呼び出される関数
     */
    void Start()
    {
        playerPos = default;
        minPosX = 0f;
        minPosY = 1f;
        maxPosX = 20f;
        maxPosY = 9f;
    }

    /**
     * @briref 1フレームごとに呼び出される関数
     */
    void Update()
    {
        MovePos();// 座標を移動
    }

    /**
     * @brief 座標を移動する
     */
    private void MovePos()
    {
        // マウスの左クリックを押している間
        if (Input.GetMouseButton(0)) {
            playerPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);// マウスの位置を取得
            playerPos = Camera.main.ScreenToWorldPoint(playerPos);// スクリーン座標をワールド座標に変換
            playerPos.z = 0f;// カメラのz座標が-10のため、プレイヤーのz座標を0にする

            // x座標で動ける範囲外(最大値より大きい)のとき
            if (maxPosX < playerPos.x)
            {
                playerPos.x = maxPosX;// ステージ内に収める
            }
            // x座標で動ける範囲外(最小値より小さい)のとき
            else if (playerPos.x < minPosX)
            {
                playerPos.x = minPosX;// ステージ内に収める
            }
            // y座標で動ける範囲外(最大値より大きい)のとき
            if (maxPosY < playerPos.y)
            {
                playerPos.y = maxPosY;// ステージ内に収める
            }
            // y座標で動ける範囲外(最小値より小さい)のとき
            else if (playerPos.y < minPosY)
            {
                playerPos.y = minPosY;// ステージ内に収める
            }
            transform.position = playerPos;// ゲーム画面に反映
        }
    }


    /**
        * @brief 2Dオブジェクトの侵入を検出する
        * @param other 侵入したオブジェクト
        */
    void OnTriggerEnter2D(Collider2D other)
    {
        //@todo 弾と衝突したらゲームオーバーかつ時間の結果を表示させる
    }
}
