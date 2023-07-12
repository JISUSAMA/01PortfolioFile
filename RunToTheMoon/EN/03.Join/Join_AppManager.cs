using System;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using NiceJson;
using UnityEngine.Events;
using System.Collections;
//using static Mopsicus.Plugins.MobileInputField;

public class Join_AppManager : MonoBehaviour
{
    public InputField email_Field;
    public InputField pw_Field;
    public InputField repw_Field;
    public GameObject errorPopup;
    public Text errText;

    string pw_Str;  //패스워드
    string id_Str;  //이미엘
    bool pwSamePw, pwNine, pwSpecial;   //비번 동일, 비법9자리 이상, 비번 특수문자
    bool isEmailFormat; //이메일 포맷
    bool invalidEmailType;

    public Toggle personalToggle;   //개인정보약관 동의 토글
    public GameObject personalPanel;    //개인정보약관 팝업
    public GameObject personalPopupOb;
    public GameObject[] personalPanelText;    //개인정보약관보기
    bool PersonInfo_bool = false;
    bool TermsConditions_bool = false;
    bool toggleIsOn;    //약관동의 여부
    void Start()
    {
        //   PlayerPrefabsReset();
          //PlayerPrefs.SetString("Player_LoginState", "");
        //  PlayerPrefabsReset();
        //패스워드 동일/패스워트9자리 이상/패스워드 특수문자
        if (PlayerPrefs.GetString("Player_LoginState") == "TermsJoinAgree")
        {
            personalPanel.SetActive(false);
        }

        pwSamePw = true; pwNine = true; pwSpecial = true;   //전부 Yes로 초기화
    }
    TouchScreenKeyboard key;
    private void Update()
    {
        //개인정보약관 판넬
        if (personalPanel.activeSelf.Equals(true))
        {
            if (PersonInfo_bool.Equals(true) && TermsConditions_bool.Equals(true))
                personalToggle.interactable = true;
            else { }
                //personalToggle.interactable = false;
        }

       ////다음 필드로 넘어감
       // if (email_Field.isFocused.Equals(true))
       // {
       //     if (ReturnKeyType.Default.Equals(true))
       //     {
       //         pw_Field.ActivateInputField();
       //         pw_Field.Select();
       //     }
       // }
       // else if (pw_Field.isFocused.Equals(true))
       // {
       //     if (ReturnKeyType.Default.Equals(true))
       //     {
       //         repw_Field.ActivateInputField();
       //         repw_Field.Select();
       //     }
       // }
    }
    //////////////////////개인정보 약관 동의서 ////////////////////////////////
    //개인정보약관 동의 이벤트
    public void Personal_InfoToggle()
    {
        if (personalToggle.isOn == true)
        {
            toggleIsOn = true;
            PlayerPrefs.SetString("Player_LoginState", "TermsJoinAgree");
        }
        else
        {
            toggleIsOn = false;
        }
    }

    //개인정보약관 완료 버튼 이벤트
    public void CompletionButtonOn()
    {
        if (toggleIsOn)  //동의했으면
        {
            personalPanel.SetActive(false);
            SoundFunction.Instance.ButtonClick_Sound();
        }
        else
        {
            //동의안했으면
            personalPanel.SetActive(true);
            SoundFunction.Instance.Warning_Sound();
        }
    }
    //  이용약관 0, 개인정보약관 1 
    //이용약관 선택
    public void OnClcik_TermsConditions()
    {
        personalPopupOb.SetActive(true);
        personalPanelText[0].SetActive(true);
    }
    //개인정보약관 선택 
    public void OnClick_PersonInfoBtn()
    {
        personalPopupOb.SetActive(true);
        personalPanelText[1].SetActive(true);
    }
    public void OnClick_PersonInfoBtnOkBtn()
    {
        if (personalPanelText[0].activeSelf.Equals(true))
        {
            personalPanelText[0].SetActive(false);
            PersonInfo_bool = true;
        }
        else if (personalPanelText[1].activeSelf.Equals(true))
        {
            personalPanelText[1].SetActive(false);
            TermsConditions_bool = true;
        }
        personalPopupOb.SetActive(false);
    }
    //////////////////////////////////////////////////////////////////
    public void Make_ID()
    {
        StartCoroutine(_Make_ID());
    }

