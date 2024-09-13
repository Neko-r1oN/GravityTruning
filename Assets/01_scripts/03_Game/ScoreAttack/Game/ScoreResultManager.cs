using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KanKikuchi.AudioManager;       //AudioManagerを使うときはこのusingを入れる

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
        BGMManager.Instance.Play(
                        audioPath: BGMPath.SCORE_HOME, //再生したいオーディオのパス
                        volumeRate: 0.3f,                //音量の倍率
                        delay: 0,                //再生されるまでの遅延時間
                        pitch: 1,                //ピッチ
                        isLoop: true,             //ループ再生するか
                        allowsDuplicate: false             //他のBGMと重複して再生させるか
                        );

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
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //再生したいオーディオのパス
           volumeRate: 0.2f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
           );
        // シーン遷移
        Initiate.Fade("ScoreHomeScene", new Color(0, 0, 0, 1.0f), 5.0f);
    }
}
