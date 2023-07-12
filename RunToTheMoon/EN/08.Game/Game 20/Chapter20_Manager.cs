using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Chapter20_Manager : MonoBehaviour
{
    public static Chapter20_Manager instance { get; private set; }
    public GameObject portal_Gate;
    public GameObject[] EventTracks;
    public CameraViewPort cameraViewPort;
    public GameObject moonOb;
    public Camera moonOb_camera;
    public float Chapter20_Progress;
    public int Chapter20_pos_int = 0;

    //995-1000 달의 조각문 이벤트
    public GameObject GatePice; //문에 장식된 달의 조각
    public Animator Gate_open_ani;
    public GameObject MoonPices_Black_ob;

    public RuneController rune_ctrl;
    public Animator FocusDoorAni;

    public GameObject hallofGates;
    public GameObject mission_975_980; //산통 구멍
    public GameObject mission_995_1000; //고장난 우주라디오


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
        //카메라 뷰 초기화
      //  RunnerPlayer1.instance._cameraView.vCame[0].GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = 1.4f;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        Debug.Log(col.name);
        if (col.tag.Equals("Narration"))
        {
            int num = int.Parse(col.name);
            Game_TypeWriterEffect.instance.Show_EventStoryText(num);
            //달의 조각문 이벤트 
            if (num.Equals(3))
            {
                StartCoroutine(_Reach_MoonSculptureGate());
            }

            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        if (col.name.Equals("OxygenBroken"))
        {
            StartCoroutine(_OxgenBorkenEvent()); //산소 부족 이벤트
            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        if (col.name.Equals("Wait"))
        {
            StartCoroutine(_GateEvent_Start());

            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        if (col.name.Equals("MoonPices_Black"))
        {
            RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("MoonPices_Black");//조각 설명
            Game_DataManager.instance.CollectionNumber = 10;

            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }

        if (col.name.Equals("GoalPos") && Chapter20_pos_int != 10)
        {
            if (RunnerPlayer1.instance.playerPosition > RunnerPlayer1.instance.cinemachineSmoothPath.PathLength)
            {
                portal_Gate.SetActive(true);
            }
        }
        else if(col.name.Equals("GoalPos") && Chapter20_pos_int == 10)
        {
            if (RunnerPlayer1.instance.playerPosition > RunnerPlayer1.instance.cinemachineSmoothPath.PathLength)
            {
                hallofGates.SetActive(true);
            }
        }

    }
    private void Start()
    {
        RunnerPlayer1.instance.cinemachineSmoothPath = GameObject.Find("PlayerTrack").GetComponent<CinemachineSmoothPath>();
        portal_Gate.SetActive(false);
        hallofGates.SetActive(false);
        StartCoroutine(_Chapter20_Progress());
        StartCoroutine(_MoonMove_CameraEvent());
    }

    //5키로 마다 나오는 이벤트 구분
    IEnumerator _Chapter20_Progress()
    {
        Chapter20_Progress = 1000 - Game_DataManager.instance.moonDis;

        //현재 지점에 따른 이벤트 구간
        if (Chapter20_Progress >= 950 && Chapter20_Progress < 955) Chapter20_pos_int = 1; //비상식량
        else if (Chapter20_Progress >= 955 && Chapter20_Progress < 960) Chapter20_pos_int = 2;
        else if (Chapter20_Progress >= 960 && Chapter20_Progress < 965) Chapter20_pos_int = 3; //갈림길
        else if (Chapter20_Progress >= 965 && Chapter20_Progress < 970) Chapter20_pos_int = 4;
        else if (Chapter20_Progress >= 970 && Chapter20_Progress < 975) Chapter20_pos_int = 5;
        else if (Chapter20_Progress >= 975 && Chapter20_Progress < 980) Chapter20_pos_int = 6; //산소통 구멍
        else if (Chapter20_Progress >= 980 && Chapter20_Progress < 985) Chapter20_pos_int = 7;
        else if (Chapter20_Progress >= 985 && Chapter20_Progress < 990) Chapter20_pos_int = 8; //비어있는 별가루
        else if (Chapter20_Progress >= 990 && Chapter20_Progress < 995) Chapter20_pos_int = 9;  //별가루
        else if (Chapter20_Progress >= 995 && Chapter20_Progress < 1000) Chapter20_pos_int = 10; //마지막 이벤트

        Debug.Log("Chapter20_pos_int : " + Chapter20_pos_int);

        EventTracks[Chapter20_pos_int - 1].SetActive(true);

        yield return null;
    }
    IEnumerator _MoonMove_CameraEvent()
    {

        while (Game_DataManager.instance.runEndState.Equals(false))
        {
            //달 회전
            if (RunnerPlayer1.instance.input_Speed > 0)
            {
                moonOb.GetComponent<Transform>().transform.RotateAround(moonOb_camera.transform.position, Vector3.down, Time.deltaTime* RunnerPlayer1.instance.input_Speed*0.01f);
                moonOb.GetComponent<Transform>().transform.Rotate(new Vector3(0, -1f, 0));
            }
            yield return null;
        }
        yield return null;

    }
    //달의 조각문 앞에 도착했을 때, 이벤트
    IEnumerator _Reach_MoonSculptureGate()
    {
        RunnerPlayer1.instance.StoryEventing = true; //플레이어 멈춰!
        RunnerPlayer1.instance.playerCart.m_Speed = 0;
        yield return new WaitForSeconds(2f);
     /*  var orbital = RunnerPlayer1.instance._cameraView.vCame[0].GetCinemachineComponent<CinemachineTransposer>();
        while (orbital.m_FollowOffset.y > 0.5)
        {
            orbital.m_FollowOffset.y = Mathf.Lerp(1.4f, 0.5f, 5f);
            yield return null;
        }*/
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority=12;
        FocusDoorAni.SetTrigger("focus");
        float viewT = 5f;
        while (viewT > 0 && Game_TypeWriterEffect.instance.Event_ing.Equals(true))
        {
            viewT -= Time.deltaTime;
            yield return null;
        }
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority = 10;
        // Debug.Log("  orbital.m_FollowOffset.y" + orbital.m_FollowOffset.y);
     /*   while (orbital.m_FollowOffset.y > 0.5)
        {
            orbital.m_FollowOffset.y = Mathf.Lerp(1.4f, 0.5f, 5f);
            yield return null;
        }*/
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority = 10;
/*        while (orbital.m_FollowOffset.y < 1.4f)
        {
            orbital.m_FollowOffset.y = Mathf.Lerp(0.5f, 1.4f, 5f);
            yield return null;
        }*/
        Game_TypeWriterEffect.instance.Show_EventStoryText(4); //라디오가 왜이래!
        /////////////////////// 5번 팔을 흔들면 넘어갑니다. //////////////////////
        mission_995_1000.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3f);
        mission_995_1000.SetActive(false);
        ///////////////////////////////////////////////////////////////////////
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        Game_UIManager.instance.Fade_in();
        //콰직! 사운드 들어가기
        MoonPices_Black_ob.SetActive(true);
        yield return new WaitForSeconds(2f);
        Game_UIManager.instance.Fade_out();

        Game_TypeWriterEffect.instance.Show_EventStoryText(5); //콰직!
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        Game_TypeWriterEffect.instance.Stop_RadioEvnet();//라디오 이벤트 멈춰!
        RunnerPlayer1.instance.StoryEventing = false; //플레이어 멈춰!
        yield return null;
    }
    //룬과의 이별-> 조각 끼우기->문열림
    IEnumerator _GateEvent_Start()
    {
        RunnerPlayer1.instance.StoryEventing = true; //플레이어 멈춰!
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false); //룬 함께해줘서 고마워!
        Game_TypeWriterEffect.instance.Show_EventStoryText(6); // 룬 고마워!
        rune_ctrl.Stop_Random_CheckAnimationState();
        //룬과의 이별
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false); //룬 함께해줘서 고마워!
        Game_TypeWriterEffect.instance.Show_EventStoryText(7); // 조각함 버튼 누르자!
        Gate_open_ani.SetTrigger("gateOpen");
        //영상 나옴!
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false); //룬 함께해줘서 고마워!
        yield return new WaitForSeconds(2f);
        RunnerPlayer1.instance.StoryEventing =false;
        //문 통과하면 명예의 전당으로 이동!
        yield return null;
    }
    void OrbitAround()
    {
        moonOb.GetComponent<Transform>().transform.RotateAround(moonOb_camera.transform.position, Vector3.down,Time.deltaTime);
    }
    IEnumerator _OxgenBorkenEvent()
    {
        yield return new WaitForSeconds(1f);
        RunnerPlayer1.instance.StoryEventing = true;
        Game_TypeWriterEffect.instance.Show_EventStoryText(9); //산소통 구멍
        yield return new WaitUntil(()=>Game_TypeWriterEffect.instance.Event_ing == false);
        mission_975_980.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3);
        mission_975_980.SetActive(false);
        RunnerPlayer1.instance.StoryEventing = false;
        RunnerPlayer1.instance.O2_eventCheck = true; //산소 부족 구간 시작!

        yield return null;
    }
}

 