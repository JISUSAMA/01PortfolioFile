using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class RunDistanceGraphDisplay : MonoBehaviour
{
    public static RunDistanceGraphDisplay instance { get; private set; }

    public GameObject[] timeView;
    public string[] toDayArr;   //오늘 날짜 배열

    public Text day_MonthText; //일별 달 텍스트
    public Text month_MonthText;    //월별 달 텍스트
    public Text yearText;   //년도텍스트
                            //  public Text day_yearText; 
                            // public Text month_yearText; 

    [SerializeField]
    ContentSizeFitter csf;


    string[] firstLoginArr;  //첫로그인 날짜 배열


    DayData dayData_scrip;
    int maxDay = 12;    //보여줄 날짜 최소한의 숫자

    //주행거리
    [Header("RunDistance_About Graphs")]
    public Text _month_day_text;
    public Text _one_third_Size;
    public Text _two_third_Size;
    // public float middle_step = 0;
    // public float max_step = 0;
    public float top_heightSize = 0;
    public float one_third_heightSize = 0;
    public float two_third_heightSize = 0;

    float biggestValue = 0f;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        //DayInitializaion();
    }

    //일별 초기화
    public void DayInitializaion()
    {
        DataInit();
        GetTodayInitialization();
        DayDataInitialization(yearText.text);
        DetailDataText();
        DayDataDelete();
        _GraphTypeImge("graph");
        RunDistanceGraphDrawLine.instance.DeleteLineDraw();
    }

    //월별 초기화
    public void MonthInitalization()
    {
        DataInit();
        GetTodayInitialization();
        MonthDataInitialization(yearText.text);
        DetailDataText();
        DayDataDelete();
        _GraphTypeImge("dot");
        RunDistanceGraphDrawLine.instance.GraphDrawLineMake();
    }

    //자세히 보기 클릭 시 그래프 상단에 있는 텍스트 비활성화
    void DetailDataText()
    {
        for (int i = 0; i < 31; i++)
        {
            dayData_scrip = timeView[i].GetComponent<DayData>();
            dayData_scrip.clickOnState = false; //클릭안했다고 변경
            timeView[i].transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);    //데이터정보를 비활성화
        }
    }
    public void DataInit()
    {
        maxDay = 12;    //화면에 보여줄 최대 숫자
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

    //데이터 초기화 그래프(일별)
    public void DayDataInitialization(string _year)
    {
        int presentYear = int.Parse(_year); //현재 보여지는 년도
        int firstYear = int.Parse(firstLoginArr[0]);    //첫 가입 년도
        int thisYear = int.Parse(toDayArr[0]);  //올해 년도

        int toDay = int.Parse(toDayArr[2]);   //오늘
        int thisMonth = int.Parse(toDayArr[1]); //이번 달
        int firstMonth = int.Parse(firstLoginArr[1]);   //가입 월
        int firstDay = int.Parse(firstLoginArr[2]); //가일 일

        string strInt = Regex.Replace(day_MonthText.text, @"\D", ""); //현재 월 숫자 추출 
        int presentMonth = int.Parse(strInt);   //현재 월
        int lastDay = DateTime.DaysInMonth(presentYear, presentMonth);  //월의 마지막 일 구하기
        Debug.Log("presentYear " + presentYear + " presentMonth " + presentMonth + " lastDay " + lastDay);

        //가입 년도와 현재 년도가 같다면(2021 - 2021)
        if (firstYear == presentYear)
        {
            //가입 년도와 올해가 같다면(2021 - 2021)
            if (firstYear == thisYear)
            {
                //이번 달이랑 현재 월이랑 같은면
                if (thisMonth == presentMonth)
                {
                    DayDataInit(thisYear, presentMonth, toDay, 0);
                    DayTextSetActionInit(firstYear, firstMonth, toDay, lastDay, firstDay);
                }
                else
                {//이번 달이랑 현재 월이 다르면

                    //가입한 달이랑 현재 월이랑 같으면
                    if (firstMonth == presentMonth)
                    {
                        //가입한 전 일에는 데이터가 없기때문에 시작을 가입일로 시작해서 그 달 마지막일 까지 데이터를 준다.
                        DayDataInit(thisYear, presentMonth, lastDay, firstDay - 1);
                        DayTextSetActionInit(firstYear, firstMonth, toDay, lastDay, firstDay);
                    }
                    else
                    {//가입한 달이랑 현재 월이 다르면
                        //가입한 달이 아니기 때문에 그 달은 전부 데이터를 넣어준다.
                        DayDataInit(thisYear, presentMonth, lastDay, 0);
                        DayTextSetActionInit(firstYear, firstMonth, lastDay, lastDay, firstDay);
                    }
                }
            }
            else
            { //가입 년도와 올해가 다르면 (2020년 - 2021년 예) 즉 과거 데이터
                //가입한 달이랑 현재 월이랑 같으면
                if (firstMonth == presentMonth)
                {
                    //가입한 전 일에는 데이터가 없기때문에 시작을 가입일로 시작해서 그 달 마지막일 까지 데이터를 준다.
                    DayDataInit(firstYear, presentMonth, lastDay, firstDay - 1);
                    DayTextSetActionInit(firstYear, firstMonth, lastDay, lastDay, firstDay);
                }
                else
                {//가입한 달이랑 현재 월이 다르면
                 //가입한 달이 아니기 때문에 그 달은 전부 데이터를 넣어준다.
                    DayDataInit(firstYear, presentMonth, lastDay, 0);
                    DayTextSetActionInit(firstYear, firstMonth, lastDay, lastDay, firstDay);
                }
            }
        }
        else
        { //가입 년도와 현재 년도가 다를 때 (2019 - 2021)
            //보이는 년도와 올해 년도가 같으면(2021-2021)
            if (presentYear == thisYear)
            {
                //이번 달이랑 현재 월이랑 같은면
                if (thisMonth == presentMonth)
                {
                    DayDataInit(presentYear, presentMonth, toDay, 0);
                    DayTextSetActionInit(firstYear, firstMonth, toDay, lastDay, firstDay);
                }
                else
                {   //이번 달과 현재 월이 다르면
                    //이번 달도 아니고 가입년도가 다르기 때문에 풀 데이터
                    DayDataInit(presentYear, presentMonth, lastDay, 0);
                    DayTextSetActionInit(firstYear, firstMonth, lastDay, lastDay, firstDay);
                }
            }
            else
            {//현재 년도와 올해 년도가 다르면
             //이번 달과 현재 월이 다르면
             //이번 달도 아니고 가입년도가 다르기 때문에 풀 데이터
                DayDataInit(presentYear, presentMonth, lastDay, 0);
                DayTextSetActionInit(firstYear, firstMonth, lastDay, lastDay, firstDay);
            }
        }
    }

    void DayDataDelete()
    {
        //데이터 초기화
        for (int i = 0; i < 31; i++)
        {
            dayData_scrip = timeView[i].GetComponent<DayData>();

            dayData_scrip.y = 0;
            dayData_scrip.posY = 0;

            top_heightSize = 0;
            one_third_heightSize = 0;
            two_third_heightSize = 0;
            dayData_scrip.LengthExtend();
        }
    }
    void _GraphTypeImge(string s)
    {
        string _Graph_type = s;
        if (_Graph_type.Equals("graph"))
        {
            for (int j = 0; j < timeView.Length; j++)
            {
                timeView[j].gameObject.GetComponent<DayData>().GraphImg_Change();
            }
        }
        if (_Graph_type.Equals("dot"))
        {
            for (int j = 0; j < timeView.Length; j++)
            {
                timeView[j].gameObject.GetComponent<DayData>().DotImg_Change();
            }
        }

    }
    //일별 데이터 집어 넣는 함수
    void DayDataInit(int _year, int _presentMonth, int _today, int _startDay)
    {
        // 서버에서 데이터 들고오기
        StartCoroutine(_DayDataInit(_year, _presentMonth, _today, _startDay));
    }

    IEnumerator _DayDataInit(int _year, int _presentMonth, int _today, int _startDay)
    {
        ServerManager.Instance.Get_DateData(_year, _presentMonth, _today, _startDay);

        yield return new WaitUntil(() => ServerManager.Instance.isGetDateDataCompleted);

        ServerManager.Instance.isGetDateDataCompleted = false;
        top_heightSize = 0f;
        biggestValue = 0f;
        // 날짜를 알기위한 배열
        string[] arrDate;

        // 실제 데이터의 최대 높이 구하기
        for (int max = 0; max < ServerManager.Instance.dataByDate.Count; max++)
        {
            if (top_heightSize < ServerManager.Instance.dataByDate[max].today_Distance)
            {
                top_heightSize = ServerManager.Instance.dataByDate[max].today_Distance;
                biggestValue = ServerManager.Instance.dataByDate[max].today_Distance;
            }
        }

        Debug.Log("biggestValue : " + biggestValue);
        
        // 거리 km, m 단위
        top_heightSize += top_heightSize / 10;

        Debug.Log("top_heightSize : " + top_heightSize);

        one_third_heightSize = Mathf.Floor(biggestValue / 3f) == 0 ? (biggestValue / 3f) : Mathf.Floor(biggestValue / 3f);

        Debug.Log("one_third_heightSize : " + one_third_heightSize);

        two_third_heightSize = Mathf.Floor(one_third_heightSize * 2) == 0 ? (one_third_heightSize * 2f) : Mathf.Floor(one_third_heightSize * 2f);
        _one_third_Size.text = one_third_heightSize.ToString("N2") + " Km";
        _two_third_Size.text = two_third_heightSize.ToString("N2") + " Km";

        //데이터 넣기
        for (int i = _startDay; i < _today; i++)
        {
            dayData_scrip = timeView[i].GetComponent<DayData>();

            if (_presentMonth == 1)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = ServerManager.Instance.dataByDate[j].today_Distance; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.posY = 0f;
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_presentMonth == 2)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = ServerManager.Instance.dataByDate[j].today_Distance; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.posY = 0f;
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_presentMonth == 3)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = ServerManager.Instance.dataByDate[j].today_Distance; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.posY = 0f;
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_presentMonth == 4)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = ServerManager.Instance.dataByDate[j].today_Distance; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.posY = 0f;
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_presentMonth == 5)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = ServerManager.Instance.dataByDate[j].today_Distance; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.posY = 0f;
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_presentMonth == 6)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = ServerManager.Instance.dataByDate[j].today_Distance; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.posY = 0f;
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_presentMonth == 7)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = ServerManager.Instance.dataByDate[j].today_Distance; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.posY = 0f;
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_presentMonth == 8)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = ServerManager.Instance.dataByDate[j].today_Distance; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.posY = 0f;
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_presentMonth == 9)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = ServerManager.Instance.dataByDate[j].today_Distance; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.posY = 0f;
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_presentMonth == 10)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = ServerManager.Instance.dataByDate[j].today_Distance; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.posY = 0f;
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_presentMonth == 11)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = ServerManager.Instance.dataByDate[j].today_Distance; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.posY = 0f;
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_presentMonth == 12)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = ServerManager.Instance.dataByDate[j].today_Distance; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.posY = 0f;
                        dayData_scrip.LengthExtend();
                    }
                }
            }
        }
    }

    //일별 그래프 생성해주는 함수
    public void DayTextSetActionInit(int _firstYear, int _firstMonth, int _today, int _lastday, int _firstDay)
    {
        Debug.Log(_today);
        //한 하면에 보여지는 갯수 12개보다 작거나 같으면 12개 그대로
        if (_today <= maxDay)
            maxDay = 12;
        else
            maxDay = maxDay + (_today - maxDay);    //15일일 경우 maxDay=15여야함. 12 + (15-12) = 15임

        if (maxDay > _lastday)
            maxDay = _lastday;  //월의 마지막을 지정 29일~31일 경우 때문에

        for (int i = 0; i < maxDay; i++)
        {
            timeView[i].SetActive(true);
            //   timeView[i].gameObject.GetComponent<DayData>().GraphImg_Change();
            //첫 가입한 년도와 그 달이면 (가입 이전 일은 비활성시켜야해서)
            if (yearText.text == _firstYear.ToString() && day_MonthText.text == _firstMonth.ToString() + "월")
            {
                //첫 가입 날짜
                if (i < _firstDay - 1)
                {
                    timeView[i].transform.GetComponent<Button>().interactable = false;  //그래프 클릭 금지
                    string dayText = timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>().text;

                    //날짜 회색 변경 - 해당 날짜가 아닌거
                    Text text = timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>();
                    text.color = Color.gray;

                    timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = dayText;
                    timeView[i].transform.GetChild(1).gameObject.SetActive(false);  //그래프 이미지 비활서화
                }
                //오늘 날짜 이후 그래프 비활성화 (7일면 8일~12일까지)
                else if (i > _today - 1)
                {
                    timeView[i].transform.GetComponent<Button>().interactable = false;  //그래프 클릭 금지
                    string dayText = timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>().text;

                    //날짜 회색 변경 - 해당 날짜가 아닌거
                    Text text = timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>();
                    text.color = Color.gray;

                    timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = dayText;
                    timeView[i].transform.GetChild(1).gameObject.SetActive(false);  //그래프 이미지 비활서화
                }
                else
                {
                    //해당 날짜 활성화 및 색상 진하게
                    timeView[i].transform.GetComponent<Button>().interactable = true;
                    string dayText2 = timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>().text;

                    //텍스트 색상변경
                    Text text = timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>();
                    text.color = Color.white;

                    timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = dayText2;
                    timeView[i].transform.GetChild(1).gameObject.SetActive(true);  //그래프 이미지 비활서화
                }
            }
            else
            {
                //오늘 날짜 이후 그래프 비활성화 (7일면 8일~12일까지)
                if (i > _today - 1)
                {
                    timeView[i].transform.GetComponent<Button>().interactable = false;  //그래프 클릭 금지
                    string dayText = timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>().text;

                    //날짜 회색 변경 - 해당 날짜가 아닌거
                    Text text = timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>();
                    text.color = Color.gray;

                    timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = dayText;
                    timeView[i].transform.GetChild(1).gameObject.SetActive(false);  //그래프 이미지 비활서화
                }
                else
                {
                    //해당 날짜 활성화 및 색상 진하게
                    timeView[i].transform.GetComponent<Button>().interactable = true;
                    string dayText2 = timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>().text;

                    //텍스트 색상변경
                    Text text = timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>();
                    text.color = Color.white;

                    timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = dayText2;
                    timeView[i].transform.GetChild(1).gameObject.SetActive(true);  //그래프 이미지 비활서화
                }
            }
        }

        //보여지는 날짜 이후꺼는 전부 비활성화
        for (int j = maxDay; j < 31; j++)
        {
            timeView[j].SetActive(false);
        }

        //컨텐츠필드 사이즈 리프레쉬
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)csf.transform);
    }


    //데이터 초기화 그래프(월별)
    public void MonthDataInitialization(string _year)
    {
        int presentYear = int.Parse(_year); //현재 보여지는 년도
        int firstYear = int.Parse(firstLoginArr[0]);    //첫 가입 년도
        int thisYear = int.Parse(toDayArr[0]);  //올해 년도

        int toDay = int.Parse(toDayArr[2]);   //오늘
        int thisMonth = int.Parse(toDayArr[1]); //이번 달
        int firstMonth = int.Parse(firstLoginArr[1]);   //가입 월
        int firstDay = int.Parse(firstLoginArr[2]); //가일 일

        string strInt = Regex.Replace(month_MonthText.text, @"\D", ""); //현재 월 숫자 추출 
        int presentMonth = int.Parse(strInt);   //현재 월
        int lastDay = DateTime.DaysInMonth(presentYear, presentMonth);  //월의 마지막 일 구하기


        //가입한 년도와 현재 년도가 같으면(2021-2021)
        if (firstYear == presentYear)
        {
            //현재 년도와 올해가 같으면 (2021 - 2021)
            if (presentYear == thisYear)
            {
                MonthDataInit(firstYear, firstMonth - 1, thisMonth); //인덱스가 0부터 시작해서 월보다 1빼준다
                MonthTextSetActionInit(firstMonth, thisMonth);
            }
            else
            {//현재 년도와 올해가 다르면(2020(가입) - 2020(현재) -2021(올해))
                MonthDataInit(firstYear, firstMonth - 1, 12);
                MonthTextSetActionInit(firstMonth, 13); //13인 건 12월까지 전부 보여줘야해서 
            }
        }
        else
        {//가입한 년도와 현재 년도가 다르면(2019 - 2021)
            //현재 년도와 올해 년도가 같으면(2021 - 2021)
            if (presentYear == thisYear)
            {
                MonthDataInit(presentYear, 0, thisMonth);
                MonthTextSetActionInit(1, thisMonth);
            }
            else
            {//현재 년도와 올해 년도가 다르면(2020 - 2021)
                MonthDataInit(presentYear, 0, 12);
                MonthTextSetActionInit(1, 13);
            }
        }

    }

    //월별 데이터 넣는 함수
    void MonthDataInit(int _year, int _startMonth, int _endMonth)
    {
        StartCoroutine(_MonthDataInit(_year, _startMonth, _endMonth));
    }

    // 수정 중
    IEnumerator _MonthDataInit(int _year, int _startMonth, int _endMonth)
    {
        ServerManager.Instance.Get_MonthData(_year, _startMonth, _endMonth);

        yield return new WaitUntil(() => ServerManager.Instance.isGetMonthDataCompleted);

        ServerManager.Instance.isGetMonthDataCompleted = false;

        top_heightSize = 0f;
        string[] arrMonth;

        // 실제 데이터의 최대 높이 구하기
        for (int max = 0; max < ServerManager.Instance.dataByMonth.Count; max++)
        {
            if (top_heightSize < ServerManager.Instance.dataByMonth[max].month_Distance)
            {
                top_heightSize = ServerManager.Instance.dataByMonth[max].month_Distance;
                biggestValue = ServerManager.Instance.dataByMonth[max].month_Distance;
            }
        }

        // 거리 km, m 단위
        top_heightSize += top_heightSize / 10;
        one_third_heightSize = Mathf.Floor(biggestValue / 3);
        two_third_heightSize = Mathf.Floor(one_third_heightSize * 2);
        _one_third_Size.text = one_third_heightSize.ToString() + " Km";
        _two_third_Size.text = two_third_heightSize.ToString() + " Km";

        // 1월에서 4월 이라면 > 0 ~ 3
        // 실제 데이터의 갯수는 4개가 아닐 수 있다. > 월을 맞춰서 그려줘야함.
        for (int i = _startMonth; i < _endMonth; i++)
        {
            for (int j = 0; j < ServerManager.Instance.dataByMonth.Count; j++)
            {

                // arrMonth[0] 년 / arrMonth[1] 월 / arrMonth[2] 일
                arrMonth = ServerManager.Instance.dataByMonth[j].monthly_Date.Split('-');
                if ((i + 1) == int.Parse(arrMonth[1]))
                {
                    dayData_scrip = timeView[i].GetComponent<DayData>();    // index : 1, 2, 3 만 그려짐
                    dayData_scrip.y = ServerManager.Instance.dataByMonth[j].month_Distance;
                    dayData_scrip.posY = Mathf.Lerp(0, 632, Mathf.InverseLerp(0, top_heightSize, ServerManager.Instance.dataByMonth[j].month_Distance));
                    dayData_scrip.LengthExtend();

                }

            }
        }
    }

    //월별 그래프 생성 함수
    public void MonthTextSetActionInit(int _startMonth, int _endMonth)
    {
        for (int i = 0; i < 12; i++)
        {
            timeView[i].SetActive(true);

            //현재 년도가 가입년도와 올해 년도가 같으면(즉 올해 가입했다는 듯)
            //가입한 월 이전은 비활성화
            if (i < _startMonth - 1)
            {
                timeView[i].transform.GetComponent<Button>().interactable = false;  //그래프 클릭 못하게 비활성화
                string dayText = timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>().text;

                //텍스트 색상변경
                Text text = timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>();
                text.color = Color.gray;

                timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = dayText;
                timeView[i].transform.GetChild(1).gameObject.SetActive(false);  //그래프 이미지 비활서화
            }
            else if (i > _endMonth - 1)
            {
                timeView[i].transform.GetComponent<Button>().interactable = false;
                string dayText = timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>().text;

                //텍스트 색상변경
                Text text = timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>();
                text.color = Color.gray;

                timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = dayText;
                timeView[i].transform.GetChild(1).gameObject.SetActive(false);  //그래프 이미지 비활서화
            }
            else
            {
                timeView[i].transform.GetComponent<Button>().interactable = true;
                string dayText = timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>().text;

                //텍스트 색상변경
                Text text = timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>();
                text.color = Color.white;

                timeView[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = dayText;
                timeView[i].transform.GetChild(1).gameObject.SetActive(true);  //그래프 이미지 활서화
            }
        }
        //13부터 31까지 비활성화
        for (int j = 12; j < 31; j++)
        {
            timeView[j].SetActive(false);
        }

        //컨텐츠필드 사이즈 리프레쉬
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)csf.transform);
    }

    //자세한 데이터 정보를 보기 위해 그래프를 클릭했을 때 이벤트 함수
    public void OneTextShowData(string _str, string _int)
    {
        int count = 0;  //그래프가 그려져있는 갯수
        int num = 0;    //인덱스배열에 인덱스 
        int[] index;

        //화면에 보여주는 객체가 몇개인지 확인
        for (int i = 0; i < 31; i++)
        {
            if (timeView[i].activeSelf == true)
            {
                if (timeView[i].transform.GetChild(1).gameObject.activeSelf == true)
                {
                    count++;
                }
            }
        }
        //Debug.Log("count " + count);
        index = new int[count]; //보여지는 객체 수만큼 생성

        //오브젝트가 해당되는 인덱스(번호)값을 저장
        for (int i = 0; i < 31; i++)
        {
            if (timeView[i].activeSelf == true)
            {
                if (timeView[i].transform.GetChild(1).gameObject.activeSelf == true)
                {
                    index[num++] = i;
                }
            }
        }
        //보여지는 객체수만큼 돌려서 클릭했을 때 화면에 하나만 나타나게 함.
        for (int i = 0; i < count; i++)
        {
            dayData_scrip = timeView[index[i]].GetComponent<DayData>();

            string strTemp = Regex.Replace(timeView[index[i]].name, @"\d", "");    //문자추출
            string strInt = Regex.Replace(timeView[index[i]].name, @"\D", ""); //숫자 추출 

            //클릭한 오브젝트가 있는지 확인
            if (dayData_scrip.clickOnState == true)
            {
                //그 중에서 방금 클릭한 오브젝트랑 다른 오브젝트를 찾는다.
                if (strInt != _int)
                {
                    dayData_scrip.clickOnState = false; //클릭안했다고 변경
                    timeView[index[i]].transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);    //데이터정보를 비활성화
                }
            }
        }
    }
}