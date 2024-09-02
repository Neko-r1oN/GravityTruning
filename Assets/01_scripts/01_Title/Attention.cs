using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  //DOTween���g���Ƃ��͂���using������




public class AttentionText : MonoBehaviour
{
    private Text AttentionTxt;

    private float timer;    //�J��Ԃ��Ԋu

    [Header("�X�^�[�g�J���[")]
    [SerializeField]
    Color32 startColor = new Color32(255, 255, 255, 0);
    //���[�v�I��(�܂�Ԃ�)���̐F��0�`255�܂ł̐����Ŏw��B
    [Header("�G���h�J���[")]
    [SerializeField]
    Color32 endColor = new Color32(255, 255, 255, 255);

    void Start()
    {
        AttentionTxt = GetComponent<Text>();
        AttentionTxt.color = startColor;

        timer = 0.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;     //���Ԃ��J�E���g����
        if (timer >= 0.5f)
        {
            AttentionTxt.color = Color.Lerp(AttentionTxt.color, new Color(1, 1, 1, 1), 2.0f * Time.deltaTime);




           
           Invoke("StartHomeScene", 5.0f);
            
        }
    }

    public void StartHomeScene()
    {
        // �V�[���J��
        Initiate.Fade("LoadScene", new Color(0, 0, 0, 255), 5.0f);
    }

}