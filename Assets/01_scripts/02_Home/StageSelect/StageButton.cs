using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    [SerializeField] int stageNum;
    [SerializeField] Text stageText;

    static public string GaneScene;

    // Start is called before the first frame update
    void Start()
    {
        stageText.text = "" + stageNum;
        GaneScene = "GameScene";
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    public void OnclickStage()
    {
        StageSelect.stageID = stageNum;

        GaneScene += stageNum;
        Debug.Log(GaneScene);
        // ÉVÅ[ÉìëJà⁄
        Initiate.Fade(GaneScene, new Color(0, 0, 0, 1), 2.0f);
    }
}
