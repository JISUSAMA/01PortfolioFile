using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Text tuto_Text;  //튜토리얼 설명
    public Image tuto_Img;  //튜토리얼 이미지

    public Sprite tutoReset_Sprite; //젤첫 이미지
    public Sprite tutoReset_Sprite_EN; //젤첫 이미지
    public Sprite[] tuto_Sprite;    //변경될 튜토리얼 사진
    public Sprite[] tuto_Sprite_EN;    //변경될 튜토리얼 사진

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
            if (PlayerPrefs.GetString("Busan_LobbyTutorial").Equals("Start") ||
                PlayerPrefs.GetString("Busan_LobbyTutorial").Equals(""))
            {
                tuto_PopupStart.SetActive(true);
                skipButton.SetActive(true);
                LobbyTutorialReset();

                if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                    tuto_Img.sprite = tutoReset_Sprite;
                else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                    tuto_Img.sprite = tutoReset_Sprite_EN;
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Quest"))
        {
            if (PlayerPrefs.GetString("Busan_QuestTutorial").Equals("Start") ||
                PlayerPrefs.GetString("Busan_QuestTutorial").Equals(""))
            {
                tuto_PopupStart.SetActive(true);
                skipButton.SetActive(true);

                if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                    tuto_Img.sprite = tutoReset_Sprite;
                else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                    tuto_Img.sprite = tutoReset_Sprite_EN;
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BusanMapMorning"))
        {
            if (PlayerPrefs.GetString("Busan_InGameTutorial").Equals("Start") ||
                PlayerPrefs.GetString("Busan_InGameTutorial").Equals(""))
            {
                tuto_PopupStart.SetActive(true);
                skipButton.SetActive(true);

                if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                    tuto_Img.sprite = tutoReset_Sprite;
                else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                    tuto_Img.sprite = tutoReset_Sprite_EN;
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BusanMapNight"))
        {
            if (PlayerPrefs.GetString("Busan_InGameTutorial").Equals("Start") ||
                PlayerPrefs.GetString("Busan_InGameTutorial").Equals(""))
            {
                tuto_PopupStart.SetActive(true);
                skipButton.SetActive(true);

                if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                    tuto_Img.sprite = tutoReset_Sprite;
                else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                    tuto_Img.sprite = tutoReset_Sprite_EN;
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BusanGreenLineMorning"))
        {


            Debug.Log("시작한다!!!!!--------------------------------------------1");
            if (PlayerPrefs.GetString("Busan_InGameTutorial").Equals("Start") ||
                PlayerPrefs.GetString("Busan_InGameTutorial").Equals(""))
            {
                Debug.Log("시작한다!!!!!--------------------------------------------2");
                tuto_PopupStart.SetActive(true);
                skipButton.SetActive(true);

                if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                    tuto_Img.sprite = tutoReset_Sprite;
                else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                    tuto_Img.sprite = tutoReset_Sprite_EN;
            }

        }
        else if (SceneManager.GetActiveScene().name.Equals("BusanGreenLineNight"))
        {
            if (PlayerPrefs.GetString("Busan_InGameTutorial").Equals("Start") ||
                PlayerPrefs.GetString("Busan_InGameTutorial").Equals(""))
            {
                tuto_PopupStart.SetActive(true);
                skipButton.SetActive(true);

                if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                    tuto_Img.sprite = tutoReset_Sprite;
                else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                    tuto_Img.sprite = tutoReset_Sprite_EN;
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("MapChoice"))
        {
            if (PlayerPrefs.GetString("Busan_MapChoiceTutorial").Equals("Start") ||
                PlayerPrefs.GetString("Busan_MapChoiceTutorial").Equals(""))
            {
                tuto_PopupStart.SetActive(true);
                skipButton.SetActive(true);

                if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
                    tuto_Img.sprite = tutoReset_Sprite;
                else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
                    tuto_Img.sprite = tutoReset_Sprite_EN;
            }
        }
    }

    public void LobbyTutorialReset()
    {
        nextNumber = 0;
        clickNumber = 0;

        //튜토리얼 초기화
        if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
        {
            tuto_Img.sprite = tutoReset_Sprite;
            tuto_Text.text = "<color=#ff0000>CYCLING TOUR</color>는 핏태그 센서를 이용하여" + "\n" +
                "운동을 하며 지역 곳곳을 투어 할 수 있는 게임입니다." + "\n" +
                "실내 사이클을 이용하여 게임을 즐겨보세요.";
        }
        else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
        {
            tuto_Img.sprite = tutoReset_Sprite_EN;
            tuto_Text.text = "<color=#ff0000>CYCLING TOUR</color> is a game where you can tour around " + "\n" +
                "the region while exercising using a fit tag sensor." + "\n" +
                "Enjoy the game using the indoor cycle.";
        }
    }


    public void LobbyTutorialNextButtonOn()
    {
        nextNumber += 1;

        if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
        {
            if (nextNumber.Equals(1))
            {
                tuto_Text.text = "먼저 로비를 살펴보도록 하겠습니다." + "\n" +
                "기능별 튜토리얼을 확인하고" + "\n" +
                "화면을 터치하여 다음 내용을 진행하세요.";
            }
            else if (nextNumber.Equals(2))
            {
                topButtonGroup.SetActive(false);    //상단버튼 그룹 비활성화
                tuto_PopupStart.SetActive(false);    //튜토리얼 팝업 비활성화
                tuto_Img.gameObject.SetActive(true);    //튜토리얼 이미지 활성화
                tuto_Img.sprite = tutoReset_Sprite;
            }
        }
        else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
        {
            if (nextNumber.Equals(1))
            {
                tuto_Text.text = "Let's take a look at the lobby first." + "\n" +
                "Check the tutorial for each function" + "\n" +
                "and touch the screen to proceed with the following.";
            }
            else if (nextNumber.Equals(2))
            {
                topButtonGroup.SetActive(false);    //상단버튼 그룹 비활성화
                tuto_PopupStart.SetActive(false);    //튜토리얼 팝업 비활성화
                tuto_Img.gameObject.SetActive(true);    //튜토리얼 이미지 활성화
                tuto_Img.sprite = tutoReset_Sprite_EN;
            }
        }
    }
    public void LobbyTutorial()
    {
        clickNumber += 1;
        if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
        {
            if (clickNumber.Equals(1))
                tuto_Img.sprite = tuto_Sprite[0];
            else if (clickNumber.Equals(2))
                tuto_Img.sprite = tuto_Sprite[1];
            else if (clickNumber.Equals(3))
                tuto_Img.sprite = tuto_Sprite[2];
            else if (clickNumber.Equals(4))
                tuto_Img.sprite = tuto_Sprite[3];
            else if (clickNumber.Equals(5))
                tuto_Img.sprite = tuto_Sprite[4];
            else if (clickNumber.Equals(6))
                tuto_Img.sprite = tuto_Sprite[5];
            else if (clickNumber.Equals(7))
                tuto_Img.sprite = tuto_Sprite[6];
            else if (clickNumber.Equals(8))
                tuto_Img.sprite = tuto_Sprite[7];
            else if (clickNumber.Equals(9))
                tuto_Img.sprite = tuto_Sprite[8];
            else if (clickNumber.Equals(10))
                tuto_Img.sprite = tuto_Sprite[9];
            else if (clickNumber.Equals(11))
            {
                tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
                skipButton.SetActive(false);    //스킵버튼 비활성화
                tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
                topButtonGroup.SetActive(true);    //상단버튼 그룹 활성화
                PlayerPrefs.SetString("Busan_LobbyTutorial", "End");
            }
        }
        else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
        {
            if (clickNumber.Equals(1))
                tuto_Img.sprite = tuto_Sprite_EN[0];
            else if (clickNumber.Equals(2))
                tuto_Img.sprite = tuto_Sprite_EN[1];
            else if (clickNumber.Equals(3))
                tuto_Img.sprite = tuto_Sprite_EN[2];
            else if (clickNumber.Equals(4))
                tuto_Img.sprite = tuto_Sprite_EN[3];
            else if (clickNumber.Equals(5))
                tuto_Img.sprite = tuto_Sprite_EN[4];
            else if (clickNumber.Equals(6))
                tuto_Img.sprite = tuto_Sprite_EN[5];
            else if (clickNumber.Equals(7))
                tuto_Img.sprite = tuto_Sprite_EN[6];
            else if (clickNumber.Equals(8))
                tuto_Img.sprite = tuto_Sprite_EN[7];
            else if (clickNumber.Equals(9))
                tuto_Img.sprite = tuto_Sprite_EN[8];
            else if (clickNumber.Equals(10))
                tuto_Img.sprite = tuto_Sprite_EN[9];
            else if (clickNumber.Equals(11))
            {
                tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
                skipButton.SetActive(false);    //스킵버튼 비활성화
                tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
                topButtonGroup.SetActive(true);    //상단버튼 그룹 활성화
                PlayerPrefs.SetString("Busan_LobbyTutorial", "End");
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
            PlayerPrefs.SetString("Busan_LobbyTutorial", "End");
            topButtonGroup.SetActive(true);    //상단버튼 그룹 활성화
        }
        else if(SceneManager.GetActiveScene().name.Equals("Quest"))
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

            //투토리얼 버튼 클릭했을 경우
            //if(Busan_DataManager.instance.tuto_btnOn.Equals(true))
            //{
            //    Busan_DataManager.instance.tuto_btnOn = false;
            //    PlayerPrefs.SetString("Busan_InGameTutorial", "End");
            //    Busan_DataManager.instance.GameTimePlay();  //게임 타이머 시작
            //}
            //else
            //{
            //    PlayerPrefs.SetString("Busan_InGameTutorial", "End");
            //    Busan_UIManager.instance.GameStart();   //게임 처음 시작
            //}

        }
        else if (SceneManager.GetActiveScene().name.Equals("MapChoice"))
            PlayerPrefs.SetString("Busan_MapChoiceTutorial", "End");
    }


    public void QuestTutorialNextButtonOn()
    {
        clickNumber = 0;
        if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
            tuto_Img.sprite = tutoReset_Sprite;
        else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
            tuto_Img.sprite = tutoReset_Sprite_EN;

        tuto_PopupStart.SetActive(false);    //튜토리얼 팝업 비활성화
        tuto_Img.gameObject.SetActive(true);    //튜토리얼 이미지 활성화
    }

    public void QuestTutorial()
    {
        clickNumber += 1;
        if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
        {
            if (clickNumber.Equals(1))
                tuto_Img.sprite = tuto_Sprite[0];
            else if (clickNumber.Equals(2))
                tuto_Img.sprite = tuto_Sprite[1];
            else if (clickNumber.Equals(3))
                tuto_Img.sprite = tuto_Sprite[2];
            else if (clickNumber.Equals(4))
            {
                tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
                skipButton.SetActive(false);    //스킵버튼 비활성화
                tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
                PlayerPrefs.SetString("Busan_QuestTutorial", "End");
            }
        }
        else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
        {
            if (clickNumber.Equals(1))
                tuto_Img.sprite = tuto_Sprite_EN[0];
            else if (clickNumber.Equals(2))
                tuto_Img.sprite = tuto_Sprite_EN[1];
            else if (clickNumber.Equals(3))
                tuto_Img.sprite = tuto_Sprite_EN[2];
            else if (clickNumber.Equals(4))
            {
                tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
                skipButton.SetActive(false);    //스킵버튼 비활성화
                tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
                PlayerPrefs.SetString("Busan_QuestTutorial", "End");
            }
        }  
    }



    public void InGameTutorialNextButtonOn()
    {
        clickNumber = 0;
        if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
            tuto_Img.sprite = tutoReset_Sprite;
        else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
            tuto_Img.sprite = tutoReset_Sprite_EN;

        tuto_PopupStart.SetActive(false);    //튜토리얼 팝업 비활성화
        tuto_Img.gameObject.SetActive(true);    //튜토리얼 이미지 활성화
    }

    public void InGameTutorial()
    {
        clickNumber += 1;

        if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
        {
            if (clickNumber.Equals(1))
                tuto_Img.sprite = tuto_Sprite[0];
            else if (clickNumber.Equals(2))
                tuto_Img.sprite = tuto_Sprite[1];
            else if (clickNumber.Equals(3))
                tuto_Img.sprite = tuto_Sprite[2];
            else if (clickNumber.Equals(4))
                tuto_Img.sprite = tuto_Sprite[3];
            else if (clickNumber.Equals(5))
                tuto_Img.sprite = tuto_Sprite[4];
            else if (clickNumber.Equals(6))
                tuto_Img.sprite = tuto_Sprite[5];
            else if (clickNumber.Equals(7))
                tuto_Img.sprite = tuto_Sprite[6];
            else if (clickNumber.Equals(8))
                tuto_Img.sprite = tuto_Sprite[7];
            else if (clickNumber.Equals(9))
                tuto_Img.sprite = tuto_Sprite[8];
            else if (clickNumber.Equals(10))
                tuto_Img.sprite = tuto_Sprite[9];
            else if (clickNumber.Equals(11))
                tuto_Img.sprite = tuto_Sprite[10];
            else if (clickNumber.Equals(12))
            {
                tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
                skipButton.SetActive(false);    //스킵버튼 비활성화
                tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
                PlayerPrefs.SetString("Busan_InGameTutorial", "End");
                //BusanMap_GameTime.instance.PlayTime();
            }
        }
        else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
        {
            if (clickNumber.Equals(1))
                tuto_Img.sprite = tuto_Sprite_EN[0];
            else if (clickNumber.Equals(2))
                tuto_Img.sprite = tuto_Sprite_EN[1];
            else if (clickNumber.Equals(3))
                tuto_Img.sprite = tuto_Sprite_EN[2];
            else if (clickNumber.Equals(4))
                tuto_Img.sprite = tuto_Sprite_EN[3];
            else if (clickNumber.Equals(5))
                tuto_Img.sprite = tuto_Sprite_EN[4];
            else if (clickNumber.Equals(6))
                tuto_Img.sprite = tuto_Sprite_EN[5];
            else if (clickNumber.Equals(7))
                tuto_Img.sprite = tuto_Sprite_EN[6];
            else if (clickNumber.Equals(8))
                tuto_Img.sprite = tuto_Sprite_EN[7];
            else if (clickNumber.Equals(9))
                tuto_Img.sprite = tuto_Sprite_EN[8];
            else if (clickNumber.Equals(10))
                tuto_Img.sprite = tuto_Sprite_EN[9];
            else if (clickNumber.Equals(11))
                tuto_Img.sprite = tuto_Sprite_EN[10];
            else if (clickNumber.Equals(12))
            {
                tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
                skipButton.SetActive(false);    //스킵버튼 비활성화
                tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
                PlayerPrefs.SetString("Busan_InGameTutorial", "End");
                //BusanMap_GameTime.instance.PlayTime();
            }
        }
    }


    public void MapChoiceTutorialNextButton()
    {
        clickNumber = 0;
        if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
            tuto_Img.sprite = tutoReset_Sprite;
        else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
            tuto_Img.sprite = tutoReset_Sprite_EN;
        tuto_PopupStart.SetActive(false);    //튜토리얼 팝업 비활성화
        tuto_Img.gameObject.SetActive(true);    //튜토리얼 이미지 활성화
    }

    public void MapChoiceTutorial()
    {
        clickNumber += 1;

        if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
        {
            if (clickNumber.Equals(1))
                tuto_Img.sprite = tuto_Sprite[0];
            else if (clickNumber.Equals(2))
                tuto_Img.sprite = tuto_Sprite[1];
            else if (clickNumber.Equals(3))
                tuto_Img.sprite = tuto_Sprite[2];
            else if (clickNumber.Equals(4))
            {
                tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
                skipButton.SetActive(false);    //스킵버튼 비활성화
                tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
                PlayerPrefs.SetString("Busan_MapChoiceTutorial", "End");
            }
        }
        else if (PlayerPrefs.GetString("Busan_Language").Equals("EN"))
        {
            if (clickNumber.Equals(1))
                tuto_Img.sprite = tuto_Sprite_EN[0];
            else if (clickNumber.Equals(2))
                tuto_Img.sprite = tuto_Sprite_EN[1];
            else if (clickNumber.Equals(3))
                tuto_Img.sprite = tuto_Sprite_EN[2];
            else if (clickNumber.Equals(4))
            {
                tuto_Img.gameObject.SetActive(false);    //튜토리얼 이미지 비활성화
                skipButton.SetActive(false);    //스킵버튼 비활성화
                tuto_PopupEnd.SetActive(true);  //끝나는 팝업 활성화
                PlayerPrefs.SetString("Busan_MapChoiceTutorial", "End");
            }
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
