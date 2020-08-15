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
    private const float MIN_POS_X = 3.5f;
    //! y座標の最小値
    private const float MIN_POS_Y = 1.5f;
    //! x座標の最大値
    private const float MAX_POS_X = 16.0f;
    //! y座標の最大値
    private const float MAX_POS_Y = 7.5f;
    //! 自クラスのインスタンス
    private static PlayerController instance;
    //! 捕まれたか
    private bool isCaught;
    //! アニメーション
    private Animator animator;
    //! 弾に当たったか
    private bool isHit;
    //! 当たるアニメーションが開始されたか
    private bool startedHitAnim;

    /**
     * @brief 最初のフレームに入る前に呼び出される関数
     */
    void Start()
    {
        instance = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    /**
     * @brief オブジェクトを有効にした直後に呼び出される関数
     */
    void OnEnable()
    {
        playerPos = new Vector2(10f, 5f);
        isCaught = false;
        isHit = false;
        startedHitAnim = false;
    }

    /**
     * @brief 自クラスのインスタンスを取得する
     * @return インスタンス
     */
    public static PlayerController GetInstance()
    {
        return instance;
    }

    /**
     * @brief プレイヤー上で左クリックが押されたときに呼び出される関数
     */
    void OnMouseDown()
    {
        isCaught = true;
    }

    /**
     * @brief 左クリックが離れたときに呼び出される関数
     */
    void OnMouseUp()
    {
        isCaught = false;
    }

    /**
     * @brief 2Dオブジェクトの侵入を検出する
     * @param[in] other 侵入したオブジェクト
     */
    void OnTriggerEnter2D(Collider2D other)
    {
        isCaught = false;
        isHit = true;
        animator.SetBool("isHit", isHit);
    }

    /**
     * @brief 操作する
     */
    public void Control()
    {
        MovePos();// 座標移動
        CheckHit();// 弾に当たったかを確認
    }

    /**
     * @brief 座標を移動する
     */
    private void MovePos()
    {
        // プレイヤーが捕まれたとき
        if (isCaught)
        {
            playerPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);// マウスの位置を取得
            playerPos = Camera.main.ScreenToWorldPoint(playerPos);// スクリーン座標をワールド座標に変換
            playerPos.z = 0f;// カメラのz座標が-10のため、プレイヤーのz座標を0にする

            // x座標で動ける範囲外(最大値より大きい)のとき
            if (MAX_POS_X < playerPos.x)
            {
                playerPos.x = MAX_POS_X;// ステージ内に収める
            }
            // x座標で動ける範囲外(最小値より小さい)のとき
            else if (playerPos.x < MIN_POS_X)
            {
                playerPos.x = MIN_POS_X;// ステージ内に収める
            }
            // y座標で動ける範囲外(最大値より大きい)のとき
            if (MAX_POS_Y < playerPos.y)
            {
                playerPos.y = MAX_POS_Y;// ステージ内に収める
            }
            // y座標で動ける範囲外(最小値より小さい)のとき
            else if (playerPos.y < MIN_POS_Y)
            {
                playerPos.y = MIN_POS_Y;// ステージ内に収める
            }
            transform.position = playerPos;// ゲーム画面に反映
        }
    }

    private void CheckHit()
    {
        // 弾に当たったとき
        if (isHit)
        {
            // ヒットのアニメーションが開始されていないとき
            if (!startedHitAnim)
            {
                startedHitAnim = StartedHitAnim();// 開始されているかを取得
            }
            // ヒットのアニメーションが開始されたとき
            else
            {
                // ヒットのアニメーションの再生が終了したとき
                if (FinishedHitAnim())
                {
                    GameSceneDirector.GetInstance().ChangeStatus(GameSceneDirector.GameStatus.GAME_OVER);// ゲームオーバー状態にする
                }
            }
        }
    }

    /**
     * @brief ヒットのアニメーションを再生したか
     * @return true 再生開始
     *         false 未再生
     */
    private bool StartedHitAnim()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);// 現在のアニメーションの状態を取得する

        // ヒットのアニメーションと同じハッシュ値のとき
        if (Animator.StringToHash("Player.hit") == stateInfo.fullPathHash)
        {
            return true;
        }
        return false;
    }

    /**
     * @brief ヒットのアニメーション再生が終了したか
     * @return true 再生終了
     *         false 再生中
     */
    private bool FinishedHitAnim()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);// 現在のアニメーションの状態を取得する

        // ヒットのアニメーションを再生し終えたとき
        if (stateInfo.normalizedTime >= 1.0f)
        {
            return true;
        }
        return false;
    }
}
