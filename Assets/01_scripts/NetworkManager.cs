using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEngine.Networking;
using System.IO;

public class NetworkManager : MonoBehaviour
{
    //https�ʐMURL
    //https://api-gravityturning.japaneast.cloudapp.azure.com/


    //https�ʐM�p
    //https://api-gravityturning.japaneast.cloudapp.azure.com/api/

    //���[�J��(�ʐM�m�F�p)
    //http://localhost:8000/api/

    const string API_BASE_URL = "https://api-gravityturning.japaneast.cloudapp.azure.com/api/";

    public int userID = 0;

    static public string pub_UserName { get; private set; }
    static public int pub_UserID { get; private set; }
    public string userName = "";
    private string password = "";
    /*static public int userID { get; private set; }
    static public string userName { get; private set; }
    static public string passWord { get; private set; }*/

    //get�v���p�e�B���Ăяo�������񎞂� static �ŕێ�
    private static NetworkManager instance;


    public static NetworkManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObj = new GameObject("NetworkManager");
                //GameObject�����ANetworkManager�R���|�[�l���g��ǉ�
                instance = gameObj.AddComponent<NetworkManager>();
                //�V�[���ړ����ɍ폜���Ȃ��悤�ɂ���
                DontDestroyOnLoad(gameObj);

            }
            return instance;
        }
    }


    /*==============================*/
    //���[�U�[�o�^����
    /*==============================*/

    public IEnumerator StoreUser(string name, string password, Action<bool> result)
    {
        //�T�[�o�[�ɑ���I�u�W�F�N�g���쐬
        StoreUserRequest requestData = new StoreUserRequest();
        requestData.Name = name;
        requestData.Password = password;
        //�T�[�o�[�ɑ���I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(requestData);
        //���M
        UnityWebRequest request = UnityWebRequest.Post(
            API_BASE_URL + "users/store", json, "application/json");

        yield return request.SendWebRequest();
        bool isSuccess = false;

        //�ʐM�����������ꍇ
        if (request.result == UnityWebRequest.Result.Success && request.responseCode == 200)
        {
            //�A���Ă���JSON�t�@�C�����I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;
            StoreUserResponse response = JsonConvert.DeserializeObject<StoreUserResponse>(resultJson);

            //�t�@�C���Ƀ��[�U�[��ۑ�
            this.userName = name;
            this.password = password;
            this.userID = response.UserID;
            SaveUserData();
            isSuccess = true;
        }
        result?.Invoke(isSuccess);  //�����ŌĂяo������result�������Ăяo��
    }


    //�f�[�^�ۑ�����
    private void SaveUserData()
    {
        SaveData saveData = new SaveData();
        saveData.Name = this.userName;
        saveData.UserID = this.userID;
        string json = JsonConvert.SerializeObject(saveData);

        //�t�@�C����Json��ۑ�
        var writer = new StreamWriter(Application.persistentDataPath + "/saveData.json");   // Application.persistentDataPath�͕ۑ��t�@�C����u���ꏊ

        writer.Write(json);
        writer.Flush();
        writer.Close();

    }

    //���[�U�[���ǂݍ��ݏ���
    public bool LoadUserData()
    {
        if (!File.Exists(Application.persistentDataPath + "/saveData.json"))
        {
            return false;
        }
        var reader = new StreamReader(Application.persistentDataPath + "/saveData.json");
        string json = reader.ReadToEnd();
        reader.Close();
        SaveData saveData = JsonConvert.DeserializeObject<SaveData>(json);

        //���[�J���t�@�C������e��l���擾
        this.userID = saveData.UserID;
        this.userName = saveData.Name;
        //�ǂݍ��ݔ���
        return true;

    }

    //�X�e�[�W���擾����
    public IEnumerator GetStage(Action<StageResponse[]> result)
    {

        UnityWebRequest request = UnityWebRequest.Get(
            API_BASE_URL + "stageList");

        yield return request.SendWebRequest();

        StageResponse[] response = null;
        //Rootobject response = new Rootobject();

        //�ʐM�����������ꍇ
        if (request.result == UnityWebRequest.Result.Success && request.responseCode == 200)
        {
            //�A���Ă���JSON�t�@�C�����I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;
            Debug.Log(resultJson);
            response = JsonConvert.DeserializeObject<StageResponse[]>(resultJson);
            
        }

        // �Ăяo������result�������Ăяo��
        result?.Invoke(response);
    }

    //�X�R�A�����L���O�ꗗ�擾API
    public IEnumerator GetScoreRanking(Action<ScoreRankingResponse[]> result)
    {
        //�X�e�[�W�ꗗ�擾API�����s
        UnityWebRequest request = UnityWebRequest.Get(
            API_BASE_URL + "scoreRanking");

        pub_UserID = userID;

        yield return request.SendWebRequest();
        ScoreRankingResponse[] response = null;

        if (request.result == UnityWebRequest.Result.Success && request.responseCode == 200)
        {
            //�A���Ă���JSON�t�@�C�����I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;
            response = JsonConvert.DeserializeObject<ScoreRankingResponse[]>(resultJson);

            Debug.Log("�X�R�A�擾����");

            Debug.Log(resultJson);
            
        }
            //�Ăяo������result�������Ăяo��
            result?.Invoke(response);
    }

    //�X�R�A�����L���O�ꗗ�擾API
    public IEnumerator GetMyScoreRanking(Action<ScoreRankingResponse[]> result)
    {
        //�X�e�[�W�ꗗ�擾API�����s
        UnityWebRequest request = UnityWebRequest.Get(
            API_BASE_URL + "getRank/"+userID.ToString());

        yield return request.SendWebRequest();
        ScoreRankingResponse[] response = null;

        if (request.result == UnityWebRequest.Result.Success && request.responseCode == 200)
        {
            //�A���Ă���JSON�t�@�C�����I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;
            response = JsonConvert.DeserializeObject<ScoreRankingResponse[]>(resultJson);

            Debug.Log("�X�R�A�擾����");

            //Debug.Log(resultJson);

        }
        //�Ăяo������result�������Ăяo��
        result?.Invoke(response);
    }

    //�X�R�A���M
    public IEnumerator StoreScore(int score , Action<bool> result)
    {
        //�T�[�o�[�ɑ���I�u�W�F�N�g���쐬
        StoreScoreReqest requestData = new StoreScoreReqest();

        requestData.Id = this.userID;
        requestData.Name = this.userName;
        requestData.Score = score;

        //�T�[�o�[�ɑ���I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(requestData);
        //���M
        UnityWebRequest request = UnityWebRequest.Post(
            API_BASE_URL + "users/sendScore", json, "application/json");

        yield return request.SendWebRequest();
        bool isSuccess = false;

        //�ʐM�����������ꍇ
        if (request.result == UnityWebRequest.Result.Success && request.responseCode == 200)
        {
            //�A���Ă���JSON�t�@�C�����I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;
            //StoreScoreReqest response = JsonConvert.DeserializeObject<StoreScoreReqest>(resultJson);

            isSuccess = true;

            Debug.Log("�ʐM���M����");
        }
        result?.Invoke(isSuccess);  //�����ŌĂяo������result�������Ăяo��
    }
}