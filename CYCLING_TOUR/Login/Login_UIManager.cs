using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login_UIManager : MonoBehaviour
{
    public GameObject gameEndPopup;
    public GameObject logo;



    private void Start()
    {
        //StartCoroutine(ShowLogo());
    }

    IEnumerator ShowLogo()
    {
        yield return new WaitForSeconds(2.5f);
        logo.SetActive(false);
    }

    private void Update()
    {
        if (Application.platform.Equals(RuntimePlatform.Android))
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                gameEndPopup.SetActive(true);
            }
        }
    }

    //게임 종료
    public void GameEndButtonOn()
    {
        Application.Quit(); //게임 종료
    }

    
}
