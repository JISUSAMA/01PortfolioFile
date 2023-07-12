using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class LobbyStepGraphDisplay : MonoBehaviour
{
    public static LobbyStepGraphDisplay instance { get; private set; }


    public GameObject[] timeView;
    public string[] toDayArr;   //오늘 날짜 배열

    [SerializeField]
    ContentSizeFitter csf;

    string[] firstLoginArr;  //첫로그인 날짜 배열

    LobbyDayData dayData_scrip;
    int maxDay = 12;    //보여줄 날짜 최소한의 숫자

    public float top_heightSize = 0;
    //public float one_third_heightSize = 0;
    //public float two_third_heightSize = 0;

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
        Invoke("DayInitializaion", 0.01f);
    }

    //일별 초기화
    public void DayInitializaion()
    {
        DataInit();
        GetTodayInitialization();
        DayDataInit();
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

        // 처음 가입한 정보 들고오기(월)
        // 이전 Lobby_UIManager.cs 에서 서버로부터 가져옴 PlayerPrefs.SetString("FirstLoginTime", "2019-11-12");
        string first = PlayerPrefs.GetString("FirstLoginTime");
        char sp = '-';
        firstLoginArr = first.Split(sp); //첫로그인 날짜 분활 ex)2021/04/06
    }

    public void DayDataInit()
    {

        int firstYear = int.Parse(firstLoginArr[0]);    //첫 가입 년도
        int firstMonth = int.Parse(firstLoginArr[1]);   //가입 월
        int firstDay = int.Parse(firstLoginArr[2]); //가입 일

        int thisYear = int.Parse(toDayArr[0]);  //올해 년도
        int thisMonth = int.Parse(toDayArr[1]); //이번 달
        int toDay = int.Parse(toDayArr[2]);   //오늘

        int lastDay = DateTime.DaysInMonth(thisYear, thisMonth);  //월의 마지막 일 구하기

        //가입 년도와 올해 년도가 같으면(2021 - 2021)
        if (firstYear == thisYear)
        {
            //가입한 월과 현재 월이 같으면(4 - 4)
            if (firstMonth == thisMonth)
            {
                //DayDataInit(firstDay - 1, toDay, thisMonth, firstYear);
                DayDataInit(firstYear, thisMonth, toDay, firstDay - 1);
                DayTextSetActionInit(toDay, lastDay, firstDay, firstYear, thisYear, firstMonth, thisMonth);
            }
            else
            {   //가입한 월과 현재 월이 다르면(4 - 5)
                //DayDataInit(0, toDay, thisMonth, firstYear);
                DayDataInit(firstYear, thisMonth, toDay, 0);
                DayTextSetActionInit(toDay, lastDay, firstDay, firstYear, thisYear, firstMonth, thisMonth);
            }
        }
        else
        {   //가입 년도와 올해 년도가 다르면(2020 - 2021)
            //DayDataInit(0, toDay, thisMonth, thisYear);
            DayDataInit(thisYear, thisMonth, toDay, 0);
            DayTextSetActionInit(toDay, lastDay, firstDay, firstYear, thisYear, firstMonth, thisMonth);
        }
    }

    //일별 데이터 집어 넣는 함수
    void DayDataInit(int _thisYear, int _thisMonth, int _today, int _startDay)
    {
        // 서버에서 데이터 들고오기
        StartCoroutine(_DayDataInit(_thisYear, _thisMonth, _today, _startDay));
    }

    IEnumerator _DayDataInit(int _thisYear, int _thisMonth, int _today, int _startDay)
    {
        ServerManager.Instance.Get_DateData(_thisYear, _thisMonth, _today, _startDay);

        yield return new WaitUntil(() => ServerManager.Instance.isGetDateDataCompleted);

        ServerManager.Instance.isGetDateDataCompleted = false;
        top_heightSize = 0f;
        biggestValue = 0f;
        // 날짜를 알기위한 배열
        string[] arrDate;

        // 실제 데이터의 최대 높이 구하기
        for (int max = 0; max < ServerManager.Instance.dataByDate.Count; max++)
        {
            if (top_heightSize < ServerManager.Instance.dataByDate[max].today_StepCount)
            {
                top_heightSize = ServerManager.Instance.dataByDate[max].today_StepCount;
                biggestValue = ServerManager.Instance.dataByDate[max].today_StepCount;
            }
        }

        // 거리 km, m 단위
        top_heightSize += top_heightSize / 10;

        //데이터 넣기
        for (int i = _startDay; i < _today; i++)
        {
            dayData_scrip = timeView[i].GetComponent<LobbyDayData>();

            if (_thisMonth == 1)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = 40f;
                        dayData_scrip.posY = ServerManager.Instance.dataByDate[j].today_StepCount; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_thisMonth == 2)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = 40f;
                        dayData_scrip.posY = ServerManager.Instance.dataByDate[j].today_StepCount; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_thisMonth == 3)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = 40f;
                        dayData_scrip.posY = ServerManager.Instance.dataByDate[j].today_StepCount; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_thisMonth == 4)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = 40f;
                        dayData_scrip.posY = ServerManager.Instance.dataByDate[j].today_StepCount; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_thisMonth == 5)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = 40f;
                        dayData_scrip.posY = ServerManager.Instance.dataByDate[j].today_StepCount; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_thisMonth == 6)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = 40f;
                        dayData_scrip.posY = ServerManager.Instance.dataByDate[j].today_StepCount; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_thisMonth == 7)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = 40f;
                        dayData_scrip.posY = ServerManager.Instance.dataByDate[j].today_StepCount; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_thisMonth == 8)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = 40f;
                        dayData_scrip.posY = ServerManager.Instance.dataByDate[j].today_StepCount; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_thisMonth == 9)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = 40f;
                        dayData_scrip.posY = ServerManager.Instance.dataByDate[j].today_StepCount; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_thisMonth == 10)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = 40f;
                        dayData_scrip.posY = ServerManager.Instance.dataByDate[j].today_StepCount; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_thisMonth == 11)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = 40f;
                        dayData_scrip.posY = ServerManager.Instance.dataByDate[j].today_StepCount; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.LengthExtend();
                    }
                }
            }
            else if (_thisMonth == 12)
            {
                // 실제로 운동한 날만 그려주어야함 > 데이터도 실제 운동한 날만 존재
                for (int j = 0; j < ServerManager.Instance.dataByDate.Count; j++)
                {
                    // 날짜를 배열 형태로 대입
                    arrDate = ServerManager.Instance.dataByDate[j].daily_Date.Split('-');
                    if ((i + 1) == int.Parse(arrDate[2]))
                    {
                        dayData_scrip.y = 40f;
                        dayData_scrip.posY = ServerManager.Instance.dataByDate[j].today_StepCount; //서버에서 값을 들고오세욧1!!!! 일별 시간!! 배열로 들고오기!!! 
                        dayData_scrip.LengthExtend();
                    }
                }
            }
        }
    }

    //일별 그래프 생성해주는 함수
    public void DayTextSetActionInit(int _today, int _lastday, int _firstDay, int _firstYear, int _thisYear, int _firstMonth, int _thisMonth)
    {
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

            //가입한 달이면 - 가입한 일(day) 이전의 데이터를 없애기 위함
            if (_firstYear == _thisYear && _firstMonth == _thisMonth)
            {
                //가입한 날짜 이전
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
                //오늘 날짜 이후 그래프 비활성화
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
            dayData_scrip = timeView[index[i]].GetComponent<LobbyDayData>();

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