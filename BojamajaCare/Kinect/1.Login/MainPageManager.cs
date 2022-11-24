using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class MainPageManager : MonoBehaviour
{
    public Text text;
    public Animator MotionAni;
    public Button motionIconBtn; 

    bool motion_open_b = true;

    private void Start()
    {
        Debug.Log(Path.Combine(Application.persistentDataPath, "/ScreenShots/"));
        text.text = Application.persistentDataPath + "/ScreenShots/";
        MotionANI_Ctrl();
        AppSoundManager.Instance.MainBGM_Sound();
    }

    public void LoginButtonClick()
    {
        PlayerPrefs.SetString("CARE_PlayMode", "LoginMode");
        SceneManager.LoadScene("1.Login");
    }

    public void GuestButtonClick()
    {
        PlayerPrefs.SetString("CARE_PlayMode", "GuestMode");
        SceneManager.LoadScene("8.ChooseGame");
    }
    //회원가입
    public void JoinOnButton()
    {
        SceneManager.LoadScene("3.FaceShoot");
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

    public void GymnasticsGame()
    {
        SceneManager.LoadScene("8.GymnasticsGame");
    }
}
