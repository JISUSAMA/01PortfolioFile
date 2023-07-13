using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemUsePopup : MonoBehaviour
{
    public Toggle coinToggle;
    public Toggle expToggle;
    public Toggle speedToggle;

    public Button useItemButton;



    void Start()
    {
        ToggleInit();
    }

    void ToggleInit()
    {
        if (PlayerPrefs.GetInt("AT_CoinUpAmount") == 0)
        {
            coinToggle.isOn = false;
            coinToggle.interactable = false;
        }

        if(PlayerPrefs.GetInt("AT_ExpUpAmount") == 0)
        {
            expToggle.isOn = false;
            expToggle.interactable = false;
        }

        if(PlayerPrefs.GetInt("AT_SpeedUpAmount") == 0)
        {
            speedToggle.isOn = false;
            speedToggle.interactable = false;
        }
        useItemButton.interactable = false;

        //초기화 - 누르지 않고 선택되어 있는것을 바로 사용하기 했을 경우에
        //if(PlayerPrefs.GetInt("CoinUpAmount") != 0)
        //{
        //    PlayerPrefs.SetString("UseItemName", "Coin");
        //}
        //else if(PlayerPrefs.GetInt("CoinUpAmount") == 0 && PlayerPrefs.GetInt("ExpUpAmount") != 0)
        //{
        //    PlayerPrefs.SetString("UseItemName", "Exp");
        //}
        //else if(PlayerPrefs.GetInt("CoinUpAmount") == 0 && PlayerPrefs.GetInt("ExpUpAmount") == 0 && PlayerPrefs.GetInt("SpeedUpAmount") != 0)
        //{
        //    PlayerPrefs.SetString("UseItemName", "Speed");
        //}

        //if (PlayerPrefs.GetInt("AT_CoinUpAmount") == 0 && PlayerPrefs.GetInt("AT_ExpUpAmount") == 0 && PlayerPrefs.GetInt("AT_SpeedUpAmount") == 0)
        //{
        //    useItemButton.interactable = false;
        //}
    }

    public void NoUseButton()
    {
        PlayerPrefs.SetString("AT_UseItemName", "");

        //다음 코스 오픈하기 위한 클리어 시간 저장, 맵 이름저장
        MapChoice_DataManager.instance.AsiaMapCourseChoiceOpenTime();

        if(PlayerPrefs.GetString("AT_GameOnePlay").Equals("MissionOk"))
            PlayerPrefs.SetString("AT_GameOnePlay", "MissionOk");
        else
            PlayerPrefs.SetString("AT_GameOnePlay", "Yes");
        Loading_SceneManager.LoadScene("Loading");
    }

    public void YesUseButton()
    {
        Debug.Log("전 갯수 : " + PlayerPrefs.GetInt("AT_CoinUpAmount"));
        if(coinToggle.isOn == true)
        {
            PlayerPrefs.SetString("AT_UseItemCoin", "Coin");
            int number = PlayerPrefs.GetInt("AT_CoinUpAmount");
            PlayerPrefs.SetInt("AT_CoinUpAmount", number - 1);
        }
        if(expToggle.isOn == true)
        {
            PlayerPrefs.SetString("AT_UseItemExp", "Exp");
            int number = PlayerPrefs.GetInt("AT_ExpUpAmount");
            PlayerPrefs.SetInt("AT_ExpUpAmount", number - 1);
        }
        if(speedToggle.isOn == true)
        {
            PlayerPrefs.SetString("AT_UseItemSpeed", "Speed");
            int number = PlayerPrefs.GetInt("AT_SpeedUpAmount");
            PlayerPrefs.SetInt("AT_SpeedUpAmount", number - 1);
        }

        Debug.Log("후 갯수 : " + PlayerPrefs.GetInt("AT_CoinUpAmount"));

        //다음 코스 오픈하기 위한 클리어 시간 저장, 맵 이름저장
        MapChoice_DataManager.instance.AsiaMapCourseChoiceOpenTime();

        if (PlayerPrefs.GetString("AT_GameOnePlay").Equals("MissionOk"))
            PlayerPrefs.SetString("AT_GameOnePlay", "MissionOk");
        else
            PlayerPrefs.SetString("AT_GameOnePlay", "Yes");

        if (PlayerPrefs.GetString("AT_ItemUse").Equals("MissionOk"))
            PlayerPrefs.SetString("AT_ItemUse", "MissionOk");
        else
            PlayerPrefs.SetString("AT_ItemUse", "Yes");

        ServerManager.Instance.Update_ConsumableItems();

        Loading_SceneManager.LoadScene("Loading");
    }
    
}
