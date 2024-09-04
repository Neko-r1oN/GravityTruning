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

    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SESlider;

    private float SEVolume;
    private float BGMVolume;



    [SerializeField] GameObject SettingWindow;

   

    void Start()
    {

        StartButton.SetActive(true);
        BuckButton.SetActive(false);
        StageSelector.SetActive(false);
        SettingWindow.SetActive(false);

        BGMManager.Instance.Play(
            audioPath: BGMPath.HOME, //再生したいオーディオのパス
            volumeRate: 100,                //音量の倍率
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
            SEManager.Instance.Play(SEPath.TAP);
        }

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
        BGMManager.Instance.ChangeBaseVolume(0.0f);
        BuckButton.SetActive(true);
        StageSelector.SetActive(true);

        StartButton.SetActive(false);

    }

    public void OnClickSetting()
    {
        SettingWindow.SetActive(true);

    }

    public void OnClickSelectReturn()
    {
       

        Invoke("CloseSelect", 0.5f);

        

    }

    public void CloseSetting()
    {
        SettingWindow.SetActive(false);
    }

    public void CloseSelect()
    {
        StageSelector.SetActive(false);
        BuckButton.SetActive(false);

        StartButton.SetActive(true);
    }
}
