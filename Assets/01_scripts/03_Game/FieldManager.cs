using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;                   //DOTween���g���Ƃ��͂���using������
using System.Threading.Tasks;

public class FieldManager : MonoBehaviour
{
    [SerializeField] GameObject Gravity;

    private bool TurnL;
    private bool TurnR;

    private bool button;
    private float Angle;
    private bool isGravity;

    private bool buttonEnabled = true;
    private WaitForSeconds waitOneSecond = new WaitForSeconds(0.3f);


    private void Start()
    {
        TurnL = false;
        TurnR = false;
        isGravity = true;
        Angle = 0f;
    }
    // Update is called once per frame
    void Update()
    {
       

            if (TurnL == true || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Angle -= 60f;


                TurnL = false;
                //��
                //transform.Rotate(0f, 0f, -60f * Time.deltaTime);


            }


            if (TurnR == true || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Angle += 60f;

                TurnR = false;
                //�E
                //transform.Rotate(0f, 0f, 60f * Time.deltaTime);

            }
           
            this.transform.DORotate(new Vector3(-4.869f, 0f, Angle), 0.3f);
        }
    

    public void TurnLeft()
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

            TurnL = true;
        }
    }

    public void TurnRight()
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
            
            TurnR = true;
        }
    }

    public void GravityChange()
    {

        if(isGravity)
        {
            Gravity.SetActive(false);
            isGravity = false;
        }
        else
        {
            Gravity.SetActive(true);
            isGravity = true;
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
