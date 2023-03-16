using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using GooglePlayGames;
using GooglePlayGames.BasicApi;



//https://xd.adobe.com/view/880f3286-bb96-4396-acf7-219fc7bf780f-dd5c/
public class Login_UIManager : MonoBehaviour
{
    public Image LogoIMG_BG;
    public Image LogoIMG;

    public InputField Loin_ID_InF;
    public InputField Loin_PW_InF;
    public Text Loing_notice_txt;

    public GameObject FindID_PW_ob;
    public ToggleGroup FindIDPW_toggleGroup;
    public Toggle [] FindIDPW_Toggle;
    public GameObject[] FindID_PW_Panel;
    string FindCheckState;

    bool Certified_b = false;
    [Header("아이디 찾기")]
    public InputField FindID_Name_Inf; //이름
    public InputField FindID_ContactInfo_Inf; //연락처
    public InputField FindID_Certified_Inf; //인증 번호

    public GameObject FindID_Complete;
    public Text FindID_Name_txt;

    [Header("비밀번호 찾기")]
    public InputField FindPW_ID_Inf; //아이디
    public InputField FindPW_Name_Inf; //이름
    public InputField FindPW_ContactInfo_Inf; //연락처
    public InputField FindPW_Certified_Inf; //인증 번호
   
    public GameObject ChangePW;
    public InputField ChangePW_Inf;
    public InputField CheckPW_Overlap_Inf;
    public GameObject PW_ResetPopup; 

    public GameObject NoticePopup;
    public Text NoticeText;
    private void Start()
    {
        Debug.Log(PlayerPrefs.GetString("UID_UserID"));
        Debug.Log(PlayerPrefs.GetString("UID_UserPW"));
        Debug.Log(PlayerPrefs.GetString("UID_UserName"));
            
            
        //PlayerPrefs.DeleteAll();
        Logo_Fade(); //로고 이미지 사라지기
    }

    void Logo_Fade()
    {
        StopCoroutine(_Logo_Fade());
        StartCoroutine(_Logo_Fade());
    }
    IEnumerator _Logo_Fade()
    {
        LogoIMG.DOFade(1, 1.2f);
        LogoIMG.gameObject.transform.DOLocalMoveZ(-200, 2);
        Debug.Log(LogoIMG.gameObject.transform.localPosition.z);
        yield return new WaitUntil(() => LogoIMG.gameObject.transform.localPosition.z == -200);
        LogoIMG.DOFade(0, 0.12f);
        LogoIMG_BG.DOFade(0, 2);
        yield return new WaitForSeconds(2);
        LogoIMG_BG.gameObject.SetActive(false);
        yield return null;
    }

    //-------------------------------------------------------------------------------------------------------------
    //UID / 구글 로그인
    public void OnClick_UID_Login()
    {
        string Id = PlayerPrefs.GetString("UID_UserID");
        string Pw = PlayerPrefs.GetString("UID_UserPW");
        if (Loin_ID_InF.text.Length > 0 && Loin_PW_InF.text.Length > 0)
        {
            if (Id.Equals(Loin_ID_InF.text) && Pw.Equals(Loin_PW_InF.text))
            {
                Loing_notice_txt.text = "";
                PlayerPrefs.SetString("Login", "UID");
                SceneManager.LoadScene("03.JBApp");
            }
            else
            {
                Loing_notice_txt.text = "ID 혹은 PW가 다릅니다.";
            }
        }
        else
        {
            Loing_notice_txt.text = "ID 혹은 PW를 입력해주세요.";
        }

    }

  
    public void OnClick_Google_Login()
    {

        //string Name = PlayerPrefs.GetString("Google_UserName");
        //if (PlayerPrefs.HasKey("Google_UserName"))
        //{
        //    Debug.Log("Name2 : " + Name);
        //    SceneManager.LoadScene("03.JBApp");
        //}
        //else
        //{
        //    Debug.Log("Name1 : " + Name);
        //    OnClick_GoogleJoin();
        //}
        

        //첫로그인 - 회원정보가 없다.
        if(PlayerPrefs.GetString("Google_UserID").Equals(""))
        {
            StartCoroutine(_OnClick_Google_Login());
        }
        else
        {
            SceneManager.LoadScene("03.JBApp");
        }
    }

