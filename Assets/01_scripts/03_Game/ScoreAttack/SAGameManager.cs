using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SAGameManager : MonoBehaviour
{
    //アイテムのプレハブ
    [SerializeField] List<BubbleController> prefabBubbles;
    //UI

    [SerializeField] Text TimerText;
    [SerializeField] GameObject panelResult;
    [SerializeField] GameObject panelStop;

    //サウンド
    [SerializeField] AudioClip seDrop;     //落下効果音
    [SerializeField] AudioClip seMerge;    //合体効果音


    public int ScoreNum;

    public int TurnNum;

    //カウントダウン
    public float countdown = 40;
    BubbleController currentBubble;

    //Audio再生装置
    AudioSource audioSource;

    static public string GaneScene;


    private void Start()
    {
        int NextStage = StageSelect.stageID;
        //サウンド再生用
        audioSource = GetComponent<AudioSource>();

        //リザルト画面を非表示
        panelResult.SetActive(false);
        //中断画面を非表示
        panelStop.SetActive(false);

        ScoreNum = 0;


        GaneScene = "GameScene";

        //最初のアイテムを生成
        /*StartCoroutine(SpawnCurrentItem());*/
    }

    private void Update()
    {
       
       
        //時間をカウントダウンする
        countdown -= Time.deltaTime;
      
        //時間を表示する
        TimerText.text = countdown.ToString("f1") + "";

        //countdownが0以下になったとき
        if (countdown <= 0)
        {
             TimerText.text = "0";
        }
    }

    //所持アイテム生成
    IEnumerator SpawnCurrentItem()
    {
        //指定された秒数待つ
        yield return new WaitForSeconds(1.0f);
        //生成されたアイテムを保持する
        currentBubble = SpawnItem(new Vector2(0, 0));
        //落ちないように重力を0にする
        currentBubble.GetComponent<Rigidbody2D>().gravityScale = 0;
    }


    BubbleController SpawnItem(Vector2 position,int colorType = 1)
    {
        int index = 1;
        if(0<colorType)
        {
            index = colorType;
        }

        BubbleController bubble = Instantiate(prefabBubbles[index], position, Quaternion.identity);

        bubble.SceneDirectorSA = this;
        bubble.ColorType = index;

        return bubble;

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
