using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneReset : MonoBehaviour
{
    private string GaneScene;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ResetScene", 0.5f);
        
    }

    // Update is called once per frame
    private void ResetScene()
    {
        GaneScene = "GameScene";
        

        GaneScene += StageSelect.stageID;
        Debug.Log(GaneScene);

        // ÉVÅ[ÉìëJà⁄

        Addressables.LoadScene(GaneScene, LoadSceneMode.Single);

    }
}
