using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter7_Manager : MonoBehaviour
{
    public static Chapter7_Manager instance { get; private set; }

    public GameObject portal_Gate;
    public GameObject[] EventTracks;

    public float Chapter7_Progress;
    public int Chapter7_pos_int = 0;

    //Chapter 7에서 발생하는 이벤트 관리
    [Header("Event")]
    public Chapter7_whaleCtrl _7Chapter_event;

    [Header("Whale tail chasing")]
    public GameObject mission_320_325;
    public bool WhaleEventFinish = false;
    public GameObject WhaleTrack_ob; 
    [Header("SteppingStone_Event")]
    public GameObject mission_330_335;
    private bool StpEventStart = false;


    private float _event_Init_Position_moonDis;
    private float _event_Init_Position_stationDis;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        Debug.Log(col.name);
        //고래가 조각을 떨구는 이벤트 발생
        if (col.name.Equals("WhaleEventPosition"))
        {
            //고래 꼬리 조각 떨어짐
            StartCoroutine(_WhaleFinishEvnet());
            col.gameObject.SetActive(false);
          //  _7Chapter_event.pice_ob.transform.parent = null; //부모와 분리
        }
        //고래 이벤트 두두둥장!
        if (col.name.Equals("WhaleEventStartPosition"))
        {
            StartCoroutine(_Whale_tail_chasing());
            col.gameObject.SetActive(false);
        }

        if (Chapter7_pos_int.Equals(7))
        {
            if (col.name.Equals("Start_SteppingStone"))
            {
                StartCoroutine(SteppingStoneRunning());
            }
            else if (col.name.Equals("End_SteppingStone"))
            {
                StopCoroutine(SteppingStoneRunning());
                RunnerPlayer1.instance.use_moonPowder = false; 
                RunnerPlayer1.instance.moonPowder_useEvnet = false; 
                StpEventStart = false;
                RunnerPlayer1.instance.StoryEventing = false;
            }
        }

        if (col.tag.Equals("Narration"))
        {
            int num = int.Parse(col.name);
            Game_TypeWriterEffect.instance.Show_EventStoryText(num);

            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        if (col.name.Equals("MoonPices_Green"))
        {
            RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("MoonPices_Green");//조각 설명
            Game_DataManager.instance.CollectionNumber = 3;
        }

    }
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        StpEventStart = false;
        RunnerPlayer1.instance.cinemachineSmoothPath = GameObject.Find("PlayerTrack").GetComponent<CinemachineSmoothPath>();
        portal_Gate.SetActive(false);
        StartCoroutine(_Chapter7_Progress());
    }

    //5키로 마다 나오는 이벤트 구분
    IEnumerator _Chapter7_Progress()
    {
        Chapter7_Progress = 1000 - Game_DataManager.instance.moonDis;
        //Chapter7_Progress = 1000 - 680;

        //현재 지점에 따른 이벤트 구간
        if (Chapter7_Progress >= 300 && Chapter7_Progress < 305) Chapter7_pos_int = 1;
        else if (Chapter7_Progress >= 305 && Chapter7_Progress < 310) Chapter7_pos_int  = 2;
        else if (Chapter7_Progress >= 310 && Chapter7_Progress < 315) Chapter7_pos_int  = 3;
        else if (Chapter7_Progress >= 315 && Chapter7_Progress < 320) Chapter7_pos_int  = 4;
        else if (Chapter7_Progress >= 320 && Chapter7_Progress < 325) Chapter7_pos_int  = 5; //고래 쫓기 이벤트
        else if (Chapter7_Progress >= 325 && Chapter7_Progress < 330) Chapter7_pos_int  = 6;
        else if (Chapter7_Progress >= 330 && Chapter7_Progress < 335) Chapter7_pos_int  = 7; //징검다리 건너기
        else if (Chapter7_Progress >= 335 && Chapter7_Progress < 340) Chapter7_pos_int  = 8;
        else if (Chapter7_Progress >= 340 && Chapter7_Progress < 345) Chapter7_pos_int  = 9;
        else if (Chapter7_Progress >= 345 && Chapter7_Progress < 350) Chapter7_pos_int = 10;

        EventTracks[Chapter7_pos_int - 1].SetActive(true);
        StartCoroutine(_Chapter7_event()); //맵 구간에 따른 이벤트 
        yield return null;
    }
    IEnumerator _Chapter7_event()
    {
   
        yield return null;
    }
    IEnumerator _Whale_tail_chasing()
    {
        float viewT = 8f;
        RunnerPlayer1.instance.StoryEventing = true;
        Game_TypeWriterEffect.instance.Show_EventStoryText(4);//저멀리서 무언가 다가오고 있어..
        yield return new WaitForSeconds(2f);
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority = 12;
        _7Chapter_event.Whale_dolly.m_Speed = 5f;
        while (viewT > 0 && Game_TypeWriterEffect.instance.Event_ing.Equals(true))
        {
            viewT -= Time.deltaTime;
            yield return null;
        }
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority = 10;
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority = 10;
        //미션 알려주기 
        mission_320_325.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3);
        mission_320_325.SetActive(false);
        yield return new WaitForSeconds(1);
        //고래 돌아다님
        RunnerPlayer1.instance.StoryEventing = false;

        yield return null;
    }
    IEnumerator _WhaleFinishEvnet()
    {
        RunnerPlayer1.instance.StoryEventing = true;
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority = 12;
        _7Chapter_event.Whale_dolly.m_Speed = 12f;
   
        yield return new WaitUntil(() => WhaleEventFinish == true);
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority = 10; //고래 캠에서 보석 이쪽으로 
        RunnerPlayer1.instance._cameraView.vCame[4].m_Priority = 12;
        _7Chapter_event.pice_ob.SetActive(true); //초록색 조각 떨굼
        yield return new WaitForSeconds(3f);
        RunnerPlayer1.instance._cameraView.vCame[4].m_Priority = 10;

        //고래 돌아다님
        RunnerPlayer1.instance.StoryEventing = false;
        yield return null;

    }
    IEnumerator SteppingStoneRunning()
    {
        Game_TypeWriterEffect.instance.Show_EventStoryText(7);
        WaitForSeconds ws = new WaitForSeconds(1f);
       
        RunnerPlayer1.instance.StoryEventing = true;    // speed = 0;
        RunnerPlayer1.instance.moonPowder_useEvnet = true; //달가루 사용 가능함

        yield return ws;
        yield return new WaitUntil(() => !Game_TypeWriterEffect.instance.Event_ing);    // 나레이션이 끝날 때 까지 기다림
        yield return ws;

        // 미션 팝업 /////////////////////////////
        mission_330_335.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
 
        //3,2,1 카운트 다운
        yield return new WaitForSeconds(3f);
        mission_330_335.SetActive(false);
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
