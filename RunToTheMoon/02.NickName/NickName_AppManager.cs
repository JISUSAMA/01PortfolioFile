using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NickName_AppManager : MonoBehaviour
{
    public InputField nickname_field;
    public Text overlapText;


    string specialStr;  //특수문자
    string nicknameStr;    //닉네임복사

    bool specialState;  //특수문자 사용 여부
    bool overlapState;  //중복상태 여부
    bool overlapOnBtn;  //중복체크 버튼 클릭 여부
    bool nullState; //닉네임 넬 체크 여부
    bool unavailableState; //사용 불가 닉네임

    
    //로그인 여부
    string loginState;

    void Start()
    {
        specialState = true;    //초기값 true가 특수문자 사용했다는것
        loginState = PlayerPrefs.GetString("Player_LoginState");    //로그인 상태 여부
       // ReadUnable_nickName();
    }


    //닉네임 중복확인 버튼 눌렀을 때
    public void NickName_OverLap_Check()
    {
        StartCoroutine(_NickName_OverLap_Check());
    }

    IEnumerator _NickName_OverLap_Check()
    {
        overlapOnBtn = true;    //중복체크 여부(체크함)
        nicknameStr = nickname_field.text;
        nicknameStr = nicknameStr.Replace(" ", "");   //공백제거
        specialState = Special_Character_Check(nicknameStr);
        unavailableState = Unalbe_nickname_Check(nicknameStr); //비속어 사용 체크

        //닉네임이 공백일 때
        if (nicknameStr == "")
        {
            nullState = true;
            //overlapText.text = "닉네임을 입력해주시길 바랍니다.";
        }
        //닉네임이 공백이 아닐 때
        else if (nicknameStr != "")
        {
            // 닉네임 체크
            //특수문자 사용
            if (specialState == true)
            {
                Debug.Log("특수문자를 사용하지 말아주십시오");
                SoundFunction.Instance.Warning_Sound();
                //overlapText.text = "특수문자를 사용하지 말아주십시오";
            }
            //특수문자 사용안함
            else if (specialState == false && nullState == false )
            {
                ServerManager.Instance.NickNameDoubleCheck(nicknameStr);

                yield return new WaitUntil(() => ServerManager.Instance.isNickNameSearchCompleted);

                ServerManager.Instance.isNickNameSearchCompleted = false;

                //서버 등록 시 열어주세요  성엽이 구간
                if (ServerManager.Instance.isExistNickName)
                {
                   // Debug.Log("닉네임 중복입니다.");
                    SoundFunction.Instance.Warning_Sound();
                    //overlapText.text = "닉네임 중복입니다.";
                    overlapState = true;    //중복
                }
                else
                {
                   // Debug.Log("사용가능한 닉네임입니다.");
                    SoundFunction.Instance.ButtonClick_Sound();
                    //overlapText.text = "사용가능한 닉네임입니다.";
                    overlapState = false;   //중복아님
                }
            }

        }
        else
        {
            //닉네임이 공백이 아닌경우 
            nullState = false;
        }

        //조건식 열어주시오~ 성엽이 서버 연결했으면
        //중복체크하고, 특수문자 사용하지 않았고, 닉네임이 중복이지 않고 널값이 아니다
        if (specialState == false && nullState == false && overlapState == false && unavailableState == false)
        {
            SoundFunction.Instance.ButtonClick_Sound();
            overlapText.text = "사용가능한 닉네임 입니다.";
        }
        //중복체크를 하지 않았다.
        else if (specialState == true)
        {
            //닉네임을 저장할 수 없다.
            SoundFunction.Instance.Warning_Sound();
            overlapText.text = "특수문자를 사용하지 말아 주세요.";
        }
        else if (nullState == true)
        {
            SoundFunction.Instance.Warning_Sound();
            overlapText.text = "닉네임을 1~10자 이내로 입력하세요.";
        }
        else if(unavailableState == true)
        {
            SoundFunction.Instance.Warning_Sound();
            overlapText.text = "사용할 수 없는 닉네임 입니다.";

        }
        else if (overlapState == true)
        {
            SoundFunction.Instance.Warning_Sound();
            overlapText.text = "이미 존재하는 닉네임입니다.";
        }
        //둘중 하나라도 아닐 경우
        else
        {
            //닉네임을 저장할 수 없다.
            SoundFunction.Instance.Warning_Sound();
            overlapText.text = "닉네임을 다시 설정하여 주세요.";
        }

        // 초기화
        ServerManager.Instance.isExistNickName = false;
    }

    //닉네임 특수문자 사용 여부 확인 함수
    bool Special_Character_Check(string _nickname)
    {
        specialStr = @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]";

        Regex regex = new Regex(specialStr);

        //Debug.Log(regex.IsMatch(_nickname));
        //True가 나오면 특수문자를 사용한다
        specialState = regex.IsMatch(_nickname);
        return regex.IsMatch(_nickname);
    }

    bool Unalbe_nickname_Check(string _nickname)
    {
        string path = Application.dataPath + @"\fword_list.txt";
        string[] textValue = System.IO.File.ReadAllLines(path);

        if (textValue.Length > 0)
        {
            for (int i = 0; i < textValue.Length; i++)
            {
                if (_nickname.Equals(textValue[i])) { unavailableState = true; break; }
                else { unavailableState = false; }  
            }
        }
        return unavailableState;
    }
    public void NickName_Save()
    {
        //조건식 열어주시오~ 성엽이 서버 연결했으면
        //중복체크하고, 특수문자 사용하지 않았고, 닉네임이 중복이지 않고 널값이 아니다
        if (overlapOnBtn == true && specialState == false && nullState == false && overlapState == false && unavailableState == false)
        {
            if (loginState == "GoogleNickName")
                ServerMyInformation("Google", nicknameStr);
            else if (loginState == "GatewaysNickName")
                ServerMyInformation("Gateways", nicknameStr);

            PlayerPrefs.SetString("FirstLoginTime", "");
            SoundFunction.Instance.ButtonClick_Sound();
            SceneManager.LoadScene("Lobby");
        }
        //중복체크를 하지 않았다.
        else if (overlapOnBtn == false)// && (specialState == true || specialState == false || nullState == false || nullState == true))
        {
            //닉네임을 저장할 수 없다.
            overlapText.text = "닉네임 중복 체크를 해주시길 바랍니다.";
            SoundFunction.Instance.Warning_Sound();
        }
        //둘중 하나라도 아닐 경우
        else
        {
            //닉네임을 저장할 수 없다.
            overlapText.text = "닉네임을 다시 설정하여 주세요.";
            SoundFunction.Instance.Warning_Sound();
        }
    }

    void ServerMyInformation(string _logindState, string _nickname)
    {
        PlayerPrefs.SetString("Player_NickName", _nickname);
        PlayerPrefs.SetString("Player_LoginState", _logindState);

        PlayerPrefs.SetInt("Player_Coin", 0);   //코인
        PlayerPrefs.SetInt("Player_ConnectDay", 1); //접속일
        PlayerPrefs.SetInt("Current_Section", 1);   //현재 구간

        PlayerPrefs.SetFloat("Today_Distance", 0);   //일별 걸음수
        PlayerPrefs.SetInt("Today_StepCount", 0);   //일별 걸음수
        PlayerPrefs.SetString("Today_StepTime", "00:00:00.00");    //일별 걸은시간
        PlayerPrefs.SetFloat("Today_Kcal", 0);  //일별 칼로리

        PlayerPrefs.SetFloat("Month_Distance", 0);   //일별 걸음수
        PlayerPrefs.SetInt("Month_StepCount", 0);   //월별 걸음수
        PlayerPrefs.SetString("Month_StepTime", "00:00:00.00");    //월별 걸은시간
        PlayerPrefs.SetFloat("Month_Kcal", 0);  //월별 칼로리

        PlayerPrefs.SetFloat("Total_Distance", 0);   //일별 걸음수
        PlayerPrefs.SetInt("Total_StepCount", 0);   //총 걸음수
        PlayerPrefs.SetString("Total_StepTime", "00:00:00.00");    //총 걸은시간
        PlayerPrefs.SetFloat("Total_Kcal", 0);  //총 칼로리s

        PlayerPrefs.SetFloat("BGM", 20);  //BGM 소리
        PlayerPrefs.SetFloat("SFX", 20);  //SFX 소리

        PlayerPrefs.SetFloat("Moon_Distance", 1000);   //달까지 남은 거리

        PlayerPrefs.SetInt("Item_SmallAirTank", 0);    //작은산소통
        PlayerPrefs.SetInt("Item_BigAirTank", 0);  //큰산소통
        PlayerPrefs.SetInt("Item_EnergyDrink", 0);   //에너지드링크

        PlayerPrefs.SetInt("MoonPieceCount", 0);   // 달의 조각 갯수
        // 서버에 데이터 값 저장
        ServerManager.Instance.CharacterInfo_Reg();

    }
  
}