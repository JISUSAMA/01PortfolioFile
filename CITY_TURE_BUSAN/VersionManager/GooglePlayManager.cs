using UnityEngine;
using Cysharp.Threading.Tasks;
using Google.Play.Common;
using Google.Play.AppUpdate;
using System;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

/// <summary>
/// Service URL
/// https://developer.android.com/guide/playcore/in-app-updates/unity?hl=ko
/// </summary>

public class GooglePlayManager : MonoBehaviour
{
    public static GooglePlayManager Instance { get; private set; }
    
    AppUpdateManager appUpdateManager = null;
    public GameObject versionBackground;
    public TMP_Text updateStatusLog;
    public Slider downloadProgressbar;    
    public float fadeTime = 1.2f;

    Button versionCheckButton;
    
    int isAppUpdateErrorCode = (int)AppUpdateStatus.Unknown;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.Log("GooglePlayManager Instance is not null");
            Destroy(gameObject);
        }

        //Instance = this;

        //if (Instance != null)
        //{
        //    Debug.Log("GooglePlayManager Instance is not null");
        //}

        versionCheckButton = GetComponent<Button>();
    }

    private void Start()
    {                
        _VersionCheck();
    }

    private void OnDestroy()
    {
        Instance = null;

        Debug.Log($"OnDestroy");
    }

    public void _VersionCheck()
    {        
        versionCheckButton.interactable = false;
        VersionCheck().Forget();
    }
    public async UniTaskVoid VersionCheck()
    {
        if (Instance != null)
        {
            isAppUpdateErrorCode = await UpdateApp();

            Debug.Log("isYesOrNoOrFailed : " + isAppUpdateErrorCode);

            if (isAppUpdateErrorCode == (int)UpdateAvailability.UpdateNotAvailable || isAppUpdateErrorCode == (int)AppUpdateStatus.Installed)
            {
                // 정상동작 : 업데이트 완료 후 or 업데이트가 없음
                versionCheckButton.interactable = true;
                versionBackground.SetActive(false);
            }
            // 업데이트 실패
            else if (isAppUpdateErrorCode == (int)AppUpdateStatus.Failed ||
                    isAppUpdateErrorCode == (int)AppUpdateStatus.Canceled)
            {
                versionCheckButton.interactable = true;
            }
            else if (isAppUpdateErrorCode == (int)AppUpdateStatus.Unknown || isAppUpdateErrorCode == (int)UpdateAvailability.Unknown)
            {
                Debug.Log("isAppUpdateErrorCode -> AppUpdateStatus.Unknown");
            }
            else
            {
                Debug.Log($">> isAppUpdateErrorCode -> {isAppUpdateErrorCode}");
                versionCheckButton.interactable = true;
            }
        }
    }
    ///// <summary>
    ///// 인앱 업데이트 호출 함수.
    ///// </summary>
    ///// <returns></returns>
    public async UniTask<int> UpdateApp()
    {
        Debug.Log(">>>> UpdateApp");

        try
        {
            appUpdateManager = new AppUpdateManager();  // Create AppUpdateManager 

            // get appupdate info
            PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation = appUpdateManager.GetAppUpdateInfo();
            await appUpdateInfoOperation;   // getupdateinfo start

            if (appUpdateInfoOperation.IsSuccessful)
            {
                var appUpdateInfoResult = appUpdateInfoOperation.GetResult();

                if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
                {
                    updateStatusLog.text = "Update Available.";

                    var appUpdateOptions = AppUpdateOptions.FlexibleAppUpdateOptions();

                    var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfoResult, appUpdateOptions);

                    while (!startUpdateRequest.IsDone)
                    {
                        await UniTask.Yield();
                        Debug.Log("startUpdateRequest.Status : " + startUpdateRequest.Status);                        
                        Debug.Log("DownloadProgress : " + startUpdateRequest.DownloadProgress);
                        Debug.Log($"{startUpdateRequest.BytesDownloaded} / {startUpdateRequest.TotalBytesToDownload}");

                        if (startUpdateRequest.Status == AppUpdateStatus.Pending)
                        {
                            downloadProgressbar.gameObject.SetActive(true);
                        }
                        else if (startUpdateRequest.Status == AppUpdateStatus.Downloading)
                        {
                            updateStatusLog.text = $"Downloading ... {Mathf.Floor(startUpdateRequest.DownloadProgress * 100)}% ({FormatBytes(startUpdateRequest.BytesDownloaded)}/{FormatBytes(startUpdateRequest.TotalBytesToDownload)})";
                            downloadProgressbar.value = startUpdateRequest.DownloadProgress;
                        }
                        else if (startUpdateRequest.Status == AppUpdateStatus.Downloaded)
                        {
                            downloadProgressbar.gameObject.SetActive(false);
                        }
                    }

                    var result = appUpdateManager.CompleteUpdate();

                    while (!result.IsDone)
                    {
                        await UniTask.Yield();
                        Debug.Log("CompleteUpdate.Status 2: " + startUpdateRequest.Status);                        
                        updateStatusLog.text = $"{startUpdateRequest.Status}.";
                    }

                    updateStatusLog.text = $"{startUpdateRequest.Status}";

                    return (int)startUpdateRequest.Status;  // 0 ~ 6
                }
                else if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.DeveloperTriggeredUpdateInProgress)
                {
                    updateStatusLog.text = "Update In Progress..";

                    downloadProgressbar.gameObject.SetActive(true);

                    var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();

                    var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfoResult, appUpdateOptions);

                    while (!startUpdateRequest.IsDone)
                    {
                        await UniTask.Yield();
                        Debug.Log(appUpdateInfoResult.ToString());
                        
                        if (startUpdateRequest.Status == AppUpdateStatus.Downloading)
                        {
                            updateStatusLog.text = $"Downloading ... {Mathf.Floor(startUpdateRequest.DownloadProgress * 100) }%";
                            downloadProgressbar.value = startUpdateRequest.DownloadProgress;
                        }
                        else if (startUpdateRequest.Status == AppUpdateStatus.Downloaded)
                        {
                            downloadProgressbar.gameObject.SetActive(false);
                        }
                    }

                    var result = appUpdateManager.CompleteUpdate();

                    while (!result.IsDone)
                    {
                        await UniTask.Yield();
                        Debug.Log("CompleteUpdate.Status 2: " + startUpdateRequest.Status);
                        updateStatusLog.text = $"{startUpdateRequest.Status}.";
                    }

                    updateStatusLog.text = $"{startUpdateRequest.Status}";

                    return (int)startUpdateRequest.Status;  // 0 ~ 6

                    //return (int)UpdateAvailability.DeveloperTriggeredUpdateInProgress;  // 3
                }
                else if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateNotAvailable)
                {
                    Debug.Log("Update Not Available.");
                    updateStatusLog.text = "Update Not Available.";
                    return (int)UpdateAvailability.UpdateNotAvailable;  // 1
                }
                else
                {
                    Debug.Log("UpdateAvailability Unknown.");
                    updateStatusLog.text = "UpdateAvailability Unknown.";
                    return (int)UpdateAvailability.Unknown; // 0
                }
            }
            else
            {
                updateStatusLog.text = $"{appUpdateInfoOperation.Error}";
                return -1;
            }
        }
        catch (Exception e) 
        { 
            Debug.Log(e.Message);
            updateStatusLog.text = $"{e.Message}";
            return -2; 
        }
    }

    // 변환 해주는 함수
    public string FormatBytes(ulong bytes)
    {
        const int scale = 1024;
        string[] orders = new string[] { "GB", "MB", "KB", "Bytes" };
        ulong max = (ulong)Math.Pow(scale, orders.Length - 1);

        foreach (string order in orders)
        {
            if (bytes > max)
                return string.Format("{0:##.##} {1}", decimal.Divide(bytes, max), order);

            max /= scale;
        }
        return "0 Bytes";
    }

    public string AppUpdateStatusString(AppUpdateStatus appUpdateStatus)
    {
        switch (appUpdateStatus)
        {
            case Google.Play.AppUpdate.AppUpdateStatus.Unknown:
                break;
            case Google.Play.AppUpdate.AppUpdateStatus.Pending:
                break;
            case Google.Play.AppUpdate.AppUpdateStatus.Downloading:
                break;
            case Google.Play.AppUpdate.AppUpdateStatus.Downloaded:
                break;
            case Google.Play.AppUpdate.AppUpdateStatus.Installing:
                break;
            case Google.Play.AppUpdate.AppUpdateStatus.Installed:
                break;
            case Google.Play.AppUpdate.AppUpdateStatus.Failed:
                break;
            case Google.Play.AppUpdate.AppUpdateStatus.Canceled:
                break;
            default:
                break;
        }

        return "";
    }

    public string AppUpdateErrorCodeString(AppUpdateErrorCode appUpdateErrorCode)
    {
        string result = "";

        switch (appUpdateErrorCode)
        {
            case Google.Play.AppUpdate.AppUpdateErrorCode.NoError:
                result = "AppUpdateErrorCode.NoError";
                break;
            case Google.Play.AppUpdate.AppUpdateErrorCode.NoErrorPartiallyAllowed:
                result = "AppUpdateErrorCode.NoErrorPartiallyAllowed";
                break;
            case Google.Play.AppUpdate.AppUpdateErrorCode.ErrorUnknown:
                result = "AppUpdateErrorCode.ErrorUnknown";
                break;
            case Google.Play.AppUpdate.AppUpdateErrorCode.ErrorApiNotAvailable:
                result = "AppUpdateErrorCode.ErrorApiNotAvailable";
                break;
            case Google.Play.AppUpdate.AppUpdateErrorCode.ErrorInvalidRequest:
                result = "AppUpdateErrorCode.ErrorInvalidRequest";
                break;
            case Google.Play.AppUpdate.AppUpdateErrorCode.ErrorUpdateUnavailable:
                result = "AppUpdateErrorCode.ErrorUpdateUnavailable";
                break;
            case Google.Play.AppUpdate.AppUpdateErrorCode.ErrorUpdateNotAllowed:
                result = "AppUpdateErrorCode.ErrorUpdateNotAllowed";
                break;
            case Google.Play.AppUpdate.AppUpdateErrorCode.ErrorDownloadNotPresent:
                result = "AppUpdateErrorCode.ErrorDownloadNotPresent";
                break;
            case Google.Play.AppUpdate.AppUpdateErrorCode.ErrorUpdateInProgress:
                result = "AppUpdateErrorCode.ErrorUpdateInProgress";
                break;
            case Google.Play.AppUpdate.AppUpdateErrorCode.ErrorInternalError:
                result = "AppUpdateErrorCode.ErrorInternalError";
                break;
            case Google.Play.AppUpdate.AppUpdateErrorCode.ErrorUserCanceled:
                result = "AppUpdateErrorCode.ErrorUserCanceled";
                break;
            case Google.Play.AppUpdate.AppUpdateErrorCode.ErrorUpdateFailed:
                result = "AppUpdateErrorCode.ErrorUpdateFailed";
                break;
            case Google.Play.AppUpdate.AppUpdateErrorCode.ErrorPlayStoreNotFound:
                result = "AppUpdateErrorCode.ErrorPlayStoreNotFound";
                break;
            case Google.Play.AppUpdate.AppUpdateErrorCode.ErrorAppNotOwned:
                result = "AppUpdateErrorCode.ErrorAppNotOwned";
                break;
            default:
                break;
        }

        return result;
    }

    //public void LoginSceneLoad()
    //{
    //    StartCoroutine(_LoginSceneLoad());        
    //}
    
    //IEnumerator _LoginSceneLoad()
    //{
    //    bool isfadeCompleted = false;

    //    Tween twBlink = DOTween.Sequence()
    //        .SetAutoKill(false)
    //        .OnStart(() =>
    //        {
    //            isfadeCompleted = false;
    //            Debug.Log("OnStart");
    //        })
    //        .Append(audioSource.DOFade(0f, fadeTime))            
    //        .OnComplete(() =>
    //        {
    //            isfadeCompleted = true;
    //            Debug.Log("OnComplete");
    //        });

    //    yield return new WaitUntil(() => isfadeCompleted);

    //    twBlink.Pause();
    //    twBlink.Kill();

    //    SceneManager.LoadScene("Login");
    //}
}