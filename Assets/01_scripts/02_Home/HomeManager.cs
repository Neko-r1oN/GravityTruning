using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    [SerializeField] GameObject StartButton;
    [SerializeField] GameObject BuckButton;
    [SerializeField] GameObject StageSelector;

    

    void Start()
    {
        StartButton.SetActive(true);
        BuckButton.SetActive(false);
        StageSelector.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickSelectStage()
    {
        BuckButton.SetActive(true);
        StageSelector.SetActive(true);

        StartButton.SetActive(false);

        
        // ÉVÅ[ÉìëJà⁄
        /*Initiate.Fade("GameScene", new Color(0, 0, 0, 1.0f), 2.0f);*/
    }

    public void OnClickSelectReturn()
    {
       

        Invoke("CloseMenu", 0.5f);

        

    }

    void CloseMenu()
    {
        StageSelector.SetActive(false);
        BuckButton.SetActive(false);

        StartButton.SetActive(true);
    }
}
