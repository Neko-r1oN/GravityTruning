using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;                   //DOTweenを使うときはこのusingを入れる
using System.Threading.Tasks;
using KanKikuchi.AudioManager;       //AudioManagerを使うときはこのusingを入れる

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

    //開始カウントダウン
    float CountDown = 5.0f;
    //開始カウントダウン
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
            //countdownが0以下になったとき
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
                    //左
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
                    //右
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
           audioPath: SEPath.TAP, //再生したいオーディオのパス
           volumeRate: 0.2f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
           );
        if (CountDown <= 0)
        {
            if (isGame)
            {
                // 制限中は動作させない
                if (buttonEnabled == false)
                {
                    return;
                }
                // 制限されていない場合
                else
                {
                    // ボタンを制限する
                    buttonEnabled = false;

                    // 一定時間経過後に解除
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
           audioPath: SEPath.TAP, //再生したいオーディオのパス
           volumeRate: 0.2f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
           );
        if (CountDown <= 0)
        {
            if (isGame)
            {
                // 制限中は動作させない
                if (buttonEnabled == false)
                {
                    return;
                }
                // 制限されていない場合
                else
                {
                    // ボタンを制限する
                    buttonEnabled = false;

                    // 一定時間経過後に解除
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
           audioPath: SEPath.TAP, //再生したいオーディオのパス
           volumeRate: 0.2f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
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

    // ボタンの制限を解除するコルーチン
    private IEnumerator EnableButton()
    {
        // 1秒後に解除         
        yield return waitOneSecond;
        buttonEnabled = true;
    }

}
