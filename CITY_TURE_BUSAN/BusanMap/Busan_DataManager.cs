using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Busan_DataManager : MonoBehaviour {
    public BusanMapStringClass stringClass;
    public static Busan_DataManager instance { get; private set; }


    public Text kcalText;

    public Text[] todayMapQuestName;

    [Header("Localized Quest String")]
    public LocalizedString gameQuest_BurnUpCalroies;
    public LocalizedString gameQuest_MaxSpeed;
    public LocalizedString gameQuest_TargetTime;
    [Space(10)]

    public GameObject[] todayMapQuestClearImg;
    public GameObject gameEndPopup; //게임종료팝업

    public bool gameTimeStop;   //게임 일시정지
    public bool tuto_btnOn; //튜토리얼 버튼 클릭 여부

    int currentLocale;
    string courseName;
    private void Awake() {
        if (instance != null)
            Destroy(this);
        else instance = this;

        stringClass = new BusanMapStringClass();
    }

    private void OnEnable() {
        gameQuest_BurnUpCalroies.StringChanged += BurnUpCalroies_StringChanged;
        gameQuest_MaxSpeed.StringChanged += MaxSpeed_StringChanged;
        gameQuest_TargetTime.StringChanged += TargetTime_StringChanged;
    }
    private void OnDisable() {
        gameQuest_BurnUpCalroies.StringChanged -= BurnUpCalroies_StringChanged;
        gameQuest_MaxSpeed.StringChanged -= MaxSpeed_StringChanged;
        gameQuest_TargetTime.StringChanged -= TargetTime_StringChanged;
    }
    private void TargetTime_StringChanged(string value) {
        todayMapQuestName[2].text = value;
    }
    private void MaxSpeed_StringChanged(string value) {
        todayMapQuestName[1].text = value;
    }
    private void BurnUpCalroies_StringChanged(string value) {
        todayMapQuestName[0].text = value;
    }

    void Start() {
        currentLocale = LanguageSelectorManager.instance.GetCurrentSelectedLanguage();

        TodayMapQuestCreation();
    }

    //프로필 이미지 
    public void ProfileImageChange(Image _profileImg) {
        _profileImg.sprite = Resources.Load<Sprite>("Profile/" + PlayerPrefs.GetInt("Busan_Player_Profile"));
    }

    //다음 코스 오픈위한 시간타임
    public string NextCourseOpenTimeMission() {
        //Debug.Log("오픈 시간 : " + PlayerPrefs.GetInt("Busan_MapCourseOpenTime"));
        int time = PlayerPrefs.GetInt("Busan_MapCourseOpenTime");
        time = time / 60;
        //_timeText.text = time.ToString() + "완주달성";

        return time.ToString();
    }

    //파티클 오프
    public void ParticleSystemOff(ParticleSystem _dust) {
        _dust.gameObject.SetActive(false);
    }

    //라이트 끔
    public void LightBeamOff(GameObject _light) {
        _light.gameObject.SetActive(false);
    }

    //내 현재 속도
    public float MySpeed(float _speed) {
        return Mathf.Lerp(0, 80f, Mathf.InverseLerp(0, 50f, _speed));   //34
    }

    //최고 속도 저장하기
    public void MaxSpeed(string _currSpeed) {
        float maxSpeed = PlayerPrefs.GetFloat("Busan_Record_MaxSpeed");
        float currSpeed = float.Parse(_currSpeed);



        //오늘최고속도 미션 체크 함수 클리어
        TodayMapQuestMaxSpeed(currSpeed);

        //게임 종류 시 퀘스트 결과 저장
        PlayerPrefs.SetString("Busan_AfterQuestMaxSpeed", TodayMapQuestMaxSpeed(currSpeed));

        if (currSpeed > maxSpeed)
            PlayerPrefs.SetFloat("Busan_Record_MaxSpeed", currSpeed);
    }

    //최종 칼로리
    public void FinishKcal() {
        if (currentLocale == (int)LanguageType.English) {
            kcalText.text = PlayerPrefs.GetFloat("Busan_CurrKcal").ToString("N0") + "kcal burned";
        } else if (currentLocale == (int)LanguageType.Korean) {
            kcalText.text = PlayerPrefs.GetFloat("Busan_CurrKcal").ToString("N0") + "kcal 소모";
        }
    }

    //상단 튜토리얼 버튼 클릭 
    public void TutorialButtonOn() {
        tuto_btnOn = true;
    }


    //일시정지 //GameEndButton 클릭 시 이벤트
    public void GameTimeStop() {
        //튜토리얼 버튼 눌렀을 경우
        if (tuto_btnOn.Equals(true))
            PlayerPrefs.SetString("Busan_InGameTutorial", "Start");

        gameTimeStop = true;
        Busan_UIManager.instance.GamePlayStop();  //스피드 0으로
    }

    //재생 //PopupCloseBtn 클릭 시 이벤트
    public void GameTimePlay() {
        gameTimeStop = false;
        BusanMap_GameTime.instance.PlayTime();   //게임다시 시작
        Busan_UIManager.instance.GamePlay();  //속도
    }

    public void BeginningGame() {
        Busan_UIManager.instance.Parent_Division();   //부모자식 분리
        Busan_UIManager.instance.ParticleSystemOff(); //연기 비활성화

        SceneManager.LoadScene("Loading");
    }

    public void GameEnd() {
        Busan_UIManager.instance.Parent_Division();   //부모자식 분리
        Busan_UIManager.instance.ParticleSystemOff(); //연기 비활성화

        SceneManager.LoadScene("MapChoice");
    }


    //-----------일일퀘스트------------------
    //일일맵퀘스트 게임 시작 시 생성함수
    public void TodayMapQuestCreation() {
        //MapOpenTimeMission();
        PlayerPrefs.SetString("Busan_BeforeQuestKcal", TodayMapQuestKcalBurnUp(0));
        TodayMapQuestKcalBurnUp(0);  //칼로리소모 미션 
        PlayerPrefs.SetString("Busan_BeforeQuestMaxSpeed", TodayMapQuestMaxSpeed(0));
        TodayMapQuestMaxSpeed(0);    //최대속도 미션

        CourseTimeLimitQuest(); //코스별 시간제한 미션
    }

    //칼로리소모 미션 확인 함수
    public string TodayMapQuestKcalBurnUp(float _finishKcal) {
        string questClear = "No"; //퀘스트 완료 여부

        int kcalNum = PlayerPrefs.GetInt("Busan_TodayQuest4");    //칼로리 소모
        float todayKcal = PlayerPrefs.GetFloat("Busan_Record_TodayKcal"); //오늘 칼로리 소모

        if (kcalNum == (int)TodayQuestBurnUpMission.Over300kcal) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetKcal" , "300" }
            };

            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_BurnUpCalroies, localVariables);
            //todayMapQuestName[0].text = "칼로리 소모: 300kcal이상";

            if ((int)todayKcal + (int)_finishKcal < 300) {
                todayMapQuestClearImg[0].SetActive(false);  //클리어 비활성화
                questClear = "No";
            } else {
                todayMapQuestClearImg[0].SetActive(true);  //클리어 활성화
                questClear = "Yes";
            }
        } else if (kcalNum == (int)TodayQuestBurnUpMission.Over400kcal) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetKcal" , "400" }
            };

            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_BurnUpCalroies, localVariables);
            //todayMapQuestName[0].text = "칼로리 소모: 400kcal이상";

            if ((int)todayKcal + (int)_finishKcal < 400) {
                todayMapQuestClearImg[0].SetActive(false);  //클리어 비활성화
                questClear = "No";
            } else {
                todayMapQuestClearImg[0].SetActive(true);  //클리어 활성화
                questClear = "Yes";
            }
        } else if (kcalNum == (int)TodayQuestBurnUpMission.Over500kcal) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetKcal" , "500" }
            };

            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_BurnUpCalroies, localVariables);

            if ((int)todayKcal + (int)_finishKcal < 500) {
                todayMapQuestClearImg[0].SetActive(false);  //클리어 비활성화
                questClear = "No";
            } else {
                todayMapQuestClearImg[0].SetActive(true);  //클리어 활성화
                questClear = "Yes";
            }
        } else if (kcalNum == (int)TodayQuestBurnUpMission.Over600kcal) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetKcal" , "600" }
            };

            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_BurnUpCalroies, localVariables);

            if ((int)todayKcal + (int)_finishKcal < 600) {
                todayMapQuestClearImg[0].SetActive(false);  //클리어 비활성화
                questClear = "No";
            } else {
                todayMapQuestClearImg[0].SetActive(true);  //클리어 활성화
                questClear = "Yes";
            }
        }
        return questClear;
    }


    //최대속도 미션 확인 함수
    string TodayMapQuestMaxSpeed(float _playSpeed) {
        string questClear = "No"; //퀘스트 완료 여부

        int maxSpeedNum = PlayerPrefs.GetInt("Busan_TodayQuest5");    //최고속도
        float todayMaxSpeed = PlayerPrefs.GetFloat("Busan_TodayMaxSpeed");    //오늘 최고속도
        //Debug.Log("최대속도 미션 : " + maxSpeedNum);

        if (todayMaxSpeed < _playSpeed)
            PlayerPrefs.SetFloat("Busan_TodayMaxSpeed", _playSpeed);  //최고속도 갱신


        if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_25) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetSpeed" , "25" }
            };

            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_MaxSpeed, localVariables);
            //todayMapQuestName[1].text = "오늘의 속도: 25km이상";

            if (todayMaxSpeed < 25f) {
                todayMapQuestClearImg[1].SetActive(false);
                questClear = "No"; //퀘스트 완료 여부
            } else {
                todayMapQuestClearImg[1].SetActive(true);   //클리어이미지활성화
                questClear = "Yes"; //퀘스트 완료 여부
            }
        } else if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_30) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetSpeed" , "30" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_MaxSpeed, localVariables);

            if (todayMaxSpeed < 30f) {
                todayMapQuestClearImg[1].SetActive(false);
                questClear = "No"; //퀘스트 완료 여부
            } else {
                todayMapQuestClearImg[1].SetActive(true);   //클리어이미지활성화
                questClear = "Yes"; //퀘스트 완료 여부
            }
        } else if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_35) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetSpeed" , "35" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_MaxSpeed, localVariables);

            if (todayMaxSpeed < 35f) {
                todayMapQuestClearImg[1].SetActive(false);
                questClear = "No"; //퀘스트 완료 여부
            } else {
                todayMapQuestClearImg[1].SetActive(true);   //클리어이미지활성화
                questClear = "Yes"; //퀘스트 완료 여부
            }
        } else if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_40) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetSpeed" , "40" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_MaxSpeed, localVariables);

            if (todayMaxSpeed < 40f) {
                todayMapQuestClearImg[1].SetActive(false);
                questClear = "No"; //퀘스트 완료 여부
            } else {
                todayMapQuestClearImg[1].SetActive(true);   //클리어이미지활성화
                questClear = "Yes"; //퀘스트 완료 여부
            }
        } else if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_45) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetSpeed" , "45" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_MaxSpeed, localVariables);

            if (todayMaxSpeed < 45f) {
                todayMapQuestClearImg[1].SetActive(false);
                questClear = "No"; //퀘스트 완료 여부
            } else {
                todayMapQuestClearImg[1].SetActive(true);   //클리어이미지활성화
                questClear = "Yes"; //퀘스트 완료 여부
            }
        } else if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_50) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetSpeed" , "50" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_MaxSpeed, localVariables);

            if (todayMaxSpeed < 50f) {
                todayMapQuestClearImg[1].SetActive(false);
                questClear = "No"; //퀘스트 완료 여부
            } else {
                todayMapQuestClearImg[1].SetActive(true);   //클리어이미지활성화
                questClear = "Yes"; //퀘스트 완료 여부
            }
        } else if (maxSpeedNum == (int)TodayQuestMaxSpeed.MaxSpeed_55) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetSpeed" , "55" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_MaxSpeed, localVariables);

            if (todayMaxSpeed < 55f) {
                todayMapQuestClearImg[1].SetActive(false);
                questClear = "No"; //퀘스트 완료 여부
            } else {
                todayMapQuestClearImg[1].SetActive(true);   //클리어이미지활성화
                questClear = "Yes"; //퀘스트 완료 여부
            }
        }
        return questClear;
    }

    //코스별 시간제한 퀘스트 미션
    public void CourseTimeLimitQuest() {

        if (PlayerPrefs.GetString("Busan_CurrentMap_Name").Equals(stringClass.BUSAN_RED_LINE)) {courseName = PlayerPrefs.GetString("Busan_MapCourseName");}
        else if(PlayerPrefs.GetString("Busan_CurrentMap_Name").Equals(stringClass.BUSAN_GREEN_LINE)) { courseName = PlayerPrefs.GetString("Busan_GreenMapCourseName"); }
           

        string timeLimitCourseNumber = "";

        if (courseName.Equals("Normal-1"))
            timeLimitCourseNumber = "Busan_AsiaNormalTimeLimitFinish1";
        else if (courseName.Equals("Normal-2"))
            timeLimitCourseNumber = "Busan_AsiaNormalTimeLimitFinish2";
        else if (courseName.Equals("Hard-1"))
            timeLimitCourseNumber = "Busan_AsiaHardTimeLimitFinish1";
        else if (courseName.Equals("Hard-2"))
            timeLimitCourseNumber = "Busan_AsiaHardTimeLimitFinish2";

        //Debug.Log("timeLimitCourseNumber " + PlayerPrefs.GetInt(timeLimitCourseNumber));

        if (PlayerPrefs.GetInt(timeLimitCourseNumber) == (int)MapQuestTargetTime.Target_5_00) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetTime" , "5:00" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_TargetTime, localVariables);

            //Debug.Log("여기 들어옴");
            //todayMapQuestName[2].text = "도전:5분 내로 완주!";
            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
        } else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(1)) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetTime" , "4:40" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_TargetTime, localVariables);

            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
        } else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(2)) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetTime" , "4:20" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_TargetTime, localVariables);

            //todayMapQuestName[2].text = "도전:4분20초 내로 완주!";
            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
        } else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(3)) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetTime" , "4:00" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_TargetTime, localVariables);

            //todayMapQuestName[2].text = "도전:4분 내로 완주!";
            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
        } else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(4)) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetTime" , "3:40" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_TargetTime, localVariables);
            //todayMapQuestName[2].text = "도전:3분40초 내로 완주!";
            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
        } else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(5)) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetTime" , "3:20" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_TargetTime, localVariables);
            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
        } else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(6)) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetTime" , "3:00" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_TargetTime, localVariables);
            //todayMapQuestName[2].text = "도전:3분 내로 완주!";
            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
        } else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(7)) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetTime" , "2:40" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_TargetTime, localVariables);

            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
        } else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(8)) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetTime" , "2:20" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_TargetTime, localVariables);
            //todayMapQuestName[2].text = "도전:2분20초 내로 완주!";
            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
        } else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(9)) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetTime" , "2:00" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_TargetTime, localVariables);
            //todayMapQuestName[2].text = "도전:2분 내로 완주!";
            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
        } else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(10)) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetTime" , "1:40" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_TargetTime, localVariables);
            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
        } else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(11)) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetTime" , "1:20" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_TargetTime, localVariables);
            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
        } else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(12)) {
            Dictionary<string, string> localVariables = new Dictionary<string, string>()
            {
                { "targetTime" , "1:20" }
            };
            LanguageSelectorManager.instance.SetLocalVariablesString(gameQuest_TargetTime, localVariables);

            todayMapQuestClearImg[2].SetActive(true);  //클리어 활성화
        }
    }

    //센서 재연결
    public void ConnectButtonOn() {
        GameObject sensor = GameObject.Find("SensorManager");
        ArduinoHM10Test2 sensor_Script = sensor.GetComponent<ArduinoHM10Test2>();

        sensor_Script.StartProcess();
    }

    //게임 종료
    public void GameEndButtonOn() {
        Application.Quit(); //게임 종료
    }

    //게임 진행
    public void PopupCloseButtonOn() {
        GameTimePlay(); //다시 진행
    }

    private void Update() {
        if (Application.platform.Equals(RuntimePlatform.Android)) {
            if (Input.GetKey(KeyCode.Escape)) {
                gameEndPopup.SetActive(true);
                GameTimeStop(); // 일시정지
            }
        }
    }
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class Busan_DataManager : MonoBehaviour
//{
//    public static Busan_DataManager instance { get; private set; }


