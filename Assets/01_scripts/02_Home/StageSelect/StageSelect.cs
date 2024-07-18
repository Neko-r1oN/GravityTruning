using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		SceneManager.LoadScene(bossID + 1);
		// シーン遷移
		Initiate.Fade("GameScene", new Color(0, 0, 0, 0), 2.0f);
	}
}