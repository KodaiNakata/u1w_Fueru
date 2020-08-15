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
 * @class BulletManager
 * @brief 弾の管理用のクラス
 */
public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab = default;
    //! 自クラスのインスタンス
    private static BulletManager instance;
    //! タイマーの区間
    private float timeSpan;
    //! カウント用のタイマー
    private float timeCount;
    //! 弾の始点位置のリスト
    private List<Vector2> bulletStartPosList;
    //! 弾のPrefabのオブジェクト
    //! タイマーの区間の最小値
    private const float MIN_TIMER_SPAN = 0.05f;
    //! 弾の追加の間隔
    private const int ADD_BULLET_SPAN = 10;
    //! 追加回数
    private int addCount;
    /**
     * @brief 最初のフレームに入る前に呼び出される関数
     */
    void Start()
    {
        instance = GetComponent<BulletManager>();
        bulletStartPosList = new List<Vector2>();
    }

    /**
     * @brief オブジェクトを有効にした直後に呼び出される関数
     */
    void OnEnable()
    {
        addCount = 1;
        timeSpan = 1f;
        timeCount = 0f;
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
        // スコアが30の倍数になったとき
        if (ScoreManager.GetInstance().GetScore() == ADD_BULLET_SPAN * addCount)
        {
            addCount++;// 追加をカウント
            ShortenTimeSpan();// タイム区間を短くする
        }
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
     * @brief 初期化する
     */
    public void Init()
    {
        addCount = 1;
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
            // 左半分の位置のとき
            if (vector2.x <= centerX)
            {
                leftPosCount++;// 左半分としてカウント
            }
            // 右半分の位置のとき
            else
            {
                rightPosCount++;// 右半分としてカウント
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
            // 下半分の位置のとき
            if (vector2.y <= centerY)
            {
                bottomPosCount++;// 下半分としてカウント
            }
            // 上半分の位置のとき
            else
            {
                topPosCount++;// 上半分としてカウント
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

    /**
     * @brief 弾を生成する
     */
    private void CreateBullet()
    {
        GameObject gameObject = Instantiate(bulletPrefab) as GameObject;
    }

    /**
     * @brief タイム区間を短くする
     */
    private void ShortenTimeSpan()
    {
        timeSpan -= 0.1f;
        // 最小のタイム区間より短いとき
        if (timeSpan < MIN_TIMER_SPAN)
        {
            timeSpan = MIN_TIMER_SPAN;
        }
    }
}
