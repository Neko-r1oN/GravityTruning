using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;                   //DOTween���g���Ƃ��͂���using������
using System.Threading.Tasks;
using KanKikuchi.AudioManager;       //AudioManager���g���Ƃ��͂���using������
public class FieldManager : MonoBehaviour
{
    [SerializeField] GameObject gravity;    //�d��
    [SerializeField] GameObject grid;       //�{�[�������K�C�h

    private bool turnL;
    private bool turnR;
    private bool turnFlag;

    private bool button;
    private float angle;
    private bool isGravity;

    private bool buttonEnabled = true;
    private WaitForSeconds waitOneSecond = new WaitForSeconds(0.3f);
    

    private int turnNum;

    private void Start()
    {
        turnL = false;
        turnR = false;
        turnFlag = true;
        isGravity = true;
        angle = 0f;
        
    }
    // Update is called once per frame
    void Update()
    {

        turnNum = GameManager.masterTurnNum;
        if (turnL == true || Input.GetKeyDown(KeyCode.RightArrow))
        {
          
            if (turnNum > 0 && turnFlag == true)
            {
                GameManager.masterTurnNum--;
                turnNum = GameManager.masterTurnNum;
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
           
            if (turnNum > 0 && turnFlag == true)
            {
                GameManager.masterTurnNum--;
                turnNum = GameManager.masterTurnNum;
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


    public void TurnLeft()
    {
        SEManager.Instance.Play(
            audioPath: SEPath.TAP, //�Đ��������I�[�f�B�I�̃p�X
            volumeRate: 0.2f,                //���ʂ̔{��
            delay: 0,                //�Đ������܂ł̒x������
            pitch: 1,                //�s�b�`
            isLoop: false             //���[�v�Đ����邩
            );
        // �������͓��삳���Ȃ�
        if (buttonEnabled == false)
        {
            return;
        }
        // ��������Ă��Ȃ��ꍇ
        else
        {

            Debug.Log(turnNum);
            turnNum--;
            // �{�^���𐧌�����
            buttonEnabled = false;

            // ��莞�Ԍo�ߌ�ɉ���
            StartCoroutine(EnableButton());
            grid.SetActive(false);
            turnL = true;
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
        // �������͓��삳���Ȃ�
        if (buttonEnabled == false)
        {
            return;
        }
        // ��������Ă��Ȃ��ꍇ
        else
        {

            Debug.Log(turnNum);
            turnNum--;
            // �{�^���𐧌�����
            buttonEnabled = false;

            // ��莞�Ԍo�ߌ�ɉ���
            StartCoroutine(EnableButton());
            grid.SetActive(false);
            turnR = true;
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
        if (isGravity)
        {
            gravity.SetActive(false);
            isGravity = false;

            Invoke("ResetGravity", 1.0f);
        }
        else
        {
           
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
        if (turnNum <= 0)
        {
            turnFlag = false;
        }
    }

    // �{�^���̐�������������R���[�`��
    private IEnumerator EnableButton()
    {
        // 1�b��ɉ���         
        yield return waitOneSecond;
        buttonEnabled = true;
    }

}
