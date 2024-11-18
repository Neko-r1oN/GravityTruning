using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;                   //DOTween���g���Ƃ��͂���using������
using System.Threading.Tasks;
using KanKikuchi.AudioManager;       //AudioManager���g���Ƃ��͂���using������

public class SAFieldManager : MonoBehaviour
{
    [SerializeField] GameObject gravity;
    [SerializeField] GameObject grid;

    private bool turnL;
    private bool turnR;
    private bool turnFlag;

    private bool button;
    private float angle;
    private bool isGravity;

    private bool buttonEnabled = true;
    private WaitForSeconds waitOneSecond = new WaitForSeconds(0.3f);

    //�J�n�J�E���g�_�E��
    float countDown = 5.0f;
    //�J�n�J�E���g�_�E��
    float countTimer = 40.0f;

    private bool isGame;

    private void Start()
    {
        turnL = false;
        turnR = false;
        turnFlag = true;
        isGravity = true;
        angle = 0f;

        isGame = false;


    }
    // Update is called once per frame
    void Update()
    {
        if (countDown >= 0)
        {
            countDown -= Time.deltaTime;

        }

        if (countDown <= 0)
        {
            isGame = true;

            countTimer -= Time.deltaTime;
            //countdown��0�ȉ��ɂȂ����Ƃ�
            if (countTimer <= 0)
            {
                isGame = false;
            }

            else
            {

            }

            if (turnL == true || Input.GetKeyDown(KeyCode.RightArrow))
            {

                if (turnFlag == true)
                {
                    GameManager.masterTurnNum--;
                    CancelInvoke("ResetGrid");
                    angle -= 60f;


                    turnL = false;
                    //��
                    //transform.Rotate(0f, 0f, -60f * Time.deltaTime);

                    Invoke("ResetGrid", 0.7f);
                }
            }


            if (turnR == true || Input.GetKeyDown(KeyCode.LeftArrow))
            {

                if (turnFlag == true)
                {
                    GameManager.masterTurnNum--;
                    CancelInvoke("ResetGrid");
                    angle += 60f;

                    turnR = false;
                    //�E
                    //transform.Rotate(0f, 0f, 60f * Time.deltaTime);
                    Invoke("ResetGrid", 0.7f);
                }

            }

            this.transform.DORotate(new Vector3(-4.869f, 0f, angle), 0.3f);

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
        if (countDown <= 0)
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
                    grid.SetActive(false);
                    turnL = true;
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
        if (countDown <= 0)
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
                    grid.SetActive(false);
                    turnR = true;
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
        if (countDown <= 0)
        {
            if (isGame)
            { 
                if (isGravity)
                {
                    gravity.SetActive(false);
                    isGravity = false;

                    Invoke("ResetGravity", 1.0f);
                }
            }
        }

    }

    private void ResetGravity()
    {
        gravity.SetActive(true);
        isGravity = true;
    }

    private void ResetGrid()
    {
        grid.SetActive(true);
    }

    // �{�^���̐�������������R���[�`��
    private IEnumerator EnableButton()
    {
        // 1�b��ɉ���         
        yield return waitOneSecond;
        buttonEnabled = true;
    }

}
