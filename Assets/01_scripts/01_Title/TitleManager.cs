using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class TitleManager : MonoBehaviour
{
    //名前フィールド
    [SerializeField] GameObject nameField;
    [SerializeField] Text nameText;

    //ボタン関係
    [SerializeField] GameObject dummyButton;
    [SerializeField] GameObject startButton;

    //判定用変数
    private bool isClick;
    bool isSuccess;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = "";
        isClick = true;
        isSuccess = NetworkManager.Instance.LoadUserData();
        nameField.SetActive(true);

        dummyButton.SetActive(true);
        startButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //端末にアカウント情報があった場合
        if (isSuccess)
        {//初回登録用のボタンを非表示

            dummyButton.SetActive(false);
            startButton.SetActive(true);
            nameField.SetActive(false);
        }
        //情報がなかった場合
        else
        {
            //入力欄に文字が入力されている場合
            if (nameText.text != "")
            {
                dummyButton.SetActive(false);
                startButton.SetActive(true);
            }
            //未記入の場合
            else
            {
                dummyButton.SetActive(true);
                startButton.SetActive(false);
            }
        }
        
        if (isClick) return;
            
        isClick = true;

             
    }

    public void UserCreate()
    {
        //ユーザーデータが保存されていない場合
        if (!isSuccess)
        {
            StartCoroutine(NetworkManager.Instance.StoreUser(
                nameText.text,       //名前
                Guid.NewGuid().ToString(),       //パスワード
                result =>
                {                      //結果
                    Debug.Log("登録完了");
                }));

        }
        //ユーザーデータが保存されている場合
        else
        {
            Debug.Log("登録済み");
        }
    }
}
