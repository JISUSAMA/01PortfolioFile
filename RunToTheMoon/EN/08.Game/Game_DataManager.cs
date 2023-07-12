using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Globalization;
using TMPro;

public class Game_DataManager : MonoBehaviour
{
    public static Game_DataManager instance { get; private set; }

    public string nickName; //닉네임
    public int playDay; //플레이날짜 - Game_DataManager.cs , 날짜 지났을 때 처리하기
    public string todayTime;    // 오늘 하루 게임시간 저장 ( 게임 후 서버에서 todayTime_f )- Game_DataManager.cs
    private string todayTimeinGame;
    public float todayTime_f;   // 게임하면서 걸은 시간
    public int today_stepCount;
    public int once_stepCount; //한 게임 마다
   
    public float todayKcal; //하루칼로리 - RunnerPlayer.cs
    public float once_Kcal; //현재 게임 칼로리 - RunnerPlayer.cs
    
    private float today_Distance;
    private float once_Distance; //현 게임에서의 걸은 거리 

    public int coin;    //코인 - RunnerPlayer.cs
    public int before_coin;    //코인 - RunnerPlayer.cs
    
    public float moonDis;   //달까지 거리 - RunnerPlayer.cs
    public float spaceStationDis;   //우주정거장 거리 - RunnerPlayer.cs
    
  
    public int currentSection;      // 현재 섹션 - Game_UIManager.cs 
    public bool runEndState;   //걷기 종료 버튼 누른 상태
    public bool gamePlaying;
    private int timeCalDay;
    public int CollectionNumber; //얻은 달의 조각 갯수

    [Header("Debug")]
    public TMP_InputField dist;
    public bool debugging;
    private float f_dist;
    [Range(5, 0f)] public float _stationDis;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        _stationDis = 5f;
        f_dist = 5f;

        DataInitialization();

