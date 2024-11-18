using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using KanKikuchi.AudioManager;       //AudioManager���g���Ƃ��͂���using������

public class SAGameManager : MonoBehaviour
{

    //�^�C�g���\���J�E���g�_�E��
    float titleCountDown = 3.0f;
    
    //�J�n�J�E���g�_�E��
    public Text countText;
    float countDown = 4.0f;
    int count;

    float sceneChangeCount = 2.0f;

    [SerializeField] GameObject BG;

    [SerializeField] Text timerText;

    [SerializeField] GameObject panelStop;

    //�T�E���h
    [SerializeField] AudioClip seDrop;     //�������ʉ�
    [SerializeField] AudioClip seMerge;    //���̌��ʉ�


    //�^�C�}�[
    public float countTimer = 40;

    //Audio�Đ����u
    AudioSource audioSource;

    //�J�E���g�_�E���p
    private bool isStartBGM;
    private bool isStartCount1;
    private bool isStartCount2;
    private bool isStartCount3;
    private bool isStartCount4;

    //�X�R�A���M�p
    private bool sendScore;

    NetworkManager networkManager;

    
    private void Start()
    {
        BGMManager.Instance.Stop();
        
        int NextStage = StageSelect.stageID;
        //�T�E���h�Đ��p
        audioSource = GetComponent<AudioSource>();

        //���f��ʂ��\��
        panelStop.SetActive(false);

        BG.SetActive(true);

        isStartBGM = true;
        isStartCount1 = true;
        isStartCount2 = true;
        isStartCount3 = true;
        isStartCount4 = true;
        sendScore = true;
    }

    private void Update()
    {
        if (titleCountDown >= 1)
        {
            titleCountDown -= Time.deltaTime;
            countText.text = "Score Attack";
        }

        if (titleCountDown <= 1)
        {
            if(isStartCount1)
            Invoke("StartCount", 0.0f);
            isStartCount1 = false;

            if (isStartCount2)
                Invoke("StartCount", 1.0f);
            isStartCount2 = false;

            if (isStartCount3)
                Invoke("StartCount", 2.0f);
            isStartCount3 = false;

            if (isStartCount4)
                Invoke("StartCount", 3.0f);
            isStartCount4 = false;

            if (countDown >= 1)
            {
                countDown -= Time.deltaTime;
                count = (int)countDown;
                countText.text = count.ToString();

            }

            if (countDown <= 1)
            {
                StartBGM();
                BG.SetActive(false);

                countText.text = "";

                //���Ԃ��J�E���g�_�E������
                countTimer -= Time.deltaTime;

                //���Ԃ�\������
                timerText.text = countTimer.ToString("f1") + "";

                //countdown��0�ȉ��ɂȂ����Ƃ�
                if (countTimer <= 0)
                {
                    

                    //���Ԃ�\������
                    timerText.text = "0";
                    BG.SetActive(true);

                    countText.text = "Finish";
                    sceneChangeCount -= Time.deltaTime;
            
                    if (sendScore)
                    {
                        Store();


                        sendScore = false;
                    }
                    if (sceneChangeCount <= 0)
                    {
                        // �V�[���J��
                        Initiate.Fade("ScoreResultScene", new Color(0, 0, 0, 1.0f), 5.0f);
                    }

                }
            }
        }
        

    }
       
    private void StartBGM()
    {
        if(isStartBGM)
        BGMManager.Instance.Play(
                      audioPath: BGMPath.SCORE_ATTACK, //�Đ��������I�[�f�B�I�̃p�X
                      volumeRate: 0.2f,                //���ʂ̔{��
                      delay: 0,                //�Đ������܂ł̒x������
                      pitch: 1,                //�s�b�`
                      isLoop: true,             //���[�v�Đ����邩
                      allowsDuplicate: false             //����BGM�Əd�����čĐ������邩
                      );
        isStartBGM = false;
    }
    private void StartCount()
    {
        SEManager.Instance.Play(
            audioPath: SEPath.COUNT_DOWN, //�Đ��������I�[�f�B�I�̃p�X
            volumeRate: 0.2f,                //���ʂ̔{��
            delay: 0,                //�Đ������܂ł̒x������
            pitch: 1,                //�s�b�`
            isLoop: false             //���[�v�Đ����邩
            );

    }

    //�X�R�A���M
    private void Store()
    {
        BGMManager.Instance.Stop();
        SEManager.Instance.Play(
           audioPath: SEPath.FINISH, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.3f,                //���ʂ̔{��
           delay: 0,                //�Đ������܂ł̒x������
           pitch: 1,                //�s�b�`
           isLoop: false             //���[�v�Đ����邩
           );

        StartCoroutine(NetworkManager.Instance.StoreScore(
                            ScoreManager.score,       //�X�R�A
                            result =>
                            {                      //����
                                Debug.Log("����");
                                // �V�[���@��
                                //Initiate.Fade("LoadScene", new Color(0, 0, 0, 1.0f), 2.0f);
                            }));
    }
    
    public void OnClikStop()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.2f,                //���ʂ̔{��
           delay: 0,                //�Đ������܂ł̒x������
           pitch: 1,                //�s�b�`
           isLoop: false             //���[�v�Đ����邩
           );
        //���U���g��ʂ��\��
        panelStop.SetActive(true);
        Time.timeScale = 0f;

    }
    public void OnClikStopBack()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.2f,                //���ʂ̔{��
           delay: 0,                //�Đ������܂ł̒x������
           pitch: 1,                //�s�b�`
           isLoop: false             //���[�v�Đ����邩
           );
        Time.timeScale = 1f;
        //���U���g��ʂ��\��
        panelStop.SetActive(false);
    }
    public void OnClikRetry()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.2f,                //���ʂ̔{��
           delay: 0,                //�Đ������܂ł̒x������
           pitch: 1,                //�s�b�`
           isLoop: false             //���[�v�Đ����邩
           );
        Time.timeScale = 1f;
        // �V�[���J��
        Addressables.LoadScene("ScoreAttackScene", LoadSceneMode.Single);

    }
    /**/
    public void OnClikHome()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.2f,                //���ʂ̔{��
           delay: 0,                //�Đ������܂ł̒x������
           pitch: 1,                //�s�b�`
           isLoop: false             //���[�v�Đ����邩
           );
        Time.timeScale = 1f;
        // �V�[���J��
        Initiate.Fade("HomeScene", new Color(0, 0, 0, 1.0f), 2.0f);

    }
}
