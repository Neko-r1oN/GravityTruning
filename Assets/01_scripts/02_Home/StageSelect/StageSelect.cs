using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;

public class StageSelect : MonoBehaviour
{
	static public int stageID;

    private void Start()
    {
		stageID = 0;
    }
    public void OnStageSelectButtonPressed(int bossID)
	{
		// シーン切り替え
		//SceneManager.LoadScene(bossID + 1);
		// シーン遷移
		Addressables.LoadScene(bossID + 1, LoadSceneMode.Single);
	}
}