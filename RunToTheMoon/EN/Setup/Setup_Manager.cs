using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using GooglePlayGames;
using System.Text.RegularExpressions;

public class Setup_Manager : MonoBehaviour
{
    public ToggleGroup setupToggleGroup;

    public GameObject basicPanel;   //기본설정
    public GameObject accountPanel; //계정설정
    public GameObject sensorPanel; //계정설정

    public GameObject pwChangePopup;    //비번변경팝업
    public GameObject errorPopup;   //알림 팝업

    public Text id_Text;    //아이디
    //public Text account_Text;   //계정
    public string accountWay_Text;
    public Image accountWay_Img;
    public Sprite[] accountWay_Sprite;

    public Text uid_Text;   //UID
    public Text errer_Text; //알림말 텍스트

    public InputField pw_field; //비밀번호
    public InputField repw_field;   //비밀번호확인

    bool pwSamePw, pwNine, pwSpecial, pwSuccess;   //찾은지, 9자리, 특수문자, 비번 설정 성공
    public Toggle Notice;
    public Toggle Push;
    public Toggle Intro;

    public Text sensorAddr_Text;    //센서주소텍스트

    public Toggle setupToggleCurrentSeletion
    {
        get { return setupToggleGroup.ActiveToggles().FirstOrDefault(); }
    }


    void Start()
    {
        Initialization();

    }

    public void Initialization()
    {
        //구글 플레이 로그를 확인하려면 활성화
        PlayGamesPlatform.DebugLogEnabled = true;

        //구글 플레이 활성화
        PlayGamesPlatform.Activate();

        id_Text.text = PlayerPrefs.GetString("Player_ID");
        //account_Text.text = PlayerPrefs.GetString("Player_LoginState");
        accountWay_Text = PlayerPrefs.GetString("Player_LoginState");
        uid_Text.text = PlayerPrefs.GetString("Player_UID");

        AccountWay_Img(); //연동상태에 따라서 이미지를 구글연동/ GW연동으로 이미지 변경 시킴
    }

    //계정연동 방법에 따라서 이미지 변경
    void AccountWay_Img()
    {
        if (accountWay_Text.Equals("Google"))
        {
            accountWay_Img.sprite = accountWay_Sprite[0];
        }
        else if (accountWay_Text.Equals("Gateways"))
        {
            accountWay_Img.sprite = accountWay_Sprite[1];
        }
    }

    //설정 상의 탭 선택 시 이벤트 함수
    public void SetupToggleChoice()
    {
        if (setupToggleGroup.ActiveToggles().Any())
        {
            SoundFunction.Instance.ButtonClick_Sound();
            if (setupToggleCurrentSeletion.name == "BasicToggle")
            {
                PanelSetAction(true, false, false);
            }
            else if (setupToggleCurrentSeletion.name == "AccountToggle")
            {
                PanelSetAction(false, true,false);
            }
            else if (setupToggleCurrentSeletion.name == "SensorToggle")
            {
                PanelSetAction(false, false, true);
                //센서주소를 넣어준다.
                sensorAddr_Text.text = PlayerPrefs.GetString("SensorAddr");
            }
        }
    }
    //계정에 따라 알림, 푸쉬, 인트로 상태 저장 필요
    //해당 판넬 활성화/비활성화
    void PanelSetAction(bool _basic, bool _account, bool _sensor)
    {
        basicPanel.SetActive(_basic);
        accountPanel.SetActive(_account);
        sensorPanel.SetActive(_sensor);

    }
    //기본설정 토글 상태 체크 (알림, 푸쉬, 인트로)
    void BasicToggle(bool _notice, bool _push, bool _intro)
    {
        //토글 상태에 따라서 체크 상태 변경
        Notice.isOn = _notice;
        Push.isOn = _push;
        Intro.isOn = _intro;
        //변경된 토글 상태 저장 *서버 업데이트/저장 필요 *
        PlayerPrefs.SetString("NoticeState", _notice.ToString());
        PlayerPrefs.SetString("PushState", _push.ToString());
        PlayerPrefs.SetString("IntroState", _intro.ToString());
        Debug.Log(_notice.ToString() + _push.ToString() + _intro.ToString());
    }
    public void OnEnable()
    {
        //알림,푸쉬, 인트로 키가 있는 경우, 존재하는 키값에 따라 상태를 토글 상태를 변경 시켜준다.
        if (PlayerPrefs.HasKey("NoticeState") && PlayerPrefs.HasKey("PushState") && PlayerPrefs.HasKey("IntroState"))
        {
            Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaa");
            string notice = PlayerPrefs.GetString("NoticeState");
            string push = PlayerPrefs.GetString("PushState"); ;
            string intro = PlayerPrefs.GetString("IntroState");
            BasicToggle(System.Convert.ToBoolean(notice), System.Convert.ToBoolean(push), System.Convert.ToBoolean(intro));
        }
        else
        {
            Debug.Log("bbbbbbbbbbbbbbbbbbbbbbbbbbb");
            PlayerPrefs.SetString("NoticeState", "True");
            PlayerPrefs.SetString("PushState", "True");
            PlayerPrefs.SetString("IntroState", "True");
            BasicToggle(true, true, true);
        }
    }
    public void PushStateCheck(Toggle ob)
    {
        //알림 On
        if (ob.isOn.Equals(true))
        {
            if (ob.gameObject.name.Equals("NoticeToggle"))
            {
                PlayerPrefs.SetString("NoticeState", "True");
            }
            else if (ob.gameObject.name.Equals("PushToggle"))
            {
                PlayerPrefs.SetString("PushState", "True");
            }
            else if (ob.gameObject.name.Equals("IntroToggle"))
            {
                PlayerPrefs.SetString("IntroState", "True");
            }
        }
        else
        {
            if (ob.gameObject.name.Equals("NoticeToggle"))
            {
                PlayerPrefs.SetString("NoticeState", "False");
            }
            else if (ob.gameObject.name.Equals("PushToggle"))
            {
                PlayerPrefs.SetString("PushState", "False");
            }
            else if (ob.gameObject.name.Equals("IntroToggle"))
            {
                PlayerPrefs.SetString("IntroState", "False");
            }
        }
        Debug.Log(ob.isOn);
    }
    public void LogOut()
    {
        if (accountWay_Text == "Goolge")
        {
            ((PlayGamesPlatform)Social.Active).SignOut();

        }
        //게이트웨이즈로 로그인 했을 경우 따로 로그아웃할것이 없어서 바로 상태저장 후 로그인해도됨.
        PlayerPrefs.SetString("Player_LoginState", "Again");
        PlayerPrefs.SetString("Player_ID", "");
        PlayerPrefs.SetString("Player_Password", "");
        PlayerPrefs.SetString("Player_UID", "");
        PlayerPrefs.SetString("Player_NickName", "");
        SceneManager.LoadScene("Login");

    }

