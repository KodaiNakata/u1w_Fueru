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
    private const float MIN_POS_X = -2f;
    //! x座標の最大値
    private const float MAX_POS_X = 22f;
    //! y座標の最小値
    private const float MIN_POS_Y = -1f;
    //! y座標の最大値
    private const float MAX_POS_Y = 11f;
    /**
     * @brief 最初のフレームに入る前に呼び出される関数
     */
    void Start()
    {
        bulletObject = this.gameObject;// 自身のオブジェクトを取得
        Vector2 startPos = GetRandomPos();// 始点位置をランダムで取得する
        Vector2 finishPos = GetRandomPos(startPos);// 終点位置ランダムで取得する
        shotForward = Vector2.Scale((finishPos - startPos), new Vector2(1, 1)).normalized;// 向きの生成
        transform.position = startPos;// 始点の位置を反映
    }

    /**
     * @brief ランダム位置を取得する
     * @return ランダム位置
     */
    private Vector2 GetRandomPos()
    {
        int random = Random.Range(0, 1);// x座標から決めるかy座標から決めるか
        float randomPosX = 0f;// ランダムのx座標
        float randomPosY = 0f;// ランダムのy座標

        // x座標から決めるとき
        if (random == 0)
        {
            randomPosX = Random.Range(MIN_POS_X, MAX_POS_X);// x座標をランダム取得

            // ランダムで取得したx座標が左端と右端を除いた位置のとき
            if (MIN_POS_X < randomPosX && randomPosX < MAX_POS_X)
            {
                int randomBoth = Random.Range(0, 1);// 両端のどちらかを決める
                if (randomBoth == 0)
                {
                    randomPosY = MIN_POS_Y;// 下端にする
                }
                else
                {
                    randomPosY = MAX_POS_Y;// 上端にする
                }
            }
            // ランダムで取得したx座標が左端または右端のとき
            else
            {
                randomPosY = Random.Range(MIN_POS_Y, MAX_POS_Y);// y座標をランダム取得
            }
        }
        // y座標から決めるとき
        else
        {
            randomPosY = Random.Range(MIN_POS_Y, MAX_POS_Y);// y座標をランダム取得

            // ランダムで取得したy座標が下端と上端を除いた位置のとき
            if (MIN_POS_Y < randomPosY && randomPosY < MAX_POS_Y)
            {
                int randomBoth = Random.Range(0, 1);// 両端のどちらかを決める
                if (randomBoth == 0)
                {
                    randomPosX = MIN_POS_X;// 左端にする
                }
                else
                {
                    randomPosX = MAX_POS_X;// 右端にする
                }
            }
            // ランダムで取得したy座標が下端または上端のとき
            else
            {
                randomPosX = Random.Range(MIN_POS_X, MAX_POS_X);// x座標をランダム取得
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
        float randomPosX = 0f;
        float randomPosY = 0f;
        float centerX = (MAX_POS_X - MIN_POS_X) / 2f;
        float centerY = (MAX_POS_Y - MIN_POS_Y) / 2f;

        // 始点の位置が左端のとき
        if (startPos.x == MIN_POS_X)
        {
            randomPosX = Random.Range(centerX, MAX_POS_X);// x座標をランダム取得(右半分)
            
            // ランダムで取得したx座標が右端を除いた位置のとき
            if (randomPosX != MAX_POS_X)
            {
                int randomBoth = Random.Range(0, 1);// 両端のどちらかを決める
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
                do {
                    randomPosY = Random.Range(MIN_POS_Y, MAX_POS_Y);// y座標をランダム取得
                } while (Mathf.Abs(startPos.y - randomPosY) < 1f);// 始点のy座標と終点のy座標の差が1未満のとき
            }
        }
        // 始点の位置が右端のとき
        else if (startPos.x == MAX_POS_X)
        {
            randomPosX = Random.Range(MIN_POS_X, centerX);// x座標をランダム取得(左半分)

            // ランダムで取得したx座標が左端を除いた位置のとき
            if (randomPosX != MAX_POS_X)
            {
                int randomBoth = Random.Range(0, 1);// 両端のどちらかを決める
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
                do {
                    randomPosY = Random.Range(MIN_POS_Y, MAX_POS_Y);// y座標をランダム取得
                } while (Mathf.Abs(startPos.y - randomPosY) < 1f) ;// 始点のy座標と終点のy座標の差が1未満のとき
            }
        }
        // 始点の位置が下端のとき
        else if (startPos.y == MIN_POS_Y)
        {
            randomPosY = Random.Range(centerY, MAX_POS_Y);// y座標をランダム取得(上半分)

            // ランダムで取得したy座標が上端を除いた位置のとき
            if (randomPosY != MAX_POS_Y)
            {
                int randomBoth = Random.Range(0, 1);// 両端のどちらかを決める
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
                do {
                    randomPosX = Random.Range(MIN_POS_X, MAX_POS_X);// x座標をランダム取得
                } while (Mathf.Abs(startPos.x - randomPosX) < 1f);// 始点のx座標と終点のx座標の差が1未満のとき
            }
        }
        // 始点の位置が上端のとき
        else if (startPos.y == MAX_POS_Y)
        {
            randomPosY = Random.Range(MIN_POS_Y, centerY);// y座標をランダム取得(下半分)

            // ランダムで取得したy座標が下端を除いた位置のとき
            if (randomPosY != MIN_POS_Y)
            {
                int randomBoth = Random.Range(0, 1);// 両端のどちらかを決める
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
                do {
                    randomPosX = Random.Range(MIN_POS_X, MAX_POS_X);// x座標をランダム取得
                } while (Mathf.Abs(startPos.x - randomPosX) < 1f);// 始点のx座標と終点のx座標の差が1未満のとき
            }
        }
        return new Vector2(randomPosX, randomPosY);
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
        bulletObject.GetComponent<Rigidbody2D>().velocity = shotForward * MOVE_SPEED;
    }
}
