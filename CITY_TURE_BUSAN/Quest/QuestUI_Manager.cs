
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Localization;
using System;
using Cysharp.Threading.Tasks;

public class QuestUI_Manager : MonoBehaviour
{
    private BusanMapStringClass stringClass;
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

    //public Sprite takeBtnSprite;  //완료버튼 이미지
    public LocalizedSprite localizedTakeCompletedButtonSprite = new LocalizedSprite();

    public GameObject[] quest_Obj;  //칼로리&최고속도 

    int connectCount;

    //----------맵 퀘스트 변수
    [Header(">>아시아맵 코스완주 필요 변수")]
    //아시아맵 코스완주 필요 변수
    public GameObject[] asiaMapCourseFinishObj;
    public Text[] busanMapCompleteText;
    public Slider[] asiaMapCourseFinishSlider;
    public Text[] asiaMapCourseFinishSliderText;
    public Button[] asiaMapCourseFinishButton;
    public Text[] asiaMapCourseFinishPriceText;

    [Space(10)]
    [Header("Map Quest Course Complete & Total Distance")]
    public LocalizedString mapQuest_MorningNormal_Complete;
    public LocalizedString mapQuest_NightNormal_Complete;
    public LocalizedString mapQuest_MorningHard_Complete;
    public LocalizedString mapQuest_NightHard_Complete;
    [Space(20)]

    [Header(">>아시아맵 코스제한시간 완주 필요 변수")]
    //아시아맵 코스제한시간 완주 필요 변수
    public GameObject[] asiaMapTimeLimitFinishObj;
    public Text[] asiaMapTimeLimitFinishTitleText;

    [Space(10)]
    [Header("Map Quest Time Limit")]
    public LocalizedString mapQuestNormal_1_Limit;
    public LocalizedString mapQuestNormal_2_Limit;
    public LocalizedString mapQuestNormal_3_Limit;
    public LocalizedString mapQuestNormal_4_Limit;
    [Space(20)]

    public Slider[] asiaMapTimeLimitFinishSlider;
    public Text[] asiaMapTimeLimitFinishSliderText;
    public Button[] asiaMapTimeLimitFinishButton;
    public Text[] asiaMapTimeLimitFinishPriceText;
    [Header(">>아시아맵 커스텀 미션 필요 변수")]
    //아시아맵 커스텀 미션 필요 변수
    public GameObject[] asiaMapCustomQuestObj;
    public Text[] basanMapCompleteAndDistanceTitleText;
    public Slider[] asiaMapCustomQuestSlider;
    public Text[] asiaMapCustomQuestSliderText;
    public Text[] asiaMapCustomQuestCoinText;

    [Space(10)]
    [Header("Map Quest Course Complete & Total Distance")]
    public LocalizedString mapQuest_1_CompleteAndTotalDistance;
    public LocalizedString mapQuest_2_CompleteAndTotalDistance;
    public LocalizedString mapQuest_3_CompleteAndTotalDistance;
    public LocalizedString mapQuest_4_CompleteAndTotalDistance;
    public LocalizedString mapQuest_5_CompleteAndTotalDistance;
    public LocalizedString mapQuest_6_CompleteAndTotalDistance;
    [Space(20)]

    //아시아맵 완주 변수
    int asiaNormal1_Count, asiaNormal2_Count, asiaNormal3_Count, asiaHard1_Count, asiaHard2_Count, asiaHard3_Count;
    //아시아맵 시간제한 완주변수
    int asiaNormalTimeLimit_Count1, asiaNormalTimeLimit_Count2, asiaNormalTimeLimit_Count3, asiaHardTimeLimit_Count1, asiaHardTimeLimit_Count2, asiaHardTimeLimit_Count3;

    public Toggle questColumnToggleGroupCurrentSeletion
    {
        get { return questColumnToggleGroup.ActiveToggles().FirstOrDefault(); }
    }
    private void Awake()
    {
        stringClass = new BusanMapStringClass();
    }
    private void OnEnable()
    {

        #region MapQuest each Times Complete And Distance
        mapQuest_1_CompleteAndTotalDistance.StringChanged += MapQuest_1_StringChanged;
        mapQuest_2_CompleteAndTotalDistance.StringChanged += MapQuest_2_StringChanged;
        mapQuest_3_CompleteAndTotalDistance.StringChanged += MapQuest_3_StringChanged;
        mapQuest_4_CompleteAndTotalDistance.StringChanged += MapQuest_4_StringChanged;
        mapQuest_5_CompleteAndTotalDistance.StringChanged += MapQuest_5_StringChanged;
        mapQuest_6_CompleteAndTotalDistance.StringChanged += MapQuest_6_StringChanged;
        #endregion

        #region TimeLimit Mission
        mapQuestNormal_1_Limit.StringChanged += MapQuestNormal_1_Limit_StringChanged;
        mapQuestNormal_2_Limit.StringChanged += MapQuestNormal_2_Limit_StringChanged;
        mapQuestNormal_3_Limit.StringChanged += MapQuestNormal_3_Limit_StringChanged;
        mapQuestNormal_4_Limit.StringChanged += MapQuestNormal_4_Limit_StringChanged;
        #endregion

        #region MapQuest Mode Complete
        mapQuest_MorningNormal_Complete.StringChanged += MapQuest_MorningNormal_Complete_StringChanged;
        mapQuest_NightNormal_Complete.StringChanged += MapQuest_NightNormal_Complete_StringChanged;
        mapQuest_MorningHard_Complete.StringChanged += MapQuest_MorningHard_Complete_StringChanged;
        mapQuest_NightHard_Complete.StringChanged += MapQuest_NightHard_Complete_StringChanged;
        #endregion
    }

    private void MapQuest_NightHard_Complete_StringChanged(string value)
    {
        busanMapCompleteText[3].text = value;
    }

    private void MapQuest_MorningHard_Complete_StringChanged(string value)
    {
        busanMapCompleteText[2].text = value;
    }

    private void MapQuest_NightNormal_Complete_StringChanged(string value)
    {
        busanMapCompleteText[1].text = value;
    }

    private void MapQuest_MorningNormal_Complete_StringChanged(string value)
    {
        busanMapCompleteText[0].text = value;
    }

    private void MapQuest_6_StringChanged(string value)
    {
        basanMapCompleteAndDistanceTitleText[5].text = value;
    }

    private void MapQuest_5_StringChanged(string value)
    {
        basanMapCompleteAndDistanceTitleText[4].text = value;
    }

    private void MapQuest_4_StringChanged(string value)
    {
        basanMapCompleteAndDistanceTitleText[3].text = value;
    }

    private void MapQuest_3_StringChanged(string value)
    {
        basanMapCompleteAndDistanceTitleText[2].text = value;
    }

    private void MapQuest_2_StringChanged(string value)
    {
        basanMapCompleteAndDistanceTitleText[1].text = value;
    }

    private void MapQuest_1_StringChanged(string value)
    {
        Debug.Log("MapQuest_1_Course_Completed_StringChanged : " + value);
        basanMapCompleteAndDistanceTitleText[0].text = value;
    }

    private void OnDisable()
    {

        #region MapQuest Course Completed
        mapQuest_1_CompleteAndTotalDistance.StringChanged -= MapQuest_1_StringChanged;
        mapQuest_2_CompleteAndTotalDistance.StringChanged -= MapQuest_2_StringChanged;
        mapQuest_3_CompleteAndTotalDistance.StringChanged -= MapQuest_3_StringChanged;
        mapQuest_4_CompleteAndTotalDistance.StringChanged -= MapQuest_4_StringChanged;
        mapQuest_5_CompleteAndTotalDistance.StringChanged -= MapQuest_5_StringChanged;
        mapQuest_6_CompleteAndTotalDistance.StringChanged -= MapQuest_6_StringChanged;
        #endregion

        #region TimeLimit Mission
        mapQuestNormal_1_Limit.StringChanged -= MapQuestNormal_1_Limit_StringChanged;
        mapQuestNormal_2_Limit.StringChanged -= MapQuestNormal_2_Limit_StringChanged;
        mapQuestNormal_3_Limit.StringChanged -= MapQuestNormal_3_Limit_StringChanged;
        mapQuestNormal_4_Limit.StringChanged -= MapQuestNormal_4_Limit_StringChanged;
        #endregion

        #region MapQuest Mode Complete
        mapQuest_MorningNormal_Complete.StringChanged -= MapQuest_MorningNormal_Complete_StringChanged;
        mapQuest_NightNormal_Complete.StringChanged -= MapQuest_NightNormal_Complete_StringChanged;
        mapQuest_MorningHard_Complete.StringChanged -= MapQuest_MorningHard_Complete_StringChanged;
        mapQuest_NightHard_Complete.StringChanged -= MapQuest_NightHard_Complete_StringChanged;
        #endregion
    }
    private void MapQuestNormal_4_Limit_StringChanged(string value)
    {
        asiaMapTimeLimitFinishTitleText[3].text = value;
    }
    private void MapQuestNormal_3_Limit_StringChanged(string value)
    {
        asiaMapTimeLimitFinishTitleText[2].text = value;
    }
    private void MapQuestNormal_2_Limit_StringChanged(string value)
    {
        asiaMapTimeLimitFinishTitleText[1].text = value;
    }
    private void MapQuestNormal_1_Limit_StringChanged(string value)
    {
        asiaMapTimeLimitFinishTitleText[0].text = value;
    }

