using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFunction : MonoBehaviour
{
    public static SoundFunction Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else Instance = this;
    }
    //사운드 함수 관리
    public void Warning_Sound()
    {
        SoundManager.Instance.PlaySFX("Pop_up_SFX_04");
    }
    public void ButtonClick_Sound()
    {
        SoundManager.Instance.PlaySFX("11 ButtonTouch1");
    }
    public void CloseClick_Sound()
    {
        SoundManager.Instance.PlaySFX("11 ButtonTouch1");
    }
    public void ItemBuy_Sound()
    {
        SoundManager.Instance.PlaySFX("Buy_Item_SFX");
    }
    public void BasicTouch_Sound()
    {
        SoundManager.Instance.PlaySFX("11 ButtonTouch1");
    }
    public void ItemDescription_Sound()
    {
        SoundManager.Instance.PlaySFX("Find_Item_SFX");
    }
    public void ItemUse_StarDust()
    {
        SoundManager.Instance.PlaySFX("Use_Stardust_SFX");
    }
    public void ItemUse_Oxygen_Tankt()
    {
        SoundManager.Instance.PlaySFX("Use_Oxygen_Tank_SFX");
    }
    //미션 발견
    public void Show_Mission_Popup()
    {
        SoundManager.Instance.PlaySFX("17 StartMission");
    }
    public void Mission_End_Sound()
    {
        SoundManager.Instance.PlaySFX("Mission_EndAlarm_SFX");
    }
    //라디오 사운드
    public void Show_Radio_narration()
    {
        SoundManager.Instance.PlaySFX("glitch2");
    }
    public void WarringSound_alert()
    {
        SoundManager.Instance.PlaySFX("Touch_Screen_SFX_01");
    }
}
