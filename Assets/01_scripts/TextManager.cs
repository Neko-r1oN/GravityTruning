using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextManager : MonoBehaviour
{
    private float repeatSpan;    //繰り返す間隔
    private float timeElapsed;   //経過時間

    // Start is called before the first frame update
    void Start()
    {
        //表示切り替え時間を指定
        repeatSpan = 0.5f;
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;     //時間をカウントする

        if (timeElapsed >= repeatSpan)
        {//時間経過でテキスト表示
            GetComponent<Text>().text = "データ読み込み中";
        }
        if (timeElapsed >= repeatSpan + 0.5f)
        {//時間経過でテキスト表示(役職)
            GetComponent<Text>().text = "データ読み込み中.";
        }
        if (timeElapsed >= repeatSpan + 1.0f)
        {//時間経過でテキスト表示(役職)
            GetComponent<Text>().text = "データ読み込み中..";
        }
        if (timeElapsed >= repeatSpan + 1.5f)
        {//時間経過でテキスト表示(役職)
            GetComponent<Text>().text = "データ読み込み中...";
            timeElapsed = 0;   //経過時間をリセットする
        }

    }
}
