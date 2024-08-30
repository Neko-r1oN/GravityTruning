using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    
    //�X�R�A
    static public int score { get; private set; }

    //�������{�[������
    static public int ballNum { get; private set; }

    //��{�|�C���g
    static public int basicScore { get; private set; }

    //�|�C���g�{��
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

        //�X�R�A�X�V
        score += basicScore;
        ballNum++;

    }
}
