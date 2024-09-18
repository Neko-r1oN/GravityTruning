using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleController : MonoBehaviour
{
    //シーンディレクター
    public GameManager SceneDirector;

    
    //カラー
    public int ColorType;
    //合体済みフラグ
    public bool IsMerged;

    

    // Update is called once per frame
    void Update()
    {        
        //画面外に落ちたら消す
        if (transform.position.y < -10)
        {
            // シーン遷移
            Initiate.Fade("GameResetScene", new Color(0, 0, 0, 1.0f), 5.0f);
        }

        
    }

    //当たり判定が発生したら呼ばれる
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //バブルじゃない場合
        BubbleController bubble = collision.gameObject.GetComponent<BubbleController>();
        if (!bubble) return;

        //合体させる
        SceneDirector.Merge(this, bubble);
        
    }
}
