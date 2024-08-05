using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Initiate.Fade(GaneScene, new Color(0, 0, 0, 1.0f), 5.0f);
        
    }
}
