using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MyPage_Coupon : MonoBehaviour
{
    public Image CouponContentImg; //0: ���� ���� ����Ʈ 1 : ���� �����
    public Text CouponContent_Title;//����Ʈ ���� ����
    public Text CouponConent_Date; //����Ʈ ���� ��¥
    public Text CouponConent_state; //��뿩��
    public string CouponConent_Code; //���� ��ȣ 

    //UI ���� ->�����̸�, ������¥, ������뿩��, ������ȣ
    public void SetCouponData(string title, string date,int price)
    {
        CouponContent_Title.text = title +" "+ price;
        CouponConent_Date.text = date;
        CouponConent_state.text = "�̻��";

        //���� ���� ��ȣ ����
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
