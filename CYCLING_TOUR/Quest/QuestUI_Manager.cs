
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class QuestUI_Manager : MonoBehaviour
{
    public ToggleGroup questColumnToggleGroup;
    public GameObject todayTogglePanel;
    public GameObject mapTogglePanel;
    public Text expText;
    public Text coinText;


    string oneCennect, twoCennect;  //처음접속, 두번째접속

    //----------일일 퀘스트 변수
    [Header(">>일일 퀘스트 변수")]
    public Text[] quest_titleText;  //제목
    public Slider[] quest_slider;   //갯수슬라이드
    public Text[] quest_sliderText; //갯수텍스트
    public Text[] quest_coinText;   //코인
    public GameObject[] quest_completeImg;  //미션성공 도장 이미지
    public Button[] quest_takeBtn;  //보상받기 버튼
    public GameObject[] quest_tekeImg;  //받기완료 이미지
    public Sprite takeBtnSprite;  //완료버튼 이미지
    public GameObject[] quest_Obj;  //칼로리&최고속도 

    int connectCount;

    //----------맵 퀘스트 변수
    [Header(">>아시아맵 코스완주 필요 변수")]
    //아시아맵 코스완주 필요 변수
    public GameObject[] asiaMapCourseFinishObj;
    public Slider[] asiaMapCourseFinishSlider;
    public Text[] asiaMapCourseFinishSliderText;
    public Button[] asiaMapCourseFinishButton;
    public Text[] asiaMapCourseFinishPriceText;
    [Header(">>아시아맵 코스제한시간 완주 필요 변수")]
    //아시아맵 코스제한시간 완주 필요 변수
    public GameObject[] asiaMapTimeLimitFinishObj;
    public Text[] asiaMapTimeLimitFinishTilteText;
    public Slider[] asiaMapTimeLimitFinishSlider;
    public Text[] asiaMapTimeLimitFinishSliderText;
    public Button[] asiaMapTimeLimitFinishButton;
    public Text[] asiaMapTimeLimitFinishPriceText;
    [Header(">>아시아맵 커스텀 미션 필요 변수")]
    //아시아맵 커스텀 미션 필요 변수
    public GameObject[] asiaMapCustomQuestObj;
    public Slider[] asiaMapCustomQuestSlider;
    public Text[] asiaMapCustomQuestSliderText;
    public Text[] asiaMapCustomQuestCoinText;


    //아시아맵 완주 변수
    int asiaNormal1_Count, asiaNormal2_Count, asiaNormal3_Count, asiaHard1_Count, asiaHard2_Count, asiaHard3_Count;
    //아시아맵 시간제한 완주변수
    int asiaNormalTimeLimit_Count1, asiaNormalTimeLimit_Count2, asiaNormalTimeLimit_Count3, asiaHardTimeLimit_Count1, asiaHardTimeLimit_Count2, asiaHardTimeLimit_Count3;



    public Toggle questColumnToggleGroupCurrentSeletion
    {
        get { return questColumnToggleGroup.ActiveToggles().FirstOrDefault(); }
    }



    void Start()
    {
        //Debug.Log("---StoreVisit " + PlayerPrefs.GetString("StoreVisit"));
        //Debug.Log("----MyInfoVisit " + PlayerPrefs.GetString("MyInfoVisit"));
        //Debug.Log("---InventoryVisit " + PlayerPrefs.GetString("InventoryVisit"));
        //Debug.Log("---RankJonVisit " + PlayerPrefs.GetString("RankJonVisit"));
        //Debug.Log("---CustomChange " + PlayerPrefs.GetString("CustomChange"));

        //Debug.Log("---GoldUse " + PlayerPrefs.GetString("GoldUse"));



        expText.text = PlayerPrefs.GetInt("AT_Player_CurrExp").ToString();
        coinText.text = QuestData_Manager.instance.CommaText(PlayerPrefs.GetInt("AT_Player_Gold"));
        QuestTogglePanelAction(true, false);    //처음 일일퀘스트 판넬 활성화 초기화
        ConnectDay();

        

        //PlayerPrefs.SetInt("AsiaNormal1Finish", 0); 
        //PlayerPrefs.SetInt("AsiaNormal2Finish", 0);
        //PlayerPrefs.SetInt("AsiaNormal3Finish", 0);
        //PlayerPrefs.SetInt("AsiaNormal1FinishAmount", 3);

        //PlayerPrefs.SetString("Asia Normal 1Course", "Fise1235/10:00");
        //PlayerPrefs.SetInt("AsiaNormalTimeLimitFinish1", 1);
        //Debug.Log(PlayerPrefs.GetString("MyInfoVisit"));
    }


    public void QuestColumnToggleGroup()
    {
        if(questColumnToggleGroup.ActiveToggles().Any())
        {
            if(questColumnToggleGroupCurrentSeletion.name == "TodayToggle")
            {
                QuestTogglePanelAction(true, false);
            }
            else if(questColumnToggleGroupCurrentSeletion.name == "MapToggle")
            {
                QuestTogglePanelAction(false, true);
            }
        }
    }
    //퀘스트 선택 시 활성화/비활성화 판넬
    void QuestTogglePanelAction(bool _today, bool _map)
    {
        todayTogglePanel.SetActive(_today);
        mapTogglePanel.SetActive(_map);
    }


    //출석 체크 형 함수 - 오늘 접속했는지
    public void ConnectDay()
    {
        //PlayerPrefs.SetString("OneCennectDay","");
        //PlayerPrefs.SetString("TwoCennectDay","");
        oneCennect = PlayerPrefs.GetString("AT_OneCennectDay");

        //접속했다는 기록이 없으면 처음 로그인
        if (oneCennect == "")
        {
            PlayerPrefs.SetString("AT_OneCennectDay", QuestData_Manager.instance.toDayArr[1] + QuestData_Manager.instance.toDayArr[2]);
            oneCennect = PlayerPrefs.GetString("AT_OneCennectDay");
        }
        else if (oneCennect != "")
        {
            PlayerPrefs.SetString("AT_TwoCennectDay", QuestData_Manager.instance.toDayArr[1] + QuestData_Manager.instance.toDayArr[2]);
            twoCennect = PlayerPrefs.GetString("AT_TwoCennectDay");
        }

        //처음 접속 날짜와 두번째 접속 날짜가 다를 경우 - 출첵
        if (oneCennect != twoCennect)
        {
            //Debug.Log("------------아--------------------------");
            //퀘스트 미션 초기화
            //퀘스트1 고정 - 접속하기
            PlayerPrefs.SetInt("AT_TodayQuest1", 0);   //퀘스트 초기화 - 오늘 처음 접속했음
            ServerManager.Instance.Update_TodayQuest("TodayQuest1", "No", 0);
            QuestData_Manager.instance.Quest_ConnectMission(quest_titleText[0], quest_sliderText[0], quest_slider[0], quest_coinText[0], quest_completeImg[0], quest_takeBtn[0], quest_tekeImg[0], takeBtnSprite);

            //퀘스트2 고정랜덤 - 방문하기
            PlayerPrefs.SetInt("AT_TodayQuest2", QuestData_Manager.instance.VisitRandom());
            QuestData_Manager.instance.Quest_VisitMission(quest_titleText[1], quest_sliderText[1], quest_slider[1], quest_coinText[1], quest_completeImg[1], quest_takeBtn[1], quest_tekeImg[1], takeBtnSprite);

            //퀘스트3 고정랜덤 - 게임활용 미션
            PlayerPrefs.SetInt("AT_TodayQuest3", QuestData_Manager.instance.GameUseRandom());
            QuestData_Manager.instance.Quest_GameUseMission(quest_titleText[2], quest_sliderText[2], quest_slider[2], quest_coinText[2], quest_completeImg[2], quest_takeBtn[2], quest_tekeImg[2], takeBtnSprite);

            //퀘스트4 고정랜덤 - 오늘 칼로리 소모
            PlayerPrefs.SetInt("AT_TodayQuest4", QuestData_Manager.instance.BurnUp_CalroieToday());
            QuestData_Manager.instance.Quest_BurnUpCalroieTodayMission(quest_Obj[0], quest_titleText[3], quest_slider[3], quest_sliderText[3], takeBtnSprite);

            //퀘스트5 고정랜덤 - 오늘 최대 속도
            PlayerPrefs.SetInt("AT_TodayQuest5", QuestData_Manager.instance.MaxSpeedToday());
            QuestData_Manager.instance.Quest_MaxSpeedTodayMission(quest_Obj[1], quest_titleText[4], quest_slider[4], quest_sliderText[4], takeBtnSprite);


            //맵퀘스트 - 커스텀 보상 퀘스트
            //아시아맵 한번씩 완주
            QuestData_Manager.instance.AsiaMap_AllOneFinish(asiaMapCustomQuestObj[0], asiaMapCustomQuestSlider[0], asiaMapCustomQuestSliderText[0]);

            //아시아맵 10번씩 완주
            QuestData_Manager.instance.AsiaMap_AllTenFinish(asiaMapCustomQuestObj[1], asiaMapCustomQuestSlider[1], asiaMapCustomQuestSliderText[1]);

            //아시아앱 20번씩 완주
            QuestData_Manager.instance.AsiaMap_AllTwentyFinish(asiaMapCustomQuestObj[2], asiaMapCustomQuestSlider[2], asiaMapCustomQuestSliderText[2]);

            //아시압맵 500km
            QuestData_Manager.instance.AsiaMap500KmPass(asiaMapCustomQuestObj[3], asiaMapCustomQuestSlider[3], asiaMapCustomQuestSliderText[3]);

            //아시압맵 500km
            QuestData_Manager.instance.AsiaMap1000KmPass(asiaMapCustomQuestObj[4], asiaMapCustomQuestSlider[4], asiaMapCustomQuestSliderText[4]);

            //아시압맵 500km
            QuestData_Manager.instance.AsiaMap1500KmPass(asiaMapCustomQuestObj[5], asiaMapCustomQuestSlider[5], asiaMapCustomQuestSliderText[5]);

            //맵 퀘스트 - 완주하기
            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AT_AsiaNormal1Finish"), PlayerPrefs.GetInt("AT_AsiaNormal1FinishAmount"), asiaMapCourseFinishObj[0],
                PlayerPrefs.GetString("AT_Asia Normal 1Course"), asiaMapCourseFinishSlider[0], asiaMapCourseFinishSliderText[0], asiaMapCourseFinishButton[0]);
            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AT_AsiaNormal2Finish"), PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount"), asiaMapCourseFinishObj[1],
                PlayerPrefs.GetString("AT_Asia Normal 2Course"), asiaMapCourseFinishSlider[1], asiaMapCourseFinishSliderText[1], asiaMapCourseFinishButton[1]);
            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AT_AsiaNormal3Finish"), PlayerPrefs.GetInt("AT_AsiaNormal3FinishAmount"), asiaMapCourseFinishObj[2],
                PlayerPrefs.GetString("AT_Asia Normal 3Course"), asiaMapCourseFinishSlider[2], asiaMapCourseFinishSliderText[2], asiaMapCourseFinishButton[2]);
            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AT_AsiaHard1Finish"), PlayerPrefs.GetInt("AT_AsiaHard1FinishAmount"), asiaMapCourseFinishObj[3],
                PlayerPrefs.GetString("AT_Asia Hard 1Course"), asiaMapCourseFinishSlider[3], asiaMapCourseFinishSliderText[3], asiaMapCourseFinishButton[3]);
            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AT_AsiaHard2Finish"), PlayerPrefs.GetInt("AT_AsiaHard2FinishAmount"), asiaMapCourseFinishObj[4],
                PlayerPrefs.GetString("AT_Asia Hard 2Course"), asiaMapCourseFinishSlider[4], asiaMapCourseFinishSliderText[4], asiaMapCourseFinishButton[4]);
            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AT_AsiaHard3Finish"), PlayerPrefs.GetInt("AT_AsiaHard3FinishAmount"), asiaMapCourseFinishObj[5],
                PlayerPrefs.GetString("AT_Asia Hard 3Course"), asiaMapCourseFinishSlider[5], asiaMapCourseFinishSliderText[5], asiaMapCourseFinishButton[5]);

            //맵 퀘스트 - 시간제한 완주하기
            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AT_AsiaNormalTimeLimitFinish1"), PlayerPrefs.GetString("AT_CurrPlayTime1"), asiaMapTimeLimitFinishTilteText[0], 
                asiaMapTimeLimitFinishSlider[0], asiaMapTimeLimitFinishSliderText[0], asiaMapTimeLimitFinishButton[0], asiaMapTimeLimitFinishObj[0], takeBtnSprite);
            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AT_AsiaNormalTimeLimitFinish2"), PlayerPrefs.GetString("AT_CurrPlayTime2"), asiaMapTimeLimitFinishTilteText[1], 
                asiaMapTimeLimitFinishSlider[1], asiaMapTimeLimitFinishSliderText[1], asiaMapTimeLimitFinishButton[1], asiaMapTimeLimitFinishObj[1], takeBtnSprite);
            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AT_AsiaNormalTimeLimitFinish3"), PlayerPrefs.GetString("AT_CurrPlayTime3"), asiaMapTimeLimitFinishTilteText[2], 
                asiaMapTimeLimitFinishSlider[2], asiaMapTimeLimitFinishSliderText[2], asiaMapTimeLimitFinishButton[2], asiaMapTimeLimitFinishObj[2], takeBtnSprite);
            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AT_AsiaHardTimeLimitFinish1"), PlayerPrefs.GetString("AT_CurrPlayTime4"), asiaMapTimeLimitFinishTilteText[3], 
                asiaMapTimeLimitFinishSlider[3], asiaMapTimeLimitFinishSliderText[3], asiaMapTimeLimitFinishButton[3], asiaMapTimeLimitFinishObj[3], takeBtnSprite);
            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AT_AsiaHardTimeLimitFinish2"), PlayerPrefs.GetString("AT_CurrPlayTime5"), asiaMapTimeLimitFinishTilteText[4], 
                asiaMapTimeLimitFinishSlider[4], asiaMapTimeLimitFinishSliderText[4], asiaMapTimeLimitFinishButton[4], asiaMapTimeLimitFinishObj[4], takeBtnSprite);
            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AT_AsiaHardTimeLimitFinish3"), PlayerPrefs.GetString("AT_CurrPlayTime6"), asiaMapTimeLimitFinishTilteText[5], 
                asiaMapTimeLimitFinishSlider[5], asiaMapTimeLimitFinishSliderText[5], asiaMapTimeLimitFinishButton[5], asiaMapTimeLimitFinishObj[5], takeBtnSprite);

            PlayerPrefs.SetString("AT_TwoCennectDay", QuestData_Manager.instance.toDayArr[1] + QuestData_Manager.instance.toDayArr[2]);
            twoCennect = PlayerPrefs.GetString("AT_TwoCennectDay");

            //두번째 접속한 날짜를 앞으로 옮겨준다. 그래야 다음 들어왔을 때 또 비교가능
            PlayerPrefs.SetString("AT_OneCennectDay", PlayerPrefs.GetString("AT_TwoCennectDay"));
            //Debug.Log("1 : " + oneCennect + " :2: " + twoCennect);
            ServerManager.Instance.UpdateConnectedState("TwoCennectDay", twoCennect);
            ServerManager.Instance.connectedStateCheck.oneConnectDay = oneCennect;
            ServerManager.Instance.connectedStateCheck.twoConnectDay = twoCennect;
        }
        else
        {
            //Debug.Log("여기 들어오게 됩니다.");
            //Debug.Log("11 : " + oneCennect + " :2: " + twoCennect);
            ServerManager.Instance.UpdateConnectedState("TwoCennectDay", twoCennect);
            ServerManager.Instance.connectedStateCheck.oneConnectDay = oneCennect;
            ServerManager.Instance.connectedStateCheck.twoConnectDay = twoCennect;

            //퀘스트1 고정 - 접속하기
            QuestData_Manager.instance.Quest_ConnectMission(quest_titleText[0], quest_sliderText[0], quest_slider[0], quest_coinText[0], quest_completeImg[0], quest_takeBtn[0], quest_tekeImg[0], takeBtnSprite);

            //퀘스트2 고정랜덤 - 방문하기
            QuestData_Manager.instance.Quest_VisitMission(quest_titleText[1], quest_sliderText[1], quest_slider[1], quest_coinText[1], quest_completeImg[1], quest_takeBtn[1], quest_tekeImg[1], takeBtnSprite);
            
            //퀘스트3 고정랜덤 - 게임활용 미션
            QuestData_Manager.instance.Quest_GameUseMission(quest_titleText[2], quest_sliderText[2], quest_slider[2], quest_coinText[2], quest_completeImg[2], quest_takeBtn[2], quest_tekeImg[2], takeBtnSprite);

            //퀘스트4 고정랜덤 - 오늘 칼로리 소모
            QuestData_Manager.instance.Quest_BurnUpCalroieTodayMission(quest_Obj[0], quest_titleText[3], quest_slider[3], quest_sliderText[3], takeBtnSprite);

            //퀘스트5 고정랜덤 - 오늘 최대 속도
            QuestData_Manager.instance.Quest_MaxSpeedTodayMission(quest_Obj[1], quest_titleText[4], quest_slider[4], quest_sliderText[4], takeBtnSprite);


            //맵퀘스트 - 커스텀 보상 퀘스트
            //아시아맵 한번씩 완주
            QuestData_Manager.instance.AsiaMap_AllOneFinish(asiaMapCustomQuestObj[0], asiaMapCustomQuestSlider[0], asiaMapCustomQuestSliderText[0]);

            //아시아맵 10번씩 완주
            QuestData_Manager.instance.AsiaMap_AllTenFinish(asiaMapCustomQuestObj[1], asiaMapCustomQuestSlider[1], asiaMapCustomQuestSliderText[1]);

            //아시아앱 20번씩 완주
            QuestData_Manager.instance.AsiaMap_AllTwentyFinish(asiaMapCustomQuestObj[2], asiaMapCustomQuestSlider[2], asiaMapCustomQuestSliderText[2]);

            //아시압맵 500km
            QuestData_Manager.instance.AsiaMap500KmPass(asiaMapCustomQuestObj[3], asiaMapCustomQuestSlider[3], asiaMapCustomQuestSliderText[3]);

            //아시압맵 1000km
            QuestData_Manager.instance.AsiaMap1000KmPass(asiaMapCustomQuestObj[4], asiaMapCustomQuestSlider[4], asiaMapCustomQuestSliderText[4]);

            //아시압맵 1500km
            QuestData_Manager.instance.AsiaMap1500KmPass(asiaMapCustomQuestObj[5], asiaMapCustomQuestSlider[5], asiaMapCustomQuestSliderText[5]);

            //맵 퀘스트 - 완주하기
            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AT_AsiaNormal1Finish"), PlayerPrefs.GetInt("AT_AsiaNormal1FinishAmount"), asiaMapCourseFinishObj[0], 
                PlayerPrefs.GetString("AT_Asia Normal 1Course"), asiaMapCourseFinishSlider[0], asiaMapCourseFinishSliderText[0], asiaMapCourseFinishButton[0]);
            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AT_AsiaNormal2Finish"), PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount"), asiaMapCourseFinishObj[1],
                PlayerPrefs.GetString("AT_Asia Normal 2Course"), asiaMapCourseFinishSlider[1], asiaMapCourseFinishSliderText[1], asiaMapCourseFinishButton[1]);
            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AT_AsiaNormal3Finish"), PlayerPrefs.GetInt("AT_AsiaNormal3FinishAmount"), asiaMapCourseFinishObj[2],
                PlayerPrefs.GetString("AT_Asia Normal 3Course"), asiaMapCourseFinishSlider[2], asiaMapCourseFinishSliderText[2], asiaMapCourseFinishButton[2]);
            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AT_AsiaHard1Finish"), PlayerPrefs.GetInt("AT_AsiaHard1FinishAmount"), asiaMapCourseFinishObj[3],
                PlayerPrefs.GetString("AT_Asia Hard 1Course"), asiaMapCourseFinishSlider[3], asiaMapCourseFinishSliderText[3], asiaMapCourseFinishButton[3]);
            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AT_AsiaHard2Finish"), PlayerPrefs.GetInt("AT_AsiaHard2FinishAmount"), asiaMapCourseFinishObj[4],
                PlayerPrefs.GetString("AT_Asia Hard 2Course"), asiaMapCourseFinishSlider[4], asiaMapCourseFinishSliderText[4], asiaMapCourseFinishButton[4]);
            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AT_AsiaHard3Finish"), PlayerPrefs.GetInt("AT_AsiaHard3FinishAmount"), asiaMapCourseFinishObj[5],
                PlayerPrefs.GetString("AT_Asia Hard 3Course"), asiaMapCourseFinishSlider[5], asiaMapCourseFinishSliderText[5], asiaMapCourseFinishButton[5]);

            //맵 퀘스트 - 시간제한 완주하기
            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AT_AsiaNormalTimeLimitFinish1"), PlayerPrefs.GetString("AT_CurrPlayTime1"), asiaMapTimeLimitFinishTilteText[0], 
                asiaMapTimeLimitFinishSlider[0], asiaMapTimeLimitFinishSliderText[0], asiaMapTimeLimitFinishButton[0], asiaMapTimeLimitFinishObj[0], takeBtnSprite);
            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AT_AsiaNormalTimeLimitFinish2"), PlayerPrefs.GetString("AT_CurrPlayTime2"), asiaMapTimeLimitFinishTilteText[1], 
                asiaMapTimeLimitFinishSlider[1], asiaMapTimeLimitFinishSliderText[1], asiaMapTimeLimitFinishButton[1], asiaMapTimeLimitFinishObj[1], takeBtnSprite);
            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AT_AsiaNormalTimeLimitFinish3"), PlayerPrefs.GetString("AT_CurrPlayTime3"), asiaMapTimeLimitFinishTilteText[2], 
                asiaMapTimeLimitFinishSlider[2], asiaMapTimeLimitFinishSliderText[2], asiaMapTimeLimitFinishButton[2], asiaMapTimeLimitFinishObj[2], takeBtnSprite);
            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AT_AsiaHardTimeLimitFinish1"), PlayerPrefs.GetString("AT_CurrPlayTime4"), asiaMapTimeLimitFinishTilteText[3], 
                asiaMapTimeLimitFinishSlider[3], asiaMapTimeLimitFinishSliderText[3], asiaMapTimeLimitFinishButton[3], asiaMapTimeLimitFinishObj[3], takeBtnSprite);
            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AT_AsiaHardTimeLimitFinish2"), PlayerPrefs.GetString("AT_CurrPlayTime5"), asiaMapTimeLimitFinishTilteText[4], 
                asiaMapTimeLimitFinishSlider[4], asiaMapTimeLimitFinishSliderText[4], asiaMapTimeLimitFinishButton[4], asiaMapTimeLimitFinishObj[4], takeBtnSprite);
            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AT_AsiaHardTimeLimitFinish3"), PlayerPrefs.GetString("AT_CurrPlayTime6"), asiaMapTimeLimitFinishTilteText[5], 
                asiaMapTimeLimitFinishSlider[5], asiaMapTimeLimitFinishSliderText[5], asiaMapTimeLimitFinishButton[5], asiaMapTimeLimitFinishObj[5], takeBtnSprite);
        }
    }

    //퀘스트 1 보상받기 버튼
    public void Quest_ConnectButtonOn()
    {
        connectCount = PlayerPrefs.GetInt("AT_TodayQuest1");
        connectCount++;
        PlayerPrefs.SetInt("AT_TodayQuest1", connectCount);

        QuestData_Manager.instance.GetCoinReward(coinText, quest_coinText[0]);

        // 서버에 반영
        ServerManager.Instance.Update_TodayQuest("TodayQuest1", "None", connectCount);

        // 현재 접속시간 서버에 반영
        ServerManager.Instance.UpdateConnectedState("ConnectTime", PlayerPrefs.GetString("AT_ConnectTime"));
        ServerManager.Instance.connectedStateCheck.connectTime = PlayerPrefs.GetString("AT_ConnectTime");

        //퀘스트1 고정 - 접속하기
        QuestData_Manager.instance.Quest_ConnectMission(quest_titleText[0], quest_sliderText[0], quest_slider[0], quest_coinText[0], quest_completeImg[0], quest_takeBtn[0], quest_tekeImg[0], takeBtnSprite);
    }

    //퀘스트2 보상받기 버튼
    public void Quest_VisitButtonOn()
    {
        quest_takeBtn[1].interactable = false;  //보상받았기 때문에 비활성화

        int visitNum = PlayerPrefs.GetInt("AT_TodayQuest2");

        if (visitNum == 0)
        {
            PlayerPrefs.SetString("AT_StoreVisit", "MissionOk");
        }
        else if (visitNum == 1)
        {
            PlayerPrefs.SetString("AT_MyInfoVisit", "MissionOk");
        }
        else if (visitNum == 2)
        {
            PlayerPrefs.SetString("AT_InventoryVisit", "MissionOk");
        }
        else if(visitNum == 3)
        {
            PlayerPrefs.SetString("AT_RankJonVisit", "MissionOk");
        }

        ServerManager.Instance.Update_TodayQuest("TodayQuest2", "MissionOk");

        QuestData_Manager.instance.GetCoinReward(coinText, quest_coinText[1]);
        quest_takeBtn[1].GetComponent<Image>().sprite = takeBtnSprite;
        quest_tekeImg[1].SetActive(true);

    }

    //퀘스트3 보상받기 버튼
    public void Quest_GameUseButtonOn()
    {
        int usetNum = PlayerPrefs.GetInt("AT_TodayQuest3");
        
        if(usetNum == 0)
            PlayerPrefs.SetString("AT_ProfileChange", "MissionOk");
        else if (usetNum == 1)
            PlayerPrefs.SetString("AT_GameOnePlay", "MissionOk");
        else if (usetNum == 2)
            PlayerPrefs.SetString("AT_RankUp", "MissionOk");
        else if (usetNum == 3)
            PlayerPrefs.SetString("AT_CustomChange", "MissionOk");
        else if (usetNum == 4)
            PlayerPrefs.SetString("AT_GoldUse", "MissionOk");
        else if (usetNum == 5)
            PlayerPrefs.SetString("AT_ItemUse", "MissionOk");
        else if (usetNum == 6)
            PlayerPrefs.SetString("AT_ItemPurchase", "MissionOk");

        ServerManager.Instance.Update_TodayQuest("TodayQuest3", "MissionOk");

        QuestData_Manager.instance.GetCoinReward(coinText, quest_coinText[2]);
        quest_takeBtn[2].GetComponent<Image>().sprite = takeBtnSprite;
        quest_tekeImg[2].SetActive(true);
    }

    public Image rewardBtn;

    //퀘스트4 보상받기 버튼
    public void Quest_KcalBurnUpButtonOn()
    {
        PlayerPrefs.SetString("AT_KcalBurnUp", "MissionOk");

        ServerManager.Instance.Update_TodayQuest("TodayQuest4", "MissionOk");
        
        QuestData_Manager.instance.GetCoinReward(coinText, quest_coinText[3]);

        rewardBtn.sprite = takeBtnSprite;    //이미지 변경
        quest_Obj[0].transform.GetChild(1).gameObject.SetActive(true);  //받기완료 이미지 활성화
    }

    //퀘스트5 보상받기 버튼
    public void Quest_MaxSpeedButtonOn()
    {
        PlayerPrefs.SetString("AT_MaxSpeedToday", "MissionOk");

        ServerManager.Instance.Update_TodayQuest("TodayQuest5", "MissionOk");

        QuestData_Manager.instance.GetCoinReward(coinText, quest_coinText[4]);

        quest_Obj[1].transform.GetChild(7).GetComponent<Image>().sprite = takeBtnSprite;    //이미지 변경
        quest_Obj[1].transform.GetChild(1).gameObject.SetActive(true); //받기완료 이미지 활성화
    }

    ////아시아맵 코스 완주
    public void AsiaCourse1FinishButtonOn()
    {
        //Debug.Log("___ 제한속도 " + PlayerPrefs.GetInt("AT_AsiaNormal1FinishAmount"));
        asiaNormal1_Count = PlayerPrefs.GetInt("AT_AsiaNormal1Finish");    //처음 불러오고
        asiaNormal1_Count++;
        PlayerPrefs.SetInt("AT_AsiaNormal1Finish", asiaNormal1_Count); //처음 저장

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCourseFinishPriceText[0]);    //코인보상 받기

        QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AT_AsiaNormal1Finish"), PlayerPrefs.GetInt("AT_AsiaNormal1FinishAmount"), asiaMapCourseFinishObj[0], 
            PlayerPrefs.GetString("AT_Asia Normal 1Course"), asiaMapCourseFinishSlider[0], asiaMapCourseFinishSliderText[0], asiaMapCourseFinishButton[0]);

        // 서버에 상태 저장 PlayerPrefs.SetInt("AsiaNormal1Finish", asiaNormal1_Count);
        ServerManager.Instance.Update_MapQuest("AsiaNormal1Finish", asiaNormal1_Count);
    }
    public void AsiaCourse2FinishButtonOn()
    {
        asiaNormal2_Count = PlayerPrefs.GetInt("AT_AsiaNormal2Finish");
        asiaNormal2_Count++;
        PlayerPrefs.SetInt("AT_AsiaNormal2Finish", asiaNormal2_Count);

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCourseFinishPriceText[1]);    //코인보상 받기

        QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AT_AsiaNormal2Finish"), PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount"), asiaMapCourseFinishObj[1],
            PlayerPrefs.GetString("AT_Asia Normal 2Course"), asiaMapCourseFinishSlider[1], asiaMapCourseFinishSliderText[1], asiaMapCourseFinishButton[1]);
        
        // 서버 저장 Normal 2
        ServerManager.Instance.Update_MapQuest("AsiaNormal2Finish", asiaNormal2_Count);
    }
    public void AsiaCourse3FinishButtonOn()
    {
        asiaNormal3_Count = PlayerPrefs.GetInt("AT_AsiaNormal3Finish");
        asiaNormal3_Count++;
        PlayerPrefs.SetInt("AT_AsiaNormal3Finish", asiaNormal3_Count);

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCourseFinishPriceText[2]);    //코인보상 받기

        QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AT_AsiaNormal3Finish"), PlayerPrefs.GetInt("AT_AsiaNormal3FinishAmount"), asiaMapCourseFinishObj[2],
            PlayerPrefs.GetString("AT_Asia Normal 3Course"), asiaMapCourseFinishSlider[2], asiaMapCourseFinishSliderText[2], asiaMapCourseFinishButton[2]);

        // 서버 저장 Normal 3
        ServerManager.Instance.Update_MapQuest("AsiaNormal3Finish", asiaNormal3_Count);
    }
    public void AsiaCourseHard1FinishButtonOn()
    {
        asiaHard1_Count = PlayerPrefs.GetInt("AT_AsiaHard1Finish");
        asiaHard1_Count++;
        PlayerPrefs.SetInt("AT_AsiaHard1Finish", asiaHard1_Count);

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCourseFinishPriceText[3]);    //코인보상 받기

        QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AT_AsiaHard1Finish"), PlayerPrefs.GetInt("AT_AsiaHard1FinishAmount"), asiaMapCourseFinishObj[3],
            PlayerPrefs.GetString("AT_Asia Hard 1Course"), asiaMapCourseFinishSlider[3], asiaMapCourseFinishSliderText[3], asiaMapCourseFinishButton[3]);

        // 서버 저장 Hard 1
        ServerManager.Instance.Update_MapQuest("AsiaHard1Finish", asiaHard1_Count);
    }
    public void AsiaCourseHard2FinishButtonOn()
    {
        asiaHard2_Count = PlayerPrefs.GetInt("AT_AsiaHard2Finish");
        asiaHard2_Count++;
        PlayerPrefs.SetInt("AT_AsiaHard2Finish", asiaHard2_Count);

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCourseFinishPriceText[4]);    //코인보상 받기

        QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AT_AsiaHard2Finish"), PlayerPrefs.GetInt("AT_AsiaHard2FinishAmount"), asiaMapCourseFinishObj[4],
            PlayerPrefs.GetString("AT_Asia Hard 2Course"), asiaMapCourseFinishSlider[4], asiaMapCourseFinishSliderText[4], asiaMapCourseFinishButton[4]);

        // 서버 저장 Hard 2
        ServerManager.Instance.Update_MapQuest("AsiaHard2Finish", asiaHard2_Count);
    }
    public void AsiaCourseHard3FinishButtonOn()
    {
        asiaHard3_Count = PlayerPrefs.GetInt("AT_AsiaHard3Finish");    //처음 불러오고
        asiaHard3_Count++;
        PlayerPrefs.SetInt("AT_AsiaHard3Finish", asiaHard3_Count); //처음 저장

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCourseFinishPriceText[5]);    //코인보상 받기

        QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AT_AsiaHard3Finish"), PlayerPrefs.GetInt("AT_AsiaHard3FinishAmount"), asiaMapCourseFinishObj[5],
            PlayerPrefs.GetString("AT_Asia Hard 3Course"), asiaMapCourseFinishSlider[5], asiaMapCourseFinishSliderText[5], asiaMapCourseFinishButton[5]);

        // 서버 저장 Hard 3
        ServerManager.Instance.Update_MapQuest("AsiaHard3Finish", asiaHard3_Count);
    }


    ////아시아맵 시간제한 코스 완주
    public void AsiaNormalCourseTimeLimitFinishButtonOn1()
    {
        Debug.Log("1___ 시간제한 " + PlayerPrefs.GetString("AT_PlayQuestState"));
        asiaNormalTimeLimit_Count1 = PlayerPrefs.GetInt("AT_AsiaNormalTimeLimitFinish1");
        asiaNormalTimeLimit_Count1++;
        PlayerPrefs.SetInt("AT_AsiaNormalTimeLimitFinish1", asiaNormalTimeLimit_Count1);

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapTimeLimitFinishPriceText[0]);    //코인보상 받기

        PlayerPrefs.SetString("AT_PlayQuestState", "No");  //보상받은 후 게임 플레이를 하지 않은 상태다.
        
        QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AT_AsiaNormalTimeLimitFinish1"), PlayerPrefs.GetString("AT_CurrPlayTime1"), asiaMapTimeLimitFinishTilteText[0], 
            asiaMapTimeLimitFinishSlider[0], asiaMapTimeLimitFinishSliderText[0], asiaMapTimeLimitFinishButton[0], asiaMapTimeLimitFinishObj[0], takeBtnSprite);

        
        // 시간제한1 코스 완료
        ServerManager.Instance.Update_MapQuest("AsiaNormalTimeLimitFinish1", asiaNormalTimeLimit_Count1);

        if(asiaNormalTimeLimit_Count1 <= 10)
            PlayerPrefs.SetString("AT_CurrPlayTime1", "F1234/99:99:99");
    }
    public void AsiaNormalCourseTimeLimitFinishButtonOn2()
    {
        //Debug.Log("2___ 시간제한 " + PlayerPrefs.GetString("AT_PlayQuestState"));
        asiaNormalTimeLimit_Count2 = PlayerPrefs.GetInt("AT_AsiaNormalTimeLimitFinish2");
        asiaNormalTimeLimit_Count2++;
        PlayerPrefs.SetInt("AT_AsiaNormalTimeLimitFinish2", asiaNormalTimeLimit_Count2);

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapTimeLimitFinishPriceText[1]);    //코인보상 받기

        PlayerPrefs.SetString("AT_PlayQuestState", "No");  //보상받은 후 게임 플레이를 하지 않은 상태다.
        
        QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AT_AsiaNormalTimeLimitFinish2"), PlayerPrefs.GetString("AT_CurrPlayTime2"), asiaMapTimeLimitFinishTilteText[1], 
            asiaMapTimeLimitFinishSlider[1], asiaMapTimeLimitFinishSliderText[1], asiaMapTimeLimitFinishButton[1], asiaMapTimeLimitFinishObj[1], takeBtnSprite);

        
        // 시간제한2 코스 완료
        ServerManager.Instance.Update_MapQuest("AsiaNormalTimeLimitFinish2", asiaNormalTimeLimit_Count2);

        if (asiaNormalTimeLimit_Count2 <= 10)
            PlayerPrefs.SetString("AT_CurrPlayTime2", "F1234/99:99:99");
    }
    public void AsiaNormalCourseTimeLimitFinishButtonOn3()
    {
        //Debug.Log("3___ 시간제한 " + PlayerPrefs.GetString("AT_PlayQuestState"));
        asiaNormalTimeLimit_Count3 = PlayerPrefs.GetInt("AT_AsiaNormalTimeLimitFinish3");
        asiaNormalTimeLimit_Count3++;
        PlayerPrefs.SetInt("AT_AsiaNormalTimeLimitFinish3", asiaNormalTimeLimit_Count3);

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapTimeLimitFinishPriceText[2]);    //코인보상 받기

        PlayerPrefs.SetString("AT_PlayQuestState", "No");  //보상받은 후 게임 플레이를 하지 않은 상태다.
        
        QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AT_AsiaNormalTimeLimitFinish3"), PlayerPrefs.GetString("AT_CurrPlayTime3"), asiaMapTimeLimitFinishTilteText[2], 
            asiaMapTimeLimitFinishSlider[2], asiaMapTimeLimitFinishSliderText[2], asiaMapTimeLimitFinishButton[2], asiaMapTimeLimitFinishObj[2], takeBtnSprite);

        
        // 시간제한3 코스 완료
        ServerManager.Instance.Update_MapQuest("AsiaNormalTimeLimitFinish3", asiaNormalTimeLimit_Count3);
        if (asiaNormalTimeLimit_Count1 <= 10)
            PlayerPrefs.SetString("AT_CurrPlayTime3", "F1234/99:99:99");
    }
    public void AsiaHardCourseTimeLimitFinishButtonOn1()
    {
        asiaHardTimeLimit_Count1 = PlayerPrefs.GetInt("AT_AsiaHardTimeLimitFinish1");
        asiaHardTimeLimit_Count1++;
        PlayerPrefs.SetInt("AT_AsiaHardTimeLimitFinish1", asiaHardTimeLimit_Count1);

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapTimeLimitFinishPriceText[3]);    //코인보상 받기

        PlayerPrefs.SetString("AT_PlayQuestState", "No");  //보상받은 후 게임 플레이를 하지 않은 상태다.
        
        QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AT_AsiaHardTimeLimitFinish1"), PlayerPrefs.GetString("AT_CurrPlayTime4"), asiaMapTimeLimitFinishTilteText[3], 
            asiaMapTimeLimitFinishSlider[3], asiaMapTimeLimitFinishSliderText[3], asiaMapTimeLimitFinishButton[3], asiaMapTimeLimitFinishObj[3], takeBtnSprite);
    
        // 시간제한 하드1 코스 완료
        ServerManager.Instance.Update_MapQuest("AsiaHardTimeLimitFinish1", asiaHardTimeLimit_Count1);
        if (asiaNormalTimeLimit_Count1 <= 10)
            PlayerPrefs.SetString("AT_CurrPlayTime4", "F1234/99:99:99");
    }
    public void AsiaHardCourseTimeLimitFinishButtonOn2()
    {
        asiaHardTimeLimit_Count2 = PlayerPrefs.GetInt("AT_AsiaHardTimeLimitFinish2");
        asiaHardTimeLimit_Count2++;
        PlayerPrefs.SetInt("AT_AsiaHardTimeLimitFinish2", asiaHardTimeLimit_Count2);

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapTimeLimitFinishPriceText[4]);    //코인보상 받기

        PlayerPrefs.SetString("AT_PlayQuestState", "No");  //보상받은 후 게임 플레이를 하지 않은 상태다.
        
        QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AT_AsiaHardTimeLimitFinish2"), PlayerPrefs.GetString("AT_CurrPlayTime5"), asiaMapTimeLimitFinishTilteText[4], 
            asiaMapTimeLimitFinishSlider[4], asiaMapTimeLimitFinishSliderText[4], asiaMapTimeLimitFinishButton[4], asiaMapTimeLimitFinishObj[4], takeBtnSprite);

        
        // 시간제한 하드2 코스 완료
        ServerManager.Instance.Update_MapQuest("AsiaHardTimeLimitFinish2", asiaHardTimeLimit_Count2);
        if (asiaNormalTimeLimit_Count1 <= 10)
            PlayerPrefs.SetString("AT_CurrPlayTime5", "F1234/99:99:99");
    }
    public void AsiaHardCourseTimeLimitFinishButtonOn3()
    {
        asiaHardTimeLimit_Count3 = PlayerPrefs.GetInt("AT_AsiaHardTimeLimitFinish3");
        asiaHardTimeLimit_Count3++;
        PlayerPrefs.SetInt("AT_AsiaHardTimeLimitFinish3", asiaHardTimeLimit_Count3);

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapTimeLimitFinishPriceText[5]);    //코인보상 받기

        PlayerPrefs.SetString("AT_PlayQuestState", "No");  //보상받은 후 게임 플레이를 하지 않은 상태다.
        
        QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AT_AsiaHardTimeLimitFinish3"), PlayerPrefs.GetString("AT_CurrPlayTime6"), asiaMapTimeLimitFinishTilteText[5], 
            asiaMapTimeLimitFinishSlider[5], asiaMapTimeLimitFinishSliderText[5], asiaMapTimeLimitFinishButton[5], asiaMapTimeLimitFinishObj[5], takeBtnSprite);

        // 시간제한 하드3 코스 완료
        ServerManager.Instance.Update_MapQuest("AsiaHardTimeLimitFinish3", asiaHardTimeLimit_Count3);
        if (asiaNormalTimeLimit_Count1 <= 10)
            PlayerPrefs.SetString("AT_CurrPlayTime6", "F1234/99:99:99");
    }


    ////아시아맵 커스텀 퀘스트 미션
    //맵 전체 완주 1, 10, 20번
    public void AsiaMapAllOneFinishButtonOn()
    {
        PlayerPrefs.SetString("AT_AllOneFinish", "MissionOk");
        asiaMapCustomQuestObj[0].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
        asiaMapCustomQuestObj[0].transform.GetChild(7).GetComponent<Button>().interactable = false;
        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCustomQuestCoinText[0]);

        ServerManager.Instance.Update_QuestReward("AllOneFinish", "MissionOk");
    }
    public void AsiaMapAllTenFinishButtonOn()   //전체 10번씩 완주
    {
        PlayerPrefs.SetString("AT_AllTenFinish", "MissionOk");
        asiaMapCustomQuestObj[1].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
        asiaMapCustomQuestObj[1].transform.GetChild(7).GetComponent<Button>().interactable = false;
        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCustomQuestCoinText[1]);

        ServerManager.Instance.Update_QuestReward("AllTenFinish", "MissionOk");
    }
    public void AsiaMapAllTwentyFinishButtonOn()    //전체 20번씩 완주
    {
        PlayerPrefs.SetString("AT_AllTwentyFinish", "MissionOk");
        asiaMapCustomQuestObj[2].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
        asiaMapCustomQuestObj[2].transform.GetChild(7).GetComponent<Button>().interactable = false;
        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCustomQuestCoinText[2]);

        ServerManager.Instance.Update_QuestReward("AllTwentyFinish", "MissionOk");
    }

    //총 거리 500, 1000, 1500km
    public void AsiaMap500kmPassButtonOn()
    {
        PlayerPrefs.SetString("AT_Distance500Km", "MissionOk");
        asiaMapCustomQuestObj[3].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
        asiaMapCustomQuestObj[3].transform.GetChild(7).GetComponent<Button>().interactable = false;
        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCustomQuestCoinText[3]);

        ServerManager.Instance.Update_QuestReward("Distance500Km", "MissionOk");
    }
    public void AsiaMap1000kmPassButtonOn()
    {
        PlayerPrefs.SetString("AT_Distance1000Km", "MissionOk");
        asiaMapCustomQuestObj[4].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
        asiaMapCustomQuestObj[4].transform.GetChild(7).GetComponent<Button>().interactable = false;
        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCustomQuestCoinText[4]);

        ServerManager.Instance.Update_QuestReward("Distance1000Km", "MissionOk");
    }
    public void AsiaMap1500kmPassButtonOn()
    {
        PlayerPrefs.SetString("AT_Distance1500Km", "MissionOk");
        asiaMapCustomQuestObj[5].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
        asiaMapCustomQuestObj[5].transform.GetChild(7).GetComponent<Button>().interactable = false;
        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCustomQuestCoinText[5]);

        ServerManager.Instance.Update_QuestReward("Distance1500Km", "MissionOk");
    }



    //백버튼 클릭 시 이벤트 - 로비 이동
    public void BackButtonOn()
    {
        SoundMaixerManager.instance.OutGameBGMPlay();
        SceneManager.LoadScene("Lobby");
    }

    //BGM 사운드
    public void BGM_SliderSound()
    {
        SoundMaixerManager.instance.AudioControl();
    }

    //효과음 사운드
    public void Effect_SliderSound()
    {
        SoundMaixerManager.instance.SFXAudioControl();
    }

}