        //spaceStationDis = 0.1f;
        //PlayerPrefs.DeleteKey("Today_date");
        if (PlayerPrefs.HasKey("Today_date"))
        {
            // 초기화 이후 동작
            // 가장 최근에 실행했던 날짜 가져온다.
            string lastDate_str = PlayerPrefs.GetString("Today_date");

            // 현재 날짜 가져온다.
            DateTime currentDate = DateTime.Now;
            string currentDate_str = currentDate.ToString("yyyy-MM-dd");

            //전 날짜 기록이 오늘 날짜 기록과 다른 경우, 초기화
            if (lastDate_str != currentDate_str)
            {
                PlayerPrefs.SetFloat("Today_StepTime_f", 0);
                todayTime_f = PlayerPrefs.GetFloat("Today_StepTime_f");

                // 오늘 날짜 넣기
                PlayerPrefs.SetString("Today_date", currentDate_str);
            }
            else
            { // 전 날짜와 오늘 날짜가 같다면 >> 현재 두번 이상함.

                todayTime_f = PlayerPrefs.GetFloat("Today_StepTime_f");
            }
        }
        else
        {
            // 완전 처음 날짜 초기화
            DateTime currentDate = DateTime.Now;
            string currentDate_str = currentDate.ToString("yyyy-MM-dd");

            // 비교 날짜 저장
            // 바꿔줄 시간 저장 
            PlayerPrefs.SetString("Today_date", currentDate_str);
            PlayerPrefs.SetFloat("Today_StepTime_f", 0);
        }

    }

    private void Start()
    {
        //SoundManager.Instance.PlayBGM("1_MainTheme");

        gamePlaying = true;     // 게임 시작
        //RunnerPlayer1.instance.resetRotation();
    }
    void Update()
    {
        //게임 종료 할래요?
        if (Application.platform == RuntimePlatform.Android)
        {
            // 종료 팝업
            if (Input.GetKey(KeyCode.Escape))
            {
                Game_UIManager.instance.BlurCameraCanvas_ob.SetActive(true);
                Game_UIManager.instance.GameClosePopup_ob.SetActive(true);
            }
        }

        // 디버깅
        if (debugging)
        {
            //MoveCharacter(_stationDis);            
            if (float.TryParse(dist.text, out f_dist))
            {
                MoveCharacter(f_dist);
            }
        }
    }
    bool bPaused;
    void OnApplicationPause(bool pasue)
    {
        //일시정지
        if (pasue)
        {
            bPaused = true;
            //SaveProcessingData();
            // todo : 어플리케이션을 내리는 순간에 처리할 행동들 /

        }
        else
        {
            if (bPaused)
            {
                bPaused = false;

            }
        }
    }
    
    void SetTodayTime_Convert(Text _today)
    {
        todayTime_f += Time.deltaTime;

        int todayTime_hour = (int)(todayTime_f / 3600f);
        int todayTime_min = (int)(todayTime_f % 3600 / 60);
        int todayTime_sec = (int)(todayTime_f % 3600 % 60);
        int todayTime_msec = (int)((todayTime_f % 1) * 100);

        todayTimeinGame = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", todayTime_hour, todayTime_min, todayTime_sec, todayTime_msec);
        _today.text = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", todayTime_hour, todayTime_min, todayTime_sec, todayTime_msec);
        /*    if (todayTime_hour.Equals(0))
            {
                todayTimeinGame = string.Format("{0:00}:{1:00}.{2:00}", todayTime_min, todayTime_sec, todayTime_msec);
                _today.text = string.Format("{0:00}:{1:00}.{2:00}", todayTime_min, todayTime_sec, todayTime_msec);
                *//*  if (todayTime_min.Equals(0))
                  {

                      todayTimeinGame = string.Format("{0:00}:{1:00}.{2:00}", todayTime_min, todayTime_sec, todayTime_msec);
                      _today.text = string.Format("{0:00}:{1:00}.{2:00}", todayTime_min, todayTime_sec, todayTime_msec);

                  }
                  else
                  {
                      todayTimeinGame = string.Format("{0:00}:{1:00}.{2:00}", todayTime_min, todayTime_sec, todayTime_msec);
                      _today.text = string.Format("{0:00}:{1:00}.{2:00}", todayTime_min, todayTime_sec, todayTime_msec);
                  }*//*
            }
            else
            {
                todayTimeinGame = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", todayTime_hour, todayTime_min, todayTime_sec, todayTime_msec);
                _today.text = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", todayTime_hour, todayTime_min, todayTime_sec, todayTime_msec);
            }
    */


    }
    //기본 데이터 초기화
    void DataInitialization()
    {
        Debug.LogError("초기화");
        //gamePlaying = true;     // 게임 시작
        //averageAcceleration = Input.acceleration.magnitude; // Initialize average filter.
        //reachSpaceStation = false;
        nickName = PlayerPrefs.GetString("Player_NickName");
        timeCalDay = PlayerPrefs.GetInt("Player_ConnectDay");
        todayTime = PlayerPrefs.GetString("Today_StepTime");
        today_stepCount = PlayerPrefs.GetInt("Today_StepCount");
        once_stepCount = 0;
        //steps = 0;
        todayTimeinGame = "";
        PlayerPrefs.SetString("Today_TimeinGame", todayTimeinGame);
        todayKcal = PlayerPrefs.GetFloat("Today_Kcal");
        coin = PlayerPrefs.GetInt("Player_Coin");
        before_coin = PlayerPrefs.GetInt("Player_Coin"); 
        CollectionNumber = PlayerPrefs.GetInt("MoonPieceCount"); //달의 조각
        currentSection = PlayerPrefs.GetInt("Current_Section");
        //      PlayerPrefs.SetFloat("Moon_Distance", 1000-1000f);
        moonDis = PlayerPrefs.GetFloat("Moon_Distance");
        spaceStationDis = 5f;    // 저장된 시점부터 ex: 995km , 990km .. 989km 에서 끝냈다면 990km 에서 다시 시작 그러므로 무조건 5km or 10km 시작
        //today_Distance = 5f;
        once_Distance = 5f;
        Debug.Log("Today Step Time f : " + PlayerPrefs.GetFloat("Today_StepTime_f"));
    }

    //UI데이터 뿌리기
    public void SetPlayData(Text _nickName, Text _playDay, Text _coin, Text _todayTime, Text _todayKcal, Text _moonDis, Text _spacestationDis)
    {
        //DataInitialization();
        _nickName.text = nickName;

        string FirstLogin = PlayerPrefs.GetString("FirstLoginTime");

        DateTime FirstLoginDay = DateTime.ParseExact(FirstLogin, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        DateTime TodayLoginDay = DateTime.Now.AddDays(1);

        TimeSpan TimeCal = TodayLoginDay - FirstLoginDay;
        int timeCalDay = TimeCal.Days;
        //Debug.Log("FirstLogin" + timeCalDay);
        _playDay.text = timeCalDay + "DAYS";

        //_playDay.text = playDay + "DAYS";
        _coin.text = string.Format("{0:#,###}", coin);
        //  _todayTime.text = todayTime;
        SetTodayTime_Convert(_todayTime);
        _todayKcal.text = string.Format("{0:N1}", todayKcal);
        _moonDis.text = moonDis.ToString("N0");
        _spacestationDis.text = string.Format("{0:N1}", spaceStationDis);
    }

    public void SaveProcessingData()
    {
        PlayerPrefs.SetFloat("Today_Distance", once_Distance - spaceStationDis);
        PlayerPrefs.SetInt("Today_StepCount", today_stepCount + once_stepCount);
        //steps = 0;

        PlayerPrefs.SetInt("Player_ConnectDay", timeCalDay);
        PlayerPrefs.SetString("Today_StepTime", PlusTodayTime(todayTime));
        //PlayerPrefs.SetFloat("Today_Kcal", PlayerPrefs.GetFloat("Today_Kcal") + todayKcal);
        PlayerPrefs.SetFloat("Today_Kcal", todayKcal+once_Kcal);
        PlayerPrefs.SetInt("Player_Coin", coin);
        PlayerPrefs.SetFloat("Today_StepTime_f", todayTime_f);//오늘 걸은 시간

        // 현재 DAY, 총 거리 등등, 걸음수 업데이트
        PlayerPrefs.SetString("Total_StepTime", PlusTodayTime(PlayerPrefs.GetString("Total_StepTime")));
        PlayerPrefs.SetFloat("Total_Distance", PlayerPrefs.GetFloat("Total_Distance") + once_Distance);
        PlayerPrefs.SetFloat("Total_Kcal", PlayerPrefs.GetFloat("Total_Kcal") + once_Kcal);
        PlayerPrefs.SetInt("Total_StepCount", PlayerPrefs.GetInt("Total_StepCount") + once_stepCount);

        // 서버에 값 전달
        ServerManager.Instance.Reg_Proceeding_Record();
    }

    public void SavePlayData()
    {
        PlayerPrefs.SetFloat("Today_Distance", once_Distance - spaceStationDis);
        PlayerPrefs.SetInt("Today_StepCount", today_stepCount + once_stepCount);
        //steps = 0;

        PlayerPrefs.SetInt("Player_ConnectDay", timeCalDay);
        PlayerPrefs.SetString("Today_StepTime", PlusTodayTime(todayTime));
        PlayerPrefs.SetFloat("Today_Kcal", todayKcal+once_Kcal);
        PlayerPrefs.SetInt("Player_Coin", coin);
        PlayerPrefs.SetFloat("Today_StepTime_f", todayTime_f);//오늘 걸은 시간

        // 쉬는지점이나 달에 도착시에는 저장
        PlayerPrefs.SetInt("Current_Section", currentSection);
        PlayerPrefs.SetFloat("Moon_Distance", Mathf.FloorToInt(moonDis));
        //달의 조각 갯수
        PlayerPrefs.SetInt("MoonPieceCount", CollectionNumber); //조각 획득
        // 현재 DAY, 총 거리 etc , 걸음수, 업데이트
        PlayerPrefs.SetString("Total_StepTime", PlusTodayTime(PlayerPrefs.GetString("Total_StepTime")));
        PlayerPrefs.SetFloat("Total_Distance", PlayerPrefs.GetFloat("Total_Distance") + once_Distance);
        PlayerPrefs.SetFloat("Total_Kcal", PlayerPrefs.GetFloat("Total_Kcal") + once_Kcal);
        PlayerPrefs.SetInt("Total_StepCount", PlayerPrefs.GetInt("Total_StepCount") + once_stepCount);

        // 서버에 값 전달
        ServerManager.Instance.Reg_Result_Record();
    }

    private string PlusTodayTime(string _todayTime)
    {
        string[] sp = _todayTime.Split(new[] { ":", "." }, StringSplitOptions.RemoveEmptyEntries);

        DateTime dateTime = default(DateTime);
        Debug.Log("인덱스 : " + sp.Length);

        dateTime = dateTime.AddHours(int.Parse(sp[0]));
        dateTime = dateTime.AddMinutes(int.Parse(sp[1]));
        dateTime = dateTime.AddSeconds(int.Parse(sp[2]));
        dateTime = dateTime.AddMilliseconds(int.Parse(sp[3]));

        int todayTime_hour = (int)(todayTime_f / 3600f);
        int todayTime_min = (int)(todayTime_f % 3600 / 60);
        int todayTime_sec = (int)(todayTime_f % 3600 % 60);
        int todayTime_msec = (int)((todayTime_f % 1) * 100);

        dateTime = dateTime.AddHours(todayTime_hour);
        dateTime = dateTime.AddMinutes(todayTime_min);
        dateTime = dateTime.AddSeconds(todayTime_sec);
        dateTime = dateTime.AddMilliseconds(todayTime_msec);

        return dateTime.ToString("yyyy-MM-dd HH:mm:ss.ff").Substring(11);
    }

    public void PlayUIData(Text _nickName, Text _playDay, Text _coin, Text _todayTime, Text _todayKcal, Text _moonDis, Text _spacestationDis)
    {
        _nickName.text = nickName;

        string FirstLogin = PlayerPrefs.GetString("FirstLoginTime");

        DateTime FirstLoginDay = DateTime.ParseExact(FirstLogin, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        DateTime TodayLoginDay = DateTime.Now.AddDays(1);

        TimeSpan TimeCal = TodayLoginDay - FirstLoginDay;
        timeCalDay = TimeCal.Days;

        _playDay.text = timeCalDay.ToString() + "DAYS";

        _coin.text = string.Format("{0:#,###}", coin);
        SetTodayTime_Convert(_todayTime);
        _todayKcal.text = string.Format("{0:N1}", todayKcal + once_Kcal);
        _moonDis.text = moonDis.ToString("N0");
        _spacestationDis.text = string.Format("{0:N1}", spaceStationDis);
    }

    public void Reset_MyPosition(float s_Position, float m_Position)
    {
        spaceStationDis = s_Position;
        moonDis = m_Position;

        // 비율에 맞춘 거리
        //RunnerPlayer1.instance.playerCart.m_Position = Mathf.Lerp(0f, RunnerPlayer1.instance.cinemachineSmoothPath.PathLength * 2, Mathf.InverseLerp(0f, 5f, 5f - spaceStationDis));

        if (SceneManager.GetActiveScene().name == "Game 3" ||
            SceneManager.GetActiveScene().name == "Game 11")
        {
            RunnerPlayer1.instance.playerCart.m_Position = Mathf.Lerp(0f, RunnerPlayer1.instance.cinemachineSmoothPath.PathLength, Mathf.InverseLerp(0f, 5f, 5f - spaceStationDis));
        }
        else
        {
            RunnerPlayer1.instance.playerCart.m_Position = Mathf.Lerp(0f, RunnerPlayer1.instance.cinemachineSmoothPath.PathLength * 2, Mathf.InverseLerp(0f, 5f, 5f - spaceStationDis));
        }
    }

    public void MoveCharacter(float s_Position)
    {
        spaceStationDis = s_Position;

        // 비율에 맞춘 거리
        //RunnerPlayer1.instance.playerCart.m_Position = Mathf.Lerp(0f, RunnerPlayer1.instance.cinemachineSmoothPath.PathLength * 2, Mathf.InverseLerp(0f, 5f, 5f - spaceStationDis));

        if (SceneManager.GetActiveScene().name == "Game 3" ||
            SceneManager.GetActiveScene().name == "Game 11")
        {
            RunnerPlayer1.instance.playerCart.m_Position = Mathf.Lerp(0f, RunnerPlayer1.instance.cinemachineSmoothPath.PathLength, Mathf.InverseLerp(0f, 5f, 5f - spaceStationDis));
        }
        else
        {
            RunnerPlayer1.instance.playerCart.m_Position = Mathf.Lerp(0f, RunnerPlayer1.instance.cinemachineSmoothPath.PathLength * 2, Mathf.InverseLerp(0f, 5f, 5f - spaceStationDis));
        }

        debugging = false;
    }

    public void OnSelected_DebuggingMode()
    {
        debugging = true;
    }
}