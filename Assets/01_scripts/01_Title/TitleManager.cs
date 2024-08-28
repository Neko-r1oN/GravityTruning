using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private bool isClick;
    // Start is called before the first frame update
    void Start()
    {
        isClick = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {
            if(isClick) return;
            
            isClick = true;

            bool isSuccess = NetworkManager.Instance.LoadUserData();

            //ユーザーデータが保存されていない場合
            if (!isSuccess)
            {
                StartCoroutine(NetworkManager.Instance.StoreUser(
                    Guid.NewGuid().ToString(),       //名前 
                    Guid.NewGuid().ToString(),       //パスワード
                    result => {                      //登録後の処理
                        // シーン遷移
                        Initiate.Fade("HomeScene", new Color(0, 0, 0, 1.0f), 2.0f);
                    }));
            }
            //ユーザーデータが保存されている場合
            else
            {
                // シーン遷移
                Initiate.Fade("HomeScene", new Color(0, 0, 0, 1.0f), 2.0f);
            }
        }
    }
}
