using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_DataManager : MonoBehaviour
{
    public static Lobby_DataManager instance { get; private set; }

    public Text[] toDayText;    //스크롤뷰에 있는 날짜들
    public string[] toDayArr;   //오늘 날짜 배열
    public string strSub;   //분리한 문자 변수

    int maxDay = 12;    //보여줄 날짜 최소한의 숫자
    string toDay;
    bool nullState, specialState, overlapState , unavailableState;  // 널값, 특수문자 사용
    string oneConnect, twoConnect;    //처음접속, 두번째접속



    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }


    void Start()
    {
        //Debug.Log("loginState" + PlayerPrefs.GetString("Player_LoginState"));
        //Debug.Log("loginState" + PlayerPrefs.GetString("Player_ID"));
        //MyDateSave();   // 초기화 데이터
        GetTodayInitialization(); //오늘 날짜 초기화

        TodayTextColorInitialization(); //오늘 날짜 텍스트 색상 변경 함수
        TodayTextSetActionInitialization(); //날짜 활성화 비활성화함수
        ConnectDay();
    }

    //오늘 몇번째 접속했는지 함수
    void ConnectDay()
    {
        //PlayerPrefs.SetString("OneCennectDay", "");    //처음 접속이라서  set 해준다.
        //PlayerPrefs.SetString("TwoCennectDay", ""); //두번째 접속이라 두번째에 set 해준다.
        //PlayerPrefs.SetInt("Player_CennectDay", 0);
        oneConnect = PlayerPrefs.GetString("OneConnectDay");
        //처음 접속 했을 때 - 처음 접속한 기록이 없음.oneday set을 해준적이 없으므로
        if (oneConnect == "")
        {
            PlayerPrefs.SetString("OneConnectDay", toDayArr[2]);    // toDayArr[2] 날짜저장 변수 > 처음 접속이라서  set 해준다.
            oneConnect = PlayerPrefs.GetString("OneConnectDay");

            //일별이기 때문에 기록 0
            PlayerPrefs.SetInt("Today_StepCount", 0);
            PlayerPrefs.SetFloat("Today_Distance", 0);
            PlayerPrefs.SetString("Today_StepTime", "0:00:00.00");     //일별 걸은시간
            PlayerPrefs.SetFloat("Today_Kcal", 0);  //일별 칼로리
        }

        if (oneConnect != "")
        {   // 날짜가 있으면 완전 처음 접속은 아님
            PlayerPrefs.SetString("TwoConnectDay", toDayArr[2]);    // toDayArr[2] 날짜저장 변수 > 두번째 접속이라 두번째에 set 해준다. 
            twoConnect = PlayerPrefs.GetString("TwoConnectDay");
        }

        //저번에 들어왔을 때랑 날짜가 다를때(오늘 처음 들어왔다는 듯)
        if (oneConnect != twoConnect)
        {
            Debug.Log("oneConnect : " + oneConnect);
            Debug.Log("twoConnect : " + twoConnect);

            accessDay_i = PlayerPrefs.GetInt("Player_ConnectDay"); //접속일자 : 1,2,3 Day..
            accessDay_i++;  //접속날짜를 올려준다.
            PlayerPrefs.SetInt("Player_ConnectDay", accessDay_i);// 저장
            Debug.Log("접속일 : " + accessDay_i);

            PlayerPrefs.SetString("OneConnectDay", twoConnect); //두번째 접속한 날짜를 앞으로 옮겨준다. 그래야 다음 들어왔을 때 또 비교가능

            //일별이기 때문에 기록 0
            PlayerPrefs.SetInt("Today_StepCount", 0);
            PlayerPrefs.SetFloat("Today_Distance", 0);
            PlayerPrefs.SetString("Today_StepTime", "0:00:00.00");    //일별 걸은시간
            PlayerPrefs.SetFloat("Today_Kcal", 0);  //일별 칼로리
        }
        else
        {
            accessDay_i = PlayerPrefs.GetInt("Player_ConnectDay"); //접속일
            todayDistance = PlayerPrefs.GetFloat("Today_Distance"); // 오늘 걸은 거리
            todayStep_i = PlayerPrefs.GetInt("Today_StepCount");   //일별 걸음수
            todayTime_s = PlayerPrefs.GetString("Today_StepTime");    //일별 걸은시간
            todayKcal_f = PlayerPrefs.GetFloat("Today_Kcal");  //일별 칼로리
        }

    }


    //오늘 날짜 초기화
    void GetTodayInitialization()
    {
        toDay = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        toDayArr = new string[4];
        toDayArr[0] = DateTime.Now.ToString("yyyy");
        toDayArr[1] = DateTime.Now.ToString("MM");
        toDayArr[2] = DateTime.Now.ToString("dd");
        toDayArr[3] = DateTime.Now.ToString("HH-mm-ss");

        strSub = toDayArr[2].Substring(0, 1);

        //1~9까지 앞에 0이 붙어서 그걸 뺀 문자 저장
        if (strSub == "0")
            toDayArr[2] = toDayArr[2].Substring(1, 1);
    }


    //오늘 날짜 텍스트 색상 변경 함수
    public void TodayTextColorInitialization()
    {
        for (int i = 0; i < toDayText.Length; i++)
        {
            if (toDayText[i].text == toDayArr[2])
            {
                toDayText[i].text = "<color=#ffff00>" + toDayArr[2] + "</color>";
            }
        }
    }


    //날짜 활성화 비활성화함수
    public void TodayTextSetActionInitialization()
    {
        int today_i = int.Parse(toDayArr[2]);   //오늘날짜 int
        //이 달의 마지막 날
        int lastDay = DateTime.DaysInMonth(int.Parse(toDayArr[0]), int.Parse(toDayArr[1]));


        if (today_i <= maxDay)
            maxDay = 12;
        else
            maxDay = maxDay + (today_i - maxDay); //15일일 경우 maxDay=15여야함. 12 + (15-12) = 15임

        if (maxDay > lastDay)
            maxDay = lastDay; //월의 마지막을 지정 29일~31일 경우 때문에

        Debug.Log("maxDay: " + maxDay);

        for (int i = 0; i < maxDay; i++)
        {
            toDayText[i].gameObject.SetActive(true);

            ////오늘 날짜 이후 그래프에 나타내는 이미지 비활성화
            //if (i > today_i - 1)
            //    toDayText[i].transform.GetChild(0).gameObject.SetActive(false);
        }

        for (int j = maxDay; j < 31; j++)
        {
            toDayText[j].gameObject.SetActive(false);
        }
    }

    //숫자 콤마 찍는 함수
    public string CommaText(int _data)
    {
        if (_data != 0)
            return string.Format("{0:#,###}", _data);
        else
            return "0";
    }

    //코인
    public int SetCoinSub(int _coinNum)
    {
        if (_coinNum <= coin_i)
        {
            coin_i -= _coinNum;
            PlayerPrefs.SetInt("Player_Coin", coin_i);
            return coin_i;
        }
        else
        {
            PlayerPrefs.SetInt("Player_Coin", coin_i);
            return coin_i;
        }
    }

    //닉네임 중복 확인
    public void NickName_OverLap_Check(string _nickname, Text _overlapText)
    {
        StartCoroutine(_NickName_OverLap_Check(_nickname, _overlapText));
    }

    IEnumerator _NickName_OverLap_Check(string _nickname, Text _overlapText)
    {
        string nicknameStr = _nickname;
        nicknameStr = nicknameStr.Replace(" ", "");   //공백제거
        specialState = Special_Character_Check(nicknameStr);
        unavailableState = Unalbe_nickname_Check(nicknameStr);
        // 닉네임 체크
        ServerManager.Instance.NickNameDoubleCheck(nicknameStr);
        yield return new WaitUntil(() => ServerManager.Instance.isNickNameSearchCompleted);

        ServerManager.Instance.isNickNameSearchCompleted = false;

        //닉네임이 공백일 때
        if (nicknameStr == "")
        {
            nullState = true;
            _overlapText.text = "닉네임을 입력해주시길 바랍니다.";
            SoundFunction.Instance.Warning_Sound();
        }
        else if (nicknameStr != "")
        {
            nullState = false;
        }

/*        //특수문자 사용
        if (specialState == true)
        {
            _overlapText.text = "특수문자를 사용하지 말아 주세요.";
            SoundFunction.Instance.Warning_Sound();
        }
        //특수문자 사용안함
        else if (specialState == false && nullState == false)
        {
            _overlapText.text = "사용가능";
        }*/

        //서버 등록 시 열어주세요  성엽이 구간
        if (ServerManager.Instance.isExistNickName && specialState == false && nullState == false)
        {
            overlapState = true;    //중복
        }
        else if (!ServerManager.Instance.isExistNickName && specialState == false && nullState == false)
        {
            overlapState = false;   //중복아님
        }

        //조건식 열어주시오~ 성엽이 서버 연결했으면
        //중복체크하고, 특수문자 사용하지 않았고, 닉네임이 중복이지 않고 널값이 아니다
        if (specialState == false && nullState == false && overlapState == false && unavailableState == false)
        {
            _overlapText.text = "사용가능한 닉네임 입니다.";
        }
        //중복체크를 하지 않았다.
        else if (specialState == true)
        {
            //닉네임을 저장할 수 없다.
            _overlapText.text = "특수문자를 사용하지 말아 주세요.";
            SoundFunction.Instance.Warning_Sound();
        }
        else if (nullState == true)
        {
            _overlapText.text = "닉네임을 1~10자 이내로 입력하세요.";
            SoundFunction.Instance.Warning_Sound();
        }
        else if (unavailableState == true)
        {
            _overlapText.text = "사용할 수 없는 닉네임 입니다.";
            SoundFunction.Instance.Warning_Sound();
        }
        //둘중 하나라도 아닐 경우
        else
        {
            //닉네임을 저장할 수 없다.
            _overlapText.text = "닉네임을 다시 설정하여 주세요.";
            SoundFunction.Instance.Warning_Sound();
        }

        // 초기화
        ServerManager.Instance.isExistNickName = false;
        yield return null;
    }

    //닉네임 특수문자 사용 여부 확인 함수
    public bool Special_Character_Check(string _nickname)
    {
        string specialStr = @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]";

        Regex regex = new Regex(specialStr);

        //Debug.Log(regex.IsMatch(_nickname));
        //True가 나오면 특수문자를 사용한다
        specialState = regex.IsMatch(_nickname);
        return regex.IsMatch(_nickname);
    }
    //닉네임 비속
    bool Unalbe_nickname_Check(string _nickname)
    {
        string path = Application.dataPath + @"\fword_list.txt";
        string[] textValue = System.IO.File.ReadAllLines(path);
        unavailableState = false; //초기화
        if (textValue.Length > 0)
        {
            for (int i = 0; i < textValue.Length; i++)
            {
                if (_nickname.Equals(textValue[i])) { unavailableState = true; break; }
            }
        }
        return unavailableState;
    }
    public string nickName_s, loginState_s; //닉네임, 로그인상태
    public int coin_i, accessDay_i, currSection_i;  //코인, 접속일, 현재구간
    public int todayStep_i, monthStep_i, totalStep_i;   //걸음수
    public float todayKcal_f, monthKcal_f, totalKcal_f; //칼로리
    public string todayTime_s, monthTime_s, totalTime_s;    //시간
    public float todayDistance, totalDistance, moonDistance;  // 오늘 걸은거리,총걸은거리, 달까지 거리
    public int itemSmall, itemBig, itemEnergy;  //아이템

    //데이터 초기화
    public void MyDateSave()
    {
        nickName_s = PlayerPrefs.GetString("Player_NickName");
        loginState_s = PlayerPrefs.GetString("Player_LoginState");

        coin_i = PlayerPrefs.GetInt("Player_Coin");   //코인
        accessDay_i = PlayerPrefs.GetInt("Player_ConnectDay"); //접속일
        currSection_i = PlayerPrefs.GetInt("Current_Section");   //현재 구간

        totalDistance = PlayerPrefs.GetFloat("Total_Distance"); //총걸은거리
        totalTime_s = PlayerPrefs.GetString("Total_StepTime");  //총 걸은시간
        totalKcal_f = PlayerPrefs.GetFloat("Total_Kcal");       //총 칼로리

        //PlayerPrefs.SetFloat("BGMVol", 1);  //BGM 소리
        todayDistance = PlayerPrefs.GetFloat("Today_Distance"); // 오늘 걸은 거리
        todayStep_i = PlayerPrefs.GetInt("Today_StepCount");
        moonDistance = PlayerPrefs.GetFloat("Moon_Distance");   //달까지 남은 거리

        itemSmall = PlayerPrefs.GetInt("Item_SmallAirTank");    //작은산소통
        itemBig = PlayerPrefs.GetInt("Item_BigAirTank");  //큰산소통
        itemEnergy = PlayerPrefs.GetInt("Item_EnergyDrink");   //에너지드링크
    }
    //로그인 상태 
    public string LoginState()
    {
        return loginState_s;
    }
}
