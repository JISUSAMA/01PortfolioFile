using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCtrl : MonoBehaviour
{
    public static SoundCtrl Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else Instance = this;

    }
    public void BGM_MainSound()
    {
        SoundManager.instance.PlayBGM("MainLobbyBGM");
    }

    public void ClickButton_Sound()
    {
        Debug.Log("��ư Ŭ��!");
        SoundManager.instance.PlaySFX_OneShot("ClickSound");
    }
    /////////////////// ���������� ���� //////////////////////
    public void BGM_Change_DeliveryMan()
    {
        SoundManager.instance.PlayBGM("ManduManBGM1");
    }

    /////////////////// Ÿ������Ŀ ���� //////////////////////
    public void BGM_Change_TowerMaker()
    {
        SoundManager.instance.PlayBGM("TopPile_BGM1");
    }
    /////////////////// ���̾��ũ ���� //////////////////////
    public void BGM_Change_WireWalking()
    {
        SoundManager.instance.PlayBGM("Lofewalking_BGM1");
    }
}
