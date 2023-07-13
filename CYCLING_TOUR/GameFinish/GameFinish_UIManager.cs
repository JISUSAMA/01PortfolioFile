using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFinish_UIManager : MonoBehaviour
{
    public GameObject[] openScene;

    public Text[] timeText;
    public Text[] courseNameText;
    public Text[] kmText;
    public Text[] kcalText;
    public Text[] questText;
    public Image[] questState;  //퀘스트 성공여부
    public Sprite questFinish;

    public GameObject coinPopup;    //코인보물상자

    //------보상확인 팝업---------
    public Text levelText;
    public Slider levelSlider;
    public Text currExpText;
    public Text totalExpText;
    public Text getExpText;
    public Text getCoinText;

    //센서 쪽 변수
    GameObject itemDataBase;
    GameObject sensorManager;
    Transform woman_player;
    WomanCtrl womanctrl_scrip;
    Transform man_player;
    ManCtrl manctrl_scrip;


    void Start()
    {
        PlayBGM();  //BGM시작
        SoundMaixerManager.instance.PeopleCheerSoundStop(); //사람들 환호소리 멈춤

        //Debug.Log("최고속도 : " + PlayerPrefs.GetFloat("AT_Record_MaxSpeed"));
        //PlayerPrefs.GetFloat("TodayMaxSpeed");  //최고속도 갱신
        PlayerInit();
        GameFinish_DataManager.instance.RePlay_PlayEnd(false);  //다시하기, 완주종료 버튼 클릭못하게

        //Debug.Log("NewOpenMap_CourseNamber "+ PlayerPrefs.GetInt("AT_NewOpenMap_CourseNamber"));
        
        if (PlayerPrefs.GetString("AT_FinishState") == "Clear")
        {
            //오픈된 맵인지 아닌지 확인하는 프리팹
            if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Normal-1") &&  PlayerPrefs.GetString("AT_OpenMap_CourseName").Equals("Normal-1") && PlayerPrefs.GetInt("AT_NewOpenMap_CourseNamber") < 2)
            {
                PlayerPrefs.SetString("AT_NewOpenMap", "Yes");
                PlayerPrefs.SetInt("AT_NewOpenMap_CourseNamber", 2);
                PlayerPrefs.SetString("AT_OpenMap_CourseName", "Normal-2");
            }
            else if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Normal-2") && PlayerPrefs.GetString("AT_OpenMap_CourseName").Equals("Normal-2") && PlayerPrefs.GetInt("AT_NewOpenMap_CourseNamber") < 3)
            {
                PlayerPrefs.SetString("AT_NewOpenMap", "Yes");
                PlayerPrefs.SetInt("AT_NewOpenMap_CourseNamber", 3);
                PlayerPrefs.SetString("AT_OpenMap_CourseName", "Normal-3");
            }
            else if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Normal-3") &&  PlayerPrefs.GetString("AT_OpenMap_CourseName").Equals("Normal-3") && PlayerPrefs.GetInt("AT_NewOpenMap_CourseNamber") < 4)
            {
                PlayerPrefs.SetString("AT_NewOpenMap", "Yes");
                PlayerPrefs.SetInt("AT_NewOpenMap_CourseNamber", 4);
                PlayerPrefs.SetString("AT_OpenMap_CourseName", "Hard-1");
            }
            else if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Hard-1") && PlayerPrefs.GetString("AT_OpenMap_CourseName").Equals("Hard-1") && PlayerPrefs.GetInt("AT_NewOpenMap_CourseNamber") < 5)
            {
                PlayerPrefs.SetString("AT_NewOpenMap", "Yes");
                PlayerPrefs.SetInt("AT_NewOpenMap_CourseNamber", 5);
                PlayerPrefs.SetString("AT_OpenMap_CourseName", "Hard-2");
            }
            else if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Hard-2") && PlayerPrefs.GetString("AT_OpenMap_CourseName").Equals("Hard-2") && PlayerPrefs.GetInt("AT_NewOpenMap_CourseNamber") < 6)
            {
                PlayerPrefs.SetString("AT_NewOpenMap", "Yes");
                PlayerPrefs.SetInt("AT_NewOpenMap_CourseNamber", 6);
                PlayerPrefs.SetString("AT_OpenMap_CourseName", "Hard-3");
            }
            else if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Hard-3") && PlayerPrefs.GetString("AT_OpenMap_CourseName").Equals("Hard-3") && PlayerPrefs.GetInt("AT_NewOpenMap_CourseNamber") < 7)
            {
                PlayerPrefs.SetString("AT_NewOpenMap", "No");
                PlayerPrefs.SetInt("AT_NewOpenMap_CourseNamber", 7);
            }


            //Debug.Log("--클릭어");
            openScene[0].SetActive(true);
            openScene[1].SetActive(false);

            courseNameText[0].text = PlayerPrefs.GetString("AT_MapCourseName");
            kmText[0].text = PlayerPrefs.GetFloat("AT_MapCourseLength").ToString() + "km";
            kcalText[0].text = PlayerPrefs.GetFloat("AT_CurrKcal").ToString("N0") + "kcal 소모";

            float kcal = PlayerPrefs.GetFloat("AT_Record_TodayKcal") + PlayerPrefs.GetFloat("AT_CurrKcal");
            PlayerPrefs.SetFloat("AT_Record_TodayKcal", (int)kcal);
            float km = PlayerPrefs.GetFloat("AT_Record_TodayKm") + PlayerPrefs.GetFloat("AT_MapCourseLength");
            PlayerPrefs.SetFloat("AT_Record_TodayKm", km);

            //Debug.Log("Record_TodayKm  :::: " + PlayerPrefs.GetFloat("AT_Record_TodayKm"));

            SceneText(0);
            GameFinish_DataManager.instance.TodayMapQuestKcalBurnUp();  //칼로리 퀘스트
            GameFinish_DataManager.instance.TodayMapQuestMaxSpeed();    //최고속도 퀘스트
            GameFinish_DataManager.instance.CourseTimeLimitQuest(); //시간제한 퀘스트

            Invoke("CoinBoxPopupShow", 2f);
        }
        else if(PlayerPrefs.GetString("AT_FinishState") == "Fail")
        {
            //Debug.Log("--놉클릭어");
            openScene[1].SetActive(true);
            openScene[0].SetActive(false);

            courseNameText[1].text = PlayerPrefs.GetString("AT_MapCourseName");
            kmText[1].text = PlayerPrefs.GetFloat("AT_MapCourseLength").ToString() + "km";
            kcalText[1].text = PlayerPrefs.GetFloat("AT_CurrKcal").ToString("N0") + "kcal 소모";

            //Debug.Log("칼로리 클릭 : " + PlayerPrefs.GetFloat("AT_Record_TodayKcal"));

            float kcal = PlayerPrefs.GetFloat("AT_Record_TodayKcal") + PlayerPrefs.GetFloat("AT_CurrKcal");
            PlayerPrefs.SetFloat("AT_Record_TodayKcal", (int)kcal);
            float km = PlayerPrefs.GetFloat("AT_Record_TodayKm") + PlayerPrefs.GetFloat("AT_MapCourseLength");
            PlayerPrefs.SetFloat("AT_Record_TodayKm", km);

            SceneText(1);
            GameFinish_DataManager.instance.TodayMapQuestKcalBurnUp();  //칼로리 퀘스트
            GameFinish_DataManager.instance.TodayMapQuestMaxSpeed();    //최고속도 퀘스트
            GameFinish_DataManager.instance.CourseTimeLimitQuest(); //시간제한 퀘스트

            Invoke("CoinBoxPopupShow", 2f);
        }
    }

    //BGM시작
    void PlayBGM()
    {
        SoundMaixerManager.instance.OutGameBGMPlay();
    }

    //플레이어 초기화
    public void PlayerInit()
    {
        //내 정보 - 모델링 위치 초기화
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            woman_player = GameObject.Find("Woman").GetComponent<Transform>();
            womanctrl_scrip = woman_player.GetComponent<WomanCtrl>();

            //내 정보 - 모델링 위치 초기화
            woman_player.localPosition = new Vector3(2.5f, 7.8f, 5.8f);
            woman_player.localRotation = Quaternion.Euler(0f, 240f, 0f);
            woman_player.localScale = new Vector3(2.3f, 2.3f, 2.3f);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            man_player = GameObject.Find("Man").GetComponent<Transform>();
            manctrl_scrip = man_player.GetComponent<ManCtrl>();

            //내 정보 - 모델링 위치 초기화
            man_player.localPosition = new Vector3(2.5f, 7.8f, 5.8f);
            man_player.localRotation = Quaternion.Euler(0f, 240f, 0f);
            man_player.localScale = new Vector3(2.3f, 2.3f, 2.3f);
        }

        itemDataBase = GameObject.Find("DataManager");
        sensorManager = GameObject.Find("SensorManager");
    }

    //해당 씬에 들어가는 텍스트 작성(성공 실패)
    void SceneText(int _index)
    {
        StartCoroutine(_SceneText(_index));
    }

    IEnumerator _SceneText(int _index)
    {
        // 오늘의 주행거리 / 총 주행거리
        // 오늘의 칼로리
        // 최대속도 저장
        ServerManager.Instance.Update_UserRecordInfo(PlayerPrefs.GetFloat("AT_Record_MaxSpeed"), PlayerPrefs.GetFloat("AT_CurrKcal"), PlayerPrefs.GetFloat("AT_TodayMaxSpeed"), PlayerPrefs.GetFloat("AT_MapCourseLength"));
        yield return new WaitUntil(() => ServerManager.Instance.isUpdatedUserRecord);
        ServerManager.Instance.isUpdatedUserRecord = false;

        if (PlayerPrefs.GetString("AT_MapCourseName") == "Normal-1")
        {
            timeText[_index].text = GameFinish_DataManager.instance.CourseOpenTimeState(PlayerPrefs.GetString("AT_Asia Normal 1Course"));
            questText[_index].text = "Normal-1코스" + GameFinish_DataManager.instance.NextCourseOpenTimeMission() + ":00 완주";

            // timeText[_index].text, PlayerPrefs.GetString("Asia Normal 1Course") 전달 후 업데이트
            ServerManager.Instance.Update_RecordByAsiaCourse("Normal-1", PlayerPrefs.GetString("AT_Asia Normal 1Course"), timeText[_index].text);
        }
        else if (PlayerPrefs.GetString("AT_MapCourseName") == "Normal-2")
        {
            timeText[_index].text = GameFinish_DataManager.instance.CourseOpenTimeState(PlayerPrefs.GetString("AT_Asia Normal 2Course"));
            questText[_index].text = "Normal-2코스" + GameFinish_DataManager.instance.NextCourseOpenTimeMission() + ":00 완주";
            ServerManager.Instance.Update_RecordByAsiaCourse("Normal-2", PlayerPrefs.GetString("AT_Asia Normal 2Course"), timeText[_index].text);
        }
        else if (PlayerPrefs.GetString("AT_MapCourseName") == "Normal-3")
        {
            timeText[_index].text = GameFinish_DataManager.instance.CourseOpenTimeState(PlayerPrefs.GetString("AT_Asia Normal 3Course"));
            questText[_index].text = "Normal-3코스" + GameFinish_DataManager.instance.NextCourseOpenTimeMission() + ":00 완주";
            ServerManager.Instance.Update_RecordByAsiaCourse("Normal-3", PlayerPrefs.GetString("AT_Asia Normal 3Course"), timeText[_index].text);
        }
        else if (PlayerPrefs.GetString("AT_MapCourseName") == "Hard-1")
        {
            timeText[_index].text = GameFinish_DataManager.instance.CourseOpenTimeState(PlayerPrefs.GetString("AT_Asia Hard 1Course"));
            questText[_index].text = "Hard-1코스" + GameFinish_DataManager.instance.NextCourseOpenTimeMission() + ":00 완주";
            ServerManager.Instance.Update_RecordByAsiaCourse("Hard-1", PlayerPrefs.GetString("AT_Asia Hard 1Course"), timeText[_index].text);
        }
        else if (PlayerPrefs.GetString("AT_MapCourseName") == "Hard-2")
        {
            timeText[_index].text = GameFinish_DataManager.instance.CourseOpenTimeState(PlayerPrefs.GetString("AT_Asia Hard 2Course"));
            questText[_index].text = "Hard-2코스" + GameFinish_DataManager.instance.NextCourseOpenTimeMission() + ":00 완주";
            ServerManager.Instance.Update_RecordByAsiaCourse("Hard-2", PlayerPrefs.GetString("AT_Asia Hard 2Course"), timeText[_index].text);
        }
        else if (PlayerPrefs.GetString("AT_MapCourseName") == "Hard-3")
        {
            timeText[_index].text = GameFinish_DataManager.instance.CourseOpenTimeState(PlayerPrefs.GetString("AT_Asia Hard 3Course"));
            questText[_index].text = "Hard-3코스" + GameFinish_DataManager.instance.NextCourseOpenTimeMission() + ":00 완주";
            ServerManager.Instance.Update_RecordByAsiaCourse("Hard-3", PlayerPrefs.GetString("AT_Asia Hard 3Course"), timeText[_index].text);
        }
    }

    //코인 팝업 활성화/비활성화
    void CoinBoxPopupShow()
    {
        coinPopup.SetActive(true);

        //내 정보 - 모델링 위치 변경
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            woman_player = GameObject.Find("Woman").GetComponent<Transform>();
            womanctrl_scrip = woman_player.GetComponent<WomanCtrl>();

            womanctrl_scrip.AnimClear_Fail(1f, false, false, false, false, PlayerPrefs.GetInt("AT_HairNumber"), PlayerPrefs.GetInt("AT_BodyNumber"),
                 PlayerPrefs.GetInt("AT_JacketNumber"), PlayerPrefs.GetInt("AT_PantsNumber"), PlayerPrefs.GetInt("AT_ShoesNumber"));

            //내 정보 - 모델링 위치 초기화
            woman_player.localPosition = new Vector3(17.18f, 7.8f, 3.37f);
            woman_player.localRotation = Quaternion.Euler(0f, 240f, 0f);
            woman_player.localScale = new Vector3(2.3f, 2.3f, 2.3f);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            man_player = GameObject.Find("Man").GetComponent<Transform>();
            manctrl_scrip = man_player.GetComponent<ManCtrl>();

            manctrl_scrip.AnimClear_Fail(1f, false, false, false, false, PlayerPrefs.GetInt("AT_HairNumber"), PlayerPrefs.GetInt("AT_BodyNumber"),
                 PlayerPrefs.GetInt("AT_JacketNumber"), PlayerPrefs.GetInt("AT_PantsNumber"), PlayerPrefs.GetInt("AT_ShoesNumber"));

            //내 정보 - 모델링 위치 초기화
            man_player.localPosition = new Vector3(17.18f, 7.8f, 3.37f);
            man_player.localRotation = Quaternion.Euler(0f, 240f, 0f);
            man_player.localScale = new Vector3(2.3f, 2.3f, 2.3f);
        }
    }

    //최종 경험치 더하고, 레벨업함.
    public void Level_Exp_Up()
    {
        //아이템 coin 사용했을 시 이미지 표시 함수
        GameFinish_DataManager.instance.ItemUseCoinImageShow();
        //아이템 exp 사용했을 시 이미지 표시 함수
        GameFinish_DataManager.instance.ItemUseExpImageShow();

        int level = PlayerPrefs.GetInt("AT_Player_Level");


        int takeExp = GameFinish_DataManager.instance.RandomExp();
        int currExp = PlayerPrefs.GetInt("AT_Player_CurrExp");
        int maxExp = level * (level + 1) * 25;
        int remainExp = Mathf.Abs(maxExp - currExp);    //남은 경험치

        //Debug.Log("currExp " + currExp + " 경험치 " + takeExp + " level " + level);
        if (PlayerPrefs.GetString("AT_UseItemExp").Equals("Exp"))
            takeExp = takeExp * 2;

        getExpText.text = "+" + takeExp;

        if (PlayerPrefs.GetString("AT_UseItemCoin").Equals("Coin"))
            getCoinText.text = "+" + GameFinish_DataManager.instance.reward * 2;
        else
            getCoinText.text = "+" + GameFinish_DataManager.instance.reward;

        //획득한 경험치가 있을때
        while (takeExp != 0)
        {
            //획득한 경험치가 남은 경험치보다 작은 경우
            if (takeExp < remainExp)
            {
                maxExp = (level * (level + 1) * 25);
                level = level;  //레벨 그대로
                currExp += takeExp; //현재경험치 + 획득한 경험치
                remainExp -= takeExp;   //남은경험치 - 획득한 경험치
                takeExp = 0;    //획득한 경험치 초기화
            }
            //획득한 경험치와 남은 경험치가 동일한 경우
            else if (takeExp == remainExp)
            {
                level += 1; //레벨 1상승
                maxExp = (level * (level + 1) * 25);
                currExp = 0; //현재 경험치 0(레벨이 올라서)
                remainExp = 0;  //남은 경험치 0(레벨이 올라서)
                takeExp = 0;    //획득한 경험치 초기화
            }
            //획득한 경험치가 남은 경험치보다 큰 경우
            else if (takeExp > remainExp)
            {
                level += 1; //레벨 1상승
                takeExp -= remainExp;   //획득한 경험치 - 남은 경험치(레벨업 되기 전 남아있는)
                currExp = 0;    //레벨업이 되어서 현재 경험치는 0
                maxExp = level * (level + 1) * 25;
                remainExp = maxExp; //레벨업이 되어서 남은 경험치도 풀 초기화
            }
        }

        PlayerPrefs.SetInt("AT_Player_Level", level);
        PlayerPrefs.SetInt("AT_Player_TakeExp", takeExp);
        PlayerPrefs.SetInt("AT_Player_CurrExp", currExp);

        // 서버에 저장
        ServerManager.Instance.Update_Level_Data(level, currExp, takeExp);
        ServerManager.Instance.Update_Gold_Data(PlayerPrefs.GetInt("AT_Player_Gold"));// + int.Parse(getCoinText.text));


        //Debug.Log("level " + level + " currExp " + currExp + " totalExpText " + (level * (level + 1) * 25).ToString());
        levelText.text = level.ToString();
        levelSlider.value = (float)currExp / (float)(level * (level + 1) * 25);
        currExpText.text = currExp.ToString();
        totalExpText.text = (level * (level + 1) * 25).ToString() + "EXP";
    }

}