//    public Text kcalText;

//    public Text[] todayMapQuestName;
//    public GameObject[] todayMapQuestClearImg;
//    public GameObject gameEndPopup; //게임종료팝업

//    public bool gameTimeStop;   //게임 일시정지
//    public bool tuto_btnOn; //튜토리얼 버튼 클릭 여부




//    private void Awake()
//    {
//        if (instance != null)
//            Destroy(this);
//        else instance = this;
//    }


//    void Start()
//    {
//        TodayMapQuestCreation();
//    }

//    //프로필 이미지 
//    public void ProfileImageChange(Image _profileImg)
//    {
//        _profileImg.sprite = Resources.Load<Sprite>("Profile/" + PlayerPrefs.GetInt("Busan_Player_Profile"));
//    }

//    //다음 코스 오픈위한 시간타임
//    public string NextCourseOpenTimeMission()
//    {
//        //Debug.Log("오픈 시간 : " + PlayerPrefs.GetInt("Busan_MapCourseOpenTime"));
//        int time = PlayerPrefs.GetInt("Busan_MapCourseOpenTime");
//        time = time / 60;
//        //_timeText.text = time.ToString() + "완주달성";

//        return time.ToString();
//    }

//    //파티클 오프
//    public void ParticleSystemOff(ParticleSystem _dust)
//    {
//        _dust.gameObject.SetActive(false);
//    }

