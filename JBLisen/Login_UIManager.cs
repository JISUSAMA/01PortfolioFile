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
    [Header("���̵� ã��")]
    public InputField FindID_Name_Inf; //�̸�
    public InputField FindID_ContactInfo_Inf; //����ó
    public InputField FindID_Certified_Inf; //���� ��ȣ

    public GameObject FindID_Complete;
    public Text FindID_Name_txt;

    [Header("��й�ȣ ã��")]
    public InputField FindPW_ID_Inf; //���̵�
    public InputField FindPW_Name_Inf; //�̸�
    public InputField FindPW_ContactInfo_Inf; //����ó
    public InputField FindPW_Certified_Inf; //���� ��ȣ
   
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
        Logo_Fade(); //�ΰ� �̹��� �������
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
    //UID / ���� �α���
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
                Loing_notice_txt.text = "ID Ȥ�� PW�� �ٸ��ϴ�.";
            }
        }
        else
        {
            Loing_notice_txt.text = "ID Ȥ�� PW�� �Է����ּ���.";
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
        

        //ù�α��� - ȸ�������� ����.
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
        //���� �÷��� �α׸� Ȯ���Ϸ��� Ȱ��ȭ
        PlayGamesPlatform.DebugLogEnabled = true;
        //���� �÷��� Ȱ��ȭ
        PlayGamesPlatform.Activate();

        //GPG�� Activate()�ϸ� Social.localUser�� GPG�� ���� ������ ������
        //�ȵ���̵� ���� �ʱ�ȭ
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

        // ���� �÷��� �α׸� Ȯ���ҷ��� Ȱ��ȭ
        //PlayGamesPlatform.DebugLogEnabled = true;

        // ���� �÷��� Ȱ��ȭ
        //PlayGamesPlatform.Activate();
        yield return null;

        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                //googleText[0].text = "����";
                //googleText[1].text = "�̸�: " + ((PlayGamesLocalUser)Social.localUser).Email;

                PlayerPrefs.SetString("Google_UserID", ((PlayGamesLocalUser)Social.localUser).Email);
                PlayerPrefs.SetString("Google_UserName", "");
                PlayerPrefs.SetString("Google_UserAge", "");
                PlayerPrefs.SetString("Google_UserContact", "");
                PlayerPrefs.SetString("Google_UserAdress", "");
                PlayerPrefs.SetString("Google_UserGender", "");

                Invoke("OnClick_GoogleJoin", 2);    //2�� �Ŀ� �� �̵�
            }
            else
            {
                //googleText[0].text = "����";
                NoticePopup.SetActive(true);
                NoticeText.text = "���� �α��� ����";
            }
        });
    }


    //-------------------------------------------------------------------------------------------------------------
    //���̵�/��й�ȣ ã��
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
                //�̸��� ������
                if (name.Equals(FindID_Name_Inf.text))
                {
                    FindID_Complete.SetActive(true);//���̵� ã��
                    FindID_Name_txt.text = name + "���� ���̵��\n" + "<color=#3455E1>" + id + "</color>" + " �Դϴ�.";
                }
                else
                {
                    NOTICE_POPUP("���̵� �������� �ʽ��ϴ�.");
                }
            }
            else
            {
                NOTICE_POPUP("������ ������ �ֽ��ϴ�.\n�ٽ� Ȯ�����ּ���.");
            }
        }
        else
        {
            NOTICE_POPUP("������ �ʿ��մϴ�.");
        }
    }

    string randomCertified;
    //���� ��ư ������ �ڵ����� ������ȣ �Է� �ǰ�
    public void Check_Certified()
    {

        //����ó�� �ۼ�������
        if (FindID_ContactInfo_Inf.text.Length != 0)
        {
            randomCertified = "";
            //���� 6�ڸ��� �Է� ���� �ڸ��� �־���
            for (int i = 0; i < 6; i++)
            {
                randomCertified += Random.Range(0, 9);
            }
            FindID_Certified_Inf.text = randomCertified; //����Ű �Է�
            Certified_b = true;
        }
        else
        {
            NOTICE_POPUP("���� ���� ����ó�� �Է����ּ���.");
        }
    }
    public void Check_Certified_PW()
    {

        //����ó�� �ۼ�������
        if (FindPW_ContactInfo_Inf.text.Length != 0)
        {
            randomCertified = "";
            //���� 6�ڸ��� �Է� ���� �ڸ��� �־���
            for (int i = 0; i < 6; i++)
            {
                randomCertified += Random.Range(0, 9);
            }
            FindPW_Certified_Inf.text = randomCertified; //����Ű �Է�
            Certified_b = true;
        }
        else
        {
            NOTICE_POPUP("���� ���� ����ó�� �Է����ּ���.");
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

    //��й�ȣ ã��
    public void OnClick_FindPW_Btn()
    {
        string name = PlayerPrefs.GetString("UID_UserName");
        string id = PlayerPrefs.GetString("UID_UserID");
        if (Certified_b)
        {
            if (FindPW_Name_Inf.text.Length != 0 && FindPW_ID_Inf.text.Length != 0 && FindPW_ContactInfo_Inf.text.Length != 0 && FindPW_Certified_Inf.text.Length != 0)
            {
                //�̸��� ������
                if (name.Equals(FindPW_Name_Inf.text) && id.Equals(FindPW_ID_Inf.text))
                {
                    ChangePW.SetActive(true);
                }
                else
                {
                    NOTICE_POPUP("���̵� �������� �ʽ��ϴ�.");
                }
            }
            else
            {
                NOTICE_POPUP("������ ������ �ֽ��ϴ�.\n�ٽ� Ȯ�����ּ���.");
            }
        }
        else
        {
            NOTICE_POPUP("������ �ʿ��մϴ�.");
        }

    }
    //��й�ȣ �����ϱ� 
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
                NOTICE_POPUP("��й�ȣ�� ��ġ�����ʽ��ϴ�.");
            }
    
        }
        else
        {
            NOTICE_POPUP("�缳�� ��й�ȣ�� �Է����ּ���.");
        }
    }
    //��й�ȣ ���� �Ϸ� 
    public void OnClick_ResetComplete()
    {
        ChangePW.SetActive(false);//��й�ȣ �缳�� �˾� ������
        PW_ResetPopup.SetActive(false);//��й�ȣ �缳�� �Ϸ� �˾� ������
        FindID_PW_ob.SetActive(false); //���̵���ã�� �˾� ������
        FindIDPW_Toggle[1].isOn = true; //��� ��ư. �ʱ�ȭ ���ֱ� 
    }

    //ȸ������ ��ư
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
