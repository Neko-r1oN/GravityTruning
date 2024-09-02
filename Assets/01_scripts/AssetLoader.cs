using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class AssetLoader : MonoBehaviour
{

    [SerializeField] Slider loadingSlider;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loading());

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator loading()
    {  
        var handle = Addressables.UpdateCatalogs();
        yield return handle;

        //ダウンロード実行
        AsyncOperationHandle downloadHandle = Addressables.DownloadDependenciesAsync("default", false);

        while (downloadHandle.Status == AsyncOperationStatus.None)
        {
            loadingSlider.value = downloadHandle.GetDownloadStatus().Percent * 100;
            yield return null;
        }
        loadingSlider.value = 100;
        Addressables.Release(downloadHandle);
        //Addressables.Release(handle);

        //次のシーンに移動

        // シーン遷移
        Initiate.Fade("HomeScene", new Color(0, 0, 0, 1.0f), 5.0f);
    }
}
