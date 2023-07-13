using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using GooglePlayGames;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEngine.Localization;

public class SetupPopupCtrl : MonoBehaviour
{
    public ToggleGroup topTapSetupToggleGroup;

    public GameObject accountPanel; //계정판넬
    public GameObject sensorPanel;  //센서판넬

    public Toggle basicToggle;  //기본 토글 설정

    public GameObject pwChangePopup;    //비번변경팝업
    public GameObject errorPopup;   //알림 팝업

    public Image loginGoogle; //구글로그인
    public Image loginNoGoogle; //노구글로그인

    public Text id_text;
    public Text uid_text;
    public Text errer_Text; //알림말 텍스트
    public Text sensorAddr_Text;    //센서주소 텍슷트

    public InputField pw_field; //비밀번호
    public InputField repw_field;   //비밀번호확인

    bool pwSamePw, pwNine, pwSpecial, pwSuccess;   //찾은지, 9자리, 특수문자, 비번 설정 성공

    [Header("String Ref")]
    public LocalizedString notMatchPw = new LocalizedString();
    public LocalizedString morethanNineDigit = new LocalizedString();
    public LocalizedString notSpecialSymbols = new LocalizedString();
    public LocalizedString changeSuccess = new LocalizedString();


    public Toggle topTapSetupCurrentSelention
    {
        get { return topTapSetupToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    private void Awake() {
        LocaleManager.Instance.OnCompleteGetString += Locale_OnCompleteGetString;
    }

    private void Locale_OnCompleteGetString(string context) {
        errer_Text.text = context;
    }

    void Start()
    {
        Initialization();
    }

    //초기화
    void Initialization()
    {
        //계정판넬 비활성화
        Basic_Account_ActionShow(false, false);
        basicToggle.isOn = true;    //기본판넬 토글 초기화

        id_text.text = PlayerPrefs.GetString("AT_Player_ID");
        if(PlayerPrefs.GetString("AT_Player_LoginState") == "Google")
        {
            LoginStateImageShow(true, false);
        }
        else
        {
            LoginStateImageShow(false, true);
        }
        
        uid_text.text = PlayerPrefs.GetString("AT_Player_UID");
    }

    void LoginStateImageShow(bool _google, bool _noGoogle)
    {
        loginGoogle.gameObject.SetActive(_google);
        loginNoGoogle.gameObject.SetActive(_noGoogle);
    }

    //상위탭 - 기본/계정 토글 선택 함수
    public void TopTapSetupToggleChoice()
    {
        if(topTapSetupToggleGroup.ActiveToggles().Any())
        {
            if(topTapSetupCurrentSelention.name == "BasicSetupToggle")
            {
                //계정판넬 비활성화
                Basic_Account_ActionShow(false, false);
            }
            else if (topTapSetupCurrentSelention.name == "AccountSetupToggle")
            {
                //기본 판넬 비활성화
                Basic_Account_ActionShow(true, false);
            }
            else if(topTapSetupCurrentSelention.name.Equals("SensorSetupToggle"))
            {
                //기본판넬비활성화, 센서팔넬 활성화
                Basic_Account_ActionShow(false, true);
                sensorAddr_Text.text = PlayerPrefs.GetString("AT_SensorAddr");
            }
        }
    }

    //기본-계정 설정 페이지가 활성화 비활성화
    void Basic_Account_ActionShow(bool _account, bool _sensor)
    {
        accountPanel.SetActive(_account);
        sensorPanel.SetActive(_sensor);
    }

    //팝업창 닫기
    public void PopupClose()
    {
        Initialization();
        this.gameObject.SetActive(false);
    }

    //로그아웃
    public void Logout()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
        PlayerPrefs.SetString("AT_Player_LoginState", "Again");

        if(PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            GameObject woman = GameObject.Find("Woman");
            GameObject server = GameObject.Find("ServerManager");
            GameObject sensor = GameObject.Find("SensorManager");
            GameObject data = GameObject.Find("DataManager");
            GameObject bluetooth = GameObject.Find("BluetoothLEReceiver");
            GameObject sound = GameObject.Find("_SoundManager");
            Destroy(woman.gameObject);
            Destroy(server.gameObject);
            Destroy(sensor.gameObject);
            Destroy(data.gameObject);
            Destroy(bluetooth.gameObject);
            Destroy(sound.gameObject);
        }
        else if(PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            GameObject man = GameObject.Find("Man");
            GameObject server = GameObject.Find("ServerManager");
            GameObject sensor = GameObject.Find("SensorManager");
            GameObject data = GameObject.Find("DataManager");
            GameObject bluetooth = GameObject.Find("BluetoothLEReceiver");
            GameObject sound = GameObject.Find("_SoundManager");
            Destroy(man.gameObject);
            Destroy(server.gameObject);
            Destroy(sensor.gameObject);
            Destroy(data.gameObject);
            Destroy(bluetooth.gameObject);
            Destroy(sound.gameObject);
        }

        SceneManager.LoadScene("Login");
    }

    //계정 탈퇴
    public void AccountDropOut()
    {
        ServerManager.Instance.AccountDropOut();

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            GameObject woman = GameObject.Find("Woman");
            GameObject server = GameObject.Find("ServerManager");
            GameObject sensor = GameObject.Find("SensorManager");
            GameObject data = GameObject.Find("DataManager");
            GameObject bluetooth = GameObject.Find("BluetoothLEReceiver");
            GameObject sound = GameObject.Find("_SoundManager");
            Destroy(woman.gameObject);
            Destroy(server.gameObject);
            Destroy(sensor.gameObject);
            Destroy(data.gameObject);
            Destroy(bluetooth.gameObject);
            Destroy(sound.gameObject);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            GameObject man = GameObject.Find("Man");
            GameObject server = GameObject.Find("ServerManager");
            GameObject sensor = GameObject.Find("SensorManager");
            GameObject data = GameObject.Find("DataManager");
            GameObject bluetooth = GameObject.Find("BluetoothLEReceiver");
            GameObject sound = GameObject.Find("_SoundManager");
            Destroy(man.gameObject);
            Destroy(server.gameObject);
            Destroy(sensor.gameObject);
            Destroy(data.gameObject);
            Destroy(bluetooth.gameObject);
            Destroy(sound.gameObject);
        }

        PlayerPrefs.SetString("AT_Player_LoginState", ""); //로그인계정 초기화
        SceneManager.LoadScene("Login");    //로그인 씬으로 이동
    }

