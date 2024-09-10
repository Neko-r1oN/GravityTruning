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
    //ランキングアイテムプレハブ
    [SerializeField] private GameObject rankItemPrefub;

    //ランキングビュー(全国)
    [SerializeField] private GameObject scoreRankingView;
    //ランキングビュー(全国)生成位置取得用
    [SerializeField] private Transform scoreRankingPos;

    //ランキングビュー(個人)
    [SerializeField] private GameObject myScoreRankingView;
    //ランキングビュー(個人)生成位置取得用
    [SerializeField] private Transform myScoreRankingPos;


    //ランキングビュー(個人)
    [SerializeField] private GameObject allRankingObj;
    //ランキングビュー(個人)生成位置取得用
    [SerializeField] private GameObject myRankingObj;
    //ランキング順位(個人)
    [SerializeField] private Text myScoreRank;
    //ランキングハイスコア(個人)
    [SerializeField] private Text myScore;

    //ランキング順位用変数
    private int rankNum;

    // Start is called before the first frame update
    void Start()
    {

        allRankingObj.SetActive(true);
        myRankingObj.SetActive(false);

        //ランクリセット
        rankNum = 0;

        //全体のランキングを取得
        StartCoroutine(NetworkManager.Instance.GetScoreRanking(
            result =>
            {
                //ステージデータが存在した場合
                if (result != null)
                {

                    foreach (ScoreRankingResponse stageData in result)
                    {
                        rankNum++;

                        //ステージボタン生成
                        GameObject rankItem = Instantiate(rankItemPrefub, Vector3.zero, Quaternion.identity, scoreRankingPos);

                        //スコア順位反映
                        rankItem.transform.GetChild(1).gameObject.GetComponent<Text>().text = rankNum.ToString();
                        //該当ユーザー名反映
                        rankItem.transform.GetChild(2).gameObject.GetComponent<Text>().text = stageData.UserName.ToString();
                        //スコア反映
                        rankItem.transform.GetChild(3).gameObject.GetComponent<Text>().text = stageData.Score.ToString();
     
                    }
                }
                //ステージが存在しない・取得に失敗した場合
                else
                {
                    //エラーテキスト表示
                    Debug.Log("えらー");
                }
            }));


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

    public void OnClickMyScore()
    {
        allRankingObj.SetActive(false);
        myRankingObj.SetActive(true);

    }

    public void OnClickAllScore()
    {
        allRankingObj.SetActive(true);
        myRankingObj.SetActive(false);

    }



}
