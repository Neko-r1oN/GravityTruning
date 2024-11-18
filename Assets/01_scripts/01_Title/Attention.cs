using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  //DOTweenを使うときはこのusingを入れる
using UnityEngine.AddressableAssets;



public class AttentionText : MonoBehaviour
{
    private Text attentionTxt;

    private float timer;    //繰り返す間隔

    [Header("スタートカラー")]
    [SerializeField]
    Color32 startColor = new Color32(255, 255, 255, 0);
    
    [Header("エンドカラー")]
    [SerializeField]
    Color32 endColor = new Color32(255, 255, 255, 255);

    //ハンドル確認変数
    private bool isCheck;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        isCheck = true;

        attentionTxt = GetComponent<Text>();
        attentionTxt.color = startColor;
        Invoke("StartHomeScene", 3.0f);
        timer = 0.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;     //時間をカウントする
        if (timer >= 0.5f)
        {
            attentionTxt.color = Color.Lerp(attentionTxt.color, new Color(1, 1, 1, 1), 2.0f * Time.deltaTime);
        }
    }


    public void StartHomeScene()
    {
        isCheck = false;

        StartCoroutine(checkCatalog());
    }

    IEnumerator checkCatalog()
    {
        var checkHandle = Addressables.CheckForCatalogUpdates(false);
        yield return checkHandle;
        var updates = checkHandle.Result;
        Addressables.Release(checkHandle);

        if (updates.Count >= 1)
        {
            Initiate.Fade("LoadScene", new Color(0, 0, 0, 1.0f), 2.0f);
        }
        else
        {
            Initiate.Fade("HomeScene", new Color(0, 0, 0, 1.0f), 5.0f);
        }
    }
}