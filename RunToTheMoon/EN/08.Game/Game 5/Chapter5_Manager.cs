using Cinemachine;
using System.Collections;
using UnityEngine;


public class Chapter5_Manager : MonoBehaviour
{
    public static Chapter5_Manager instance { get; private set; }

    public GameObject[] EventTracks;
    public GameObject BabyStar;
    public GameObject BabyStar_fake;
    public GameObject MotherStar;
    public GameObject MotherStar_Baby;

    public CinemachineDollyCart _PlayerDolly;
    public CameraViewPort _cameraView;
    public RunnerPlayer1 playerRunner_sc;
    public GameObject portal_Gate;

    [Header("Blick_Event")]
    public Material[] Blick_Materials;
    public Material FinishLine_mat;
    float time = 0; float F_time = 1; //초기화
    public GameObject mission_200_205; //아기별의 엄마를 찾아주자
    public GameObject mission_220_225; //길사라짐으로 인한 뛰기
    public GameObject mission_245_250; //빨리가자
    int blick_pos = 0;
    float disappearValue = 0;

    [Header("Pice")]
    public GameObject ParticleOB;
    public GameObject BaseFx;
    public GameObject BaseFx_Flare;
    public GameObject ObjectFx;

    public float Chapter5_Progress;
    public int Chapter5_pos_int = 0;

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
        portal_Gate.SetActive(false);
        StartCoroutine(_Chapter5_Progress());
    }
    //Chapter 5 : 길 잃은 아기별(200-250)
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        if (col.tag.Equals("Narration"))
        {
            int num = int.Parse(col.name);
            Game_TypeWriterEffect.instance.Show_EventStoryText(num);
            //미츄와의 만남
            if (num.Equals(2))
            {
                StartCoroutine(_Meet_BabyStar());
            }
            else if (num.Equals(5))
            {
                StartCoroutine(_Meet_MotherStar());
            }
            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        if (Chapter5_pos_int.Equals(5))
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
            }
            if (col.name.Equals("Chapter5_PlankBlick2"))
            {
                disappearValue = 0;
                blick_pos = 1;
            }
            else if (col.name.Equals("Chapter5_PlankBlick3"))
            {
                disappearValue = 0;
                blick_pos = 2;
            }
            else if (col.name.Equals("Chapter5_PlankBlick4"))
            {
                disappearValue = 0;
                blick_pos = 3;
            }
            else if (col.name.Equals("Chapter5_PlankBlick5"))
            {
                disappearValue = 0;
                blick_pos = 4;
            }
            else if (col.name.Equals("Chapter5_PlankBlick6"))
            {
                disappearValue = 0;
                blick_pos = 5;
            }
            else if (col.name.Equals("Chapter5_PlankBlick7"))
            {
                disappearValue = 0;
                blick_pos = 6;
            }
            else if (col.name.Equals("Chapter5_PlankBlick8"))
            {
                disappearValue = 0;
                blick_pos = 7;
            }
        }
        if (col.name.Equals("MoonPices_LightGreen"))
        {
            RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("MoonPices_LightGreen");
            Game_DataManager.instance.CollectionNumber = 2; //달의 조각 획득! 
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
    IEnumerator _Chapter5_Progress()
    {
        Chapter5_Progress = 1000 - Game_DataManager.instance.moonDis;
        //Chapter5_Progress = 1000 - 755;

        //현재 지점에 따른 이벤트 구간
        if (Chapter5_Progress >= 200 && Chapter5_Progress < 205) Chapter5_pos_int = 1;  // 아기별, 길사라짐
        else if (Chapter5_Progress >= 205 && Chapter5_Progress < 210) Chapter5_pos_int = 2;
        else if (Chapter5_Progress >= 210 && Chapter5_Progress < 215) Chapter5_pos_int = 3;
        else if (Chapter5_Progress >= 215 && Chapter5_Progress < 220) Chapter5_pos_int = 4;
        else if (Chapter5_Progress >= 220 && Chapter5_Progress < 225) Chapter5_pos_int = 5;
        else if (Chapter5_Progress >= 225 && Chapter5_Progress < 230) Chapter5_pos_int = 6;
        else if (Chapter5_Progress >= 230 && Chapter5_Progress < 235) Chapter5_pos_int = 7;
        else if (Chapter5_Progress >= 235 && Chapter5_Progress < 240) Chapter5_pos_int = 8;
        else if (Chapter5_Progress >= 240 && Chapter5_Progress < 245) Chapter5_pos_int = 9;
        else if (Chapter5_Progress >= 245 && Chapter5_Progress < 250) Chapter5_pos_int = 10;    // 미츄's 마더, 별가루

        EventTracks[Chapter5_pos_int - 1].SetActive(true);
        //미츄 활성화
        if (Chapter5_pos_int > 1)
        {
            BabyStar.SetActive(true);
        }
        else
        {
            BabyStar.SetActive(false);
        }
        Debug.LogError("Chapter5_Progress" + Chapter5_Progress);
        Debug.LogError("Chapter5_pos_int" + Chapter5_pos_int);
        yield return null;
    }
    //길 사라짐 이벤트 구간!
    bool RunToStart = false; //달리기!
    IEnumerator _DisappearEvent()
    {
        RunnerPlayer1.instance.StoryEventing = true;
        RunToStart = false;
        //////////////////////////////////////////////////////////////////////////////////////
        mission_220_225.SetActive(true);// 미션 팝업
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3f);
        mission_220_225.SetActive(false);// 미션 팝업
        RunnerPlayer1.instance.StoryEventing = false;
        ////////////////////////////////////////////////////////////////////////////////////////////

        // 초기화 위치 저장
        _event_Init_Position_moonDis = Game_DataManager.instance.moonDis + 0.01f;
        _event_Init_Position_stationDis = Game_DataManager.instance.spaceStationDis + 0.01f;
        float dontMoveSpeed = 10;
        float lefttime = 2f;
        Game_TypeWriterEffect.instance.Show_EventStoryText(4);
        RunToStart = true;
        while (RunToStart.Equals(true))
        {
            if (SpeedMeter.instance.velocity_f<18f)
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
    //미츄 만남!
    IEnumerator _Meet_BabyStar()
    {
        RunnerPlayer1.instance.StoryEventing = true;
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        //나레이션이 끝날때 까지 움직이지 않음
        //////////////////////////////////////////////////////////////////////////////////////
        mission_200_205.SetActive(true);// 미션 팝업
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3f);
        mission_200_205.SetActive(false);// 미션 팝업
       ////////////////////////////////////////////////////////////////////////////////////////////
        yield return new WaitUntil(() => mission_200_205.activeSelf == false); //팝업이 사라지면
        yield return new WaitForSeconds(1f);
        _cameraView.vCame[3].m_Priority = 12;
        //함께가자~!
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        Game_UIManager.instance.Fade_in();
        yield return new WaitForSeconds(1f);
        BabyStar_fake.SetActive(false); //정면 미츄 사라짐
        _cameraView.vCame[3].m_Priority = 10;
        BabyStar.SetActive(true);
        Game_UIManager.instance.Fade_out();
        yield return new WaitForSeconds(1f);

        RunnerPlayer1.instance.StoryEventing = false;
        yield return null;

    }
    //미츄 엄마 만나!
    IEnumerator _Meet_MotherStar()
    {
        yield return new WaitForSeconds(1f);
        //나레이션이 끝날때 까지 움직이지 않음
        RunnerPlayer1.instance.StoryEventing = true;
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        yield return new WaitForSeconds(1f);
        Game_UIManager.instance.Fade_in();
        yield return new WaitForSeconds(1f);
        MotherStar.SetActive(true);
        BabyStar.SetActive(false);
        yield return new WaitForSeconds(1.2f);
        Game_UIManager.instance.Fade_out();
        Game_TypeWriterEffect.instance.Show_EventStoryText(5); //엄마별 만나기(후)
        _cameraView.vCame[3].m_Priority = 12;
        yield return new WaitForSeconds(1.2f);

        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);

        //나레이션이 끝날때 까지 움직이지 않음
        RunnerPlayer1.instance.StoryEventing = true;
        Game_UIManager.instance.Fade_in();
        yield return new WaitForSeconds(1.2f);
        MotherStar.SetActive(false);
        _cameraView.vCame[3].m_Priority = 10;
        StartCoroutine(_Start_DropPice());
        yield return new WaitForSeconds(1f);
        Game_UIManager.instance.Fade_out();
        Game_TypeWriterEffect.instance.Show_EventStoryText(7); //달가루 사용 유도
     
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        StartCoroutine(_Use_MoonPowder_Evnet());
        yield return null;

    }
    IEnumerator _Use_MoonPowder_Evnet()
    {
        RunnerPlayer1.instance.moonPowder_useEvnet = true; //달가루 사용 가능
        mission_245_250.SetActive(true);//미션 팝업
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3f);
        mission_245_250.SetActive(false);
        float waitTiem = 6f;
        //5초 이상 달가루를 사용하지 않으면 "달가루를 사용해줘" 사인 보내기
        while (waitTiem > -1)
        {
            Debug.Log(waitTiem);
            waitTiem -= Time.deltaTime;
            if (!RunnerPlayer1.instance.use_moonPowder && waitTiem <= 0)
            {
                Game_TypeWriterEffect.instance.Show_TypingText("Take the moon powder out of the bag and try it!");
                waitTiem = 8f; //초기화
            }
            if (RunnerPlayer1.instance.use_moonPowder)
            {
                break;
            }
            yield return null;
        }
        //달가루 사용하면 넘어감
        yield return new WaitUntil(() => RunnerPlayer1.instance.use_moonPowder == true);
        RunnerPlayer1.instance.StoryEventing = false;
        RunnerPlayer1.instance.moonPowder_useEvnet = false; //사용하고 다시 사용못하도록 막음
        yield return new WaitForSeconds(3f);
        RunnerPlayer1.instance.use_moonPowder = false; //달가루 사용 여부 초기화, 파티클 멈춤 
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

