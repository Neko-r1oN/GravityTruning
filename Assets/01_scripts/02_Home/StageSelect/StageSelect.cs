using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using KanKikuchi.AudioManager;       //AudioManager���g���Ƃ��͂���using������

public class StageSelect : MonoBehaviour
{
    static public string GaneScene;
    static public int stageID;

    //�X�e�[�W�{�^���v���n�u
	[SerializeField] private GameObject stageItemPrefub;

    //�X�N���[���r���[
    [SerializeField] private GameObject scrollView;

    //�X�e�[�W�{�^�������ʒu�擾�p
    [SerializeField] private Transform scrollViewPos;

    private void Start()
    {
        stageID = 0;

        GaneScene = "GameScene";

        //�X�e�[�W�����擾
        StartCoroutine(NetworkManager.Instance.GetStage(
            result =>
            {
                //�X�e�[�W�f�[�^�����݂����ꍇ
                if (result != null)
                {     

                    foreach (StageResponse stageData in result)
                    {
                        //�X�e�[�W�{�^������
                        GameObject stageButton = Instantiate(stageItemPrefub, Vector3.zero, Quaternion.identity, scrollViewPos);

                        //�X�e�[�WNo���f
                        stageButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = stageData.StageID.ToString();

                        //�N���b�N������ǉ�
                        stageButton.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            
                            OnStageSelectButtonPressed(stageData.StageID);
                        });
                    }
                }
                //�X�e�[�W�����݂��Ȃ��E�擾�Ɏ��s�����ꍇ
                else
                {
                    //�G���[�e�L�X�g�\��
                    Debug.Log("����[");
                }
            }));


    }
    public void OnStageSelectButtonPressed(int bossID)
	{
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.2f,                //���ʂ̔{��
           delay: 0,                //�Đ������܂ł̒x������
           pitch: 1,                //�s�b�`
           isLoop: false             //���[�v�Đ����邩
           );
        // �V�[���؂�ւ�
        //SceneManager.LoadScene(bossID + 1);
        stageID = bossID;
        GaneScene += stageID;
        Debug.Log(GaneScene);
        // �V�[���J��
        Addressables.LoadScene(GaneScene, LoadSceneMode.Single);
	}
}