//    //라이트 끔
//    public void LightBeamOff(GameObject _light)
//    {
//        _light.gameObject.SetActive(false);
//    }

//    //내 현재 속도
//    public float MySpeed(float _speed)
//    {
//        return Mathf.Lerp(0, 80f, Mathf.InverseLerp(0, 50f, _speed));   //34
//    }

//    //최고 속도 저장하기
//    public void MaxSpeed(string _currSpeed)
//    {
//        float maxSpeed = PlayerPrefs.GetFloat("Busan_Record_MaxSpeed");
//        float currSpeed = float.Parse(_currSpeed);



//        //오늘최고속도 미션 체크 함수 클리어
//        TodayMapQuestMaxSpeed(currSpeed);

//        //게임 종류 시 퀘스트 결과 저장
//        PlayerPrefs.SetString("Busan_AfterQuestMaxSpeed", TodayMapQuestMaxSpeed(currSpeed));

//        if (currSpeed > maxSpeed)
//            PlayerPrefs.SetFloat("Busan_Record_MaxSpeed", currSpeed);
//    }

//    //최종 칼로리
//    public void FinishKcal()
//    {
//        kcalText.text = PlayerPrefs.GetFloat("Busan_CurrKcal").ToString("N0") + "kcal 소모";
//    }



//    //상단 튜토리얼 버튼 클릭 
//    public void TutorialButtonOn()
//    {
//        tuto_btnOn = true;
//    }


