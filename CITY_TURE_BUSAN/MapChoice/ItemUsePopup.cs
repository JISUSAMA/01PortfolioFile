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
        if (PlayerPrefs.GetInt("Busan_CoinUpAmount") == 0)
        {
            coinToggle.isOn = false;
            coinToggle.interactable = false;
        }

        if(PlayerPrefs.GetInt("Busan_ExpUpAmount") == 0)
        {
            expToggle.isOn = false;
            expToggle.interactable = false;
        }

        if(PlayerPrefs.GetInt("Busan_SpeedUpAmount") == 0)
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

        //if (PlayerPrefs.GetInt("Busan_CoinUpAmount") == 0 && PlayerPrefs.GetInt("Busan_ExpUpAmount") == 0 && PlayerPrefs.GetInt("Busan_SpeedUpAmount") == 0)
        //{
        //    useItemButton.interactable = false;
        //}
    }

    public void NoUseButton()
    {
        PlayerPrefs.SetString("Busan_UseItemName", "");

        //다음 코스 오픈하기 위한 클리어 시간 저장, 맵 이름저장
        MapChoice_DataManager.instance.AsiaMapCourseChoiceOpenTime();

        if(PlayerPrefs.GetString("Busan_GameOnePlay").Equals("MissionOk"))
            PlayerPrefs.SetString("Busan_GameOnePlay", "MissionOk");
        else
            PlayerPrefs.SetString("Busan_GameOnePlay", "Yes");

        PlayerPrefs.SetString("Busan_CurrentMap_Name",MapChoice_DataManager.instance.CurrentChoiceMap);
        Loading_SceneManager.LoadScene("Loading");
    }

    public void YesUseButton()
    {
        Debug.Log("전 갯수 : " + PlayerPrefs.GetInt("Busan_CoinUpAmount"));
        if(coinToggle.isOn == true)
        {
            PlayerPrefs.SetString("Busan_UseItemCoin", "Coin");
            int number = PlayerPrefs.GetInt("Busan_CoinUpAmount");
            PlayerPrefs.SetInt("Busan_CoinUpAmount", number - 1);
        }
        if(expToggle.isOn == true)
        {
            PlayerPrefs.SetString("Busan_UseItemExp", "Exp");
            int number = PlayerPrefs.GetInt("Busan_ExpUpAmount");
            PlayerPrefs.SetInt("Busan_ExpUpAmount", number - 1);
        }
        if(speedToggle.isOn == true)
        {
            PlayerPrefs.SetString("Busan_UseItemSpeed", "Speed");
            int number = PlayerPrefs.GetInt("Busan_SpeedUpAmount");
            PlayerPrefs.SetInt("Busan_SpeedUpAmount", number - 1);
        }

        Debug.Log("후 갯수 : " + PlayerPrefs.GetInt("Busan_CoinUpAmount"));

        //다음 코스 오픈하기 위한 클리어 시간 저장, 맵 이름저장
        MapChoice_DataManager.instance.AsiaMapCourseChoiceOpenTime();

        if (PlayerPrefs.GetString("Busan_GameOnePlay").Equals("MissionOk"))
            PlayerPrefs.SetString("Busan_GameOnePlay", "MissionOk");
        else
            PlayerPrefs.SetString("Busan_GameOnePlay", "Yes");

        if (PlayerPrefs.GetString("Busan_ItemUse").Equals("MissionOk"))
            PlayerPrefs.SetString("Busan_ItemUse", "MissionOk");
        else
            PlayerPrefs.SetString("Busan_ItemUse", "Yes");

        ServerManager.Instance.Update_ConsumableItems();
        PlayerPrefs.SetString("Busan_CurrentMap_Name",MapChoice_DataManager.instance.CurrentChoiceMap);

        Loading_SceneManager.LoadScene("Loading");
    }
    
}
