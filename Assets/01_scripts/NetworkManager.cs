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
    private int userID = 0;
    private string userName = "";
    private string password = "";


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
        //�X�e�[�W�ꗗ�擾API�����s
        UnityWebRequest request = UnityWebRequest.Get(
            API_BASE_URL + "stageList");

        yield return request.SendWebRequest();
        //�ʐM�����������ꍇ
        if (request.result == UnityWebRequest.Result.Success && request.responseCode == 200)
        {
            //�A���Ă���JSON�t�@�C�����I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;
            StoreUserResponse response = JsonConvert.DeserializeObject<StoreUserResponse>(resultJson);

            this.userID = response.UserID;
            SaveUserData();
        }
        else
        {
            result?.Invoke(null);
        }
    }


    public IEnumerator GetRanking(Action<RankingResponse[]> result)
    {
        //�X�e�[�W�ꗗ�擾API�����s
        UnityWebRequest request = UnityWebRequest.Get(
            API_BASE_URL + "users/scoreRanking");

        // ���ʂ���M����܂őҋ@
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            // �ʐM�����������ꍇ�A�Ԃ��Ă���JSON���I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;
            RankingResponse[] response = JsonConvert.DeserializeObject<RankingResponse[]>(resultJson);

            // �Ăяo������result�������Ăяo��
            result?.Invoke(response);
        }
        else
        {
            // �Ăяo������result�������Ăяo��
            result?.Invoke(null);
        }
    }
}