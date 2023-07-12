using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_SenesorEvent : MonoBehaviour
{

    public void OnClick_ConnectBtn()
    {
        L_ESP32BLEApp.instance.StartProcess();
        SoundFunction.Instance.ButtonClick_Sound();
    }

}
