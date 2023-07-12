using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Chapter17_Manager : MonoBehaviour
{
    public static Chapter17_Manager instance { get; private set; }
    //public GameObject portal_Gate;
    public GameObject[] EventTracks;

    public float Chapter17_Progress;
    public int Chapter17_pos_int = 0;

    [Header("Evnet")]
    public GameObject Bed;
    public Material[] BedMat;
    public Color Bed_color;
    
    float targetAlpha = 1.0f;

    public GameObject mission_845_850;
    private float _event_Init_Position_moonDis;
    private float _event_Init_Position_stationDis;
    //845-850 잠에서 깨기 이벤트 
    bool RunToStart = false; //달리기!
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        RunnerPlayer1.instance.cinemachineSmoothPath = GameObject.Find("PlayerTrack").GetComponent<CinemachineSmoothPath>();
    }
    private void Start()
    {
        //portal_Gate.SetActive(false);
        StartCoroutine(_Chapter17_Progress());
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        Debug.Log(col.name);
        if (col.tag.Equals("Narration"))
        {
            int num = int.Parse(col.name);
            Game_TypeWriterEffect.instance.Show_EventStoryText(num);
            //침대
            if (num.Equals(2))
            {
                StartCoroutine(_Touch_Bed());
                StartCoroutine(_Description_Object("MyFavorite_bed"));
            }
            //아이스크림
            if (num.Equals(3))
            {
                StartCoroutine(_Description_Object("MyFavorite_Icecream"));
                StartCoroutine(_Event_ShowEvent("MyFavorite_Icecream"));
            }
            //가장 좋아하는 책
            if (num.Equals(4))
            {
                StartCoroutine(_Description_Object("MyFavorite_book"));
                StartCoroutine(_Event_ShowEvent("MyFavorite_book"));
            }
            //사진앨범
            if (num.Equals(5))
            {
                StartCoroutine(_Description_Object("MyAlbum"));
                StartCoroutine(_Event_ShowEvent("MyAlbum"));
            }
            //바람개비
            if (num.Equals(7))
            {
                StartCoroutine(_Description_Object("MyFavorite_pinwheel"));
                StartCoroutine(_Event_ShowEvent("MyFavorite_pinwheel"));
            }
            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        //845-850 잠에서 깨기 이벤트 시작
        if (col.name.Equals("RunStart"))
        {
            StartCoroutine(_WakingUpEvnet());
        }
        else if (col.name.Equals("RunEnd"))
        {
            RunToStart = false;
            SoundFunction.Instance.Mission_End_Sound();
        }
    }
    //5키로 마다 나오는 이벤트 구분
    IEnumerator _Chapter17_Progress()
    {
        Chapter17_Progress = 1000 - Game_DataManager.instance.moonDis;
        //RunnerPlayer1.instance.WakeUp();
        //현재 지점에 따른 이벤트 구간
        if (Chapter17_Progress >= 800 && Chapter17_Progress < 805) Chapter17_pos_int = 1; //눈뜸
        else if (Chapter17_Progress >= 805 && Chapter17_Progress < 810) Chapter17_pos_int = 2;
        else if (Chapter17_Progress >= 810 && Chapter17_Progress < 815) Chapter17_pos_int = 3;//침대
        else if (Chapter17_Progress >= 815 && Chapter17_Progress < 820) Chapter17_pos_int = 4;
        else if (Chapter17_Progress >= 820 && Chapter17_Progress < 825) Chapter17_pos_int = 5;//아이스크림
        else if (Chapter17_Progress >= 825 && Chapter17_Progress < 830) Chapter17_pos_int = 6; //책
        else if (Chapter17_Progress >= 830 && Chapter17_Progress < 835) Chapter17_pos_int = 7;//앨범
        else if (Chapter17_Progress >= 835 && Chapter17_Progress < 840) Chapter17_pos_int = 8;
        else if (Chapter17_Progress >= 840 && Chapter17_Progress < 845) Chapter17_pos_int = 9;//바람개비
        else if (Chapter17_Progress >= 845 && Chapter17_Progress < 850) Chapter17_pos_int = 10;//꿈에서 깨기위해 달려라

        Debug.Log("Chapter17_pos_int : " + Chapter17_pos_int);

        EventTracks[Chapter17_pos_int - 1].SetActive(true);
        StartCoroutine(_Chapter17_event()); //맵 구간에 따른 이벤트 
        yield return null;
    }
    IEnumerator _Chapter17_event()
    {
        //yield return new WaitForSeconds(1f);
        //RunnerPlayer1.instance.WakeUp();
        //yield return new WaitForSeconds(3f);

        if (Chapter17_pos_int.Equals(1))
        {
            Game_TypeWriterEffect.instance.Show_EventStoryText(1);
            yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        }

        yield return null;
    }

    IEnumerator _Touch_Bed()
    {
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority = 12;
        //스킵했을 경우 바로 전환
        yield return new WaitForSeconds(5f);
        Bed.GetComponent<MeshRenderer>().material = BedMat[1];
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority = 10;
        while (!(Mathf.Abs(Bed_color.a) < 0.01f))
        {
            if (Mathf.Abs(Bed_color.a) < 0.01f)
            {
                Bed_color.a = 0f;
            }
            Bed_color.a = Mathf.Lerp(Bed_color.a, 0, Time.deltaTime );
            BedMat[1].color = Bed_color;
            Debug.Log(Bed_color.a);
            yield return null;
        }
        Bed.SetActive(false); //침대 비활성화 시키기

        yield return null;
    }
    IEnumerator _Description_Object(string name)
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription(name);
        yield return null;
    }

    IEnumerator _Event_ShowEvent(string name)
    {
        //아이스크림
        if (name.Equals("MyFavorite_Icecream"))
        {
            RunnerPlayer1.instance._cameraView.vCame[5].m_Priority = 12;
            yield return new WaitForSeconds(5f);
            RunnerPlayer1.instance._cameraView.vCame[5].m_Priority = 10;
        }
        //내가 좋아하는 책
        else if (name.Equals("MyFavorite_book"))
        {
            RunnerPlayer1.instance._cameraView.vCame[4].m_Priority = 12;
            yield return new WaitForSeconds(5f);
            RunnerPlayer1.instance._cameraView.vCame[4].m_Priority = 10;
        }
        //나의 앨범
        else if (name.Equals("MyAlbum"))
        {
            RunnerPlayer1.instance._cameraView.vCame[7].m_Priority = 12;
            yield return new WaitForSeconds(5f);
            RunnerPlayer1.instance._cameraView.vCame[7].m_Priority = 10; 

        }
        //바람개비
        else if (name.Equals("MyFavorite_pinwheel"))
        {
            RunnerPlayer1.instance._cameraView.vCame[6].m_Priority = 12;
            yield return new WaitForSeconds(5f);
            RunnerPlayer1.instance._cameraView.vCame[6].m_Priority = 10;
        }
        yield return null;
    }

    IEnumerator _WakingUpEvnet()
    {
        RunToStart = false;
        RunnerPlayer1.instance.StoryEventing = true;
        Game_TypeWriterEffect.instance.Show_EventStoryText(6);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        mission_845_850.SetActive(true);// 미션 팝업 
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3f);
        mission_845_850.SetActive(false);// 미션 팝업
        RunnerPlayer1.instance.StoryEventing = false;
        // 초기화 위치 저장
        _event_Init_Position_moonDis = Game_DataManager.instance.moonDis + 0.01f;
        _event_Init_Position_stationDis = Game_DataManager.instance.spaceStationDis + 0.01f;
        float dontMoveSpeed = 10;
        float lefttime = 2f;
        RunToStart = true;
        while (RunToStart.Equals(true))
        {
            if (SpeedMeter.instance.velocity_f<20)
            {
                dontMoveSpeed -= Time.deltaTime;
                lefttime -= Time.deltaTime;
                //5초 이상 움직이지 않거나 달리지 않았을 경우,
                if (dontMoveSpeed < 0)
                {
                    //다시 시작
                    RunToStart = false;
                    StopCoroutine(_WakingUpEvnet());

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
}

