using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExricaiseRecord_DataManager : MonoBehaviour
{
    public static ExricaiseRecord_DataManager instance { get; private set; }

    public string[] toDayArr;   //오늘 날짜 배열

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        GetTodayInitialization();
    }

    //이번달 
    public string ThisMonth()
    {
        return toDayArr[1];
    }
    //이번년
    public string ThisYear()
    {
        return toDayArr[0];
    }

    //첫 로그인 날짜
    public string FirstLogin()
    {
        string firstLogin = PlayerPrefs.GetString("FirstLoginTime");

        return firstLogin;
    }

    //처음 페이지 설정
    public void StartScrollViewShow(GameObject _time, bool _timeState, GameObject _kcal, bool _kcalState, GameObject _stepCount, bool _stepState, GameObject _distance, bool _distanceState)
    {
        _time.SetActive(_timeState);
        _kcal.SetActive(_kcalState);
        _stepCount.SetActive(_stepState);
        _distance.SetActive(_distanceState);
    }



    //오늘 날짜 초기화
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
    }


    ////////////////////////////////////////////////////////////////////////////////////////////
    //타입 텍스트 설정 왼쪽 버튼
    public string TypeChangeLeftButton(string _type, GameObject _time, GameObject _kcal, GameObject _stepCount, GameObject _distance)
    {
        if (_type == "Walking Time")
        {
            RunTimeGraphDrawLine.instance.DeleteLineDraw();
            _type = "Distance Walked";
            _time.SetActive(false);
            _kcal.SetActive(false);
            _stepCount.SetActive(false);
            _distance.SetActive(true);
        }
        else if (_type == "Distance Walked")
        {
            RunDistanceGraphDrawLine.instance.DeleteLineDraw();
            _type = "Step Count";
            _time.SetActive(false);
            _kcal.SetActive(false);
            _stepCount.SetActive(true);
            _distance.SetActive(false);
        }
        else if (_type == "Step Count")
        {
            RunStepGraphDrawLine.instance.DeleteLineDraw();
            _type = "Calories Burned";
            _time.SetActive(false);
            _kcal.SetActive(true);
            _stepCount.SetActive(false);
            _distance.SetActive(false);
        }
        else if (_type == "Calories Burned")
        {
            RunKcalGraphDrawLine.instance.DeleteLineDraw();
            _type = "Walking Time";
            _time.SetActive(true);
            _kcal.SetActive(false);
            _stepCount.SetActive(false);
            _distance.SetActive(false);
        }

        return _type;
    }
    //타입 텍스트 설정 오른쪽 버튼
    public string TypeChangeRightButton(string _type, GameObject _time, GameObject _kcal, GameObject _stepCount, GameObject _distance)
    {
        if (_type == "Walking Time")
        {
            RunTimeGraphDrawLine.instance.DeleteLineDraw();
            _type = "Calories Burned";
            _time.SetActive(false);
            _kcal.SetActive(true);
            _stepCount.SetActive(false);
            _distance.SetActive(false);
        }
        else if (_type == "Calories Burned")
        {
            RunKcalGraphDrawLine.instance.DeleteLineDraw();
            _type = "Step Count";
            _time.SetActive(false);
            _kcal.SetActive(false);
            _stepCount.SetActive(true);
            _distance.SetActive(false);
        }
        else if (_type == "Step Count")
        {
            RunStepGraphDrawLine.instance.DeleteLineDraw();
            _type = "Distance Walked";
            _time.SetActive(false);
            _kcal.SetActive(false);
            _stepCount.SetActive(false);
            _distance.SetActive(true);
        }
        else if (_type == "Distance Walked")
        {
            RunDistanceGraphDrawLine.instance.DeleteLineDraw();
            _type = "Walking Time";
            _time.SetActive(true);
            _kcal.SetActive(false);
            _stepCount.SetActive(false);
            _distance.SetActive(false);
        }

        return _type;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////
    //월 텍스트 설정 왼쪽 버튼
    public string MonthChangeLeftButton(string _month)
    {
        if (_month == "Jan")
            _month = "Dec";
        else if (_month == "Feb")
            _month = "Jan";
        else if (_month == "Mar")
            _month = "Feb";
        else if (_month == "Apr")
            _month = "Mar";
        else if (_month == "May")
            _month = "Apr";
        else if (_month == "Jun")
            _month = "May";
        else if (_month == "Jul")
            _month = "Jun";
        else if (_month == "Aug")
            _month = "Jul";
        else if (_month == "Sep")
            _month = "Aug";
        else if (_month == "Oct")
            _month = "Sep";
        else if (_month == "Nov")
            _month = "Oct";
        else if (_month == "Dec")
            _month = "Nov";

        return _month;
    }
    //월 텍스트 설정 오른쪽 버튼
    public string MonthChangeRightButton(string _month)
    {
        if (_month == "Jan")
            _month = "Feb";
        else if (_month == "Feb")
            _month = "Mar";
        else if (_month == "Mar")
            _month = "Apr";
        else if (_month == "Apr")
            _month = "May";
        else if (_month == "May")
            _month = "Jun";
        else if (_month == "Jun")
            _month = "Jul";
        else if (_month == "Jul")
            _month = "Aug";
        else if (_month == "Aug")
            _month = "Sep";
        else if (_month == "Sep")
            _month = "Oct";
        else if (_month == "Oct")
            _month = "Nov";
        else if (_month == "Nov")
            _month = "Dec";
        else if (_month == "Dec")
            _month = "Jan";

        return _month;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////
    //일별월별 텍스트 설정 좌우 (탑 bar에 표시되는 오브젝트 , 월별/ 일별 )
    public string DayMonthChangeLeftRightButton(string _dayMonth, GameObject _daily, GameObject _month)
    {
        if (_dayMonth == "Daily")
        {
            _dayMonth = "Monthly";
            _daily.SetActive(false);
            _month.SetActive(true);
        }
        else if (_dayMonth == "Monthly")
        {
            _dayMonth = "Daily";
            _daily.SetActive(true);
            _month.SetActive(false);
        }
        return _dayMonth;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////

}
