using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;       //AudioManagerを使うときはこのusingを入れる
using DG.Tweening;                   //DOTweenを使うときはこのusingを入れる
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    [SerializeField] GameObject StartButton;
    [SerializeField] GameObject BuckButton;
    [SerializeField] GameObject StageSelector;

    //[SerializeField] Text userName;

    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SESlider;

    private float SEVolume;
    private float BGMVolume;



    [SerializeField] GameObject SettingWindow;

   

    void Start()
    {

        //userName.text = NetworkManager.pub_UserName;

        StartButton.SetActive(true);
        BuckButton.SetActive(false);
        StageSelector.SetActive(false);
        SettingWindow.SetActive(false);

        BGMManager.Instance.Play(
            audioPath: BGMPath.HOME, //再生したいオーディオのパス
            volumeRate: 0.2f,                //音量の倍率
            delay: 0,                //再生されるまでの遅延時間
            pitch: 1,                //ピッチ
            isLoop: true,             //ループ再生するか
            allowsDuplicate: false             //他のBGMと重複して再生させるか
            );

    }

    // Update is called once per frame
    void Update()
    {
       
        //スライダーの値を音量に反映
        BGMVolume = BGMSlider.value * 0.01f;
        SEVolume = SESlider.value * 0.01f;

        //BGM全体のボリュームを変更
        BGMManager.Instance.ChangeBaseVolume(BGMVolume);

        //SE全体のボリュームを変更
        SEManager.Instance.ChangeBaseVolume(SEVolume);

    }

    public void OnClickSelectStage()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //再生したいオーディオのパス
           volumeRate: 0.2f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
           );

        BGMManager.Instance.ChangeBaseVolume(0.0f);
        BuckButton.SetActive(true);
        StageSelector.SetActive(true);

        StartButton.SetActive(false);

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
        SettingWindow.SetActive(true);

    }

    public void OnClickSelectReturn()
    {

        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //再生したいオーディオのパス
           volumeRate: 0.2f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
           );
       CloseSelect();

        

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
        SettingWindow.SetActive(false);
    }

    public void CloseSelect()
    {
        
        StageSelector.SetActive(false);
        BuckButton.SetActive(false);

        StartButton.SetActive(true);
    }
}