    //계정 탈퇴
    public void AccountDropOut()
    {
        // 지우고
        ServerManager.Instance.Delete_UserInfo();
        // 로그아웃
        LogOut();
    }

    //고객센터
    public void CustomerCenter()
    {
        Application.OpenURL("http://gateways.kr/contact/");
    }

    //비밀번호 변경 버튼 이벤트
    public void PassWordChangeButtonOn()
    {
        Make_ID();
    }

    public void Make_ID()
    {
        string pw_Str = pw_field.text;
        Check_Password(pw_Str); //비밀번호 체크

        //비밀번호가 9자리 이상이 아니면
        if (pwNine == false)
        {
            errorPopup.SetActive(true); pwSuccess = false;
            errer_Text.text = "The password cannot be 9 digits.";
            Debug.Log("비밀번호 9자리가 되지 않습니다.");
        }
        //비밀번호가 동일하지 않으면
        else if (pwSamePw == false)
        {
            errorPopup.SetActive(true); pwSuccess = false;
            errer_Text.text = "The passwords are not the same.";
            Debug.Log("비밀번호가 동일하지 않습니다.");
        }
        //비밀번호가 특수문자가 섞여있지 않으면
        else if (pwSpecial == false)
        {
            errorPopup.SetActive(true); pwSuccess = false;
            errer_Text.text = "Please use special characters, numbers,\nand English together.";
            Debug.Log("특수문자와 숫자, 영어를 함께 사용하시길 바랍니다.");
        }
        //이메일 ,비밀번호가 전부 통과 되었을 때 /개인정보 저장
        else if (pwNine == true && pwSamePw == true && pwSamePw == true)
        {
            pwSuccess = true;
        }

        if (pwSuccess)
        {
            pwChangePopup.SetActive(false);
            errorPopup.SetActive(true);
            errer_Text.text = "Successfully changed your password.";

            //////////////// 변경된 플레이어 비밀번호 서버에 저장하기 ///////////////////////////
            PlayerPrefs.SetString("Player_Password", pw_Str);   //비번 새로 저장

            ServerManager.Instance.PasswordChange(pw_Str);
        }

        Debug.Log(pw_Str);
        //email_Field.text = Check_Id_Pw(test).ToString();
        Debug.Log(Check_Password(pw_Str));
    }


    //비밀번호 확인하는 함수
    bool Check_Password(string _pw)
    {
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

        //비밀번호가 동일하지 않을 때
        if (pw_field.text != repw_field.text)
        {
            //Debug.Log("비밀번호가 동일하지 않습니다.");
            pwSamePw = false;
            return false;
        }
        else if (pw_field.text == repw_field.text)
        {
            pwSamePw = true;
        }

        //특수문자가 섞여잇는지
        Regex rxPassword =
            new Regex(@"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{9,}$",
            RegexOptions.IgnorePatternWhitespace);

        pwSpecial = rxPassword.IsMatch(_pw);
        return rxPassword.IsMatch(_pw);
    }

    //일반 - 센서 연결
    public void SensorConnect()
    {
        L_ESP32BLEApp.instance.StartProcess();
    }
}
