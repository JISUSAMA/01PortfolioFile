using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Chapter16_Manager : MonoBehaviour
{
    public static Chapter16_Manager instance { get; private set; }
    public GameObject portal_Gate;
    public GameObject[] EventTracks;

    public float Chapter16_Progress;
    public int Chapter16_pos_int = 0;

    private float _event_Init_Position_moonDis;
    private float _event_Init_Position_stationDis;
    bool RunToStart = false; //달리기!
    
    bool Event_ing = false;
    public GameObject mission_770_775; //우주라디오 켜기 
    public GameObject mission_775_780;  //징검다리 
    public GameObject mission_795_800;//달의 포자

    public GameObject SporeMissionStart;
    public GameObject SporeMissionEnd;
    private bool StpEventStart = false;
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
        StartCoroutine(_Chapter16_Progress());
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        Debug.Log(col.name);
        if (col.tag.Equals("Narration"))
        {
            int num = int.Parse(col.name);
            Game_TypeWriterEffect.instance.Show_EventStoryText(num);
            if (Chapter16_pos_int.Equals(8)) 
            {
                if (num.Equals(6))
                {
             
                    StartCoroutine(_Constellation_Event());
                }
            }
            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        if (col.name.Equals("BrokenRadio"))
        {
            StartCoroutine(_TurnOn_CosmicRadio()); //우주라디오 켜기
        }

        if (col.name.Equals("GoalPos"))
        {
            if (RunnerPlayer1.instance.playerPosition > RunnerPlayer1.instance.cinemachineSmoothPath.PathLength)
            {
                //달의 포자 걷는 도중 쓰러짐
                if (Chapter16_pos_int.Equals(10))
                {

                }
                else
                {
                    portal_Gate.SetActive(true);
                }
               
            }
        }
        //달의 포자 미션 시작
        if (col.name.Equals("SporeMissionStart"))
        {
            StartCoroutine(_SporeZoonEvnet());

        }
        //끝자락 도착
        if (col.name.Equals("SporeMissionEnd"))
        {
            RunToStart = false;
            SoundFunction.Instance.Mission_End_Sound();
            StartCoroutine(_SporeMissionEndPos());
        }

        if (Chapter16_pos_int.Equals(6))
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
                //
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
        RunnerPlayer1.instance.moonPowder_useEvnet = true;
        yield return ws;
        yield return new WaitUntil(() => !Game_TypeWriterEffect.instance.Event_ing);    // 나레이션이 끝날 때 까지 기다림
        yield return ws;

        // 미션 팝업 /////////////////////////////
        mission_775_780.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
   
        //3,2,1 카운트 다운
        yield return new WaitForSeconds(3f);
        mission_775_780.SetActive(false);
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
    IEnumerator _Chapter16_Progress()
    {
        Chapter16_Progress = 1000 - Game_DataManager.instance.moonDis;
        
        //현재 지점에 따른 이벤트 구간
        if (Chapter16_Progress >= 750 && Chapter16_Progress < 755) Chapter16_pos_int = 1;
        else if (Chapter16_Progress >= 755 && Chapter16_Progress < 760) Chapter16_pos_int = 2;
        else if (Chapter16_Progress >= 760 && Chapter16_Progress < 765) Chapter16_pos_int = 3;
        else if (Chapter16_Progress >= 765 && Chapter16_Progress < 770) Chapter16_pos_int = 4;//우주라디오 켜기 
        else if (Chapter16_Progress >= 770 && Chapter16_Progress < 775) Chapter16_pos_int = 5;
        else if (Chapter16_Progress >= 775 && Chapter16_Progress < 780) Chapter16_pos_int = 6;//징검다리 
        else if (Chapter16_Progress >= 780 && Chapter16_Progress < 785) Chapter16_pos_int = 7;
        else if (Chapter16_Progress >= 785 && Chapter16_Progress < 790) Chapter16_pos_int = 8; //별자리 
        else if (Chapter16_Progress >= 790 && Chapter16_Progress < 795) Chapter16_pos_int = 9;
        else if (Chapter16_Progress >= 795 && Chapter16_Progress < 800) Chapter16_pos_int = 10;//달의 포자 

        EventTracks[Chapter16_pos_int - 1].SetActive(true);
        StartCoroutine(_Chapter16_event()); //맵 구간에 따른 이벤트 
        yield return null;
    }
    IEnumerator _Chapter16_event()
    {
        yield return new WaitForSeconds(1);
        //달의 포자 구간
        if (Chapter16_pos_int.Equals(10))
        {
           
        }
        yield return null;
    }
    //우주라디오 켜기 이벤트
    IEnumerator _TurnOn_CosmicRadio()
    {
        RunnerPlayer1.instance.StoryEventing = true;
        RunnerPlayer1.instance.moonPowder_useEvnet = true;
        //라디오 고장
        Game_TypeWriterEffect.instance.Show_EventStoryText(4);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        mission_770_775.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3f);
        mission_770_775.SetActive(false);
        
        float waitTiem = 6f;
        //5초 이상 달가루를 사용하지 않으면 "달가루를 사용해줘" 사인 보내기
        while (waitTiem > -1)
        {
            Debug.Log(waitTiem);
            waitTiem -= Time.deltaTime;
            if (!RunnerPlayer1.instance.use_moonPowder && waitTiem <= 0)
            {
                Game_TypeWriterEffect.instance.Show_TypingText("가방에서 달가루를 꺼내 사용해보자!");
                waitTiem = 8f; //초기화
            }
            if (RunnerPlayer1.instance.use_moonPowder)
            {
                break;
            }
            yield return null;
        }

        yield return new WaitUntil(() => RunnerPlayer1.instance.use_moonPowder == true);
        RunnerPlayer1.instance.use_moonPowder = true;
        SoundFunction.Instance.Mission_End_Sound();
        Game_TypeWriterEffect.instance.Show_EventStoryText(5);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        RunnerPlayer1.instance.use_moonPowder = false;
        RunnerPlayer1.instance.StoryEventing = false;
        
       yield return null;
    }
    IEnumerator _Constellation_Event()
    {
        float viewT = 10f;
        yield return new WaitForSeconds(3f);
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority = 12;
        while (viewT > 0 && Game_TypeWriterEffect.instance.Event_ing.Equals(true))
        {
            viewT -= Time.deltaTime;
            yield return null;
        }
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority = 10;
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority = 10;
        yield return null;
    }
    IEnumerator _SporeZoonEvnet()
    {
        RunnerPlayer1.instance.StoryEventing = true;
        Game_TypeWriterEffect.instance.Show_EventStoryText(7);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        mission_795_800.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3);
        mission_795_800.SetActive(false);
        RunnerPlayer1.instance.StoryEventing = false;
        RunnerPlayer1.instance.O2_eventCheck = true; //산소 1.2배
        // 초기화 위치 저장
        _event_Init_Position_moonDis = Game_DataManager.instance.moonDis + 0.01f;
        _event_Init_Position_stationDis = Game_DataManager.instance.spaceStationDis + 0.01f;
        float dontMoveSpeed = 10;
        float lefttime = 2f;
        RunToStart = true;
        while (RunToStart.Equals(true))
        {
            if (SpeedMeter.instance.velocity_f < 20f)
            {
                dontMoveSpeed -= Time.deltaTime;
                lefttime -= Time.deltaTime;
                //5초 이상 움직이지 않거나 달리지 않았을 경우,
                if (dontMoveSpeed < 0)
                {
                    //다시 시작
                    RunToStart = false;
                    StopCoroutine(_SporeZoonEvnet());

                    // 미션 실패 조건 성립
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
                    RunnerPlayer1.instance.O2_eventCheck = false; //산소 1.2배
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
    }
    //달의 포자 달리기 끝! 쓰러지고 게임 종료 
    IEnumerator _SporeMissionEndPos()
    {
        RunnerPlayer1.instance.StoryEventing = true;
        Game_TypeWriterEffect.instance.Show_EventStoryText(8);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        //쓰러지는 애니메이션 
        RunnerPlayer1.instance.anim.SetBool("Death", true);

        yield return new WaitForSeconds(4f);
        Game_UIManager.instance.Fade_in(); //눈감고 뜨기
        yield return new WaitForSeconds(1f);

        RunnerPlayer1.instance.anim.SetBool("Death", false);
        Game_UIManager.instance.Reach_SpaceStation();
        yield return null;
    }
}

