using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using KanKikuchi.AudioManager;       //AudioManager���g���Ƃ��͂���using������

public class GameManager : MonoBehaviour
{
    //�A�C�e���̃v���n�u
    [SerializeField] List<BubbleController> prefabBubbles;
    //UI

    [SerializeField] Text stageText;
    [SerializeField] Text goalText;
    [SerializeField] Text textScore;
    [SerializeField] Text TurnText;
    [SerializeField] GameObject panelResult;
    [SerializeField] GameObject panelStop;

    [SerializeField] int CrearMergeNum;         //�N���A�ɕK�v�ȍ�����
    [SerializeField] static public int MasterTurnNum { get; set; }              //�c���]�\��
    [SerializeField] string goalTextMessage;    //�N���A�ɕK�v�ȍ�����
    

    
    public int TurnNum;
    int MergeNum;
    //���݂̃A�C�e��
    BubbleController currentBubble;
    //�����ʒu
    const float SpawnItemY = 3.5f;
    //Audio�Đ����u
    //AudioSource audioSource;

    static public string GaneScene;
    

    private void Start()
    {

        BGMManager.Instance.Stop();
        BGMManager.Instance.Play(
            audioPath: BGMPath.NORMAL_BGM, //�Đ��������I�[�f�B�I�̃p�X
            volumeRate: 0.4f,                //���ʂ̔{��
            delay: 0,                //�Đ������܂ł̒x������
            pitch: 1,                //�s�b�`
            isLoop: true,             //���[�v�Đ����邩
            allowsDuplicate: false             //����BGM�Əd�����čĐ������邩
            );

        int NextStage = StageSelect.stageID;
        //�T�E���h�Đ��p
        //audioSource = GetComponent<AudioSource>();

        //���U���g��ʂ��\��
        panelResult.SetActive(false);
        //���f��ʂ��\��
        panelStop.SetActive(false);
        //���vMerge��������
        MasterTurnNum = TurnNum;

        //goalText = "�{�[��";

        stageText.text = "Stage:" + StageSelect.stageID;
        goalText.text = goalTextMessage;

        GaneScene = "GameScene";

        //�ŏ��̃A�C�e���𐶐�
        //StartCoroutine(SpawnCurrentItem());
    }

    private void Update()
    {
        TurnText.text = ""+ MasterTurnNum;
    }

    //�A�C�e�������̂�����
    public void Merge(BubbleController bubbleA,BubbleController bubbleB)
    {
        MergeNum++;
       
        //�}�[�W�ς�
        if (bubbleA.IsMerged || bubbleB.IsMerged) return;

        //�Ⴄ�F
        if (bubbleA.ColorType != bubbleB.ColorType) return;

        //���ɗp�ӂ��Ă��郊�X�g�̍ő吔�𒴂���
        int nextColor = bubbleA.ColorType + 1;
        if (prefabBubbles.Count - 1 < nextColor) return;

        //2�_�Ԃ̒��S
        Vector2 lerpPosition = Vector2.Lerp(bubbleA.transform.position, bubbleB.transform.position, 0.5f);

        //�V�����A�C�e���𐶐�
        //BubbleController newBubble = SpawnItem(lerpPosition, nextColor);

        //�}�[�W�ς݃t���OON
        bubbleA.IsMerged = true;
        bubbleB.IsMerged = true;

        Destroy(bubbleA.gameObject);
        Destroy(bubbleB.gameObject);

        //�_���v�Z
        //Score += newBubble.ColorType * 10;
        //textScore.text = "" + Score;

        //Destroy(newBubble.gameObject);
        //SE�Đ�
        SEManager.Instance.Play(SEPath.HIT);
        SEManager.Instance.Play(SEPath.CLUTCH);

        //���쒆�̃A�C�e���ƂԂ�������Q�[���I�[�o�[
        if (MergeNum >= CrearMergeNum)
        {
            //����Update���ɓ���Ȃ��悤�ɂ���
            enabled = false;
            //���U���g�p�l���\��
            panelResult.SetActive(true);

            return;
        }
    }
    public void OnClikStop()
    {
        SEManager.Instance.Play(SEPath.TAP);
        //���U���g��ʂ��\��
        panelStop.SetActive(true);
        Time.timeScale = 0f;

    }
    public void OnClikStopBack()
    {
        SEManager.Instance.Play(SEPath.TAP);
        Time.timeScale = 1f;
        //���U���g��ʂ��\��
        panelStop.SetActive(false);
    }
    public void OnClikRetry()
    {
        SEManager.Instance.Play(SEPath.TAP);
        Time.timeScale = 1f;
        // �V�[���J��
        Initiate.Fade("GameResetScene", new Color(0, 0, 0, 1.0f), 5.0f);
        
    }
    /**/
    public void OnClikHome()
    {
        SEManager.Instance.Play(SEPath.TAP);
        Time.timeScale = 1f;
        // �V�[���J��
        Initiate.Fade("HomeScene", new Color(0, 0, 0, 1.0f), 2.0f);

    }

    public void OnClikNext()
    {
        SEManager.Instance.Play(SEPath.TAP);
        GaneScene = "GameScene";
        int NextStage = StageSelect.stageID + 1;

        GaneScene += NextStage;
        Debug.Log(GaneScene);
        StageSelect.stageID = NextStage;
        // �V�[���J��
        Addressables.LoadScene(GaneScene, LoadSceneMode.Single);
    }
}
