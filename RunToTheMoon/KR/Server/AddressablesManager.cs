using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class AddressablesManager : MonoBehaviour
{
    // Single-tone Instance
    public static AddressablesManager Instance { get; private set; }

    // Level Loading
    private bool clearPreviousScene = false;
    private SceneInstance previousLoadedScene;
    //[SerializeField] string LableForBundleDown = "Scene";
    private AsyncOperationHandle<SceneInstance> handle;    
    long Downloadsize = 0;

    public Loading_SceneManager downloadProgressScript;
    

    private void Awake()
    {
        //Addressables.ClearDependencyCacheAsync("Assets/00.Scenes/Game 1.unity");

        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else Instance = this;

        //downloadProgressScript = GameObject.Find("Loading_SceneManager").GetComponent<Loading_SceneManager>();

        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadAddressableLevel(string addressableKey)
    {
        StartCoroutine(_LoadAddressableLevel(addressableKey));

        //// 이전 씬이 존재했다면, Release 해주고
        //if (clearPreviousScene)
        //{
        //    Addressables.UnloadSceneAsync(previousLoadedScene).Completed += (asyncHandle) =>
        //    {
        //        clearPreviousScene = false;
        //        previousLoadedScene = new SceneInstance();
        //        Debug.Log("Unloaded scene " + addressableKey + " successfully");
        //    };
        //}

        //// Release 해준 new SceneInstance() 에 메모리넣기
        //Addressables.LoadSceneAsync(addressableKey, LoadSceneMode.Single).Completed += (asyncHandle) =>
        //{
        //    clearPreviousScene = true;
        //    previousLoadedScene = asyncHandle.Result;
        //    Debug.Log("Loaded scene " + addressableKey + " successfully");
        //};
    }

    //IEnumerator _LoadAddressableLevel(string _addressableKey)
    //{
    //    // 이전 씬이 존재했다면, Release 해주고
    //    if (clearPreviousScene)
    //    {
    //        Addressables.UnloadSceneAsync(previousLoadedScene).Completed += (asyncHandle) =>
    //        {
    //            clearPreviousScene = false;                 // 이전 씬 릴리즈 상태 체크
    //            previousLoadedScene = new SceneInstance();  // 이전 씬 새로 로드
    //            //Addressables.Release(asyncHandle);          // 혹시 몰라서 릴리즈
    //            Debug.LogError("Unloaded scene " + _addressableKey + " successfully");
    //        };
           
    //    }

    //    var downloadScene = Addressables.LoadSceneAsync(_addressableKey, LoadSceneMode.Single);
    //    downloadScene.Completed += DownLoadScene;

    //    while (!downloadScene.IsDone)
    //    {
    //        var status = downloadScene.GetDownloadStatus();
    //        //var status = downloadScene.PercentComplete;
    //        float progress = status.Percent;
       
    //        downloadProgressScript.downloadSliderProgressInput = progress;
    //        downloadProgressScript.downloadProgressInput = progress * 100;
    //        yield return null;
    //    }
     
    //    downloadProgressScript.downloadProgressInput = 100;
    //    downloadProgressScript.downloadSliderProgressInput = 1;
    //}
    IEnumerator _LoadAddressableLevel(string _addressableKey)
    {
        // 이전 씬이 존재했다면, Release 해주고
        if (clearPreviousScene)
        {
            Addressables.UnloadSceneAsync(previousLoadedScene).Completed += (asyncHandle) =>
            {
                clearPreviousScene = false;                 // 이전 씬 릴리즈 상태 체크
                previousLoadedScene = new SceneInstance();  // 이전 씬 새로 로드
                //Addressables.Release(asyncHandle);          // 혹시 몰라서 릴리즈
                Debug.LogError("Unloaded scene " + _addressableKey + " successfully");
            };
        
        }
        var downloadScene = Addressables.LoadSceneAsync(_addressableKey, LoadSceneMode.Single);
        downloadScene.Completed += DownLoadScene;
  

        while (!downloadScene.IsDone)
        {
         
            var status = downloadScene.GetDownloadStatus();
            float status1 = downloadScene.PercentComplete;
            float progress = status1;
       
            downloadProgressScript.downloadSliderProgressInput = progress;
            downloadProgressScript.downloadProgressInput = progress * 100;
         //   Debug.LogError("progress"+ progress+ "downloadProgressInput"+ downloadProgressScript.downloadProgressInput);
            yield return null;
        }
    
        downloadProgressScript.downloadProgressInput = 100;
        downloadProgressScript.downloadSliderProgressInput = 1;
    }

    private void DownLoadScene(AsyncOperationHandle<SceneInstance> _handle)
    {

        if (_handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.LogError(_handle.Result.Scene.name + " successfully loaded");
            clearPreviousScene = true;
            previousLoadedScene = _handle.Result;
        }
    }


    //private IEnumerator _UnloadScene()
    //{
    //    yield return new WaitForSeconds(10);

    //    Addressables.UnloadSceneAsync(handle, true).Completed += op =>
    //    {
    //        if (op.Status == AsyncOperationStatus.Succeeded)
    //        {
    //            // UnloadSceneAsync
    //        }
    //    };
    //}
}
