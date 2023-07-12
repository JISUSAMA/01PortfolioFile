using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Chapter8_Manager : MonoBehaviour
{
    public static Chapter8_Manager instance { get; private set; }

    public GameObject portal_Gate;
    public GameObject[] EventTracks;
    public GameObject SporeZoonParticle;
    public float Chapter8_Progress;
    public int Chapter8_pos_int = 0;
    [Header("The spores of the moon")]
    public GameObject mission_360_365; //구간 전체 1.2배 산소 깎임
    public GameObject mission_390_395;  //일부 구간에서 
   
    private float _event_Init_Position_moonDis;
    private float _event_Init_Position_stationDis;
    bool RunToStart = false; //달리기!
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
        StartCoroutine(_Chapter8_Progress());
    }

    //5키로 마다 나오는 이벤트 구분
    IEnumerator _Chapter8_Progress()
    {
        Chapter8_Progress = 1000 - Game_DataManager.instance.moonDis;
        //Chapter8_Progress = 1000 - 610;

        //현재 지점에 따른 이벤트 구간
        if (Chapter8_Progress >= 350 && Chapter8_Progress < 355) Chapter8_pos_int = 1;
        else if (Chapter8_Progress >= 355 && Chapter8_Progress < 360) Chapter8_pos_int = 2;
        else if (Chapter8_Progress >= 360 && Chapter8_Progress < 365) Chapter8_pos_int = 3; // 달의포자 산소 깎임 구간
        else if (Chapter8_Progress >= 365 && Chapter8_Progress < 370) Chapter8_pos_int = 4;
        else if (Chapter8_Progress >= 370 && Chapter8_Progress < 375) Chapter8_pos_int = 5;
        else if (Chapter8_Progress >= 375 && Chapter8_Progress < 380) Chapter8_pos_int = 6;
        else if (Chapter8_Progress >= 380 && Chapter8_Progress < 385) Chapter8_pos_int = 7;
        else if (Chapter8_Progress >= 385 && Chapter8_Progress < 390) Chapter8_pos_int = 8;
        else if (Chapter8_Progress >= 390 && Chapter8_Progress < 395) Chapter8_pos_int = 9; //달의 포자 잠깐
        else if (Chapter8_Progress >= 395 && Chapter8_Progress < 400) Chapter8_pos_int = 10;

        EventTracks[Chapter8_pos_int - 1].SetActive(true);
        StartCoroutine(_Chapter8_event()); //맵 구간에 따른 이벤트 
        yield return null;
    }
    IEnumerator _Chapter8_event()
    {
        //달의 포자의 길에서 살아남기
        if (Chapter8_pos_int.Equals(3))
        {
            //달의 포자 발견!
            Game_TypeWriterEffect.instance.Show_EventStoryText(2);
            StartCoroutine(_MoonSporeZoon()); //1.2배 감소
            SporeZoonParticle.SetActive(true);
        }
        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        Debug.Log(col.name);

        if (col.tag.Equals("Narration"))
        {
            int num = int.Parse(col.name);
            Game_TypeWriterEffect.instance.Show_EventStoryText(num);
            //달의 포자 구간 390-395
            if (num.Equals(5))
            {
                StartCoroutine(_MoonSporePart());
            }
            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        //달의 포자 지나기
        else if (col.name.Equals("SporePointEnd"))
        {
            RunnerPlayer1.instance.O2_eventCheck = false; //다시 산소 1배로 돌아감
        }

        if (col.name.Equals("GoalPos"))
        {
            if (RunnerPlayer1.instance.playerPosition > RunnerPlayer1.instance.cinemachineSmoothPath.PathLength)
            {
                portal_Gate.SetActive(true);
            }
        }
    }
    //360-365 달의 포자 등장 산소 1.2배 깎임
    IEnumerator _MoonSporeZoon()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        RunnerPlayer1.instance.StoryEventing = true;

        mission_360_365.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3f);
        mission_360_365.SetActive(false);
        RunnerPlayer1.instance.StoryEventing = false;
        RunnerPlayer1.instance.O2_eventCheck = true; //산소 깎임 1.2배
        yield return null;

    }

    //달의 포자 잠시 등장 18 속도로 뛰어야함
    IEnumerator _MoonSporePart()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        RunnerPlayer1.instance.StoryEventing = true;
        mission_390_395.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3f);
        mission_390_395.SetActive(false);
        RunnerPlayer1.instance.StoryEventing = false;
        RunnerPlayer1.instance.O2_eventCheck = true; //산소 깎임 1.2배
        // 초기화 위치 저장
        _event_Init_Position_moonDis = Game_DataManager.instance.moonDis + 0.01f;
        _event_Init_Position_stationDis = Game_DataManager.instance.spaceStationDis + 0.01f;
        float dontMoveSpeed = 10;
        float lefttime = 2f;
        RunToStart = true;
        while (RunToStart.Equals(true))
        {
            if (SpeedMeter.instance.velocity_f < 18f)
            {
                dontMoveSpeed -= Time.deltaTime;
                lefttime -= Time.deltaTime;
                //5초 이상 움직이지 않거나 달리지 않았을 경우,
                if (dontMoveSpeed < 0)
                {
                    //다시 시작
                    RunToStart = false;
                    StopCoroutine(_MoonSporePart());

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
    //길 사라짐 이벤트 구간!
}

