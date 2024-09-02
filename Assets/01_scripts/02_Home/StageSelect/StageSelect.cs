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
		// �V�[���؂�ւ�
		//SceneManager.LoadScene(bossID + 1);
		// �V�[���J��
		Addressables.LoadScene(bossID + 1, LoadSceneMode.Single);
	}
}