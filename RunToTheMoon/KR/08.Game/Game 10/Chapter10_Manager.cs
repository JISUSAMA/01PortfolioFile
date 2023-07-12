using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Chapter10_Manager : MonoBehaviour
{
    public static Chapter10_Manager instance { get; private set; }
    public GameObject portal_Gate;
    public GameObject[] EventTracks;

    public float Chapter10_Progress;
    public int Chapter10_pos_int = 0;

    public GameObject FireworksOb;
    [Header("Blick_Event")]
    public Material[] Blick_Materials;
    public Material FinishLine_mat;
    float time = 0; float F_time = 1; //초기화
    public GameObject mission_480_485;//사라지는 길
    public GameObject mission_495_500; //고장난 산소통
    int blick_pos = 0;
    float disappearValue = 0;

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
        StartCoroutine(_Chapter10_Progress());
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        Debug.Log(col.name);
        if (col.tag.Equals("Narration"))
        {
            int num = int.Parse(col.name);
            Game_TypeWriterEffect.instance.Show_EventStoryText(num);
            //산소 부족
            if (num.Equals(6))
            {
                StartCoroutine(_Lack_oxygen());
            }
            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        else if (Chapter10_pos_int.Equals(7))
        {
            //길사라짐
            if (col.name.Equals("Start_PlankBlick"))
            {
                StartCoroutine(_DisappearEvent());
                StartCoroutine(_DisapeearLine());
                StartCoroutine(_RoadDisappearEvent());
                Debug.LogError("Start_PlankBlick");
            }
            else if (col.name.Equals("End_PlankBlick"))
            {
                // 길갈라짐 오브젝트 초기화
                for (int i = 0; i < Blick_Materials.Length; i++)
                {
                    Blick_Materials[i].SetFloat("Dissolve", 0);
                }
                blick_pos = 0;
                disappearValue = 0;
                RunToStart = false; //달리기!
                SoundFunction.Instance.Mission_End_Sound();
            }
            if (col.name.Equals("Chapter10_PlankBlick2"))
            {
                disappearValue = 0;
                blick_pos = 1;
            }
            else if (col.name.Equals("Chapter10_PlankBlick3"))
            {
                disappearValue = 0;
                blick_pos = 2;
            }
            else if (col.name.Equals("Chapter10_PlankBlick4"))
            {
                disappearValue = 0;
                blick_pos = 3;
            }
            else if (col.name.Equals("Chapter10_PlankBlick5"))
            {
                disappearValue = 0;
                blick_pos = 4;
            }
            else if (col.name.Equals("Chapter10_PlankBlick6"))
            {
                disappearValue = 0;
                blick_pos = 5;
            }
            else if (col.name.Equals("Chapter10_PlankBlick7"))
            {
                disappearValue = 0;
                blick_pos = 6;
            }
            else if (col.name.Equals("Chapter10_PlankBlick8"))
            {
                disappearValue = 0;
                blick_pos = 7;
            }
        }
        else if (Chapter10_pos_int.Equals(3))
        {
            //횟불대 줍기
            if (col.name.Equals("Torch"))
            {
                Game_TypeWriterEffect.instance.Show_EventStoryText(3);
                RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("Torch");
                col.gameObject.SetActive(false);
            }
        }
        else if (Chapter10_pos_int.Equals(4))
        {
            //불꽃놀이 시작!
            if (col.name.Equals("FireStart"))
            {
                FireworksOb.SetActive(true);
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
    //5키로 마다 나오는 이벤트 구분
    IEnumerator _Chapter10_Progress()
    {
        Chapter10_Progress = 1000 - Game_DataManager.instance.moonDis;
        //현재 지점에 따른 이벤트 구간
        if (Chapter10_Progress >= 450 && Chapter10_Progress < 455) Chapter10_pos_int = 1; //갈림길
        else if (Chapter10_Progress >= 455 && Chapter10_Progress < 460) Chapter10_pos_int = 2; 
        else if (Chapter10_Progress >= 460 && Chapter10_Progress < 465) Chapter10_pos_int = 3;
        else if (Chapter10_Progress >= 465 && Chapter10_Progress < 470) Chapter10_pos_int = 4; //불꽃놀이
        else if (Chapter10_Progress >= 470 && Chapter10_Progress < 475) Chapter10_pos_int = 5;
        else if (Chapter10_Progress >= 475 && Chapter10_Progress < 480) Chapter10_pos_int = 6;
        else if (Chapter10_Progress >= 480 && Chapter10_Progress < 485) Chapter10_pos_int = 7;
        else if (Chapter10_Progress >= 485 && Chapter10_Progress < 490) Chapter10_pos_int = 8;
        else if (Chapter10_Progress >= 490 && Chapter10_Progress < 495) Chapter10_pos_int = 9;
        else if (Chapter10_Progress >= 495 && Chapter10_Progress < 500) Chapter10_pos_int = 10;//산소 부족

        EventTracks[Chapter10_pos_int - 1].SetActive(true);
        StartCoroutine(_Chapter10_event()); //맵 구간에 따른 이벤트 
        yield return null;
    }
    IEnumerator _Chapter10_event()
    {
      
        yield return null;
    }
    //길 사라짐 이벤트 구간!
    bool RunToStart = false; //달리기!
    IEnumerator _DisappearEvent()
    {
        RunToStart = false;
       
        //3,2,1 카운트 다운
    
        float dontMoveSpeed = 10;
        float lefttime = 2f;
        RunnerPlayer1.instance.StoryEventing = true;
        mission_480_485.SetActive(true);// 미션 팝업
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3f);
        mission_480_485.SetActive(false);// 미션 팝업
        RunnerPlayer1.instance.StoryEventing = false;
        //초기화 위치 저장
        _event_Init_Position_moonDis = Game_DataManager.instance.moonDis + 0.01f;
        _event_Init_Position_stationDis = Game_DataManager.instance.spaceStationDis + 0.01f;

        Game_TypeWriterEffect.instance.Show_EventStoryText(4);
        RunToStart = true;
        while (RunToStart.Equals(true))
        {
            if (SpeedMeter.instance.velocity_f<20f)
            {
                dontMoveSpeed -= Time.deltaTime;
                lefttime -= Time.deltaTime;
                //5초 이상 움직이지 않거나 달리지 않았을 경우,
                if (dontMoveSpeed < 0)
                {
                    //다시 시작
                    RunToStart = false;
                    StopCoroutine(_DisappearEvent());

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

                    //길사라짐 구간 초기화
                    for (int i = 0; i < Blick_Materials.Length; i++)
                    {
                        Blick_Materials[i].SetFloat("Dissolve", 0);
                    }
                    blick_pos = 0;

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
    IEnumerator _DisapeearLine()
    {
        yield return new WaitUntil(() => RunToStart == true);
        while (RunToStart)
        {
            time = 0; F_time = 1; //초기화
            Color alpha = FinishLine_mat.color;
            while (alpha.a > 0f)
            {
                time += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(1, 0, time);
                FinishLine_mat.color = alpha;
                yield return null;
            }
            time = 0; F_time = 1; //초기화
            alpha = FinishLine_mat.color;
            while (alpha.a < 1f)
            {
                time += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(0, 1, time);
                FinishLine_mat.color = alpha;
                yield return null;
            }
            yield return null;
        }
    }
    IEnumerator _RoadDisappearEvent()
    {
        disappearValue = 0;
        yield return new WaitUntil(() => RunToStart == true);
        while (RunToStart)
        {
            if (blick_pos.Equals(0))
            {
                if (disappearValue < 0.51f)
                {
                    disappearValue += Time.deltaTime * 0.1f;
                    Blick_Materials[0].SetFloat("Dissolve", disappearValue);
                }
            }
            else if (blick_pos.Equals(1))
            {
                if (disappearValue < 0.51f)
                {
                    disappearValue += Time.deltaTime * 0.1f;
                    Blick_Materials[1].SetFloat("Dissolve", disappearValue);
                }
            }
            else if (blick_pos.Equals(2))
            {
                if (disappearValue < 0.51f)
                {
                    disappearValue += Time.deltaTime * 0.1f;
                    Blick_Materials[2].SetFloat("Dissolve", disappearValue);
                }
            }
            else if (blick_pos.Equals(3))
            {
                if (disappearValue < 0.51f)
                {
                    disappearValue += Time.deltaTime * 0.1f;
                    Blick_Materials[3].SetFloat("Dissolve", disappearValue);
                }
            }
            else if (blick_pos.Equals(4))
            {
                if (disappearValue < 0.51f)
                {
                    disappearValue += Time.deltaTime * 0.1f;
                    Blick_Materials[4].SetFloat("Dissolve", disappearValue);
                }
            }
            else if (blick_pos.Equals(5))
            {
                if (disappearValue < 0.51f)
                {
                    disappearValue += Time.deltaTime * 0.1f;
                    Blick_Materials[5].SetFloat("Dissolve", disappearValue);
                }
            }
            else if (blick_pos.Equals(6))
            {
                if (disappearValue < 0.51f)
                {
                    disappearValue += Time.deltaTime * 0.1f;
                    Blick_Materials[6].SetFloat("Dissolve", disappearValue);
                }
            }
            else if (blick_pos.Equals(7))
            {
                if (disappearValue < 0.51f)
                {
                    disappearValue += Time.deltaTime * 0.1f;
                    Blick_Materials[7].SetFloat("Dissolve", disappearValue);
                }
            }

            yield return null;
        }
        Debug.Log(disappearValue + " disappearValue" + " blick_pos" + blick_pos);
        yield return null;
    }
   IEnumerator _Lack_oxygen()
    {
        RunnerPlayer1.instance.StoryEventing = true; 
        yield return new WaitUntil(()=>Game_TypeWriterEffect.instance.Event_ing.Equals(false));
        mission_495_500.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3f);
        mission_495_500.SetActive(false);
        RunnerPlayer1.instance.O2_eventCheck = true;
        RunnerPlayer1.instance.StoryEventing = false;

        yield return null;
    }
}

