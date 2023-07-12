using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Chapter18_Manager : MonoBehaviour
{
    public static Chapter18_Manager instance { get; private set; }
    public GameObject portal_Gate;
    public GameObject[] EventTracks;

    public float Chapter18_Progress;
    public int Chapter18_pos_int = 0;

    public GameObject mission_870_875;  //징검다리 
    public GameObject mission_895_900; //고장난 산소통

    private bool StpEventStart = false;

    private float _event_Init_Position_moonDis;
    private float _event_Init_Position_stationDis;
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
        StartCoroutine(_Chapter18_Progress());
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        Debug.Log(col.name);
        if (col.tag.Equals("Narration"))
        {
            int num = int.Parse(col.name);
            Game_TypeWriterEffect.instance.Show_EventStoryText(num);
            //895-900 산소통 구멍 발생!
            if (Chapter18_pos_int.Equals(10))
            {
                if (num.Equals(8))
                {
                    StartCoroutine(_OxygenTank_Broken());
                }
            }
            col.SetActive(false); //이벤트 발생 콜라이더 지우기
            
        }

        if (Chapter18_pos_int.Equals(5))
        {
            if (col.name.Equals("Start_SteppingStone"))
            {
                StartCoroutine(SteppingStoneRunning());
            }
            else if (col.name.Equals("End_SteppingStone"))
            {
                StopCoroutine(SteppingStoneRunning());
                StpEventStart = false;
                SoundFunction.Instance.Mission_End_Sound();
                RunnerPlayer1.instance.StoryEventing = false;

                RunnerPlayer1.instance.moonPowder_useEvnet = false;
                RunnerPlayer1.instance.use_moonPowder = false;
            }
        }

        if (col.name.Equals("GoalPos"))
        {
            if (RunnerPlayer1.instance.playerPosition > RunnerPlayer1.instance.cinemachineSmoothPath.PathLength)
            {
                portal_Gate.SetActive(true);
            }
        }
    }
    IEnumerator SteppingStoneRunning()
    {
        Game_TypeWriterEffect.instance.Show_EventStoryText(4);
        WaitForSeconds ws = new WaitForSeconds(1f);
        RunnerPlayer1.instance.StoryEventing = true;    // speed = 0;
        RunnerPlayer1.instance.moonPowder_useEvnet = true; //달가루 자동으로 사용

        yield return ws;
        yield return new WaitUntil(() => !Game_TypeWriterEffect.instance.Event_ing);    // 나레이션이 끝날 때 까지 기다림
        yield return ws;

        // 미션 팝업 /////////////////////////////
        mission_870_875.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();

        //3,2,1 카운트 다운
        yield return new WaitForSeconds(3f);
        mission_870_875.SetActive(false);
        RunnerPlayer1.instance.StoryEventing = false;   // speed input
        /////////////////////////////////////////
        Game_UIManager.instance.myItemPopup_sc.Use_MoonPowder(); //달가루 자동으로 사용

        // 초기화 위치 저장
        _event_Init_Position_moonDis = Game_DataManager.instance.moonDis + 0.01f;
        _event_Init_Position_stationDis = Game_DataManager.instance.spaceStationDis + 0.01f;
        float dontMoveSpeed = 10;
        float lefttime = 2;
        StpEventStart = true;

        while (StpEventStart.Equals(true))
        {
            if (SpeedMeter.instance.velocity_f < 15)
            {
                dontMoveSpeed -= Time.deltaTime;
                lefttime -= Time.deltaTime;
                //5초 이상 움직이지 않거나 달리지 않았을 경우,
                if (dontMoveSpeed < 0)
                {
                    // 미션 실패 조건 성립
                    RunnerPlayer1.instance.StoryEventing = true;
                    RunnerPlayer1.instance.anim.SetBool("Death", true);

                    yield return new WaitForSeconds(4f);
                    Game_UIManager.instance.Fade_in(); //눈감고 뜨기
                    yield return new WaitForSeconds(1f);

                    //다시 시작
                    StpEventStart = false;
                    RunnerPlayer1.instance.use_moonPowder = false;
                    RunnerPlayer1.instance.anim.SetBool("Death", false);

                    // 리셋 : 멀리서 실패하면 시간 좀 더 줘야함
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
    //5키로 마다 나오는 이벤트 구분
    IEnumerator _Chapter18_Progress()
    {
        Chapter18_Progress = 1000 - Game_DataManager.instance.moonDis;
        //Chapter18_Progress = 1000 - 130;

        //현재 지점에 따른 이벤트 구간
        if (Chapter18_Progress >= 850 && Chapter18_Progress < 855) Chapter18_pos_int = 1;
        else if (Chapter18_Progress >= 855 && Chapter18_Progress < 860) Chapter18_pos_int = 2;
        else if (Chapter18_Progress >= 860 && Chapter18_Progress < 865) Chapter18_pos_int = 3;
        else if (Chapter18_Progress >= 865 && Chapter18_Progress < 870) Chapter18_pos_int = 4;
        else if (Chapter18_Progress >= 870 && Chapter18_Progress < 875) Chapter18_pos_int = 5;  //징검다리
        else if (Chapter18_Progress >= 875 && Chapter18_Progress < 880) Chapter18_pos_int = 6;
        else if (Chapter18_Progress >= 880 && Chapter18_Progress < 885) Chapter18_pos_int = 7;
        else if (Chapter18_Progress >= 885 && Chapter18_Progress < 890) Chapter18_pos_int = 8;
        else if (Chapter18_Progress >= 890 && Chapter18_Progress < 895) Chapter18_pos_int = 9; //하늘섬
        else if (Chapter18_Progress >= 895 && Chapter18_Progress < 900) Chapter18_pos_int = 10; //산소통 구멍

        EventTracks[Chapter18_pos_int - 1].SetActive(true);
        StartCoroutine(_Chapter18_event()); //맵 구간에 따른 이벤트 
        yield return null;
    }
    IEnumerator _Chapter18_event()
    {
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.isReadyTextEvent);
        Game_TypeWriterEffect.instance.isReadyTextEvent = false;

        if (Chapter18_pos_int.Equals(1))
        {
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
            Game_TypeWriterEffect.instance.Show_EventStoryText(5);//잠에서 깨어남
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
            //조각획득
            RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("MoonPices_Blue");
            Game_DataManager.instance.CollectionNumber = 8;
        }

        if (Chapter18_pos_int.Equals(9))
        {
            Debug.Log("하늘섬 이벤트???");
            Game_TypeWriterEffect.instance.Show_EventStoryText(6); //하늘섬 이벤트 
        }
        yield return null;
    }

    //산소통 구멍
    IEnumerator _OxygenTank_Broken()
    {
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        mission_895_900.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3);
        mission_895_900.SetActive(false);
        RunnerPlayer1.instance.O2_eventCheck = true;
        yield return null;
    }
}

