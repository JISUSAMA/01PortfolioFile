using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UI;

public class SetupBundle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI SizeText;
    [SerializeField] TextMeshProUGUI LoadingText;

    [Space]
    [Header("다운로드를 원하는 번들 또는 번들들에 포함된 레이블중 아무거나 입력해주세요.")]
    [SerializeField] string LableForBundleDown = "Scene";

    public bool isReady;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //Time.fixedDeltaTime = 1f / 60f;

        isReady = false;
        //Addressables.ClearDependencyCacheAsync(LableForBundleDown);
        StartCoroutine(_AssetsBundleDown());
    }

    IEnumerator _AssetsBundleDown()
    {
        DownloadFileSize();  // 사이즈 체크

        var downloadBundle = Addressables.DownloadDependenciesAsync(LableForBundleDown);
        downloadBundle.Completed += DownloadComplete;
        //Addressables.DownloadDependenciesAsync(LableForBundleDown).Completed +=
        //    (AsyncOperationHandle Handle) =>
        //    {
        //        //DownloadPercent프로퍼티로 다운로드 정도를 확인할 수 있음.
        //        //ex) float DownloadPercent = Handle.PercentComplete;

        //        Debug.Log("다운로드 완료!");

        //        //다운로드가 끝나면 메모리 해제.
        //        Addressables.Release(Handle);

        //    };
        while (!downloadBundle.IsDone)
        {
            var status = downloadBundle.GetDownloadStatus();
            float progress = status.Percent;
            Debug.Log("progress : " + progress);
            LoadingText.text = "Loading ... " + (progress*100).ToString("N0") + "%";
            yield return null;
        }
        
    }
    //SceneInstance m_LoadedScene;
    private void DownloadComplete(AsyncOperationHandle _handle)
    {
        //DownloadPercent프로퍼티로 다운로드 정도를 확인할 수 있음.
        //ex) float DownloadPercent = Handle.PercentComplete;

        Debug.Log("다운로드 완료!");

        //다운로드가 끝나면 메모리 해제.
        //Addressables.Release(_handle);
        //m_LoadedScene = _handle;

        isReady = true;

        LoadingText.text = "Loading ... 100%";

        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        LoadingText.text = "Download Completed";
        yield return new WaitForSeconds(1f);
        LoadingText.text = "";
        yield return null;
    }

    private void DownloadFileSize()
    {
        //크기를 확인할 번들 또는 번들들에 포함된 레이블을 인자로 주면 됨.
        //long타입으로 반환되는게 특징임.
        Addressables.GetDownloadSizeAsync(LableForBundleDown).Completed +=
            (AsyncOperationHandle<long> SizeHandle) =>
            {
                string sizeText = string.Concat(SizeHandle.Result, " byte");

                if (SizeHandle.Result == 0)
                {
                    LoadingText.text = "";
                }

                //SizeText.text = sizeText;
                Debug.Log("sizeText : " + sizeText);
                
                //SizeText.text = sizeText;

                //메모리 해제.
                Addressables.Release(SizeHandle);
            };
    }
}
