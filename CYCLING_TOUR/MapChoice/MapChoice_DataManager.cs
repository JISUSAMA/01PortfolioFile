using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class MapChoice_DataManager : MonoBehaviour
{
 
    public static MapChoice_DataManager instance { get; private set; }

    [Header("[맵 지도]")]
    public GameObject[] courseImg;  //맵 이미지

    [Header("[첫번째 판넬 코스 선택 버튼]")]
    public Image[] asiaNormal_FirstCourseBtn;   //코스선택 버튼
    public Image[] asiaHard_FirstCourseBtn; 

    [Header("[첫번째 판넬 - 모드별 판넬 ]")]
    public GameObject[] asiaMapFirstPanel; //아시아맵 코스 버튼 선택 판넬

    [Header("[두번째 판넬 모드별 토글]")]
    public Toggle[] asiaNormalToggle; //코스선택 노멀 토글
    public Toggle[] asiaHardToggle; //코스선택 하드 토글

    [Header("[두번째 판넬 - 모드별 판넬 ]")]
    public GameObject[] asiaMapTwoPanel; //아시아맵 코스 토글 선택 판넬

    public GameObject[] hardToggle;    //버튼판, 토글판 하드버튼 0:첫페이지 1:두번째 페이지
    public Sprite[] courseBtnSprite;  //잠금 해제 시 이미지

    public GameObject mapOpenPopup; //맵오픈팝업
    public Text mapOpenText;    //오픈 시 텍스트(코스이름)

    public GameObject gameEndPopup; //게임종료팝업

    //각 코스별 잠금 해제 타임
    int normalcourse1_Time = 300;
    int normalcourse2_Time = 300;
    int normalcourse3_Time = 300;
    int hardcourse1_Time = 420;
    int hardcourse2_Time = 420;
    int hardcourse3_Time = 420;

    string[] asia_courseTime;   //아시아 노멀맵 시간
    string[] asiaNor_course1OpenTime;  //아시아 노멀1 오픈 시간
    string[] asiaNor_course2OpenTime;
    string[] asiaNor_course3OpenTime;
    string[] asiaHard_course1OpenTime;  //아시아 하드1 오픈 시간
    string[] asiaHard_course2OpenTime;
    string[] asiaHard_course3OpenTime;

    float normal1, normal2, normal3, hard1, hard2, hard3; //코스기록 저장하는 변수
    int openAmount; //오픈맵 갯수


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        PlayerPrefs.SetString("AT_UseItemCoin", "");   //사용한 아이템 초기화 - 
        PlayerPrefs.SetString("AT_UseItemExp", "");   //사용한 아이템 초기화 - 
        PlayerPrefs.SetString("AT_UseItemSpeed", "");   //사용한 아이템 초기화 - 

        Initialization();
        NewMapCourseOpenPopup();    //새 맵 오픈 팝업
    }


    //아이템 갯수 초기화
    public void ItemInit(Text _coin, Text _exp, Text _speed)
    {
        _coin.text = PlayerPrefs.GetInt("AT_CoinUpAmount").ToString();
        _exp.text = PlayerPrefs.GetInt("AT_ExpUpAmount").ToString();
        _speed.text = PlayerPrefs.GetInt("AT_SpeedUpAmount").ToString();
    }

    public void After_Before_Same_CourseNumber()
    {
        PlayerPrefs.SetInt("AT_BeforeOpenMapAmount", PlayerPrefs.GetInt("AT_OpenMapCourseAmount"));

        //if(mapOpenText.text != "Asia - 제 3코스 - Hard")
            PlayerPrefs.SetString("AT_NewOpenMap", "No");
        //else
        //    PlayerPrefs.SetString("AT_NewOpenMap", "Yes");
        //Debug.Log("맵이름: " + PlayerPrefs.GetString("AT_OpenMap_CourseName"));
        //오픈된 맵 이름 서버에 저장
        ServerManager.Instance.Update_MapProgress();
    }

    //새로운 맵 오픈 시 팝업 띄우기
    void NewMapCourseOpenPopup()
    {
        //Debug.Log("1OpenMapCourseAmount : " + PlayerPrefs.GetInt("AT_OpenMapCourseAmount"));
        //Debug.Log("1BeforeOpenMapAmount : " + PlayerPrefs.GetInt("AT_BeforeOpenMapAmount"));
        //Debug.Log("NewOpenMap " + PlayerPrefs.GetString("AT_NewOpenMap"));
       // Debug.Log("NewOpNewOpenMap_CourseNamberenMap " + PlayerPrefs.GetInt("AT_NewOpenMap_CourseNamber"));


        if (PlayerPrefs.GetString("AT_NewOpenMap").Equals("Yes"))
        {
            if(PlayerPrefs.GetInt("AT_NewOpenMap_CourseNamber").Equals(1))
            {
                mapOpenText.text = "Asia - 제 1코스 - Normal";
                PlayerPrefs.SetString("AT_OpenMap_CourseName", "Normal-1");
            }
            else if (PlayerPrefs.GetInt("AT_NewOpenMap_CourseNamber").Equals(2))
            {
                mapOpenPopup.SetActive(true);
                mapOpenText.text = "Asia - 제 2코스 - Normal";
                PlayerPrefs.SetString("AT_OpenMap_CourseName", "Normal-2");
            }
            else if(PlayerPrefs.GetInt("AT_NewOpenMap_CourseNamber").Equals(3))
            {
                mapOpenPopup.SetActive(true);
                mapOpenText.text = "Asia - 제 3코스 - Normal";
                PlayerPrefs.SetString("AT_OpenMap_CourseName", "Normal-3");
            } 
            else if(PlayerPrefs.GetInt("AT_NewOpenMap_CourseNamber").Equals(4))
            {
                mapOpenPopup.SetActive(true);
                mapOpenText.text = "Asia - 제 1코스 - Hard";
                PlayerPrefs.SetString("AT_OpenMap_CourseName", "Hard-1");
            }  
            else if (PlayerPrefs.GetInt("AT_NewOpenMap_CourseNamber").Equals(5))
            {
                mapOpenPopup.SetActive(true);
                mapOpenText.text = "Asia - 제 2코스 - Hard";
                PlayerPrefs.SetString("AT_OpenMap_CourseName", "Hard-2");
            } 
            else if (PlayerPrefs.GetInt("AT_NewOpenMap_CourseNamber").Equals(6))
            {
                mapOpenPopup.SetActive(true);
                mapOpenText.text = "Asia - 제 3코스 - Hard";
                PlayerPrefs.SetString("AT_OpenMap_CourseName", "Hard-3");
            }
        }

        //Debug.Log("???? 여기 오픈" + PlayerPrefs.GetString("AT_OpenMap_CourseName"));
        if (PlayerPrefs.GetString("AT_OpenMap_CourseName").Equals("Normal-1"))
        {
            //NormalCourseOpen(1);
        }
        else if (PlayerPrefs.GetString("AT_OpenMap_CourseName").Equals("Normal-2"))
        {
            //Debug.Log("???? 여기 오픈");
            NormalCourseOpen(1);
        }
        else if (PlayerPrefs.GetString("AT_OpenMap_CourseName").Equals("Normal-3"))
        {
            NormalCourseOpen(1);
            NormalCourseOpen(2);
        }
        else if (PlayerPrefs.GetString("AT_OpenMap_CourseName").Equals("Hard-1"))
        {
            NormalCourseOpen(1);
            NormalCourseOpen(2);
            hardToggle[0].GetComponent<Toggle>().interactable = true;
            hardToggle[0].transform.GetChild(1).gameObject.SetActive(false);  //하드모드 해제
            hardToggle[1].GetComponent<Toggle>().interactable = true;
            hardToggle[1].transform.GetChild(1).gameObject.SetActive(false);  //하드모드 해제
        }
        else if (PlayerPrefs.GetString("AT_OpenMap_CourseName").Equals("Hard-2"))
        {
            NormalCourseOpen(1);
            NormalCourseOpen(2);
            hardToggle[0].GetComponent<Toggle>().interactable = true;
            hardToggle[0].transform.GetChild(1).gameObject.SetActive(false);  //하드모드 해제
            hardToggle[1].GetComponent<Toggle>().interactable = true;
            hardToggle[1].transform.GetChild(1).gameObject.SetActive(false);  //하드모드 해제
            HardCourseOpen(1);
        }
        else if (PlayerPrefs.GetString("AT_OpenMap_CourseName").Equals("Hard-3"))
        {
            NormalCourseOpen(1);
            NormalCourseOpen(2);
            hardToggle[0].GetComponent<Toggle>().interactable = true;
            hardToggle[0].transform.GetChild(1).gameObject.SetActive(false);  //하드모드 해제
            hardToggle[1].GetComponent<Toggle>().interactable = true;
            hardToggle[1].transform.GetChild(1).gameObject.SetActive(false);  //하드모드 해제
            HardCourseOpen(1);
            HardCourseOpen(2);
        }
    }
    
    void Initialization()
    {
        asia_courseTime = new string[3];

        //Debug.Log("코스1 "+PlayerPrefs.GetString("AT_Asia Normal 1Course"));
        //Debug.Log("코스2 "+PlayerPrefs.GetString("AT_Asia Normal 2Course"));
        //Debug.Log("코스3 " + PlayerPrefs.GetString("AT_Asia Normal 3Course"));
        //Debug.Log("코스4 " + PlayerPrefs.GetString("AT_Asia Hard 1Course"));
        //Debug.Log("코스5 " + PlayerPrefs.GetString("AT_Asia Hard 2Course"));
        //Debug.Log("코스6 " + PlayerPrefs.GetString("AT_Asia Hard 3Course")); 
        //PlayerPrefs.SetString("Asia Normal 1Course", "F123/10:30"); //예>F123/10:30
        //PlayerPrefs.SetString("Asia Normal 2Course", "");
        //PlayerPrefs.SetString("Asia Normal 3Course", "");
        //PlayerPrefs.SetString("Asia Hard 1Course", "");
        //PlayerPrefs.SetString("Asia Hard 2Course", "");
        //PlayerPrefs.SetString("Asia Hard 3Course", "");

        //asia_courseTime[0] = PlayerPrefs.GetString("Asia Normal 1Course");
        if(PlayerPrefs.GetString("AT_Asia Normal 1Course") != "F1234/99:99:99" && PlayerPrefs.GetString("AT_Asia Normal 1Course") != "")
            normal1 = CourseOpenTimeState(0, PlayerPrefs.GetString("AT_Asia Normal 1Course"), asiaNor_course1OpenTime);
        if (PlayerPrefs.GetString("AT_Asia Normal 1Course") != "F1234/99:99:99" && PlayerPrefs.GetString("AT_Asia Normal 2Course") != "")
            normal2 = CourseOpenTimeState(1, PlayerPrefs.GetString("AT_Asia Normal 2Course"), asiaNor_course2OpenTime);
        if (PlayerPrefs.GetString("AT_Asia Normal 1Course") != "F1234/99:99:99" && PlayerPrefs.GetString("AT_Asia Normal 3Course") != "")
            normal3 = CourseOpenTimeState(2, PlayerPrefs.GetString("AT_Asia Normal 3Course"), asiaNor_course3OpenTime);
        if (PlayerPrefs.GetString("AT_Asia Normal 1Course") != "F1234/99:99:99" && PlayerPrefs.GetString("AT_Asia Hard 1Course") != "")
            hard1 = CourseOpenTimeState(0, PlayerPrefs.GetString("AT_Asia Hard 1Course"), asiaHard_course1OpenTime);
        if (PlayerPrefs.GetString("AT_Asia Normal 1Course") != "F1234/99:99:99" && PlayerPrefs.GetString("AT_Asia Hard 2Course") != "")
            hard2 = CourseOpenTimeState(1, PlayerPrefs.GetString("AT_Asia Hard 2Course"), asiaHard_course2OpenTime);
        if (PlayerPrefs.GetString("AT_Asia Normal 1Course") != "F1234/99:99:99" && PlayerPrefs.GetString("AT_Asia Hard 3Course") != "")
            hard3 = CourseOpenTimeState(2, PlayerPrefs.GetString("AT_Asia Hard 3Course"), asiaHard_course3OpenTime);
        //Debug.Log(asia_courseTime[0] + "  :   " + asia_courseOpenStat[0] + " :: " + asia_courseOpenStat[1]);


        if (normal1 <= normalcourse1_Time && normal1 != 0)
            openAmount = 2;

        if (normal2 <= normalcourse2_Time && normal2 != 0)
            openAmount = 3;

        if (normal3 <= normalcourse3_Time && normal3 != 0)
            openAmount = 4;

        if (hard1 <= hardcourse1_Time && hard1 != 0)
            openAmount = 5;

        if (hard2 <= hardcourse2_Time && hard2 != 0)
            openAmount = 6;

        if (hard3 <= hardcourse3_Time && hard3 != 0)
            openAmount = 7;

        PlayerPrefs.SetInt("AT_OpenMapCourseAmount", openAmount);
    }

    void NormalCourseOpen(int _index)
    {
        asiaNormal_FirstCourseBtn[_index].GetComponent<Button>().interactable = true;    //버튼 선택 불가능 해지
        asiaNormal_FirstCourseBtn[_index].sprite = courseBtnSprite[0];
        asiaNormal_FirstCourseBtn[_index].transform.GetChild(3).gameObject.SetActive(false); //잠금 이미지 비활성화
        asiaNormal_FirstCourseBtn[_index].transform.GetChild(2).gameObject.SetActive(true);

        asiaNormalToggle[_index].GetComponent<Toggle>().interactable = true; //토글 선택 불가능 해지
        asiaNormalToggle[_index].transform.GetChild(3).gameObject.SetActive(true);
        asiaNormalToggle[_index].transform.GetChild(4).gameObject.SetActive(false); //잠금 이미지 비활성화
    }
    void HardCourseOpen(int _index)
    {
        asiaHard_FirstCourseBtn[_index].GetComponent<Button>().interactable = true;  //버튼 선택 불가능 해지
        asiaHard_FirstCourseBtn[_index].sprite = courseBtnSprite[1];
        asiaHard_FirstCourseBtn[_index].transform.GetChild(3).gameObject.SetActive(false);   //잠금 이미지 비활성화
        asiaHard_FirstCourseBtn[_index].transform.GetChild(2).gameObject.SetActive(true);

        asiaHardToggle[_index].GetComponent<Toggle>().interactable = true;   //토글 선택 불가능 해지
        asiaHardToggle[_index].transform.GetChild(3).gameObject.SetActive(true);
        asiaHardToggle[_index].transform.GetChild(4).gameObject.SetActive(false);    //잠금 이미지 비활성화
    }




    //코스별 시간 기록 가져오는 함수
    float CourseOpenTimeState(int _index, string _data, string[] _openTime)
    {
        float courseTime = 0;

        if (_data != "")
        {
            string[] openTime;
            asia_courseTime[_index] = _data;    //예 : 2021/12/10:00

            char sp = '/';
            openTime = asia_courseTime[_index].Split(sp);   //2021,12,10:00 따로 저장

            char bar = ':';
            _openTime = openTime[1].Split(bar); //10:00 분해해서 10,00 따로

            courseTime = (int.Parse(_openTime[0]) * 60 * 60) + int.Parse(_openTime[1]) * 60 +  float.Parse(_openTime[2]);
        }
        return courseTime;
    }

    //코스 선택 함수
    public void AsiaMapImageShow(bool _corse1, bool _corse2, bool _corse3)
    {
        courseImg[0].SetActive(_corse1);
        courseImg[1].SetActive(_corse2);
        courseImg[2].SetActive(_corse3);
    }

    //아시아 첫번째 페이지 버튼 클릭 시 두번째 페이지 토글 isOn 함수
    public void AisaNormalChoiceToggleIsOn(bool _asiaNor, bool _asiaHard, bool _corse1, bool _corse2, bool _corse3)
    {
        asiaMapTwoPanel[0].SetActive(_asiaNor);
        asiaMapTwoPanel[1].SetActive(_asiaHard);
        asiaNormalToggle[0].isOn = _corse1;
        asiaNormalToggle[1].isOn = _corse2;
        asiaNormalToggle[2].isOn = _corse3;
    }

    //아시아 첫번째 페이지 버튼 클릭 시 두번째 페이지 토글 isOn 함수
    public void AsiaHardChoiceToggleIsOn(bool _asiaNor, bool _asiaHard, bool _corse1, bool _corse2, bool _corse3)
    {
        asiaMapTwoPanel[0].SetActive(_asiaNor);
        asiaMapTwoPanel[1].SetActive(_asiaHard);
        asiaHardToggle[0].isOn = _corse1;
        asiaHardToggle[1].isOn = _corse2;
        asiaHardToggle[2].isOn = _corse3;
    }

    //두번째 페이지에 있는 아시아코스 선택 버튼 클릭 시 토글 처음 isOn하는 함수
    public void AsiaModeButtonFirstToggleIsOn(bool _normal, bool _hard)
    {
        asiaNormalToggle[0].isOn = _normal;
        asiaHardToggle[0].isOn = _hard;
    }
    
    //아시아맵 첫번째 페이지 모드 판넬 오픈- 노멀 하드
    public void AsiaMapPanelShow(bool _normal, bool _hard)
    {
        asiaMapFirstPanel[0].SetActive(_normal);
        asiaMapFirstPanel[1].SetActive(_hard);
    }
    //아시아맵 두번째 페이지 모드 판넬 오픈 - 노멀 하드
    public void AsiaMapTogglePanelShow(bool _normal, bool _hard)
    {
        asiaMapTwoPanel[0].SetActive(_normal);
        asiaMapTwoPanel[1].SetActive(_hard);
        Initialization();   
    }

    public void HardToggleCheck()
    {
        hardToggle[1].GetComponent<Toggle>().isOn = true;
    }

    public void HardToggleUnCheck()
    {
        hardToggle[1].GetComponent<Toggle>().isOn = false;
    }

    public void AsiaMapCourseChoiceOpenTime()
    {
        if(hardToggle[1].GetComponent<Toggle>().isOn != true)
        {
            //Debug.Log("여기 아닙니까 ?!?!?!??!!?");
            if (asiaNormalToggle[0].isOn == true)
            {
                //Debug.Log("코스1선택");
                PlayerPrefs.SetInt("AT_MapCourseOpenTime", normalcourse1_Time);
                PlayerPrefs.SetString("AT_MapCourseName", "Normal-1");
                PlayerPrefs.SetFloat("AT_MapCourseLength", 1.93f);
            }
            else if (asiaNormalToggle[1].isOn == true)
            {
                //Debug.Log("코스2선택");
                PlayerPrefs.SetInt("AT_MapCourseOpenTime", normalcourse2_Time);
                PlayerPrefs.SetString("AT_MapCourseName", "Normal-2");
                PlayerPrefs.SetFloat("AT_MapCourseLength", 1.92f);
            }
            else if (asiaNormalToggle[2].isOn == true)
            {
                PlayerPrefs.SetInt("AT_MapCourseOpenTime", normalcourse3_Time);
                PlayerPrefs.SetString("AT_MapCourseName", "Normal-3");
                PlayerPrefs.SetFloat("AT_MapCourseLength", 1.98f);
            }
        }
        else
        {
            if (asiaHardToggle[0].isOn == true)
            {
                Debug.Log("하드모드");
                PlayerPrefs.SetInt("AT_MapCourseOpenTime", hardcourse1_Time);
                PlayerPrefs.SetString("AT_MapCourseName", "Hard-1");
                PlayerPrefs.SetFloat("AT_MapCourseLength", 1.93f);
            }
            else if (asiaHardToggle[1].isOn == true)
            {
                PlayerPrefs.SetInt("AT_MapCourseOpenTime", hardcourse2_Time);
                PlayerPrefs.SetString("AT_MapCourseName", "Hard-2");
                PlayerPrefs.SetFloat("AT_MapCourseLength", 1.92f);
            }
            else if (asiaHardToggle[2].isOn == true)
            {
                PlayerPrefs.SetInt("AT_MapCourseOpenTime", hardcourse3_Time);
                PlayerPrefs.SetString("AT_MapCourseName", "Hard-3");
                PlayerPrefs.SetFloat("AT_MapCourseLength", 1.98f);
            }
        }
    }


    public void ConnectButtonOn()
    {
        GameObject sensor = GameObject.Find("SensorManager");
        ArduinoHM10Test2 sensor_Script = sensor.GetComponent<ArduinoHM10Test2>();

        sensor_Script.StartProcess();
    }

    //게임 종료
    public void GameEndButtonOn()
    {
        Application.Quit(); //게임 종료
    }

    private void Update()
    {
        if (Application.platform.Equals(RuntimePlatform.Android))
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                gameEndPopup.SetActive(true);
            }
        }
    }

    private void OnDestroy()
    {
        Resources.UnloadUnusedAssets();
    }
}
