using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemAppManager : MonoBehaviour
{
    public GameObject NoticePopup;
    void Update()
    {
#if UNITY_ANDROID

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            NoticePopup.SetActive(true);
        }

    }

#endif
    public void OnClick_Exit_Yes()
    {
        Application.Quit();
    }
}

