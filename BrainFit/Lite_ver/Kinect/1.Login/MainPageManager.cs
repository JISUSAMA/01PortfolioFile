using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPageManager : MonoBehaviour
{

   
    //public Text text;
    public Animator MotionAni;
    public Button motionIconBtn; 

    bool motion_open_b = true;

    public Text debug_file_;
    private void OnEnable()
    {
        Debug.Log("뭐지????  " + WebCamTexture.devices[0].name);
       // debug_file_.text= Application.persistentDataPath;
        AppSoundManager.Instance.PlayBGM("MainBGM1");
        MotionANI_Ctrl();
    }
    //게임 시작 버튼
    public void GameStartButtonClick()
    {
        PlayerPrefs.SetString("CARE_PlayMode", "GuestMode");
        SceneManager.LoadScene("8.ChooseGame");
    }

    public void SystemCloseBtnClickOn()
    {
        Application.Quit();
    }

    public void MotionANI_Ctrl()
    {
        StopCoroutine(_MotionAniCount());
        StartCoroutine(_MotionAniCount());
    }
    IEnumerator _MotionAniCount()
    {
        MotionAni.SetTrigger("Open");
        yield return new WaitForSeconds(5f);
        MotionAni.SetTrigger("Close");
        yield return null;
    }


}
