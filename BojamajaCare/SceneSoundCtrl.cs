using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSoundCtrl : MonoBehaviour
{
    public static SceneSoundCtrl instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }


    void Start()
    {
        StartCoroutine(SoundManagerInit());
    }

    IEnumerator SoundManagerInit()
    {
        yield return new WaitForSeconds(0.5f);
    }


    public void ClickSound()
    {
        AppSoundManager.Instance.ClickSound();
    }

    public void ShootSound()
    {
        AppSoundManager.Instance.ShootSound();
    }

    public void WindowTouchSound()
    {
        Debug.Log("???????");
        AppSoundManager.Instance.WindowTouchSound();
    }

    public void KeyboardTouchSound()
    {
        AppSoundManager.Instance.KeyboardTouchSound();
    }


    public void MainBGM_Sound()
    {
        AppSoundManager.Instance.MainBGM_Sound();
    }

    public void MainBGM_SoundPuase()
    {
        AppSoundManager.Instance.MainBGM_SoundPuase();
    }
}
