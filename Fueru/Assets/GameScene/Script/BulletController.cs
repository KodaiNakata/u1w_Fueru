using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @file BulletController.cs
 * @brief 弾の操作スクリプトのファイル
 * @author Kodai Nakata
 */

/**
 * @class BulletController
 * @brief 弾の操作用のクラス
 */
public class BulletController : MonoBehaviour
{
    //! 弾のオブジェクト
    private GameObject bulletObject;
    //! 弾の向き
    private Vector2 shotForward;
    //! 移動の速さ
    private const float MOVE_SPEED = 1f;
    //! x座標の最小値
    private const int MIN_POS_X = 2;
    //! x座標の最大値
    private const int MAX_POS_X = 19;
    //! y座標の最小値
    private const int MIN_POS_Y = -1;
    //! y座標の最大値
    private const int MAX_POS_Y = 11;
    //! 始点の位置
    private Vector2 startPos;
    /**
     * @brief 最初のフレームに入る前に呼び出される関数
     */
    void Start()
    {
        bulletObject = this.gameObject;// 自身のオブジェクトを取得
        startPos = GetRandomPos();// 始点位置をランダムで取得する
        Vector2 finishPos = GetRandomPos(startPos);// 終点位置ランダムで取得する
        shotForward = Vector2.Scale((finishPos - startPos), new Vector2(1, 1)).normalized;// 向きの生成
        BulletManager.GetInstance().AddBulletPosList(startPos);// 弾の始点位置のリストに追加
        transform.position = startPos;// 始点の位置を反映
    }

    /**
     * @briref 1フレームごとに呼び出される関数
     */
    void Update()
    {
        // ステージ内のとき
        if (OnStage())
        {
            MovePos();// 座標を移動
            // プレイ状態から変化したとき
            if (ChangedGameStatus())
            {
                DestroyImmediate(bulletObject);// ステージ内の弾を破棄
            }
        }
        // ステージ外のとき
        else
        {
            BulletManager.GetInstance().RemoveAddBulletPosList(startPos);// 弾の始点位置のリストから削除
            DestroyImmediate(bulletObject);// ステージ外の弾を破棄
        }
    }

    /**
     * @brief ランダム位置を取得する
     * @return ランダム位置
     */
    private Vector2 GetRandomPos()
    {
        int random = Random.Range(0, 2);// x座標から決めるかy座標から決めるか
        int randomPosX = 0;// ランダムのx座標
        int randomPosY = 0;// ランダムのy座標

        // x座標から決めるとき
        if (random == 0)
        {
            randomPosX = Random.Range(MIN_POS_X, MAX_POS_X + 1);// x座標をランダム取得

            // ランダムで取得したx座標が左端と右端を除いた位置のとき
            if (MIN_POS_X < randomPosX && randomPosX < MAX_POS_X)
            {
                randomPosY = BulletManager.GetInstance().GetStartRandomPosY(MIN_POS_Y, MAX_POS_Y);// 両端のどちらかをランダムで決める
            }
            // ランダムで取得したx座標が左端または右端のとき
            else
            {
                randomPosY = Random.Range(MIN_POS_Y, MAX_POS_Y + 1);// y座標をランダム取得
            }
        }
        // y座標から決めるとき
        else
        {
            randomPosY = Random.Range(MIN_POS_Y, MAX_POS_Y + 1);// y座標をランダム取得

            // ランダムで取得したy座標が下端と上端を除いた位置のとき
            if (MIN_POS_Y < randomPosY && randomPosY < MAX_POS_Y)
            {
                randomPosX = BulletManager.GetInstance().GetStartRandomPosX(MIN_POS_X, MAX_POS_X);// 両端のどちらかをランダムで決める
            }
            // ランダムで取得したy座標が下端または上端のとき
            else
            {
                randomPosX = Random.Range(MIN_POS_X, MAX_POS_X + 1);// x座標をランダム取得
            }
        }
        return new Vector2(randomPosX, randomPosY);
    }

