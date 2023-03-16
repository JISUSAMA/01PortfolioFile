using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MyPage_Coupon : MonoBehaviour
{
    public Image CouponContentImg; //0: 전주 돼지 포인트 1 : 전주 맛배달
    public Text CouponContent_Title;//포인트 적립 내역
    public Text CouponConent_Date; //포인트 적립 날짜
    public Text CouponConent_state; //사용여부
    public string CouponConent_Code; //쿠폰 번호 

    //UI 셋팅 ->쿠폰이름, 쿠폰날짜, 쿠폰사용여부, 쿠폰번호
    public void SetCouponData(string title, string date,int price)
    {
        CouponContent_Title.text = title +" "+ price;
        CouponConent_Date.text = date;
        CouponConent_state.text = "미사용";

        //랜덤 쿠폰 번호 생성
        string str = "Q,W,E,R,T,Y,U,I,O,P,A,S,D,F,G,H,J,K,L,Z,X,C,V,B,N,M,0,1,2,3,4,5,6,7,8,9";
        string[] str_word = str.Split(',');

        for (int p = 0; p < 4; p++)
        {
            for (int i = 0; i < 4; i++)
            {
                CouponConent_Code += str_word[Random.Range(0, str_word.Length)];
                Debug.Log("CouponConent_Code" + CouponConent_Code + "Random :" + Random.Range(0, str_word.Length));
            }
            if (p != 3) CouponConent_Code += "-";
        }

        PlayerPrefs.SetString("CouponData" + AppDataManager.Instance.AppCoupon_list_count
            , CouponContent_Title.text + "," + CouponConent_Date.text + "," + CouponConent_state.text+","+CouponConent_Code);
   
    }
}
