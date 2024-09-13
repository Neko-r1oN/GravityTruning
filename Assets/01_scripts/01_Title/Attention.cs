using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  //DOTween���g���Ƃ��͂���using������
using UnityEngine.AddressableAssets;



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

    //�n���h���m�F�ϐ�
    private bool isCheck;

    // Start is called before the first frame update
    
        
    
    void Start()
    {
        isCheck = true;

        AttentionTxt = GetComponent<Text>();
        AttentionTxt.color = startColor;
        Invoke("StartHomeScene", 3.0f);
        timer = 0.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;     //���Ԃ��J�E���g����
        if (timer >= 0.5f)
        {
            AttentionTxt.color = Color.Lerp(AttentionTxt.color, new Color(1, 1, 1, 1), 2.0f * Time.deltaTime);
            
        }

    }

    public void StartHomeScene()
    {
        isCheck = false;


        StartCoroutine(checkCatalog());
    }

    IEnumerator checkCatalog()
    {
        var checkHandle = Addressables.CheckForCatalogUpdates(false);
        yield return checkHandle;
        var updates = checkHandle.Result;
        Addressables.Release(checkHandle);

        if (updates.Count >= 1)
        {
            Initiate.Fade("LoadScene", new Color(0, 0, 0, 1.0f), 2.0f);
        }
        else
        {
            Initiate.Fade("HomeScene", new Color(0, 0, 0, 1.0f), 5.0f);
        }


    }

}