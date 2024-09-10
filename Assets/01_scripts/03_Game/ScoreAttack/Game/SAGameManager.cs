using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SAGameManager : MonoBehaviour
{
    //�A�C�e���̃v���n�u
    [SerializeField] List<BubbleController> prefabBubbles;

    //�^�C�g���\���J�E���g�_�E��
    float TitleCountDown = 3.0f;
    
    //�J�n�J�E���g�_�E��
    public Text CountText;
    float CountDown = 4.0f;
    int Count;

    float SceneChangeCount = 2.0f;

    [SerializeField] GameObject BG;

    [SerializeField] Text TimerText;
    [SerializeField] GameObject panelResult;
    [SerializeField] GameObject panelStop;

    //�T�E���h
    [SerializeField] AudioClip seDrop;     //�������ʉ�
    [SerializeField] AudioClip seMerge;    //���̌��ʉ�


    //�^�C�}�[
    public float CountTimer = 40;

    //Audio�Đ����u
    AudioSource audioSource;

    private bool sendScore;
    static public string GaneScene;

    NetworkManager networkManager;

    

    private void Start()
    {
        int NextStage = StageSelect.stageID;
        //�T�E���h�Đ��p
        audioSource = GetComponent<AudioSource>();

        //���U���g��ʂ��\��
        panelResult.SetActive(false);
        //���f��ʂ��\��
        panelStop.SetActive(false);

        BG.SetActive(true);

        sendScore = true;
        //userID = NetworkManager.userID;
        //userName = NetworkManager.userName;
        
        GaneScene = "GameScene";

    }

    private void Update()
    {
        if (TitleCountDown >= 1)
        {
            TitleCountDown -= Time.deltaTime;
            CountText.text = "Score Attack";

        }

        if (TitleCountDown <= 1)
        {
            if (CountDown >= 1)
            {
                CountDown -= Time.deltaTime;
                Count = (int)CountDown;
                CountText.text = Count.ToString();

            }

            if (CountDown <= 1)
            {
                BG.SetActive(false);

                CountText.text = "";

                //���Ԃ��J�E���g�_�E������
                CountTimer -= Time.deltaTime;

                //���Ԃ�\������
                TimerText.text = CountTimer.ToString("f1") + "";

                //countdown��0�ȉ��ɂȂ����Ƃ�
                if (CountTimer <= 0)
                {
                    //���Ԃ�\������
                    TimerText.text = "0";
                    BG.SetActive(true);

                    CountText.text = "Finish";
                    SceneChangeCount -= Time.deltaTime;
            
                    if (sendScore)
                    {
                        Store();


                        sendScore = false;
                    }
                    if (SceneChangeCount <= 0)
                    {
                        // �V�[���J��
                        Initiate.Fade("ScoreResultScene", new Color(0, 0, 0, 1.0f), 5.0f);
                    }

                }
            }
        }
        

    }
       
    private void Store()
    {
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
        //���U���g��ʂ��\��
        panelStop.SetActive(true);
        Time.timeScale = 0f;

    }
    public void OnClikStopBack()
    {
        Time.timeScale = 1f;
        //���U���g��ʂ��\��
        panelStop.SetActive(false);
    }
    public void OnClikRetry()
    {
        Time.timeScale = 1f;
        // �V�[���J��
        Initiate.Fade("GameResetScene", new Color(0, 0, 0, 1.0f), 5.0f);

    }
    /**/
    public void OnClikHome()
    {
        Time.timeScale = 1f;
        // �V�[���J��
        Initiate.Fade("HomeScene", new Color(0, 0, 0, 1.0f), 2.0f);

    }

    public void OnClikNext()
    {
        GaneScene = "GameScene";
        int NextStage = StageSelect.stageID + 1;

        GaneScene += NextStage;
        Debug.Log(GaneScene);
        StageSelect.stageID = NextStage;
        // �V�[���J��
        Initiate.Fade(GaneScene, new Color(0, 0, 0, 1), 2.0f);

    }
}
