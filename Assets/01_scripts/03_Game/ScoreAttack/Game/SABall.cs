using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;       //AudioManager���g���Ƃ��͂���using������

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
    { //�����Ȃ��悤�ɏd�͂�0�ɂ���
        GetComponent<Rigidbody2D>().gravityScale = gravity;

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))//����������Tagutukeru�Ƃ����^�O������I�u�W�F�N�g����Ł`�Ƃ��������̉�
        {
            SEManager.Instance.Play(
           audioPath: SEPath.HIT, //�Đ��������I�[�f�B�I�̃p�X
           volumeRate: 0.2f,                //���ʂ̔{��
           delay: 0,                //�Đ������܂ł̒x������
           pitch: 1,                //�s�b�`
           isLoop: false             //���[�v�Đ����邩
           );
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            // ���x����������O�ɂ���B
            rb.velocity = Vector3.zero;
            //�����Ȃ��悤�ɏd�͂�0�ɂ���
            GetComponent<Rigidbody2D>().gravityScale = 0;

            Invoke("OnGravity", 1);

        }
    }
}