//    //일시정지 //GameEndButton 클릭 시 이벤트
//    public void GameTimeStop()
//    {
//        //튜토리얼 버튼 눌렀을 경우
//        if (tuto_btnOn.Equals(true))
//            PlayerPrefs.SetString("Busan_InGameTutorial", "Start");

//        gameTimeStop = true;
//        Busan_UIManager.instance.GamePlayStop();  //스피드 0으로
//    }

//    //재생 //PopupCloseBtn 클릭 시 이벤트
//    public void GameTimePlay()
//    {
//        gameTimeStop = false;
//        BusanMap_GameTime.instance.PlayTime();   //게임다시 시작
//        Busan_UIManager.instance.GamePlay();  //속도
//    }

//    public void BeginningGame()
//    {
//        Busan_UIManager.instance.Parent_Division();   //부모자식 분리
//        Busan_UIManager.instance.ParticleSystemOff(); //연기 비활성화

//        SceneManager.LoadScene("Loading");
//    }

//    public void GameEnd()
//    {
//        Busan_UIManager.instance.Parent_Division();   //부모자식 분리
//        Busan_UIManager.instance.ParticleSystemOff(); //연기 비활성화

//        SceneManager.LoadScene("MapChoice");
//    }


//    //-----------일일퀘스트------------------
//    //일일맵퀘스트 게임 시작 시 생성함수
//    public void TodayMapQuestCreation()
//    {

