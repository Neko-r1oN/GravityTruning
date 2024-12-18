using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using KanKikuchi.AudioManager;       //AudioManagerを使うときはこのusingを入れる

public class SAGameManager : MonoBehaviour
{

    //タイトル表示カウントダウン
    float titleCountDown = 3.0f;
    
    //開始カウントダウン
    public Text countText;
    float countDown = 4.0f;
    int count;

    float sceneChangeCount = 2.0f;

    [SerializeField] GameObject BG;

    [SerializeField] Text timerText;

    [SerializeField] GameObject panelStop;

    //サウンド
    [SerializeField] AudioClip seDrop;     //落下効果音
    [SerializeField] AudioClip seMerge;    //合体効果音


    //タイマー
    public float countTimer = 40;

    //Audio再生装置
    AudioSource audioSource;

    //カウントダウン用
    private bool isStartBGM;
    private bool isStartCount1;
    private bool isStartCount2;
    private bool isStartCount3;
    private bool isStartCount4;

    //スコア送信用
    private bool sendScore;

    NetworkManager networkManager;

    
    private void Start()
    {
        BGMManager.Instance.Stop();
        
        int NextStage = StageSelect.stageID;
        //サウンド再生用
        audioSource = GetComponent<AudioSource>();

        //中断画面を非表示
        panelStop.SetActive(false);

        BG.SetActive(true);

        isStartBGM = true;
        isStartCount1 = true;
        isStartCount2 = true;
        isStartCount3 = true;
        isStartCount4 = true;
        sendScore = true;
    }

    private void Update()
    {
        if (titleCountDown >= 1)
        {
            titleCountDown -= Time.deltaTime;
            countText.text = "Score Attack";
        }

        if (titleCountDown <= 1)
        {
            if(isStartCount1)
            Invoke("StartCount", 0.0f);
            isStartCount1 = false;

            if (isStartCount2)
                Invoke("StartCount", 1.0f);
            isStartCount2 = false;

            if (isStartCount3)
                Invoke("StartCount", 2.0f);
            isStartCount3 = false;

            if (isStartCount4)
                Invoke("StartCount", 3.0f);
            isStartCount4 = false;

            if (countDown >= 1)
            {
                countDown -= Time.deltaTime;
                count = (int)countDown;
                countText.text = count.ToString();

            }

            if (countDown <= 1)
            {
                StartBGM();
                BG.SetActive(false);

                countText.text = "";

                //時間をカウントダウンする
                countTimer -= Time.deltaTime;

                //時間を表示する
                timerText.text = countTimer.ToString("f1") + "";

                //countdownが0以下になったとき
                if (countTimer <= 0)
                {
                    

                    //時間を表示する
                    timerText.text = "0";
                    BG.SetActive(true);

                    countText.text = "Finish";
                    sceneChangeCount -= Time.deltaTime;
            
                    if (sendScore)
                    {
                        Store();


                        sendScore = false;
                    }
                    if (sceneChangeCount <= 0)
                    {
                        // シーン遷移
                        Initiate.Fade("ScoreResultScene", new Color(0, 0, 0, 1.0f), 5.0f);
                    }

                }
            }
        }
        

    }
       
    private void StartBGM()
    {
        if(isStartBGM)
        BGMManager.Instance.Play(
                      audioPath: BGMPath.SCORE_ATTACK, //再生したいオーディオのパス
                      volumeRate: 0.2f,                //音量の倍率
                      delay: 0,                //再生されるまでの遅延時間
                      pitch: 1,                //ピッチ
                      isLoop: true,             //ループ再生するか
                      allowsDuplicate: false             //他のBGMと重複して再生させるか
                      );
        isStartBGM = false;
    }
    private void StartCount()
    {
        SEManager.Instance.Play(
            audioPath: SEPath.COUNT_DOWN, //再生したいオーディオのパス
            volumeRate: 0.2f,                //音量の倍率
            delay: 0,                //再生されるまでの遅延時間
            pitch: 1,                //ピッチ
            isLoop: false             //ループ再生するか
            );

    }

    //スコア送信
    private void Store()
    {
        BGMManager.Instance.Stop();
        SEManager.Instance.Play(
           audioPath: SEPath.FINISH, //再生したいオーディオのパス
           volumeRate: 0.3f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
           );

        StartCoroutine(NetworkManager.Instance.StoreScore(
                            ScoreManager.score,       //スコア
                            result =>
                            {                      //結果
                                Debug.Log("成功");
                                // シーン繊維
                                //Initiate.Fade("LoadScene", new Color(0, 0, 0, 1.0f), 2.0f);
                            }));
    }
    
    public void OnClikStop()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //再生したいオーディオのパス
           volumeRate: 0.2f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
           );
        //リザルト画面を非表示
        panelStop.SetActive(true);
        Time.timeScale = 0f;

    }
    public void OnClikStopBack()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //再生したいオーディオのパス
           volumeRate: 0.2f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
           );
        Time.timeScale = 1f;
        //リザルト画面を非表示
        panelStop.SetActive(false);
    }
    public void OnClikRetry()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //再生したいオーディオのパス
           volumeRate: 0.2f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
           );
        Time.timeScale = 1f;
        // シーン遷移
        Addressables.LoadScene("ScoreAttackScene", LoadSceneMode.Single);

    }
    /**/
    public void OnClikHome()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //再生したいオーディオのパス
           volumeRate: 0.2f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
           );
        Time.timeScale = 1f;
        // シーン遷移
        Initiate.Fade("HomeScene", new Color(0, 0, 0, 1.0f), 2.0f);

    }
}
