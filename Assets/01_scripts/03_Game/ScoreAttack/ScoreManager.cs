using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    static public int Score { get; private set; }

    int basicScore = 200;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "" + Score;
    }
    public void AddScore()
    {
        
        Score += basicScore;
    }
}