//        //MapOpenTimeMission();
//        PlayerPrefs.SetString("Busan_BeforeQuestKcal", TodayMapQuestKcalBurnUp(0));
//        TodayMapQuestKcalBurnUp(0);  //칼로리소모 미션 
//        PlayerPrefs.SetString("Busan_BeforeQuestMaxSpeed", TodayMapQuestMaxSpeed(0));
//        TodayMapQuestMaxSpeed(0);    //최대속도 미션

//        CourseTimeLimitQuest(); //코스별 시간제한 미션
//    }



//    //칼로리소모 미션 확인 함수
//    public string TodayMapQuestKcalBurnUp(float _finishKcal)
//    {
//        string questClear = "No"; //퀘스트 완료 여부

//        int kcalNum = PlayerPrefs.GetInt("Busan_TodayQuest4");    //칼로리 소모
//        float todayKcal = PlayerPrefs.GetFloat("Busan_Record_TodayKcal"); //오늘 칼로리 소모

//        if (kcalNum.Equals(0))
//        {
//            todayMapQuestName[0].text = "칼로리 소모: 300kcal이상";
//            if ((int)todayKcal + (int)_finishKcal < 300)
//            {
//                todayMapQuestClearImg[0].SetActive(false);  //클리어 비활성화
//                questClear = "No";
//            }
//            else
//            {
//                todayMapQuestClearImg[0].SetActive(true);  //클리어 활성화
//                questClear = "Yes";
//            }
//        }
//        else if (kcalNum.Equals(1))
//        {
//            todayMapQuestName[0].text = "칼로리 소모: 400kcal이상";
//            if ((int)todayKcal + (int)_finishKcal < 400)
//            {
//                todayMapQuestClearImg[0].SetActive(false);  //클리어 비활성화
//                questClear = "No";
//            }
//            else
//            {
//                todayMapQuestClearImg[0].SetActive(true);  //클리어 활성화
//                questClear = "Yes";
//            }
//        }
//        else if (kcalNum.Equals(2))
//        {
//            todayMapQuestName[0].text = "칼로리 소모: 500kcal이상";
//            if ((int)todayKcal + (int)_finishKcal < 500)
//            {
//                todayMapQuestClearImg[0].SetActive(false);  //클리어 비활성화
//                questClear = "No";
//            }
//            else
//            {
//                todayMapQuestClearImg[0].SetActive(true);  //클리어 활성화
//                questClear = "Yes";
//            }
//        }
//        else if (kcalNum.Equals(3))
//        {
//            todayMapQuestName[0].text = "칼로리 소모: 600kcal이상";
//            if ((int)todayKcal + (int)_finishKcal < 600)
//            {
//                todayMapQuestClearImg[0].SetActive(false);  //클리어 비활성화
//                questClear = "No";
//            }
//            else
//            {
//                todayMapQuestClearImg[0].SetActive(true);  //클리어 활성화
//                questClear = "Yes";
//            }
//        }
//        return questClear;
//    }


