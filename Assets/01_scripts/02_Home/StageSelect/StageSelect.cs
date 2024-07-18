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
		// �V�[���؂�ւ�
		SceneManager.LoadScene(bossID + 1);
		// �V�[���J��
		Initiate.Fade("GameScene", new Color(0, 0, 0, 0), 2.0f);
	}
}