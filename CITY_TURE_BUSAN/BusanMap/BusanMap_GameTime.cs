using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BusanMap_GameTime : MonoBehaviour
{
    private BusanMapStringClass stringClass;
    public static BusanMap_GameTime instance { get; private set; }


    public Text timer;
    public Text speedText;
    public float currTime = 0; //현재시간

    string[] openTime;    //경기 기록 저장

    public bool chainBrakeState;    //브레이크 시작 여부
    

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
        
        stringClass = new BusanMapStringClass(); 
    }


    void Start()
    {
        
    }

    public void PlayTime()
    {
        StartCoroutine(_PlayTime());
    }

    IEnumerator _PlayTime()
    {
        //Debug.Log(" ---- ??? 왜쥬 ???" + Busan_UIManager.instance.gameStartState + " : " + Busan_DataManager.instance.gameTimeStop + " : " + PlayerPrefs.GetString("Busan_InGameTutorial"));

        //게임 진행중
        while (currTime >= 0 && Busan_UIManager.instance.gameStartState.Equals(true) &&
            Busan_DataManager.instance.gameTimeStop.Equals(false) &&  PlayerPrefs.GetString("Busan_InGameTutorial").Equals("End"))
        {
            currTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();

            timer.text = GetParseTime(currTime);
            speedText.text = Busan_UIManager.instance.MySpeed(currTime);

            //자전거 움직일 때 체인 소리
            if (float.Parse(Busan_UIManager.instance.MySpeed(currTime)) >= 5f && SoundMaixerManager.instance.chainStartState.Equals(false))
                SoundMaixerManager.instance.BicycleChainSoundPlay();
            else if (float.Parse(Busan_UIManager.instance.MySpeed(currTime)) < 5f && SoundMaixerManager.instance.chainStartState.Equals(true))
            {
                SoundMaixerManager.instance.BicycleChainSoundStop();
                //chainBrakeState = true;
            }

            //if (chainBrakeState.Equals(true))// && SoundMaixerManager.instance.chainStartState.Equals(false))
            //{
            //    chainBrakeState = false;
            //    SoundMaixerManager.instance.BicycleChainBrakeSoundPlay();
            //}
                

            Busan_UIManager.instance.PlayDistance();    //플레이 거리
            Busan_UIManager.instance.PlayKcal(currTime);    //플레이 칼로리 소모
        }

        //게임 끝났을 때
        if (Busan_UIManager.instance.gameStartState.Equals(false))
        {
            SoundMaixerManager.instance.PeopleCheerSoundPlay(); //끝났을 때 환호
            Busan_UIManager.instance.Parent_Division(); //부모자식 분리
            Busan_UIManager.instance.ParticleSystemOff();   //연기 비활성화
            SoundMaixerManager.instance.BicycleChainSoundStop();    //체인 소리 멈춤
            Debug.Log("FINISH!!!!!!!!!!!!!!!!!!!!");
            if (PlayerPrefs.GetString("Busan_CurrentMap_Name").Equals(stringClass.BUSAN_RED_LINE))
            {
                //부산 낮모드 노멀
                if (PlayerPrefs.GetString("Busan_MapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_NOR1))
                {
                    PlayerPrefs.SetString("Busan_Asia Normal 1Course", PlayerPrefs.GetString("Busan_Wear_BicycleStyleName") + "/" + timer.text);
                    ServerManager.Instance.Reg_Asia_Ranking("1", "1", PlayerPrefs.GetString("Busan_Wear_BicycleStyleName"), timer.text);
                }
                //부산 밤모드 노멀
                else if (PlayerPrefs.GetString("Busan_MapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_NOR2))
                {
                    PlayerPrefs.SetString("Busan_Asia Normal 2Course", PlayerPrefs.GetString("Busan_Wear_BicycleStyleName") + "/" + timer.text);
                    ServerManager.Instance.Reg_Asia_Ranking("1", "2", PlayerPrefs.GetString("Busan_Wear_BicycleStyleName"), timer.text);
                }
                //부산 낮모드 하드
                else if (PlayerPrefs.GetString("Busan_MapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_HARD1))
                {
                    PlayerPrefs.SetString("Busan_Asia Hard 1Course", PlayerPrefs.GetString("Busan_Wear_BicycleStyleName") + "/" + timer.text);
                    ServerManager.Instance.Reg_Asia_Ranking("2", "1", PlayerPrefs.GetString("Busan_Wear_BicycleStyleName"), timer.text);
                }
                //부산 밤모드 하드
                else if (PlayerPrefs.GetString("Busan_MapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_HARD2))
                {
                    PlayerPrefs.SetString("Busan_Asia Hard 2Course", PlayerPrefs.GetString("Busan_Wear_BicycleStyleName") + "/" + timer.text);
                    ServerManager.Instance.Reg_Asia_Ranking("2", "2", PlayerPrefs.GetString("Busan_Wear_BicycleStyleName"), timer.text);
                }
            } 
            else if (PlayerPrefs.GetString("Busan_CurrentMap_Name").Equals(stringClass.BUSAN_GREEN_LINE))
            {
                //그린라인 낮모드 노멀
                if (PlayerPrefs.GetString("Busan_GreenMapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_NOR1))
                {
                    PlayerPrefs.SetString("Busan_Green Normal 1Course", PlayerPrefs.GetString("Busan_Wear_BicycleStyleName") + "/" + timer.text);
                    ServerManager.Instance.Reg_Asia_Ranking("1", "1", PlayerPrefs.GetString("Busan_Wear_BicycleStyleName"), timer.text);
                }
                //그린 밤모드 노멀
                else if (PlayerPrefs.GetString("Busan_GreenMapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_NOR2))
                {
                    PlayerPrefs.SetString("Busan_Green Normal 2Course", PlayerPrefs.GetString("Busan_Wear_BicycleStyleName") + "/" + timer.text);
                    ServerManager.Instance.Reg_Asia_Ranking("1", "2", PlayerPrefs.GetString("Busan_Wear_BicycleStyleName"), timer.text);
                }
                //그린 낮모드 하드
                else if (PlayerPrefs.GetString("Busan_GreenMapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_HARD1))
                {
                    PlayerPrefs.SetString("Busan_Green Hard 1Course", PlayerPrefs.GetString("Busan_Wear_BicycleStyleName") + "/" + timer.text);
                    ServerManager.Instance.Reg_Asia_Ranking("2", "1", PlayerPrefs.GetString("Busan_Wear_BicycleStyleName"), timer.text);
                }
                //그린 밤모드 하드
                else if (PlayerPrefs.GetString("Busan_GreenMapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_HARD2))
                {
                    PlayerPrefs.SetString("Busan_Green Hard 2Course", PlayerPrefs.GetString("Busan_Wear_BicycleStyleName") + "/" + timer.text);
                    ServerManager.Instance.Reg_Asia_Ranking("2", "2", PlayerPrefs.GetString("Busan_Wear_BicycleStyleName"), timer.text);
                }
            }
            Busan_DataManager.instance.FinishKcal();    //끝났을때 칼로리 저장
                Invoke("FinishScene", 3f);  //3초 후 결과화면으로 이동
      
        }
    }

    public string GetParseTime(float _time)
    {
        string t = TimeSpan.FromSeconds(_time).ToString("hh\\:mm\\:ss\\.ff");
        string[] tokens = t.Split(':');
        return tokens[0] + ":" + tokens[1] + ":" + tokens[2];
    }


    //결과 화면 씬 이동
    public void FinishScene()
    {
        Debug.Log("Busan_MapCourseOpenTime :" +PlayerPrefs.GetInt("Busan_MapCourseOpenTime"));
        if (CourseOpenTimeState(timer.text) <= PlayerPrefs.GetInt("Busan_MapCourseOpenTime"))
        {
            PlayerPrefs.SetString("Busan_FinishState", "Clear");
        }
        else
            PlayerPrefs.SetString("Busan_FinishState", "Fail");

        SceneManager.LoadScene("GameFinish");
    }

    public string Clear_FailAnim()
    {
        if (CourseOpenTimeState(timer.text) <= PlayerPrefs.GetInt("Busan_MapCourseOpenTime"))
        {
     //       Debug.Log("여기가 맞는데");
            PlayerPrefs.SetString("Busan_FinishState", "Clear");
        }
        else
            PlayerPrefs.SetString("Busan_FinishState", "Fail");

        return PlayerPrefs.GetString("Busan_FinishState");
    }

    //코스별 시간 기록 가져오는 함수
    public float CourseOpenTimeState(string _data)
    {
        if(_data != "")
        {
            string courseData = _data; //예: 2021/10/12:00
            char sp = ':';
            openTime = courseData.Split(sp);    //2021, 10, 12:00
        }

        if (openTime[0].Equals("00"))
            openTime[0] = "0";

        float sec = int.Parse(openTime[0]) * 60 * 60 + int.Parse(openTime[1]) * 60 + float.Parse(openTime[2]);
        string secSte = sec.ToString();
        sec = float.Parse(secSte);

        return sec;
    }

}
