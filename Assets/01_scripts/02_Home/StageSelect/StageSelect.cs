using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using KanKikuchi.AudioManager;       //AudioManagerを使うときはこのusingを入れる

public class StageSelect : MonoBehaviour
{
    static public string GaneScene;
    static public int stageID;

    //ステージボタンプレハブ
	[SerializeField] private GameObject stageItemPrefub;

    //スクロールビュー
    [SerializeField] private GameObject scrollView;

    //ステージボタン生成位置取得用
    [SerializeField] private Transform scrollViewPos;

    private void Start()
    {
        stageID = 0;

        GaneScene = "GameScene";

        //ステージ情報を取得
        StartCoroutine(NetworkManager.Instance.GetStage(
            result =>
            {
                //ステージデータが存在した場合
                if (result != null)
                {     

                    foreach (StageResponse stageData in result)
                    {
                        //ステージボタン生成
                        GameObject stageButton = Instantiate(stageItemPrefub, Vector3.zero, Quaternion.identity, scrollViewPos);

                        //ステージNo反映
                        stageButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = stageData.StageID.ToString();

                        //クリック処理を追加
                        stageButton.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            
                            OnStageSelectButtonPressed(stageData.StageID);
                        });
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
    public void OnStageSelectButtonPressed(int bossID)
	{
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //再生したいオーディオのパス
           volumeRate: 0.2f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
           );
        // シーン切り替え
        //SceneManager.LoadScene(bossID + 1);
        stageID = bossID;
        GaneScene += stageID;
        Debug.Log(GaneScene);
        // シーン遷移
        Addressables.LoadScene(GaneScene, LoadSceneMode.Single);
	}
}