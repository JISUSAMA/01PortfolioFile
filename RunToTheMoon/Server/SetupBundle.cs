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
    [Header("�ٿ�ε带 ���ϴ� ���� �Ǵ� ����鿡 ���Ե� ���̺��� �ƹ��ų� �Է����ּ���.")]
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
        DownloadFileSize();  // ������ üũ

        var downloadBundle = Addressables.DownloadDependenciesAsync(LableForBundleDown);
        downloadBundle.Completed += DownloadComplete;
        //Addressables.DownloadDependenciesAsync(LableForBundleDown).Completed +=
        //    (AsyncOperationHandle Handle) =>
        //    {
        //        //DownloadPercent������Ƽ�� �ٿ�ε� ������ Ȯ���� �� ����.
        //        //ex) float DownloadPercent = Handle.PercentComplete;

        //        Debug.Log("�ٿ�ε� �Ϸ�!");

        //        //�ٿ�ε尡 ������ �޸� ����.
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
        //DownloadPercent������Ƽ�� �ٿ�ε� ������ Ȯ���� �� ����.
        //ex) float DownloadPercent = Handle.PercentComplete;

        Debug.Log("�ٿ�ε� �Ϸ�!");

        //�ٿ�ε尡 ������ �޸� ����.
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
        //ũ�⸦ Ȯ���� ���� �Ǵ� ����鿡 ���Ե� ���̺��� ���ڷ� �ָ� ��.
        //longŸ������ ��ȯ�Ǵ°� Ư¡��.
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

                //�޸� ����.
                Addressables.Release(SizeHandle);
            };
    }
}
