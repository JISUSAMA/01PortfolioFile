using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GooglePlayGames;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class LoginManager : MonoBehaviour
{
    public GameObject loginPanel;   //로그인 판넬
    public GameObject startPanel;   //게임시작 판넬
    public GameObject[] button; //씬이동 버튼(로비, 닉네임, 캐릭터생성 씬)
    public GameObject informationPopup; //게임정보 동의 팝업

    public InputField id_Field; //아아디
    public InputField pw_Field; //비밀번호

    public GameObject pwFindPopup;  //비번찾기판넬
    public InputField pwFind_Field; //비번찾기
    public GameObject successPopup; //메일보내기 성공 팝업

    public Text logText;



    void Start()
    {
        //각 커스텀 숫자 저장
        //PlayerPrefs.SetInt("JacketNumber", 0); PlayerPrefs.SetString("Wear_JacketKind", "ItemID_Nais");
        //PlayerPrefs.SetInt("PantsNumber", 0); PlayerPrefs.SetString("Wear_PantsKind", "ItemID_Short");
        //PlayerPrefs.SetInt("ShoesNumber", 0); PlayerPrefs.SetString("Wear_ShoesKind", "ItemID_Sandal");
        //PlayerPrefs.SetInt("HelmetNumber", 100); PlayerPrefs.SetString("Wear_HelmetKind", "No");
        //PlayerPrefs.SetInt("GlovesNumber", 100); PlayerPrefs.SetString("Wear_GlovesKind", "No");


        //PlayerPrefs.SetString("Player_LoginState", "");


        logText.text = PlayerPrefs.GetString("Player_LoginState");
        //구글 플레이 로그를 확인하려면 활성화
        PlayGamesPlatform.DebugLogEnabled = true;

        //구글 플레이 활성화
        PlayGamesPlatform.Activate();


        //로그인을 한번도 하지 않았을 경우(처음)
        if (PlayerPrefs.GetString("Player_LoginState") == "")
        {
            Peraonel_Information_Popup();   //개인정보 동의 팝업
            StartCoroutine(LoginPanelShow(true, 0));  //로그인 판넬 활성화
            StartCoroutine(GameStartShow(false, 0, false, false, false));    //로그인을 한번도 하지 않아서 게임시작화면 비활성화
        }
        //다른 로그인을 할 때
        else if (PlayerPrefs.GetString("Player_LoginState") == "다시")
        {
            StartCoroutine(LoginPanelShow(true, 0));  //로그인 판넬 활성화
            StartCoroutine(GameStartShow(false, 0, false, false, false));    //로그인을 한번도 하지 않아서 게임시작화면 비활성화
        }
        //과거 구글 로그인을 했을 경우
        else if(PlayerPrefs.GetString("Player_LoginState") == "Google")
        {
            StartCoroutine(LoginPanelShow(false, 0));   //로그인 판넬 비활성화

            AutoGoogleLogin();  //과거 구글 로그인했을 경우 자동으로 구글로그인

            StartCoroutine(GameStartShow(true, 3, true, false, false)); //게임시작파넬 활성화 3초후에(과거 아이디 흔적이 있어서)
        }
        //과거 게이트웨이즈 로그인을 햇을 경우
        else if(PlayerPrefs.GetString("Player_LoginState")  == "Gateways")
        {
            StartCoroutine(LoginPanelShow(false, 0));   //로그인 판넬 비활성화
            InformationSave("Gateways");    //개인정보 저장
            StartCoroutine(GameStartShow(true, 3, true, false, false)); //게임시작파넬 활성화 3초 후에(과거 아이디 흔적이 있어서)
        }
        //아이디만 만들고 닉네임을 만들지 않았을 경우
        else if(PlayerPrefs.GetString("Player_LoginState") == "GoogleNickName")
        {
            StartCoroutine(LoginPanelShow(false, 0));   //로그인 판넬 비활성화

            InformationSave("GoogleNickName");    //개인정보 저장

            StartCoroutine(GameStartShow(true, 3, false, true, false)); //게임시작파넬 활성화 3초 후에(과거 아이디 흔적이 있어서)
        }
        else if (PlayerPrefs.GetString("Player_LoginState") == "GatewaysNickName")
        {
            StartCoroutine(LoginPanelShow(false, 0));   //로그인 판넬 비활성화
            InformationSave("GatewaysNickName");    //개인정보 저장
            StartCoroutine(GameStartShow(true, 3, false, true, false)); //게임시작파넬 활성화 3초 후에(과거 아이디 흔적이 있어서)
        }
        //닉네임까지만 만들고 캐릭터 생성을 하지 않았을 겨우
        else if(PlayerPrefs.GetString("Player_LoginState") == "GoogleCharacter")
        {
            StartCoroutine(LoginPanelShow(false, 0));   //로그인 판넬 비활성화
            InformationSave("GoogleCharacter");    //개인정보 저장
            StartCoroutine(GameStartShow(true, 3, false, false, true)); //게임시작파넬 활성화 3초 후에(과거 아이디 흔적이 있어서)
        }
        else if (PlayerPrefs.GetString("Player_LoginState") == "GatewaysCharacter")
        {
            StartCoroutine(LoginPanelShow(false, 0));   //로그인 판넬 비활성화
            InformationSave("GatewaysCharacter");    //개인정보 저장
            StartCoroutine(GameStartShow(true, 3, false, false, true)); //게임시작파넬 활성화 3초 후에(과거 아이디 흔적이 있어서)
        }

    }

    //이용약관 및 개인정보방침 동의 팝업 띄우는 함수
    void Peraonel_Information_Popup()
    {
        informationPopup.SetActive(true);
    }

    //개인정보 저장 함수 / 로그인 방법, 아이디, 비번
    void InformationSave(string _loginName)
    {
        PlayerPrefs.SetString("Player_LoginState", _loginName); //로그인방법

        if (_loginName == "Gateways")
        {
            PlayerPrefs.SetString("Player_ID", PlayerPrefs.GetString("Player_ID"));  //아이디
            PlayerPrefs.SetString("Player_PassWord", PlayerPrefs.GetString("Player_PassWord"));    //비밀번호
        }
        else if (_loginName == "Google")
            return;
    }

    //게임 시작 버튼 화면 활성화 함수
    IEnumerator GameStartShow(bool _active, float _time, bool _start, bool _nickname, bool _character)
    {
        yield return new WaitForSeconds(_time);

        startPanel.SetActive(_active);

        button[0].SetActive(_start);
        button[1].SetActive(_nickname);
        button[2].SetActive(_character);
    }

    //로그인 화면 활성화 함수
    IEnumerator LoginPanelShow(bool _active, float _time)
    {
        yield return new WaitForSeconds(_time);

        loginPanel.SetActive(_active);
    }


    //과거 구글로그인 했을 경우 자동로그인되는 함수
    public void AutoGoogleLogin()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                logText.text = Social.localUser.id + " \n" + Social.localUser.userName;
                InformationSave("Google");
            }
            else logText.text = "구글 로그인 실패";
        });
    }

    //구글로그인 버튼을 누르고 첫 구글로그인할때 사용하는 함수
    public void GoogleLogin()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                StartCoroutine(LoginPanelShow(false, 0));   //로그인 판넬 비활성화
                logText.text = Social.localUser.id + " \n" + Social.localUser.userName;
                PlayerPrefs.SetString("Player_Character", "No");    //캐릭터 선택 확인 여부
                InformationSave("Google");
                Invoke("NickNameScene", 2); //닉네임 씬으로 이동
            }
            else logText.text = "구글 로그인 실패";
        });
    }


    //로그인 화면에 있는 로그인 버튼을 눌렀을 때 이벤트
    public void GatewaysLogin()
    {
        PlayerPrefs.SetString("Player_LoginState", "Gateways"); //로그인방법
        PlayerPrefs.SetString("Player_ID", id_Field.text);  //아이디
        PlayerPrefs.SetString("Player_PassWord", pw_Field.text);    //비밀번호

        StartCoroutine(LoginPanelShow(false, 0));  //로그인 판넬 비활성화
        //아이디가 있는지 매칭
        StartCoroutine(GameStartShow(true, 3, true, false, false));    //게임 아이디가 있기 때문에 게임 시작 버튼 나타내기
    }

    //계정만들기 버튼
    public void Join()
    {
        SceneManager.LoadScene("Join");
    }

    //비밀번호찾기 버튼 클릭 이벤트
    public void PassWordFind()
    {
        pwFindPopup.SetActive(true);
    }

    //비밀번호 찾기 버튼 이벤트
    public void EmailServerDataCompartison()
    {
        if (pwFind_Field.text == "서버에 아이디가 있는지 확인")
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("gateways202001@gmail.com"); // 보내는사람
            mail.To.Add("lllehzflll@gmail.com"); // 받는 사람
            mail.Subject = "[gateways]비밀번호 재설정";
            mail.Body = "This is for testing SMTP mail from GMAIL"; //임의의 비밀번호 넣기


            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("gateways202001@gmail.com", "gw2020!!!!") as ICredentialsByHost; // 보내는사람 주소 및 비밀번호 확인
            smtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)

            { return true; };

            smtpServer.Send(mail);

            Debug.Log("success");
            successPopup.SetActive(true);
        }
    }


    public void NickNameScene()
    {
        //구글로그인 후 닉네임씬으로
        SceneManager.LoadScene("NickName");
    }

    public void GameStartScene()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void CharacterChoiceScene()
    {
        SceneManager.LoadScene("CharacterChoice");
    }



    //스타일-텍스쳐
    string id_JacketStr, id_PantsStr, id_ShoesStr, id_hairStr, id_glovesStr, id_helmetStr, id_bicycleStr;
    //장착 아이디
    string wear_HairKind, wear_GlovesKind, wear_HelmetKind, wear_Jacketkind, wear_PantsKind, wear_ShoesKind, wear_BicycleKind;

    //각 아이템 배열에 저장된 번호
    int id_jacketNum, id_pantsNum, id_shoesNum, id_hairNum, id_bodyNum, id_helmetNum, id_glovesNum, id_bicycleNum;

    public void MyData_InfoSave()
    {
        //장착 아이템 번호 저장
        PlayerPrefs.SetInt("HairNumber", id_hairNum);
        PlayerPrefs.SetInt("BodyNumber", id_bodyNum);
        PlayerPrefs.SetInt("JacketNumber", id_jacketNum);
        PlayerPrefs.SetInt("PantsNumber", id_pantsNum);
        PlayerPrefs.SetInt("ShoesNumber", id_shoesNum);
        PlayerPrefs.SetInt("HelmetNumber", id_helmetNum);
        PlayerPrefs.SetInt("GlovesNumber", id_glovesNum);
        //PlayerPrefs.SetInt("BicycleNumber", id_bicycleNum);

        //장착 아이템 텍스쳐 저장
        PlayerPrefs.SetString("Wear_JacketStyleName", id_JacketStr);
        PlayerPrefs.SetString("Wear_PantsStyleName", id_PantsStr);
        PlayerPrefs.SetString("Wear_ShoesStyleName", id_ShoesStr);
        PlayerPrefs.SetString("Wear_HelmetStyleName", id_helmetStr);
        PlayerPrefs.SetString("Wear_GlovesStyleName", id_glovesStr);
        PlayerPrefs.SetString("Wear_HairStyleName", id_hairStr);
        //PlayerPrefs.SetString("Wear_BicycleStyleName", id_bicycleStr);

        //장착 아이템 아이디 저장
        PlayerPrefs.SetString("Wear_GlovesKind", wear_GlovesKind);
        PlayerPrefs.SetString("Wear_HelmetKind", wear_HelmetKind);
        PlayerPrefs.SetString("Wear_JacketKind", wear_Jacketkind);
        PlayerPrefs.SetString("Wear_PantsKind", wear_PantsKind);
        PlayerPrefs.SetString("Wear_ShoesKind", wear_ShoesKind);
        //PlayerPrefs.SetString("Wear_BicycleKind", wear_bicycleStr);

    }
}
