using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @file BulletGenerator.cs
 * @brief 弾の生成スクリプトのファイル
 * @author Kodai Nakata
 */

/**
 * @class BulletGenerator
 * @brief 弾の生成用のクラス
 */
public class BulletGenerator : MonoBehaviour
{
    //! 自クラスのインスタンス
    private static BulletGenerator instance;
    //! タイマーの区間
    private float timeSpan = 1f;
    //! カウント用のタイマー
    private float timeCount = 0f;
    //! 弾のPrefabのオブジェクト
    [SerializeField]
    private GameObject bulletPrefab = default;
    /**
     * @brief 最初のフレームに入る前に呼び出される関数
     */
    void Start()
    {
    }

    /**
     * @briref 1フレームごとに呼び出される関数
     */
    void Update()
    {
        timeCount += Time.deltaTime;
        if (timeSpan < timeCount)
        {
            CreateBullet();
            timeCount = 0f;
        }
    }

    /**
     * @brief 弾を生成する
     */
    private void CreateBullet()
    {
        GameObject gameObject = Instantiate(bulletPrefab) as GameObject;
    }
}
