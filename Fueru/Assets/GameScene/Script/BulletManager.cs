using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
/**
 * @file BulletManager.cs
 * @brief 弾の管理スクリプトのファイル
 * @author Kodai Nakata
 */

/**
 * @class BulletGenerator
 * @brief 弾の管理用のクラス
 */
public class BulletManager : MonoBehaviour
{
    //! 自クラスのインスタンス
    private static BulletManager instance;
    //! タイマーの区間
    private float timeSpan = 1f;
    //! カウント用のタイマー
    private float timeCount = 0f;
    //! 弾の始点位置のリスト
    private List<Vector2> bulletStartPosList;
    //! 弾のPrefabのオブジェクト
    [SerializeField]
    private GameObject bulletPrefab = default;
    /**
     * @brief 最初のフレームに入る前に呼び出される関数
     */
    void Start()
    {
        instance = GetComponent<BulletManager>();
        bulletStartPosList = new List<Vector2>();
    }

    /**
     * @briref 1フレームごとに呼び出される関数
     */
    void Update()
    {
        timeCount += Time.deltaTime;
        // タイマーが一定期間を超えたとき
        if (timeSpan < timeCount)
        {
            CreateBullet();// 弾を生成する
            timeCount = 0f;// タイマーをリセットする
        }
    }

    /**
     * @brief 弾を生成する
     */
    private void CreateBullet()
    {
        GameObject gameObject = Instantiate(bulletPrefab) as GameObject;
    }

    /**
     * @brief インスタンスを取得する
     * @return インスタンス
     */
    public static BulletManager GetInstance()
    {
        return instance;
    }

    /**
     * @brief 弾の位置リストに追加する
     * @param[out] pos 弾の位置
     */
    public void AddBulletPosList(Vector2 pos)
    {
        bulletStartPosList.Add(pos);
    }

    /**
     * @brief 弾の位置のリストを削除する
     * @param[out] pos 弾の位置
     */
    public void RemoveAddBulletPosList(Vector2 pos)
    {
        List<Vector2> tmpList = bulletStartPosList;
        bulletStartPosList.Remove(pos);
    }

    /**
     * @brief 弾の位置のリストをもとにx座標の始点位置を取得する
     * @param[in] minPosX x座標の最小値
     * @param[in] maxPosX x座標の最大値
     * @return 弾の位置のリストをもとに決めたx座標の始点位置
     */
    public int GetStartRandomPosX(int minPosX, int maxPosX)
    {
        int centerX = (maxPosX - minPosX) / 2;
        int leftPosCount = 0;
        int rightPosCount = 0;
        foreach(Vector2 vector2 in bulletStartPosList)
        {
            if (vector2.x <= centerX)
            {
                leftPosCount++;
            }
            else
            {
                rightPosCount++;
            }
        }
        // 始点位置が右のほうが多いとき
        if(leftPosCount < rightPosCount)
        {
            return minPosX;// 左端を始点位置とする
        }
        // 始点位置が左のほうが多いとき
        else
        {
            return maxPosX;// 右端を始点位置とする
        }
    }

    /**
     * @brief 弾の位置のリストをもとにy座標の始点位置を取得する
     * @param[in] minPosY y座標の最小値
     * @param[in] maxPosY y座標の最大値
     * @return 弾の位置のリストをもとに決めたy座標の始点位置
     */
    public int GetStartRandomPosY(int minPosY, int maxPosY)
    {
        int centerY = (maxPosY - minPosY) / 2;
        int topPosCount = 0;
        int bottomPosCount = 0;
        foreach (Vector2 vector2 in bulletStartPosList)
        {
            if (vector2.y <= centerY)
            {
                bottomPosCount++;
            }
            else
            {
                topPosCount++;
            }
        }
        // 始点位置が上のほうが多いとき
        if (bottomPosCount < topPosCount)
        {
            return minPosY;// 下端を始点位置とする
        }
        // 始点位置が下のほうが多いとき
        else
        {
            return maxPosY;// 上端を始点位置とする
        }
    }
}
