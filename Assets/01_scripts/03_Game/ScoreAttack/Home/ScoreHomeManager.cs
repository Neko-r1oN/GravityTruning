using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using KanKikuchi.AudioManager;       //AudioManager���g���Ƃ��͂���using������
using DG.Tweening;                   //DOTween���g���Ƃ��͂���using������
using UnityEngine.SceneManagement;


public class ScoreHomeManager : MonoBehaviour
{
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SESlider;

    private float SEVolume;
    private float BGMVolume;


    //[SerializeField] Text playerName;
    [SerializeField] GameObject SettingWindow;
    [SerializeField] GameObject TutorialWindow;



    void Start()
    {
        SettingWindow.SetActive(false);
        TutorialWindow.SetActive(false);

        BGMManager.Instance.Stop();

        BGMManager.Instance.Play(
           audioPath: BGMPath.SCORE_HOME, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.3f,                //���ʂ̔{��
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
            //SEManager.Instance.Play(SEPath.TAP);
        }

        BGMVolume = BGMSlider.value * 0.01f;
        SEVolume = SESlider.value * 0.01f;

        //BGM�S�̂̃{�����[����ύX
        BGMManager.Instance.ChangeBaseVolume(BGMVolume);

        //SE�S�̂̃{�����[����ύX
        SEManager.Instance.ChangeBaseVolume(SEVolume);

    }

    public void OnClickPlayGame()
    {
        //�X�R�A�A�^�b�N�V�[���ɑJ��
        // �V�[���J��
        Addressables.LoadScene("ScoreAttackScene", LoadSceneMode.Single);

    }

    public void OnClickTutorial()
    {
        SEManager.Instance.Play(SEPath.TAP);
        TutorialWindow.SetActive(true);

    }
    public void CloseTutorial()
    {
        SEManager.Instance.Play(SEPath.TAP);
        TutorialWindow.SetActive(false);
    }

    public void OnClickSetting()
    {
        SEManager.Instance.Play(SEPath.TAP);
        SettingWindow.SetActive(true);

    }
    public void CloseSetting()
    {
        SEManager.Instance.Play(SEPath.TAP);
        SettingWindow.SetActive(false);
    }

    public void OnClickRanking()
    {
        SEManager.Instance.Play(SEPath.TAP);
        //�����L���O�V�[���ɑJ��
        // �V�[���J��
        Initiate.Fade("ScoreRankingScene", new Color(0, 0, 0, 1.0f), 5.0f);
    }

    public void OnClickBackHome()
    {
        SEManager.Instance.Play(SEPath.TAP);
        //�z�[���V�[���ɑJ��
        // �V�[���J��
        Initiate.Fade("HomeScene", new Color(0, 0, 0, 1.0f), 5.0f);
    }

    public void OnClickNameChange()
    {

    }
}
