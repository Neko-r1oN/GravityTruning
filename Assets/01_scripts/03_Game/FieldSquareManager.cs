using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;                   //DOTweenを使うときはこのusingを入れる
using System.Threading.Tasks;

public class FieldSquareManager : MonoBehaviour
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
    GameManager gameManager;

    private int TurnNum;
    private void Start()
    {
        TurnL = false;
        TurnR = false;
        TurnFlag = true;
        isGravity = true;
        Angle = 0f;
        TurnNum = GameManager.MasterTurnNum;
    }
    // Update is called once per frame
    void Update()
    {


        if (TurnL == true || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (TurnNum > 0 && TurnFlag == true)
            {
                CancelInvoke("ResetGrid");
                Angle -= 90f;

                
                TurnL = false;
                //左
                //transform.Rotate(0f, 0f, -60f * Time.deltaTime);

                Invoke("ResetGrid", 0.7f);

                TurnNum--;
            }
        }


        if (TurnR == true || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (TurnNum > 0 && TurnFlag == true)
            { 
                CancelInvoke("ResetGrid");
                
                Angle += 90f;

                TurnR = false;
                //右
                //transform.Rotate(0f, 0f, 60f * Time.deltaTime);
                Invoke("ResetGrid", 0.7f);
                TurnNum--;
            }

        }

        this.transform.DORotate(new Vector3(-4.869f, 0f, Angle), 0.3f);


    }


    public void TurnLeft()
    {

        // 制限中は動作させない
        if (buttonEnabled == false)
        {
            return;
        }
        // 制限されていない場合
        else
        {
            Debug.Log(TurnNum);
            TurnNum--;
            // ボタンを制限する
            buttonEnabled = false;

            // 一定時間経過後に解除
            StartCoroutine(EnableButton());
            Grid.SetActive(false);
            TurnL = true;
        }
    }

    public void TurnRight()
    {
        // 制限中は動作させない
        if (buttonEnabled == false)
        {
            return;
        }
        // 制限されていない場合
        else
        {
            Debug.Log(TurnNum);
            TurnNum--;

            // ボタンを制限する
            buttonEnabled = false;

            // 一定時間経過後に解除
            StartCoroutine(EnableButton());
            Grid.SetActive(false);
            TurnR = true;
        }
    }

    public void GravityChange()
    {

        if (isGravity)
        {
            Gravity.SetActive(false);
            isGravity = false;

            Invoke("ResetGravity", 1.0f);
        }
        else
        {

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
        if (TurnNum <= 0)
        {
            TurnFlag = false;
        }
    }

    // ボタンの制限を解除するコルーチン
    private IEnumerator EnableButton()
    {
        // 1秒後に解除         
        yield return waitOneSecond;
        buttonEnabled = true;
    }

}
