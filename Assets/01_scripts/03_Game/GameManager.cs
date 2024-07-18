using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //�A�C�e���̃v���n�u
    [SerializeField] List<BubbleController> prefabBubbles;
    //UI
    [SerializeField] Text textScore;
    [SerializeField] GameObject panelResult;
    [SerializeField] GameObject panelStop;

    [SerializeField] int CrearMergeNum;    //�N���A�ɕK�v�ȍ�����
    //�T�E���h
    [SerializeField] AudioClip seDrop;     //�������ʉ�
    [SerializeField] AudioClip seMerge;    //���̌��ʉ�

    //�X�R�A
    int Score;
    //
    int MergeNum;
    //���݂̃A�C�e��
    BubbleController currentBubble;
    //�����ʒu
    const float SpawnItemY = 3.5f;
    //Audio�Đ����u
    AudioSource audioSource;

    private void Start()
    {
        //�T�E���h�Đ��p
        audioSource = GetComponent<AudioSource>();
        //���U���g��ʂ��\��
        panelResult.SetActive(false);
        //���f��ʂ��\��
        panelStop.SetActive(false);
        //���vMerge��������
        MergeNum = 0;

        //�ŏ��̃A�C�e���𐶐�
        //StartCoroutine(SpawnCurrentItem());
    }

    private void Update()
    {
        //�A�C�e�����Ȃ���Ύ��s���Ȃ�
        if (!currentBubble) return;

        //�}�E�X�|�W�V����(�X�N���[�����W)���烏�[���h���W�ɕϊ�
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //X���W���}�E�X�ɍ��킹��
        Vector2 bubblePosition = new Vector2(worldPoint.x, SpawnItemY);
        currentBubble.transform.position = bubblePosition;

        //�^�b�`����
        if(Input.GetMouseButtonUp(0))
        {
            //�d�͂��Z�b�g���ăh���b�v
            currentBubble.GetComponent<Rigidbody2D>().gravityScale = 1;
            //�����A�C�e�����Z�b�g
            currentBubble = null;
            //���̃A�C�e��
            StartCoroutine(SpawnCurrentItem());
            //SE�Đ�
            //audioSource.PlayOneShot(seDrop);

        }
    }

    //�A�C�e������
    //BubbleController SpawnItem(Vector2 position, int colorType = -1)
    //{
        //�F�����_��
        //int index = Random.Range(0, prefabBubbles.Count / 2);

        //�F�̎w�肪����Ώ㏑��
        //if(0 < colorType)
        //{
            //index = colorType;
        //}
        //����
        //BubbleController bubble = Instantiate(prefabBubbles[index], position, Quaternion.identity);

        //�K�v�f�[�^�Z�b�g
        //bubble.SceneDirector = this;
        //bubble.ColorType = index;

        //return bubble;
    //}

    //�����A�C�e������
    IEnumerator SpawnCurrentItem()
    {
        //�w�肳�ꂽ�b���҂�
        yield return new WaitForSeconds(1.0f);
        //�������ꂽ�A�C�e����ێ�����
        //currentBubble = SpawnItem(new Vector2(0, SpawnItemY));
        //�����Ȃ��悤�ɏd�͂�0�ɂ���
        currentBubble.GetComponent<Rigidbody2D>().gravityScale = 0;
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
        textScore.text = "" + Score;

        //Destroy(newBubble.gameObject);
        //SE�Đ�
        audioSource.PlayOneShot(seMerge);

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
        //���U���g��ʂ��\��
        panelStop.SetActive(true);

    }
    public void OnClikStopBack()
    {
        //���U���g��ʂ��\��
        panelStop.SetActive(false);

    }
    public void OnClikRetry()
    {
        // �V�[���J��
        Initiate.Fade("GameResetScene", new Color(0, 0, 0, 1.0f), 5.0f);
        
    }
    public void OnClikHome()
    {
        // �V�[���J��
        Initiate.Fade("HomeScene", new Color(0, 0, 0, 1.0f), 2.0f);

    }
}
