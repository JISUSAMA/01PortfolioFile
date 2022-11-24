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
        if (_type == "걸은 시간")
        {
            RunTimeGraphDrawLine.instance.DeleteLineDraw();
            _type = "걸은 거리";
            _time.SetActive(false);
            _kcal.SetActive(false);
            _stepCount.SetActive(false);
            _distance.SetActive(true);
        }
        else if (_type == "걸은 거리")
        {
            RunDistanceGraphDrawLine.instance.DeleteLineDraw();
            _type = "걸음수";
            _time.SetActive(false);
            _kcal.SetActive(false);
            _stepCount.SetActive(true);
            _distance.SetActive(false);
        }
        else if (_type == "걸음수")
        {
            RunStepGraphDrawLine.instance.DeleteLineDraw();
            _type = "소모 칼로리";
            _time.SetActive(false);
            _kcal.SetActive(true);
            _stepCount.SetActive(false);
            _distance.SetActive(false);
        }
        else if (_type == "소모 칼로리")
        {
            RunKcalGraphDrawLine.instance.DeleteLineDraw();
            _type = "걸은 시간";
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
        if (_type == "걸은 시간")
        {
            RunTimeGraphDrawLine.instance.DeleteLineDraw();
            _type = "소모 칼로리";
            _time.SetActive(false);
            _kcal.SetActive(true);
            _stepCount.SetActive(false);
            _distance.SetActive(false);
        }
        else if (_type == "소모 칼로리")
        {
            RunKcalGraphDrawLine.instance.DeleteLineDraw();
            _type = "걸음수";
            _time.SetActive(false);
            _kcal.SetActive(false);
            _stepCount.SetActive(true);
            _distance.SetActive(false);
        }
        else if (_type == "걸음수")
        {
            RunStepGraphDrawLine.instance.DeleteLineDraw();
            _type = "걸은 거리";
            _time.SetActive(false);
            _kcal.SetActive(false);
            _stepCount.SetActive(false);
            _distance.SetActive(true);
        }
        else if (_type == "걸은 거리")
        {
            RunDistanceGraphDrawLine.instance.DeleteLineDraw();
            _type = "걸은 시간";
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
        if (_month == "1월")
            _month = "12월";
        else if (_month == "2월")
            _month = "1월";
        else if (_month == "3월")
            _month = "2월";
        else if (_month == "4월")
            _month = "3월";
        else if (_month == "5월")
            _month = "4월";
        else if (_month == "6월")
            _month = "5월";
        else if (_month == "7월")
            _month = "6월";
        else if (_month == "8월")
            _month = "7월";
        else if (_month == "9월")
            _month = "8월";
        else if (_month == "10월")
            _month = "9월";
        else if (_month == "11월")
            _month = "10월";
        else if (_month == "12월")
            _month = "11월";

        return _month;
    }
    //월 텍스트 설정 오른쪽 버튼
    public string MonthChangeRightButton(string _month)
    {
        if (_month == "1월")
            _month = "2월";
        else if (_month == "2월")
            _month = "3월";
        else if (_month == "3월")
            _month = "4월";
        else if (_month == "4월")
            _month = "5월";
        else if (_month == "5월")
            _month = "6월";
        else if (_month == "6월")
            _month = "7월";
        else if (_month == "7월")
            _month = "8월";
        else if (_month == "8월")
            _month = "9월";
        else if (_month == "9월")
            _month = "10월";
        else if (_month == "10월")
            _month = "11월";
        else if (_month == "11월")
            _month = "12월";
        else if (_month == "12월")
            _month = "1월";

        return _month;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////
    //일별월별 텍스트 설정 좌우 (탑 bar에 표시되는 오브젝트 , 월별/ 일별 )
    public string DayMonthChangeLeftRightButton(string _dayMonth, GameObject _daily, GameObject _month)
    {
        if (_dayMonth == "일별")
        {
            _dayMonth = "월별";
            _daily.SetActive(false);
            _month.SetActive(true);
        }
        else if (_dayMonth == "월별")
        {
            _dayMonth = "일별";
            _daily.SetActive(true);
            _month.SetActive(false);
        }
        return _dayMonth;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////

}
