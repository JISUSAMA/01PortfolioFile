using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppDataManager : MonoBehaviour
{
    [Header("MY Info")]
    public string User_Login; //�α��� ���

    public string User_id; //���̵� UserID
    public string User_pw; //��й�ȣ UserPW

    public string User_name; //�̸� UserName
    public string User_Age; //���� 
    public string User_ContractInfo; //����ó 
    public string User_Adress; //�ּ�
    public string User_Gender; //����

    public int User_Point;//���� ����Ʈ UserPoint
    public int AppPoint_list_count; //����Ʈ ���� ���� AppPointCount
    public int AppCoupon_list_count; //���� ������ ���� AppCouponCount


    public static AppDataManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else Instance = this;
    }
    private void Start()
    {
       ClearData();
        //���� �ҷ�����
        Initialization();
     
    }

    void Initialization()
    {
        //�α��� ���
        User_Login = PlayerPrefs.GetString("Login");
        if (User_Login.Equals("UID"))
        {
            User_id = PlayerPrefs.GetString("UID_UserID");
            User_pw = PlayerPrefs.GetString("UID_UserPW");

            User_name = PlayerPrefs.GetString("UID_UserName");
            User_Age = PlayerPrefs.GetString("UID_UserAge");
            User_ContractInfo= PlayerPrefs.GetString("UID_UserContact");
            User_Adress = PlayerPrefs.GetString("UID_UserAdress");
            User_Gender = PlayerPrefs.GetString("UID_UserGender");
     
            User_Point = PlayerPrefs.GetInt("UID_UserPoint");
            AppPoint_list_count = PlayerPrefs.GetInt("UID_AppPointCount"); //����Ʈ ���� ����
            AppCoupon_list_count = PlayerPrefs.GetInt("UID_AppCouponCount");//���� ������ ����

        }
        //���۷α���
        else if(User_Login.Equals("Google"))
        {
            User_id = PlayerPrefs.GetString("Google_UserID");
            User_name = PlayerPrefs.GetString("Google_UserName");
            User_Age = PlayerPrefs.GetString("Google_UserAge");
            User_ContractInfo = PlayerPrefs.GetString("Google_UserContact");
            User_Adress = PlayerPrefs.GetString("Google_UserAdress");
            User_Gender = PlayerPrefs.GetString("Google_UserGender");

            User_Point = PlayerPrefs.GetInt("Google_UserPoint");
            AppPoint_list_count = PlayerPrefs.GetInt("Google_AppPointCount"); //����Ʈ ���� ����
            AppCoupon_list_count = PlayerPrefs.GetInt("Google_AppCouponCount");//���� ������ ����
        }

     
        
        Debug.Log("User_name" + User_name);
        Debug.Log("User_id" + User_id);
        Debug.Log("User_pw" + User_pw);
        Debug.Log("User_Point" + User_Point);


        SetMyPointList();
    }
    void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
    void SetMyPointList()
    {
        Debug.Log("AppPoint_list_count :" + AppPoint_list_count + "AppCoupon_list_count:" + AppCoupon_list_count);
        //����Ʈ ����,��¥,����Ʈ����
        if (AppPoint_list_count > 0)
        {
            //   Debug.Log(PointContent_Title + "," + PointContent_Date + "," + score);
            for (int i = 0; i < AppPoint_list_count; i++)
            {
                //������ ����
                GameObject point = Instantiate(JBApp_UIManager.Instance.PointListPrefeb) as GameObject;
                point.transform.SetParent(JBApp_UIManager.Instance.PointList_Parent.transform, false);

                string pointPrefeb_data = PlayerPrefs.GetString("PointData" + i);
                string[] PointData_split = pointPrefeb_data.Split(',');
               Debug.Log("pointPrefeb_data : "+ pointPrefeb_data);
               // Debug.Log("datat 0: "+PointData_split[0] + "," + "datat 1: " + PointData_split[1] + "datat 2: " + PointData_split[2]);
                point.GetComponent<MyPage_Point>().PointContent_Title.text = PointData_split[0];
                point.GetComponent<MyPage_Point>().PointContent_Date.text = PointData_split[1];
                point.GetComponent<MyPage_Point>().PointContent_Score.text = PointData_split[2];
              
            }
        }
        //������ ���� 
        //���� �̸�, ���� ��¥, ���� ��뿩��, ������ȣ
        if (AppCoupon_list_count > 0)
        {
            JBApp_UIManager.Instance.CouponBtns = new GameObject[AppCoupon_list_count];
            for (int i = 0; i < AppCoupon_list_count-1; i++)
            {
                //������ ����
                GameObject point = Instantiate(JBApp_UIManager.Instance.HaveCouponPrefeb) as GameObject;
                point.transform.SetParent(JBApp_UIManager.Instance.Coupon_Parent.transform, false);
                
                string couponPrefeb_data = PlayerPrefs.GetString("CouponData" + i);
                string[] couponData_split = couponPrefeb_data.Split(',');

                Debug.Log("coupondatat 0: " + couponData_split[0] + "," + "coupondatat 1: " + couponData_split[1] + "coupondatat 2: " + couponData_split[2]);
                point.GetComponent<MyPage_Coupon>().CouponContent_Title.text = couponData_split[0];
                point.GetComponent<MyPage_Coupon>().CouponConent_Date.text = couponData_split[1];
                point.GetComponent<MyPage_Coupon>().CouponConent_state.text = couponData_split[2];
                point.GetComponent<MyPage_Coupon>().CouponConent_Code = couponData_split[3];

                //��ư�� �迭�ȿ� �־���
                JBApp_UIManager.Instance.CouponBtns[i] = point;
                JBApp_UIManager.Instance.CouponBtns[i].GetComponent<Button>().onClick.AddListener(() => JBApp_UIManager.Instance.Show_CouponCodeSign(point));
            }

        }
    }
}
