using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter2_Manager : MonoBehaviour
{
    public static Chapter2_Manager instance { get; private set; }
    public GameObject[] EventTracks;
    public float Chapter2_Progress;
    public int Chapter2_pos_int = 0;
    [Header("To shake off loneliness")]
    public GameObject mission_55_60; //외로움 쫒기
    bool RunToStart = false;

    [Header("조각함")]
    public GameObject[] ITEM_SculptureDescription;
    public Text Sculpture_Description_count_txt;

    public GameObject portal_Gate;

    private float _event_Init_Position_moonDis;
    private float _event_Init_Position_stationDis;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }
    void Start()
    {
        RunnerPlayer1.instance.cinemachineSmoothPath = GameObject.Find("PlayerTrack").GetComponent<CinemachineSmoothPath>();
        portal_Gate.SetActive(false);
        StartCoroutine(_Chapter2_Progress());
    }

    //5키로 마다 나오는 이벤트 구분
    IEnumerator _Chapter2_Progress()
    {
        Chapter2_Progress = 1000 - Game_DataManager.instance.moonDis;
        //현재 지점에 따른 이벤트 구간
        if (Chapter2_Progress >= 50 && Chapter2_Progress < 55) Chapter2_pos_int = 1;
        else if (Chapter2_Progress >= 55 && Chapter2_Progress < 60) Chapter2_pos_int = 2;
        else if (Chapter2_Progress >= 60 && Chapter2_Progress < 65) Chapter2_pos_int = 3;
        else if (Chapter2_Progress >= 65 && Chapter2_Progress < 70) Chapter2_pos_int = 4;
        else if (Chapter2_Progress >= 70 && Chapter2_Progress < 75) Chapter2_pos_int = 5;
        else if (Chapter2_Progress >= 75 && Chapter2_Progress < 80) Chapter2_pos_int = 6;
        else if (Chapter2_Progress >= 80 && Chapter2_Progress < 85) Chapter2_pos_int = 7;
        else if (Chapter2_Progress >= 85 && Chapter2_Progress < 90) Chapter2_pos_int = 8;
        else if (Chapter2_Progress >= 90 && Chapter2_Progress < 95) Chapter2_pos_int = 9;
        else if (Chapter2_Progress >= 95 && Chapter2_Progress < 100) Chapter2_pos_int = 10;

        EventTracks[Chapter2_pos_int - 1].SetActive(true);
        StartCoroutine(_Chapter2_event());
        yield return null;
    }
    IEnumerator _Chapter2_event()
    {

        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        //50-55 누군가의 쪽지
        if (Chapter2_pos_int.Equals(1))
        {
            
            if (col.name.Equals("Letter"))
            {
                StartCoroutine(_SomeonesLetter());
                Destroy(col.gameObject);
            }
        }
        //55-60
        else if (Chapter2_pos_int.Equals(2))
        {
            //55-60 외로움 쫓기 이벤트 시작
            if (col.name.Equals("RunStart"))
            {
                StartCoroutine(_RunFromLonely());
            }
            else if (col.name.Equals("RunEnd"))
            {
                RunToStart = false;
            }
        }
        //70-75
        else if (Chapter2_pos_int.Equals(5))
        {
            //갈림길
            if (col.name.Equals("Crossroads"))
            {
                Game_TypeWriterEffect.instance.Show_EventStoryText(3);
            }
        }
        //75-80
        else if (Chapter2_pos_int.Equals(6))
        {
            //조각함 줍기
            if (col.name.Equals("JewelBox"))
            {
                StartCoroutine(_SculptureBoxEvnet());
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
   //외로움 쫓기 이벤트 //12 velocity
    IEnumerator _RunFromLonely()
    {
        RunToStart = false;
        Game_TypeWriterEffect.instance.Show_EventStoryText(2);
        RunnerPlayer1.instance.StoryEventing = true;
        yield return new WaitForSeconds(1.5f);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        yield return new WaitForSeconds(1.5f);
        mission_55_60.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3f);
        mission_55_60.SetActive(false);
        RunnerPlayer1.instance.StoryEventing = false;

        float dontMoveSpeed = 10;
        float lefttime = 2f;
        // 초기화 위치 저장
        _event_Init_Position_moonDis = Game_DataManager.instance.moonDis + 0.01f;
        _event_Init_Position_stationDis = Game_DataManager.instance.spaceStationDis + 0.01f;

        RunToStart = true;
        while (RunToStart.Equals(true))
        {
            if (RunnerPlayer1.instance.input_Speed == 0 || SpeedMeter.instance.velocity_f<12)
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
    //의문의 조각함 이벤트
    IEnumerator _SculptureBoxEvnet()
    {
        Game_TypeWriterEffect.instance.Show_EventStoryText(4); 
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        //튜토리얼 시작
        ITEM_SculptureDescription[0].SetActive(true);
        for(int i =5; i>0; i--)
        {
            Sculpture_Description_count_txt.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        Time.timeScale = 0;
        ITEM_SculptureDescription[1].SetActive(false);
        ITEM_SculptureDescription[2].SetActive(true);
    
        yield return null;
    }
    public void ScultureBox_close()
    {
        ITEM_SculptureDescription[6].SetActive(false);
        ITEM_SculptureDescription[1].SetActive(true);
        ITEM_SculptureDescription[0].SetActive(false);
        Time.timeScale = 1;
    }
   
    IEnumerator _SomeonesLetter()
    { 
        //애니메이션 완료 후 실행되는 부분
        RunnerPlayer1.instance.anim.SetBool("Idle 2", false);
        RunnerPlayer1.instance.anim.SetBool("Idle 3", false);
        RunnerPlayer1.instance.anim.SetBool("Look_Behind", false);

        RunnerPlayer1.instance.speed = 0f;
        RunnerPlayer1.instance.anim.SetBool("Pickup", true);

        RunnerPlayer1.instance.CheckAnimationState();   // Anim State Check

        Game_TypeWriterEffect.instance.Show_EventStoryText(1);
      yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false); //나레이션 끝나면
        RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("Letter");
        
        yield return null;
    }
}
