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
    //ランキング順位用変数
    private int myRankNum;

    // Start is called before the first frame update
    void Start()
    {

        myScoreRank.text = "----";
        myScore.text = "----";
        allRankingObj.SetActive(true);
        myRankingObj.SetActive(false);

        //ランクリセット
        rankNum = 0;
        myRankNum = 0;

        //自分のランキングを取得
        /*StartCoroutine(NetworkManager.Instance.GetMyScoreRanking(
            result =>
            {
                //ステージデータが存在した場合
                if (result != null)
                {

                    foreach (ScoreRankingResponse myScoreData in result)
                    {
                        myRankNum++;

                        //ステージボタン生成
                        GameObject rankItem = Instantiate(rankItemPrefub, Vector3.zero, Quaternion.identity, myScoreRankingPos);

                        //スコア順位反映
                        rankItem.transform.GetChild(1).gameObject.GetComponent<Text>().text = rankNum.ToString();
                        //該当ユーザー名反映
                        rankItem.transform.GetChild(2).gameObject.GetComponent<Text>().text = myScoreData.UserName.ToString();
                        //スコア反映
                        rankItem.transform.GetChild(3).gameObject.GetComponent<Text>().text = myScoreData.Score.ToString();
     
                    }
                }
                //ステージが存在しない・取得に失敗した場合
                else
                {
                    //エラーテキスト表示
                    Debug.Log("えらー");
                }
            }));*/

        //全体のランキングを取得
        StartCoroutine(NetworkManager.Instance.GetScoreRanking(
            result =>
            {
                //ステージデータが存在した場合
                if (result != null)
                {

                    foreach (ScoreRankingResponse scoreData in result)
                    {
                        rankNum++;

                        //ステージボタン生成
                        GameObject rankItem = Instantiate(rankItemPrefub, Vector3.zero, Quaternion.identity, scoreRankingPos);

                        //スコア順位反映
                        rankItem.transform.GetChild(1).gameObject.GetComponent<Text>().text = rankNum.ToString();
                        //該当ユーザー名反映
                        rankItem.transform.GetChild(2).gameObject.GetComponent<Text>().text = scoreData.UserName.ToString();
                        //スコア反映
                        rankItem.transform.GetChild(3).gameObject.GetComponent<Text>().text = scoreData.Score.ToString();

                        if(NetworkManager.pub_UserID == scoreData.ID)
                        {
                            myScoreRank.text = rankNum.ToString(); ;
                            myScore.text = scoreData.Score.ToString();
                        }

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
        SEManager.Instance.Play(SEPath.TAP);
        // シーン遷移
        Addressables.LoadScene("ScoreAttackScene", LoadSceneMode.Single);
    }

    public void OnClickScoreHome()
    {
        SEManager.Instance.Play(SEPath.TAP);
        // シーン遷移
        Initiate.Fade("ScoreHomeScene", new Color(0, 0, 0, 1.0f), 5.0f);
    }

    public void OnClickMyScore()
    {
        SEManager.Instance.Play(SEPath.TAP);
        allRankingObj.SetActive(false);
        myRankingObj.SetActive(true);

    }

    public void OnClickAllScore()
    {
        SEManager.Instance.Play(SEPath.TAP);
        allRankingObj.SetActive(true);
        myRankingObj.SetActive(false);

    }



}
