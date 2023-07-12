using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System;

public class ExricaiseRecord_UIManager : MonoBehaviour
{
    public static ExricaiseRecord_UIManager instance { get; private set; }

    public Text type_Text;  //걸은시간, 소모칼로리, 걸음수 보여주는 텍스트
    public Text month_Text; //달
    public Text dayMonth_Text;   //일별월별
    public Text yearText;   //년도 텍스트
    public Text Coin_Text;
    public Button monthBtn_Left;    //월 이전 버튼
    public Button monthBtn_Right;   //월 이후 버튼

    public GameObject daily_ob;  //월 선택하는
    public GameObject month_ob; //월 표시 텍스터

    public GameObject timeObj;  //달린시간 그래프
    public GameObject kcalObj;  //소모칼로리 그래프
    public GameObject stepObj;  //걸음수 그래프
    public GameObject distanceObj;  //걸은거리 그래프

    string[] firstLoginPart;

    public bool month_bool = false;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        month_bool = false;
    }


    void Start()
    {
        month_Text.text = ExricaiseRecord_DataManager.instance.ThisMonth() + "month";
        daily_ob.SetActive(true); /*yearImgObj.SetActive(false);*/

        Coin_Text.text = PlayerPrefs.GetInt("Player_Coin").ToString();

        MonthSetActionLeft(month_Text.text);    //첫 로그인 월과 같은 월이면 전 월로 가는 왼쪽 버튼 비활성화
        MonthSetActionRight(month_Text.text); //현 월이랑 같으면 이후 월로 가는 오른쪽버튼 비활성화(데이터가 없기때문)
        StartScrollView(); //처음 시작했을 때 뷰 화면 설정
    }

    //처음 시작했을 때 뷰 화면 설정
    void StartScrollView()
    {
        string startType = PlayerPrefs.GetString("StartView");
        Debug.Log("startType " + startType);
        if (startType == "Distance")
        {
            type_Text.text = "Distance Walked";
            ExricaiseRecord_DataManager.instance.StartScrollViewShow(timeObj, false, kcalObj, false, stepObj, false, distanceObj, true);
            RunDistanceGraphDisplay.instance.DayInitializaion();
        }
        else if (startType == "Time")
        {
            type_Text.text = "Walking Time";
            ExricaiseRecord_DataManager.instance.StartScrollViewShow(timeObj, true, kcalObj, false, stepObj, false, distanceObj, false);
            RunTimeGraphDisplay.instance.DayInitializaion();
        }
        else if (startType == "Kcal")
        {
            type_Text.text = "Calories Burned";
            ExricaiseRecord_DataManager.instance.StartScrollViewShow(timeObj, false, kcalObj, true, stepObj, false, distanceObj, false);
            RunKcalGraphDisplay.instance.DayInitializaion();
        }
        else if (startType == "StepCount")
        {
            type_Text.text = "Step Count";
            ExricaiseRecord_DataManager.instance.StartScrollViewShow(timeObj, false, kcalObj, false, stepObj, true, distanceObj, false);
            RunStepGraphDisplay.instance.DayInitializaion();
        }

    }

    //월 이전이후 버튼 활성화 비활성화
    public void BeforeAfterMonthText()
    {
        string first = ExricaiseRecord_DataManager.instance.FirstLogin();
        char sp = '-';
        firstLoginPart = first.Split(sp); //첫로그인 날짜 분활 ex)2021/04/06

        string strMonth = firstLoginPart[1].Substring(0, 1);
        if (strMonth == "0") //1~9까지 앞에 0이 붙어서 그걸 뺀 문자 저장
            firstLoginPart[1] = firstLoginPart[1].Substring(1, 1);

        string strInt = Regex.Replace(month_Text.text, @"\D", ""); //현재 월 숫자 추출 

        int presentMonth = int.Parse(strInt);   //현재 월
        int presentYear = int.Parse(yearText.text); //현재 보여지는 년도
        int firstMonth = int.Parse(firstLoginPart[1]); //가입 월
        int firstYear = int.Parse(firstLoginPart[0]);   //가입 년도
        int thisYear = int.Parse(DateTime.Now.ToString("yyyy"));    //올해
        int thisMonth = int.Parse(DateTime.Now.ToString("MM")); //이번 달

        //현재 년도와 가입 년도가 같으면(2021 -2021)
        if (firstYear == presentYear)
        {
            //가입 년도와 올해 년도 같으면(2021 - 2021)
            if (firstYear == thisYear)
            {
                //이번 달과 현재 월이 같으면
                if (thisMonth == presentMonth)
                {
                    if (presentMonth == 1)  //현재 1월이면
                        monthBtn_Interactabel(false, false);    //왼쪽 비활 - 오른쪽 비활
                    else if (presentMonth == 12) //12월이면
                        monthBtn_Interactabel(true, false); //왼 활 -오 비활
                    else
                        monthBtn_Interactabel(true, false);  //왼 활 - 오 비활 : 이달 이후에 정보가 없기 때문
                }
                else
                {   //이번 달과 현재 월이 다르면
                    if (presentMonth == 1)
                        monthBtn_Interactabel(false, true);
                    else if (presentMonth == 12)
                        monthBtn_Interactabel(true, false);
                    else
                        monthBtn_Interactabel(true, true);
                }
            }
            else
            {//가입 년도와 올해 년도가 다르면 2020(가입) - 2020(현재뷰) - 2021(올해)
                //가입 월이랑 현재 월이랑 같으면
                if (firstMonth == presentMonth)
                {
                    monthBtn_Interactabel(false, true); //가입한 월 전에는 데이터가 없고, 올해가 아닌 이전 해기 때문에 이후 데이터는 잇음
                }
                else
                {
                    if (presentMonth == 12)
                        monthBtn_Interactabel(true, false);
                    else
                        monthBtn_Interactabel(true, true);
                }
            }
        }
        else
        {//현재 년도와 가입 년도가 다르면 2021(현재) - 2019(가입)
            //현재 년도와 올해 년도가 같으면(2021 - 2021)
            if (presentYear == thisYear)
            {
                // 이번 달이랑 현재 월이랑 같은면
                if (thisMonth == presentMonth)
                {
                    if (presentMonth == 1)
                        monthBtn_Interactabel(false, false);
                    else if (presentMonth == 12)
                        monthBtn_Interactabel(true, false);
                    else
                        monthBtn_Interactabel(true, false);
                }
                else
                {//이번 달이랑 현재적힌 월이랑 다르면
                    if (presentMonth == 1)
                        monthBtn_Interactabel(false, true);
                    else
                        monthBtn_Interactabel(true, true);
                }
            }
            else
            {//현재 년도와 올해 년도가 다르면(2020(현재뷰) - 2021(올해))
                //가입 월이랑 현재 월이랑 같으면
                if (presentMonth == 1)
                    monthBtn_Interactabel(false, true);
                else if (presentMonth == 12)
                    monthBtn_Interactabel(true, false);
                else
                    monthBtn_Interactabel(true, true);
            }
        }
    }

    //월 이전 이후 버튼 활성화/비활성화 함수
    void monthBtn_Interactabel(bool _left, bool _right)
    {
        monthBtn_Left.interactable = _left;
        monthBtn_Right.interactable = _right;
    }


    //첫 로그인 월 전으로 넘기려 할 때 버튼 비활성화
    public void MonthSetActionLeft(string _month)
    {
        string first = ExricaiseRecord_DataManager.instance.FirstLogin();
        char sp = '-';
        firstLoginPart = first.Split(sp); //첫로그인 날짜 분활 ex)2021/04/06

        string strMonth = firstLoginPart[1].Substring(0, 1);
        if (strMonth == "0") //1~9까지 앞에 0이 붙어서 그걸 뺀 문자 저장
            firstLoginPart[1] = firstLoginPart[1].Substring(1, 1);
        //Debug.Log("_month " + _month + " firstLoginPart " + firstLoginPart[1] + "월");
        //첫 로그인 이전의 데이터가 없기때문에 왼쪽 버튼을 비활성화 시킴
        if (_month == firstLoginPart[1] + "month")
        {
            monthBtn_Left.interactable = false;
        }
        else
        {
            monthBtn_Left.interactable = true;
        }
    }

    //월 오른쪽 버튼 활성/ 비활성화
    public void MonthSetActionRight(string _month)
    {
        string thisMonth = ExricaiseRecord_DataManager.instance.ThisMonth();    //현재 월

        string strMonth = thisMonth.Substring(0, 1);
        if (strMonth == "0") //1~9까지 앞에 0이 붙어서 그걸 뺀 문자 저장
            thisMonth = thisMonth.Substring(1, 1);

        //보여지는 월과 현재 월이 같은 경우, 오른쪽 버튼 비활성화
        if (_month == thisMonth + "month")
        {
            monthBtn_Right.interactable = false;
        }
        else
        {
            monthBtn_Right.interactable = true;
        }
    }


    //운동 기록 타입 변경 버튼 좌우이벤트
    public void TypeTextChangeButtonRightOn()
    {
        string type = ExricaiseRecord_DataManager.instance.TypeChangeRightButton(type_Text.text, timeObj, kcalObj, stepObj, distanceObj);
        type_Text.text = type;
        Debug.Log("type " + type);

        if (dayMonth_Text.text == "Daily")
        {
            if (type == "Walking Time")
                RunTimeGraphDisplay.instance.DayInitializaion();
            else if (type == "Calories Burned")
                RunKcalGraphDisplay.instance.DayInitializaion();
            else if (type == "Step Count")
                RunStepGraphDisplay.instance.DayInitializaion();
            else if (type == "Distance Walked")
                RunDistanceGraphDisplay.instance.DayInitializaion();
        }
        else if (dayMonth_Text.text == "Monthly")
        {
            if (type == "Walking Time")
            {
                RunTimeGraphDrawLine.instance.DeleteLineDraw();
                RunTimeGraphDisplay.instance.MonthInitalization();
            }
            else if (type == "Calories Burned")
            {
                RunKcalGraphDrawLine.instance.DeleteLineDraw();
                RunKcalGraphDisplay.instance.MonthInitalization();
            }
            else if (type == "Step Count")
            {
                RunStepGraphDrawLine.instance.DeleteLineDraw();
                RunStepGraphDisplay.instance.MonthInitalization();
            }
            else if (type == "Distance Walked")
            {
                RunDistanceGraphDrawLine.instance.DeleteLineDraw();
                RunDistanceGraphDisplay.instance.MonthInitalization();
            }

        }
    }
    public void TypeTextChangeButtonLeftOn()
    {
        string type = ExricaiseRecord_DataManager.instance.TypeChangeLeftButton(type_Text.text, timeObj, kcalObj, stepObj, distanceObj);
        type_Text.text = type;
        Debug.Log("type " + type + " " + dayMonth_Text.text);

        if (dayMonth_Text.text == "Daily")
        {
            if (type == "Walking Time")
                RunTimeGraphDisplay.instance.DayInitializaion();
            else if (type == "Calories Burned")
                RunKcalGraphDisplay.instance.DayInitializaion();
            else if (type == "Step Count")
                RunStepGraphDisplay.instance.DayInitializaion();
            else if (type == "Distance Walked")
                RunDistanceGraphDisplay.instance.DayInitializaion();
        }
        else if (dayMonth_Text.text == "Monthly")
        {
            Debug.Log("?????????");
            if (type == "Walking Time")
            {
                RunTimeGraphDrawLine.instance.DeleteLineDraw();
                RunTimeGraphDisplay.instance.MonthInitalization();
            }
            else if (type == "Calories Burned")
            {
                RunKcalGraphDrawLine.instance.DeleteLineDraw();
                RunKcalGraphDisplay.instance.MonthInitalization();
            }
            else if (type == "Step Count")
            {
                RunStepGraphDrawLine.instance.DeleteLineDraw();
                RunStepGraphDisplay.instance.MonthInitalization();
            }
            else if (type == "Distance Walked")
            {
                RunDistanceGraphDrawLine.instance.DeleteLineDraw();
                RunDistanceGraphDisplay.instance.MonthInitalization();
            }

        }

    }

    //달 버튼 오른쪽 버튼 이벤트
    public void MonthTextChangeButtonRightOn()
    {
        string month = ExricaiseRecord_DataManager.instance.MonthChangeRightButton(month_Text.text);
        month_Text.text = month;
        BeforeAfterMonthText();

        if (type_Text.text == "Walking Time")
            RunTimeGraphDisplay.instance.DayInitializaion();
        else if (type_Text.text == "Calories Burned")
            RunKcalGraphDisplay.instance.DayInitializaion();
        else if (type_Text.text == "Step Count")
            RunStepGraphDisplay.instance.DayInitializaion();
        else if (type_Text.text == "Distance Walked")
            RunDistanceGraphDisplay.instance.DayInitializaion();
    }
    //달 버튼 왼쪽 버튼 이벤트
    public void MonthTextChangeButtonLeftOn()
    {
        string month = ExricaiseRecord_DataManager.instance.MonthChangeLeftButton(month_Text.text);
        month_Text.text = month;
        BeforeAfterMonthText();

        if (type_Text.text == "Walking Time")
            RunTimeGraphDisplay.instance.DayInitializaion();
        else if (type_Text.text == "Calories Burned")
            RunKcalGraphDisplay.instance.DayInitializaion();
        else if (type_Text.text == "Step Count")
            RunStepGraphDisplay.instance.DayInitializaion();
        else if (type_Text.text == "Distance Walked")
            RunDistanceGraphDisplay.instance.DayInitializaion();
    }

    //일별월별 버튼 좌우이벤트
    public void DayMonthTextChangeButtonLeftRightOn()
    {
        string dayMonth = ExricaiseRecord_DataManager.instance.DayMonthChangeLeftRightButton(dayMonth_Text.text, daily_ob, month_ob/*yearImgObj*/);
        dayMonth_Text.text = dayMonth;
        Debug.Log(month_bool);
        //그래프
        if (dayMonth == "Daily")
        {
            month_bool = false;
            if (type_Text.text == "Walking Time")
            {
                RunTimeGraphDisplay.instance.DayInitializaion();
            }
            else if (type_Text.text == "Calories Burned")
            {
                RunKcalGraphDisplay.instance.DayInitializaion();
            }

            else if (type_Text.text == "Step Count")
            {
                RunStepGraphDisplay.instance.DayInitializaion();
            }

            else if (type_Text.text == "Distance Walked")
            {
                RunDistanceGraphDisplay.instance.DayInitializaion();
            }

        }
        //점선
        else if (dayMonth == "Monthly")
        {
            month_bool = true;
            if (type_Text.text == "Walking Time")
            {

                RunTimeGraphDisplay.instance.MonthInitalization();

            }

            else if (type_Text.text == "Calories Burned")
            {

                RunKcalGraphDisplay.instance.MonthInitalization();

            }

            else if (type_Text.text == "Step Count")
            {

                RunStepGraphDisplay.instance.MonthInitalization();

            }

            else if (type_Text.text == "Distance Walked")
            {
                RunDistanceGraphDisplay.instance.MonthInitalization();
            }

        }
    }

    //백버튼 로비
    public void BackButtonLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
