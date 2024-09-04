using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject nameField;
    [SerializeField] Text nameText;
    private bool isClick;

    bool isSuccess;
    // Start is called before the first frame update
    void Start()
    {
        nameText.text = "";
        isClick = true;
        isSuccess = NetworkManager.Instance.LoadUserData();
        nameField.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if (isSuccess)
        {
            nameField.SetActive(false);
        }
        if (isClick) return;
            
        isClick = true;

        if(nameText.text =="")
        {
            nameText.text = "�Q�X�g";
        }
            //StartCoroutine(checkCatalog());

           


            //���[�U�[�f�[�^���ۑ�����Ă��Ȃ��ꍇ
            if (!isSuccess)
            {
                StartCoroutine(NetworkManager.Instance.StoreUser(
                    nameText.text,       //���O
                    Guid.NewGuid().ToString(),       //�p�X���[�h
                    result => {                      //����
                                             // �V�[���@��
                        //Initiate.Fade("LoadScene", new Color(0, 0, 0, 1.0f), 2.0f);
                    }));
                Debug.Log("�o�^����");
            }
            //���[�U�[�f�[�^���ۑ�����Ă���ꍇ
            else
            {
                Debug.Log("�o�^�ς�");
                // �V�[���J��
                //Initiate.Fade("LoadScene", new Color(0, 0, 0, 1.0f), 2.0f);
            }
    }

    public void OnClickStart()
    {
         isClick = false;
    }

    IEnumerator checkCatalog()
    {
        var checkHandle = Addressables.CheckForCatalogUpdates(false);
        yield return checkHandle;
        var updates = checkHandle.Result;
        Addressables.Release(checkHandle);

        if(updates.Count >= 1)
        {
            Initiate.Fade("LoadScene", new Color(0, 0, 0, 1.0f), 2.0f);
        }
        else
        {
            Initiate.Fade("HomeScene", new Color(0, 0, 0, 1.0f), 5.0f);
        }

    }

}
