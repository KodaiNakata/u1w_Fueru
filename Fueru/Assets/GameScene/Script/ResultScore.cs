using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/**
 * @file ResultScore.cs
 * @brief スコアの結果スクリプトのファイル
 * @author Kodai Nakata
 */

/**
 * @class ResultScore
 * @brief スコアの結果用のクラス
 */
public class ResultScore : MonoBehaviour
{
    /**
     * @brief オブジェクトを有効にした直後に呼び出される関数
     */
    void OnEnable()
    {
        TextMeshProUGUI scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = "Result 0000";
        int nowScore = 0;
        int score = ScoreManager.GetInstance().GetScore();// スコア管理から取得する
        GameObject returnObject = GameObject.Find("Return");// 戻るオブジェクトを取得
        Animator returnAnim = returnObject.GetComponent<Animator>();// 戻るのアニメーションを取得
        Sequence sequence = DOTween.Sequence();
        sequence
            .AppendInterval(1f) // 各アニメーション間の待機
            .Append(DOTween.To(() => nowScore, (n) => nowScore = n, score, 1)// スコアのアニメーション
                        .OnUpdate(() => scoreText.text = "Result " + nowScore.ToString("0000"))
                        .OnComplete(() =>
                        {
                            returnAnim.SetBool("flash", true);// 点滅アニメーションへ移行
                            MouseManager.GetInstance().SetControl(true);// マウスの操作可能にする
                        }));
    }
}
