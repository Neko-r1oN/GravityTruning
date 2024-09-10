using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using KanKikuchi.AudioManager;       //AudioManager���g���Ƃ��͂���using������
using DG.Tweening;                   //DOTween���g���Ƃ��͂���using������
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System;
using UnityEngine.Networking;
using System.IO;

public class RankingManager : MonoBehaviour
{
    //�����L���O�A�C�e���v���n�u
    [SerializeField] private GameObject rankItemPrefub;

    //�����L���O�r���[(�S��)
    [SerializeField] private GameObject scoreRankingView;
    //�����L���O�r���[(�S��)�����ʒu�擾�p
    [SerializeField] private Transform scoreRankingPos;

    //�����L���O�r���[(�l)
    [SerializeField] private GameObject myScoreRankingView;
    //�����L���O�r���[(�l)�����ʒu�擾�p
    [SerializeField] private Transform myScoreRankingPos;


    //�����L���O�r���[(�l)
    [SerializeField] private GameObject allRankingObj;
    //�����L���O�r���[(�l)�����ʒu�擾�p
    [SerializeField] private GameObject myRankingObj;
    //�����L���O����(�l)
    [SerializeField] private Text myScoreRank;
    //�����L���O�n�C�X�R�A(�l)
    [SerializeField] private Text myScore;

    //�����L���O���ʗp�ϐ�
    private int rankNum;

    // Start is called before the first frame update
    void Start()
    {

        allRankingObj.SetActive(true);
        myRankingObj.SetActive(false);

        //�����N���Z�b�g
        rankNum = 0;

        //�S�̂̃����L���O���擾
        StartCoroutine(NetworkManager.Instance.GetScoreRanking(
            result =>
            {
                //�X�e�[�W�f�[�^�����݂����ꍇ
                if (result != null)
                {

                    foreach (ScoreRankingResponse stageData in result)
                    {
                        rankNum++;

                        //�X�e�[�W�{�^������
                        GameObject rankItem = Instantiate(rankItemPrefub, Vector3.zero, Quaternion.identity, scoreRankingPos);

                        //�X�R�A���ʔ��f
                        rankItem.transform.GetChild(1).gameObject.GetComponent<Text>().text = rankNum.ToString();
                        //�Y�����[�U�[�����f
                        rankItem.transform.GetChild(2).gameObject.GetComponent<Text>().text = stageData.UserName.ToString();
                        //�X�R�A���f
                        rankItem.transform.GetChild(3).gameObject.GetComponent<Text>().text = stageData.Score.ToString();
     
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

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClickStartGame()
    {
        // �V�[���J��
        Addressables.LoadScene("ScoreAttackScene", LoadSceneMode.Single);
    }

    public void OnClickScoreHome()
    {
        // �V�[���J��
        Initiate.Fade("ScoreHomeScene", new Color(0, 0, 0, 1.0f), 5.0f);
    }

    public void OnClickMyScore()
    {
        allRankingObj.SetActive(false);
        myRankingObj.SetActive(true);

    }

    public void OnClickAllScore()
    {
        allRankingObj.SetActive(true);
        myRankingObj.SetActive(false);

    }



}
