using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class TitleManager : MonoBehaviour
{
    //���O�t�B�[���h
    [SerializeField] GameObject nameField;
    [SerializeField] Text nameText;

    //�{�^���֌W
    [SerializeField] GameObject dummyButton;
    [SerializeField] GameObject startButton;

    //����p�ϐ�
    private bool isClick;
    bool isSuccess;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = "";
        isClick = true;
        isSuccess = NetworkManager.Instance.LoadUserData();
        nameField.SetActive(true);

        dummyButton.SetActive(true);
        startButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //�[���ɃA�J�E���g��񂪂������ꍇ
        if (isSuccess)
        {//����o�^�p�̃{�^�����\��

            dummyButton.SetActive(false);
            startButton.SetActive(true);
            nameField.SetActive(false);
        }
        //��񂪂Ȃ������ꍇ
        else
        {
            //���͗��ɕ��������͂���Ă���ꍇ
            if (nameText.text != "")
            {
                dummyButton.SetActive(false);
                startButton.SetActive(true);
            }
            //���L���̏ꍇ
            else
            {
                dummyButton.SetActive(true);
                startButton.SetActive(false);
            }
        }
        
        if (isClick) return;
            
        isClick = true;

             
    }

    public void UserCreate()
    {
        //���[�U�[�f�[�^���ۑ�����Ă��Ȃ��ꍇ
        if (!isSuccess)
        {
            StartCoroutine(NetworkManager.Instance.StoreUser(
                nameText.text,       //���O
                Guid.NewGuid().ToString(),       //�p�X���[�h
                result =>
                {                      //����
                    Debug.Log("�o�^����");
                }));

        }
        //���[�U�[�f�[�^���ۑ�����Ă���ꍇ
        else
        {
            Debug.Log("�o�^�ς�");
        }
    }
}
