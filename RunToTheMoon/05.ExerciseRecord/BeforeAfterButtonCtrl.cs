using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeforeAfterButtonCtrl : MonoBehaviour
{
    public Button beforeYearBtn;    //이전버튼 
    public Button afterYearBtn; //이후버튼

    public Text day_MonthText;  //일별 월 텍스트
    public Text dayMontText;    //일별월별 텍스트

  //  public Text yearText_Day;   //일별년도텍스트
   // public Text yearText_Month;   //월별년도텍스트
    public Text yearText; 
    public int before_year, after_year; 
    public Text typeText;   //타입 텍스트


    public string[] toDayArr;   //오늘 날짜 배열
    string[] firstLoginArr;  //첫로그인 날짜 배열


    //Day = '일별'  Month = "월별" -> 일별(타입,읿별/월별,년도,월) 월별(타입,읿별/월별,년도)

    void Start()
    {
        GetTodayInitialization();
        if (dayMontText.text == "일별")
        {
            yearText.text = toDayArr[0];   //올해 년도//2021
           // yearText_Day.text = toDayArr[0];   //올해 년도//2021
          
          //  int present = int.Parse(yearText_Day.text);
            int present = int.Parse(yearText.text);
            before_year = present - 1; //2020
            after_year = present + 1;   //2022

            /*  //올해와 첫로그인 년도가 같으면
              if (yearText_Day.text == firstLoginArr[0])
              {
                  //이전 이후 데이터가 없기에 버튼 비활성화
                  beforeYearBtn.gameObject.SetActive(false);
                  afterYearBtn.gameObject.SetActive(false);
              }
              //올해와 첫 로그인 년도가 다르면
              else if (yearText_Day.text != firstLoginArr[0])
              {
                  //이전 데이터가 있기 때문에 활서화
                  beforeYearBtn.gameObject.SetActive(true);
                  //   beforeYearBtn.transform.GetChild(0).gameObject.GetComponent<Text>().text = (beforeYear-1).ToString();  
                  afterYearBtn.gameObject.SetActive(false);   //이후는 없기에 비활성화
              }*/
            if (yearText.text == firstLoginArr[0])
            {
                //이전 이후 데이터가 없기에 버튼 비활성화
                beforeYearBtn.gameObject.SetActive(false);
                afterYearBtn.gameObject.SetActive(false);
            }
            //올해와 첫 로그인 년도가 다르면
            else if (yearText.text != firstLoginArr[0])
            {
                //이전 데이터가 있기 때문에 활서화
                beforeYearBtn.gameObject.SetActive(true);
                //   beforeYearBtn.transform.GetChild(0).gameObject.GetComponent<Text>().text = (beforeYear-1).ToString();  
                afterYearBtn.gameObject.SetActive(false);   //이후는 없기에 비활성화
            }
        }
        else if (dayMontText.text == "월별")
        {
            yearText.text = toDayArr[0];   //올해 년도

            int present = int.Parse(yearText.text);
            before_year = present - 1; //2020
            after_year = present + 1;   //2022

            //올해와 첫로그인 년도가 같으면
            if (yearText.text == firstLoginArr[0])
            {
                //이전 이후 데이터가 없기에 버튼 비활성화
                beforeYearBtn.gameObject.SetActive(false);
                afterYearBtn.gameObject.SetActive(false);
            }
            //올해와 첫 로그인 년도가 다르면
            else if (yearText.text != firstLoginArr[0])
            {
               // int beforeYear = int.Parse(yearText_Month.text);

                //이전 데이터가 있기 때문에 활서화
                beforeYearBtn.gameObject.SetActive(true);
               // beforeYearBtn.transform.GetChild(0).gameObject.GetComponent<Text>().text = (beforeYear-1).ToString();  
                afterYearBtn.gameObject.SetActive(false);   //이후는 없기에 비활성화
            }
        }
    }

    //오늘 날짜 초기화 / 첫 로그인
    void GetTodayInitialization()
    {
        toDayArr = new string[4];
        toDayArr[0] = DateTime.Now.ToString("yyyy");
        toDayArr[1] = DateTime.Now.ToString("MM");
        toDayArr[2] = DateTime.Now.ToString("dd");
        toDayArr[3] = DateTime.Now.ToString("HH-mm-ss");

        string strSub = toDayArr[2].Substring(0, 1);
        if (strSub == "0") //1~9까지 앞에 0이 붙어서 그걸 뺀 문자 저장
            toDayArr[2] = toDayArr[2].Substring(1, 1);

        string strMonth = toDayArr[1].Substring(0, 1);
        if (strMonth == "0") //1~9까지 앞에 0이 붙어서 그걸 뺀 문자 저장
            toDayArr[1] = toDayArr[1].Substring(1, 1);

        //처음 가입한 정보 들고오기(월)
        string first = ExricaiseRecord_DataManager.instance.FirstLogin();
        char sp = '-';
        firstLoginArr = first.Split(sp); //첫로그인 날짜 분활 ex)2021/04/06
    }


    //이전 년도 버튼 클릭
    public void BeforeButtonOn()
    {
        int present = int.Parse(yearText.text);
        //현재 년도
        if (dayMontText.text == "일별")
        {
          //  int present = int.Parse(yearText.text);
            before_year = present - 1; //2020
            after_year = present + 1;   //2022

            yearText.text = before_year.ToString();  
            day_MonthText.text = "12월";
        }
        else if (dayMontText.text == "월별")
        {
            before_year = present - 1; //2020
            after_year = present + 1;   //2022
            yearText.text = before_year.ToString();
         // day_MonthText.text = "12월";
        }
        BeforeAfterButtonAction();
        ExricaiseRecord_UIManager.instance.BeforeAfterMonthText();   //월별 버튼 활성비활성

        if (dayMontText.text == "일별")
        {
            if (typeText.text == "걸은 시간")
                RunTimeGraphDisplay.instance.DayInitializaion();
            else if (typeText.text == "소모 칼로리")
                RunKcalGraphDisplay.instance.DayInitializaion();
            else if (typeText.text == "걸음수")
                RunStepGraphDisplay.instance.DayInitializaion();
            else if (typeText.text == "걸은 거리")
                RunDistanceGraphDisplay.instance.DayInitializaion();
        }
        else if (dayMontText.text == "월별")
        {
            if (typeText.text == "걸은 시간")
            {
                RunTimeGraphDrawLine.instance.DeleteLineDraw();
                RunTimeGraphDisplay.instance.MonthInitalization();
            }
            else if (typeText.text == "소모 칼로리")
            {
                RunKcalGraphDrawLine.instance.DeleteLineDraw();
                RunKcalGraphDisplay.instance.MonthInitalization();
            } 
            else if (typeText.text == "걸음수")
            {
                RunKcalGraphDrawLine.instance.DeleteLineDraw();
                RunStepGraphDisplay.instance.MonthInitalization();
            }
            else if (typeText.text == "걸은 거리")
            {
                RunDistanceGraphDrawLine.instance.DeleteLineDraw();
                RunDistanceGraphDisplay.instance.MonthInitalization();
            }
                
        }
            
    }

    //이후 년도 버튼 클릭
    public void AfterButtonOn()
    {
        int present = int.Parse(yearText.text);
        //현재 년도
        if (dayMontText.text.Equals("일별"))
        {
            
            before_year = present - 1;
            after_year = present + 1;
            yearText.text = after_year.ToString();
            day_MonthText.text = "1월";

        }
        else if (dayMontText.text.Equals("월별"))
        {
           
            before_year = present - 1;
            after_year = present + 1;
            yearText.text = after_year.ToString();
        }

        BeforeAfterButtonAction();
        ExricaiseRecord_UIManager.instance.BeforeAfterMonthText(); //월별 버튼 활성비활성

        if (dayMontText.text == "일별")
        {
            if (typeText.text == "걸은 시간")
                RunTimeGraphDisplay.instance.DayInitializaion();
            else if (typeText.text == "소모 칼로리")
                RunKcalGraphDisplay.instance.DayInitializaion();
            else if (typeText.text == "걸음수")
                RunStepGraphDisplay.instance.DayInitializaion();
            else if (typeText.text == "걸은 거리")
                RunDistanceGraphDisplay.instance.DayInitializaion();
        }  
        else if (dayMontText.text == "월별")
        {
            if (typeText.text == "걸은 시간")
            {
                RunTimeGraphDrawLine.instance.DeleteLineDraw();
                RunTimeGraphDisplay.instance.MonthInitalization();
            }
            else if (typeText.text == "소모 칼로리")
            {
                RunKcalGraphDrawLine.instance.DeleteLineDraw();
                RunKcalGraphDisplay.instance.MonthInitalization();
            }
            else if (typeText.text == "걸음수")
            {
                RunStepGraphDrawLine.instance.DeleteLineDraw();
                RunStepGraphDisplay.instance.MonthInitalization();
            }
            else if (typeText.text == "걸은 거리")
            {
                RunDistanceGraphDrawLine.instance.DeleteLineDraw();
                RunDistanceGraphDisplay.instance.MonthInitalization();
            }
                
        }
            
    }

    //이전년도, 이후년도 버튼 활성화 비활성화 함수
    void BeforeAfterButtonAction()
    { 
        int presentYear =0;
        if (dayMontText.text == "일별")
        {
             presentYear = int.Parse(yearText.text); //현재 보여지는 년도
        }
        else if(dayMontText.text == "월별")
        {
             presentYear = int.Parse(yearText.text); //현재 보여지는 년도
        }

        int firstYear = 2018/*int.Parse(firstLoginArr[0])*/;    //첫 가입 년도
        int thisYear = int.Parse(toDayArr[0]);  //올해 년도

        Debug.Log("presentYear" + presentYear + "firstYear" + firstYear + "thisYear" + thisYear);
        //현재 보여지는 년도와 가입 년도가 같으면
        if (firstYear == presentYear)
        {
            //가입 - 현재가 같으면
            if (firstYear == thisYear)
            {
                //가입 년도와 현재가 같으면 데이터가 없기 때문에 버튼 둘다 비활성화
                beforeYearBtn.gameObject.SetActive(false);
                afterYearBtn.gameObject.SetActive(false);
            }
            else
            {
                //가입2019년 현재 2019년 올해가 현재랑 다르면
                //이전 데이터가 없기 때문에 이전비활성 / 이후버튼 활성화
                beforeYearBtn.gameObject.SetActive(false);
                afterYearBtn.gameObject.SetActive(true);
               // afterYearBtn.transform.GetChild(0).gameObject.GetComponent<Text>().text = (presentYear + 1).ToString();   //이전 년도 적기
            }
        }
        else
        {//현재 보여지는 년도와 가입 년도가 다르면 (현재 2021 - 가입년도 2018년)

            //현재 2021 올해 2021 가입 2018년이면 
            if (presentYear == thisYear)
            {
                //이전 데이터가 있기때문에 이전 활성화 이후는 없기에 비활성화
                beforeYearBtn.gameObject.SetActive(true);
               // beforeYearBtn.transform.GetChild(0).gameObject.GetComponent<Text>().text = (presentYear ).ToString();   //이전 년도 적기
                afterYearBtn.gameObject.SetActive(false);
            }
            else
            {//현재 2020 올해 2021 가입 2018이면
                //이전 이후 데이터가 다 있기때문에 둘 다 활성화
                beforeYearBtn.gameObject.SetActive(true);
                //beforeYearBtn.transform.GetChild(0).gameObject.GetComponent<Text>().text = (presentYear ).ToString();   //이전 년도 적기
                afterYearBtn.gameObject.SetActive(true);
               // afterYearBtn.transform.GetChild(0).gameObject.GetComponent<Text>().text = (presentYear + 1).ToString();   //이전 년도 적기
            }
        }
    }
}
