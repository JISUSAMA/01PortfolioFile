using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Join_UIManager : MonoBehaviour
{
    [Header("TermsPanel")]
    public Toggle[] TermsToggle;
    bool terms1, terms2, terms3;//약관 동의 

    [Header("Join")]
    public GameObject[] JoinPanel;
    [Header("UID")]
    public InputField ID_input;
    public InputField PW_input;
    public InputField CheckPW_input;
    public InputField Name_input;
    public InputField Age_input;
    public InputField ContactInfo_input;
    public InputField Certified_input;

    public InputField Adress_input;
    public InputField AdressDetail_input;

    public ToggleGroup GenderToggleGroup;
    public string User_Gender;
    string login_way;
    public Image[] Certified_img;
    public Sprite[] Certified_sp;

    public Text PwNotice_text, notice_text;
    [Header("Google")]
    public InputField Google_Name_input;
    public InputField Google_Age_Inf;
    public InputField Google_ContactInfo_Inf;
    public InputField Google_Certified_Inf;
  
    public InputField Google_Adress_input;
    public InputField Google_AdressDetail_input;

    public ToggleGroup Google_GenderToggleGroup;

    [Header("Join_Succese")]
    public GameObject JoinEndPanel;

    [Header("Notic")]
    public GameObject NoticePopup;
    public Text NoticeText;
    public Button CheckBtn;

    bool canUse_ID = false, canUse_number = false, canUse_Adr = false;
    public void Check_TermsToggle()
    {
        if (TermsToggle[0].isOn.Equals(true)) { terms1 = true; }
        else { terms1 = false; }

        if (TermsToggle[1].isOn.Equals(true)) { terms2 = true; }
        else { terms2 = false; }

        if (TermsToggle[2].isOn.Equals(true))
        { 
            TermsToggle[1].isOn = true; TermsToggle[0].isOn = true;
            terms1 = true; terms2 = true; terms3 = true; 
        }
        else 
        {
            TermsToggle[1].isOn = false; TermsToggle[0].isOn = false; 
            terms1 = false; terms2 = false; terms3 = false; 
        }

    }
    public void OnClick_JoinBtn()
    {
        if (terms1 && terms2 && terms3)
        {
            login_way = PlayerPrefs.GetString("Login");
            if (login_way.Equals("UID"))
            {
                JoinPanel[0].SetActive(true);
            }
            else
            {
                JoinPanel[1].SetActive(true);
            }
        }
        else
        {
            NOTICE_POPUP("모든 이용 약관을 동의해주세요.");
        }
    }
    public Toggle GenderCurrentSeletion
    {
        get { return GenderToggleGroup.ActiveToggles().FirstOrDefault(); }
    }
    public Toggle Google_GenderCurrentSeletion
    {
        get { return Google_GenderToggleGroup.ActiveToggles().FirstOrDefault(); }
    }
    public void Check_Gender_Toggle()
    {
        if (GenderToggleGroup.ActiveToggles().Any())
        {
            if (GenderCurrentSeletion.name.Equals("MEN")) { User_Gender = "MEN"; }
            else if (GenderCurrentSeletion.name.Equals("WOMEN")) { User_Gender = "WOMEN"; }
        }
        else if (Google_GenderToggleGroup.ActiveToggles().Any())
        {
            if (Google_GenderCurrentSeletion.name.Equals("MEN")) { User_Gender = "MEN"; }
            else if (Google_GenderCurrentSeletion.name.Equals("WOMEN")) { User_Gender = "WOMEN"; }
        }
    }
    //모든 글 작성 완료!
    public void OnClick_Join_Complete()
    {
        if (canUse_ID && canUse_number && canUse_Adr)
        {
            //모든 
            if (ID_input.text.Length != 0 && PW_input.text.Length != 0 && CheckPW_input.text.Length != 0
                && Name_input.text.Length != 0 && Age_input.text.Length != 0 && ContactInfo_input.text.Length != 0
                && Certified_input.text.Length != 0 && Adress_input.text.Length != 0 && AdressDetail_input.text.Length != 0)
            {
                if (PW_input.text == CheckPW_input.text)
                {
                    PwNotice_text.text = "";
                    notice_text.text = "";
                    Save_UserLoginData();
                    JoinEndPanel.SetActive(true);

                }
                else
                {
                    PwNotice_text.text = "패스워드가 일치하지 않습니다.";
                }

            }
            else
            {
                notice_text.text = "누락된 정보가 있습니다. 다시 확인해주세요.";
            }
        }
        else
        {
            if (!canUse_ID) NOTICE_POPUP("아이디 중복확인을 해주세요.");
            else if (!canUse_number) NOTICE_POPUP("인증번호를 확인 해주세요.");
            else if (!canUse_Adr) NOTICE_POPUP("우편번호를 확인 해주세요.");
        }

    }
    public void Save_UserLoginData()
    {

        if (login_way.Equals("UID"))
        {
            PlayerPrefs.SetString("UID_UserID", ID_input.text); //아이디
            PlayerPrefs.SetString("UID_UserPW", PW_input.text); //비번

            PlayerPrefs.SetString("UID_UserName", Name_input.text); //이름
            PlayerPrefs.SetString("UID_UserAge", Age_input.text); //나이
            PlayerPrefs.SetString("UID_UserContact", Certified_input.text); //연락처
            PlayerPrefs.SetString("UID_UserAdress", Adress_input.text + " " + AdressDetail_input.text); //주소
            PlayerPrefs.SetString("UID_UserGender", User_Gender); //성별
            
            PlayerPrefs.SetInt("UID_UserPoint", 0);
            PlayerPrefs.SetInt("UID_AppPointCount", 300);
            PlayerPrefs.SetInt("UID_AppCouponCount", 0);
        }
        //구글
        else if(login_way.Equals("Google"))
        {
            PlayerPrefs.SetString("Google_UserName", Google_Name_input.text); //이름
            PlayerPrefs.SetString("Google_UserAge",Google_Age_Inf.text); //나이
            PlayerPrefs.SetString("Google_UserContact",Google_Certified_Inf.text); //연락처
            PlayerPrefs.SetString("Google_UserAdress", Google_Adress_input.text + " " + Google_AdressDetail_input.text); //주소
            PlayerPrefs.SetString("Google_UserGender", User_Gender); //성별

            PlayerPrefs.SetInt("Google_UserPoint", 0);
            PlayerPrefs.SetInt("Google_AppPointCount", 300);
            PlayerPrefs.SetInt("Google_AppCouponCount", 0);
        }


    }
    public void Check_OverlapNickName()
    {
        Debug.Log("ID : " + PlayerPrefs.GetString("UserID"));
        //연락처를 작성했으면
        if (ID_input.text.Length != 0)
        {
            //기존에 저장된 ID와 같은 경우,  
            if (PlayerPrefs.GetString("UserID").Equals(ID_input.text))
            {
                NOTICE_POPUP("다른 ID를 사용해주세요.");
                canUse_ID = false;
            }
            else
            {
                NOTICE_POPUP("사용 가능한 ID입니다.");
                canUse_ID = true;
            }
        }
        else
        {
            NOTICE_POPUP("사용할 ID를 입력해주세요.");
        }
    }
    string randomCertified;
    //인증 버튼 누르면 자동으로 인증번호 입력 되게
    public void Check_Certified()
    {

        //연락처를 작성했으면
        if (ContactInfo_input.text.Length != 0)
        {
            randomCertified = "";
            //랜덤 6자리를 입력 인증 자리에 넣어줌
            for (int i = 0; i < 6; i++)
            {
                randomCertified += Random.Range(0, 9);
            }
            canUse_number = true;
            Certified_img[0].sprite = Certified_sp[1];
            Certified_input.text = randomCertified; //인증키 입력
        }
        else
        {
            NOTICE_POPUP("인증 받을 연락처를 입력해주세요.");
        }
    }
    public void Check_Adressed()
    {
        canUse_Adr = true;

        Adress_input.text = "전라북도 전주시 완산구 효자동3가 1693-5";
        AdressDetail_input.text = "웨스트빌 207호";
    }
    //시작하기 버튼 누르면
    public void OnClick_JoinEnd_Complete()
    {
        SceneManager.LoadScene("03.JBApp");
    }
    public void Google_Certified()
    {
        //연락처를 작성했으면
        if (Google_ContactInfo_Inf.text.Length != 0)
        {
            randomCertified = "";
            //랜덤 6자리를 입력 인증 자리에 넣어줌
            for (int i = 0; i < 6; i++)
            {
                randomCertified += Random.Range(0, 9);
            }
            canUse_number = true;
            Certified_img[1].sprite = Certified_sp[1];
            Google_Certified_Inf.text = randomCertified; //인증키 입력
        }
        else
        {
            NOTICE_POPUP("인증 받을 연락처를 입력해주세요.");
        }
    }
    public void Google_Join_btn()
    {
        if (canUse_number && canUse_Adr)
        {
            if (Google_Certified_Inf.text.Length != 0 && Google_ContactInfo_Inf.text.Length != 0 && Google_Age_Inf.text.Length != 0)
            {
                PwNotice_text.text = "";
                notice_text.text = "";
                Save_UserLoginData();
                JoinEndPanel.SetActive(true);
            }
            else
            {
                NOTICE_POPUP("누락된 정보가 있습니다. 다시 확인해주세요.");
            }
        }
        else
        {

            if (!canUse_number) NOTICE_POPUP("인증번호를 확인 해주세요.");
            else if (!canUse_Adr) NOTICE_POPUP("우편번호를 확인 해주세요.");
        }
    }
    public void Google_Check_Adressed()
    {
        canUse_Adr = true;

        Google_Adress_input.text = "전라북도 전주시 완산구 효자동3가 1693-5";
        Google_AdressDetail_input.text = "웨스트빌 207호";
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
