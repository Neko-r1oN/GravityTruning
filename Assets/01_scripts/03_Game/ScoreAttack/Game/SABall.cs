using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;       //AudioManagerを使うときはこのusingを入れる

public class SABall : MonoBehaviour
{
    private bool RespownFlag;
    private float Gravity;
    // Start is called before the first frame update
    void Start()
    {
        Gravity = 1.21f;
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnGravity()
    { //落ちないように重力を0にする
        GetComponent<Rigidbody2D>().gravityScale = Gravity;

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))//さっきつけたTagutukeruというタグがあるオブジェクト限定で～という条件の下
        {
            SEManager.Instance.Play(SEPath.HIT);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            // 速度をいったん０にする。
            rb.velocity = Vector3.zero;
            //落ちないように重力を0にする
            GetComponent<Rigidbody2D>().gravityScale = 0;

            Invoke("OnGravity", 1);

        }
    }
}
