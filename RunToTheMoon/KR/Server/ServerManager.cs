using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using System;
using UnityEngine.SceneManagement;

[System.Serializable]
public class UserInfo
{
    public string player_UID;
    public string player_ID;
    public string player_PW;
    public string player_NickName;
    public string player_LoginState;
    
    public UserInfo() { /*초기화*/ }

    // 순서대로 저장
    public UserInfo(string _player_UID, string _player_ID, string _player_PW, string _player_NickName, string _player_LoginState)
    {
        player_UID = _player_UID;
        player_ID = _player_ID;
        player_PW = _player_PW;
        player_NickName = _player_NickName;
        player_LoginState = _player_LoginState;
    }
}

[System.Serializable]
public class LobbyInfo
{
    public int player_Coin;
    public int player_ConnectDate;
    public int player_Current_Section;
    
    public LobbyInfo() { /*초기화*/ }

    // 순서대로 저장
    public LobbyInfo(int _player_Coin, int _player_ConnectDate, int _player_Current_Section)
    {
        player_Coin = _player_Coin;
        player_ConnectDate = _player_ConnectDate;
        player_Current_Section = _player_Current_Section;
    }
}

[System.Serializable]
public class DistanceTotheMoon
{
    public float distance;

    public DistanceTotheMoon() { /*초기화*/ }

    // 순서대로 저장
    public DistanceTotheMoon(float _distance)
    {
        distance = _distance;
    }
}

[System.Serializable]
public class DataByDate
{
    // 내용보고 수정
    public string daily_Date;
    public float today_Distance;
    public int today_StepCount;
    public string today_StepTime;
    public float today_Kcal;

    public DataByDate() { /*초기화*/ }

    // 순서대로 저장
    public DataByDate(string _daily_Date, float _today_Distance, int _today_StepCount, string _today_StepTime, float _today_Kcal)
    {
        daily_Date = _daily_Date;
        today_Distance = _today_Distance;
        today_StepTime = _today_StepTime;
        today_Kcal = _today_Kcal;
        today_StepCount = _today_StepCount;
    }
}

[System.Serializable]
public class DataByMonth
{
    // 내용보고 수정
    public string monthly_Date;
    public float month_Distance;
    public string month_StepTime;
    public float month_Kcal;
    public int month_StepCount;

    public DataByMonth() { /*초기화*/ }

    // 순서대로 저장
    public DataByMonth(string _monthly_Date, float _month_Distance, string _month_StepTime, float _month_Kcal, int _month_StepCount)
    {
        monthly_Date = _monthly_Date;
        month_Distance = _month_Distance;
        month_StepTime = _month_StepTime;
        month_Kcal = _month_Kcal;
        month_StepCount = _month_StepCount;
    }
}

[System.Serializable]
public class TotalData_To_Date
{
    // 내용보고 수정
    public float total_Distance;
    public string total_StepTime;
    public float total_Kcal;
    public int total_StepCount;
    public float moon_dist;

    public TotalData_To_Date() { /*초기화*/ }

    // 순서대로 저장
    public TotalData_To_Date(float _total_Distance, string _total_StepTime, float _total_Kcal, int _total_StepCount, float _moon_dist)
    {
        total_Distance = _total_Distance;
        total_StepTime = _total_StepTime;
        total_Kcal = _total_Kcal;
        total_StepCount = _total_StepCount;
        moon_dist = _moon_dist;
    }
}

[System.Serializable]
public class Company_INFO
{
    public string email;
    public string email_PW;

    public Company_INFO() { /*초기화*/ }

    // 순서대로 저장
    public Company_INFO(string _email, string _email_PW)
    {
        email = _email;
        email_PW = _email_PW;
    }
}

[System.Serializable]
public class Consumable_Item
{
    public int Item_SmallAirTank;
    public int Item_BigAirTank;
    public int Item_EnergyDrink;

    public Consumable_Item() { /*초기화*/ }

    // 순서대로 저장
    public Consumable_Item(int _Item_SmallAirTank, int _Item_BigAirTank, int _Item_EnergyDrink)
    {
        Item_SmallAirTank = _Item_SmallAirTank;
        Item_BigAirTank = _Item_BigAirTank;
        Item_EnergyDrink = _Item_EnergyDrink;
    }
}

[System.Serializable]
public class MoonPiece
{
    public int count;

    public MoonPiece() { /*초기화*/ }

    // 순서대로 저장
    public MoonPiece(int _count)
    {
        count = _count;
    }
}

[System.Serializable]
public class Attendance_Check
{
    public string once_Connected;
    public string morethantwice_Connected;

