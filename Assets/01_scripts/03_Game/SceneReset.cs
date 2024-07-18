using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ResetScene", 0.5f);
    }

    // Update is called once per frame
    private void ResetScene()
    {
        // ƒV[ƒ“‘JˆÚ
        Initiate.Fade(StageButton.GaneScene, new Color(0, 0, 0, 1.0f), 5.0f);
    }
}
