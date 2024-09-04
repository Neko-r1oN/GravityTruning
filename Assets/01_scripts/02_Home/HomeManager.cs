using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;       //AudioManager���g���Ƃ��͂���using������
using DG.Tweening;                   //DOTween���g���Ƃ��͂���using������
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
            audioPath: BGMPath.HOME, //�Đ��������I�[�f�B�I�̃p�X
            volumeRate: 100,                //���ʂ̔{��
            delay: 0,                //�Đ������܂ł̒x������
            pitch: 1,                //�s�b�`
            isLoop: true,             //���[�v�Đ����邩
            allowsDuplicate: false             //����BGM�Əd�����čĐ������邩
            );

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SEManager.Instance.Play(SEPath.TAP);
        }

        //�X���C�_�[�̒l�����ʂɔ��f
        BGMVolume = BGMSlider.value * 0.01f;
        SEVolume = SESlider.value * 0.01f;

        //BGM�S�̂̃{�����[����ύX
        BGMManager.Instance.ChangeBaseVolume(BGMVolume);

        //SE�S�̂̃{�����[����ύX
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
