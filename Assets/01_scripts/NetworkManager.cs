using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEngine.Networking;
using System.IO;

public class NetworkManager : MonoBehaviour
{
    //https通信URL
    //https://api-gravityturning.japaneast.cloudapp.azure.com/


    //https通信用
    //https://api-gravityturning.japaneast.cloudapp.azure.com/api/

    //ローカル(通信確認用)
    //http://localhost:8000/api/

    const string API_BASE_URL = "https://api-gravityturning.japaneast.cloudapp.azure.com/api/";

    public int userID = 0;

    static public string pub_UserName { get; private set; }
    static public int pub_UserID { get; private set; }
    public string userName = "";
    private string password = "";
    private string authToken; //APIトークン

    /*static public int userID { get; private set; }
    static public string userName { get; private set; }
    static public string passWord { get; private set; }*/

    //getプロパティを呼び出した初回時に static で保持
    private static NetworkManager instance;

   
    public static NetworkManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObj = new GameObject("NetworkManager");
                //GameObject生成、NetworkManagerコンポーネントを追加
                instance = gameObj.AddComponent<NetworkManager>();
                //シーン移動時に削除しないようにする
                DontDestroyOnLoad(gameObj);

            }
            return instance;
        }
    }


    /*==============================*/
    //ユーザー登録処理
    /*==============================*/

    public IEnumerator StoreUser(string name, string password, Action<bool> result)
    {
        //サーバーに送るオブジェクトを作成
        StoreUserRequest requestData = new StoreUserRequest();
        requestData.Name = name;
        requestData.Password = password;
        //サーバーに送るオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(requestData);
        //送信
        UnityWebRequest request = UnityWebRequest.Post(
            API_BASE_URL + "users/store", json, "application/json");

        yield return request.SendWebRequest();
        bool isSuccess = false;

       

        //通信が成功した場合
        if (request.result == UnityWebRequest.Result.Success && request.responseCode == 200)
        {
            //帰ってきたJSONファイルをオブジェクトに変換
            string resultJson = request.downloadHandler.text;
            StoreUserResponse response = JsonConvert.DeserializeObject<StoreUserResponse>(resultJson);

            //ファイルにユーザーを保存
            this.userName = name;
            this.password = password;
            this.userID = response.UserID;
            //this.authToken = response.AuthToken;
            SaveUserData();
            isSuccess = true;
        }
        result?.Invoke(isSuccess);  //ここで呼び出し元のresult処理を呼び出す
    }


    //データ保存処理
    private void SaveUserData()
    {
        SaveData saveData = new SaveData();
        saveData.Name = this.userName;
        saveData.UserID = this.userID;
        saveData.AuthToken = this.authToken;
        string json = JsonConvert.SerializeObject(saveData);

        //ファイルにJsonを保存
        var writer = new StreamWriter(Application.persistentDataPath + "/saveData.json");   // Application.persistentDataPathは保存ファイルを置く場所

        writer.Write(json);
        writer.Flush();
        writer.Close();

    }

    //ユーザー情報読み込み処理
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

        //ローカルファイルから各種値を取得
        this.userID = saveData.UserID;
        this.userName = saveData.Name;
        this.authToken = saveData.AuthToken;
        //読み込み判定
        return true;

    }


    //トークン生成処理  ※ユーザーが登録済み且つTokenがnullの場合のみ実行
    public IEnumerator CreateToken(Action<bool>response)
    {
        var requestData = new
        {
            user_id = this.userID,
        };

        string json = JsonConvert.SerializeObject(requestData);
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "users/createToken", json, "application/json");    //トークン生成用のAPIURLを記入

        yield return request.SendWebRequest();
        if(request.result == UnityWebRequest.Result.Success)
        {
            //帰ってきたJSONファイルをオブジェクトに変換
            string resultJson = request.downloadHandler.text;
            CreateTokenResponse tokenResponse = JsonConvert.DeserializeObject<CreateTokenResponse>(resultJson);

            //ファイルにユーザーを保存
            this.userID = tokenResponse.AuthToken;
            //this.authToken = response.AuthToken;
            SaveUserData();
        }
        response?.Invoke(request.result == UnityWebRequest.Result.Success);
    }


    //ステージ情報取得処理
    public IEnumerator GetStage(Action<StageResponse[]> result)
    {

        UnityWebRequest request = UnityWebRequest.Get(
            API_BASE_URL + "stageList");

        yield return request.SendWebRequest();

        StageResponse[] response = null;
        //Rootobject response = new Rootobject();

        //通信が成功した場合
        if (request.result == UnityWebRequest.Result.Success && request.responseCode == 200)
        {
            //帰ってきたJSONファイルをオブジェクトに変換
            string resultJson = request.downloadHandler.text;
            Debug.Log(resultJson);
            response = JsonConvert.DeserializeObject<StageResponse[]>(resultJson);
            
        }

        // 呼び出し元のresult処理を呼び出す
        result?.Invoke(response);
    }

    //スコアランキング一覧取得API
    public IEnumerator GetScoreRanking(Action<ScoreRankingResponse[]> result)
    {
        //ステージ一覧取得APIを実行
        UnityWebRequest request = UnityWebRequest.Get(
            API_BASE_URL + "scoreRanking");

        pub_UserID = userID;

        yield return request.SendWebRequest();
        ScoreRankingResponse[] response = null;

        if (request.result == UnityWebRequest.Result.Success && request.responseCode == 200)
        {
            //帰ってきたJSONファイルをオブジェクトに変換
            string resultJson = request.downloadHandler.text;
            response = JsonConvert.DeserializeObject<ScoreRankingResponse[]>(resultJson);

            Debug.Log("スコア取得完了");

            Debug.Log(resultJson);
            
        }
            //呼び出し元のresult処理を呼び出す
            result?.Invoke(response);
    }

    //スコアランキング一覧取得API
    public IEnumerator GetMyScoreRanking(Action<ScoreRankingResponse[]> result)
    {
        //ステージ一覧取得APIを実行
        UnityWebRequest request = UnityWebRequest.Get(
            API_BASE_URL + "getRank/"+userID.ToString());

        yield return request.SendWebRequest();
        ScoreRankingResponse[] response = null;

        if (request.result == UnityWebRequest.Result.Success && request.responseCode == 200)
        {
            //帰ってきたJSONファイルをオブジェクトに変換
            string resultJson = request.downloadHandler.text;
            response = JsonConvert.DeserializeObject<ScoreRankingResponse[]>(resultJson);

            Debug.Log("スコア取得完了");

            //Debug.Log(resultJson);

        }
        //呼び出し元のresult処理を呼び出す
        result?.Invoke(response);
    }

    //スコア送信
    public IEnumerator StoreScore(int score , Action<bool> result)
    {
        //サーバーに送るオブジェクトを作成
        StoreScoreReqest requestData = new StoreScoreReqest();

        requestData.Id = this.userID;
        requestData.Name = this.userName;
        requestData.Score = score;

        //サーバーに送るオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(requestData);
        //送信
        UnityWebRequest request = UnityWebRequest.Post(
            API_BASE_URL + "users/sendScore", json, "application/json");

        yield return request.SendWebRequest();
        bool isSuccess = false;

        //通信が成功した場合
        if (request.result == UnityWebRequest.Result.Success && request.responseCode == 200)
        {
            //帰ってきたJSONファイルをオブジェクトに変換
            string resultJson = request.downloadHandler.text;
            //StoreScoreReqest response = JsonConvert.DeserializeObject<StoreScoreReqest>(resultJson);

            isSuccess = true;

            Debug.Log("通信送信成功");
        }
        result?.Invoke(isSuccess);  //ここで呼び出し元のresult処理を呼び出す
    }
}