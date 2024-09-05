using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreResultManager : MonoBehaviour
{

    [SerializeField] Text TotalText;
    [SerializeField] Text BaseText;
    [SerializeField] Text BallText;
    [SerializeField] Text ScaleText;

    int totalScore;  //合計得点
    int ballNum;     //ボール総数
    int baseScore;   //基本得点
    float scalePoint;  //得点倍率
    // tart is called before the first frame update
    void Start()
    {
        totalScore = ScoreManager.score;
        baseScore = ScoreManager.basicScore;
        ballNum = ScoreManager.ballNum;
        scalePoint = ScoreManager.pointScale;

        TotalText.text = "" + totalScore;
        BaseText.text = "" + baseScore;
        BallText.text = "" + ballNum;
        ScaleText.text = "" + scalePoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickBack()
    {
        // シーン遷移
        Initiate.Fade("ScoreHomeScene", new Color(0, 0, 0, 1.0f), 5.0f);
    }
}
