using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Chapter9_Manager : MonoBehaviour
{
    public static Chapter9_Manager instance { get; private set; }
    public GameObject portal_Gate;
    public GameObject[] EventTracks;

    public float Chapter9_Progress;
    public int Chapter9_pos_int = 0;
    public GameObject mission_430_435;
    [Header("Pice")]
    public GameObject ParticleOB;
    public GameObject BaseFx;
    public GameObject BaseFx_Flare;
    public GameObject ObjectFx;
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
        StartCoroutine(_Chapter9_Progress());
    }

    //5키로 마다 나오는 이벤트 구분
    IEnumerator _Chapter9_Progress()
    {
        Chapter9_Progress = 1000 - Game_DataManager.instance.moonDis;
        //Chapter9_Progress = 1000 - 570;
 
        //현재 지점에 따른 이벤트 구간
        if (Chapter9_Progress >= 400 && Chapter9_Progress < 405) Chapter9_pos_int = 1;
        else if (Chapter9_Progress >= 405 && Chapter9_Progress <410) Chapter9_pos_int = 2;
        else if (Chapter9_Progress >= 410 && Chapter9_Progress <415) Chapter9_pos_int = 3;
        else if (Chapter9_Progress >= 415 && Chapter9_Progress <420) Chapter9_pos_int = 4;
        else if (Chapter9_Progress >= 420 && Chapter9_Progress <425) Chapter9_pos_int = 5;
        else if (Chapter9_Progress >= 425 && Chapter9_Progress <430) Chapter9_pos_int = 6;
        else if (Chapter9_Progress >= 430 && Chapter9_Progress <435) Chapter9_pos_int = 7; //우주 꽃 살리기
        else if (Chapter9_Progress >= 435 && Chapter9_Progress <440) Chapter9_pos_int = 8;
        else if (Chapter9_Progress >= 440 && Chapter9_Progress <445) Chapter9_pos_int = 9;
        else if (Chapter9_Progress >= 445 && Chapter9_Progress <450) Chapter9_pos_int = 10;

        EventTracks[Chapter9_pos_int - 1].SetActive(true);
        StartCoroutine(_Chapter9_event()); //맵 구간에 따른 이벤트 
        yield return null;
    }
    IEnumerator _Chapter9_event()
    {
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
            if (num.Equals(4))
            {
                StartCoroutine(_SobbingFlower());
            }
            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        else if (col.name.Equals("SpacePlantBook"))
        {
            Game_TypeWriterEffect.instance.Show_EventStoryText(2);
            RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("SpacePlantBook");
            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        else if (col.name.Equals("MoonPices_Yellow"))
        {
            RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("MoonPices_Yellow");//조각 설명
            Game_DataManager.instance.CollectionNumber = 4;
        }

        if (col.name.Equals("GoalPos"))
        {
            if (RunnerPlayer1.instance.playerPosition > RunnerPlayer1.instance.cinemachineSmoothPath.PathLength)
            {
                portal_Gate.SetActive(true);
            }
        }
    }

    //430-435 우주 꽃 흐느낌 듣기 이벤트 
    IEnumerator _SobbingFlower()
    {
        RunnerPlayer1.instance.StoryEventing = true;
        RunnerPlayer1.instance.moonPowder_useEvnet = true; //달가루 사용 가능
        float viewT = 5f;
        yield return new WaitForSeconds(1f);
        //나레이션이 끝날때 까지 움직이지 않음
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority = 12; //플라워 조준~!
        while (viewT > 0 && Game_TypeWriterEffect.instance.Event_ing.Equals(true))
        {
            viewT -= Time.deltaTime;
            yield return null;
        }
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority = 10;
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        mission_430_435.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3f);
        mission_430_435.SetActive(false);
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
       
        //달가루를 뿌렸다.
        yield return new WaitUntil(() => RunnerPlayer1.instance.use_moonPowder == true);
        Game_TypeWriterEffect.instance.Show_EventStoryText(7);
        RunnerPlayer1.instance.moonPowder_useEvnet = false; //달가루 사용 가능
        yield return new WaitUntil(()=>Game_TypeWriterEffect.instance.Event_ing == false);
        StartCoroutine(_Start_DropPice());
        RunnerPlayer1.instance.StoryEventing = false;
        yield return null; 
    }
    IEnumerator _Start_DropPice()
    {
        ParticleOB.transform.parent = null; //부모랑 분리
        ParticleOB.SetActive(true);
        yield return new WaitForSeconds(1f);
        BaseFx_Flare.SetActive(true); //폭죽 터짐
        yield return new WaitForSeconds(0.3f);
        ObjectFx.SetActive(true);
        // StartCoroutine(_y_rotation());
        yield return null;
    }
}

