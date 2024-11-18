using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneReset : MonoBehaviour
{
    private string ganeScene;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ResetScene", 0.5f);
        
    }

    // Update is called once per frame
    private void ResetScene()
    {
        ganeScene = "GameScene";
        

        ganeScene += StageSelect.stageID;
        Debug.Log(ganeScene);

        // ÉVÅ[ÉìëJà⁄

        Addressables.LoadScene(ganeScene, LoadSceneMode.Single);

    }
}
