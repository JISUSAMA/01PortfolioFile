using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WattingZoon_UIManager : MonoBehaviour
{
    bool watingForSec = false;
    float sec;
    public Text TimerText;
    private void Start()
    {
        Waiting_SecTime();
    }
    public void Waiting_SecTime()
    {
        sec = 5f;
        watingForSec = true;
        StartCoroutine("_Waiting_SecTime");
        Debug.Log("뭐시여 들어오는 것이여???????????????????????????");

    }
    IEnumerator _Waiting_SecTime()
    {
        while (sec>0)
        {
            Debug.Log("것이여???????????????????????????");
            sec -= Time.deltaTime;
            TimerText.text = sec.ToString("N0");
            yield return null;
        }
        TimerText.text = "Start!";
        yield return new WaitForSeconds(1);
        Touch_GameManager.instance.Game_LoadScene();

    }
}
