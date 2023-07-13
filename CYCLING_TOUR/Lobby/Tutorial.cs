using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Text tuto_Text;  //튜토리얼 설명
    public Image tuto_Img;  //튜토리얼 이미지

    public Sprite[] tutoReset_Sprite; //젤첫 이미지
    public Sprite[] tuto_SpriteKR;    //변경될 튜토리얼 사진 한국어
    public Sprite[] tuto_SpriteEN;    //변경될 튜토리얼 사진 영어

    public GameObject tuto_PopupStart;   //튜토리얼 시작 팝업
    public GameObject tuto_PopupEnd;    //튜토리얼 끝나는 팝업
    public GameObject skipButton;   //스킵버튼

    public GameObject topButtonGroup;   //상단 버튼 그룹

    int nextNumber; //튜토리얼 팝업 설명 클릭수
    int clickNumber;    //클릭한 수

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Lobby"))
        {
            if (PlayerPrefs.GetString("AT_LobbyTutorial").Equals("Start"))
            {
                tuto_PopupStart.SetActive(true);
                skipButton.SetActive(true);
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Quest"))
        {
            if (PlayerPrefs.GetString("AT_QuestTutorial").Equals("Start"))
            {
                tuto_PopupStart.SetActive(true);
                skipButton.SetActive(true);
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("AsiaMap 3"))
        {
            if (PlayerPrefs.GetString("AT_InGameTutorial").Equals("Start") ||
                PlayerPrefs.GetString("AT_InGameTutorial").Equals(""))
            {
                tuto_PopupStart.SetActive(true);
                skipButton.SetActive(true);
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("MapChoice"))
        {
            if (PlayerPrefs.GetString("AT_MapChoiceTutorial").Equals("Start"))
            {
                tuto_PopupStart.SetActive(true);
                skipButton.SetActive(true);
            }
        }
    }

    public void LobbyTutorialReset()
    {
        nextNumber = 0;
        clickNumber = 0;

        if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.English) {
            tuto_Img.sprite = tutoReset_Sprite[0];
            tuto_Text.text = "<color=#ff0000>CYCLING TOUR</color> is a game for visiting various places in your vicinity\n" +
                            "while exercising using the fit-tag sensor.\n" +
                            "Try using indoor cycles to engage in the game. ";
        } else if(PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.Korean) {
            //튜토리얼 초기화
            tuto_Img.sprite = tutoReset_Sprite[1];
            tuto_Text.text = "<color=#ff0000>CYCLING TOUR</color>는 핏태그 센서를 이용하여" + "\n" +
                "운동을 하며 지역 곳곳을 투어 할 수 있는 게임입니다." + "\n" +
                "실내 사이클을 이용하여 게임을 즐겨보세요.";
        }
    }


    public void LobbyTutorialNextButtonOn()
    {
        nextNumber += 1;
        Debug.Log("넘버 : " + nextNumber);
        if(nextNumber.Equals(1))
        {
            if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.English) {
                tuto_Text.text = "First, let’s examine the lobby.\n" +
                "Go over the tutorial for each feature\n" +
                "and tap the screen to move to the next step.";
            } else if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.Korean) {
                tuto_Text.text = "먼저 로비를 살펴보도록 하겠습니다." + "\n" +
                "기능별 튜토리얼을 확인하고" + "\n" +
                "화면을 터치하여 다음 내용을 진행하세요.";
            }
        }
        else if(nextNumber.Equals(2))
        {
            topButtonGroup.SetActive(false);    //상단버튼 그룹 비활성화
            tuto_PopupStart.SetActive(false);    //튜토리얼 팝업 비활성화
            tuto_Img.gameObject.SetActive(true);    //튜토리얼 이미지 활성화

            if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.English) {
                tuto_Img.sprite = tutoReset_Sprite[0];
            } else if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.Korean) {
                tuto_Img.sprite = tutoReset_Sprite[1];
            }            
        }
    }
    public void LobbyTutorial()
    {
        clickNumber += 1;

        if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.English) {
            if (clickNumber.Equals(1))
                tuto_Img.sprite = tuto_SpriteEN[0];
            else if (clickNumber.Equals(2))
                tuto_Img.sprite = tuto_SpriteEN[1];
            else if (clickNumber.Equals(3))
                tuto_Img.sprite = tuto_SpriteEN[2];
            else if (clickNumber.Equals(4))
                tuto_Img.sprite = tuto_SpriteEN[3];
            else if (clickNumber.Equals(5))
                tuto_Img.sprite = tuto_SpriteEN[4];
            else if (clickNumber.Equals(6))
                tuto_Img.sprite = tuto_SpriteEN[5];
            else if (clickNumber.Equals(7))
                tuto_Img.sprite = tuto_SpriteEN[6];
            else if (clickNumber.Equals(8))
                tuto_Img.sprite = tuto_SpriteEN[7];
            else if (clickNumber.Equals(9))
                tuto_Img.sprite = tuto_SpriteEN[8];
            else if (clickNumber.Equals(10))
                tuto_Img.sprite = tuto_SpriteEN[9];
            else if (clickNumber.Equals(11)) {
                tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
                skipButton.SetActive(false);    //스킵버튼 비활성화
                tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
                topButtonGroup.SetActive(true);    //상단버튼 그룹 활성화
                PlayerPrefs.SetString("AT_LobbyTutorial", "End");
            }
        } else if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.Korean) {
            if (clickNumber.Equals(1))
                tuto_Img.sprite = tuto_SpriteKR[0];
            else if (clickNumber.Equals(2))
                tuto_Img.sprite = tuto_SpriteKR[1];
            else if (clickNumber.Equals(3))
                tuto_Img.sprite = tuto_SpriteKR[2];
            else if (clickNumber.Equals(4))
                tuto_Img.sprite = tuto_SpriteKR[3];
            else if (clickNumber.Equals(5))
                tuto_Img.sprite = tuto_SpriteKR[4];
            else if (clickNumber.Equals(6))
                tuto_Img.sprite = tuto_SpriteKR[5];
            else if (clickNumber.Equals(7))
                tuto_Img.sprite = tuto_SpriteKR[6];
            else if (clickNumber.Equals(8))
                tuto_Img.sprite = tuto_SpriteKR[7];
            else if (clickNumber.Equals(9))
                tuto_Img.sprite = tuto_SpriteKR[8];
            else if (clickNumber.Equals(10))
                tuto_Img.sprite = tuto_SpriteKR[9];
            else if (clickNumber.Equals(11)) {
                tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
                skipButton.SetActive(false);    //스킵버튼 비활성화
                tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
                topButtonGroup.SetActive(true);    //상단버튼 그룹 활성화
                PlayerPrefs.SetString("AT_LobbyTutorial", "End");
            }
        }


    }
    public void SkipButtonOn()
    {

        if (tuto_PopupStart.activeSelf.Equals(true))
            tuto_PopupStart.SetActive(false);
        else if (tuto_Img.gameObject.activeSelf.Equals(true))
            tuto_Img.gameObject.SetActive(false);

        skipButton.SetActive(false);    //스킵버튼 비활성화

        if(SceneManager.GetActiveScene().name.Equals("Lobby"))
        {
            PlayerPrefs.SetString("AT_LobbyTutorial", "End");
            topButtonGroup.SetActive(true);    //상단버튼 그룹 활성화
        }
        else if(SceneManager.GetActiveScene().name.Equals("Quest"))
            PlayerPrefs.SetString("AT_QuestTutorial", "End");
        else if (SceneManager.GetActiveScene().name.Equals("AsiaMap 3"))
        {
            if (AsiaMap_UIManager.instance.gameStartState.Equals(true))
            {
                PlayerPrefs.SetString("AT_InGameTutorial", "End");
                GamePlayStart();
            }
            else
            {
                PlayerPrefs.SetString("AT_InGameTutorial", "End");
                AsiaMap_UIManager.instance.GameStart();
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("MapChoice"))
            PlayerPrefs.SetString("AT_MapChoiceTutorial", "End");
    }


    public void QuestTutorialNextButtonOn()
    {
        clickNumber = 0;

        if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.English) {
            tuto_Img.sprite = tutoReset_Sprite[0];
        } else if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.Korean) {
            tuto_Img.sprite = tutoReset_Sprite[1];
        }

        tuto_PopupStart.SetActive(false);    //튜토리얼 팝업 비활성화
        tuto_Img.gameObject.SetActive(true);    //튜토리얼 이미지 활성화
    }

    public void QuestTutorial()
    {
        clickNumber += 1;

        if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.English) {
            if (clickNumber.Equals(1))
                tuto_Img.sprite = tuto_SpriteEN[0];
            else if (clickNumber.Equals(2))
                tuto_Img.sprite = tuto_SpriteEN[1];
            else if (clickNumber.Equals(3))
                tuto_Img.sprite = tuto_SpriteEN[2];
            else if (clickNumber.Equals(4)) {
                tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
                skipButton.SetActive(false);    //스킵버튼 비활성화
                tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
                PlayerPrefs.SetString("AT_QuestTutorial", "End");
            }
        } else if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.Korean) {
            if (clickNumber.Equals(1))
                tuto_Img.sprite = tuto_SpriteKR[0];
            else if (clickNumber.Equals(2))
                tuto_Img.sprite = tuto_SpriteKR[1];
            else if (clickNumber.Equals(3))
                tuto_Img.sprite = tuto_SpriteKR[2];
            else if (clickNumber.Equals(4)) {
                tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
                skipButton.SetActive(false);    //스킵버튼 비활성화
                tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
                PlayerPrefs.SetString("AT_QuestTutorial", "End");
            }
        }
    }



    public void InGameTutorialNextButtonOn()
    {
        clickNumber = 0;

        if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.English) {
            tuto_Img.sprite = tutoReset_Sprite[0];
        } else if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.Korean) {
            tuto_Img.sprite = tutoReset_Sprite[1];
        }

        tuto_PopupStart.SetActive(false);    //튜토리얼 팝업 비활성화
        tuto_Img.gameObject.SetActive(true);    //튜토리얼 이미지 활성화
    }

    public void InGameTutorial()
    {
        clickNumber += 1;

        if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.English) {
            if (clickNumber.Equals(1))
                tuto_Img.sprite = tuto_SpriteEN[0];
            else if (clickNumber.Equals(2))
                tuto_Img.sprite = tuto_SpriteEN[1];
            else if (clickNumber.Equals(3))
                tuto_Img.sprite = tuto_SpriteEN[2];
            else if (clickNumber.Equals(4))
                tuto_Img.sprite = tuto_SpriteEN[3];
            else if (clickNumber.Equals(5))
                tuto_Img.sprite = tuto_SpriteEN[4];
            else if (clickNumber.Equals(6))
                tuto_Img.sprite = tuto_SpriteEN[5];
            else if (clickNumber.Equals(7))
                tuto_Img.sprite = tuto_SpriteEN[6];
            else if (clickNumber.Equals(8))
                tuto_Img.sprite = tuto_SpriteEN[7];
            else if (clickNumber.Equals(9))
                tuto_Img.sprite = tuto_SpriteEN[8];
            else if (clickNumber.Equals(10))
                tuto_Img.sprite = tuto_SpriteEN[9];
            else if (clickNumber.Equals(11))
                tuto_Img.sprite = tuto_SpriteEN[10];
            else if (clickNumber.Equals(12))
                tuto_Img.sprite = tuto_SpriteEN[11];
            else if (clickNumber.Equals(13)) {
                tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
                skipButton.SetActive(false);    //스킵버튼 비활성화
                tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
                PlayerPrefs.SetString("AT_InGameTutorial", "End");
            }
        } else if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.Korean) {
            if (clickNumber.Equals(1))
                tuto_Img.sprite = tuto_SpriteKR[0];
            else if (clickNumber.Equals(2))
                tuto_Img.sprite = tuto_SpriteKR[1];
            else if (clickNumber.Equals(3))
                tuto_Img.sprite = tuto_SpriteKR[2];
            else if (clickNumber.Equals(4))
                tuto_Img.sprite = tuto_SpriteKR[3];
            else if (clickNumber.Equals(5))
                tuto_Img.sprite = tuto_SpriteKR[4];
            else if (clickNumber.Equals(6))
                tuto_Img.sprite = tuto_SpriteKR[5];
            else if (clickNumber.Equals(7))
                tuto_Img.sprite = tuto_SpriteKR[6];
            else if (clickNumber.Equals(8))
                tuto_Img.sprite = tuto_SpriteKR[7];
            else if (clickNumber.Equals(9))
                tuto_Img.sprite = tuto_SpriteKR[8];
            else if (clickNumber.Equals(10))
                tuto_Img.sprite = tuto_SpriteKR[9];
            else if (clickNumber.Equals(11))
                tuto_Img.sprite = tuto_SpriteKR[10];
            else if (clickNumber.Equals(12))
                tuto_Img.sprite = tuto_SpriteKR[11];
            else if (clickNumber.Equals(13)) {
                tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
                skipButton.SetActive(false);    //스킵버튼 비활성화
                tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
                PlayerPrefs.SetString("AT_InGameTutorial", "End");
            }
        }
    }



    public void MapChoiceTutorialNextButton()
    {
        clickNumber = 0;
        if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.English) {
            tuto_Img.sprite = tutoReset_Sprite[0];
        } else if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.Korean) {
            tuto_Img.sprite = tutoReset_Sprite[1];
        }
        tuto_PopupStart.SetActive(false);    //튜토리얼 팝업 비활성화
        tuto_Img.gameObject.SetActive(true);    //튜토리얼 이미지 활성화
    }

    public void MapChoiceTutorial()
    {
        clickNumber += 1;

        if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.English) {
            if (clickNumber.Equals(1))
                tuto_Img.sprite = tuto_SpriteEN[0];
            else if (clickNumber.Equals(2))
                tuto_Img.sprite = tuto_SpriteEN[1];
            else if (clickNumber.Equals(3))
                tuto_Img.sprite = tuto_SpriteEN[2];
            else if (clickNumber.Equals(4)) {
                tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
                skipButton.SetActive(false);    //스킵버튼 비활성화
                tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
                PlayerPrefs.SetString("AT_MapChoiceTutorial", "End");
            }
        } else if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.Korean) {
            if (clickNumber.Equals(1))
                tuto_Img.sprite = tuto_SpriteKR[0];
            else if (clickNumber.Equals(2))
                tuto_Img.sprite = tuto_SpriteKR[1];
            else if (clickNumber.Equals(3))
                tuto_Img.sprite = tuto_SpriteKR[2];
            else if (clickNumber.Equals(4)) {
                tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
                skipButton.SetActive(false);    //스킵버튼 비활성화
                tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
                PlayerPrefs.SetString("AT_MapChoiceTutorial", "End");
            }
        }
    }

    public void GamePlayStart()
    {
        if(AsiaMap_DataManager.instance.tuto_btnOn.Equals(true))
        {
            //Debug.Log("여긴가요");
            AsiaMap_DataManager.instance.tuto_btnOn = false;
            PlayerPrefs.SetString("AT_InGameTutorial", "End");
            AsiaMap_DataManager.instance.GameTimePlay();    //게임타이머 시작
        }
        else
        {
            //Debug.Log("아닌가요 여긴가요");
            PlayerPrefs.SetString("AT_InGameTutorial", "End");
            AsiaMap_UIManager.instance.GameStart(); //게임 처음 시작
        }
    }

}
