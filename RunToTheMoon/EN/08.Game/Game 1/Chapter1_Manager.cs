using Cinemachine;
using System.Collections;
using UnityEngine;

public class Chapter1_Manager : MonoBehaviour
{
    public static Chapter1_Manager instance { get; private set; }
    public GameObject[] EventTracks;
    public GameObject Radio_ob;
    //public RunnerPlayer1 playerRunner_sc;
    //public CinemachineDollyCart _PlayerDolly;
    //길 사라짐 이벤트

    [Header("Blick_Event")]
    public Material[] Blick_Materials;
    public Material FinishLine_mat;
    float time = 0; float F_time = 1; //초기화
    public GameObject mission_35_40; //길사라짐으로 인한 뛰기
    public GameObject mission_15_20; //우주라디오 이벤트 
    int blick_pos = 0;
    float disappearValue = 0;
    [Header("TutorialPopupEvent")]
    public GameObject[] Oxygen_CanisterUI;
    public GameObject[] SpaceRestAreaUI;
    public GameObject[] StarPowderUI;
    public GameObject spaceRestArea_ob;
    public float Chapter1_Progress;
    public int Chapter1_pos_int = 0;

    public bool Tutorial_ing = false;
    bool Ox_close = false, StarDust_Close = false, Ox_buy = false;

    private float _event_Init_Position_moonDis;
    private float _event_Init_Position_stationDis;

