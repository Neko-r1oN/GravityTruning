using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //アイテムのプレハブ
    [SerializeField] List<BubbleController> prefabBubbles;
    //UI
    [SerializeField] Text textScore;
    [SerializeField] GameObject panelResult;
    [SerializeField] GameObject panelStop;

    [SerializeField] int CrearMergeNum;    //クリアに必要な合成回数
    //サウンド
    [SerializeField] AudioClip seDrop;     //落下効果音
    [SerializeField] AudioClip seMerge;    //合体効果音

    //スコア
    int Score;
    //
    int MergeNum;
    //現在のアイテム
    BubbleController currentBubble;
    //生成位置
    const float SpawnItemY = 3.5f;
    //Audio再生装置
    AudioSource audioSource;

    private void Start()
    {
        //サウンド再生用
        audioSource = GetComponent<AudioSource>();
        //リザルト画面を非表示
        panelResult.SetActive(false);
        //中断画面を非表示
        panelStop.SetActive(false);
        //合計Merge数初期化
        MergeNum = 0;

        //最初のアイテムを生成
        //StartCoroutine(SpawnCurrentItem());
    }

    private void Update()
    {
        //アイテムがなければ実行しない
        if (!currentBubble) return;

        //マウスポジション(スクリーン座標)からワールド座標に変換
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //X座標をマウスに合わせる
        Vector2 bubblePosition = new Vector2(worldPoint.x, SpawnItemY);
        currentBubble.transform.position = bubblePosition;

        //タッチ処理
        if(Input.GetMouseButtonUp(0))
        {
            //重力をセットしてドロップ
            currentBubble.GetComponent<Rigidbody2D>().gravityScale = 1;
            //所持アイテムリセット
            currentBubble = null;
            //次のアイテム
            StartCoroutine(SpawnCurrentItem());
            //SE再生
            //audioSource.PlayOneShot(seDrop);

        }
    }

    //アイテム生成
    //BubbleController SpawnItem(Vector2 position, int colorType = -1)
    //{
        //色ランダム
        //int index = Random.Range(0, prefabBubbles.Count / 2);

        //色の指定があれば上書き
        //if(0 < colorType)
        //{
            //index = colorType;
        //}
        //生成
        //BubbleController bubble = Instantiate(prefabBubbles[index], position, Quaternion.identity);

        //必要データセット
        //bubble.SceneDirector = this;
        //bubble.ColorType = index;

        //return bubble;
    //}

    //所持アイテム生成
    IEnumerator SpawnCurrentItem()
    {
        //指定された秒数待つ
        yield return new WaitForSeconds(1.0f);
        //生成されたアイテムを保持する
        //currentBubble = SpawnItem(new Vector2(0, SpawnItemY));
        //落ちないように重力を0にする
        currentBubble.GetComponent<Rigidbody2D>().gravityScale = 0;
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
        textScore.text = "" + Score;

        //Destroy(newBubble.gameObject);
        //SE再生
        audioSource.PlayOneShot(seMerge);

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
        //リザルト画面を非表示
        panelStop.SetActive(true);

    }
    public void OnClikStopBack()
    {
        //リザルト画面を非表示
        panelStop.SetActive(false);

    }
    public void OnClikRetry()
    {
        // シーン遷移
        Initiate.Fade("GameResetScene", new Color(0, 0, 0, 1.0f), 5.0f);
        
    }
    public void OnClikHome()
    {
        // シーン遷移
        Initiate.Fade("HomeScene", new Color(0, 0, 0, 1.0f), 2.0f);

    }
}
