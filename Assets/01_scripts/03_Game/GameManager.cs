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
    [SerializeField] Text TurnText;
    [SerializeField] GameObject panelResult;
    [SerializeField] GameObject panelStop;

    [SerializeField] int crearMergeNum;         //クリアに必要な合成回数
    [SerializeField] static public int masterTurnNum { get; set; }              //残り回転可能回数
    [SerializeField] string goalTextMessage;    //クリアに必要な合成回数
    

    
    public int turnNum;
    int mergeNum;
    //現在のアイテム
    BubbleController currentBubble;
    //生成位置
    const float spawnItemY = 3.5f;
    //Audio再生装置
    //AudioSource audioSource;

    static public string ganeScene;
    

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
        masterTurnNum = turnNum;

        //goalText = "ボール";

        stageText.text = "Stage:" + StageSelect.stageID;
        goalText.text = goalTextMessage;

        ganeScene = "GameScene";

        //最初のアイテムを生成
        //StartCoroutine(SpawnCurrentItem());
    }

    private void Update()
    {
        TurnText.text = ""+ masterTurnNum;
    }

    //アイテムを合体させる
    public void Merge(BubbleController bubbleA,BubbleController bubbleB)
    {
        mergeNum++;
       
        //マージ済み
        if (bubbleA.isMerged || bubbleB.isMerged) return;

        //違う色
        if (bubbleA.colorType != bubbleB.colorType) return;

        //次に用意してあるリストの最大数を超える
        int nextColor = bubbleA.colorType + 1;
        if (prefabBubbles.Count - 1 < nextColor) return;

        //2点間の中心
        Vector2 lerpPosition = Vector2.Lerp(bubbleA.transform.position, bubbleB.transform.position, 0.5f);

        //新しいアイテムを生成
        //BubbleController newBubble = SpawnItem(lerpPosition, nextColor);

        //マージ済みフラグON
        bubbleA.isMerged = true;
        bubbleB.isMerged = true;

        Destroy(bubbleA.gameObject);
        Destroy(bubbleB.gameObject);

        //点数計算
        //Score += newBubble.ColorType * 10;
        //textScore.text = "" + Score;

        //Destroy(newBubble.gameObject);
        //SE再生
        SEManager.Instance.Play(
           audioPath: SEPath.HIT, //再生したいオーディオのパス
           volumeRate: 0.2f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
           );
        SEManager.Instance.Play(
            audioPath: SEPath.CLUTCH, //再生したいオーディオのパス
            volumeRate: 0.3f,                //音量の倍率
            delay: 0,                //再生されるまでの遅延時間
            pitch: 1,                //ピッチ
            isLoop: false             //ループ再生するか
            );

        //操作中のアイテムとぶつかったらゲームオーバー
        if (mergeNum >= crearMergeNum)
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
        Initiate.Fade("GameResetScene", new Color(0, 0, 0, 1.0f), 5.0f);
        
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

    public void OnClikNext()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //再生したいオーディオのパス
           volumeRate: 0.2f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
           );
        ganeScene = "GameScene";
        int NextStage = StageSelect.stageID + 1;

        ganeScene += NextStage;
        Debug.Log(ganeScene);
        StageSelect.stageID = NextStage;
        // シーン遷移
        Addressables.LoadScene(ganeScene, LoadSceneMode.Single);
    }
}
