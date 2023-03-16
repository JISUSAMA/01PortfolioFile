using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyPage_Point : MonoBehaviour
{
    public Text PointContent_Title;//포인트 적립 내역
    public Text PointContent_Date; //포인트 적립 날짜
    public Text PointContent_Score; //적립 포인트 

    //UI 셋팅
    public void SetPointData(string title,string date,  string score)
    {
        PointContent_Title.text = title;
        PointContent_Date.text = date;
        PointContent_Score.text = score+"P";
        PlayerPrefs.SetString("PointData" + (AppDataManager.Instance.AppPoint_list_count)
            , PointContent_Title.text + "," + date + "," + score);
        Debug.Log(AppDataManager.Instance.AppPoint_list_count +","+PointContent_Title.text + "," + PointContent_Date.text + "," + score+"P");
    }
}