    //고객센터
    public void CustomerCenter()
    {
        Application.OpenURL("http://gateways.kr/contact/");
    }

    public void Make_ID()
    {
        string pw_Str = pw_field.text;
        Check_Password(pw_Str); //비밀번호 체크

        //비밀번호가 동일하지 않으면
        if (pwSamePw == false)
        {
            errorPopup.SetActive(true); pwSuccess = false;
            //errer_Text.text = "비밀번호가 동일하지 않습니다.";
            //Debug.Log("비밀번호가 동일하지 않습니다.");
            LocaleManager.Instance.GetStringLocale(notMatchPw);
        }
        //비밀번호가 9자리 이상이 아니면
        else if (pwNine == false)
        {
            errorPopup.SetActive(true); pwSuccess = false;
            //errer_Text.text = "비밀번호 9자리가 되지 않습니다.";
            //Debug.Log("비밀번호 9자리가 되지 않습니다.");
            LocaleManager.Instance.GetStringLocale(morethanNineDigit);
        }
        //비밀번호가 특수문자가 섞여있지 않으면
        else if (pwSpecial == false)
        {
            errorPopup.SetActive(true); pwSuccess = false;
            //errer_Text.text = "특수문자와 숫자, 영어를 함께 사용하시길 바랍니다.";
            //Debug.Log("특수문자와 숫자, 영어를 함께 사용하시길 바랍니다.");
            LocaleManager.Instance.GetStringLocale(morethanNineDigit);
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
            //errer_Text.text = "성공적으로 비밀번호가 변경되었습니다.";
            LocaleManager.Instance.GetStringLocale(changeSuccess);

            pw_field.text = "";
            repw_field.text = "";

            // 비밀번호 바꾸기
            ServerManager.Instance.PasswordChange(pw_Str);
        }

        //Debug.Log(pw_Str);
        //email_Field.text = Check_Id_Pw(test).ToString();
        //Debug.Log(Check_Password(pw_Str));
    }


    //비밀번호 확인하는 함수
    bool Check_Password(string _pw)
    {
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



    //센서값 초기화
    public void SensorAddrInit()
    {
        //센서값 초기값
        PlayerPrefs.SetString("AT_SensorAddr", "");
        sensorAddr_Text.text = PlayerPrefs.GetString("AT_SensorAddr");
    }

    //센서값 등록 - 셋업에서
    public void SensorAddrSave()
    {
        StartCoroutine(_SensorAddrSave());
    }

    IEnumerator _SensorAddrSave()
    {
        //GameObject sensorManger = GameObject.Find("SensorManager");
        //ArduinoHM10Test2 sensorScrip = sensorManger.GetComponent<ArduinoHM10Test2>();

        ArduinoHM10Test2.instance.StartProcess();


        yield return new WaitForSeconds(3f);

        //센서스트립트에 있는 startProcess()로 연결 시도하고
        //몇 초후에 연결되었으면 텍스트로 뿌려준다.
        sensorAddr_Text.text = PlayerPrefs.GetString("AT_SensorAddr");
    }

    //일반 - 센서 연결
    public void SensorConnect()
    {
        ArduinoHM10Test2.instance.StartProcess();
    }
    
}
