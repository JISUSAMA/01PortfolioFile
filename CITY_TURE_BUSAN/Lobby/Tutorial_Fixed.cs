using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial_Fixed : MonoBehaviour
{
    public Text tuto_Text;  //튜토리얼 설명
    public Image tuto_Img;  //튜토리얼 이미지

    // lobby : page 9, text 3
    [Header("Tutorial Localized Sprite & String Setting")]
    public LocalizedSprite[] tutorialPage;
    public LocalizedString[] tutorialText;

    //public Sprite tutoReset_Sprite; //젤첫 이미지
    //public Sprite tutoReset_Sprite_EN; //젤첫 이미지
    //public Sprite[] tuto_Sprite;    //변경될 튜토리얼 사진
    //public Sprite[] tuto_Sprite_EN;    //변경될 튜토리얼 사진

    public GameObject tuto_PopupStart;   //튜토리얼 시작 팝업
    public GameObject tuto_PopupEnd;    //튜토리얼 끝나는 팝업
    public GameObject skipButton;   //스킵버튼

    public GameObject topButtonGroup;   //상단 버튼 그룹

    int nextNumber; //튜토리얼 팝업 설명 클릭수
    int clickNumber;    //클릭한 수

    private void Awake()
    {
        LanguageSelectorManager.instance.OnCompleteGetSprite += Locale_OnCompleteGetSprite;
        LanguageSelectorManager.instance.OnCompleteGetString += Locale_OnCompleteGetString;
    }
    private void OnDestroy()
    {
        LanguageSelectorManager.instance.OnCompleteGetSprite -= Locale_OnCompleteGetSprite;
        LanguageSelectorManager.instance.OnCompleteGetString -= Locale_OnCompleteGetString;
    }

    private void Locale_OnCompleteGetString(string text)
    {
        Debug.Log("Tutorial OnCompleteGetString");
        tuto_Text.text = text;
    }

    private void Locale_OnCompleteGetSprite(Sprite sprite)
    {
        Debug.Log("Tutorial OnCompleteGetSprite");
        tuto_Img.sprite = sprite;
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Lobby"))
        {
            if (PlayerPrefs.GetString("Busan_LobbyTutorial").Equals("Start") ||
                PlayerPrefs.GetString("Busan_LobbyTutorial").Equals(""))
            {
                tuto_PopupStart.SetActive(true);
                skipButton.SetActive(true);
                LobbyTutorialReset();

            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Quest"))
        {
            if (PlayerPrefs.GetString("Busan_QuestTutorial").Equals("Start") ||
                PlayerPrefs.GetString("Busan_QuestTutorial").Equals(""))
            {
                tuto_PopupStart.SetActive(true);
                skipButton.SetActive(true);
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BusanMapMorning"))
        {
            if (PlayerPrefs.GetString("Busan_InGameTutorial").Equals("Start") ||
                PlayerPrefs.GetString("Busan_InGameTutorial").Equals(""))
            {
                tuto_PopupStart.SetActive(true);
                skipButton.SetActive(true);

            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BusanMapNight"))
        {
            if (PlayerPrefs.GetString("Busan_InGameTutorial").Equals("Start") ||
                PlayerPrefs.GetString("Busan_InGameTutorial").Equals(""))
            {
                tuto_PopupStart.SetActive(true);
                skipButton.SetActive(true);

            }

        }
        else if (SceneManager.GetActiveScene().name.Equals("BusanGreenLineMorning"))
        {
            if (PlayerPrefs.GetString("Busan_InGameTutorial").Equals("Start") ||
                PlayerPrefs.GetString("Busan_InGameTutorial").Equals(""))
            {
                tuto_PopupStart.SetActive(true);
                skipButton.SetActive(true);

            }

        }
        else if (SceneManager.GetActiveScene().name.Equals("BusanGreenLineNight"))
        {
            if (PlayerPrefs.GetString("Busan_InGameTutorial").Equals("Start") ||
                PlayerPrefs.GetString("Busan_InGameTutorial").Equals(""))
            {
                tuto_PopupStart.SetActive(true);
                skipButton.SetActive(true);

            }

        }
        else if (SceneManager.GetActiveScene().name.Equals("MapChoice"))
        {
            if (PlayerPrefs.GetString("Busan_MapChoiceTutorial").Equals("Start") ||
                PlayerPrefs.GetString("Busan_MapChoiceTutorial").Equals(""))
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

        int text_lobby_tutorial_page1 = 0;
        int sprite_lobby_tutorial_page1_sprite = 0;

        LanguageSelectorManager.instance.GetSpriteLocale(tutorialPage[sprite_lobby_tutorial_page1_sprite]).Forget();
        LanguageSelectorManager.instance.GetStringLocale(tutorialText[text_lobby_tutorial_page1]).Forget();

    }


    public void LobbyTutorialNextButtonOn()
    {
        nextNumber += 1;

        if (nextNumber.Equals(1))
        {
            int text_lobby_tutorial_page2 = 1;
            LanguageSelectorManager.instance.GetStringLocale(tutorialText[text_lobby_tutorial_page2]).Forget();

        }
        else if (nextNumber.Equals(2))
        {
            topButtonGroup.SetActive(false);    //상단버튼 그룹 비활성화
            tuto_PopupStart.SetActive(false);    //튜토리얼 팝업 비활성화
            tuto_Img.gameObject.SetActive(true);    //튜토리얼 이미지 활성화

            int sprite_lobby_tutorial_page1 = 0;
            LanguageSelectorManager.instance.GetSpriteLocale(tutorialPage[sprite_lobby_tutorial_page1]).Forget();
        }
    }
    public void LobbyTutorial()
    {
        clickNumber += 1;

        if (clickNumber == 11)
        {
            skipButton.SetActive(false);    //스킵버튼 비활성화
            topButtonGroup.SetActive(true);    //상단버튼 그룹 활성화
            tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화            
            tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화            
            PlayerPrefs.SetString("Busan_LobbyTutorial", "End");
        }
        else
        {
            // LobbyTutorialNextButtonOn 함수에서 이미 첫번째 튜토리얼 이미지 부름. 그래서 그대로 1부터 시작
            LanguageSelectorManager.instance.GetSpriteLocale(tutorialPage[clickNumber]).Forget();
        }

    }
    public void SkipButtonOn()
    {
        if (tuto_PopupStart.activeSelf.Equals(true))
            tuto_PopupStart.SetActive(false);
        else if (tuto_Img.gameObject.activeSelf.Equals(true))
            tuto_Img.gameObject.SetActive(false);

        skipButton.SetActive(false);    //스킵버튼 비활성화

        if (SceneManager.GetActiveScene().name.Equals("Lobby"))
        {
            PlayerPrefs.SetString("Busan_LobbyTutorial", "End");
            topButtonGroup.SetActive(true);    //상단버튼 그룹 활성화
        }
        else if (SceneManager.GetActiveScene().name.Equals("Quest"))
            PlayerPrefs.SetString("Busan_QuestTutorial", "End");

        else if (SceneManager.GetActiveScene().name.Equals("BusanMapMorning") || SceneManager.GetActiveScene().name.Equals("BusanMapNight")
            || SceneManager.GetActiveScene().name.Equals("BusanGreenLineMorning") || SceneManager.GetActiveScene().name.Equals("BusanGreenLineNight"))
        {
            if (Busan_UIManager.instance.gameStartState.Equals(true))
            {
                PlayerPrefs.SetString("Busan_InGameTutorial", "End");
                GamePlayStart();
            }
            else
            {
                PlayerPrefs.SetString("Busan_InGameTutorial", "End");
                Busan_UIManager.instance.GameStart();
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("MapChoice"))
            PlayerPrefs.SetString("Busan_MapChoiceTutorial", "End");
    }

    public void QuestTutorialNextButtonOn()
    {
        clickNumber = 0;
        tuto_PopupStart.SetActive(false);    //튜토리얼 팝업 비활성화
        tuto_Img.gameObject.SetActive(true);    //튜토리얼 이미지 활성화

        LanguageSelectorManager.instance.GetSpriteLocale(tutorialPage[clickNumber]).Forget();
    }

    public void QuestTutorial()
    {
        clickNumber += 1;

        if (clickNumber == 4)
        {
            tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
            skipButton.SetActive(false);    //스킵버튼 비활성화
            tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
            PlayerPrefs.SetString("Busan_QuestTutorial", "End");
        }
        else
        {
            LanguageSelectorManager.instance.GetSpriteLocale(tutorialPage[clickNumber]).Forget();
        }

    }



    public void InGameTutorialNextButtonOn()
    {
    //    Debug.Log("IN_GAMETUTORIAL : -------------------1"+ clickNumber);
        clickNumber = 0;
        tuto_PopupStart.SetActive(false);    //튜토리얼 팝업 비활성화
        tuto_Img.gameObject.SetActive(true);    //튜토리얼 이미지 활성화

        LanguageSelectorManager.instance.GetSpriteLocale(tutorialPage[0]).Forget();
    }

    public void InGameTutorial()
    {
      //  Debug.Log("IN_GAMETUTORIAL : -------------------2"+ clickNumber);
        clickNumber += 1;

        if (clickNumber == 12)
        {
            tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
            skipButton.SetActive(false);    //스킵버튼 비활성화
            tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
            PlayerPrefs.SetString("Busan_InGameTutorial", "End");
        }
        else
        {
            //Debug.Log("IN_GAMETUTORIAL : -------------------3" + clickNumber);
            LanguageSelectorManager.instance.GetSpriteLocale(tutorialPage[clickNumber]).Forget();
        }

    }


    public void MapChoiceTutorialNextButton()
    {
        clickNumber = 0;

        tuto_PopupStart.SetActive(false);    //튜토리얼 팝업 비활성화
        tuto_Img.gameObject.SetActive(true);    //튜토리얼 이미지 활성화

        LanguageSelectorManager.instance.GetSpriteLocale(tutorialPage[0]).Forget();
    }

    public void MapChoiceTutorial()
    {
        clickNumber += 1;

        if (clickNumber == 4)
        {
            tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
            skipButton.SetActive(false);    //스킵버튼 비활성화
            tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
            PlayerPrefs.SetString("Busan_MapChoiceTutorial", "End");
        }
        else
        {
            LanguageSelectorManager.instance.GetSpriteLocale(tutorialPage[clickNumber]).Forget();
        }

    }

    public void GamePlayStart()
    {
        //BusanMap_GameTime.instance.PlayTime();
        //투토리얼 버튼 클릭했을 경우
        if (Busan_DataManager.instance.tuto_btnOn.Equals(true))
        {
            Busan_DataManager.instance.tuto_btnOn = false;
            PlayerPrefs.SetString("Busan_InGameTutorial", "End");
            Busan_DataManager.instance.GameTimePlay();  //게임 타이머 시작
        }
        else
        {
            PlayerPrefs.SetString("Busan_InGameTutorial", "End");
            Busan_UIManager.instance.GameStart();   //게임 처음 시작
        }
    }
}
