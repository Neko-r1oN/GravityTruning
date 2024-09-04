using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using KanKikuchi.AudioManager;       //AudioManager���g���Ƃ��͂���using������
using DG.Tweening;                   //DOTween���g���Ƃ��͂���using������
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class RankingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
}