    public Attendance_Check() { /*초기화*/ }

    // 순서대로 저장
    public Attendance_Check(string _once_Connected, string _morethantwice_Connected)
    {
        once_Connected = _once_Connected;
        morethantwice_Connected = _morethantwice_Connected;
    }
}

[System.Serializable]
public class Rank
{
    //public string player_ID;
    public int ranking; // 랭킹
    public string nickname;
    public string arrival_time;

    public Rank() { /*초기화*/ }

    public Rank(int _ranking, string _nickname, string _arrival_time)
    {
        //player_ID = _player_ID;
        ranking = _ranking;
        nickname = _nickname;
        arrival_time = _arrival_time;
    }
}

public class ServerManager : MonoBehaviour
{
    /// <summary>
    /// AWS, PHP, MariaDB, SimpleJSON Plugin
    /// AWS EC2 FitTag Server IP 3.36.135.181
    /// </summary>
    public static ServerManager Instance { get; private set; }

    [Header("User")]
    public UserInfo userInfo;  // 유저정보

    [Header("Lobby Info")]
    public LobbyInfo lobbyInfo;  // 기본정보 : 코인 / 접속일 / 현재구간

    [Header("Consumable Item")]
    public Consumable_Item consumable_Item; // 소비성 아이템

    [Header("MoonPiece")]
    public MoonPiece moonPiece;

    [Header("Graph Data")]
    public List<DataByDate> dataByDate;
    public List<DataByMonth> dataByMonth;
    public TotalData_To_Date totalData_To_Date;

    [Header("Attendance_Check")]
    public Attendance_Check attendance_Check;

    [Header("Company_INFO")]
    public Company_INFO company_Info;

    [Header("Rank")]
    public List<Rank> Ranking = new List<Rank>();    // 랭킹존 리스트

    public Text serverLogtext;

    public bool isConnected = false;    // 서버에 연결되었나 확인
    public bool isConnCompleted = false;    // 서버 연결상태체크 완료 확인
    public bool isUserSearchCompleted = false;    // 서버 USER SEARCH SQL 후 완료 상태
    public bool isCharacterDataStackCompleted = false;    // 서버  SEARCH SQL 후 완료 상태
    public bool isStoreItemStackCompleted = false;    // 서버  SEARCH SQL 후 완료 상태
    public bool isExistNickName = false;    // 서버  SEARCH SQL 후 완료 상태
    public bool isNickNameSearchCompleted = false;  // 서버 닉네임 서치 SQL 완료 상태
    public bool isUserNickNameRegUpdate = false;    // 서버 닉네임 생성 및 로그인 상태 업데이트
    public bool isUserInfoRegCompleted = false;      // 유저 등록 완료
    public bool isGetDateDataCompleted = false;      // 일별 데이터 가져오기 완료
    public bool isGetMonthDataCompleted = false;        // 월별 데이터 가져오기 완료
    public bool isExistID = false;                  // 서버 ID 존재여부
    public bool isIDSearchCompleted = false;          // 서버 ID 존재여부 검색완료 상태체크
    public bool isDailyDateExisted = false;          // 서버에 날짜(Daily Date) 존재여부확인체크
    public bool isDailyDateSearchingCompleted = false;          // 서버에 날짜(Daily Date) 존재여부확인 완료
    public bool isGetMyProfileDataCompleted = false;          // 초기화 데이터 완료 상태 
    public bool isCompanyEmailSearchCompleted = false;          // 서버에 회사 이메일 검색 완료
    public bool isGetRankingCompleted = false;          
    

    public GameObject networkConnectStatePopUp;

    void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void Get_CompanyEmail()
    {
        StartCoroutine(_Get_CompanyEmail());
    }