    /**
     * @brief ランダム位置を取得する
     * @param[out] startPos 始点位置
     * @return ランダム位置
     */
    private Vector2 GetRandomPos(Vector2 startPos)
    {
        float randomPosX = 0;
        float randomPosY = 0;
        int centerX = (MAX_POS_X - MIN_POS_X) / 2;
        int centerY = (MAX_POS_Y - MIN_POS_Y) / 2;

        // 始点の位置が左端のとき
        if (startPos.x == MIN_POS_X)
        {
            randomPosX = Random.Range(centerX, MAX_POS_X);// x座標をランダム取得(右半分)
            
            // ランダムで取得したx座標が右端を除いた位置のとき
            if (randomPosX != MAX_POS_X)
            {
                int randomBoth = Random.Range(0, 2);// 両端のどちらかを決める
                if (randomBoth == 0)
                {
                    randomPosY = MIN_POS_Y;// 下端にする
                }
                else
                {
                    randomPosY = MAX_POS_Y;// 上端にする
                }
            }
            // ランダムで取得したx座標が右端のとき
            else
            {
                do
                {
                    randomPosY = Random.Range(MIN_POS_Y, MAX_POS_Y);// y座標をランダム取得
                } while (Mathf.Abs(startPos.y - randomPosY) < 3f);// 始点のy座標と終点のy座標の差が3未満のとき
            }
        }
        // 始点の位置が右端のとき
        else if (startPos.x == MAX_POS_X)
        {
            randomPosX = Random.Range(MIN_POS_X, centerX + 1);// x座標をランダム取得(左半分)

            // ランダムで取得したx座標が左端を除いた位置のとき
            if (randomPosX != MAX_POS_X)
            {
                int randomBoth = Random.Range(0, 2);// 両端のどちらかを決める
                if (randomBoth == 0)
                {
                    randomPosY = MIN_POS_Y;// 下端にする
                }
                else
                {
                    randomPosY = MAX_POS_Y;// 上端にする
                }
            }
            // ランダムで取得したx座標が左端のとき
            else
            {
                do
                {
                    randomPosY = Random.Range(MIN_POS_Y, MAX_POS_Y + 1);// y座標をランダム取得
                } while (Mathf.Abs(startPos.y - randomPosY) < 3f) ;// 始点のy座標と終点のy座標の差が3未満のとき
            }
        }
        // 始点の位置が下端のとき
        else if (startPos.y == MIN_POS_Y)
        {
            randomPosY = Random.Range(centerY, MAX_POS_Y + 1);// y座標をランダム取得(上半分)

            // ランダムで取得したy座標が上端を除いた位置のとき
            if (randomPosY != MAX_POS_Y)
            {
                int randomBoth = Random.Range(0, 2);// 両端のどちらかを決める
                if (randomBoth == 0)
                {
                    randomPosX = MIN_POS_X;// 左端にする
                }
                else
                {
                    randomPosX = MAX_POS_X;// 右端にする
                }
            }
            // ランダムで取得したy座標が上端のとき
            else
            {
                do
                {
                    randomPosX = Random.Range(MIN_POS_X, MAX_POS_X + 1);// x座標をランダム取得
                } while (Mathf.Abs(startPos.x - randomPosX) < 3f);// 始点のx座標と終点のx座標の差が3未満のとき
            }
        }
        // 始点の位置が上端のとき
        else if (startPos.y == MAX_POS_Y)
        {
            randomPosY = Random.Range(MIN_POS_Y, centerY + 1);// y座標をランダム取得(下半分)

            // ランダムで取得したy座標が下端を除いた位置のとき
            if (randomPosY != MIN_POS_Y)
            {
                int randomBoth = Random.Range(0, 2);// 両端のどちらかを決める
                if (randomBoth == 0)
                {
                    randomPosX = MIN_POS_X;// 左端にする
                }
                else
                {
                    randomPosX = MAX_POS_X;// 右端にする
                }
            }
            // ランダムで取得したy座標が下端のとき
            else
            {
                do
                {
                    randomPosX = Random.Range(MIN_POS_X, MAX_POS_X);// x座標をランダム取得
                } while (Mathf.Abs(startPos.x - randomPosX) < 3f);// 始点のx座標と終点のx座標の差が3未満のとき
            }
        }
        return new Vector2(randomPosX, randomPosY);
    }

    /**
     * @brief 座標を移動する
     */
    private void MovePos()
    {
        bulletObject.GetComponent<Rigidbody2D>().velocity = shotForward * MOVE_SPEED;// 始点位置から終点位置に向かって移動
    }

    /**
     * @brief プレイ状態から状態が変化したか
     * @return true 変化あり
     *         false 変化なし
     */
    private bool ChangedGameStatus()
    {
        GameSceneDirector.GameStatus status = GameSceneDirector.GetInstance().GetCurrentStatus();
        // スタート状態またはゲームオーバー状態のとき
        if (status == GameSceneDirector.GameStatus.START || status == GameSceneDirector.GameStatus.GAME_OVER)
        {
            return true;// 変化あり
        }
        return false;// 変化なし
    }

    /**
     * @brief ステージ内か
     * @return true ステージ内
     *         false ステージ外
     */
    private bool OnStage()
    {
        Vector2 pos = bulletObject.transform.position;// 現在の弾の位置を取得

        // x座標がステージ外にあるとき
        if (pos.x < MIN_POS_X || MAX_POS_X < pos.x)
        {
            return false;// ステージ外
        }
        // y座標がステージ外にあるとき
        else if (pos.y < MIN_POS_Y || MAX_POS_Y < pos.y)
        {
            return false;// ステージ外
        }
        return true;// ステージ内
    }
}
