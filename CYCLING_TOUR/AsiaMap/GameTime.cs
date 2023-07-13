using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    public static GameTime instance { get; private set; }

    public Text timer;
    public Text speedText;
    public float currTime = 0; //현재 시간

    string[] openTime;  //경기 기록 저장


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }



    public void PlayTime()
    {
        StartCoroutine(_PlayTime());
    }


    IEnumerator _PlayTime()
    {
        //PlayerPrefs.SetFloat("ConnectTime", 0);

        //currTime = PlayerPrefs.GetFloat("ConnectTime");
        //Debug.Log("currTime : " + currTime + " gameStartState : " + AsiaMap_UIManager.instance.gameStartState);
        //Debug.Log("gameTimeStop : " + AsiaMap_DataManager.instance.gameTimeStop + " AT_InGameTutorial : " + PlayerPrefs.GetString("AT_InGameTutorial"));
        
        while (currTime >= 0 && AsiaMap_UIManager.instance.gameStartState.Equals(true) && 
            AsiaMap_DataManager.instance.gameTimeStop.Equals(false) && PlayerPrefs.GetString("AT_InGameTutorial").Equals("End"))
        {
            currTime += Time.deltaTime;
            //Debug.Log("currTime " + currTime);
            yield return new WaitForEndOfFrame();
            //string timeStr = currTime.ToString("N2");
            //timeStr = timeStr.Replace(".", ":");
            timer.text = getPasrseTime(currTime);
            speedText.text = AsiaMap_UIManager.instance.MySpeed(currTime);

            //자전거 움직일 때 체인 소리
            if (float.Parse(AsiaMap_UIManager.instance.MySpeed(currTime)) >= 5f && SoundMaixerManager.instance.chainStartState.Equals(false))
                SoundMaixerManager.instance.BicycleChainSoundPlay();
            else if (float.Parse(AsiaMap_UIManager.instance.MySpeed(currTime)) < 5f && SoundMaixerManager.instance.chainStartState.Equals(true))
                SoundMaixerManager.instance.BicycleChainSoundStop();

            AsiaMap_UIManager.instance.SpeedCameraViewChange(); //속도에 따른 카메라 뷰 변경
            AsiaMap_UIManager.instance.PlayDistance();  //플레이 거리 
            AsiaMap_UIManager.instance.PlayKcal(currTime);   //플레이 칼로리 소모
        }
        
        if(AsiaMap_UIManager.instance.gameStartState == false)
        {
            SoundMaixerManager.instance.PeopleCheerSoundPlay(); //끝났을 때 환호
            AsiaMap_UIManager.instance.SpeedCameraViewChange(); //속도에 따른 카메라 뷰 변경
            AsiaMap_UIManager.instance.Parent_Division();   //부모자식 분리
            AsiaMap_UIManager.instance.ParticleSystemOff(); //연기 비활성화
            SoundMaixerManager.instance.BicycleChainSoundStop();    //체인 소리 멈춤

            //Debug.Log("게임 종료");
            if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Normal-1"))
            {
                //Debug.Log("1게임 종료");
                PlayerPrefs.SetString("AT_Asia Normal 1Course", PlayerPrefs.GetString("AT_Wear_BicycleStyleName") + "/" + timer.text);
                ServerManager.Instance.Reg_Asia_Ranking("1", "1", PlayerPrefs.GetString("AT_Wear_BicycleStyleName"), timer.text);
            }   
            else if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Normal-2"))
            {
                //Debug.Log("2게임 종료");
                PlayerPrefs.SetString("AT_Asia Normal 2Course", PlayerPrefs.GetString("AT_Wear_BicycleStyleName") + "/" + timer.text);
                ServerManager.Instance.Reg_Asia_Ranking("1", "2", PlayerPrefs.GetString("AT_Wear_BicycleStyleName"), timer.text);
            }
            else if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Normal-3"))
            {
                //Debug.Log("3게임 종료");
                PlayerPrefs.SetString("AT_Asia Normal 3Course", PlayerPrefs.GetString("AT_Wear_BicycleStyleName") + "/" + timer.text);
                ServerManager.Instance.Reg_Asia_Ranking("1", "3", PlayerPrefs.GetString("AT_Wear_BicycleStyleName"), timer.text);
            }
            else if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Hard-1"))
            {
                PlayerPrefs.SetString("AT_Asia Hard 1Course", PlayerPrefs.GetString("AT_Wear_BicycleStyleName") + "/" + timer.text);
                ServerManager.Instance.Reg_Asia_Ranking("2", "1", PlayerPrefs.GetString("AT_Wear_BicycleStyleName"), timer.text);
            }   
            else if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Hard-2"))
            {
                PlayerPrefs.SetString("AT_Asia Hard 2Course", PlayerPrefs.GetString("AT_Wear_BicycleStyleName") + "/" + timer.text);
                ServerManager.Instance.Reg_Asia_Ranking("2", "2", PlayerPrefs.GetString("AT_Wear_BicycleStyleName"), timer.text);
            }
            else if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Hard-3"))
            {
                PlayerPrefs.SetString("AT_Asia Hard 3Course", PlayerPrefs.GetString("AT_Wear_BicycleStyleName") + "/" + timer.text);
                ServerManager.Instance.Reg_Asia_Ranking("2", "3", PlayerPrefs.GetString("AT_Wear_BicycleStyleName"), timer.text);
            }
                

            AsiaMap_DataManager.instance.FinishKcal();
            //Debug.Log("칼로리 : " + PlayerPrefs.GetFloat("AT_CurrKcal"));

            Invoke("FinishScene", 3f);
        }
    }

    public string getPasrseTime(float _time)
    {
        string t = TimeSpan.FromSeconds(_time).ToString("hh\\:mm\\:ss\\.ff");
        string[] tokens = t.Split(':');
        return tokens[0] + ":" + tokens[1] + ":" + tokens[2];
    }

    //결과 화면 씬 이동
    public void FinishScene()
    {
        //Debug.Log(" -------- " + CourseOpenTimeState(timer.text));
        //Debug.Log(" 11-------- " + PlayerPrefs.GetInt("AT_MapCourseOpenTime"));
        if (CourseOpenTimeState(timer.text) <= PlayerPrefs.GetInt("AT_MapCourseOpenTime"))
        {
            //Debug.Log("여기가 맞는데");
            PlayerPrefs.SetString("AT_FinishState", "Clear");
        }
        else
            PlayerPrefs.SetString("AT_FinishState", "Fail");

        SceneManager.LoadScene("GameFinish");
    }

    public string Clear_FailAnim()
    {
        if (CourseOpenTimeState(timer.text) <= PlayerPrefs.GetInt("AT_MapCourseOpenTime"))
        {
            //Debug.Log("여기가 맞는데");
            PlayerPrefs.SetString("AT_FinishState", "Clear");
        }
        else
            PlayerPrefs.SetString("AT_FinishState", "Fail");

        return PlayerPrefs.GetString("AT_FinishState");
    }

    //코스별 시간 기록 가져오는 함수
    public float CourseOpenTimeState(string _data)
    {
        //Debug.Log(" === " + _data);
        //int courseTime = 0;
        if (_data != "")
        {
            string courseData = _data;    //예 : 2021/12/10:00

            char sp = ':';
            openTime = courseData.Split(sp);   //2021,12,10:00 따로 저장
        }
        //float sec = int.Parse(openTime[0]) * 60 + int.Parse(openTime[1]);
        //string secSte = sec + "." + openTime[2];
        if (openTime[0].Equals("00"))
            openTime[0] = "0";

        //Debug.Log(openTime[0] + " :: " + openTime[1] + " :: " + openTime[2]);

        //Debug.Log(int.Parse(openTime[0]) + " :: " + int.Parse(openTime[1]) + " :: " + float.Parse(openTime[2]));
        float sec = int.Parse(openTime[0]) * 60 * 60 + int.Parse(openTime[1]) * 60  + float.Parse(openTime[2]);
        string secSte = sec.ToString();// + "." + openTime[2];
        sec = float.Parse(secSte);
        
        return sec;
    }


}