    IEnumerator _Get_CompanyEmail()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Get_CompanyEmail.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                _Get_CompanyEmail(www.downloadHandler.text);
            }
        }
    }

    private void _Get_CompanyEmail(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            for (int i = 0; i < Array.Count; i++)
            {
                company_Info = new Company_INFO(Array[i]["COMPANY_EMAIL"].Value, Array[i]["COMPANY_PW"].Value);
            }
        }
        else
        {
            Debug.Log("값이 존재하지 않습니다.");
        }

        isCompanyEmailSearchCompleted = true;
    }

    public void PasswordChange(string _pw)
    {
        StartCoroutine(_PasswordChange(_pw));
    }

    IEnumerator _PasswordChange(string _pw)
    {
        WWWForm form = new WWWForm();

        // ID 
        form.AddField("Player_ID", PlayerPrefs.GetString("Player_ID"));
        form.AddField("Player_Password", _pw);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Update_PasswordChange.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log("변경완료");
                PlayerPrefs.SetString("Player_Password", _pw);   //비번 새로 저장
            }
        }
    }

    // 첫 구글 로그인시 아이디 있는지 없는지 검사하기
    public void Search_USER_Info(string _userID)
    {
        StartCoroutine(_Search_USER_Info(_userID));
    }

    IEnumerator _Search_USER_Info(string _userID)
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_ID", _userID);
        //form.AddField("Player_ID", "1234");

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Search_UserInfo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                GetSearched_USER_Info(www.downloadHandler.text);
            }
        }
    }

    // 아이디 검색해서 가져옴
    public void GetSearched_USER_Info(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            for (int i = 0; i < Array.Count; i++)
            {
                // 1. 랭커 정보 클래스에 저장 > Rank
                //highRanker.Add(new Rank(Int32.Parse(Array[i]["RANKING"].Value), Array[i]["NICKNAME"].Value, Int32.Parse(Array[i]["TOTALSCORE"].Value)));
                //Debug.Log("PLAYER_ID : " + Array[i]["PLAYER_ID"].Value);

                //public string player_ID;
                //public string player_UID;
                //public string player_PW;
                //public string player_NickName;
                //public string player_LoginState;

                userInfo = new UserInfo(Array[i]["PLAYER_UID"].Value, Array[i]["PLAYER_ID"].Value,
                                        Array[i]["PLAYER_PW"].Value,  Array[i]["PLAYER_NICKNAME"].Value,
                                        Array[i]["PLAYER_LOGINSTATE"].Value);
            }
        }
        else
        {
            Debug.Log("값이 존재하지 않습니다.");
            SoundFunction.Instance.Warning_Sound();
        }

        //serverLogtext.text = userInfo.player_ID +" :: "+ userInfo.player_UID +" :: "+ userInfo.player_PW + " :: " +userInfo.player_NickName + " :: "+userInfo.player_LoginState;

        isUserSearchCompleted = true;
    }

    // 아이디/PW 등록하기
    public void UserInfo_Reg()
    {
        StartCoroutine(_UserInfo_Reg());
    }

    IEnumerator _UserInfo_Reg()
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_LoginState", PlayerPrefs.GetString("Player_LoginState"));
        form.AddField("Player_ID", PlayerPrefs.GetString("Player_ID"));
        form.AddField("Player_UID", PlayerPrefs.GetString("Player_UID"));
        form.AddField("Player_Password", PlayerPrefs.GetString("Player_Password"));

        Debug.Log("유저 아디 만듦");
        Debug.Log(PlayerPrefs.GetString("Player_LoginState"));
        Debug.Log(PlayerPrefs.GetString("Player_ID"));
        Debug.Log(PlayerPrefs.GetString("Player_UID"));
        Debug.Log(PlayerPrefs.GetString("Player_Password"));

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Reg_UserInfo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }

        isUserInfoRegCompleted = true;
    }

    // 아이디/PW 등록하기
    public void Delete_UserInfo()
    {
        StartCoroutine(_Delete_UserInfo());
    }

    IEnumerator _Delete_UserInfo()
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_UID", PlayerPrefs.GetString("Player_UID"));

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Delete_UserInfo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }

        isUserInfoRegCompleted = true;
    }

    // ID 중복 체크
    public void ID_DoubleCheck(string _userID)
    {
        StartCoroutine(_ID_DoubleCheck(_userID));
    }

    IEnumerator _ID_DoubleCheck(string _userID)
    {
        WWWForm form = new WWWForm();

        // ID
        form.AddField("Player_ID", _userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_ID_DoubleCheck.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Get_ID_DoubleCheckJSON(www.downloadHandler.text);
            }
        }
    }

    public void Get_ID_DoubleCheckJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            isExistID = true;
        }
        else
        {
            Debug.Log("값이 존재하지 않습니다.");
            isExistID = false;
        }

        isIDSearchCompleted = true;
    }

    // 닉네임 중복 체크
    public void NickNameDoubleCheck(string _nickname)
    {
        StartCoroutine(_NickNameDoubleCheck(_nickname));
    }

    IEnumerator _NickNameDoubleCheck(string _nickname)
    {
        WWWForm form = new WWWForm();

        // ID 
        form.AddField("Player_NickName", _nickname);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_NickName_DoubleCheck.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                GetNickNameDoubleCheckJSON(www.downloadHandler.text);
            }
        }
    }

    public void GetNickNameDoubleCheckJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            isExistNickName = true;
        }
        else
        {
            Debug.Log("값이 존재하지 않습니다.");
            isExistNickName = false;
        }

        isNickNameSearchCompleted = true;
    }

    // 닉네임 등록 및 로그인상태 등록
    public void UserNickNameInfo_Reg()
    {
        StartCoroutine(_UserNickNameInfo_Reg());
    }

    IEnumerator _UserNickNameInfo_Reg()
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_ID", PlayerPrefs.GetString("Player_ID"));
        form.AddField("Player_LoginState", PlayerPrefs.GetString("Player_LoginState"));
        form.AddField("Player_NickName", PlayerPrefs.GetString("Player_NickName"));
        
        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Reg_UserNickNameInfo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }

        isUserNickNameRegUpdate = true;
    }

    // 로그인 상태 업데이트
    public void UserLoginState_Update()
    {
        StartCoroutine(_UserLoginState_Update());
    }

    IEnumerator _UserLoginState_Update()
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_ID", PlayerPrefs.GetString("Player_ID"));
        form.AddField("Player_LoginState", PlayerPrefs.GetString("Player_LoginState"));

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Update_UserLoginState.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }


    // 캐릭터 정보 저장
    public void CharacterInfo_Reg()
    {
        StartCoroutine(_CharacterInfo_Reg());
    }

    IEnumerator _CharacterInfo_Reg()
    {
        WWWForm form = new WWWForm();

        // 현재 유저의 ID
        form.AddField("Player_ID", PlayerPrefs.GetString("Player_ID"));
        form.AddField("Player_LoginState", PlayerPrefs.GetString("Player_LoginState"));
        form.AddField("Player_NickName", PlayerPrefs.GetString("Player_NickName"));

        //캐릭터 정보
        form.AddField("Player_Coin", PlayerPrefs.GetInt("Player_Coin"));
        form.AddField("Player_ConnectDay", PlayerPrefs.GetInt("Player_ConnectDay"));
        form.AddField("Current_Section", PlayerPrefs.GetInt("Current_Section"));

        form.AddField("Today_Distance", PlayerPrefs.GetFloat("Today_Distance").ToString());
        form.AddField("Today_StepCount", PlayerPrefs.GetInt("Today_StepCount"));
        form.AddField("Today_StepTime", PlayerPrefs.GetString("Today_StepTime"));
        form.AddField("Today_Kcal", PlayerPrefs.GetFloat("Today_Kcal").ToString());
        form.AddField("Moon_Distance", PlayerPrefs.GetFloat("Moon_Distance").ToString());

        form.AddField("Month_Distance", PlayerPrefs.GetFloat("Month_Distance").ToString());
        form.AddField("Month_StepCount", PlayerPrefs.GetInt("Month_StepCount"));
        form.AddField("Month_StepTime", PlayerPrefs.GetString("Month_StepTime"));
        form.AddField("Month_Kcal", PlayerPrefs.GetFloat("Month_Kcal").ToString());

        form.AddField("Total_Distance", PlayerPrefs.GetFloat("Total_Distance").ToString());
        form.AddField("Total_StepCount", PlayerPrefs.GetInt("Total_StepCount"));
        form.AddField("Total_StepTime", PlayerPrefs.GetString("Total_StepTime"));
        form.AddField("Total_Kcal", PlayerPrefs.GetFloat("Total_Kcal").ToString());

        form.AddField("Item_SmallAirTank", PlayerPrefs.GetInt("Item_SmallAirTank"));
        form.AddField("Item_BigAirTank", PlayerPrefs.GetInt("Item_BigAirTank"));
        form.AddField("Item_EnergyDrink", PlayerPrefs.GetInt("Item_EnergyDrink"));

        form.AddField("MoonPieceCount", PlayerPrefs.GetInt("MoonPieceCount"));

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Reg_CharacterInfo.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    // 일별 날짜 초기화 : 로그인시 데이터가 없으면 서버에 값 넣음
    public void Reg_InitDateData(string _userID, string _currentDate)
    {
        StartCoroutine(_Reg_InitDateData(_userID, _currentDate));
    }

    IEnumerator _Reg_InitDateData(string _userID, string _currentDate)
    {
        WWWForm form = new WWWForm();

        // 현재 유저의 ID
        form.AddField("Player_ID", _userID);
        form.AddField("CurrentDate", _currentDate);
        
        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Reg_InitDateData.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    // 일별 날짜 들고오기
    public void Get_DateData(int Year, int Month, int today, int startDay)
    {
        StartCoroutine(_Get_DateData(Year, Month, today, startDay));
    }

    // 처음 가입 날짜, 오늘날짜, 이번달, 이번년
    IEnumerator _Get_DateData(int _thisYear, int _thisMonth, int _today, int _startDay)
    {
        WWWForm form = new WWWForm(); 
        string currentDate;
        string startDate;

        // 현재 유저의 ID
        form.AddField("Player_ID", PlayerPrefs.GetString("Player_ID"));
        //form.AddField("Player_ID", "yeobi27@naver.com");

        // 날짜 정보
        if (_startDay == 0)
        {
            // 가입날짜가 이번년/월이 아니면
            // 가입날짜
            startDate = _thisYear + "-" + _thisMonth + "-" + (_startDay + 1);
            // 현재날짜
            currentDate = _thisYear + "-" + _thisMonth + "-" + _today;

            form.AddField("StartDate", startDate);
            form.AddField("CurrentDate", currentDate);
            //Debug.Log("startDate : " + startDate);
            //Debug.Log("currentDate : " + currentDate);
        }
        else
        {
            // 가입날짜
            startDate = _thisYear + "-" + _thisMonth + "-" + _startDay;
            // 현재날짜
            currentDate = _thisYear + "-" + _thisMonth + "-" + _today;

            form.AddField("StartDate", startDate);
            form.AddField("CurrentDate", currentDate);
            //Debug.Log("startDate : " + startDate);
            //Debug.Log("currentDate : " + currentDate);
        }


        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Get_DateData.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                _Get_DateData(www.downloadHandler.text, currentDate);
            }
        }
    }

    public void _Get_DateData(string _jsonData, string _currentDate)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            dataByDate.Clear();
            for (int i = 0; i < Array.Count; i++)
            {
                dataByDate.Add(new DataByDate(Array[i]["DAILY_DATE"].Value, float.Parse(Array[i]["TODAY_DISTANCE"].Value),
                                              int.Parse(Array[i]["TODAY_STEPCOUNT"].Value), Array[i]["TODAY_STEPTIME"].Value,
                                              float.Parse(Array[i]["TODAY_KCAL"].Value)));
            }
        }
        else
        {
            dataByDate.Clear();
            Debug.Log("값이 존재하지 않습니다.");
            // 오늘 날짜로 검색이 안된다면 Insert 하고 ServerManager 에 값을 넣어둬야함
            dataByDate.Add(new DataByDate(_currentDate, 0, 0, "00:00:00.00", 0));
            Reg_InitDateData(PlayerPrefs.GetString("Player_ID"), _currentDate);
        }

        isGetDateDataCompleted = true;
    }

    // 월별 날짜 들고오기
    public void Get_MonthData(int year, int startMonth, int endMonth)
    {
        StartCoroutine(_Get_MonthData(year, startMonth, endMonth));
    }

    // 처음 가입 날짜, 오늘날짜, 이번달, 이번년
    IEnumerator _Get_MonthData(int _year, int _startMonth, int _endMonth)
    {
        WWWForm form = new WWWForm();

        // 현재 유저의 ID
        form.AddField("Player_ID", PlayerPrefs.GetString("Player_ID"));
        //form.AddField("Player_ID", "yeobi27@naver.com");

        string startMonth = _year + "-" + _startMonth + "-01";
        string endMonth = _year + "-" + _endMonth + "-01";

        form.AddField("StartMonth", startMonth);
        form.AddField("EndMonth", endMonth);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Get_MonthData.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                _Get_MonthData(www.downloadHandler.text);
            }
        }
    }

    public void _Get_MonthData(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            dataByMonth.Clear();
            for (int i = 0; i < Array.Count; i++)
            {
                // 1. 랭커 정보 클래스에 저장 > Rank
                //highRanker.Add(new Rank(Int32.Parse(Array[i]["RANKING"].Value), Array[i]["NICKNAME"].Value, Int32.Parse(Array[i]["TOTALSCORE"].Value)));
                //Debug.Log("PLAYER_ID : " + Array[i]["PLAYER_ID"].Value);

                dataByMonth.Add(new DataByMonth(Array[i]["MONTHLY_DATE"].Value, float.Parse(Array[i]["MONTH_DISTANCE"].Value),
                                        Array[i]["MONTH_STEPTIME"].Value, float.Parse(Array[i]["MONTH_KCAL"].Value),
                                        int.Parse(Array[i]["MONTH_STEPCOUNT"].Value)));
            }
        }
        else
        {
            Debug.Log("값이 존재하지 않습니다.");
        }

        isGetMonthDataCompleted = true;
    }



    // 일별 날짜 들고오기
    public void Get_MyProfileData(string _userID)
    {
        StartCoroutine(_Get_MyProfileData(_userID));
    }

    // 처음 가입 날짜, 오늘날짜, 이번달, 이번년
    IEnumerator _Get_MyProfileData(string _userID)
    {
        WWWForm form = new WWWForm();

        // 현재 유저의 ID
        form.AddField("Player_ID", _userID);
        //form.AddField("Player_ID", "yeobi27@naver.com");

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Get_LobbyInitData.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                _Get_MyProfileDataJSON(www.downloadHandler.text);
            }
        }
    }

    private void _Get_MyProfileDataJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            for (int i = 0; i < Array.Count; i++)
            {
                lobbyInfo = new LobbyInfo(int.Parse(Array[i]["PLAYER_COIN"].Value), int.Parse(Array[i]["PLAYER_CONNECTDAY"].Value), int.Parse(Array[i]["PLAYER_CURRENTSECTION"].Value));
                totalData_To_Date = new TotalData_To_Date(float.Parse(Array[i]["TOTAL_DISTANCE"].Value), Array[i]["TOTAL_STEPTIME"].Value, float.Parse(Array[i]["TOTAL_KCAL"].Value), int.Parse(Array[i]["TOTAL_STEPCOUNT"].Value), float.Parse(Array[i]["MOON_DISTANCE"].Value));
                consumable_Item = new Consumable_Item(int.Parse(Array[i]["ITEM_SMALLAIRTANK"].Value), int.Parse(Array[i]["ITEM_BIGAIRTANK"].Value), int.Parse(Array[i]["ITEM_ENERGYDRINK"].Value));
                moonPiece = new MoonPiece(int.Parse(Array[i]["MOONPIECECOUNT"].Value));
            }
        }
        else
        {
            Debug.Log("값이 존재하지 않습니다.");
        }

        isGetMyProfileDataCompleted = true;
    }

    // 오늘 기록이 있는지 확인
    // 기록이 있으면 업데이트, 없으면 등록 후 업데이트
    public void Searching_DailyDate()
    {
        StartCoroutine(_Searching_DailyDate());
    }

    IEnumerator _Searching_DailyDate()
    {
        WWWForm form = new WWWForm();
        // 현재 유저의 ID
        form.AddField("Player_ID", PlayerPrefs.GetString("Player_ID"));
        //form.AddField("Player_ID", "yeobi27@naver.com");
        string todayDate = DateTime.Now.ToString("yyyy-MM-dd"); //2021-05-03
        form.AddField("Daily_Date", todayDate);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Searching_DailyDate.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                _Searching_DailyDate(www.downloadHandler.text);
            }
        }
    }

    public void _Searching_DailyDate(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            isDailyDateExisted = true;
        }
        else
        {
            Debug.Log("값이 존재하지 않습니다.");
            isDailyDateExisted = false;
        }

        isDailyDateSearchingCompleted = true;
    }

    // 게임 휴게소 중간에 저장하는 함수
    public void Reg_Result_Record()
    {
        StartCoroutine(_Reg_Result_Record());
    }

    IEnumerator _Reg_Result_Record()
    {
        WWWForm form = new WWWForm();

        // isDailyDateExisted 체크
        Searching_DailyDate();
        yield return new WaitUntil(() => isDailyDateSearchingCompleted);
        isDailyDateSearchingCompleted = false;

        string todayDate = DateTime.Now.ToString("yyyy-MM-dd"); //2021-05-03
        form.AddField("Daily_Date", todayDate);
        string monthDate = DateTime.Now.ToString("yyyy-MM") + "-01";
        form.AddField("Monthly_Date", monthDate);
        // 현재 유저의 ID
        form.AddField("Player_ID", PlayerPrefs.GetString("Player_ID"));

        form.AddField("Player_Coin", PlayerPrefs.GetInt("Player_Coin"));
        form.AddField("Player_ConnectDay", PlayerPrefs.GetInt("Player_ConnectDay"));
        form.AddField("Current_Section", PlayerPrefs.GetInt("Current_Section"));

        // 전체기록
        form.AddField("Total_Distance", PlayerPrefs.GetFloat("Total_Distance").ToString());
        form.AddField("Total_StepTime", PlayerPrefs.GetString("Total_StepTime"));
        form.AddField("Total_Kcal", PlayerPrefs.GetFloat("Total_Kcal").ToString());
        form.AddField("Total_StepCount", PlayerPrefs.GetInt("Total_StepCount"));
        form.AddField("Moon_Distance", PlayerPrefs.GetFloat("Moon_Distance").ToString());

        // 오늘기록
        form.AddField("Today_Distance", PlayerPrefs.GetFloat("Today_Distance").ToString());
        form.AddField("Today_StepTime", PlayerPrefs.GetString("Today_StepTime"));
        form.AddField("Today_Kcal", PlayerPrefs.GetFloat("Today_Kcal").ToString());
        form.AddField("Today_StepCount", PlayerPrefs.GetInt("Today_StepCount"));

        // 월 기록
        form.AddField("Month_Distance", PlayerPrefs.GetFloat("Month_Distance").ToString());
        form.AddField("Month_StepTime", PlayerPrefs.GetString("Month_StepTime"));
        form.AddField("Month_Kcal", PlayerPrefs.GetFloat("Month_Kcal").ToString());
        form.AddField("Month_StepCount", PlayerPrefs.GetInt("Month_StepCount"));

        form.AddField("Item_SmallAirTank", PlayerPrefs.GetInt("Item_SmallAirTank"));
        form.AddField("Item_BigAirTank", PlayerPrefs.GetInt("Item_BigAirTank"));
        form.AddField("Item_EnergyDrink", PlayerPrefs.GetInt("Item_EnergyDrink"));

        form.AddField("MoonPieceCount", PlayerPrefs.GetInt("MoonPieceCount"));

        if (isDailyDateExisted)
        {
            using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Update_GameResultData.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                }
            }
        }
        else
        {
            using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Reg_GameResultData.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                }
            }
        }
    }
    // 게임도중 종료시 데이터 저장하는 함수
    public void Reg_Proceeding_Record()
    {
        StartCoroutine(_Reg_Proceeding_Record());
    }

    IEnumerator _Reg_Proceeding_Record()
    {
        WWWForm form = new WWWForm();

        // isDailyDateExisted 체크
        Searching_DailyDate();
        yield return new WaitUntil(() => isDailyDateSearchingCompleted);
        isDailyDateSearchingCompleted = false;

        string todayDate = DateTime.Now.ToString("yyyy-MM-dd"); //2021-05-03
        form.AddField("Daily_Date", todayDate);
        string monthDate = DateTime.Now.ToString("yyyy-MM") + "-01";
        form.AddField("Monthly_Date", monthDate);

        // 현재 유저의 ID
        form.AddField("Player_ID", PlayerPrefs.GetString("Player_ID"));

        form.AddField("Player_Coin", PlayerPrefs.GetInt("Player_Coin"));    // 이미 다 계산된 값 > 서버에는 그대로 Update 적용
        form.AddField("Player_ConnectDay", PlayerPrefs.GetInt("Player_ConnectDay"));
        //form.AddField("Current_Section", PlayerPrefs.GetInt("Current_Section"));

        // 전체기록
        //form.AddField("Total_Distance", PlayerPrefs.GetFloat("Total_Distance").ToString());
        //form.AddField("Total_StepTime", PlayerPrefs.GetString("Total_StepTime"));
        //form.AddField("Total_Kcal", PlayerPrefs.GetFloat("Total_Kcal").ToString());
        //form.AddField("Total_StepCount", PlayerPrefs.GetInt("Total_StepCount"));
        //form.AddField("Moon_Distance", PlayerPrefs.GetFloat("Moon_Distance").ToString());

        // 오늘기록
        form.AddField("Today_Distance", PlayerPrefs.GetFloat("Today_Distance").ToString());
        form.AddField("Today_StepTime", PlayerPrefs.GetString("Today_StepTime"));
        form.AddField("Today_Kcal", PlayerPrefs.GetFloat("Today_Kcal").ToString());
        form.AddField("Today_StepCount", PlayerPrefs.GetInt("Today_StepCount"));

        // 월 기록
        form.AddField("Month_Distance", PlayerPrefs.GetFloat("Month_Distance").ToString());
        form.AddField("Month_StepTime", PlayerPrefs.GetString("Month_StepTime"));
        form.AddField("Month_Kcal", PlayerPrefs.GetFloat("Month_Kcal").ToString());
        form.AddField("Month_StepCount", PlayerPrefs.GetInt("Month_StepCount"));

        form.AddField("Item_SmallAirTank", PlayerPrefs.GetInt("Item_SmallAirTank"));
        form.AddField("Item_BigAirTank", PlayerPrefs.GetInt("Item_BigAirTank"));
        form.AddField("Item_EnergyDrink", PlayerPrefs.GetInt("Item_EnergyDrink"));

        // 현재 등록되어있는 값이 있음.
        if (isDailyDateExisted)
        {
            using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Update_GameProcessingData.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                }
            }
        }
        else
        {// 현재 등록되어있는 값이 없음.
            using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Reg_GameProcessingData.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                }
            }
        }


        SceneManager.LoadScene("Lobby");
    }


    // 랭킹정보 등록
    public void Reg_Ranking(string nickName)
    {
        StartCoroutine(_Reg_Ranking(nickName));
    }

    IEnumerator _Reg_Ranking(string _nickName)
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_NickName", _nickName);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Reg_Ranking.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public void Get_Ranking()
    {
        StartCoroutine(_Get_Ranking());
    }

    IEnumerator _Get_Ranking()
    {
        WWWForm form = new WWWForm();

        //form.AddField("Search_Amount", _amount);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Get_Ranking.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                GetRankingJSON(www.downloadHandler.text);
            }
        }
    }

    //// Get Ranking JSON
    public void GetRankingJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            Ranking.Clear();
            for (int i = 0; i < Array.Count; i++)
            {
                // 1. 랭커 정보 클래스에 저장 > Rank
                Ranking.Add(new Rank(Int32.Parse(Array[i]["RANKING"].Value), Array[i]["NICKNAME"].Value,
                                    Array[i]["ARRIVAL_TIME"].Value));
            }
        }
        else
        {
            Debug.Log("값이 존재하지 않습니다.");
        }

        isGetRankingCompleted = true;
    }

    // 닉네임 업데이트 ( 변경 )
    public void Update_NickName(string wantNickName)
    {
        StartCoroutine(_Update_NickName(wantNickName));
    }

    IEnumerator _Update_NickName(string _wantNickName)
    {
        WWWForm form = new WWWForm();

        form.AddField("Player_ID", PlayerPrefs.GetString("Player_ID"));
        form.AddField("Player_NickName", _wantNickName);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Update_NickName.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
            }
        }
    }

    // 코인 업데이트
    public void Update_Coin(string _operation, int _coin)
    {
        StartCoroutine(_Update_Coin(_operation, _coin));
    }

    IEnumerator _Update_Coin(string _operation, int _coin)
    {
        WWWForm form = new WWWForm();

        if (_operation.Equals("+"))
        {
            form.AddField("Way", "0");
        }
        else if (_operation.Equals("-"))
        {
            form.AddField("Way", "1");
        }

        form.AddField("Player_ID", PlayerPrefs.GetString("Player_ID"));
        form.AddField("Coin", _coin.ToString());

        lobbyInfo.player_Coin = _coin;

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Update_Coin.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public void Update_Item(string player_id, string itemKind, int itemCount)
    {
        StartCoroutine(_Update_Item(player_id, itemKind, itemCount));
    }

    IEnumerator _Update_Item(string player_id, string _itemKind, int _itemCount)
    {
        WWWForm form = new WWWForm();

        if (_itemKind.Equals("Small"))
        {
            form.AddField("Way", "0");
        }
        else if (_itemKind.Equals("Big"))
        {
            form.AddField("Way", "1");
        }
        else if (_itemKind.Equals("Energy"))
        {
            form.AddField("Way", "2");
        }

        form.AddField("Player_ID", player_id);
        form.AddField("Amount", _itemCount);

        using (UnityWebRequest www = UnityWebRequest.Post("http://3.36.135.181/fitness_running/fb_Update_Item.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
            }
        }
    }


    /***************************************************************************************/

    public void GetNetworkState()
    {
        StartCoroutine(_GetNetworkState());
    }

    IEnumerator _GetNetworkState()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_rm/bmp_two_conn_state.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                isConnected = false;
                // 여기다 네트워크 연결안됬다 팝업창 띄우기
                NetworkErrorPopUp();
           
            }
            else
            {
                isConnected = true;
                Debug.Log(www.downloadHandler.text);
            }
        }

        isConnCompleted = true;
    }

    private void NetworkErrorPopUp()
    {
        SoundFunction.Instance.Warning_Sound();
        networkConnectStatePopUp.SetActive(true);
        StartCoroutine(_NetworkErrorPopUp());
    }

    IEnumerator _NetworkErrorPopUp()
    {
        WaitForSeconds ws = new WaitForSeconds(0.8f);
        yield return ws;
        networkConnectStatePopUp.SetActive(false);
    }
}