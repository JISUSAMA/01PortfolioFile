using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Chapter14_Manager : MonoBehaviour
{
    public static Chapter14_Manager instance { get; private set; }
    public GameObject portal_Gate;
    public GameObject[] EventTracks;

    public float Chapter14_Progress;
    public int Chapter14_pos_int = 0;

    public GameObject LightBird_Track_ob;
    public CinemachineDollyCart Cart1;
    public CinemachineDollyCart Cart2;
  
    bool RunToStart = false;
    public GameObject mission_670_675;
    public GameObject mission_695_700;
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
        StartCoroutine(_Chapter14_Progress());
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        Debug.Log(col.name);
        if (col.tag.Equals("Narration"))
        {
            int num = int.Parse(col.name);
            Game_TypeWriterEffect.instance.Show_EventStoryText(num);
            if (col.name.Equals(4))
            {
                LightBird_Track_ob.SetActive(true);
            }
            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        //695-700
        if (Chapter14_pos_int.Equals(10))
        {
            if (col.name.Equals("RunStart"))
            {
                RunnerPlayer1.instance.StoryEventing = true;
                StartCoroutine(_ColdNess_road());
            }
            else if (col.name.Equals("RunEnd"))
            {
                RunToStart = false;
                SoundFunction.Instance.Mission_End_Sound();
            }
        }

        if (col.name.Equals("GoalPos"))
        {
            if (RunnerPlayer1.instance.playerPosition > RunnerPlayer1.instance.cinemachineSmoothPath.PathLength)
            {
                portal_Gate.SetActive(true);
            }
        }

        if (Chapter14_pos_int.Equals(5))
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
                RunnerPlayer1.instance.use_moonPowder = false;
                RunnerPlayer1.instance.moonPowder_useEvnet = false;
            }
        }
    }
    IEnumerator SteppingStoneRunning()
    {
        Game_TypeWriterEffect.instance.Show_EventStoryText(3);
        WaitForSeconds ws = new WaitForSeconds(1f);
        RunnerPlayer1.instance.StoryEventing = true;    // speed = 0;
        RunnerPlayer1.instance.moonPowder_useEvnet = true; //달가루 사용 가능함
        yield return ws;
        yield return new WaitUntil(() => !Game_TypeWriterEffect.instance.Event_ing);    // 나레이션이 끝날 때 까지 기다림
        yield return ws;
        // 미션 팝업 /////////////////////////////
        mission_670_675.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        
        //3,2,1 카운트 다운
        yield return new WaitForSeconds(3f);
        mission_670_675.SetActive(false);
        RunnerPlayer1.instance.StoryEventing = false;   // speed input
        /////////////////////////////////////////
        Game_UIManager.instance.myItemPopup_sc.Use_MoonPowder(); //자동으로 달가루 사용
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
                    RunnerPlayer1.instance.use_moonPowder = false; //달가루 사용된거 사라짐
                    RunnerPlayer1.instance.anim.SetBool("Death", false);

                    // 리셋 : 멀리서 실패하면 시간 좀 더 줘야함
                    Game_DataManager.instance.Reset_MyPosition(_event_Init_Position_stationDis, _event_Init_Position_moonDis);

                    yield return new WaitForSeconds(3.5f);

                    Game_UIManager.instance.Fade_out(); //눈감고 뜨기
                    RunnerPlayer1.instance.use_moonPowder = false;
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
    IEnumerator _Chapter14_Progress()
    {
        Chapter14_Progress = 1000 - Game_DataManager.instance.moonDis;

        //현재 지점에 따른 이벤트 구간
        if (Chapter14_Progress >= 650 && Chapter14_Progress < 655) Chapter14_pos_int = 1; //크리스탈 결정체
        else if (Chapter14_Progress >= 655 && Chapter14_Progress < 660) Chapter14_pos_int = 2;
        else if (Chapter14_Progress >= 660 && Chapter14_Progress < 665) Chapter14_pos_int = 3;
        else if (Chapter14_Progress >= 665 && Chapter14_Progress < 670) Chapter14_pos_int = 4;
        else if (Chapter14_Progress >= 670 && Chapter14_Progress < 675) Chapter14_pos_int = 5; //징검다리 건너기 
        else if (Chapter14_Progress >= 675 && Chapter14_Progress < 680) Chapter14_pos_int = 6;
        else if (Chapter14_Progress >= 680 && Chapter14_Progress < 685) Chapter14_pos_int = 7; //빛새
        else if (Chapter14_Progress >= 685 && Chapter14_Progress < 690) Chapter14_pos_int = 8;
        else if (Chapter14_Progress >= 690 && Chapter14_Progress < 695) Chapter14_pos_int = 9;
        else if (Chapter14_Progress >= 695 && Chapter14_Progress < 700) Chapter14_pos_int = 10; //길의 차가움

        EventTracks[Chapter14_pos_int - 1].SetActive(true);
        StartCoroutine(_Chapter14_event()); //맵 구간에 따른 이벤트 
        yield return null;
    }
    IEnumerator _Chapter14_event()
    {
        //크리스탈 결정체
        if (Chapter14_pos_int.Equals(1))
        {
            yield return new WaitForSeconds(15f);
            Game_TypeWriterEffect.instance.Show_EventStoryText(5); //크리스탈 결정체
        }
        //680-685 빛새
        if (Chapter14_pos_int.Equals(7))
        {
            StartCoroutine(_LightBird_Track());
        }
        yield return null;
    }
    //처음 등장 후, 150초 마다 빛새가 날아 다님
    IEnumerator _LightBird_Track()
    {
        yield return new WaitForSeconds(150f); 
        float Cart_ActiveTime = 150f;
        LightBird_Track_ob.SetActive(true);
        Game_TypeWriterEffect.instance.Show_EventStoryText(4);
        while (!Game_DataManager.instance.runEndState)
        {
            Debug.Log(Cart_ActiveTime);
            //빛새 활성화 되있을 경우,
            if (LightBird_Track_ob.activeSelf)
            {
                //카트 위치가 0에 도착하면 멈춤
                if (Mathf.Floor(Cart1.m_Position).Equals(0))
                {
                    Cart1.m_Position = 1f;
                    Cart1.m_Speed = 0;
                }
                if (Mathf.Floor(Cart2.m_Position).Equals(0))
                {
                    Cart2.m_Position = 1f;
                    Cart2.m_Speed = 0;
                }
                //둘다 한바퀴 돌았을 경우, 부모 비활성화
                if(Cart1.m_Speed ==0 && Cart2.m_Speed == 0)
                {
                    LightBird_Track_ob.SetActive(false);
                    Cart_ActiveTime = 150;
                }
            }
            else
            {
                //150초가 지나고 나면 다시 활성화 시켜줌
                Cart_ActiveTime -= Time.deltaTime;
                if (Cart_ActiveTime < 0)
                {
                    LightBird_Track_ob.SetActive(true);
                    Cart1.m_Speed = 5;
                    Cart2.m_Speed = 5;
                }
            }
            yield return null;
        }
        yield return null;
    }
    //Mission_695_700
    IEnumerator _ColdNess_road()
    {
        RunToStart = false;
        Game_TypeWriterEffect.instance.Show_EventStoryText(6);
        RunnerPlayer1.instance.StoryEventing = true;
        yield return new WaitForSeconds(1.5f);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        yield return new WaitForSeconds(1.5f);
        mission_695_700.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        //3,2,1 카운트 다운
        yield return new WaitForSeconds(3);
        mission_695_700.SetActive(false);
        // 초기화 위치 저장
        _event_Init_Position_moonDis = Game_DataManager.instance.moonDis + 0.01f;
        _event_Init_Position_stationDis = Game_DataManager.instance.spaceStationDis + 0.01f;
       
        RunnerPlayer1.instance.StoryEventing = false;
        RunToStart = true;
        float dontMoveSpeed = 10;
        float lefttime = 2f;
        while (RunToStart.Equals(true))
        {
            if (SpeedMeter.instance.velocity_f<12)
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
}

