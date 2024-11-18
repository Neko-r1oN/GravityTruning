using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;       //AudioManagerを使うときはこのusingを入れる

public class SABall : MonoBehaviour
{
    private bool respownFlag;
    private float gravity;

    // Start is called before the first frame update
    void Start()
    {
        gravity = 1.21f;
        
    }

   
    void OnGravity()
    { //落ちないように重力を0にする
        GetComponent<Rigidbody2D>().gravityScale = gravity;

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))//さっきつけたTagutukeruというタグがあるオブジェクト限定で～という条件の下
        {
            SEManager.Instance.Play(
           audioPath: SEPath.HIT, //再生したいオーディオのパス
           volumeRate: 0.2f,                //音量の倍率
           delay: 0,                //再生されるまでの遅延時間
           pitch: 1,                //ピッチ
           isLoop: false             //ループ再生するか
           );
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            // 速度をいったん０にする。
            rb.velocity = Vector3.zero;
            //落ちないように重力を0にする
            GetComponent<Rigidbody2D>().gravityScale = 0;

            Invoke("OnGravity", 1);

        }
    }
}
