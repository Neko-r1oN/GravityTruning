using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextManager : MonoBehaviour
{
    private float repeatSpan;    //�J��Ԃ��Ԋu
    private float timeElapsed;   //�o�ߎ���

    // Start is called before the first frame update
    void Start()
    {
        //�\���؂�ւ����Ԃ��w��
        repeatSpan = 0.5f;
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;     //���Ԃ��J�E���g����

        if (timeElapsed >= repeatSpan)
        {//���Ԍo�߂Ńe�L�X�g�\��
            GetComponent<Text>().text = "�f�[�^�ǂݍ��ݒ�";
        }
        if (timeElapsed >= repeatSpan + 0.5f)
        {//���Ԍo�߂Ńe�L�X�g�\��(��E)
            GetComponent<Text>().text = "�f�[�^�ǂݍ��ݒ�.";
        }
        if (timeElapsed >= repeatSpan + 1.0f)
        {//���Ԍo�߂Ńe�L�X�g�\��(��E)
            GetComponent<Text>().text = "�f�[�^�ǂݍ��ݒ�..";
        }
        if (timeElapsed >= repeatSpan + 1.5f)
        {//���Ԍo�߂Ńe�L�X�g�\��(��E)
            GetComponent<Text>().text = "�f�[�^�ǂݍ��ݒ�...";
            timeElapsed = 0;   //�o�ߎ��Ԃ����Z�b�g����
        }

    }
}
