using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using KanKikuchi.AudioManager;       //AudioManagerを使うときはこのusingを入れる

public class GameManager : MonoBehaviour
{
    //アイテムのプレハブ
    [SerializeField] List<BubbleController> prefabBubbles;
    //UI

    [SerializeField] Text stageText;
    [SerializeField] Text goalText;
    [SerializeField] Text textScore;
    [SerializeField] Text TurnText;
    [SerializeField] GameObject panelResult;
    [SerializeField] GameObject panelStop;

    [SerializeField] int CrearMergeNum;         //クリアに必要な合成回数
    [SerializeField] static public int MasterTurnNum { get; set; }              //残り回転可能回数
    [SerializeField] string goalTextMessage;    //クリアに必要な合成回数
    

    
    public int TurnNum;
    int MergeNum;
    //現在のアイテム
    BubbleController currentBubble;
    //生成位置
    const float SpawnItemY = 3.5f;
    //Audio再生装置
    //AudioSource audioSource;

    static public string GaneScene;
    

    private void Start()
    {

        BGMManager.Instance.Stop();
        BGMManager.Instance.Play(
            audioPath: BGMPath.NORMAL_BGM, //再生したいオーディオのパス
            volumeRate: 0.4f,                //音量の倍率
            delay: 0,                //再生されるまでの遅延時間
            pitch: 1,                //ピッチ
            isLoop: true,             //ループ再生するか
            allowsDuplicate: false             //他のBGMと重複して再生させるか
            );

        int NextStage = StageSelect.stageID;
        //サウンド再生用
        //audioSource = GetComponent<AudioSource>();

        //リザルト画面を非表示
        panelResult.SetActive(false);
        //中断画面を非表示
        panelStop.SetActive(false);
        //合計Merge数初期化
        MasterTurnNum = TurnNum;

        //goalText = "ボール";

        stageText.text = "Stage:" + StageSelect.stageID;
        goalText.text = goalTextMessage;

        GaneScene = "GameScene";

        //最初のアイテムを生成
        //StartCoroutine(SpawnCurrentItem());
    }

    private void Update()
    {
        TurnText.text = ""+ MasterTurnNum;
    }

    //アイテムを合体させる
    public void Merge(BubbleController bubbleA,BubbleController bubbleB)
    {
        MergeNum++;
       
        //マージ済み
        if (bubbleA.IsMerged || bubbleB.IsMerged) return;

        //違う色
        if (bubbleA.ColorType != bubbleB.ColorType) return;

        //次に用意してあるリストの最大数を超える
        int nextColor = bubbleA.ColorType + 1;
        if (prefabBubbles.Count - 1 < nextColor) return;

        //2点間の中心
        Vector2 lerpPosition = Vector2.Lerp(bubbleA.transform.position, bubbleB.transform.position, 0.5f);

        //新しいアイテムを生成
        //BubbleController newBubble = SpawnItem(lerpPosition, nextColor);

        //マージ済みフラグON
        bubbleA.IsMerged = true;
        bubbleB.IsMerged = true;

        Destroy(bubbleA.gameObject);
        Destroy(bubbleB.gameObject);

        //点数計算
        //Score += newBubble.ColorType * 10;
        //textScore.text = "" + Score;

        //Destroy(newBubble.gameObject);
        //SE再生
        SEManager.Instance.Play(SEPath.HIT);
        SEManager.Instance.Play(SEPath.CLUTCH);

        //操作中のアイテムとぶつかったらゲームオーバー
        if (MergeNum >= CrearMergeNum)
        {
            //このUpdate内に入らないようにする
            enabled = false;
            //リザルトパネル表示
            panelResult.SetActive(true);

            return;
        }
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
        Initiate.Fade("GameResetScene", new Color(0, 0, 0, 1.0f), 5.0f);
        
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
        Addressables.LoadScene(GaneScene, LoadSceneMode.Single);
    }
}
