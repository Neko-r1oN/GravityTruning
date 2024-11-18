using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;                   //DOTweenを使うときはこのusingを入れる
using System.Threading.Tasks;
using KanKikuchi.AudioManager;       //AudioManagerを使うときはこのusingを入れる

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

    //開始カウントダウン
    float countDown = 5.0f;
    //開始カウントダウン
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
            //countdownが0以下になったとき
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
                    //左
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
                    //右
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
           audioPath: SEPath.TAP, //再生したいオーディオのパス
           volumeRate: 0.2f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
           );
        if (countDown <= 0)
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
                    grid.SetActive(false);
                    turnL = true;
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
        if (countDown <= 0)
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
                    grid.SetActive(false);
                    turnR = true;
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

    // ボタンの制限を解除するコルーチン
    private IEnumerator EnableButton()
    {
        // 1秒後に解除         
        yield return waitOneSecond;
        buttonEnabled = true;
    }

}
