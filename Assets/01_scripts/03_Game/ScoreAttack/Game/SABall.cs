using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;       //AudioManager���g���Ƃ��͂���using������

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
    { //�����Ȃ��悤�ɏd�͂�0�ɂ���
        GetComponent<Rigidbody2D>().gravityScale = Gravity;

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))//����������Tagutukeru�Ƃ����^�O������I�u�W�F�N�g����Ł`�Ƃ��������̉�
        {
            SEManager.Instance.Play(SEPath.HIT);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            // ���x����������O�ɂ���B
            rb.velocity = Vector3.zero;
            //�����Ȃ��悤�ɏd�͂�0�ɂ���
            GetComponent<Rigidbody2D>().gravityScale = 0;

            Invoke("OnGravity", 1);

        }
    }
}