    public Text[] googleText;
    IEnumerator _OnClick_Google_Login()
    {
        //구글 플레이 로그를 확인하려면 활성화
        PlayGamesPlatform.DebugLogEnabled = true;
        //구글 플레이 활성화
        PlayGamesPlatform.Activate();

        //GPG를 Activate()하면 Social.localUser가 GPG의 계정 정보로 설정됨
        //안드로이드 빌더 초기화
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .AddOauthScope("profile")
            .AddOauthScope("email")
            .AddOauthScope("https://www.googleapis.com/auth/games")
            .AddOauthScope("https://www.googleapis.com/auth/plus.login")
            .RequestServerAuthCode(false)
            .RequestIdToken()
            .RequestEmail()
            .Build();
        PlayGamesPlatform.InitializeInstance(config);

        // 구글 플레이 로그를 확인할려면 활성화
        //PlayGamesPlatform.DebugLogEnabled = true;

        // 구글 플레이 활성화
        //PlayGamesPlatform.Activate();
        yield return null;

        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                //googleText[0].text = "성공";
                //googleText[1].text = "이름: " + ((PlayGamesLocalUser)Social.localUser).Email;

                PlayerPrefs.SetString("Google_UserID", ((PlayGamesLocalUser)Social.localUser).Email);
                PlayerPrefs.SetString("Google_UserName", "");
                PlayerPrefs.SetString("Google_UserAge", "");
                PlayerPrefs.SetString("Google_UserContact", "");
                PlayerPrefs.SetString("Google_UserAdress", "");
                PlayerPrefs.SetString("Google_UserGender", "");

                Invoke("OnClick_GoogleJoin", 2);    //2초 후에 씬 이동
            }
            else
            {
                //googleText[0].text = "실패";
                NoticePopup.SetActive(true);
                NoticeText.text = "구글 로그인 실패";
            }
        });
    }


    //-------------------------------------------------------------------------------------------------------------
    //아이디/비밀번호 찾기
    public Toggle FindIDPW_CurrentSeletion
    {
        get { return FindIDPW_toggleGroup.ActiveToggles().FirstOrDefault(); }
    }
    public void Check_IDPW_Toggle()
    {
        if (FindIDPW_toggleGroup.ActiveToggles().Any())
        {
            if (FindIDPW_CurrentSeletion.name.Equals("FindID_Toggle")) { Check_FindToggleState("FIND_ID", true, false); }
            else if (FindIDPW_CurrentSeletion.name.Equals("FindPW_Toggle")) { Check_FindToggleState("FIND_PW", false, true); }
        }
    }
    void Check_FindToggleState(string state, bool id, bool pw)
    {
        FindID_PW_Panel[0].SetActive(id);
        FindID_PW_Panel[1].SetActive(pw);
    }


    public void OnClick_FindID_Btn()
    {
        string name = PlayerPrefs.GetString("UID_UserName");
        string id = PlayerPrefs.GetString("UID_UserID");
        if (Certified_b)
        {
            if (FindID_Name_Inf.text.Length != 0 && FindID_ContactInfo_Inf.text.Length != 0 && FindID_Certified_Inf.text.Length != 0)
            {
                //이름이 같으면
                if (name.Equals(FindID_Name_Inf.text))
                {
                    FindID_Complete.SetActive(true);//아이디 찾음
                    FindID_Name_txt.text = name + "님의 아이디는\n" + "<color=#3455E1>" + id + "</color>" + " 입니다.";
                }
                else
                {
                    NOTICE_POPUP("아이디가 존재하지 않습니다.");
                }
            }
            else
            {
                NOTICE_POPUP("누락된 정보가 있습니다.\n다시 확인해주세요.");
            }
        }
        else
        {
            NOTICE_POPUP("인증이 필요합니다.");
        }
    }

    string randomCertified;
    //인증 버튼 누르면 자동으로 인증번호 입력 되게
    public void Check_Certified()
    {

        //연락처를 작성했으면
        if (FindID_ContactInfo_Inf.text.Length != 0)
        {
            randomCertified = "";
            //랜덤 6자리를 입력 인증 자리에 넣어줌
            for (int i = 0; i < 6; i++)
            {
                randomCertified += Random.Range(0, 9);
            }
            FindID_Certified_Inf.text = randomCertified; //인증키 입력
            Certified_b = true;
        }
        else
        {
            NOTICE_POPUP("인증 받을 연락처를 입력해주세요.");
        }
    }
    public void Check_Certified_PW()
    {

        //연락처를 작성했으면
        if (FindPW_ContactInfo_Inf.text.Length != 0)
        {
            randomCertified = "";
            //랜덤 6자리를 입력 인증 자리에 넣어줌
            for (int i = 0; i < 6; i++)
            {
                randomCertified += Random.Range(0, 9);
            }
            FindPW_Certified_Inf.text = randomCertified; //인증키 입력
            Certified_b = true;
        }
        else
        {
            NOTICE_POPUP("인증 받을 연락처를 입력해주세요.");
        }
    }
    public void GO_FindPW_Btn()
    {
        FindID_PW_Panel[0].SetActive(true);
        FindIDPW_Toggle[1].isOn = true;
        FindID_Complete.SetActive(false);
    }
    public void GO_Loin_Btn()
    {
        FindID_Complete.gameObject.SetActive(false);
        FindIDPW_Toggle[0].isOn = true;
        FindID_PW_ob.SetActive(false);
    }

    //비밀번호 찾기
    public void OnClick_FindPW_Btn()
    {
        string name = PlayerPrefs.GetString("UID_UserName");
        string id = PlayerPrefs.GetString("UID_UserID");
        if (Certified_b)
        {
            if (FindPW_Name_Inf.text.Length != 0 && FindPW_ID_Inf.text.Length != 0 && FindPW_ContactInfo_Inf.text.Length != 0 && FindPW_Certified_Inf.text.Length != 0)
            {
                //이름이 같으면
                if (name.Equals(FindPW_Name_Inf.text) && id.Equals(FindPW_ID_Inf.text))
                {
                    ChangePW.SetActive(true);
                }
                else
                {
                    NOTICE_POPUP("아이디가 존재하지 않습니다.");
                }
            }
            else
            {
                NOTICE_POPUP("누락된 정보가 있습니다.\n다시 확인해주세요.");
            }
        }
        else
        {
            NOTICE_POPUP("인증이 필요합니다.");
        }

    }
    //비밀번호 변경하기 
    public void OnClick_ChangePW()
    {
        if (ChangePW_Inf.text.Length != 0 && CheckPW_Overlap_Inf.text.Length != 0)
        {

                if (ChangePW_Inf.text.Equals(CheckPW_Overlap_Inf.text))
                {
                    PlayerPrefs.SetString("UID_UserPW", ChangePW_Inf.text);
                    PW_ResetPopup.SetActive(true);
                }
            else
            {
                NOTICE_POPUP("비밀번호가 일치하지않습니다.");
            }
    
        }
        else
        {
            NOTICE_POPUP("재설정 비밀번호를 입력해주세요.");
        }
    }
    //비밀번호 변경 완료 
    public void OnClick_ResetComplete()
    {
        ChangePW.SetActive(false);//비밀번호 재설정 팝업 내리기
        PW_ResetPopup.SetActive(false);//비밀번호 재설정 완료 팝업 내리기
        FindID_PW_ob.SetActive(false); //아이디비번찾기 팝업 내리기
        FindIDPW_Toggle[1].isOn = true; //토글 버튼. 초기화 해주기 
    }

    //회원가입 버튼
    public void OnClick_joinBtn()
    {
        PlayerPrefs.SetString("Login", "UID");
        SceneManager.LoadScene("02.Join");
    }
    public void OnClick_GoogleJoin()
    {
        PlayerPrefs.SetString("Login", "Google");
        SceneManager.LoadScene("02.Join");
    }
    
    void NOTICE_POPUP(string notice)
    {
        NoticePopup.SetActive(true);
        NoticeText.text = notice;
    }
    public void Call_WEBsite(string link)
    {
        string URL_link = link;
        Application.OpenURL(URL_link);
    }
}
