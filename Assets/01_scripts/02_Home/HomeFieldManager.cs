using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;                   //DOTween���g���Ƃ��͂���using������
using KanKikuchi.AudioManager;       //AudioManager���g���Ƃ��͂���using������

public class HomeFieldManager : MonoBehaviour
{
    [SerializeField] GameObject Gravity;

    private bool isStart;
    private bool isScore;
    private float Angle;
    private bool isGravity;

    private bool isClick1;
    private bool isClick2;

    int num1;
    int num2;


    // Start is called before the first frame update
    void Start()
    {

        isStart = false;
        isScore = false;
        Angle = 0f;

        isClick1 = false;
        isClick2 = false;

        num1 = 0;
        num2 = 0;

        isGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart == true && isClick1 == true)
        {
           Angle += 60f;

            isClick1 = false;
            
            Invoke("GravityChange", 1.0f);
        }

        if (isScore == true && isClick2 == true)
        {
            Angle += 60f;

            isClick2 = false;
           
            Invoke("GravityChange", 1.0f);
            
        }

        

        this.transform.DORotate(new Vector3(-4.869f, 0f, Angle), 0.3f);

    }

    public void IsStart()
    {
       
        if (!isClick1 && num1 ==0)
        {
            SEManager.Instance.Play(
           audioPath: SEPath.TAP, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.2f,                //���ʂ̔{��
           delay: 0,                //�Đ������܂ł̒x������
           pitch: 1,                //�s�b�`
           isLoop: false             //���[�v�Đ����邩
           );
            isStart = true;
            isClick1 = true;
            num1++;
        }
        
       
        
    }

    public void isStartScore()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.2f,                //���ʂ̔{��
           delay: 0,                //�Đ������܂ł̒x������
           pitch: 1,                //�s�b�`
           isLoop: false             //���[�v�Đ����邩
           );
        if (!isScore && num2 == 0)
        {
            isScore = true;
            isClick2 = true;
            num2++;
        }
        Invoke("ChangeScene", 3.5f);
    }

    public void GravityChange()
    {
        
        if (isGravity)
          {
              Gravity.SetActive(false);
              isGravity = false;

              Invoke("ResetGravity", 1.0f);
          }
    }

    private void ResetGravity()
    {
        Gravity.SetActive(true);
        isGravity = true;
    }

    public void ChangeScene()
    {
      
        // �V�[���J��
        Initiate.Fade("ScoreHomeScene", new Color(0, 0, 0, 1.0f), 7.0f);
    }
}

