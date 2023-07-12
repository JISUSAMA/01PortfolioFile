using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Chapter12_Manager : MonoBehaviour
{
    public static Chapter12_Manager instance { get; private set; }
    public GameObject portal_Gate;
    public GameObject[] EventTracks;

    public bool RunToStart = false;
    public GameObject mission_570_575;//답답함 떨치기 
    private float _event_Init_Position_moonDis;
    private float _event_Init_Position_stationDis;

    public float Chapter12_Progress;
    public int Chapter12_pos_int = 0;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }
    private void Start()
    {
        RunnerPlayer1.instance.cinemachineSmoothPath = GameObject.Find("PlayerTrack").GetComponent<CinemachineSmoothPath>();
        portal_Gate.SetActive(false);
        StartCoroutine(_Chapter12_Progress());
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        Debug.Log(col.name);
        if (col.tag.Equals("Narration"))
        {
            int num = int.Parse(col.name);
            Game_TypeWriterEffect.instance.Show_EventStoryText(num);
            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        //실타래 이벤트
        else if (col.name.Equals("Thread"))
        {
            Game_TypeWriterEffect.instance.Show_EventStoryText(2);
            RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("Thread");
            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        if (Chapter12_pos_int.Equals(6))
        {

        }
        //575-580 답답함 떨치기 이벤트 구현
        if (col.name.Equals("RunStart")) { StartCoroutine(_StartRunEvent()); }
        if (col.name.Equals("RunEnd")) { RunToStart = false; SoundFunction.Instance.Mission_End_Sound(); }

        if (col.name.Equals("GoalPos"))
        {
            if (RunnerPlayer1.instance.playerPosition > RunnerPlayer1.instance.cinemachineSmoothPath.PathLength)
            {
                portal_Gate.SetActive(true);
            }
        }
    }
    //5키로 마다 나오는 이벤트 구분
    IEnumerator _Chapter12_Progress()
    {
        Chapter12_Progress = 1000 - Game_DataManager.instance.moonDis;

        //현재 지점에 따른 이벤트 구간
        if (Chapter12_Progress >= 550 && Chapter12_Progress < 555) Chapter12_pos_int = 1;
        else if (Chapter12_Progress >= 555 && Chapter12_Progress < 560) Chapter12_pos_int = 2;
        else if (Chapter12_Progress >= 560 && Chapter12_Progress < 565) Chapter12_pos_int = 3;
        else if (Chapter12_Progress >= 565 && Chapter12_Progress < 570) Chapter12_pos_int = 4;
        else if (Chapter12_Progress >= 570 && Chapter12_Progress < 575) Chapter12_pos_int = 5;//답답함 떨치기 이벤트 구현 필요
        else if (Chapter12_Progress >= 575 && Chapter12_Progress < 580) Chapter12_pos_int = 6; 
        else if (Chapter12_Progress >= 580 && Chapter12_Progress < 585) Chapter12_pos_int = 7;
        else if (Chapter12_Progress >= 585 && Chapter12_Progress < 590) Chapter12_pos_int = 8;
        else if (Chapter12_Progress >= 590 && Chapter12_Progress < 595) Chapter12_pos_int = 9;
        else if (Chapter12_Progress >= 595 && Chapter12_Progress < 600) Chapter12_pos_int = 10;

        EventTracks[Chapter12_pos_int - 1].SetActive(true);
        StartCoroutine(_Chapter12_event()); //맵 구간에 따른 이벤트 
        yield return null;
    }
    IEnumerator _Chapter12_event()
    {
        yield return null;
    }
    
    //답답함 떨치기 이벤트
    IEnumerator _StartRunEvent()
    {
        RunToStart = false;
        RunnerPlayer1.instance.StoryEventing = true;
        Game_TypeWriterEffect.instance.Show_EventStoryText(7);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        yield return new WaitForSeconds(1f);
        mission_570_575.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3f);
        mission_570_575.SetActive(false);
        RunnerPlayer1.instance.StoryEventing = false;

        float dontMoveSpeed = 10;
        float lefttime = 2f;
        // 초기화 위치 저장
        _event_Init_Position_moonDis = Game_DataManager.instance.moonDis + 0.01f;
        _event_Init_Position_stationDis = Game_DataManager.instance.spaceStationDis + 0.01f;

        RunToStart = true;

        while (RunToStart.Equals(true))
        {
            if (RunnerPlayer1.instance.input_Speed == 0 || SpeedMeter.instance.velocity_f < 12)
            {
                dontMoveSpeed -= Time.deltaTime;
                lefttime -= Time.deltaTime;
                //5초 이상 움직이지 않거나 달리지 않았을 경우,
                if (dontMoveSpeed < 0)
                {
                    //다시 시작
                    RunToStart = false;

                    RunnerPlayer1.instance.StoryEventing = true;
                    RunnerPlayer1.instance.anim.SetBool("Death", true);
                    yield return new WaitForSeconds(4f);
                    Game_UIManager.instance.Fade_in(); //눈감고 뜨기
                    yield return new WaitForSeconds(1f);

                    RunnerPlayer1.instance.anim.SetBool("Death", false);

                    Game_DataManager.instance.Reset_MyPosition(_event_Init_Position_stationDis, _event_Init_Position_moonDis);

                    yield return new WaitForSeconds(3.5f);

                    Game_UIManager.instance.Fade_out(); //눈감고 뜨기

                    RunnerPlayer1.instance.StoryEventing = false;
                }

                // 한번만 들어가면됨
                if (lefttime < 0)
                {
                    lefttime = 2f;
                    Game_UIManager.instance.Alert();
                }
            }
            else
            {
                lefttime = 2f;
                dontMoveSpeed = 10f;
            }
            yield return null;
        }

        yield return null;


    }
}

