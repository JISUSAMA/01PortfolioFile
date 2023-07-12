using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnetTime : MonoBehaviour
{

    public static ConnetTime instance { get; private set; }

    public float currTime; //현재 시간
    float maxTime = 3600;  //최대 시간 60분

    public string[] toDayArr;   //오늘 날짜 배열
    string toDay;

    private void OnApplicationQuit()
    {
        ServerManager.Instance.UpdateConnectedState("ConnectTime", PlayerPrefs.GetString("Busan_ConnectTime"));
    }
    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;

        DontDestroyOnLoad(this.gameObject);


        GetTodayInitialization();
        TodayVisitData();
        StartCoroutine(ConnectTime());
    }

    IEnumerator ConnectTime()
    {
        //PlayerPrefs.SetFloat("ConnectTime", 0);

        currTime = PlayerPrefs.GetFloat("Busan_ConnectTime");
        while (currTime <= maxTime)
        {
            currTime += Time.deltaTime;
            PlayerPrefs.SetFloat("Busan_ConnectTime", currTime);
            //Debug.Log("currTime " + currTime);
            yield return new WaitForEndOfFrame();
        }
        PlayerPrefs.SetFloat("Busan_ConnectTime", currTime);
    }

    void GetTodayInitialization()
    {
        toDay = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        toDayArr = new string[4];
        toDayArr[0] = DateTime.Now.ToString("yyyy");
        toDayArr[1] = DateTime.Now.ToString("MM");
        toDayArr[2] = DateTime.Now.ToString("dd");
        toDayArr[3] = DateTime.Now.ToString("HH-mm-ss");
    }

    //private void Update()
    //{
    //    Debug.Log("---GoldUse " + PlayerPrefs.GetString("GoldUse"));
    //}

    void TodayVisitData()
    {
        //PlayerPrefs.SetString("TODAY_DATA", "");
        //Debug.Log("---GoldUse " + PlayerPrefs.GetString("Busan_GoldUse"));

        //오늘날짜와 todayData가 같지 않으면 = 즉 오늘 접속한적이 없으면
        if(PlayerPrefs.GetString("Busan_TODAY_DATA") != toDayArr[0] + toDayArr[1] + toDayArr[2])
        {
            //Debug.Log("방문한적 없어서 초기화 햇줌");
            PlayerPrefs.SetFloat("Busan_ConnectTime", 0);
            PlayerPrefs.SetInt("Busan_TodayQuest1", 0);
            PlayerPrefs.SetInt("Busan_TodayQuest2", 0);
            PlayerPrefs.SetInt("Busan_TodayQuest3", 0);
            
            //방문한적이 없다고 저장
            PlayerPrefs.SetString("Busan_StoreVisit", "No");  //상점방문
            PlayerPrefs.SetString("Busan_InventoryVisit", "No");  //배낭방문
            PlayerPrefs.SetString("Busan_RankJonVisit", "No");    //랭킹방문
            PlayerPrefs.SetString("Busan_MyInfoVisit", "No"); //내정보방문

            PlayerPrefs.SetString("Busan_ProfileChange", "No");   //프로필변경
            PlayerPrefs.SetString("Busan_GameOnePlay", "No"); //게임한번하기
            PlayerPrefs.SetString("Busan_RankUp", "No");  //순위올리기
            PlayerPrefs.SetString("Busan_CustomChange", "No");    //커스텀변경
            PlayerPrefs.SetString("Busan_GoldUse", "No"); //골드사용
            PlayerPrefs.SetString("Busan_ItemUse", "No"); //아이템 사용
            PlayerPrefs.SetString("Busan_ItemPurchase", "No");  //아이템 구매

            //오늘 접속한 날짜를 저장한다
            PlayerPrefs.SetString("Busan_TODAY_DATA", toDayArr[0] + toDayArr[1] + toDayArr[2]);
        }
    }

}
