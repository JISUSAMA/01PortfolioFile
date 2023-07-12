using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TodayModeChoice_UIManager : MonoBehaviour
{
    [SerializeField] Text CoinText;
    [SerializeField] GameObject ChooseMode_Panel;
    [SerializeField] Text ChooseMode_Text;
    [SerializeField] GameObject BlurCamera_ob;
    [SerializeField] Button EasyModeBtn;
    [SerializeField] Button ProModeBtn;
    string chooseMode_name;
    public void Awake()
    {
        SoundManager.Instance.PlayBGM("1_MainTheme");
        CoinText.text = PlayerPrefs.GetInt("Player_Coin").ToString();
        chooseMode_name = "";

        //남은 거리가 5km보다 작을 경우,
        if (PlayerPrefs.GetFloat("Moon_Distance") < 10f)
        {
            ProModeBtn.interactable = false;
        }

    }

    //모드를 선택했을때, 바로 게임이 진행되는것이 아니라 정말로 해당 모드를 플레이할것 인지 물어봄
    public void Open_chooseMode_Panel(GameObject name)
    {
        ChooseMode_Panel.SetActive(true);
        BlurCamera_ob.SetActive(true);
        string mode_name = name.name;
        //이지 모드를 선택 했을 경우, 
        if (mode_name.Equals("EasyModeButton"))
        {
            ChooseMode_Text.text = "<color=#7FCDFE>Easy Mode</color> 를 선택 하셨습니다.";
            chooseMode_name = mode_name.ToString();
        }
        //프로 모드를 선택 했을 경우,
        else if (mode_name.Equals("ProModeButton"))
        {
            ChooseMode_Text.text = "<color=#7FCDFE>Pro Mode</color> 를 선택 하셨습니다.";
            chooseMode_name = mode_name.ToString();
        }
        Debug.Log("chooseMode_name" + chooseMode_name);
        Debug.Log("ChooseMode_Text" + ChooseMode_Text);
    }
    //네 
    public void OnClick_YesBtn()
    {
        if (chooseMode_name.Equals("EasyModeButton"))
        {
            EasyModeButtonOn();
        }
        else if (chooseMode_name.Equals("ProModeButton"))
        {
            ProModeButtonOn();
        }
    }
    //아니오
    public void OnClick_NoBtn()
    {
        chooseMode_name = "";
        ChooseMode_Panel.SetActive(false);
        BlurCamera_ob.SetActive(false);
    }
    //이지모드 선택
    public void EasyModeButtonOn()
    {
        PlayerPrefs.SetString("Mode", "Easy");
     //   Loading_SceneManager.LoadScene("Loading");
    }
    //프로모드 선택
    public void ProModeButtonOn()
    {
        PlayerPrefs.SetString("Mode", "Pro");
   //     Loading_SceneManager.LoadScene("Loading");
    }

    public void BackButtonLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

}