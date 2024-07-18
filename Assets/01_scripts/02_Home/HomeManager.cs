using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
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
        // ÉVÅ[ÉìëJà⁄
        Initiate.Fade("GameScene", new Color(0, 0, 0, 1.0f), 2.0f);
    }
}
