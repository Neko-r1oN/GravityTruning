using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBall : MonoBehaviour
{
    private float Gravity;
    // Start is called before the first frame update
    void Start()
    {
        Gravity = 0.5f;
        
    }

    void OnGravity()
    { //落ちないように重力を0にする
        GetComponent<Rigidbody2D>().gravityScale = Gravity;

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            // 速度をいったん０にする。
            rb.velocity = Vector3.zero;
            //落ちないように重力を0にする
            GetComponent<Rigidbody2D>().gravityScale = 0.2f;

            //Invoke("OnGravity", 0.3f);

        }
    }
}
