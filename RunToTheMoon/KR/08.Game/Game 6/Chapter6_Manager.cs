using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Chapter6_Manager : MonoBehaviour
{
    public static Chapter6_Manager instance { get; private set; }

    public GameObject[] EventTracks;
    public GameObject Oxygen_ob_290_295;
    public GameObject portal_Gate;

    [Header("Blick_Event")]
    public Material[] Blick_Materials;
    public Material FinishLine_mat;
    float time = 0; float F_time = 1; //초기화
    public GameObject mission_260_265;
    int blick_pos = 0;
    float disappearValue = 0;
    [Header("Broken oxygen tank")]
     public GameObject mission_290_295;

    [Header("SteppingStone_Event")]
    public GameObject mission_275_280;
    private bool StpEventStart = false;

    //바닥 컬러 007FFF 000000
    public Material[] Plank6_mat;
  //  public Renderer[] Plank6_render;
    public GameObject FlashLight_ob; //손전등

    float speed =1f;
    public Color startColor, endColor;
    public float Chapter6_Progress;
    public int Chapter6_pos_int = 0;

    private float _event_Init_Position_moonDis;
    private float _event_Init_Position_stationDis;
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
    private void Start()
    {
        RunnerPlayer1.instance.cinemachineSmoothPath = GameObject.Find("PlayerTrack").GetComponent<CinemachineSmoothPath>();
        Plank6_mat[0].color = startColor; //직선길
        Plank6_mat[1].color = startColor; //갈림길
        SoundManager.Instance.PlayBGM("6_GamePlay_Horror"); //뮤서운 사운드!
        portal_Gate.SetActive(false);
        StartCoroutine(_Chapter6_Progress());
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        if (col.tag.Equals("Narration"))
        {
            int num = int.Parse(col.name);
            Game_TypeWriterEffect.instance.Show_EventStoryText(num);
            //290_295 구간 일경우
            if (Chapter6_pos_int.Equals(9))
            {
                //산소통이 구멍, 이벤트 발생
                if (num.Equals(6))
                {
                    StartCoroutine(_BrokenOX_Evnet());
                }
            }
            //누군가의 편지
            if (num.Equals(5))
            {
                StartCoroutine(_Find_Letter());
            }
            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        //260-265
        if (Chapter6_pos_int.Equals(3))
        {
            //길사라짐
            if (col.name.Equals("Start_PlankBlick"))
            {
                SoundManager.Instance.PlayBGM("5_GamePlay2_Urgent"); //긴박
                StartCoroutine(_DisappearEvent());
                StartCoroutine(_DisapeearLine());
                StartCoroutine(_RoadDisappearEvent());
                Debug.LogError("Start_PlankBlick");
            }
            else if (col.name.Equals("End_PlankBlick"))
            {
                SoundManager.Instance.PlayBGM("6_GamePlay_Horror"); //뮤서운 사운드!
                // 길갈라짐 오브젝트 초기화
                for (int i = 0; i < Blick_Materials.Length; i++)
                {
                    Blick_Materials[i].SetFloat("Dissolve", 0);
                }
                blick_pos = 0;
                disappearValue = 0;
                RunToStart = false; //달리기!
            }
            if (col.name.Equals("Chapter6_PlankBlick2"))
            {
                disappearValue = 0;
                blick_pos = 1;
            }
            else if (col.name.Equals("Chapter6_PlankBlick3"))
            {
                disappearValue = 0;
                blick_pos = 2;
            }
            else if (col.name.Equals("Chapter6_PlankBlick4"))
            {
                disappearValue = 0;
                blick_pos = 3;
            }
            else if (col.name.Equals("Chapter6_PlankBlick5"))
            {
                disappearValue = 0;
                blick_pos = 4;
            }
            else if (col.name.Equals("Chapter6_PlankBlick6"))
            {
                disappearValue = 0;
                blick_pos = 5;
            }
            else if (col.name.Equals("Chapter6_PlankBlick7"))
            {
                disappearValue = 0;
                blick_pos = 6;
            }
        }
        // 275-280  징검다리 이벤트
        else if (Chapter6_pos_int.Equals(6))
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
        //290-295 산소통 구멍남
        else if (Chapter6_pos_int.Equals(9))
        {
            //6번 콜라이더랑 부딪히면 나레이션 나옴
            //다시 현재 위치에 왔을 때, 대형 산소통 하나더 뿌려줌
            if (col.name.Equals("6Position"))
            {
                Oxygen_ob_290_295.SetActive(true);
            }
        }
        else if (Chapter6_pos_int.Equals(4))
        {

            //손전등 주웠을 떄
            if (col.name.Equals("FlashLigth"))
            {
                //애니메이션 완료 후 실행되는 부분
                RunnerPlayer1.instance.anim.SetBool("Idle 2", false);
                RunnerPlayer1.instance.anim.SetBool("Idle 3", false);
                RunnerPlayer1.instance.anim.SetBool("Look_Behind", false);

                RunnerPlayer1.instance.speed = 0f;
                RunnerPlayer1.instance.anim.SetBool("Pickup", true);

                RunnerPlayer1.instance.CheckAnimationState();   // Anim State Check
                col.SetActive(false);
                Game_TypeWriterEffect.instance.Show_EventStoryText(3);
                //손전등 주웠을 경우, 바닥의 길 색상 변경
                StartCoroutine(TurnOn_FlashColor()); //바닥색상변경
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
        Game_TypeWriterEffect.instance.Show_EventStoryText(7);       
        RunnerPlayer1.instance.StoryEventing = true;    // speed = 0;
        WaitForSeconds ws = new WaitForSeconds(1f);
        yield return ws;
        yield return new WaitUntil(() => !Game_TypeWriterEffect.instance.Event_ing);    // 나레이션이 끝날 때 까지 기다림
        yield return ws;

        // 미션 팝업 /////////////////////////////
        mission_275_280.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();

        //3,2,1 카운트 다운
        yield return new WaitForSeconds(3f);
        mission_275_280.SetActive(false);
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

    IEnumerator _BrokenOX_Evnet()
    { 
        RunnerPlayer1.instance.StoryEventing = true;
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false); //나레이션 끝나면 
        mission_290_295.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
      
        yield return new WaitForSeconds(3f);
        mission_290_295.SetActive(false);
        RunnerPlayer1.instance.StoryEventing = false;
        RunnerPlayer1.instance.O2_eventCheck = true; //산소 부족 구간 시작!
    }
    IEnumerator _Find_Letter()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("Letter");
        yield return null;
    }
    //5키로 마다 나오는 이벤트 구분
    IEnumerator _Chapter6_Progress()
    {
        Chapter6_Progress = 1000 - Game_DataManager.instance.moonDis;
        //Chapter6_Progress = 1000 - 710;

        //현재 지점에 따른 이벤트 구간
        if (Chapter6_Progress >= 250 && Chapter6_Progress < 255) Chapter6_pos_int = 1;
        else if (Chapter6_Progress >= 255 && Chapter6_Progress < 260) Chapter6_pos_int = 2;
        else if (Chapter6_Progress >= 260 && Chapter6_Progress < 265) Chapter6_pos_int = 3;//길사라짐
        else if (Chapter6_Progress >= 265 && Chapter6_Progress < 270) Chapter6_pos_int = 4;//손전등
        else if (Chapter6_Progress >= 270 && Chapter6_Progress < 275) Chapter6_pos_int = 5; //비상식량
        else if (Chapter6_Progress >= 275 && Chapter6_Progress < 280) Chapter6_pos_int = 6; // 징검다리
        else if (Chapter6_Progress >= 280 && Chapter6_Progress < 285) Chapter6_pos_int = 7;
        else if (Chapter6_Progress >= 285 && Chapter6_Progress < 290) Chapter6_pos_int = 8;
        else if (Chapter6_Progress >= 290 && Chapter6_Progress < 295) Chapter6_pos_int = 9; // 산소통 이벤트
        else if (Chapter6_Progress >= 295 && Chapter6_Progress < 300) Chapter6_pos_int = 10;

        EventTracks[Chapter6_pos_int - 1].SetActive(true);
        StartCoroutine(_Chapter6_event()); //맵 구간에 따른 이벤트 
        yield return null;
    }
    IEnumerator _Chapter6_event()
    {
        //손전등 주웠음
        if (Chapter6_pos_int > 4)
        {
            FlashLight_ob.SetActive(true); //손전등 켜기
            for(int i =0; i< Plank6_mat.Length; i++)
            {
                Plank6_mat[i].color = endColor;
            }
         /*   Plank6_mat[0].color = endColor; //직선길
            Plank6_mat[1].color = endColor; //갈림길
            Plank6_mat[2].color = endColor; 
            Plank6_mat[3].color = endColor; */
            FlashLight_ob.SetActive(true); //손전등 켜기
        }
        yield return null;
    }

    private IEnumerator TurnOn_FlashColor()
    {
        yield return new WaitForSeconds(5f);
        FlashLight_ob.SetActive(true); //손전등 켜기
        RunnerPlayer1.instance.anim.SetBool("Grab_Flash", true);
        float tick = 0;
        for (int i = 0; i < Plank6_mat.Length; i++)
        {
            Plank6_mat[i].color = endColor;
        }
        string color_cod_Start = ColorUtility.ToHtmlStringRGBA(Plank6_mat[0].color);
        string color_cod_End = ColorUtility.ToHtmlStringRGBA(endColor);
        while (color_cod_Start != color_cod_End)
        {
            color_cod_Start = ColorUtility.ToHtmlStringRGBA(Plank6_mat[0].color);
            tick += Time.deltaTime * speed;
            Plank6_mat[0].color = Color.Lerp(startColor, endColor, tick);
            Plank6_mat[1].color = Color.Lerp(startColor, endColor, tick);
            yield return null;

        }
        RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("Flashlight"); //아이템 설명
        yield return null;
    }
    //길 사라짐 이벤트 구간!
    bool RunToStart = false; //달리기!
    IEnumerator _DisappearEvent()
    {        
        RunnerPlayer1.instance.StoryEventing = true;
        RunToStart = false;
        mission_260_265.SetActive(true);// 미션 팝업
        SoundFunction.Instance.Show_Mission_Popup();

        yield return new WaitForSeconds(3);
        mission_260_265.SetActive(false);// 미션 팝업
        RunnerPlayer1.instance.StoryEventing = false;
        // 초기화 위치 저장
        _event_Init_Position_moonDis = Game_DataManager.instance.moonDis + 0.01f;
        _event_Init_Position_stationDis = Game_DataManager.instance.spaceStationDis + 0.01f;

        Game_TypeWriterEffect.instance.Show_EventStoryText(4);
        RunToStart = true;
        float dontMoveSpeed = 10;
        float lefttime = 2f;
        while (RunToStart.Equals(true))
        {
            if (SpeedMeter.instance.velocity_f<18)
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


}

