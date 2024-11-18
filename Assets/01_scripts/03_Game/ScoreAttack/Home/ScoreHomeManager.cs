using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using KanKikuchi.AudioManager;       //AudioManagerを使うときはこのusingを入れる
using DG.Tweening;                   //DOTweenを使うときはこのusingを入れる
using UnityEngine.SceneManagement;


public class ScoreHomeManager : MonoBehaviour
{
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SESlider;

    private float SEVolume;
    private float BGMVolume;


    //[SerializeField] Text playerName;
    [SerializeField] GameObject settingWindow;
    [SerializeField] GameObject tutorialWindow;



    void Start()
    {
        settingWindow.SetActive(false);
        tutorialWindow.SetActive(false);

        BGMManager.Instance.Stop();

        BGMManager.Instance.Play(
           audioPath: BGMPath.SCORE_HOME, //再生したいオーディオのパス
           volumeRate: 0.1f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: true,             //ループ再生するか
           allowsDuplicate: false             //他のBGMと重複して再生させるか
           );

        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //SEManager.Instance.Play(SEPath.TAP);
        }

        BGMVolume = BGMSlider.value * 0.01f;
        SEVolume = SESlider.value * 0.01f;

        //BGM全体のボリュームを変更
        BGMManager.Instance.ChangeBaseVolume(BGMVolume);

        //SE全体のボリュームを変更
        SEManager.Instance.ChangeBaseVolume(SEVolume);

    }

    public void OnClickPlayGame()
    {
        //スコアアタックシーンに遷移
        // シーン遷移
        Addressables.LoadScene("ScoreAttackScene", LoadSceneMode.Single);

    }

    public void OnClickTutorial()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //再生したいオーディオのパス
           volumeRate: 0.2f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
           );
        tutorialWindow.SetActive(true);

    }
    public void CloseTutorial()
    {
        SEManager.Instance.Play(
            audioPath: SEPath.TAP, //再生したいオーディオのパス
            volumeRate: 0.2f,                //音量の倍率
            delay: 0,                //再生されるまでの遅延時間
            pitch: 1,                //ピッチ
            isLoop: false             //ループ再生するか
            );
        tutorialWindow.SetActive(false);
    }

    public void OnClickSetting()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //再生したいオーディオのパス
           volumeRate: 0.2f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
           );
        settingWindow.SetActive(true);

    }
    public void CloseSetting()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //再生したいオーディオのパス
           volumeRate: 0.2f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
           );
        settingWindow.SetActive(false);
    }

    public void OnClickRanking()
    {
        SEManager.Instance.Play(
            audioPath: SEPath.TAP, //再生したいオーディオのパス
            volumeRate: 0.2f,                //音量の倍率
            delay: 0,                //再生されるまでの遅延時間
            pitch: 1,                //ピッチ
            isLoop: false             //ループ再生するか
            );
        //ランキングシーンに遷移
        // シーン遷移
        Initiate.Fade("ScoreRankingScene", new Color(0, 0, 0, 1.0f), 5.0f);
    }

    public void OnClickBackHome()
    {
        SEManager.Instance.Play(
            audioPath: SEPath.TAP, //再生したいオーディオのパス
            volumeRate: 0.2f,                //音量の倍率
            delay: 0,                //再生されるまでの遅延時間
            pitch: 1,                //ピッチ
            isLoop: false             //ループ再生するか
            );
        //ホームシーンに遷移
        // シーン遷移
        Initiate.Fade("HomeScene", new Color(0, 0, 0, 1.0f), 5.0f);
    }

    
}
