using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SAGameManager : MonoBehaviour
{
    //�A�C�e���̃v���n�u
    [SerializeField] List<BubbleController> prefabBubbles;
    //UI

    [SerializeField] Text TimerText;
    [SerializeField] GameObject panelResult;
    [SerializeField] GameObject panelStop;

    //�T�E���h
    [SerializeField] AudioClip seDrop;     //�������ʉ�
    [SerializeField] AudioClip seMerge;    //���̌��ʉ�


    public int ScoreNum;

    public int TurnNum;

    //�J�E���g�_�E��
    public float countdown = 40;
    BubbleController currentBubble;

    //Audio�Đ����u
    AudioSource audioSource;

    static public string GaneScene;


    private void Start()
    {
        int NextStage = StageSelect.stageID;
        //�T�E���h�Đ��p
        audioSource = GetComponent<AudioSource>();

        //���U���g��ʂ��\��
        panelResult.SetActive(false);
        //���f��ʂ��\��
        panelStop.SetActive(false);

        ScoreNum = 0;


        GaneScene = "GameScene";

        //�ŏ��̃A�C�e���𐶐�
        /*StartCoroutine(SpawnCurrentItem());*/
    }

    private void Update()
    {
       
       
        //���Ԃ��J�E���g�_�E������
        countdown -= Time.deltaTime;
      
        //���Ԃ�\������
        TimerText.text = countdown.ToString("f1") + "";

        //countdown��0�ȉ��ɂȂ����Ƃ�
        if (countdown <= 0)
        {
             TimerText.text = "0";
        }
    }

    //�����A�C�e������
    IEnumerator SpawnCurrentItem()
    {
        //�w�肳�ꂽ�b���҂�
        yield return new WaitForSeconds(1.0f);
        //�������ꂽ�A�C�e����ێ�����
        currentBubble = SpawnItem(new Vector2(0, 0));
        //�����Ȃ��悤�ɏd�͂�0�ɂ���
        currentBubble.GetComponent<Rigidbody2D>().gravityScale = 0;
    }


    BubbleController SpawnItem(Vector2 position,int colorType = 1)
    {
        int index = 1;
        if(0<colorType)
        {
            index = colorType;
        }

        BubbleController bubble = Instantiate(prefabBubbles[index], position, Quaternion.identity);

        bubble.SceneDirectorSA = this;
        bubble.ColorType = index;

        return bubble;

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
