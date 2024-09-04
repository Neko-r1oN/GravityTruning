using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    
    //スコア
    static public int score { get; private set; }

    //消したボール総数
    static public int ballNum { get; private set; }

    //基本ポイント
    static public int basicScore { get; private set; }

    //ポイント倍率
    static public float pointScale { get; private set; }


    void Start()
    {
        pointScale = 1.0f;
        basicScore = 200 *(int)pointScale;
        score = 0;
        ballNum = 0;
    }


    void Update()
    {
        GetComponent<Text>().text = "" + score;
        //
    }
    public void AddScore()
    {

        //スコア更新
        score += basicScore;
        ballNum++;

    }
}
