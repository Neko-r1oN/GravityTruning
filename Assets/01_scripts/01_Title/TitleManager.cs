using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private bool isClick;
    // Start is called before the first frame update
    void Start()
    {
        isClick = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {
            if (isClick)
            {
                return;
            }
            isClick = true;

            // ÉVÅ[ÉìëJà⁄
            Initiate.Fade("HomeScene", new Color(0, 0, 0, 1.0f), 2.0f);
        }

        }
}
