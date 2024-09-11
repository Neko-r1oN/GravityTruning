using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using KanKikuchi.AudioManager;       //AudioManagerを使うときはこのusingを入れる

public class SAGameManager : MonoBehaviour
{
    //アイテムのプレハブ
    [SerializeField] List<BubbleController> prefabBubbles;

    //タイトル表示カウントダウン
    float TitleCountDown = 3.0f;
    
    //開始カウントダウン
    public Text CountText;
    float CountDown = 4.0f;
    int Count;

    float SceneChangeCount = 2.0f;

    [SerializeField] GameObject BG;

    [SerializeField] Text TimerText;
    [SerializeField] GameObject panelResult;
    [SerializeField] GameObject panelStop;

    //サウンド
    [SerializeField] AudioClip seDrop;     //落下効果音
    [SerializeField] AudioClip seMerge;    //合体効果音


    //タイマー
    public float CountTimer = 40;

    //Audio再生装置
    AudioSource audioSource;

    private bool isStartBGM;
    private bool isStartCount1;
    private bool isStartCount2;
    private bool isStartCount3;
    private bool isStartCount4;

    private bool sendScore;
    static public string GaneScene;

    NetworkManager networkManager;

    

    private void Start()
    {
        BGMManager.Instance.Stop();
        

        int NextStage = StageSelect.stageID;
        //サウンド再生用
        audioSource = GetComponent<AudioSource>();

        //リザルト画面を非表示
        panelResult.SetActive(false);
        //中断画面を非表示
        panelStop.SetActive(false);

        BG.SetActive(true);

        isStartBGM = true;
        isStartCount1 = true;
        isStartCount2 = true;
        isStartCount3 = true;
        isStartCount4 = true;
        sendScore = true;
        //userID = NetworkManager.userID;
        //userName = NetworkManager.userName;
        
        GaneScene = "GameScene";

    }

    private void Update()
    {
        if (TitleCountDown >= 1)
        {
            TitleCountDown -= Time.deltaTime;
            CountText.text = "Score Attack";

        }

        if (TitleCountDown <= 1)
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

            if (CountDown >= 1)
            {
                CountDown -= Time.deltaTime;
                Count = (int)CountDown;
                CountText.text = Count.ToString();

            }

            if (CountDown <= 1)
            {
                StartBGM();
                BG.SetActive(false);

                CountText.text = "";

                //時間をカウントダウンする
                CountTimer -= Time.deltaTime;

                //時間を表示する
                TimerText.text = CountTimer.ToString("f1") + "";

                //countdownが0以下になったとき
                if (CountTimer <= 0)
                {
                    

                    //時間を表示する
                    TimerText.text = "0";
                    BG.SetActive(true);

                    CountText.text = "Finish";
                    SceneChangeCount -= Time.deltaTime;
            
                    if (sendScore)
                    {
                        Store();


                        sendScore = false;
                    }
                    if (SceneChangeCount <= 0)
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
                      volumeRate: 0.8f,                //音量の倍率
                      delay: 0,                //再生されるまでの遅延時間
                      pitch: 1,                //ピッチ
                      isLoop: true,             //ループ再生するか
                      allowsDuplicate: false             //他のBGMと重複して再生させるか
                      );
        isStartBGM = false;
    }
    private void StartCount()
    {
       SEManager.Instance.Play(SEPath.COUNT_DOWN);
      
    }

    private void Store()
    {
        BGMManager.Instance.Stop();
        SEManager.Instance.Play(SEPath.FINISH);
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
        SEManager.Instance.Play(SEPath.TAP);
        //リザルト画面を非表示
        panelStop.SetActive(true);
        Time.timeScale = 0f;

    }
    public void OnClikStopBack()
    {
        SEManager.Instance.Play(SEPath.TAP);
        Time.timeScale = 1f;
        //リザルト画面を非表示
        panelStop.SetActive(false);
    }
    public void OnClikRetry()
    {
        SEManager.Instance.Play(SEPath.TAP);
        Time.timeScale = 1f;
        // シーン遷移
        Addressables.LoadScene("ScoreAttackScene", LoadSceneMode.Single);

    }
    /**/
    public void OnClikHome()
    {
        SEManager.Instance.Play(SEPath.TAP);
        Time.timeScale = 1f;
        // シーン遷移
        Initiate.Fade("HomeScene", new Color(0, 0, 0, 1.0f), 2.0f);

    }

    public void OnClikNext()
    {
        SEManager.Instance.Play(SEPath.TAP);
        GaneScene = "GameScene";
        int NextStage = StageSelect.stageID + 1;

        GaneScene += NextStage;
        Debug.Log(GaneScene);
        StageSelect.stageID = NextStage;
        // シーン遷移
        Initiate.Fade(GaneScene, new Color(0, 0, 0, 1), 2.0f);

    }
}
