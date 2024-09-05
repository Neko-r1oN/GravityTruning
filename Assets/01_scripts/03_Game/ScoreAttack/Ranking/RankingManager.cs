using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using KanKikuchi.AudioManager;       //AudioManagerを使うときはこのusingを入れる
using DG.Tweening;                   //DOTweenを使うときはこのusingを入れる
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System;
using UnityEngine.Networking;
using System.IO;

public class RankingManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        //StartCoroutine("GetRanking");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClickStartGame()
    {
        // シーン遷移
        Addressables.LoadScene("ScoreAttackScene", LoadSceneMode.Single);
    }

    public void OnClickScoreHome()
    {
        // シーン遷移
        Initiate.Fade("ScoreHomeScene", new Color(0, 0, 0, 1.0f), 5.0f);
    }

    



    public class UserData
    {
        public int Rank { get; set; }
        public string UserName { get; set; }
        public int Score { get; set; }
    }
}
