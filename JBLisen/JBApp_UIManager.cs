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
    public GameObject LocalNews_canvas_ob; //지역소식 게시판
    public GameObject Announcement_canvas_ob;// 공지사항 게시판
    public Button[] LocalNews_list_btn, Announc_list_btn; //버튼 목록
    public GameObject[] LocalContents, AnnouncementConents; //지역소식 , 공지사항 게시글

    [Header("AD UI")]
    public ToggleGroup ADToggleGroup;
    public Toggle[] ADToggle;
    public GameObject LocalCan, CityCan;

    [Header("Point UI")]
    public GameObject Point_UseLocalPoint; //지역 상품권 포인트
    public GameObject Point_UseDeliveryPoint; //전주 맛배달 쿠폰
    public GameObject Point_UseDonation; //기부


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
        Local_MyPoint_txt.text = AppDataManager.Instance.User_Point.ToString() + "P";//지역상품권
        Donation_Mypoint_txt.text = AppDataManager.Instance.User_Point.ToString() + "P";//기부
        myMoney.text = AppDataManager.Instance.User_Point.ToString() + "P";//Top Bar 포인트
        MyPoint_txt.text = AppDataManager.Instance.User_Point.ToString() + "P"; //마이페이지
        //보유 머니 저장!
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
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false); //부모 비활성화
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
    //지역 상품권
    public void Active_UseLocalPoint()
    {
        Point_UseLocalPoint.SetActive(true);
        HaveMoney_Initialization(); // UI초기화
    }

    public void OnClick_UseLocalPoint()
    {
        //입력 했을 때
        if (Local_MyPoint_input.text.Length > 0)
        {
            //int changePoint = int.Parse(Local_MyPoint_input.text);
            int changePoint = Convert.ToInt32(Local_MyPoint_input.text);
            if (AppDataManager.Instance.User_Point >= 500)
            {
                if (AppDataManager.Instance.User_Point >= changePoint)
                {
                    AppDataManager.Instance.User_Point -= changePoint;
                    ShowSignText("교환이 완료되었습니다.\n마이페이지>구매 상품 보관함에서\n확인가능합니다.");
                    Point_Save("지역 상품권 포인트", "-" + changePoint);
                    HaveMoney_Initialization(); // UI초기화
                }
                else
                {
                    ShowSignText("보유 포인트를 확인해주세요.");
                }
            }
            else
            {
                ShowSignText("최소 500P 부터\n교환이 가능합니다.");
            }
        }
        else
        {
            ShowSignText("교환을 원하는 포인트를 입력해주세요.");
        }

    }


    //전주 맛배달 쿠폰
    public void Active_DeliveryCoupon()
    {
        DeliveryPointOB.SetActive(true);

        //입력한 쿠폰의 UI 뿌려주기
       // int Coupon_price = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        int Coupon_price = Convert.ToInt32(EventSystem.current.currentSelectedGameObject.name);
        if (Coupon_price.Equals(1000)) { Purchase_Name.text = "전주맛배달 1000P 구매"; Purchase_Price.text = "-1000P"; Purchase_Price_i = 1000; }
        else if (Coupon_price.Equals(3000)) { Purchase_Name.text = "전주맛배달 3000P 구매"; Purchase_Price.text = "-3000P"; Purchase_Price_i = 3000; }
        else if (Coupon_price.Equals(5000)) { Purchase_Name.text = "전주맛배달 5000P 구매"; Purchase_Price.text = "-5000P"; Purchase_Price_i = 5000; }
        else if (Coupon_price.Equals(10000)) { Purchase_Name.text = "전주맛배달 10000P 구매"; Purchase_Price.text = "-10000P"; Purchase_Price_i = 10000; }

    }

    public void OnClick_DeliveryCoupon()
    {
        if (AppDataManager.Instance.User_Point > Purchase_Price_i)
        {
            //구매 완료
            ShowSignText("교환이 완료되었습니다.\n마이페이지>구매 상품 보관함에서\n확인가능합니다.");
            AppDataManager.Instance.User_Point -= Purchase_Price_i;

            Point_Save(Purchase_Name.text, "-" + Purchase_Price_i);
            Point_Coupon("전주 맛배달 쿠폰", Purchase_Price_i);

            HaveMoney_Initialization(); // UI초기화
                                        //구매한 쿠폰 데이터 저장 / 구매한 쿠폰 종류, 구매한 금액, 구매한 날짜

        }
        else
        {
            ShowSignText("포인트가 부족하여 구매하실 수 없습니다.");
        }
    }

    public void Active_Donation()
    {
        Point_UseDonation.SetActive(true);
        HaveMoney_Initialization();
    }
    public void OnClick_UseDonation()
    {
        //입력 했을 때
        if (Donation_MyPoint_input.text.Length > 0)
        {
           // int changePoint = int.Parse(Donation_MyPoint_input.text);
            int changePoint = Convert.ToInt32(Donation_MyPoint_input.text);
            if (AppDataManager.Instance.User_Point >= changePoint)
            {
                AppDataManager.Instance.User_Point -= changePoint;
                ShowSignText("기부가 완료되었습니다.");
                Point_Save("포인트 기부", "-" + changePoint);
                HaveMoney_Initialization(); // UI초기화
            }
            else
            {
                ShowSignText("포인트가 부족합니다.");
            }
        }
        else
        {
            ShowSignText("기부하실 포인트를 입력해주세요.");
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

    //포인트 적립/사용
    public void Point_Save(string title, string score)
    {
        //프리팹 생성
        GameObject point = Instantiate(PointListPrefeb) as GameObject;
        point.transform.SetParent(PointList_Parent.transform, false);

        string date = DateTime.Now.ToString("yyyy-MM-dd");
        point.GetComponent<MyPage_Point>().SetPointData(title, date, score);

        AppDataManager.Instance.AppPoint_list_count += 1; //적립 횟수 추가 
        if (AppDataManager.Instance.User_Login.Equals("UID"))
        { PlayerPrefs.SetInt("UID_AppPointCount", AppDataManager.Instance.AppPoint_list_count); }
        else { PlayerPrefs.SetInt("Google_AppPointCount", AppDataManager.Instance.AppPoint_list_count); }
     
    }
    //쿠폰 내역
    public void Point_Coupon(string title, int price)
    {
        //프리팹 생성
        GameObject point = Instantiate(HaveCouponPrefeb) as GameObject;
        point.transform.SetParent(Coupon_Parent.transform, false);

        string date = DateTime.Now.ToString("yyyy-MM-dd");
        point.GetComponent<MyPage_Coupon>().SetCouponData(title, date, price);

        AppDataManager.Instance.AppCoupon_list_count += 1; //적립 횟수 추가 
        if (AppDataManager.Instance.User_Login.Equals("UID"))
            { PlayerPrefs.SetInt("UID_AppCouponCount", AppDataManager.Instance.AppPoint_list_count); }
        else { PlayerPrefs.SetInt("Google_AppCouponCount", AppDataManager.Instance.AppPoint_list_count); }

        //버튼을 배열안에 넣어줌
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
