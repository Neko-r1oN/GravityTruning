using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;                   //DOTweenを使うときはこのusingを入れる
using System.Threading.Tasks;
using KanKikuchi.AudioManager;       //AudioManagerを使うときはこのusingを入れる
public class FieldManager : MonoBehaviour
{
    [SerializeField] GameObject gravity;    //重力
    [SerializeField] GameObject grid;       //ボール落下ガイド

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
                //左
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
                //右
                //transform.Rotate(0f, 0f, 60f * Time.deltaTime);
                Invoke("ResetGrid", 0.7f);
            }

        }

        this.transform.DORotate(new Vector3(-4.869f, 0f, angle), 0.3f);

       
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
        // 制限中は動作させない
        if (buttonEnabled == false)
        {
            return;
        }
        // 制限されていない場合
        else
        {

            Debug.Log(turnNum);
            turnNum--;
            // ボタンを制限する
            buttonEnabled = false;

            // 一定時間経過後に解除
            StartCoroutine(EnableButton());
            grid.SetActive(false);
            turnL = true;
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
        // 制限中は動作させない
        if (buttonEnabled == false)
        {
            return;
        }
        // 制限されていない場合
        else
        {

            Debug.Log(turnNum);
            turnNum--;
            // ボタンを制限する
            buttonEnabled = false;

            // 一定時間経過後に解除
            StartCoroutine(EnableButton());
            grid.SetActive(false);
            turnR = true;
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

    // ボタンの制限を解除するコルーチン
    private IEnumerator EnableButton()
    {
        // 1秒後に解除         
        yield return waitOneSecond;
        buttonEnabled = true;
    }

}
