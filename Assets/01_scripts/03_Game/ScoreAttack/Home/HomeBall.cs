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
    { //�����Ȃ��悤�ɏd�͂�0�ɂ���
        GetComponent<Rigidbody2D>().gravityScale = Gravity;

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            // ���x����������O�ɂ���B
            rb.velocity = Vector3.zero;
            //�����Ȃ��悤�ɏd�͂�0�ɂ���
            GetComponent<Rigidbody2D>().gravityScale = 0.2f;

            //Invoke("OnGravity", 0.3f);

        }
    }
}
