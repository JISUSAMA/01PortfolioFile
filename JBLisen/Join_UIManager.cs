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
    bool terms1, terms2, terms3;//��� ���� 

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
            NOTICE_POPUP("��� �̿� ����� �������ּ���.");
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
    //��� �� �ۼ� �Ϸ�!
    public void OnClick_Join_Complete()
    {
        if (canUse_ID && canUse_number && canUse_Adr)
        {
            //��� 
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
                    PwNotice_text.text = "�н����尡 ��ġ���� �ʽ��ϴ�.";
                }

            }
            else
            {
                notice_text.text = "������ ������ �ֽ��ϴ�. �ٽ� Ȯ�����ּ���.";
            }
        }
        else
        {
            if (!canUse_ID) NOTICE_POPUP("���̵� �ߺ�Ȯ���� ���ּ���.");
            else if (!canUse_number) NOTICE_POPUP("������ȣ�� Ȯ�� ���ּ���.");
            else if (!canUse_Adr) NOTICE_POPUP("�����ȣ�� Ȯ�� ���ּ���.");
        }

    }
    public void Save_UserLoginData()
    {

        if (login_way.Equals("UID"))
        {
            PlayerPrefs.SetString("UID_UserID", ID_input.text); //���̵�
            PlayerPrefs.SetString("UID_UserPW", PW_input.text); //���

            PlayerPrefs.SetString("UID_UserName", Name_input.text); //�̸�
            PlayerPrefs.SetString("UID_UserAge", Age_input.text); //����
            PlayerPrefs.SetString("UID_UserContact", Certified_input.text); //����ó
            PlayerPrefs.SetString("UID_UserAdress", Adress_input.text + " " + AdressDetail_input.text); //�ּ�
            PlayerPrefs.SetString("UID_UserGender", User_Gender); //����
            
            PlayerPrefs.SetInt("UID_UserPoint", 0);
            PlayerPrefs.SetInt("UID_AppPointCount", 300);
            PlayerPrefs.SetInt("UID_AppCouponCount", 0);
        }
        //����
        else if(login_way.Equals("Google"))
        {
            PlayerPrefs.SetString("Google_UserName", Google_Name_input.text); //�̸�
            PlayerPrefs.SetString("Google_UserAge",Google_Age_Inf.text); //����
            PlayerPrefs.SetString("Google_UserContact",Google_Certified_Inf.text); //����ó
            PlayerPrefs.SetString("Google_UserAdress", Google_Adress_input.text + " " + Google_AdressDetail_input.text); //�ּ�
            PlayerPrefs.SetString("Google_UserGender", User_Gender); //����

            PlayerPrefs.SetInt("Google_UserPoint", 0);
            PlayerPrefs.SetInt("Google_AppPointCount", 300);
            PlayerPrefs.SetInt("Google_AppCouponCount", 0);
        }


    }
    public void Check_OverlapNickName()
    {
        Debug.Log("ID : " + PlayerPrefs.GetString("UserID"));
        //����ó�� �ۼ�������
        if (ID_input.text.Length != 0)
        {
            //������ ����� ID�� ���� ���,  
            if (PlayerPrefs.GetString("UserID").Equals(ID_input.text))
            {
                NOTICE_POPUP("�ٸ� ID�� ������ּ���.");
                canUse_ID = false;
            }
            else
            {
                NOTICE_POPUP("��� ������ ID�Դϴ�.");
                canUse_ID = true;
            }
        }
        else
        {
            NOTICE_POPUP("����� ID�� �Է����ּ���.");
        }
    }
    string randomCertified;
    //���� ��ư ������ �ڵ����� ������ȣ �Է� �ǰ�
    public void Check_Certified()
    {

        //����ó�� �ۼ�������
        if (ContactInfo_input.text.Length != 0)
        {
            randomCertified = "";
            //���� 6�ڸ��� �Է� ���� �ڸ��� �־���
            for (int i = 0; i < 6; i++)
            {
                randomCertified += Random.Range(0, 9);
            }
            canUse_number = true;
            Certified_img[0].sprite = Certified_sp[1];
            Certified_input.text = randomCertified; //����Ű �Է�
        }
        else
        {
            NOTICE_POPUP("���� ���� ����ó�� �Է����ּ���.");
        }
    }
    public void Check_Adressed()
    {
        canUse_Adr = true;

        Adress_input.text = "����ϵ� ���ֽ� �ϻ걸 ȿ�ڵ�3�� 1693-5";
        AdressDetail_input.text = "����Ʈ�� 207ȣ";
    }
    //�����ϱ� ��ư ������
    public void OnClick_JoinEnd_Complete()
    {
        SceneManager.LoadScene("03.JBApp");
    }
    public void Google_Certified()
    {
        //����ó�� �ۼ�������
        if (Google_ContactInfo_Inf.text.Length != 0)
        {
            randomCertified = "";
            //���� 6�ڸ��� �Է� ���� �ڸ��� �־���
            for (int i = 0; i < 6; i++)
            {
                randomCertified += Random.Range(0, 9);
            }
            canUse_number = true;
            Certified_img[1].sprite = Certified_sp[1];
            Google_Certified_Inf.text = randomCertified; //����Ű �Է�
        }
        else
        {
            NOTICE_POPUP("���� ���� ����ó�� �Է����ּ���.");
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
                NOTICE_POPUP("������ ������ �ֽ��ϴ�. �ٽ� Ȯ�����ּ���.");
            }
        }
        else
        {

            if (!canUse_number) NOTICE_POPUP("������ȣ�� Ȯ�� ���ּ���.");
            else if (!canUse_Adr) NOTICE_POPUP("�����ȣ�� Ȯ�� ���ּ���.");
        }
    }
    public void Google_Check_Adressed()
    {
        canUse_Adr = true;

        Google_Adress_input.text = "����ϵ� ���ֽ� �ϻ걸 ȿ�ڵ�3�� 1693-5";
        Google_AdressDetail_input.text = "����Ʈ�� 207ȣ";
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
