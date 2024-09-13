using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KanKikuchi.AudioManager;       //AudioManager���g���Ƃ��͂���using������

public class ScoreResultManager : MonoBehaviour
{

    [SerializeField] Text TotalText;
    [SerializeField] Text BaseText;
    [SerializeField] Text BallText;
    [SerializeField] Text ScaleText;

    int totalScore;  //���v���_
    int ballNum;     //�{�[������
    int baseScore;   //��{���_
    float scalePoint;  //���_�{��
    // tart is called before the first frame update
    void Start()
    {
        BGMManager.Instance.Play(
                        audioPath: BGMPath.SCORE_HOME, //�Đ��������I�[�f�B�I�̃p�X
                        volumeRate: 0.3f,                //���ʂ̔{��
                        delay: 0,                //�Đ������܂ł̒x������
                        pitch: 1,                //�s�b�`
                        isLoop: true,             //���[�v�Đ����邩
                        allowsDuplicate: false             //����BGM�Əd�����čĐ������邩
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
           audioPath: SEPath.TAP, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.2f,                //���ʂ̔{��
           delay: 0,                //�Đ������܂ł̒x������
           pitch: 1,                //�s�b�`
           isLoop: false             //���[�v�Đ����邩
           );
        // �V�[���J��
        Initiate.Fade("ScoreHomeScene", new Color(0, 0, 0, 1.0f), 5.0f);
    }
}