    public GameObject portal_Gate;

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
        //////////////////////////////////////////////////
    }
    void Start()
    {
        RunnerPlayer1.instance.cinemachineSmoothPath = GameObject.Find("PlayerTrack").GetComponent<CinemachineSmoothPath>();
        SoundManager.Instance.PlayBGM("4_GamePlay_Quiet");//기본 사운드
        portal_Gate.SetActive(false);
        StartCoroutine(_Chapter1_Progress());
    }
    //5키로 마다 나오는 이벤트 구분
    IEnumerator _Chapter1_Progress()
    {
        Chapter1_Progress = 1000 - Game_DataManager.instance.moonDis;

        //현재 지점에 따른 이벤트 구간
        if (Chapter1_Progress >= 0 && Chapter1_Progress < 5) Chapter1_pos_int = 1;
        else if (Chapter1_Progress >= 5 && Chapter1_Progress < 10) Chapter1_pos_int = 2;
        else if (Chapter1_Progress >= 10 && Chapter1_Progress < 15) Chapter1_pos_int = 3;
        else if (Chapter1_Progress >= 15 && Chapter1_Progress < 20) Chapter1_pos_int = 4;
        else if (Chapter1_Progress >= 20 && Chapter1_Progress < 25) Chapter1_pos_int = 5;
        else if (Chapter1_Progress >= 25 && Chapter1_Progress < 30) Chapter1_pos_int = 6;
        else if (Chapter1_Progress >= 30 && Chapter1_Progress < 35) Chapter1_pos_int = 7;
        else if (Chapter1_Progress >= 35 && Chapter1_Progress < 40) Chapter1_pos_int = 8;
        else if (Chapter1_Progress >= 40 && Chapter1_Progress < 45) Chapter1_pos_int = 9;
        else if (Chapter1_Progress >= 45 && Chapter1_Progress < 50) Chapter1_pos_int = 10;

        //Debug.Log("Chapter1_Progress" + Chapter1_Progress + " " + "moonDis" + Game_DataManager.instance.moonDis);
        EventTracks[Chapter1_pos_int - 1].SetActive(true);
        StartCoroutine(_Chapter1_event());
        yield return null;
    }
    IEnumerator _Chapter1_event()
    {
        //Debug.LogError("EventStoryText");
        if (Chapter1_pos_int < 5)
        {
            Game_TypeWriterEffect.instance.RadioEventAble = false;
        }
        while (!Game_DataManager.instance.runEndState)
        {
            if (Chapter1_pos_int.Equals(1) && Game_DataManager.instance.spaceStationDis < 0.5f)
            {
                StartCoroutine(_SpaceRestArea_Event()); //우주 정거장 도착
                Game_TypeWriterEffect.instance.Show_EventStoryText(1);
                break;
               // Debug.LogError("Show_EventStoryText(1)");
            }
            yield return null;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        //0-5구간(첫 산소통 GEt
        if (Chapter1_pos_int.Equals(1))
        {
            //산소통 줍기
            if (col.name.Equals("OxygenTank_Event1"))
            {
                StartCoroutine(_ItemBox_Event());
            }
        }
        //10-15구간(별가루 Get 튜토리얼
        else if (Chapter1_pos_int.Equals(2))
        {
            //별가루 줍기
            if (col.name.Equals("StarDust_Event1"))
            {
                StartCoroutine(_StartDust_Event());
            }
        }
        //15-20구간 (라디오 Get)
        else if (Chapter1_pos_int.Equals(4))
        {
            //라디오 이벤트
            //우주 라디오 켜기(획득 전)
            if (col.name.Equals("Radio_Event1"))
            {
                Game_TypeWriterEffect.instance.Show_EventStoryText(5);
                Radio_ob.SetActive(true);
                col.gameObject.SetActive(false); //이벤트 사라져
            }
            //우주라디오 주움
            else if (col.name.Equals("Radio_Event2"))
            {
                //하고싶은것들
                StartCoroutine(_Radio_StarDust_evnet());
                Radio_ob.SetActive(false);
            }
        }

        if (col.tag.Equals("Narration"))
        {
            int num = int.Parse(col.name);
            Game_TypeWriterEffect.instance.Show_EventStoryText(num);
            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        if (col.name.Equals("Start_PlankBlick"))
        {
            StartCoroutine(_DisappearEvent());
            StartCoroutine(_DisapeearLine());
            StartCoroutine(_RoadDisappearEvent());
           // Debug.LogError("Start_PlankBlick");
        }
        else if (col.name.Equals("End_PlankBlick"))
        {
            Game_TypeWriterEffect.instance.Show_EventStoryText(10);
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
        if (col.name.Equals("Chapter1_PlankBlick2"))
        {
            disappearValue = 0;
            blick_pos = 1;
        }
        else if (col.name.Equals("Chapter1_PlankBlick3"))
        {
            disappearValue = 0;
            blick_pos = 2;
        }
        else if (col.name.Equals("Chapter1_PlankBlick4"))
        {
            disappearValue = 0;
            blick_pos = 3;
        }
        else if (col.name.Equals("Chapter1_PlankBlick5"))
        {
            disappearValue = 0;
            blick_pos = 4;
        }
        else if (col.name.Equals("Chapter1_PlankBlick6"))
        {
            disappearValue = 0;
            blick_pos = 5;
        }
        else if (col.name.Equals("Chapter1_PlankBlick7"))
        {
            disappearValue = 0;
            blick_pos = 6;
        }
        //별가루 줍기
        if (col.name.Equals("StarDust_Event1"))
        {
            StartCoroutine(_StartDust_Event());
        }

        if (col.name.Equals("GoalPos"))
        {
            if (RunnerPlayer1.instance.playerPosition > RunnerPlayer1.instance.cinemachineSmoothPath.PathLength)
            {
                portal_Gate.SetActive(true);
            }
        }
    }
    //튜토리얼 상태 체크
    //마치기 버튼으로 튜토리얼 완료 확인
    public void Tutorial_SetActive(GameObject ob)
    {
        if (Tutorial_ing.Equals(true))
        {
            if (ob.name.Equals("ItemBoxTutorialEnd"))
            {
                RunnerPlayer1.instance.StoryEventing = false; //다시 움직여
                Ox_close = true;

            }
            else if (ob.name.Equals("StarPowderTutorialEnd"))
            {
                RunnerPlayer1.instance.StoryEventing = false; //다시 움직여
                StarDust_Close = true;

            }
        }
    }
    //산소통 획득 튜토리얼
    IEnumerator _ItemBox_Event()
    {
        //나중에 지우기
       
        Game_TypeWriterEffect.instance.Show_EventStoryText(2);
        //산소통 튜토리얼을 안했을 경우, 튜토리얼 시작
        if (!PlayerPrefs.HasKey("Oxygen_tutorial"))
        {
            RunnerPlayer1.instance.StoryEventing = true;
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false); //나레이션 끝나고 
            yield return new WaitForSeconds(1f);
            Tutorial_ing = true;
      
            //튜토리얼 시작합니다.
            Game_UIManager.instance.BlurCameraCanvas_ob.SetActive(true);
            Game_UIManager.instance.StroyOb_Background.SetActive(false);
            Oxygen_CanisterUI[0].SetActive(true);
            O2Timer.instance.SmallItemUse_OneTimePlus();
            RunnerPlayer1.instance.Use_Oxygen_Small();
            yield return new WaitUntil(() => Ox_close == true);
            Oxygen_CanisterUI[5].SetActive(false); //튜토리얼 끝!@
            Tutorial_ing = false;
            Game_UIManager.instance.StroyOb_Background.SetActive(true);
            Time.timeScale = 1;
            PlayerPrefs.SetString("Oxygen_tutorial", "True");
            yield return null;
        }
        //튜토리얼을 이미했을 경우에는 아이템 설명을 해줌
        else
        {
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false); //나레이션 끝나고 
            yield return new WaitForSeconds(1.2f);
            //작은 산소통
            RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("OxygenTank_small_New");
        }
        yield return null;
    }
    // 10-15 별가루 획득 튜토리얼
    IEnumerator _StartDust_Event()
    {
        //    PlayerPrefs.DeleteKey("StartDust_tutorial");
        Debug.LogError("PlayerPrefs" + PlayerPrefs.HasKey("StartDust_tutorial"));
        Game_TypeWriterEffect.instance.Show_EventStoryText(3); // 별가루 획득 이벤트 텍스트
        //별가루 튜토리얼을 안했을 경우, 튜토리얼 시작
        if (!PlayerPrefs.HasKey("StartDust_tutorial"))
        {
            RunnerPlayer1.instance.StoryEventing = true;
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false); //나레이션 끝나고 
            yield return new WaitForSeconds(1.2f);
            Tutorial_ing = true;

            //튜토리얼 시작합니다.
            Game_UIManager.instance.BlurCameraCanvas_ob.SetActive(true);
            Game_UIManager.instance.StroyOb_Background.SetActive(false);
            StarPowderUI[0].SetActive(true);

            yield return new WaitUntil(() => StarDust_Close == true);
            StarPowderUI[5].SetActive(false); //튜토리얼 끝!@
            Tutorial_ing = false;
            Time.timeScale = 1;
            Game_UIManager.instance.StroyOb_Background.SetActive(true);
            PlayerPrefs.SetString("StartDust_tutorial", "True");
            RunnerPlayer1.instance.Use_StarDust();
            RunnerPlayer1.instance.StoryEventing = false;
        }
        else
        {
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false); //나레이션 끝나고 
            yield return new WaitForSeconds(1f);
            //별가루
            RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("StarDust_New");
        
        }
        yield return null;

    }
    //우주 정거장 튜토리얼
    IEnumerator _SpaceRestArea_Event()
    {
        if (Chapter1_pos_int.Equals(1))
        {
            if (!PlayerPrefs.HasKey("SpaceRestArea_tutorial"))
            {
                yield return new WaitUntil(() => spaceRestArea_ob.activeSelf.Equals(true));
                //튜토리얼 시작합니다.
                SpaceRestAreaUI[0].SetActive(true);
            }
            else
            {
                PlayerPrefs.SetString("SpaceRestArea_tutorial", "True");
            }
        }
        yield return null;
    }
    //라디오에 달가루 뿌리기 이벤트 후 나레이션
    IEnumerator _Radio_StarDust_evnet()
    {
        RunnerPlayer1.instance.StoryEventing = true;
        RunnerPlayer1.instance.moonPowder_useEvnet = true; //달가루를 사용해야하는 구간인 경우
        Game_TypeWriterEffect.instance.Show_EventStoryText(6);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        mission_15_20.SetActive(true);// 미션 팝업
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3);
        mission_15_20.SetActive(false);

        float waitTiem = 6f;
        //5초 이상 달가루를 사용하지 않으면 "달가루를 사용해줘" 사인 보내기
        while(waitTiem>-1)
        {
            Debug.Log(waitTiem);
            waitTiem -= Time.deltaTime;
            if (!RunnerPlayer1.instance.use_moonPowder && waitTiem<=0)
            {
                Game_TypeWriterEffect.instance.Show_TypingText("Take some moon powder out of the bag and try it!");
                waitTiem = 8f; //초기화
            }
            if (RunnerPlayer1.instance.use_moonPowder)
            {
                break;
            }
            yield return null;
        }
        //달빛 가루 뿌렸을 경우,
        yield return new WaitUntil(() => RunnerPlayer1.instance.use_moonPowder == true);
        SoundFunction.Instance.Mission_End_Sound();
        Game_TypeWriterEffect.instance.Show_EventStoryText(7); //우주 라디오 켜기(행동이후)
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("SpaceRadio");//우주 라디오
        RunnerPlayer1.instance.StoryEventing = false;
        //달가루 이벤트
        RunnerPlayer1.instance.use_moonPowder = false; //달가루 사용 여부 초기화, 파티클 멈춤 
        RunnerPlayer1.instance.moonPowder_useEvnet = false;
        yield return null;
    }
    //길 사라짐 이벤트 구간!
    bool RunToStart = false; //달리기!
    IEnumerator _DisappearEvent()
    {
        RunnerPlayer1.instance.StoryEventing = true;
        RunToStart = false;
        mission_35_40.SetActive(true);// 미션 팝업
        SoundFunction.Instance.Show_Mission_Popup();
        //3,2,1 카운트 다운
        yield return new WaitForSeconds(3);
        mission_35_40.SetActive(false);// 미션 팝업
        RunnerPlayer1.instance.StoryEventing = false;
        // 초기화 위치 저장
        _event_Init_Position_moonDis = Game_DataManager.instance.moonDis + 0.01f;
        _event_Init_Position_stationDis = Game_DataManager.instance.spaceStationDis + 0.01f;

        Game_TypeWriterEffect.instance.Show_EventStoryText(9);
        RunToStart = true;
        float dontMoveSpeed = 10;
        float lefttime = 2f;
        while (RunToStart.Equals(true))
        {
            //2021-11-18일 수정
            if (RunnerPlayer1.instance.input_Speed == 0 || SpeedMeter.instance.velocity_f < 20)
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
       // Debug.Log(disappearValue + " disappearValue" + " blick_pos" + blick_pos);
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
}

