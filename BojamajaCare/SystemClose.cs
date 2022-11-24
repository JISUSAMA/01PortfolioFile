using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemClose : MonoBehaviour
{
    public GameObject ClosePopup;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            ClosePopup.SetActive(true);
        }
    }
    public void SystemClose_YseBtnClickOn()
    {
        Application.Quit();
    }
    public void SystemCloseNoBtnClickOn()
    {
        Time.timeScale = 1;
        ClosePopup.SetActive(false);
    }
}