    IEnumerator _Make_ID()
    {
        ServerManager.Instance.ID_DoubleCheck(email_Field.text);

        yield return new WaitUntil(() => ServerManager.Instance.isIDSearchCompleted);

        ServerManager.Instance.isIDSearchCompleted = false;

        // 아이디가 존재한다.
        if (ServerManager.Instance.isExistID)
        {
            errorPopup.SetActive(true);
            errText.text = "ID exists.";
            SoundFunction.Instance.Warning_Sound();
            Debug.Log("아이디가 존재합니다.");
        }
        else
        {
            // 존재하지 않는다.
            pw_Str = pw_Field.text;
            Check_Password(pw_Str); //비밀번호 체크
            id_Str = email_Field.text;
            Check_Id(id_Str);   //이메일 아이디 체크

            if (isEmailFormat == false)
            {
                errorPopup.SetActive(true);
                errText.text = "Invalid email.";
                SoundFunction.Instance.Warning_Sound();
                Debug.Log("올바르지 않은 이메일입니다.");
            }
            //비밀번호가 동일하지 않으면
            else if (pwSamePw == false)
            {
                errorPopup.SetActive(true);
                errText.text = "Passwords are not the same.";
                SoundFunction.Instance.Warning_Sound();
                Debug.Log("비밀번호가 동일하지 않습니다.");
            }
            //비밀번호가 9자리 이상이 아니면
            else if (pwNine == false)
            {
                errorPopup.SetActive(true);
                errText.text = "Password is less than 9 digits.";
                Debug.Log("비밀번호 9자리가 되지 않습니다.");
            }
            //비밀번호가 특수문자가 섞여있지 않으면
            else if (pwSpecial == false)
            {
                errorPopup.SetActive(true);
                errText.text = "Please use special characters, numbers, and English together.";
                SoundFunction.Instance.Warning_Sound();
                Debug.Log("특수문자와 숫자, 영어를 함께 사용하시길 바랍니다.");
            }
            //이메일 ,비밀번호가 전부 통과 되었을 때 /개인정보 저장
            else if (isEmailFormat == true && pwNine == true && pwSamePw == true && pwSamePw == true)
            {
                InformationSave("GatewaysNickName");
                SceneManager.LoadScene("NickName"); //닉네임 씬 이동
            }

            Debug.Log(pw_Str);
            //email_Field.text = Check_Id_Pw(test).ToString();
            Debug.Log(Check_Password(pw_Str));
        }        
    }

    //올바른 이메일인지 체크
    public bool Check_Id(string _id)
    {
        if (string.IsNullOrEmpty(_id))
            isEmailFormat = false;

        _id = Regex.Replace(_id, @"(@)(.+)$", this.DomainMapper, RegexOptions.None);
        if (invalidEmailType)
            isEmailFormat = false;

        isEmailFormat = Regex.IsMatch(_id,
                  @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                  RegexOptions.IgnoreCase);

        Debug.Log("이메일 체크 : " + isEmailFormat);
        return isEmailFormat;
    }

    //도메인으로 변경 함수
    private string DomainMapper(Match match)
    {
        // IdnMapping class with default property values.
        IdnMapping idn = new IdnMapping();

        string domainName = match.Groups[2].Value;
        try
        {
            domainName = idn.GetAscii(domainName);
        }
        catch (ArgumentException)
        {
            invalidEmailType = true;
        }
        return match.Groups[1].Value + domainName;
    }


    //비밀번호 확인하는 함수
    public bool Check_Password(string _pw)
    {
        //비밀번호가 동일하지 않을 때
        if (pw_Field.text != repw_Field.text)
        {
            //Debug.Log("비밀번호가 동일하지 않습니다.");
            pwSamePw = false;
            return false;
        }
        else if (pw_Field.text == repw_Field.text)
        {
            pwSamePw = true;
        }

        //비밀번호가 9자리가 넘지 않을 경우
        if (_pw.Length < 9)
        {
            //Debug.Log("9자리안됨");
            pwNine = false;
            return false;
        }
        else if (_pw.Length >= 9)
        {
            pwNine = true;
        }

        //특수문자가 섞여잇는지
        Regex rxPassword =
            new Regex(@"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{9,}$",
            RegexOptions.IgnorePatternWhitespace);

        pwSpecial = rxPassword.IsMatch(_pw);
        return rxPassword.IsMatch(_pw);
    }

