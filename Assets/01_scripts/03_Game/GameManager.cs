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
    [SerializeField] Text TurnText;
    [SerializeField] GameObject panelResult;
    [SerializeField] GameObject panelStop;

    [SerializeField] int crearMergeNum;         //�N���A�ɕK�v�ȍ�����
    [SerializeField] static public int masterTurnNum { get; set; }              //�c���]�\��
    [SerializeField] string goalTextMessage;    //�N���A�ɕK�v�ȍ�����
    

    
    public int turnNum;
    int mergeNum;
    //���݂̃A�C�e��
    BubbleController currentBubble;
    //�����ʒu
    const float spawnItemY = 3.5f;
    //Audio�Đ����u
    //AudioSource audioSource;

    static public string ganeScene;
    

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
        masterTurnNum = turnNum;

        //goalText = "�{�[��";

        stageText.text = "Stage:" + StageSelect.stageID;
        goalText.text = goalTextMessage;

        ganeScene = "GameScene";

        //�ŏ��̃A�C�e���𐶐�
        //StartCoroutine(SpawnCurrentItem());
    }

    private void Update()
    {
        TurnText.text = ""+ masterTurnNum;
    }

    //�A�C�e�������̂�����
    public void Merge(BubbleController bubbleA,BubbleController bubbleB)
    {
        mergeNum++;
       
        //�}�[�W�ς�
        if (bubbleA.isMerged || bubbleB.isMerged) return;

        //�Ⴄ�F
        if (bubbleA.colorType != bubbleB.colorType) return;

        //���ɗp�ӂ��Ă��郊�X�g�̍ő吔�𒴂���
        int nextColor = bubbleA.colorType + 1;
        if (prefabBubbles.Count - 1 < nextColor) return;

        //2�_�Ԃ̒��S
        Vector2 lerpPosition = Vector2.Lerp(bubbleA.transform.position, bubbleB.transform.position, 0.5f);

        //�V�����A�C�e���𐶐�
        //BubbleController newBubble = SpawnItem(lerpPosition, nextColor);

        //�}�[�W�ς݃t���OON
        bubbleA.isMerged = true;
        bubbleB.isMerged = true;

        Destroy(bubbleA.gameObject);
        Destroy(bubbleB.gameObject);

        //�_���v�Z
        //Score += newBubble.ColorType * 10;
        //textScore.text = "" + Score;

        //Destroy(newBubble.gameObject);
        //SE�Đ�
        SEManager.Instance.Play(
           audioPath: SEPath.HIT, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.2f,                //���ʂ̔{��
           delay: 0,                //�Đ������܂ł̒x������
           pitch: 1,                //�s�b�`
           isLoop: false             //���[�v�Đ����邩
           );
        SEManager.Instance.Play(
            audioPath: SEPath.CLUTCH, //�Đ��������I�[�f�B�I�̃p�X
            volumeRate: 0.3f,                //���ʂ̔{��
            delay: 0,                //�Đ������܂ł̒x������
            pitch: 1,                //�s�b�`
            isLoop: false             //���[�v�Đ����邩
            );

        //���쒆�̃A�C�e���ƂԂ�������Q�[���I�[�o�[
        if (mergeNum >= crearMergeNum)
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
        Initiate.Fade("GameResetScene", new Color(0, 0, 0, 1.0f), 5.0f);
        
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

    public void OnClikNext()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.2f,                //���ʂ̔{��
           delay: 0,                //�Đ������܂ł̒x������
           pitch: 1,                //�s�b�`
           isLoop: false             //���[�v�Đ����邩
           );
        ganeScene = "GameScene";
        int NextStage = StageSelect.stageID + 1;

        ganeScene += NextStage;
        Debug.Log(ganeScene);
        StageSelect.stageID = NextStage;
        // �V�[���J��
        Addressables.LoadScene(ganeScene, LoadSceneMode.Single);
    }
}
