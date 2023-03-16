using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JBApp_UIManager : MonoBehaviour
{
    [Header("Base UI")]
    public Text myMoney;
    public ToggleGroup BaseToggleGroup;
    public Toggle[] BaseToggle;
    bool home_B, survey_b, event_b, ad_b, point_b, myPage_B, customer_B;
    public GameObject HomeCan, SurveyCan, EventCan, AdCan, PointCan, MypageCan, CSCan;

    [Header("HOME UI")]
    public GameObject LocalNews_canvas_ob; //�����ҽ� �Խ���
    public GameObject Announcement_canvas_ob;// �������� �Խ���
    public Button[] LocalNews_list_btn, Announc_list_btn; //��ư ���
    public GameObject[] LocalContents, AnnouncementConents; //�����ҽ� , �������� �Խñ�

    [Header("AD UI")]
    public ToggleGroup ADToggleGroup;
    public Toggle[] ADToggle;
    public GameObject LocalCan, CityCan;

    [Header("Point UI")]
    public GameObject Point_UseLocalPoint; //���� ��ǰ�� ����Ʈ
    public GameObject Point_UseDeliveryPoint; //���� ����� ����
    public GameObject Point_UseDonation; //���


    public Text Local_MyPoint_txt;
    public InputField Local_MyPoint_input;

    public GameObject DeliveryPointOB;

    public Text Donation_Mypoint_txt;
    public InputField Donation_MyPoint_input;


    public Text Purchase_Name;
    public Text Purchase_Price;
    int Purchase_Price_i;

    [Header("MyPage UI")]
    public Text MyPoint_txt;
    public ToggleGroup MypageToggleGroup;
    public Toggle[] MyPageToggle;
    bool usePoint_b, purchaseItem_b;
    public GameObject PointListCan, CouponCan;
    public GameObject PointList_Parent, Coupon_Parent;
    public GameObject PointListPrefeb, HaveCouponPrefeb;
    public GameObject[] CouponBtns;
    [Header("Ect UI")]
    public GameObject SignPopup;
    public Text SignPopup_text;

    public GameObject CouponPopup;
    public Text Coupon_title;
    public Text Coupon_code;


    public static JBApp_UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else Instance = this;

        SetBulletinBtn();
        HaveMoney_Initialization();
    }

   public void HaveMoney_Initialization()
    {
        Local_MyPoint_txt.text = AppDataManager.Instance.User_Point.ToString() + "P";//������ǰ��
        Donation_Mypoint_txt.text = AppDataManager.Instance.User_Point.ToString() + "P";//���
        myMoney.text = AppDataManager.Instance.User_Point.ToString() + "P";//Top Bar ����Ʈ
        MyPoint_txt.text = AppDataManager.Instance.User_Point.ToString() + "P"; //����������
        //���� �Ӵ� ����!
        if (AppDataManager.Instance.User_Login.Equals("UID"))
        { PlayerPrefs.SetInt("UID_UserPoint", AppDataManager.Instance.User_Point); }
        else { PlayerPrefs.SetInt("Google_UserPoint", AppDataManager.Instance.User_Point); }
    }
    void SetBulletinBtn()
    {
        for (int i = 0; i < LocalNews_list_btn.Length; i++)
        {
            int index = i;
            LocalNews_list_btn[i].onClick.AddListener(() => Local_OpenBulletin(index));
        }

        for (int i = 0; i < Announc_list_btn.Length; i++)
        {
            int index = i;
            Announc_list_btn[i].onClick.AddListener(() => Announce_OpenBulletin(index));
        }
    }
    public void Local_OpenBulletin(int BulletinNum)
    {
        Debug.Log("num : " + BulletinNum);
        LocalContents[BulletinNum].SetActive(true);

    }
    public void Announce_OpenBulletin(int BulletinNum)
    {
        Debug.Log("num1 : " + BulletinNum);
        AnnouncementConents[BulletinNum].SetActive(true);
    }
    public void ActiceTrue_Object(GameObject ob)
    {
        ob.SetActive(true);
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false); //�θ� ��Ȱ��ȭ
    }
    public Toggle setupToggleCurrentSeletion
    {
        get { return BaseToggleGroup.ActiveToggles().FirstOrDefault(); }
    }
    public void Check_toggle()
    {
        if (BaseToggleGroup.ActiveToggles().Any())
        {
            if (setupToggleCurrentSeletion.name.Equals("HOME")) { OnClick_Toggle(true, false, false, false, false, false, false); }
            else if (setupToggleCurrentSeletion.name.Equals("SURVEY")) { OnClick_Toggle(false, true, false, false, false, false, false); }
            else if (setupToggleCurrentSeletion.name.Equals("EVENT")) { OnClick_Toggle(false, false, true, false, false, false, false); }
            else if (setupToggleCurrentSeletion.name.Equals("AD")) { OnClick_Toggle(false, false, false, true, false, false, false); }
            else if (setupToggleCurrentSeletion.name.Equals("POINT")) { OnClick_Toggle(false, false, false, false, true, false, false); }
            else if (setupToggleCurrentSeletion.name.Equals("MYPAGE")) { OnClick_Toggle(false, false, false, false, false, true, false); }
            else if (setupToggleCurrentSeletion.name.Equals("CS")) { OnClick_Toggle(false, false, false, false, false, false, true); }
        }
    }

    public void OnClick_Toggle(bool h, bool sur, bool evt, bool ad, bool point, bool my, bool cs)
    {
        HomeCan.SetActive(h);
        SurveyCan.SetActive(sur);
        EventCan.SetActive(evt);
        AdCan.SetActive(ad);
        PointCan.SetActive(point);
        MypageCan.SetActive(my);
        CSCan.SetActive(cs);
    }
    // <  Survey  > 

    // <  Event  > 
    public void Call_WEBsite(string link)
    {
        string URL_link = link;
        Application.OpenURL(URL_link);
    }
    // <  AD  > 
    public Toggle ADCurrentSeletion
    {
        get { return ADToggleGroup.ActiveToggles().FirstOrDefault(); }
    }
    public void Check_AD_toggle()
    {
        if (ADCurrentSeletion.name.Equals("LOCAL_AD")) { onClick_AdToggle(true, false); }
        else if (ADCurrentSeletion.name.Equals("CITY_AD")) { onClick_AdToggle(false, true); }
    }
    void onClick_AdToggle(bool local, bool city)
    {
        LocalCan.SetActive(local);
        CityCan.SetActive(city);
        ADToggle[0].isOn = local;
        ADToggle[1].isOn = city;
    }
    // <  Point  >
    //���� ��ǰ��
    public void Active_UseLocalPoint()
    {
        Point_UseLocalPoint.SetActive(true);
        HaveMoney_Initialization(); // UI�ʱ�ȭ
    }

    public void OnClick_UseLocalPoint()
    {
        //�Է� ���� ��
        if (Local_MyPoint_input.text.Length > 0)
        {
            //int changePoint = int.Parse(Local_MyPoint_input.text);
            int changePoint = Convert.ToInt32(Local_MyPoint_input.text);
            if (AppDataManager.Instance.User_Point >= 500)
            {
                if (AppDataManager.Instance.User_Point >= changePoint)
                {
                    AppDataManager.Instance.User_Point -= changePoint;
                    ShowSignText("��ȯ�� �Ϸ�Ǿ����ϴ�.\n����������>���� ��ǰ �����Կ���\nȮ�ΰ����մϴ�.");
                    Point_Save("���� ��ǰ�� ����Ʈ", "-" + changePoint);
                    HaveMoney_Initialization(); // UI�ʱ�ȭ
                }
                else
                {
                    ShowSignText("���� ����Ʈ�� Ȯ�����ּ���.");
                }
            }
            else
            {
                ShowSignText("�ּ� 500P ����\n��ȯ�� �����մϴ�.");
            }
        }
        else
        {
            ShowSignText("��ȯ�� ���ϴ� ����Ʈ�� �Է����ּ���.");
        }

    }


    //���� ����� ����
    public void Active_DeliveryCoupon()
    {
        DeliveryPointOB.SetActive(true);

        //�Է��� ������ UI �ѷ��ֱ�
       // int Coupon_price = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        int Coupon_price = Convert.ToInt32(EventSystem.current.currentSelectedGameObject.name);
        if (Coupon_price.Equals(1000)) { Purchase_Name.text = "���ָ���� 1000P ����"; Purchase_Price.text = "-1000P"; Purchase_Price_i = 1000; }
        else if (Coupon_price.Equals(3000)) { Purchase_Name.text = "���ָ���� 3000P ����"; Purchase_Price.text = "-3000P"; Purchase_Price_i = 3000; }
        else if (Coupon_price.Equals(5000)) { Purchase_Name.text = "���ָ���� 5000P ����"; Purchase_Price.text = "-5000P"; Purchase_Price_i = 5000; }
        else if (Coupon_price.Equals(10000)) { Purchase_Name.text = "���ָ���� 10000P ����"; Purchase_Price.text = "-10000P"; Purchase_Price_i = 10000; }

    }

    public void OnClick_DeliveryCoupon()
    {
        if (AppDataManager.Instance.User_Point > Purchase_Price_i)
        {
            //���� �Ϸ�
            ShowSignText("��ȯ�� �Ϸ�Ǿ����ϴ�.\n����������>���� ��ǰ �����Կ���\nȮ�ΰ����մϴ�.");
            AppDataManager.Instance.User_Point -= Purchase_Price_i;

            Point_Save(Purchase_Name.text, "-" + Purchase_Price_i);
            Point_Coupon("���� ����� ����", Purchase_Price_i);

            HaveMoney_Initialization(); // UI�ʱ�ȭ
                                        //������ ���� ������ ���� / ������ ���� ����, ������ �ݾ�, ������ ��¥

        }
        else
        {
            ShowSignText("����Ʈ�� �����Ͽ� �����Ͻ� �� �����ϴ�.");
        }
    }

    public void Active_Donation()
    {
        Point_UseDonation.SetActive(true);
        HaveMoney_Initialization();
    }
    public void OnClick_UseDonation()
    {
        //�Է� ���� ��
        if (Donation_MyPoint_input.text.Length > 0)
        {
           // int changePoint = int.Parse(Donation_MyPoint_input.text);
            int changePoint = Convert.ToInt32(Donation_MyPoint_input.text);
            if (AppDataManager.Instance.User_Point >= changePoint)
            {
                AppDataManager.Instance.User_Point -= changePoint;
                ShowSignText("��ΰ� �Ϸ�Ǿ����ϴ�.");
                Point_Save("����Ʈ ���", "-" + changePoint);
                HaveMoney_Initialization(); // UI�ʱ�ȭ
            }
            else
            {
                ShowSignText("����Ʈ�� �����մϴ�.");
            }
        }
        else
        {
            ShowSignText("����Ͻ� ����Ʈ�� �Է����ּ���.");
        }
    }

    public Toggle MyPageCurrentSeletion
    {
        get { return MypageToggleGroup.ActiveToggles().FirstOrDefault(); }
    }
    public void Check_MyPage_toggle()
    {
        if (MypageToggleGroup.ActiveToggles().Any())
        {
            if (MyPageCurrentSeletion.name.Equals("POINT_LIST")) { onClick_MypageToggle(true, false); }
            else if (MyPageCurrentSeletion.name.Equals("HAVE_COUPON")) { onClick_MypageToggle(false, true); }
        }
    }

    void onClick_MypageToggle(bool point, bool coupon)
    {
        PointListCan.SetActive(point);
        CouponCan.SetActive(coupon);
    }

    //����Ʈ ����/���
    public void Point_Save(string title, string score)
    {
        //������ ����
        GameObject point = Instantiate(PointListPrefeb) as GameObject;
        point.transform.SetParent(PointList_Parent.transform, false);

        string date = DateTime.Now.ToString("yyyy-MM-dd");
        point.GetComponent<MyPage_Point>().SetPointData(title, date, score);

        AppDataManager.Instance.AppPoint_list_count += 1; //���� Ƚ�� �߰� 
        if (AppDataManager.Instance.User_Login.Equals("UID"))
        { PlayerPrefs.SetInt("UID_AppPointCount", AppDataManager.Instance.AppPoint_list_count); }
        else { PlayerPrefs.SetInt("Google_AppPointCount", AppDataManager.Instance.AppPoint_list_count); }
     
    }
    //���� ����
    public void Point_Coupon(string title, int price)
    {
        //������ ����
        GameObject point = Instantiate(HaveCouponPrefeb) as GameObject;
        point.transform.SetParent(Coupon_Parent.transform, false);

        string date = DateTime.Now.ToString("yyyy-MM-dd");
        point.GetComponent<MyPage_Coupon>().SetCouponData(title, date, price);

        AppDataManager.Instance.AppCoupon_list_count += 1; //���� Ƚ�� �߰� 
        if (AppDataManager.Instance.User_Login.Equals("UID"))
            { PlayerPrefs.SetInt("UID_AppCouponCount", AppDataManager.Instance.AppPoint_list_count); }
        else { PlayerPrefs.SetInt("Google_AppCouponCount", AppDataManager.Instance.AppPoint_list_count); }

        //��ư�� �迭�ȿ� �־���
        CouponBtns = new GameObject[AppDataManager.Instance.AppCoupon_list_count];
        CouponBtns[AppDataManager.Instance.AppCoupon_list_count-1] = point;
        CouponBtns[AppDataManager.Instance.AppCoupon_list_count-1].GetComponent<Button>().onClick.AddListener(() =>Show_CouponCodeSign(point));

    }
    public void Show_CouponCodeSign(GameObject btn)
    {
        CouponPopup.SetActive(true);
        Coupon_title.text = btn.GetComponent<MyPage_Coupon>().CouponContent_Title.text;
        Coupon_code.text = btn.GetComponent<MyPage_Coupon>().CouponConent_Code;

    }
    public void ShowSignText(string sign)
    {
        SignPopup.SetActive(true);
        SignPopup_text.text = sign;
    }

}