//    //최대속도 미션 확인 함수
//    string TodayMapQuestMaxSpeed(float _playSpeed)
//    {
//        string questClear = "No"; //퀘스트 완료 여부

//        int maxSpeedNum = PlayerPrefs.GetInt("Busan_TodayQuest5");    //최고속도
//        float todayMaxSpeed = PlayerPrefs.GetFloat("Busan_TodayMaxSpeed");    //오늘 최고속도
//        //Debug.Log("최대속도 미션 : " + maxSpeedNum);

//        if (todayMaxSpeed < _playSpeed)
//            PlayerPrefs.SetFloat("Busan_TodayMaxSpeed", _playSpeed);  //최고속도 갱신


//        if (maxSpeedNum.Equals(0))
//        {
//            todayMapQuestName[1].text = "오늘의 속도: 25km이상";
//            if (todayMaxSpeed < 25f)
//            {
//                todayMapQuestClearImg[1].SetActive(false);
//                questClear = "No"; //퀘스트 완료 여부
//            }
//            else
//            {
//                todayMapQuestClearImg[1].SetActive(true);   //클리어이미지활성화
//                questClear = "Yes"; //퀘스트 완료 여부
//            }
//        }
//        else if (maxSpeedNum.Equals(1))
//        {
//            todayMapQuestName[1].text = "오늘의 속도: 30km이상";
//            if (todayMaxSpeed < 30f)
//            {
//                todayMapQuestClearImg[1].SetActive(false);
//                questClear = "No"; //퀘스트 완료 여부
//            }
//            else
//            {
//                todayMapQuestClearImg[1].SetActive(true);   //클리어이미지활성화
//                questClear = "Yes"; //퀘스트 완료 여부
//            }
//        }
//        else if (maxSpeedNum.Equals(2))
//        {
//            todayMapQuestName[1].text = "오늘의 속도: 35km이상";
//            if (todayMaxSpeed < 35f)
//            {
//                todayMapQuestClearImg[1].SetActive(false);
//                questClear = "No"; //퀘스트 완료 여부
//            }
//            else
//            {
//                todayMapQuestClearImg[1].SetActive(true);   //클리어이미지활성화
//                questClear = "Yes"; //퀘스트 완료 여부
//            }
//        }
//        else if (maxSpeedNum.Equals(3))
//        {
//            todayMapQuestName[1].text = "오늘의 속도: 40km이상";
//            if (todayMaxSpeed < 40f)
//            {
//                todayMapQuestClearImg[1].SetActive(false);
//                questClear = "No"; //퀘스트 완료 여부
//            }
//            else
//            {
//                todayMapQuestClearImg[1].SetActive(true);   //클리어이미지활성화
//                questClear = "Yes"; //퀘스트 완료 여부
//            }
//        }
//        else if (maxSpeedNum.Equals(4))
//        {
//            todayMapQuestName[1].text = "오늘의 속도: 45km이상";
//            if (todayMaxSpeed < 45f)
//            {
//                todayMapQuestClearImg[1].SetActive(false);
//                questClear = "No"; //퀘스트 완료 여부
//            }
//            else
//            {
//                todayMapQuestClearImg[1].SetActive(true);   //클리어이미지활성화
//                questClear = "Yes"; //퀘스트 완료 여부
//            }
//        }
//        else if (maxSpeedNum.Equals(5))
//        {
//            todayMapQuestName[1].text = "오늘의 속도: 50km이상";
//            if (todayMaxSpeed < 50f)
//            {
//                todayMapQuestClearImg[1].SetActive(false);
//                questClear = "No"; //퀘스트 완료 여부
//            }
//            else
//            {
//                todayMapQuestClearImg[1].SetActive(true);   //클리어이미지활성화
//                questClear = "Yes"; //퀘스트 완료 여부
//            }
//        }
//        else if (maxSpeedNum.Equals(6))
//        {
//            todayMapQuestName[1].text = "오늘의 속도: 55km이상";
//            if (todayMaxSpeed < 55f)
//            {
//                todayMapQuestClearImg[1].SetActive(false);
//                questClear = "No"; //퀘스트 완료 여부
//            }
//            else
//            {
//                todayMapQuestClearImg[1].SetActive(true);   //클리어이미지활성화
//                questClear = "Yes"; //퀘스트 완료 여부
//            }
//        }
//        return questClear;
//    }