    //개인정보 저장 함수 / 로그인 방법, 아이디, 비번
    void InformationSave(string _loginName)
    {
        string tempUID = UniqueID();

        PlayerPrefs.SetString("Player_LoginState", _loginName); //로그인방법
        PlayerPrefs.SetString("Player_ID", email_Field.text);  //아이디
        PlayerPrefs.SetString("Player_Password", pw_Field.text);    //비밀번호
        PlayerPrefs.SetString("Player_UID", tempUID);    //고유아이디

        PlayerPrefabsReset();

        ServerManager.Instance.UserInfo_Reg();
    }

    void PlayerPrefabsReset()
    {
        PlayerPrefs.SetInt("Player_Coin", 0);   //코인
        PlayerPrefs.SetInt("Player_ConnectDay", 1); //접속일
        PlayerPrefs.SetInt("Current_Section", 1);   //현재 구간

        PlayerPrefs.SetFloat("Today_Distance", 0);  //일별 걸은거리
        PlayerPrefs.SetInt("Today_StepCount", 0);   //일별 걸음수
        PlayerPrefs.SetString("Today_StepTime", "");    //일별 걸은시간
        PlayerPrefs.SetFloat("Today_Kcal", 0);  //일별 칼로리
        PlayerPrefs.SetFloat("Month_Distance", 0);  //월별 걸은거리
        PlayerPrefs.SetInt("Month_StepCount", 0);   //월별 걸음수
        PlayerPrefs.SetString("Month_StepTime", "");    //월별 걸은시간
        PlayerPrefs.SetFloat("Month_Kcal", 0);  //월별 칼로리
        PlayerPrefs.SetInt("Total_StepCount", 0);   //총 걸음수
        PlayerPrefs.SetString("Total_StepTime", "");    //총 걸은시간
        PlayerPrefs.SetFloat("Total_Kcal", 0);  //총 칼로리s
        PlayerPrefs.SetFloat("Total_Distance", 0);  //총걸은거리

        PlayerPrefs.SetFloat("BGM", 20);  //BGM 소리
        PlayerPrefs.SetFloat("SFX", 20);  //SFX 소리
        PlayerPrefs.SetFloat("Moon_Distance", 0);   //달까지 남은 거리

        PlayerPrefs.SetInt("Item_SmallTank", 0);    //작은산소통
        PlayerPrefs.SetInt("Item_BigAirTank", 0);  //큰산소통
        PlayerPrefs.SetInt("Item_EnergyDrink", 0);   //에너지드링크

        PlayerPrefs.SetInt("MoonPieceCount", 0);   // 달의 조각 갯수

        PlayerPrefs.SetString("NoticeState", "true");
        PlayerPrefs.SetString("PushState", "true");
        PlayerPrefs.SetString("TutorialState", "true");
        //튜토리얼
        if (PlayerPrefs.HasKey("Oxygen_tutorial")) { PlayerPrefs.DeleteKey("Oxygen_tutorial"); }
        if (PlayerPrefs.HasKey("StartDust_tutorial")) { PlayerPrefs.DeleteKey("StartDust_tutorial"); }
        if (PlayerPrefs.HasKey("SpaceRestArea_tutorial")) { PlayerPrefs.DeleteKey("SpaceRestArea_tutorial"); }
 
    }

    //고유아이디 만드는 함수
    string UniqueID()
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        int z1 = UnityEngine.Random.Range(0, 1000000);
        string number = String.Format("%07d", z1);
        string uid = currentEpochTime + "02" + z1;
        return uid;
    }
    public void OnClick_BackBtn()
    {
        SceneManager.LoadScene("Login");
    }
}