    void Start()
    {
        expText.text = PlayerPrefs.GetInt("Busan_Player_CurrExp").ToString();
        coinText.text = QuestData_Manager.instance.CommaText(PlayerPrefs.GetInt("Busan_Player_Gold"));
        QuestTogglePanelAction(true, false);    //처음 일일퀘스트 판넬 활성화 초기화
        ConnectDay();
    }
    public void QuestColumnToggleGroup()
    {
        if (questColumnToggleGroup.ActiveToggles().Any())
        {
            if (questColumnToggleGroupCurrentSeletion.name == "TodayToggle")
            {
                QuestTogglePanelAction(true, false);
            }
            else if (questColumnToggleGroupCurrentSeletion.name == "MapToggle")
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
        oneCennect = PlayerPrefs.GetString("Busan_OneCennectDay");

        //접속했다는 기록이 없으면 처음 로그인
        if (oneCennect == "")
        {
            PlayerPrefs.SetString("Busan_OneCennectDay", QuestData_Manager.instance.toDayArr[1] + QuestData_Manager.instance.toDayArr[2]);
            oneCennect = PlayerPrefs.GetString("Busan_OneCennectDay");
        }
        else if (oneCennect != "")
        {
            PlayerPrefs.SetString("Busan_TwoCennectDay", QuestData_Manager.instance.toDayArr[1] + QuestData_Manager.instance.toDayArr[2]);
            twoCennect = PlayerPrefs.GetString("Busan_TwoCennectDay");
        }

        //처음 접속 날짜와 두번째 접속 날짜가 다를 경우 - 출첵
        if (oneCennect != twoCennect)
        {
            Debug.Log("------------아--------------------------");
            //퀘스트 미션 초기화
            //퀘스트1 고정 - 접속하기
            PlayerPrefs.SetInt("Busan_TodayQuest1", 0);   //퀘스트 초기화 - 오늘 처음 접속했음
            ServerManager.Instance.Update_TodayQuest("TodayQuest1", "No", 0);
            QuestData_Manager.instance.Quest_ConnectMission(quest_titleText[0], quest_sliderText[0], quest_slider[0], quest_coinText[0], quest_completeImg[0], quest_takeBtn[0], quest_tekeImg[0]).Forget();

            //퀘스트2 고정랜덤 - 방문하기
            PlayerPrefs.SetInt("Busan_TodayQuest2", QuestData_Manager.instance.VisitRandom());
            QuestData_Manager.instance.Quest_VisitMission(quest_titleText[1], quest_sliderText[1], quest_slider[1], quest_coinText[1], quest_completeImg[1], quest_takeBtn[1], quest_tekeImg[1]).Forget();

            //퀘스트3 고정랜덤 - 게임활용 미션
            PlayerPrefs.SetInt("Busan_TodayQuest3", QuestData_Manager.instance.GameUseRandom());
            QuestData_Manager.instance.Quest_GameUseMission(quest_titleText[2], quest_sliderText[2], quest_slider[2], quest_coinText[2], quest_completeImg[2], quest_takeBtn[2], quest_tekeImg[2]).Forget();

            //퀘스트4 고정랜덤 - 오늘 칼로리 소모
            PlayerPrefs.SetInt("Busan_TodayQuest4", QuestData_Manager.instance.BurnUp_CalroieToday());
            QuestData_Manager.instance.Quest_BurnUpCalroieTodayMission(quest_Obj[0], quest_titleText[3], quest_slider[3], quest_sliderText[3]).Forget();

            //퀘스트5 고정랜덤 - 오늘 최대 속도
            PlayerPrefs.SetInt("Busan_TodayQuest5", QuestData_Manager.instance.MaxSpeedToday());
            QuestData_Manager.instance.Quest_MaxSpeedTodayMission(quest_Obj[1], quest_titleText[4], quest_slider[4], quest_sliderText[4]).Forget();


            //맵퀘스트 - 커스텀 보상 퀘스트
            //아시아맵 한번씩 완주
            QuestData_Manager.instance.AsiaMap_AllOneFinish(asiaMapCustomQuestObj[0], asiaMapCustomQuestSlider[0], asiaMapCustomQuestSliderText[0]).Forget();

            //아시아맵 10번씩 완주
            QuestData_Manager.instance.AsiaMap_AllTenFinish(asiaMapCustomQuestObj[1], asiaMapCustomQuestSlider[1], asiaMapCustomQuestSliderText[1]).Forget();

            //아시아앱 20번씩 완주
            QuestData_Manager.instance.AsiaMap_AllTwentyFinish(asiaMapCustomQuestObj[2], asiaMapCustomQuestSlider[2], asiaMapCustomQuestSliderText[2]).Forget();

            //아시압맵 500km
            QuestData_Manager.instance.AsiaMap500KmPass(asiaMapCustomQuestObj[3], asiaMapCustomQuestSlider[3], asiaMapCustomQuestSliderText[3]).Forget();

            //아시압맵 500km
            QuestData_Manager.instance.AsiaMap1000KmPass(asiaMapCustomQuestObj[4], asiaMapCustomQuestSlider[4], asiaMapCustomQuestSliderText[4]).Forget();

            //아시압맵 500km
            QuestData_Manager.instance.AsiaMap1500KmPass(asiaMapCustomQuestObj[5], asiaMapCustomQuestSlider[5], asiaMapCustomQuestSliderText[5]).Forget();

            //맵 퀘스트 - 완주하기
            if (PlayerPrefs.GetString("Busan_CurrentMap_Name").Equals(stringClass.BUSAN_RED_LINE))
            {
                QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaNormal1Finish"), PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount"), asiaMapCourseFinishObj[0],
                PlayerPrefs.GetString("Busan_Asia Normal 1Course"), asiaMapCourseFinishSlider[0], asiaMapCourseFinishSliderText[0], asiaMapCourseFinishButton[0]);
                QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaNormal2Finish"), PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount"), asiaMapCourseFinishObj[1],
                    PlayerPrefs.GetString("Busan_Asia Normal 2Course"), asiaMapCourseFinishSlider[1], asiaMapCourseFinishSliderText[1], asiaMapCourseFinishButton[1]);
                QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaHard1Finish"), PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount"), asiaMapCourseFinishObj[2],
                    PlayerPrefs.GetString("Busan_Asia Hard 1Course"), asiaMapCourseFinishSlider[2], asiaMapCourseFinishSliderText[2], asiaMapCourseFinishButton[2]);
                QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaHard2Finish"), PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount"), asiaMapCourseFinishObj[3],
                    PlayerPrefs.GetString("Busan_Asia Hard 2Course"), asiaMapCourseFinishSlider[3], asiaMapCourseFinishSliderText[3], asiaMapCourseFinishButton[3]);

                //맵 퀘스트 - 시간제한 완주하기
                QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaNormalTimeLimitFinish1"), PlayerPrefs.GetString("Busan_CurrPlayTime1"), mapQuestNormal_1_Limit,
                    asiaMapTimeLimitFinishSlider[0], asiaMapTimeLimitFinishSliderText[0], asiaMapTimeLimitFinishButton[0], asiaMapTimeLimitFinishObj[0]).Forget();
                QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaNormalTimeLimitFinish2"), PlayerPrefs.GetString("Busan_CurrPlayTime2"), mapQuestNormal_2_Limit,
                    asiaMapTimeLimitFinishSlider[1], asiaMapTimeLimitFinishSliderText[1], asiaMapTimeLimitFinishButton[1], asiaMapTimeLimitFinishObj[1]).Forget();
                QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaHardTimeLimitFinish1"), PlayerPrefs.GetString("Busan_CurrPlayTime4"), mapQuestNormal_3_Limit,
                    asiaMapTimeLimitFinishSlider[2], asiaMapTimeLimitFinishSliderText[2], asiaMapTimeLimitFinishButton[2], asiaMapTimeLimitFinishObj[2]).Forget();
                QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaHardTimeLimitFinish2"), PlayerPrefs.GetString("Busan_CurrPlayTime5"), mapQuestNormal_4_Limit,
                    asiaMapTimeLimitFinishSlider[3], asiaMapTimeLimitFinishSliderText[3], asiaMapTimeLimitFinishButton[3], asiaMapTimeLimitFinishObj[3]).Forget();

            }
            else if (PlayerPrefs.GetString("Busan_CurrentMap_Name").Equals(stringClass.BUSAN_GREEN_LINE))
            {
                QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_GreenNormal1Finish"), PlayerPrefs.GetInt("Busan_GreenNormal1FinishAmount"), asiaMapCourseFinishObj[0],
            PlayerPrefs.GetString("Busan_Green Normal 1Course"), asiaMapCourseFinishSlider[0], asiaMapCourseFinishSliderText[0], asiaMapCourseFinishButton[0]);
                QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_GreenNormal2Finish"), PlayerPrefs.GetInt("Busan_GreenaNormal2FinishAmount"), asiaMapCourseFinishObj[1],
                    PlayerPrefs.GetString("Busan_Green Normal 2Course"), asiaMapCourseFinishSlider[1], asiaMapCourseFinishSliderText[1], asiaMapCourseFinishButton[1]);
                QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_GreenHard1Finish"), PlayerPrefs.GetInt("Busan_GreenHard1FinishAmount"), asiaMapCourseFinishObj[2],
                    PlayerPrefs.GetString("Busan_Green Hard 1Course"), asiaMapCourseFinishSlider[2], asiaMapCourseFinishSliderText[2], asiaMapCourseFinishButton[2]);
                QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_GreenHard2Finish"), PlayerPrefs.GetInt("Busan_GreenHard2FinishAmount"), asiaMapCourseFinishObj[3],
                    PlayerPrefs.GetString("Busan_Green Hard 2Course"), asiaMapCourseFinishSlider[3], asiaMapCourseFinishSliderText[3], asiaMapCourseFinishButton[3]);

                //맵 퀘스트 - 시간제한 완주하기
                QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_GreenNormalTimeLimitFinish1"), PlayerPrefs.GetString("Busan_CurrPlayTime1"), mapQuestNormal_1_Limit,
                    asiaMapTimeLimitFinishSlider[0], asiaMapTimeLimitFinishSliderText[0], asiaMapTimeLimitFinishButton[0], asiaMapTimeLimitFinishObj[0]).Forget();
                QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_GreenNormalTimeLimitFinish2"), PlayerPrefs.GetString("Busan_CurrPlayTime2"), mapQuestNormal_2_Limit,
                    asiaMapTimeLimitFinishSlider[1], asiaMapTimeLimitFinishSliderText[1], asiaMapTimeLimitFinishButton[1], asiaMapTimeLimitFinishObj[1]).Forget();
                QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_GreenHardTimeLimitFinish1"), PlayerPrefs.GetString("Busan_CurrPlayTime4"), mapQuestNormal_3_Limit,
                    asiaMapTimeLimitFinishSlider[2], asiaMapTimeLimitFinishSliderText[2], asiaMapTimeLimitFinishButton[2], asiaMapTimeLimitFinishObj[2]).Forget();
                QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_GreenHardTimeLimitFinish2"), PlayerPrefs.GetString("Busan_CurrPlayTime5"), mapQuestNormal_4_Limit,
                    asiaMapTimeLimitFinishSlider[3], asiaMapTimeLimitFinishSliderText[3], asiaMapTimeLimitFinishButton[3], asiaMapTimeLimitFinishObj[3]).Forget();

            }

            PlayerPrefs.SetString("Busan_TwoCennectDay", QuestData_Manager.instance.toDayArr[1] + QuestData_Manager.instance.toDayArr[2]);
            twoCennect = PlayerPrefs.GetString("Busan_TwoCennectDay");

            //두번째 접속한 날짜를 앞으로 옮겨준다. 그래야 다음 들어왔을 때 또 비교가능
            PlayerPrefs.SetString("Busan_OneCennectDay", PlayerPrefs.GetString("Busan_TwoCennectDay"));

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
            QuestData_Manager.instance.Quest_ConnectMission(quest_titleText[0], quest_sliderText[0], quest_slider[0], quest_coinText[0], quest_completeImg[0], quest_takeBtn[0], quest_tekeImg[0]).Forget();

            //퀘스트2 고정랜덤 - 방문하기
            QuestData_Manager.instance.Quest_VisitMission(quest_titleText[1], quest_sliderText[1], quest_slider[1], quest_coinText[1], quest_completeImg[1], quest_takeBtn[1], quest_tekeImg[1]).Forget();

            //퀘스트3 고정랜덤 - 게임활용 미션
            QuestData_Manager.instance.Quest_GameUseMission(quest_titleText[2], quest_sliderText[2], quest_slider[2], quest_coinText[2], quest_completeImg[2], quest_takeBtn[2], quest_tekeImg[2]).Forget();

            //퀘스트4 고정랜덤 - 오늘 칼로리 소모
            QuestData_Manager.instance.Quest_BurnUpCalroieTodayMission(quest_Obj[0], quest_titleText[3], quest_slider[3], quest_sliderText[3]).Forget();

            //퀘스트5 고정랜덤 - 오늘 최대 속도
            QuestData_Manager.instance.Quest_MaxSpeedTodayMission(quest_Obj[1], quest_titleText[4], quest_slider[4], quest_sliderText[4]).Forget();


            //맵퀘스트 - 커스텀 보상 퀘스트
            //아시아맵 한번씩 완주
            QuestData_Manager.instance.AsiaMap_AllOneFinish(asiaMapCustomQuestObj[0], asiaMapCustomQuestSlider[0], asiaMapCustomQuestSliderText[0]).Forget();

            //아시아맵 10번씩 완주
            QuestData_Manager.instance.AsiaMap_AllTenFinish(asiaMapCustomQuestObj[1], asiaMapCustomQuestSlider[1], asiaMapCustomQuestSliderText[1]).Forget();

            //아시아앱 20번씩 완주
            QuestData_Manager.instance.AsiaMap_AllTwentyFinish(asiaMapCustomQuestObj[2], asiaMapCustomQuestSlider[2], asiaMapCustomQuestSliderText[2]).Forget();

            //아시압맵 500km
            QuestData_Manager.instance.AsiaMap500KmPass(asiaMapCustomQuestObj[3], asiaMapCustomQuestSlider[3], asiaMapCustomQuestSliderText[3]).Forget();

            //아시압맵 500km
            QuestData_Manager.instance.AsiaMap1000KmPass(asiaMapCustomQuestObj[4], asiaMapCustomQuestSlider[4], asiaMapCustomQuestSliderText[4]).Forget();

            //아시압맵 500km
            QuestData_Manager.instance.AsiaMap1500KmPass(asiaMapCustomQuestObj[5], asiaMapCustomQuestSlider[5], asiaMapCustomQuestSliderText[5]).Forget();

            //맵 퀘스트 - 완주하기
            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaNormal1Finish"), PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount"), asiaMapCourseFinishObj[0],
                PlayerPrefs.GetString("Busan_Asia Normal 1Course"), asiaMapCourseFinishSlider[0], asiaMapCourseFinishSliderText[0], asiaMapCourseFinishButton[0]);
            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaNormal2Finish"), PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount"), asiaMapCourseFinishObj[1],
                PlayerPrefs.GetString("Busan_Asia Normal 2Course"), asiaMapCourseFinishSlider[1], asiaMapCourseFinishSliderText[1], asiaMapCourseFinishButton[1]);
            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaHard1Finish"), PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount"), asiaMapCourseFinishObj[2],
                PlayerPrefs.GetString("Busan_Asia Hard 1Course"), asiaMapCourseFinishSlider[2], asiaMapCourseFinishSliderText[2], asiaMapCourseFinishButton[2]);
            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaHard2Finish"), PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount"), asiaMapCourseFinishObj[3],
                PlayerPrefs.GetString("Busan_Asia Hard 2Course"), asiaMapCourseFinishSlider[3], asiaMapCourseFinishSliderText[3], asiaMapCourseFinishButton[3]);

            //맵 퀘스트 - 시간제한 완주하기
            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaNormalTimeLimitFinish1"), PlayerPrefs.GetString("Busan_CurrPlayTime1"), mapQuestNormal_1_Limit,
                asiaMapTimeLimitFinishSlider[0], asiaMapTimeLimitFinishSliderText[0], asiaMapTimeLimitFinishButton[0], asiaMapTimeLimitFinishObj[0]).Forget();
            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaNormalTimeLimitFinish2"), PlayerPrefs.GetString("Busan_CurrPlayTime2"), mapQuestNormal_2_Limit,
                asiaMapTimeLimitFinishSlider[1], asiaMapTimeLimitFinishSliderText[1], asiaMapTimeLimitFinishButton[1], asiaMapTimeLimitFinishObj[1]).Forget();
            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaHardTimeLimitFinish1"), PlayerPrefs.GetString("Busan_CurrPlayTime4"), mapQuestNormal_3_Limit,
                asiaMapTimeLimitFinishSlider[2], asiaMapTimeLimitFinishSliderText[2], asiaMapTimeLimitFinishButton[2], asiaMapTimeLimitFinishObj[2]).Forget();
            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaHardTimeLimitFinish2"), PlayerPrefs.GetString("Busan_CurrPlayTime5"), mapQuestNormal_4_Limit,
                asiaMapTimeLimitFinishSlider[3], asiaMapTimeLimitFinishSliderText[3], asiaMapTimeLimitFinishButton[3], asiaMapTimeLimitFinishObj[3]).Forget();


        }
    }

    //퀘스트 1 보상받기 버튼
    public void Quest_ConnectButtonOn()
    {
        connectCount = PlayerPrefs.GetInt("Busan_TodayQuest1");
        connectCount++;
        PlayerPrefs.SetInt("Busan_TodayQuest1", connectCount);

        QuestData_Manager.instance.GetCoinReward(coinText, quest_coinText[0]);

        // 서버에 반영
        ServerManager.Instance.Update_TodayQuest("TodayQuest1", "None", connectCount);

        // 현재 접속시간 서버에 반영
        ServerManager.Instance.UpdateConnectedState("ConnectTime", PlayerPrefs.GetString("Busan_ConnectTime"));
        ServerManager.Instance.connectedStateCheck.connectTime = PlayerPrefs.GetString("Busan_ConnectTime");

        //퀘스트1 고정 - 접속하기
        QuestData_Manager.instance.Quest_ConnectMission(quest_titleText[0], quest_sliderText[0], quest_slider[0], quest_coinText[0], quest_completeImg[0], quest_takeBtn[0], quest_tekeImg[0]).Forget();
    }

    //퀘스트2 보상받기 버튼
    public async void Quest_VisitButtonOn()
    {
        quest_takeBtn[1].interactable = false;  //보상받았기 때문에 비활성화

        int visitNum = PlayerPrefs.GetInt("Busan_TodayQuest2");

        if (visitNum == 0)
        {
            PlayerPrefs.SetString("Busan_StoreVisit", "MissionOk");
        }
        else if (visitNum == 1)
        {
            PlayerPrefs.SetString("Busan_MyInfoVisit", "MissionOk");
        }
        else if (visitNum == 2)
        {
            PlayerPrefs.SetString("Busan_InventoryVisit", "MissionOk");
        }
        else if (visitNum == 3)
        {
            PlayerPrefs.SetString("Busan_RankJonVisit", "MissionOk");
        }

        ServerManager.Instance.Update_TodayQuest("TodayQuest2", "MissionOk");

        QuestData_Manager.instance.GetCoinReward(coinText, quest_coinText[1]);

        quest_takeBtn[1].GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);
        //quest_takeBtn[1].GetComponent<Image>().sprite = result;
        quest_tekeImg[1].SetActive(true);

    }

    //퀘스트3 보상받기 버튼
    public async void Quest_GameUseButtonOn()
    {
        //quest_takeBtn[2].interactable = false;  //보상받았기 때문에 비활성화
        int usetNum = PlayerPrefs.GetInt("Busan_TodayQuest3");

        if (usetNum == 0)
            PlayerPrefs.SetString("Busan_ProfileChange", "MissionOk");
        else if (usetNum == 1)
            PlayerPrefs.SetString("Busan_GameOnePlay", "MissionOk");
        else if (usetNum == 2)
            PlayerPrefs.SetString("Busan_RankUp", "MissionOk");
        else if (usetNum == 3)
            PlayerPrefs.SetString("Busan_GoldUse", "MissionOk");
        else if (usetNum == 4)
            PlayerPrefs.SetString("Busan_ItemUse", "MissionOk");
        else if (usetNum == 5)
            PlayerPrefs.SetString("Busan_ItemPurchase", "MissionOk");

        ServerManager.Instance.Update_TodayQuest("TodayQuest3", "MissionOk");

        QuestData_Manager.instance.GetCoinReward(coinText, quest_coinText[2]);

        quest_takeBtn[2].GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);

        quest_tekeImg[2].SetActive(true);
    }

    //퀘스트4 보상받기 버튼
    public async void Quest_KcalBurnUpButtonOn()
    {
        //quest_Obj[0].transform.GetChild(7).GetComponent<Button>().interactable = false;

        PlayerPrefs.SetString("Busan_KcalBurnUp", "MissionOk");

        ServerManager.Instance.Update_TodayQuest("TodayQuest4", "MissionOk");

        QuestData_Manager.instance.GetCoinReward(coinText, quest_coinText[3]);

        quest_Obj[0].transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);
        quest_Obj[0].transform.GetChild(1).gameObject.SetActive(true);  //받기완료 이미지 활성화
    }

    //퀘스트5 보상받기 버튼
    public async void Quest_MaxSpeedButtonOn()
    {
        //quest_Obj[1].transform.GetChild(7).GetComponent<Button>().interactable = false;
        PlayerPrefs.SetString("Busan_MaxSpeedToday", "MissionOk");

        ServerManager.Instance.Update_TodayQuest("TodayQuest5", "MissionOk");

        QuestData_Manager.instance.GetCoinReward(coinText, quest_coinText[4]);

        quest_Obj[1].transform.GetChild(7).GetComponent<Image>().sprite = await LanguageSelectorManager.instance.ImmediateGetSpriteLocale(localizedTakeCompletedButtonSprite);    //이미지 변경
        quest_Obj[1].transform.GetChild(1).gameObject.SetActive(true); //받기완료 이미지 활성화
    }

    ////아시아맵 코스 완주
    public void AsiaCourse1FinishButtonOn()
    {
        //Debug.Log("___ 제한속도 " + PlayerPrefs.GetInt("Busan_AsiaNormal1Finish"));
        asiaNormal1_Count = PlayerPrefs.GetInt("Busan_AsiaNormal1Finish");    //처음 불러오고
        asiaNormal1_Count++;
        PlayerPrefs.SetInt("Busan_AsiaNormal1Finish", asiaNormal1_Count); //처음 저장

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCourseFinishPriceText[0]);    //코인보상 받기

        QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaNormal1Finish"), PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount"), asiaMapCourseFinishObj[0],
            PlayerPrefs.GetString("Busan_Asia Normal 1Course"), asiaMapCourseFinishSlider[0], asiaMapCourseFinishSliderText[0], asiaMapCourseFinishButton[0]);

        // 서버에 상태 저장 PlayerPrefs.SetInt("AsiaNormal1Finish", asiaNormal1_Count);
        ServerManager.Instance.Update_MapQuest("AsiaNormal1Finish", asiaNormal1_Count);
    }
    public void AsiaCourse2FinishButtonOn()
    {
        asiaNormal2_Count = PlayerPrefs.GetInt("Busan_AsiaNormal2Finish");
        asiaNormal2_Count++;
        PlayerPrefs.SetInt("Busan_AsiaNormal2Finish", asiaNormal2_Count);

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCourseFinishPriceText[1]);    //코인보상 받기

        QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaNormal2Finish"), PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount"), asiaMapCourseFinishObj[1],
            PlayerPrefs.GetString("Busan_Asia Normal 2Course"), asiaMapCourseFinishSlider[1], asiaMapCourseFinishSliderText[1], asiaMapCourseFinishButton[1]);

        // 서버 저장 Normal 2
        ServerManager.Instance.Update_MapQuest("AsiaNormal2Finish", asiaNormal2_Count);
    }
    //public void AsiaCourse3FinishButtonOn()
    //{
    //    asiaNormal3_Count = PlayerPrefs.GetInt("AsiaNormal3Finish");
    //    asiaNormal3_Count++;
    //    PlayerPrefs.SetInt("AsiaNormal3Finish", asiaNormal3_Count);

    //    QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCourseFinishPriceText[2]);    //코인보상 받기

    //    QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AsiaNormal3Finish"), PlayerPrefs.GetInt("AsiaNormal3FinishAmount"), asiaMapCourseFinishObj[2],
    //        PlayerPrefs.GetString("Asia Normal 3Course"), asiaMapCourseFinishSlider[2], asiaMapCourseFinishSliderText[2], asiaMapCourseFinishButton[2]);

    //    // 서버 저장 Normal 3
    //    ServerManager.Instance.Update_MapQuest("AsiaNormal3Finish", asiaNormal3_Count);
    //}
    public void AsiaCourseHard1FinishButtonOn()
    {


        asiaHard1_Count = PlayerPrefs.GetInt("Busan_AsiaHard1Finish");
        asiaHard1_Count++;
        PlayerPrefs.SetInt("Busan_AsiaHard1Finish", asiaHard1_Count);

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCourseFinishPriceText[2]);    //코인보상 받기

        QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaHard1Finish"), PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount"), asiaMapCourseFinishObj[2],
            PlayerPrefs.GetString("Busan_Asia Hard 1Course"), asiaMapCourseFinishSlider[2], asiaMapCourseFinishSliderText[2], asiaMapCourseFinishButton[2]);

        // 서버 저장 Hard 1
        ServerManager.Instance.Update_MapQuest("AsiaHard1Finish", asiaHard1_Count);
    }
    public void AsiaCourseHard2FinishButtonOn()
    {
        asiaHard2_Count = PlayerPrefs.GetInt("Busan_AsiaHard2Finish");
        asiaHard2_Count++;
        PlayerPrefs.SetInt("Busan_AsiaHard2Finish", asiaHard2_Count);

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCourseFinishPriceText[3]);    //코인보상 받기

        QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaHard2Finish"), PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount"), asiaMapCourseFinishObj[3],
            PlayerPrefs.GetString("Busan_Asia Hard 2Course"), asiaMapCourseFinishSlider[3], asiaMapCourseFinishSliderText[3], asiaMapCourseFinishButton[3]);

        // 서버 저장 Hard 2
        ServerManager.Instance.Update_MapQuest("AsiaHard2Finish", asiaHard2_Count);
    }

    ////아시아맵 시간제한 코스 완주
    public void AsiaNormalCourseTimeLimitFinishButtonOn1()
    {
        //Debug.Log("___ 시간제한 " + PlayerPrefs.GetInt("Busan_AsiaNormalTimeLimitFinish1"));
        asiaNormalTimeLimit_Count1 = PlayerPrefs.GetInt("Busan_AsiaNormalTimeLimitFinish1");
        asiaNormalTimeLimit_Count1++;
        PlayerPrefs.SetInt("Busan_AsiaNormalTimeLimitFinish1", asiaNormalTimeLimit_Count1);

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapTimeLimitFinishPriceText[0]);    //코인보상 받기

        PlayerPrefs.SetString("Busan_PlayQuestState", "No");  //보상받은 후 게임 플레이를 하지 않은 상태다.
        QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaNormalTimeLimitFinish1"), PlayerPrefs.GetString("Busan_CurrPlayTime1"), mapQuestNormal_1_Limit,
            asiaMapTimeLimitFinishSlider[0], asiaMapTimeLimitFinishSliderText[0], asiaMapTimeLimitFinishButton[0], asiaMapTimeLimitFinishObj[0]).Forget();


        // 시간제한1 코스 완료
        ServerManager.Instance.Update_MapQuest("AsiaNormalTimeLimitFinish1", asiaNormalTimeLimit_Count1);

        if (asiaNormalTimeLimit_Count1 <= 10)
            PlayerPrefs.SetString("Busan_CurrPlayTime1", "F1234/99:99:99");
    }
    public void AsiaNormalCourseTimeLimitFinishButtonOn2()
    {
        asiaNormalTimeLimit_Count2 = PlayerPrefs.GetInt("Busan_AsiaNormalTimeLimitFinish2");
        asiaNormalTimeLimit_Count2++;
        PlayerPrefs.SetInt("Busan_AsiaNormalTimeLimitFinish2", asiaNormalTimeLimit_Count2);

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapTimeLimitFinishPriceText[1]);    //코인보상 받기

        PlayerPrefs.SetString("Busan_PlayQuestState", "No");  //보상받은 후 게임 플레이를 하지 않은 상태다.
        QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaNormalTimeLimitFinish2"), PlayerPrefs.GetString("Busan_CurrPlayTime2"), mapQuestNormal_2_Limit,
            asiaMapTimeLimitFinishSlider[1], asiaMapTimeLimitFinishSliderText[1], asiaMapTimeLimitFinishButton[1], asiaMapTimeLimitFinishObj[1]).Forget();


        // 시간제한2 코스 완료
        ServerManager.Instance.Update_MapQuest("AsiaNormalTimeLimitFinish2", asiaNormalTimeLimit_Count2);

        if (asiaNormalTimeLimit_Count1 <= 10)
            PlayerPrefs.SetString("Busan_CurrPlayTime2", "F1234/99:99:99");
    }
    public void AsiaHardCourseTimeLimitFinishButtonOn1()
    {
        asiaHardTimeLimit_Count1 = PlayerPrefs.GetInt("Busan_AsiaHardTimeLimitFinish1");
        asiaHardTimeLimit_Count1++;
        PlayerPrefs.SetInt("Busan_AsiaHardTimeLimitFinish1", asiaHardTimeLimit_Count1);

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapTimeLimitFinishPriceText[2]);    //코인보상 받기

        PlayerPrefs.SetString("Busan_PlayQuestState", "No");  //보상받은 후 게임 플레이를 하지 않은 상태다.
        QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaHardTimeLimitFinish1"), PlayerPrefs.GetString("Busan_CurrPlayTime4"), mapQuestNormal_3_Limit,
            asiaMapTimeLimitFinishSlider[2], asiaMapTimeLimitFinishSliderText[2], asiaMapTimeLimitFinishButton[2], asiaMapTimeLimitFinishObj[2]).Forget();

        // 시간제한 하드1 코스 완료
        ServerManager.Instance.Update_MapQuest("AsiaHardTimeLimitFinish1", asiaHardTimeLimit_Count1);
        if (asiaNormalTimeLimit_Count1 <= 10)
            PlayerPrefs.SetString("Busan_CurrPlayTime4", "F1234/99:99:99");
    }
    public void AsiaHardCourseTimeLimitFinishButtonOn2()
    {
        asiaHardTimeLimit_Count2 = PlayerPrefs.GetInt("Busan_AsiaHardTimeLimitFinish2");
        asiaHardTimeLimit_Count2++;
        PlayerPrefs.SetInt("Busan_AsiaHardTimeLimitFinish2", asiaHardTimeLimit_Count2);

        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapTimeLimitFinishPriceText[3]);    //코인보상 받기

        PlayerPrefs.SetString("Busan_PlayQuestState", "No");  //보상받은 후 게임 플레이를 하지 않은 상태다.
        QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaHardTimeLimitFinish2"), PlayerPrefs.GetString("Busan_CurrPlayTime5"), mapQuestNormal_4_Limit,
            asiaMapTimeLimitFinishSlider[3], asiaMapTimeLimitFinishSliderText[3], asiaMapTimeLimitFinishButton[3], asiaMapTimeLimitFinishObj[3]).Forget();


        // 시간제한 하드2 코스 완료
        ServerManager.Instance.Update_MapQuest("AsiaHardTimeLimitFinish2", asiaHardTimeLimit_Count2);
        if (asiaNormalTimeLimit_Count1 <= 10)
            PlayerPrefs.SetString("Busan_CurrPlayTime5", "F1234/99:99:99");
    }


    ////아시아맵 커스텀 퀘스트 미션
    //맵 전체 완주 1, 10, 20번
    public void AsiaMapAllOneFinishButtonOn()
    {
        PlayerPrefs.SetString("Busan_AllOneFinish", "MissionOk");
        asiaMapCustomQuestObj[0].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
        asiaMapCustomQuestObj[0].transform.GetChild(7).GetComponent<Button>().interactable = false;
        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCustomQuestCoinText[0]);

        ServerManager.Instance.Update_QuestReward("AllOneFinish", "MissionOk");
    }
    public void AsiaMapAllTenFinishButtonOn()   //전체 10번씩 완주
    {
        PlayerPrefs.SetString("Busan_AllTenFinish", "MissionOk");
        asiaMapCustomQuestObj[1].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
        asiaMapCustomQuestObj[1].transform.GetChild(7).GetComponent<Button>().interactable = false;
        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCustomQuestCoinText[1]);

        ServerManager.Instance.Update_QuestReward("AllTenFinish", "MissionOk");
    }
    public void AsiaMapAllTwentyFinishButtonOn()    //전체 20번씩 완주
    {
        PlayerPrefs.SetString("Busan_AllTwentyFinish", "MissionOk");
        asiaMapCustomQuestObj[2].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
        asiaMapCustomQuestObj[2].transform.GetChild(7).GetComponent<Button>().interactable = false;
        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCustomQuestCoinText[2]);

        ServerManager.Instance.Update_QuestReward("AllTwentyFinish", "MissionOk");
    }

    //총 거리 500, 1000, 1500km
    public void AsiaMap500kmPassButtonOn()
    {
        PlayerPrefs.SetString("Busan_Distance500Km", "MissionOk");
        asiaMapCustomQuestObj[3].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
        asiaMapCustomQuestObj[3].transform.GetChild(7).GetComponent<Button>().interactable = false;
        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCustomQuestCoinText[3]);

        ServerManager.Instance.Update_QuestReward("Distance500Km", "MissionOk");
    }
    public void AsiaMap1000kmPassButtonOn()
    {
        PlayerPrefs.SetString("Busan_Distance1000Km", "MissionOk");
        asiaMapCustomQuestObj[4].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
        asiaMapCustomQuestObj[4].transform.GetChild(7).GetComponent<Button>().interactable = false;
        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCustomQuestCoinText[4]);

        ServerManager.Instance.Update_QuestReward("Distance1000Km", "MissionOk");
    }
    public void AsiaMap1500kmPassButtonOn()
    {
        PlayerPrefs.SetString("Busan_Distance1500Km", "MissionOk");
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






//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;
//using System.Linq;

//public class QuestUI_Manager : MonoBehaviour
//{
//    public ToggleGroup questColumnToggleGroup;
//    public GameObject todayTogglePanel;
//    public GameObject mapTogglePanel;
//    public Text expText;
//    public Text coinText;


//    string oneCennect, twoCennect;  //처음접속, 두번째접속

//    //----------일일 퀘스트 변수
//    [Header(">>일일 퀘스트 변수")]
//    public Text[] quest_titleText;  //제목
//    public Slider[] quest_slider;   //갯수슬라이드
//    public Text[] quest_sliderText; //갯수텍스트
//    public Text[] quest_coinText;   //코인
//    public GameObject[] quest_completeImg;  //미션성공 도장 이미지
//    public Button[] quest_takeBtn;  //보상받기 버튼
//    public GameObject[] quest_tekeImg;  //받기완료 이미지
//    public Sprite takeBtnSprite;  //완료버튼 이미지
//    public GameObject[] quest_Obj;  //칼로리&최고속도 

//    int connectCount;

//    //----------맵 퀘스트 변수
//    [Header(">>아시아맵 코스완주 필요 변수")]
//    //아시아맵 코스완주 필요 변수
//    public GameObject[] asiaMapCourseFinishObj;
//    public Slider[] asiaMapCourseFinishSlider;
//    public Text[] asiaMapCourseFinishSliderText;
//    public Button[] asiaMapCourseFinishButton;
//    public Text[] asiaMapCourseFinishPriceText;
//    [Header(">>아시아맵 코스제한시간 완주 필요 변수")]
//    //아시아맵 코스제한시간 완주 필요 변수
//    public GameObject[] asiaMapTimeLimitFinishObj;
//    public Text[] asiaMapTimeLimitFinishTilteText;
//    public Slider[] asiaMapTimeLimitFinishSlider;
//    public Text[] asiaMapTimeLimitFinishSliderText;
//    public Button[] asiaMapTimeLimitFinishButton;
//    public Text[] asiaMapTimeLimitFinishPriceText;
//    [Header(">>아시아맵 커스텀 미션 필요 변수")]
//    //아시아맵 커스텀 미션 필요 변수
//    public GameObject[] asiaMapCustomQuestObj;
//    public Slider[] asiaMapCustomQuestSlider;
//    public Text[] asiaMapCustomQuestSliderText;
//    public Text[] asiaMapCustomQuestCoinText;


//    //아시아맵 완주 변수
//    int asiaNormal1_Count, asiaNormal2_Count, asiaNormal3_Count, asiaHard1_Count, asiaHard2_Count, asiaHard3_Count;
//    //아시아맵 시간제한 완주변수
//    int asiaNormalTimeLimit_Count1, asiaNormalTimeLimit_Count2, asiaNormalTimeLimit_Count3, asiaHardTimeLimit_Count1, asiaHardTimeLimit_Count2, asiaHardTimeLimit_Count3;



//    public Toggle questColumnToggleGroupCurrentSeletion
//    {
//        get { return questColumnToggleGroup.ActiveToggles().FirstOrDefault(); }
//    }



//    void Start()
//    {
//        //Debug.Log("---StoreVisit " + PlayerPrefs.GetString("StoreVisit"));
//        //Debug.Log("----MyInfoVisit " + PlayerPrefs.GetString("MyInfoVisit"));
//        //Debug.Log("---InventoryVisit " + PlayerPrefs.GetString("InventoryVisit"));
//        //Debug.Log("---RankJonVisit " + PlayerPrefs.GetString("RankJonVisit"));
//        //Debug.Log("---CustomChange " + PlayerPrefs.GetString("Busan_CustomChange"));

//        //Debug.Log("---GoldUse " + PlayerPrefs.GetString("Busan_GoldUse"));



//        expText.text = PlayerPrefs.GetInt("Busan_Player_CurrExp").ToString();
//        coinText.text = QuestData_Manager.instance.CommaText(PlayerPrefs.GetInt("Busan_Player_Gold"));
//        QuestTogglePanelAction(true, false);    //처음 일일퀘스트 판넬 활성화 초기화
//        ConnectDay();



//        //PlayerPrefs.SetInt("AsiaNormal1Finish", 0); 
//        //PlayerPrefs.SetInt("AsiaNormal2Finish", 0);
//        //PlayerPrefs.SetInt("AsiaNormal3Finish", 0);
//        //PlayerPrefs.SetInt("AsiaNormal1FinishAmount", 3);

//        //PlayerPrefs.SetString("Asia Normal 1Course", "Fise1235/10:00");
//        //PlayerPrefs.SetInt("AsiaNormalTimeLimitFinish1", 1);
//        //Debug.Log(PlayerPrefs.GetString("MyInfoVisit"));
//    }


//    public void QuestColumnToggleGroup()
//    {
//        if(questColumnToggleGroup.ActiveToggles().Any())
//        {
//            if(questColumnToggleGroupCurrentSeletion.name == "TodayToggle")
//            {
//                QuestTogglePanelAction(true, false);
//            }
//            else if(questColumnToggleGroupCurrentSeletion.name == "MapToggle")
//            {
//                QuestTogglePanelAction(false, true);
//            }
//        }
//    }
//    //퀘스트 선택 시 활성화/비활성화 판넬
//    void QuestTogglePanelAction(bool _today, bool _map)
//    {
//        todayTogglePanel.SetActive(_today);
//        mapTogglePanel.SetActive(_map);
//    }


//    //출석 체크 형 함수 - 오늘 접속했는지
//    public void ConnectDay()
//    {
//        //PlayerPrefs.SetString("OneCennectDay","");
//        //PlayerPrefs.SetString("TwoCennectDay","");
//        oneCennect = PlayerPrefs.GetString("Busan_OneCennectDay");

//        //접속했다는 기록이 없으면 처음 로그인
//        if (oneCennect == "")
//        {
//            PlayerPrefs.SetString("Busan_OneCennectDay", QuestData_Manager.instance.toDayArr[1] + QuestData_Manager.instance.toDayArr[2]);
//            oneCennect = PlayerPrefs.GetString("Busan_OneCennectDay");
//        }
//        else if (oneCennect != "")
//        {
//            PlayerPrefs.SetString("Busan_TwoCennectDay", QuestData_Manager.instance.toDayArr[1] + QuestData_Manager.instance.toDayArr[2]);
//            twoCennect = PlayerPrefs.GetString("Busan_TwoCennectDay");
//        }

//        //처음 접속 날짜와 두번째 접속 날짜가 다를 경우 - 출첵
//        if (oneCennect != twoCennect)
//        {
//            Debug.Log("------------아--------------------------");
//            //퀘스트 미션 초기화
//            //퀘스트1 고정 - 접속하기
//            PlayerPrefs.SetInt("Busan_TodayQuest1", 0);   //퀘스트 초기화 - 오늘 처음 접속했음
//            ServerManager.Instance.Update_TodayQuest("TodayQuest1", "No", 0);
//            QuestData_Manager.instance.Quest_ConnectMission(quest_titleText[0], quest_sliderText[0], quest_slider[0], quest_coinText[0], quest_completeImg[0], quest_takeBtn[0], quest_tekeImg[0], takeBtnSprite);

//            //퀘스트2 고정랜덤 - 방문하기
//            PlayerPrefs.SetInt("Busan_TodayQuest2", QuestData_Manager.instance.VisitRandom());
//            QuestData_Manager.instance.Quest_VisitMission(quest_titleText[1], quest_sliderText[1], quest_slider[1], quest_coinText[1], quest_completeImg[1], quest_takeBtn[1], quest_tekeImg[1], takeBtnSprite);

//            //퀘스트3 고정랜덤 - 게임활용 미션
//            PlayerPrefs.SetInt("Busan_TodayQuest3", QuestData_Manager.instance.GameUseRandom());
//            QuestData_Manager.instance.Quest_GameUseMission(quest_titleText[2], quest_sliderText[2], quest_slider[2], quest_coinText[2], quest_completeImg[2], quest_takeBtn[2], quest_tekeImg[2], takeBtnSprite);

//            //퀘스트4 고정랜덤 - 오늘 칼로리 소모
//            PlayerPrefs.SetInt("Busan_TodayQuest4", QuestData_Manager.instance.BurnUp_CalroieToday());
//            QuestData_Manager.instance.Quest_BurnUpCalroieTodayMission(quest_Obj[0], quest_titleText[3], quest_slider[3], quest_sliderText[3], takeBtnSprite);

//            //퀘스트5 고정랜덤 - 오늘 최대 속도
//            PlayerPrefs.SetInt("Busan_TodayQuest5", QuestData_Manager.instance.MaxSpeedToday());
//            QuestData_Manager.instance.Quest_MaxSpeedTodayMission(quest_Obj[1], quest_titleText[4], quest_slider[4], quest_sliderText[4], takeBtnSprite);


//            //맵퀘스트 - 커스텀 보상 퀘스트
//            //아시아맵 한번씩 완주
//            QuestData_Manager.instance.AsiaMap_AllOneFinish(asiaMapCustomQuestObj[0], asiaMapCustomQuestSlider[0], asiaMapCustomQuestSliderText[0]);

//            //아시아맵 10번씩 완주
//            QuestData_Manager.instance.AsiaMap_AllTenFinish(asiaMapCustomQuestObj[1], asiaMapCustomQuestSlider[1], asiaMapCustomQuestSliderText[1]);

//            //아시아앱 20번씩 완주
//            QuestData_Manager.instance.AsiaMap_AllTwentyFinish(asiaMapCustomQuestObj[2], asiaMapCustomQuestSlider[2], asiaMapCustomQuestSliderText[2]);

//            //아시압맵 500km
//            QuestData_Manager.instance.AsiaMap500KmPass(asiaMapCustomQuestObj[3], asiaMapCustomQuestSlider[3], asiaMapCustomQuestSliderText[3]);

//            //아시압맵 500km
//            QuestData_Manager.instance.AsiaMap1000KmPass(asiaMapCustomQuestObj[4], asiaMapCustomQuestSlider[4], asiaMapCustomQuestSliderText[4]);

//            //아시압맵 500km
//            QuestData_Manager.instance.AsiaMap1500KmPass(asiaMapCustomQuestObj[5], asiaMapCustomQuestSlider[5], asiaMapCustomQuestSliderText[5]);

//            //맵 퀘스트 - 완주하기
//            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaNormal1Finish"), PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount"), asiaMapCourseFinishObj[0], 
//                PlayerPrefs.GetString("Busan_Asia Normal 1Course"), asiaMapCourseFinishSlider[0], asiaMapCourseFinishSliderText[0], asiaMapCourseFinishButton[0]);
//            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaNormal2Finish"), PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount"), asiaMapCourseFinishObj[1],
//                PlayerPrefs.GetString("Busan_Asia Normal 2Course"), asiaMapCourseFinishSlider[1], asiaMapCourseFinishSliderText[1], asiaMapCourseFinishButton[1]);
//            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaHard1Finish"), PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount"), asiaMapCourseFinishObj[2],
//                PlayerPrefs.GetString("Busan_Asia Hard 1Course"), asiaMapCourseFinishSlider[2], asiaMapCourseFinishSliderText[2], asiaMapCourseFinishButton[2]);
//            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaHard2Finish"), PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount"), asiaMapCourseFinishObj[3],
//                PlayerPrefs.GetString("Busan_Asia Hard 2Course"), asiaMapCourseFinishSlider[3], asiaMapCourseFinishSliderText[3], asiaMapCourseFinishButton[3]);

//            //맵 퀘스트 - 시간제한 완주하기
//            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaNormalTimeLimitFinish1"), PlayerPrefs.GetString("Busan_CurrPlayTime1"), asiaMapTimeLimitFinishTilteText[0], 
//                asiaMapTimeLimitFinishSlider[0], asiaMapTimeLimitFinishSliderText[0], asiaMapTimeLimitFinishButton[0], asiaMapTimeLimitFinishObj[0], takeBtnSprite);
//            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaNormalTimeLimitFinish2"), PlayerPrefs.GetString("Busan_CurrPlayTime2"), asiaMapTimeLimitFinishTilteText[1], 
//                asiaMapTimeLimitFinishSlider[1], asiaMapTimeLimitFinishSliderText[1], asiaMapTimeLimitFinishButton[1], asiaMapTimeLimitFinishObj[1], takeBtnSprite);
//            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaHardTimeLimitFinish1"), PlayerPrefs.GetString("Busan_CurrPlayTime4"), asiaMapTimeLimitFinishTilteText[2], 
//                asiaMapTimeLimitFinishSlider[2], asiaMapTimeLimitFinishSliderText[2], asiaMapTimeLimitFinishButton[2], asiaMapTimeLimitFinishObj[2], takeBtnSprite);
//            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaHardTimeLimitFinish2"), PlayerPrefs.GetString("Busan_CurrPlayTime5"), asiaMapTimeLimitFinishTilteText[3], 
//                asiaMapTimeLimitFinishSlider[3], asiaMapTimeLimitFinishSliderText[3], asiaMapTimeLimitFinishButton[3], asiaMapTimeLimitFinishObj[3], takeBtnSprite);

//            PlayerPrefs.SetString("Busan_TwoCennectDay", QuestData_Manager.instance.toDayArr[1] + QuestData_Manager.instance.toDayArr[2]);
//            twoCennect = PlayerPrefs.GetString("Busan_TwoCennectDay");

//            //두번째 접속한 날짜를 앞으로 옮겨준다. 그래야 다음 들어왔을 때 또 비교가능
//            PlayerPrefs.SetString("Busan_OneCennectDay", PlayerPrefs.GetString("Busan_TwoCennectDay"));

//            //Debug.Log("1 : " + oneCennect + " :2: " + twoCennect);
//            ServerManager.Instance.UpdateConnectedState("TwoCennectDay", twoCennect);
//            ServerManager.Instance.connectedStateCheck.oneConnectDay = oneCennect;
//            ServerManager.Instance.connectedStateCheck.twoConnectDay = twoCennect;
//        }
//        else
//        {
//            //Debug.Log("여기 들어오게 됩니다.");

//            //Debug.Log("11 : " + oneCennect + " :2: " + twoCennect);
//            ServerManager.Instance.UpdateConnectedState("TwoCennectDay", twoCennect);
//            ServerManager.Instance.connectedStateCheck.oneConnectDay = oneCennect;
//            ServerManager.Instance.connectedStateCheck.twoConnectDay = twoCennect;

//            //퀘스트1 고정 - 접속하기
//            QuestData_Manager.instance.Quest_ConnectMission(quest_titleText[0], quest_sliderText[0], quest_slider[0], quest_coinText[0], quest_completeImg[0], quest_takeBtn[0], quest_tekeImg[0], takeBtnSprite);

//            //퀘스트2 고정랜덤 - 방문하기
//            QuestData_Manager.instance.Quest_VisitMission(quest_titleText[1], quest_sliderText[1], quest_slider[1], quest_coinText[1], quest_completeImg[1], quest_takeBtn[1], quest_tekeImg[1], takeBtnSprite);

//            //퀘스트3 고정랜덤 - 게임활용 미션
//            QuestData_Manager.instance.Quest_GameUseMission(quest_titleText[2], quest_sliderText[2], quest_slider[2], quest_coinText[2], quest_completeImg[2], quest_takeBtn[2], quest_tekeImg[2], takeBtnSprite);

//            //퀘스트4 고정랜덤 - 오늘 칼로리 소모
//            QuestData_Manager.instance.Quest_BurnUpCalroieTodayMission(quest_Obj[0], quest_titleText[3], quest_slider[3], quest_sliderText[3], takeBtnSprite);

//            //퀘스트5 고정랜덤 - 오늘 최대 속도
//            QuestData_Manager.instance.Quest_MaxSpeedTodayMission(quest_Obj[1], quest_titleText[4], quest_slider[4], quest_sliderText[4], takeBtnSprite);


//            //맵퀘스트 - 커스텀 보상 퀘스트
//            //아시아맵 한번씩 완주
//            QuestData_Manager.instance.AsiaMap_AllOneFinish(asiaMapCustomQuestObj[0], asiaMapCustomQuestSlider[0], asiaMapCustomQuestSliderText[0]);

//            //아시아맵 10번씩 완주
//            QuestData_Manager.instance.AsiaMap_AllTenFinish(asiaMapCustomQuestObj[1], asiaMapCustomQuestSlider[1], asiaMapCustomQuestSliderText[1]);

//            //아시아앱 20번씩 완주
//            QuestData_Manager.instance.AsiaMap_AllTwentyFinish(asiaMapCustomQuestObj[2], asiaMapCustomQuestSlider[2], asiaMapCustomQuestSliderText[2]);

//            //아시압맵 500km
//            QuestData_Manager.instance.AsiaMap500KmPass(asiaMapCustomQuestObj[3], asiaMapCustomQuestSlider[3], asiaMapCustomQuestSliderText[3]);

//            //아시압맵 500km
//            QuestData_Manager.instance.AsiaMap1000KmPass(asiaMapCustomQuestObj[4], asiaMapCustomQuestSlider[4], asiaMapCustomQuestSliderText[4]);

//            //아시압맵 500km
//            QuestData_Manager.instance.AsiaMap1500KmPass(asiaMapCustomQuestObj[5], asiaMapCustomQuestSlider[5], asiaMapCustomQuestSliderText[5]);

//            //맵 퀘스트 - 완주하기
//            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaNormal1Finish"), PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount"), asiaMapCourseFinishObj[0], 
//                PlayerPrefs.GetString("Busan_Asia Normal 1Course"), asiaMapCourseFinishSlider[0], asiaMapCourseFinishSliderText[0], asiaMapCourseFinishButton[0]);
//            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaNormal2Finish"), PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount"), asiaMapCourseFinishObj[1],
//                PlayerPrefs.GetString("Busan_Asia Normal 2Course"), asiaMapCourseFinishSlider[1], asiaMapCourseFinishSliderText[1], asiaMapCourseFinishButton[1]);
//            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaHard1Finish"), PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount"), asiaMapCourseFinishObj[2],
//                PlayerPrefs.GetString("Busan_Asia Hard 1Course"), asiaMapCourseFinishSlider[2], asiaMapCourseFinishSliderText[2], asiaMapCourseFinishButton[2]);
//            QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaHard2Finish"), PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount"), asiaMapCourseFinishObj[3],
//                PlayerPrefs.GetString("Busan_Asia Hard 2Course"), asiaMapCourseFinishSlider[3], asiaMapCourseFinishSliderText[3], asiaMapCourseFinishButton[3]);

//            //맵 퀘스트 - 시간제한 완주하기
//            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaNormalTimeLimitFinish1"), PlayerPrefs.GetString("Busan_CurrPlayTime1"), asiaMapTimeLimitFinishTilteText[0], 
//                asiaMapTimeLimitFinishSlider[0], asiaMapTimeLimitFinishSliderText[0], asiaMapTimeLimitFinishButton[0], asiaMapTimeLimitFinishObj[0], takeBtnSprite);
//            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaNormalTimeLimitFinish2"), PlayerPrefs.GetString("Busan_CurrPlayTime2"), asiaMapTimeLimitFinishTilteText[1], 
//                asiaMapTimeLimitFinishSlider[1], asiaMapTimeLimitFinishSliderText[1], asiaMapTimeLimitFinishButton[1], asiaMapTimeLimitFinishObj[1], takeBtnSprite);
//            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaHardTimeLimitFinish1"), PlayerPrefs.GetString("Busan_CurrPlayTime4"), asiaMapTimeLimitFinishTilteText[2], 
//                asiaMapTimeLimitFinishSlider[2], asiaMapTimeLimitFinishSliderText[2], asiaMapTimeLimitFinishButton[2], asiaMapTimeLimitFinishObj[2], takeBtnSprite);
//            QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaHardTimeLimitFinish2"), PlayerPrefs.GetString("Busan_CurrPlayTime5"), asiaMapTimeLimitFinishTilteText[3], 
//                asiaMapTimeLimitFinishSlider[3], asiaMapTimeLimitFinishSliderText[3], asiaMapTimeLimitFinishButton[3], asiaMapTimeLimitFinishObj[3], takeBtnSprite);
//        }
//    }

//    //퀘스트 1 보상받기 버튼
//    public void Quest_ConnectButtonOn()
//    {
//        connectCount = PlayerPrefs.GetInt("Busan_TodayQuest1");
//        connectCount++;
//        PlayerPrefs.SetInt("Busan_TodayQuest1", connectCount);

//        QuestData_Manager.instance.GetCoinReward(coinText, quest_coinText[0]);

//        // 서버에 반영
//        ServerManager.Instance.Update_TodayQuest("TodayQuest1", "None", connectCount);

//        // 현재 접속시간 서버에 반영
//        ServerManager.Instance.UpdateConnectedState("ConnectTime", PlayerPrefs.GetString("Busan_ConnectTime"));
//        ServerManager.Instance.connectedStateCheck.connectTime = PlayerPrefs.GetString("Busan_ConnectTime");

//        //퀘스트1 고정 - 접속하기
//        QuestData_Manager.instance.Quest_ConnectMission(quest_titleText[0], quest_sliderText[0], quest_slider[0], quest_coinText[0], quest_completeImg[0], quest_takeBtn[0], quest_tekeImg[0], takeBtnSprite);
//    }

//    //퀘스트2 보상받기 버튼
//    public void Quest_VisitButtonOn()
//    {
//        quest_takeBtn[1].interactable = false;  //보상받았기 때문에 비활성화

//        int visitNum = PlayerPrefs.GetInt("Busan_TodayQuest2");

//        if (visitNum == 0)
//        {
//            PlayerPrefs.SetString("Busan_StoreVisit", "MissionOk");
//        }
//        else if (visitNum == 1)
//        {
//            PlayerPrefs.SetString("Busan_MyInfoVisit", "MissionOk");
//        }
//        else if (visitNum == 2)
//        {
//            PlayerPrefs.SetString("Busan_InventoryVisit", "MissionOk");
//        }
//        else if(visitNum == 3)
//        {
//            PlayerPrefs.SetString("Busan_RankJonVisit", "MissionOk");
//        }

//        ServerManager.Instance.Update_TodayQuest("TodayQuest2", "MissionOk");

//        QuestData_Manager.instance.GetCoinReward(coinText, quest_coinText[1]);
//        quest_takeBtn[1].GetComponent<Image>().sprite = takeBtnSprite;
//        quest_tekeImg[1].SetActive(true);

//    }

//    //퀘스트3 보상받기 버튼
//    public void Quest_GameUseButtonOn()
//    {
//        //quest_takeBtn[2].interactable = false;  //보상받았기 때문에 비활성화
//        int usetNum = PlayerPrefs.GetInt("Busan_TodayQuest3");

//        if(usetNum == 0)
//            PlayerPrefs.SetString("Busan_ProfileChange", "MissionOk");
//        else if (usetNum == 1)
//            PlayerPrefs.SetString("Busan_GameOnePlay", "MissionOk");
//        else if (usetNum == 2)
//            PlayerPrefs.SetString("Busan_RankUp", "MissionOk");
//        else if (usetNum == 3)
//            PlayerPrefs.SetString("Busan_GoldUse", "MissionOk");
//        else if (usetNum == 4)
//            PlayerPrefs.SetString("Busan_ItemUse", "MissionOk");
//        else if (usetNum == 5)
//            PlayerPrefs.SetString("Busan_ItemPurchase", "MissionOk");

//        ServerManager.Instance.Update_TodayQuest("TodayQuest3", "MissionOk");

//        QuestData_Manager.instance.GetCoinReward(coinText, quest_coinText[2]);
//        quest_takeBtn[2].GetComponent<Image>().sprite = takeBtnSprite;
//        quest_tekeImg[2].SetActive(true);
//    }

//    public Image rewardBtn;

//    //퀘스트4 보상받기 버튼
//    public void Quest_KcalBurnUpButtonOn()
//    {
//        //quest_Obj[0].transform.GetChild(7).GetComponent<Button>().interactable = false;

//        PlayerPrefs.SetString("Busan_KcalBurnUp", "MissionOk");

//        ServerManager.Instance.Update_TodayQuest("TodayQuest4", "MissionOk");

//        QuestData_Manager.instance.GetCoinReward(coinText, quest_coinText[3]);

//        rewardBtn.sprite = takeBtnSprite;    //이미지 변경
//        quest_Obj[0].transform.GetChild(1).gameObject.SetActive(true);  //받기완료 이미지 활성화
//    }

//    //퀘스트5 보상받기 버튼
//    public void Quest_MaxSpeedButtonOn()
//    {
//        //quest_Obj[1].transform.GetChild(7).GetComponent<Button>().interactable = false;
//        PlayerPrefs.SetString("Busan_MaxSpeedToday", "MissionOk");

//        ServerManager.Instance.Update_TodayQuest("TodayQuest5", "MissionOk");

//        QuestData_Manager.instance.GetCoinReward(coinText, quest_coinText[4]);

//        quest_Obj[1].transform.GetChild(7).GetComponent<Image>().sprite = takeBtnSprite;    //이미지 변경
//        quest_Obj[1].transform.GetChild(1).gameObject.SetActive(true); //받기완료 이미지 활성화
//    }

//    ////아시아맵 코스 완주
//    public void AsiaCourse1FinishButtonOn()
//    {
//        //Debug.Log("___ 제한속도 " + PlayerPrefs.GetInt("Busan_AsiaNormal1Finish"));
//        asiaNormal1_Count = PlayerPrefs.GetInt("Busan_AsiaNormal1Finish");    //처음 불러오고
//        asiaNormal1_Count++;
//        PlayerPrefs.SetInt("Busan_AsiaNormal1Finish", asiaNormal1_Count); //처음 저장

//        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCourseFinishPriceText[0]);    //코인보상 받기

//        QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaNormal1Finish"), PlayerPrefs.GetInt("Busan_AsiaNormal1FinishAmount"), asiaMapCourseFinishObj[0], 
//            PlayerPrefs.GetString("Busan_Asia Normal 1Course"), asiaMapCourseFinishSlider[0], asiaMapCourseFinishSliderText[0], asiaMapCourseFinishButton[0]);

//        // 서버에 상태 저장 PlayerPrefs.SetInt("AsiaNormal1Finish", asiaNormal1_Count);
//        ServerManager.Instance.Update_MapQuest("AsiaNormal1Finish", asiaNormal1_Count);
//    }
//    public void AsiaCourse2FinishButtonOn()
//    {
//        asiaNormal2_Count = PlayerPrefs.GetInt("Busan_AsiaNormal2Finish");
//        asiaNormal2_Count++;
//        PlayerPrefs.SetInt("Busan_AsiaNormal2Finish", asiaNormal2_Count);

//        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCourseFinishPriceText[1]);    //코인보상 받기

//        QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaNormal2Finish"), PlayerPrefs.GetInt("Busan_AsiaNormal2FinishAmount"), asiaMapCourseFinishObj[1],
//            PlayerPrefs.GetString("Busan_Asia Normal 2Course"), asiaMapCourseFinishSlider[1], asiaMapCourseFinishSliderText[1], asiaMapCourseFinishButton[1]);

//        // 서버 저장 Normal 2
//        ServerManager.Instance.Update_MapQuest("AsiaNormal2Finish", asiaNormal2_Count);
//    }
//    //public void AsiaCourse3FinishButtonOn()
//    //{
//    //    asiaNormal3_Count = PlayerPrefs.GetInt("AsiaNormal3Finish");
//    //    asiaNormal3_Count++;
//    //    PlayerPrefs.SetInt("AsiaNormal3Finish", asiaNormal3_Count);

//    //    QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCourseFinishPriceText[2]);    //코인보상 받기

//    //    QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AsiaNormal3Finish"), PlayerPrefs.GetInt("AsiaNormal3FinishAmount"), asiaMapCourseFinishObj[2],
//    //        PlayerPrefs.GetString("Asia Normal 3Course"), asiaMapCourseFinishSlider[2], asiaMapCourseFinishSliderText[2], asiaMapCourseFinishButton[2]);

//    //    // 서버 저장 Normal 3
//    //    ServerManager.Instance.Update_MapQuest("AsiaNormal3Finish", asiaNormal3_Count);
//    //}
//    public void AsiaCourseHard1FinishButtonOn()
//    {


//        asiaHard1_Count = PlayerPrefs.GetInt("Busan_AsiaHard1Finish");
//        asiaHard1_Count++;
//        PlayerPrefs.SetInt("Busan_AsiaHard1Finish", asiaHard1_Count);

//        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCourseFinishPriceText[2]);    //코인보상 받기

//        QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaHard1Finish"), PlayerPrefs.GetInt("Busan_AsiaHard1FinishAmount"), asiaMapCourseFinishObj[2],
//            PlayerPrefs.GetString("Busan_Asia Hard 1Course"), asiaMapCourseFinishSlider[2], asiaMapCourseFinishSliderText[2], asiaMapCourseFinishButton[2]);

//        // 서버 저장 Hard 1
//        ServerManager.Instance.Update_MapQuest("AsiaHard1Finish", asiaHard1_Count);
//    }
//    public void AsiaCourseHard2FinishButtonOn()
//    {
//        asiaHard2_Count = PlayerPrefs.GetInt("Busan_AsiaHard2Finish");
//        asiaHard2_Count++;
//        PlayerPrefs.SetInt("Busan_AsiaHard2Finish", asiaHard2_Count);

//        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCourseFinishPriceText[3]);    //코인보상 받기

//        QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("Busan_AsiaHard2Finish"), PlayerPrefs.GetInt("Busan_AsiaHard2FinishAmount"), asiaMapCourseFinishObj[3],
//            PlayerPrefs.GetString("Busan_Asia Hard 2Course"), asiaMapCourseFinishSlider[3], asiaMapCourseFinishSliderText[3], asiaMapCourseFinishButton[3]);

//        // 서버 저장 Hard 2
//        ServerManager.Instance.Update_MapQuest("AsiaHard2Finish", asiaHard2_Count);
//    }
//    //public void AsiaCourseHard3FinishButtonOn()
//    //{
//    //    asiaHard3_Count = PlayerPrefs.GetInt("AsiaHard3Finish");    //처음 불러오고
//    //    asiaHard3_Count++;
//    //    PlayerPrefs.SetInt("AsiaHard3Finish", asiaHard3_Count); //처음 저장

//    //    QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCourseFinishPriceText[5]);    //코인보상 받기

//    //    QuestData_Manager.instance.AsiaMapCourse_Finish(PlayerPrefs.GetInt("AsiaHard3Finish"), PlayerPrefs.GetInt("AsiaHard3FinishAmount"), asiaMapCourseFinishObj[5],
//    //        PlayerPrefs.GetString("Asia Hard 3Course"), asiaMapCourseFinishSlider[5], asiaMapCourseFinishSliderText[5], asiaMapCourseFinishButton[5]);

//    //    // 서버 저장 Hard 3
//    //    ServerManager.Instance.Update_MapQuest("AsiaHard3Finish", asiaHard3_Count);
//    //}


//    ////아시아맵 시간제한 코스 완주
//    public void AsiaNormalCourseTimeLimitFinishButtonOn1()
//    {
//        //Debug.Log("___ 시간제한 " + PlayerPrefs.GetInt("Busan_AsiaNormalTimeLimitFinish1"));
//        asiaNormalTimeLimit_Count1 = PlayerPrefs.GetInt("Busan_AsiaNormalTimeLimitFinish1");
//        asiaNormalTimeLimit_Count1++;
//        PlayerPrefs.SetInt("Busan_AsiaNormalTimeLimitFinish1", asiaNormalTimeLimit_Count1);

//        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapTimeLimitFinishPriceText[0]);    //코인보상 받기

//        PlayerPrefs.SetString("Busan_PlayQuestState", "No");  //보상받은 후 게임 플레이를 하지 않은 상태다.
//        QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaNormalTimeLimitFinish1"), PlayerPrefs.GetString("Busan_CurrPlayTime1"), asiaMapTimeLimitFinishTilteText[0], 
//            asiaMapTimeLimitFinishSlider[0], asiaMapTimeLimitFinishSliderText[0], asiaMapTimeLimitFinishButton[0], asiaMapTimeLimitFinishObj[0], takeBtnSprite);


//        // 시간제한1 코스 완료
//        ServerManager.Instance.Update_MapQuest("AsiaNormalTimeLimitFinish1", asiaNormalTimeLimit_Count1);

//        if (asiaNormalTimeLimit_Count1 <= 10)
//            PlayerPrefs.SetString("Busan_CurrPlayTime1", "F1234/99:99:99");
//    }
//    public void AsiaNormalCourseTimeLimitFinishButtonOn2()
//    {
//        asiaNormalTimeLimit_Count2 = PlayerPrefs.GetInt("Busan_AsiaNormalTimeLimitFinish2");
//        asiaNormalTimeLimit_Count2++;
//        PlayerPrefs.SetInt("Busan_AsiaNormalTimeLimitFinish2", asiaNormalTimeLimit_Count2);

//        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapTimeLimitFinishPriceText[1]);    //코인보상 받기

//        PlayerPrefs.SetString("Busan_PlayQuestState", "No");  //보상받은 후 게임 플레이를 하지 않은 상태다.
//        QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaNormalTimeLimitFinish2"), PlayerPrefs.GetString("Busan_CurrPlayTime2"), asiaMapTimeLimitFinishTilteText[1], 
//            asiaMapTimeLimitFinishSlider[1], asiaMapTimeLimitFinishSliderText[1], asiaMapTimeLimitFinishButton[1], asiaMapTimeLimitFinishObj[1], takeBtnSprite);


//        // 시간제한2 코스 완료
//        ServerManager.Instance.Update_MapQuest("AsiaNormalTimeLimitFinish2", asiaNormalTimeLimit_Count2);

//        if (asiaNormalTimeLimit_Count1 <= 10)
//            PlayerPrefs.SetString("Busan_CurrPlayTime2", "F1234/99:99:99");
//    }
//    //public void AsiaNormalCourseTimeLimitFinishButtonOn3()
//    //{
//    //    asiaNormalTimeLimit_Count3 = PlayerPrefs.GetInt("AsiaNormalTimeLimitFinish3");
//    //    asiaNormalTimeLimit_Count3++;
//    //    PlayerPrefs.SetInt("AsiaNormalTimeLimitFinish3", asiaNormalTimeLimit_Count3);

//    //    QuestData_Manager.instance.GetCoinReward(coinText, asiaMapTimeLimitFinishPriceText[2]);    //코인보상 받기

//    //    PlayerPrefs.SetString("PlayQuestState", "No");  //보상받은 후 게임 플레이를 하지 않은 상태다.
//    //    QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AsiaNormalTimeLimitFinish3"), PlayerPrefs.GetString("CurrPlayTime3"), asiaMapTimeLimitFinishTilteText[2], 
//    //        asiaMapTimeLimitFinishSlider[2], asiaMapTimeLimitFinishSliderText[2], asiaMapTimeLimitFinishButton[2], asiaMapTimeLimitFinishObj[2], takeBtnSprite);


//    //    // 시간제한3 코스 완료
//    //    ServerManager.Instance.Update_MapQuest("AsiaNormalTimeLimitFinish3", asiaNormalTimeLimit_Count3);
//    //}
//    public void AsiaHardCourseTimeLimitFinishButtonOn1()
//    {
//        asiaHardTimeLimit_Count1 = PlayerPrefs.GetInt("Busan_AsiaHardTimeLimitFinish1");
//        asiaHardTimeLimit_Count1++;
//        PlayerPrefs.SetInt("Busan_AsiaHardTimeLimitFinish1", asiaHardTimeLimit_Count1);

//        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapTimeLimitFinishPriceText[2]);    //코인보상 받기

//        PlayerPrefs.SetString("Busan_PlayQuestState", "No");  //보상받은 후 게임 플레이를 하지 않은 상태다.
//        QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaHardTimeLimitFinish1"), PlayerPrefs.GetString("Busan_CurrPlayTime4"), asiaMapTimeLimitFinishTilteText[2], 
//            asiaMapTimeLimitFinishSlider[2], asiaMapTimeLimitFinishSliderText[2], asiaMapTimeLimitFinishButton[2], asiaMapTimeLimitFinishObj[2], takeBtnSprite);

//        // 시간제한 하드1 코스 완료
//        ServerManager.Instance.Update_MapQuest("AsiaHardTimeLimitFinish1", asiaHardTimeLimit_Count1);
//        if (asiaNormalTimeLimit_Count1 <= 10)
//            PlayerPrefs.SetString("Busan_CurrPlayTime4", "F1234/99:99:99");
//    }
//    public void AsiaHardCourseTimeLimitFinishButtonOn2()
//    {
//        asiaHardTimeLimit_Count2 = PlayerPrefs.GetInt("Busan_AsiaHardTimeLimitFinish2");
//        asiaHardTimeLimit_Count2++;
//        PlayerPrefs.SetInt("Busan_AsiaHardTimeLimitFinish2", asiaHardTimeLimit_Count2);

//        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapTimeLimitFinishPriceText[3]);    //코인보상 받기

//        PlayerPrefs.SetString("Busan_PlayQuestState", "No");  //보상받은 후 게임 플레이를 하지 않은 상태다.
//        QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("Busan_AsiaHardTimeLimitFinish2"), PlayerPrefs.GetString("Busan_CurrPlayTime5"), asiaMapTimeLimitFinishTilteText[3], 
//            asiaMapTimeLimitFinishSlider[3], asiaMapTimeLimitFinishSliderText[3], asiaMapTimeLimitFinishButton[3], asiaMapTimeLimitFinishObj[3], takeBtnSprite);


//        // 시간제한 하드2 코스 완료
//        ServerManager.Instance.Update_MapQuest("AsiaHardTimeLimitFinish2", asiaHardTimeLimit_Count2);
//        if (asiaNormalTimeLimit_Count1 <= 10)
//            PlayerPrefs.SetString("Busan_CurrPlayTime5", "F1234/99:99:99");
//    }
//    //public void AsiaHardCourseTimeLimitFinishButtonOn3()
//    //{
//    //    asiaHardTimeLimit_Count3 = PlayerPrefs.GetInt("AsiaHardTimeLimitFinish3");
//    //    asiaHardTimeLimit_Count3++;
//    //    PlayerPrefs.SetInt("AsiaHardTimeLimitFinish3", asiaHardTimeLimit_Count3);

//    //    QuestData_Manager.instance.GetCoinReward(coinText, asiaMapTimeLimitFinishPriceText[5]);    //코인보상 받기

//    //    PlayerPrefs.SetString("PlayQuestState", "No");  //보상받은 후 게임 플레이를 하지 않은 상태다.
//    //    QuestData_Manager.instance.AsiaCourse_TimeLimitFinish(PlayerPrefs.GetInt("AsiaHardTimeLimitFinish3"), PlayerPrefs.GetString("CurrPlayTime6"), asiaMapTimeLimitFinishTilteText[5], 
//    //        asiaMapTimeLimitFinishSlider[5], asiaMapTimeLimitFinishSliderText[5], asiaMapTimeLimitFinishButton[5], asiaMapTimeLimitFinishObj[5], takeBtnSprite);

//    //    // 시간제한 하드3 코스 완료
//    //    ServerManager.Instance.Update_MapQuest("AsiaHardTimeLimitFinish3", asiaHardTimeLimit_Count3);
//    //}


//    ////아시아맵 커스텀 퀘스트 미션
//    //맵 전체 완주 1, 10, 20번
//    public void AsiaMapAllOneFinishButtonOn()
//    {
//        PlayerPrefs.SetString("Busan_AllOneFinish", "MissionOk");
//        asiaMapCustomQuestObj[0].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
//        asiaMapCustomQuestObj[0].transform.GetChild(7).GetComponent<Button>().interactable = false;
//        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCustomQuestCoinText[0]);

//        ServerManager.Instance.Update_QuestReward("AllOneFinish", "MissionOk");
//    }
//    public void AsiaMapAllTenFinishButtonOn()   //전체 10번씩 완주
//    {
//        PlayerPrefs.SetString("Busan_AllTenFinish", "MissionOk");
//        asiaMapCustomQuestObj[1].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
//        asiaMapCustomQuestObj[1].transform.GetChild(7).GetComponent<Button>().interactable = false;
//        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCustomQuestCoinText[1]);

//        ServerManager.Instance.Update_QuestReward("AllTenFinish", "MissionOk");
//    }
//    public void AsiaMapAllTwentyFinishButtonOn()    //전체 20번씩 완주
//    {
//        PlayerPrefs.SetString("Busan_AllTwentyFinish", "MissionOk");
//        asiaMapCustomQuestObj[2].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
//        asiaMapCustomQuestObj[2].transform.GetChild(7).GetComponent<Button>().interactable = false;
//        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCustomQuestCoinText[2]);

//        ServerManager.Instance.Update_QuestReward("AllTwentyFinish", "MissionOk");
//    }

//    //총 거리 500, 1000, 1500km
//    public void AsiaMap500kmPassButtonOn()
//    {
//        PlayerPrefs.SetString("Busan_Distance500Km", "MissionOk");
//        asiaMapCustomQuestObj[3].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
//        asiaMapCustomQuestObj[3].transform.GetChild(7).GetComponent<Button>().interactable = false;
//        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCustomQuestCoinText[3]);

//        ServerManager.Instance.Update_QuestReward("Distance500Km", "MissionOk");
//    }
//    public void AsiaMap1000kmPassButtonOn()
//    {
//        PlayerPrefs.SetString("Busan_Distance1000Km", "MissionOk");
//        asiaMapCustomQuestObj[4].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
//        asiaMapCustomQuestObj[4].transform.GetChild(7).GetComponent<Button>().interactable = false;
//        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCustomQuestCoinText[4]);

//        ServerManager.Instance.Update_QuestReward("Distance1000Km", "MissionOk");
//    }
//    public void AsiaMap1500kmPassButtonOn()
//    {
//        PlayerPrefs.SetString("Busan_Distance1500Km", "MissionOk");
//        asiaMapCustomQuestObj[5].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
//        asiaMapCustomQuestObj[5].transform.GetChild(7).GetComponent<Button>().interactable = false;
//        QuestData_Manager.instance.GetCoinReward(coinText, asiaMapCustomQuestCoinText[5]);

//        ServerManager.Instance.Update_QuestReward("Distance1500Km", "MissionOk");
//    }



//    //백버튼 클릭 시 이벤트 - 로비 이동
//    public void BackButtonOn()
//    {
//        SoundMaixerManager.instance.OutGameBGMPlay();
//        SceneManager.LoadScene("Lobby");
//    }

//    //BGM 사운드
//    public void BGM_SliderSound()
//    {
//        SoundMaixerManager.instance.AudioControl();
//    }

//    //효과음 사운드
//    public void Effect_SliderSound()
//    {
//        SoundMaixerManager.instance.SFXAudioControl();
//    }

//}
