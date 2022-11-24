using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter4_Manager : MonoBehaviour
{
    public GameObject[] EventTracks;
    public float Chapter4_Progress; //전체 진행정도
    public int Chapter4_pos_int = 0; //현재 맵에서의 진행 정도
    public GameObject portal_Gate;

    [Header("Blick_Event")]
    public Material[] Blick_Materials;
    public Material FinishLine_mat;
    float time = 0; float F_time = 1; //초기화
    public GameObject mission_190_195;
    int blick_pos = 0;
    float disappearValue = 0;
    [Header("SteppingStone_Event")]
    public GameObject mission_175_180;
    private bool StpEventStart = false;

    private float _event_Init_Position_moonDis;
    private float _event_Init_Position_stationDis;
    public static Chapter4_Manager instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        //길사라짐 구간 초기화
        for (int i = 0; i < Blick_Materials.Length; i++)
        {
            Blick_Materials[i].SetFloat("Dissolve", 0);
        }
        blick_pos = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        RunnerPlayer1.instance.cinemachineSmoothPath = GameObject.Find("PlayerTrack").GetComponent<CinemachineSmoothPath>();
        StpEventStart = false;
        portal_Gate.SetActive(false);
        StartCoroutine(_Chapter4_Progress());
    }
    //5키로 마다 나오는 이벤트 구분
    IEnumerator _Chapter4_Progress()
    {
        Chapter4_Progress = 1000 - Game_DataManager.instance.moonDis;
        //Chapter4_Progress = 1000 - 810;
        //현재 지점에 따른 이벤트 구간
        if (Chapter4_Progress >= 150 && Chapter4_Progress < 155) Chapter4_pos_int = 1;
        else if (Chapter4_Progress >= 155 &&Chapter4_Progress < 160) Chapter4_pos_int = 2;
        else if (Chapter4_Progress >= 160 &&Chapter4_Progress < 165) Chapter4_pos_int = 3;
        else if (Chapter4_Progress >= 165 &&Chapter4_Progress < 170) Chapter4_pos_int = 4;
        else if (Chapter4_Progress >= 170 &&Chapter4_Progress < 175) Chapter4_pos_int = 5;
        else if (Chapter4_Progress >= 175 &&Chapter4_Progress < 180) Chapter4_pos_int = 6;
        else if (Chapter4_Progress >= 180 &&Chapter4_Progress < 185) Chapter4_pos_int = 7;
        else if (Chapter4_Progress >= 185 &&Chapter4_Progress < 190) Chapter4_pos_int = 8;
        else if (Chapter4_Progress >= 190 &&Chapter4_Progress < 195) Chapter4_pos_int = 9;
        else if (Chapter4_Progress >= 195 &&Chapter4_Progress < 200) Chapter4_pos_int = 10;

        EventTracks[Chapter4_pos_int - 1].SetActive(true);
        StartCoroutine(_Chapter4_event()); //맵 구간에 따른 이벤트 
        yield return null;
    }
    IEnumerator _Chapter4_event()
    {
        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        Debug.Log(col.name);
        Debug.Log(Chapter4_pos_int);
        if (col.tag.Equals("Narration"))
        {
            int num = int.Parse(col.name);
            Game_TypeWriterEffect.instance.Show_EventStoryText(num);
            //195-200 조각난 우주선 파편
            if (Chapter4_pos_int.Equals(10))
            {
                if (num.Equals(6))
                {
                    RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("SpacecraftDamage");
                }
            }
            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        //165-170 
        if (Chapter4_pos_int.Equals(4))
        {
            //갈림길 이벤트
            if (col.name.Equals("Crossroads"))
            {
                Game_TypeWriterEffect.instance.Show_EventStoryText(2);
                col.gameObject.SetActive(false);
            }

        }
        // 175-180  징검다리 이벤트
        else if (Chapter4_pos_int.Equals(6))
        {
            if (col.name.Equals("Start_SteppingStone"))
            {
                StartCoroutine(SteppingStoneRunning());
            }
            else if (col.name.Equals("End_SteppingStone"))
            {
                StopCoroutine(SteppingStoneRunning());
                StpEventStart = false;
                RunnerPlayer1.instance.StoryEventing = false;
            }
        }
        //190-195 길사라짐 이벤트
        else if (Chapter4_pos_int.Equals(9))
        {
            if (col.name.Equals("Start_PlankBlick"))
            {
                StartCoroutine(_DisappearEvent());
                StartCoroutine(_DisapeearLine());
                StartCoroutine(_RoadDisappearEvent());
                //Debug.LogError("Start_PlankBlick");
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
            }
            if (col.name.Equals("Chapter4_PlankBlick2"))
            {
                disappearValue = 0;
                blick_pos = 1;
                //Debug.Log("Chapter4_PlankBlick2 충돌");
            }
            else if (col.name.Equals("Chapter4_PlankBlick3"))
            {
                disappearValue = 0;
                blick_pos = 2;
                //Debug.Log("Chapter4_PlankBlick3 충돌");
            }
            else if (col.name.Equals("Chapter4_PlankBlick4"))
            {
                disappearValue = 0;
                blick_pos = 3;
                //Debug.Log("Chapter4_PlankBlick4 충돌");
            }
            else if (col.name.Equals("Chapter4_PlankBlick5"))
            {
                disappearValue = 0;
                blick_pos = 4;
                //Debug.Log("Chapter4_PlankBlick5 충돌");
            }
            else if (col.name.Equals("Chapter4_PlankBlick6"))
            {
                disappearValue = 0;
                blick_pos = 5;
                //Debug.Log("Chapter4_PlankBlick6 충돌");
            }
            else if (col.name.Equals("Chapter4_PlankBlick7"))
            {
                disappearValue = 0;
                blick_pos = 6;
                //Debug.Log("Chapter4_PlankBlick7 충돌");
            }
            else if (col.name.Equals("Chapter4_PlankBlick8"))
            {
                disappearValue = 0;
                blick_pos = 7;
                //Debug.Log("Chapter4_PlankBlick8 충돌");
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
    // 징검다리 달리기
    IEnumerator SteppingStoneRunning()
    {
        RunnerPlayer1.instance.StoryEventing = true;    // speed = 0;
        Game_TypeWriterEffect.instance.Show_EventStoryText(5);
        WaitForSeconds ws = new WaitForSeconds(1f);
        yield return ws;
        yield return new WaitUntil(() => !Game_TypeWriterEffect.instance.Event_ing);    // 나레이션이 끝날 때 까지 기다림
        yield return ws;

        // 미션 팝업 /////////////////////////////
        mission_175_180.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();

        //3,2,1 카운트 다운
        yield return new WaitForSeconds(3f);
        mission_175_180.SetActive(false);
        RunnerPlayer1.instance.StoryEventing = false;   // speed input
        /////////////////////////////////////////

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
    bool RunToStart = false; //달리기!
    IEnumerator _DisappearEvent()
    {
        RunnerPlayer1.instance.StoryEventing = true;
        RunToStart = false;
        mission_190_195.SetActive(true);// 미션 팝업
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3f);
        mission_190_195.SetActive(false);// 미션 팝업
        RunnerPlayer1.instance.StoryEventing = false; 

        // 초기화 위치 저장
        _event_Init_Position_moonDis = Game_DataManager.instance.moonDis + 0.01f;
        _event_Init_Position_stationDis = Game_DataManager.instance.spaceStationDis + 0.01f;

        Game_TypeWriterEffect.instance.Show_EventStoryText(4);
        float dontMoveSpeed = 10f;
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
            Debug.Log("blick_pos : " + blick_pos);
            Debug.Log("disappearValue : " + disappearValue);
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
}