//    //코스별 시간제한 퀘스트 미션
//    public void CourseTimeLimitQuest()
//    {
//        string courseName = PlayerPrefs.GetString("Busan_MapCourseName");
//        string timeLimitCourseNumber = "";

//        if (courseName.Equals("Normal-1"))
//            timeLimitCourseNumber = "Busan_AsiaNormalTimeLimitFinish1";
//        else if (courseName.Equals("Normal-2"))
//            timeLimitCourseNumber = "Busan_AsiaNormalTimeLimitFinish2";
//        else if (courseName.Equals("Hard-1"))
//            timeLimitCourseNumber = "Busan_AsiaHardTimeLimitFinish1";
//        else if (courseName.Equals("Hard-2"))
//            timeLimitCourseNumber = "Busan_AsiaHardTimeLimitFinish2";

//        //Debug.Log("timeLimitCourseNumber " + PlayerPrefs.GetInt(timeLimitCourseNumber));

//        if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(0))
//        {
//            //Debug.Log("여기 들어옴");
//            todayMapQuestName[2].text = "도전:5분 내로 완주!";
//            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
//        }
//        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(1))
//        {
//            todayMapQuestName[2].text = "도전:4분40초 내로 완주!";
//            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
//        }
//        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(2))
//        {
//            todayMapQuestName[2].text = "도전:4분20초 내로 완주!";
//            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
//        }
//        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(3))
//        {
//            todayMapQuestName[2].text = "도전:4분 내로 완주!";
//            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
//        }
//        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(4))
//        {
//            todayMapQuestName[2].text = "도전:3분40초 내로 완주!";
//            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
//        }
//        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(5))
//        {
//            todayMapQuestName[2].text = "도전:3분20초 내로 완주!";
//            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
//        }
//        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(6))
//        {
//            todayMapQuestName[2].text = "도전:3분 내로 완주!";
//            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
//        }
//        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(7))
//        {
//            todayMapQuestName[2].text = "도전:2분40초 내로 완주!";
//            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
//        }
//        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(8))
//        {
//            todayMapQuestName[2].text = "도전:2분20초 내로 완주!";
//            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
//        }
//        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(9))
//        {
//            todayMapQuestName[2].text = "도전:2분 내로 완주!";
//            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
//        }
//        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(10))
//        {
//            todayMapQuestName[2].text = "도전:1분40초 내로 완주!";
//            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
//        }
//        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(11))
//        {
//            todayMapQuestName[2].text = "도전:1분20초 내로 완주!";
//            todayMapQuestClearImg[2].SetActive(false);  //클리어 비활성화
//        }
//        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(12))
//        {
//            todayMapQuestName[2].text = "도전:1분20초 내로 완주!";
//            todayMapQuestClearImg[2].SetActive(true);  //클리어 활성화
//        }
//    }

//    //센서 재연결
//    public void ConnectButtonOn()
//    {
//        GameObject sensor = GameObject.Find("SensorManager");
//        ArduinoHM10Test2 sensor_Script = sensor.GetComponent<ArduinoHM10Test2>();

//        sensor_Script.StartProcess();
//    }

//    //게임 종료
//    public void GameEndButtonOn()
//    {
//        Application.Quit(); //게임 종료
//    }

//    //게임 진행
//    public void PopupCloseButtonOn()
//    {
//        GameTimePlay(); //다시 진행
//    }

//    private void Update()
//    {
//        if (Application.platform.Equals(RuntimePlatform.Android))
//        {
//            if (Input.GetKey(KeyCode.Escape))
//            {
//                gameEndPopup.SetActive(true);
//                GameTimeStop(); // 일시정지
//            }
//        }
//    }
//}
