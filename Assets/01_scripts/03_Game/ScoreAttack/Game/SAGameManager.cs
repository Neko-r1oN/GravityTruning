using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private bool sendScore;
    static public string GaneScene;

    NetworkManager networkManager;

    

    private void Start()
    {
        int NextStage = StageSelect.stageID;
        //サウンド再生用
        audioSource = GetComponent<AudioSource>();

        //リザルト画面を非表示
        panelResult.SetActive(false);
        //中断画面を非表示
        panelStop.SetActive(false);

        BG.SetActive(true);

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
            if (CountDown >= 1)
            {
                CountDown -= Time.deltaTime;
                Count = (int)CountDown;
                CountText.text = Count.ToString();

            }

            if (CountDown <= 1)
            {
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
       
    private void Store()
    {
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
        //リザルト画面を非表示
        panelStop.SetActive(true);
        Time.timeScale = 0f;

    }
    public void OnClikStopBack()
    {
        Time.timeScale = 1f;
        //リザルト画面を非表示
        panelStop.SetActive(false);
    }
    public void OnClikRetry()
    {
        Time.timeScale = 1f;
        // シーン遷移
        Initiate.Fade("GameResetScene", new Color(0, 0, 0, 1.0f), 5.0f);

    }
    /**/
    public void OnClikHome()
    {
        Time.timeScale = 1f;
        // シーン遷移
        Initiate.Fade("HomeScene", new Color(0, 0, 0, 1.0f), 2.0f);

    }

    public void OnClikNext()
    {
        GaneScene = "GameScene";
        int NextStage = StageSelect.stageID + 1;

        GaneScene += NextStage;
        Debug.Log(GaneScene);
        StageSelect.stageID = NextStage;
        // シーン遷移
        Initiate.Fade(GaneScene, new Color(0, 0, 0, 1), 2.0f);

    }
}
