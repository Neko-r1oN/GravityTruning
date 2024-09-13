using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;                   //DOTween���g���Ƃ��͂���using������
using System.Threading.Tasks;
using KanKikuchi.AudioManager;       //AudioManager���g���Ƃ��͂���using������

public class SAFieldManager : MonoBehaviour
{
    [SerializeField] GameObject Gravity;
    [SerializeField] GameObject Grid;

    private bool TurnL;
    private bool TurnR;
    private bool TurnFlag;

    private bool button;
    private float Angle;
    private bool isGravity;

    private bool buttonEnabled = true;
    private WaitForSeconds waitOneSecond = new WaitForSeconds(0.3f);

    //�J�n�J�E���g�_�E��
    float CountDown = 5.0f;
    //�J�n�J�E���g�_�E��
    float CountTimer = 40.0f;

    private bool isGame;

    private void Start()
    {
        TurnL = false;
        TurnR = false;
        TurnFlag = true;
        isGravity = true;
        Angle = 0f;

        isGame = false;


    }
    // Update is called once per frame
    void Update()
    {
        if (CountDown >= 0)
        {
            CountDown -= Time.deltaTime;

        }

        if (CountDown <= 0)
        {
            isGame = true;

            CountTimer -= Time.deltaTime;
            //countdown��0�ȉ��ɂȂ����Ƃ�
            if (CountTimer <= 0)
            {
                isGame = false;
            }

            else
            {

            }

            if (TurnL == true || Input.GetKeyDown(KeyCode.RightArrow))
            {

                if (TurnFlag == true)
                {
                    GameManager.MasterTurnNum--;
                    CancelInvoke("ResetGrid");
                    Angle -= 60f;


                    TurnL = false;
                    //��
                    //transform.Rotate(0f, 0f, -60f * Time.deltaTime);

                    Invoke("ResetGrid", 0.7f);
                }
            }


            if (TurnR == true || Input.GetKeyDown(KeyCode.LeftArrow))
            {

                if (TurnFlag == true)
                {
                    GameManager.MasterTurnNum--;
                    CancelInvoke("ResetGrid");
                    Angle += 60f;

                    TurnR = false;
                    //�E
                    //transform.Rotate(0f, 0f, 60f * Time.deltaTime);
                    Invoke("ResetGrid", 0.7f);
                }

            }

            this.transform.DORotate(new Vector3(-4.869f, 0f, Angle), 0.3f);

        }


    }


    public void TurnLeft()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.2f,                //���ʂ̔{��
           delay: 0,                //�Đ������܂ł̒x������
           pitch: 1,                //�s�b�`
           isLoop: false             //���[�v�Đ����邩
           );
        if (CountDown <= 0)
        {
            if (isGame)
            {
                // �������͓��삳���Ȃ�
                if (buttonEnabled == false)
                {
                    return;
                }
                // ��������Ă��Ȃ��ꍇ
                else
                {
                    // �{�^���𐧌�����
                    buttonEnabled = false;

                    // ��莞�Ԍo�ߌ�ɉ���
                    StartCoroutine(EnableButton());
                    Grid.SetActive(false);
                    TurnL = true;
                }
            }
        }
    }

    public void TurnRight()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.2f,                //���ʂ̔{��
           delay: 0,                //�Đ������܂ł̒x������
           pitch: 1,                //�s�b�`
           isLoop: false             //���[�v�Đ����邩
           );
        if (CountDown <= 0)
        {
            if (isGame)
            {
                // �������͓��삳���Ȃ�
                if (buttonEnabled == false)
                {
                    return;
                }
                // ��������Ă��Ȃ��ꍇ
                else
                {
                    // �{�^���𐧌�����
                    buttonEnabled = false;

                    // ��莞�Ԍo�ߌ�ɉ���
                    StartCoroutine(EnableButton());
                    Grid.SetActive(false);
                    TurnR = true;
                }
            }
        }
    }

    public void GravityChange()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.2f,                //���ʂ̔{��
           delay: 0,                //�Đ������܂ł̒x������
           pitch: 1,                //�s�b�`
           isLoop: false             //���[�v�Đ����邩
           );
        if (CountDown <= 0)
        {
            if (isGame)
            { 
                if (isGravity)
                {
                    Gravity.SetActive(false);
                    isGravity = false;

                    Invoke("ResetGravity", 1.0f);
                }
            }
        }

    }

    private void ResetGravity()
    {
        Gravity.SetActive(true);
        isGravity = true;
    }

    private void ResetGrid()
    {
        Grid.SetActive(true);
    }

    // �{�^���̐�������������R���[�`��
    private IEnumerator EnableButton()
    {
        // 1�b��ɉ���         
        yield return waitOneSecond;
        buttonEnabled = true;
    }

}
