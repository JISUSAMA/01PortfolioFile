using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Globalization;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public class Join_AppManager : MonoBehaviour
{
    public GameObject popup;    //알림 팝업
    public Text contentText;    //알림 내용
    public InputField email_Field;
    public InputField pw_Field;
    public InputField repw_Field;
    string pw_Str;  //패스워드
    string id_Str;  //이메일
    bool pwSamePw, pwNine, pwSpecial;   //비번 동일, 비법9자리 이상, 비번 특수문자
    bool isEmailFormat; //이메일 포맷
    bool invalidEmailType;

    [Header("Popup Text Ref")]
    public LocalizedString existID = new LocalizedString();
    public LocalizedString notEmailFormat = new LocalizedString();
    public LocalizedString passwordNotMatch = new LocalizedString();
    public LocalizedString passwordNotMoreThanNineWords = new LocalizedString();
    public LocalizedString passwordNotSpecialSymbols = new LocalizedString();
    private void Awake() {
        LocaleManager.Instance.OnCompleteGetString += Locale_OnCompleteGetString;
    }
    void Start()
    {
        //패스워드 동일/패스워트9자리 이상/패스워드 특수문자
        pwSamePw = true; pwNine = true; pwSpecial = true;   //전부 Yes로 초기화        
    }

    private void OnDestroy() {
        LocaleManager.Instance.OnCompleteGetString -= Locale_OnCompleteGetString;
    }

    private void Locale_OnCompleteGetString(string context) {        
        popup.SetActive(true);
        contentText.text = context;
    }

    public void Make_ID()
    {
        pw_Str = pw_Field.text;
        Check_Password(pw_Str); //비밀번호 체크
        id_Str = email_Field.text;
        Check_Id(id_Str);   //이메일 아이디 체크

        StartCoroutine(_Make_ID(id_Str));
    }

    IEnumerator _Make_ID(string _id_Str)
    {
        ServerManager.Instance.ID_DoubleCheck(_id_Str);

        yield return new WaitUntil(() => ServerManager.Instance.isIDSearchCompleted);

        ServerManager.Instance.isIDSearchCompleted = false;

        if (ServerManager.Instance.isExistID) // 아이디가 존재할 때
        {
            Debug.Log("이메일이 존재합니다.");
            
            LocaleManager.Instance.GetStringLocale(existID);
            //contentText.text = "존재하는 아이디입니다." + "\n" + "다시 입력해주세요.";
        }
        else
        {
            if (isEmailFormat == false)
            {
                //Debug.Log("올바르지 않은 이메일입니다.");
                //popup.SetActive(true);
                LocaleManager.Instance.GetStringLocale(notEmailFormat);
                //contentText.text = "올바르지 않은 이메일입니다." + "\n" + "다시 입력해주세요.";
            }
            //비밀번호가 동일하지 않으면
            else if (pwSamePw == false)
            {
                //Debug.Log("비밀번호가 동일하지 않습니다.");
                LocaleManager.Instance.GetStringLocale(passwordNotMatch);
                //contentText.text = "비밀번호가 동일하지 않습니다." + "\n" + "다시 입력해주세요.";
            }
            //비밀번호가 9자리 이상이 아니면
            else if (pwNine == false)
            {
                Debug.Log("비밀번호 9자리가 되지 않습니다.");
                LocaleManager.Instance.GetStringLocale(passwordNotMoreThanNineWords);
                //contentText.text = "비밀번호가 9자리 이상이 되지 않습니다." + "\n" + "다시 입력해주세요.";
            }
            //비밀번호가 특수문자가 섞여있지 않으면
            else if (pwSpecial == false)
            {
                Debug.Log("특수문자와 숫자, 영어를 함께 사용하시길 바랍니다.");
                LocaleManager.Instance.GetStringLocale(passwordNotSpecialSymbols);
                //contentText.text = "특수문자와 숫자, 영어를 함께 사용하시길 바랍니다.";
            }
            //이메일 ,비밀번호가 전부 통과 되었을 때 /개인정보 저장
            else if (isEmailFormat == true && pwNine == true && pwSamePw == true && pwSpecial == true)
            {
                InformationSave("GatewaysNickName");

                Invoke("Delay", 0.5f);

                SceneManager.LoadScene("NickName"); //닉네임 씬 이동
            }

            Debug.Log(pw_Str);
            //email_Field.text = Check_Id_Pw(test).ToString();
            Debug.Log(Check_Password(pw_Str));
        }
    }

    IEnumerator Delay(float _delay)
    {
        WaitForSeconds ws = new WaitForSeconds(_delay);

        yield return ws;
    }

    string uniqueID()
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        int z1 = UnityEngine.Random.Range(0, 1000000);
        string number = String.Format("%07d", z1);
        string uid = currentEpochTime + "01" + z1;
        return uid;
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

        //Debug.Log("이메일 체크 : " + isEmailFormat);
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
        //Debug.Log("????????" + _pw.Length);
        //비밀번호가 동일하지 않을 때
        if (pw_Field.text != repw_Field.text)
        {
            //Debug.Log("비밀번호가 동일하지 않습니다.");
            pwSamePw = false;
            return false;
        }
        else if(pw_Field.text == repw_Field.text)
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
        else if(_pw.Length >= 9)
        {
            //Debug.Log("여기 들어오는데");
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
        string player_uid = uniqueID();    //고유 아이디 생성
        PlayerPrefs.SetString("AT_Player_LoginState", _loginName); //로그인방법
        PlayerPrefs.SetString("AT_Player_ID", email_Field.text);  //아이디
        PlayerPrefs.SetString("AT_Player_UID", player_uid);    //고유 아이디 저장
        PlayerPrefs.SetString("AT_Player_PassWord", pw_Field.text);    //비밀번호

        PlayerPrefabsReset();

        ServerManager.Instance.UserInfo_Reg();
    }

    void PlayerPrefabsReset()
    {
        //캐릭터 정보
        PlayerPrefs.SetString("AT_Player_Sex", "Woman");
        PlayerPrefs.SetString("AT_Player_Face", "Asian");
        PlayerPrefs.SetString("AT_Player_SkinColor", "FaceApricot");
        PlayerPrefs.SetString("AT_Player_Hair", "Hair1");
        PlayerPrefs.SetString("AT_Player_HairColor", "HairBrown");
        PlayerPrefs.SetInt("AT_Player_Profile", 1);

        //UI정보
        PlayerPrefs.SetInt("AT_Player_Gold", 1000);
        PlayerPrefs.SetInt("AT_Player_Level", 1);
        PlayerPrefs.SetInt("AT_Player_CurrExp", 0);
        PlayerPrefs.SetInt("AT_Player_TakeExp", 0);

        //기록정보
        PlayerPrefs.SetFloat("AT_TodayMaxSpeed", 0f);
        PlayerPrefs.SetFloat("AT_Record_MaxSpeed", 0f);
        PlayerPrefs.SetFloat("AT_Record_TodayKcal", 0f);
        PlayerPrefs.SetFloat("AT_Record_TodayKm", 0f);
        PlayerPrefs.SetFloat("AT_Record_TotalKm", 0f);

        //장착 아이템 스타일
        PlayerPrefs.SetString("AT_Wear_HairStyleName", "Hair1");
        PlayerPrefs.SetString("AT_Wear_JacketStyleName", "BasicNasi");
        PlayerPrefs.SetString("AT_Wear_PantsStyleName", "BasicShort");
        PlayerPrefs.SetString("AT_Wear_ShoesStyleName", "BasicSandal");
        PlayerPrefs.SetString("AT_Wear_BicycleStyleName", "BasicBicycle");
        PlayerPrefs.SetString("AT_Wear_HelmetStyleName", "No");
        PlayerPrefs.SetString("AT_Wear_GlovesStyleName", "No");

        //아이템 장착 종류
        PlayerPrefs.SetString("AT_Wear_HairKind", "ItemID_Hair");
        PlayerPrefs.SetString("AT_Wear_GlovesKind", "No");
        PlayerPrefs.SetString("AT_Wear_HelmetKind", "No");
        PlayerPrefs.SetString("AT_Wear_JacketKind", "ItemID_Nasi");
        PlayerPrefs.SetString("AT_Wear_PantsKind", "ItemID_Short");
        PlayerPrefs.SetString("AT_Wear_ShoesKind", "ItemID_Sandal");
        PlayerPrefs.SetString("AT_Wear_BicycleKind", "ItemID_Bicycle");

        //각 커스텀 숫자 저장
        PlayerPrefs.SetInt("AT_JacketNumber", 0);
        PlayerPrefs.SetInt("AT_PantsNumber", 0);
        PlayerPrefs.SetInt("AT_ShoesNumber", 0);
        PlayerPrefs.SetInt("AT_HelmetNumber", 100);
        PlayerPrefs.SetInt("AT_GlovesNumber", 100);
        PlayerPrefs.SetInt("AT_BicycleNumber", 0);
    }


    public void LoginSceneGo()
    {
        SceneManager.LoadScene("Login");
    }
}
