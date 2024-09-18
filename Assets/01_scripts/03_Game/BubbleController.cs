using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleController : MonoBehaviour
{
    //�V�[���f�B���N�^�[
    public GameManager SceneDirector;

    
    //�J���[
    public int ColorType;
    //���̍ς݃t���O
    public bool IsMerged;

    

    // Update is called once per frame
    void Update()
    {        
        //��ʊO�ɗ����������
        if (transform.position.y < -10)
        {
            // �V�[���J��
            Initiate.Fade("GameResetScene", new Color(0, 0, 0, 1.0f), 5.0f);
        }

        
    }

    //�����蔻�肪����������Ă΂��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�o�u������Ȃ��ꍇ
        BubbleController bubble = collision.gameObject.GetComponent<BubbleController>();
        if (!bubble) return;

        //���̂�����
        SceneDirector.Merge(this, bubble);
        
    }
}
