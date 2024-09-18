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

    //�����L���O�ǂݍ��݃e�L�X�g(�l)
    [SerializeField] private GameObject rankText;
    //�����L���O�r���[(�l)�����ʒu�擾�p
    [SerializeField] private GameObject myRankText;

    //�����L���O���ʗp�ϐ�
    private int rankNum;
    //�����L���O���ʗp�ϐ�
    private int myRankNum;

    // Start is called before the first frame update
    void Start()
    {

        myScoreRank.text = "----";
        myScore.text = "----";
        allRankingObj.SetActive(true);
        myRankingObj.SetActive(false);

        rankText.SetActive(true);
        myRankText.SetActive(true);

        //�����N���Z�b�g
        rankNum = 0;
        myRankNum = 0;

        //�����̃����L���O���擾
        //�S�̂̃����L���O���擾
        StartCoroutine(NetworkManager.Instance.GetMyScoreRanking(
            result =>
            {
                //�X�e�[�W�f�[�^�����݂����ꍇ
                if (result != null)
                {

                    foreach (ScoreRankingResponse scoreData in result)
                    {
                        myRankNum++;

                        //�����L���O�A�C�e������
                        GameObject rankItem = Instantiate(rankItemPrefub, Vector3.zero, Quaternion.identity, myScoreRankingPos);

                        //�X�R�A���ʔ��f
                        rankItem.transform.GetChild(1).gameObject.GetComponent<Text>().text = myRankNum.ToString();
                        //�Y�����[�U�[�����f
                        rankItem.transform.GetChild(2).gameObject.GetComponent<Text>().text = scoreData.UserName.ToString();
                        //�X�R�A���f
                        rankItem.transform.GetChild(3).gameObject.GetComponent<Text>().text = scoreData.Score.ToString();

                       
                    }
                    myRankText.SetActive(false);
                }
                //�X�e�[�W�����݂��Ȃ��E�擾�Ɏ��s�����ꍇ
                else
                {
                    //�G���[�e�L�X�g�\��
                    Debug.Log("����[");
                }
            }));

        //�S�̂̃����L���O���擾
        StartCoroutine(NetworkManager.Instance.GetScoreRanking(
            result =>
            {
                //�X�e�[�W�f�[�^�����݂����ꍇ
                if (result != null)
                {

                    foreach (ScoreRankingResponse scoreData in result)
                    {
                        rankNum++;

                        //�����L���O�A�C�e������
                        GameObject rankItem = Instantiate(rankItemPrefub, Vector3.zero, Quaternion.identity, scoreRankingPos);

                        //�X�R�A���ʔ��f
                        rankItem.transform.GetChild(1).gameObject.GetComponent<Text>().text = rankNum.ToString();
                        //�Y�����[�U�[�����f
                        rankItem.transform.GetChild(2).gameObject.GetComponent<Text>().text = scoreData.UserName.ToString();
                        //�X�R�A���f
                        rankItem.transform.GetChild(3).gameObject.GetComponent<Text>().text = scoreData.Score.ToString();

                        if(NetworkManager.pub_UserID == scoreData.ID)
                        {
                            myScoreRank.text = rankNum.ToString(); ;
                            myScore.text = scoreData.Score.ToString();
                        }

                    }
                    rankText.SetActive(false);
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
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.2f,                //���ʂ̔{��
           delay: 0,                //�Đ������܂ł̒x������
           pitch: 1,                //�s�b�`
           isLoop: false             //���[�v�Đ����邩
           );
        // �V�[���J��
        Addressables.LoadScene("ScoreAttackScene", LoadSceneMode.Single);
    }

    public void OnClickScoreHome()
    {
        SEManager.Instance.Play(
            audioPath: SEPath.TAP, //�Đ��������I�[�f�B�I�̃p�X
            volumeRate: 0.2f,                //���ʂ̔{��
            delay: 0,                //�Đ������܂ł̒x������
            pitch: 1,                //�s�b�`
            isLoop: false             //���[�v�Đ����邩
            );
        // �V�[���J��
        Initiate.Fade("ScoreHomeScene", new Color(0, 0, 0, 1.0f), 5.0f);
    }

    public void OnClickMyScore()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.2f,                //���ʂ̔{��
           delay: 0,                //�Đ������܂ł̒x������
           pitch: 1,                //�s�b�`
           isLoop: false             //���[�v�Đ����邩
           );
        allRankingObj.SetActive(false);
        myRankingObj.SetActive(true);

    }

    public void OnClickAllScore()
    {
        SEManager.Instance.Play(
           audioPath: SEPath.TAP, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.2f,                //���ʂ̔{��
           delay: 0,                //�Đ������܂ł̒x������
           pitch: 1,                //�s�b�`
           isLoop: false             //���[�v�Đ����邩
           );
        allRankingObj.SetActive(true);
        myRankingObj.SetActive(false);

    }



}
