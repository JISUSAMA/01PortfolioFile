using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TunnelEffect;

public class Chapter11_Manager : MonoBehaviour
{
    public static Chapter11_Manager instance { get; private set; }
    public GameObject portal_Gate;
    public GameObject moonPiece_Red;
    public GameObject[] EventTracks;

    public float Chapter11_Progress;
    public int Chapter11_pos_int = 0;


    private float _event_Init_Position_moonDis;
    private float _event_Init_Position_stationDis;
    bool RunToStart = false; //달리기!

    public GameObject mission_500_505; //소원석 통로 지나기
    public GameObject mission_510_515; //터널 속 산소부족
    public GameObject mission_545_550; //달의 포자 피하기

    [Header("터널") ]
    public TunnelFX2 _tunnelFx2;
    public Material _plank11_mat;
    public Color Tunnel_BaseColor; 
    public Color Tunnel_DarkColor;
    public GameObject Torch_ob; 
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }
    private void Start()
    {
        _plank11_mat.color = Tunnel_DarkColor;
      RunnerPlayer1.instance.cinemachineSmoothPath = GameObject.Find("PlayerTrack").GetComponent<CinemachineSmoothPath>();
        portal_Gate.SetActive(false);
        StartCoroutine(_Chapter11_Progress());
    }
    public void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;

        Debug.Log("col : " + col.name);
        if (Chapter11_pos_int.Equals(10))
        {
            //달의 포자 지나기
            if (col.name.Equals("SporePointStart"))
            {
                StartCoroutine(_SporeZoonEvnet());
            }
            else if (col.name.Equals("SporePointEnd"))
            {
                Game_TypeWriterEffect.instance.Show_EventStoryText(5);
                RunnerPlayer1.instance.O2_eventCheck = false; //다시 산소 1배로 돌아감
                SoundFunction.Instance.Mission_End_Sound();

            }
            // 달의 조각 발견 하기
            else if (col.name.Equals("Find_MoonPices_Red"))
            {
                moonPiece_Red.SetActive(true);
                Game_TypeWriterEffect.instance.Show_EventStoryText(8);
            }
            // 달의 조각 먹기
            else if (col.name.Equals("MoonPices_Red"))
            {
                Game_TypeWriterEffect.instance.Show_EventStoryText(9);
                RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("MoonPices_Red");//조각 설명
                Game_DataManager.instance.CollectionNumber = 5;
            }
        }
        //나레이션 대사 일 경우, 
        if (col.tag.Equals("Narration"))
        {
            int num = int.Parse(col.name);
            Game_TypeWriterEffect.instance.Show_EventStoryText(num);
            //500-505 구간 
            if (Chapter11_pos_int.Equals(1))
            {
                //소원석 벽화
                if (num.Equals(6))
                {
                    StartCoroutine(_Murall_Event());
                }
              
            }
            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }

        // 포탈 생기기
        if (col.name.Equals("GoalPos"))
        {
            portal_Gate.SetActive(true);
        }
    }
    //5키로 마다 나오는 이벤트 구분
    IEnumerator _Chapter11_Progress()
    {
        Chapter11_Progress = 1000 - Game_DataManager.instance.moonDis;
        //Chapter11_Progress = 1000 - 455;

        //현재 지점에 따른 이벤트 구간
        if (Chapter11_Progress >= 500 && Chapter11_Progress < 505) Chapter11_pos_int = 1;  //달의 신전의 비밀
        else if (Chapter11_Progress >= 505 && Chapter11_Progress < 510) Chapter11_pos_int = 2;
        else if (Chapter11_Progress >= 510 && Chapter11_Progress < 515) Chapter11_pos_int = 3; //산소 빨리 닳음
        else if (Chapter11_Progress >= 515 && Chapter11_Progress < 520) Chapter11_pos_int = 4;
        else if (Chapter11_Progress >= 520 && Chapter11_Progress < 525) Chapter11_pos_int = 5;
        else if (Chapter11_Progress >= 525 && Chapter11_Progress < 530) Chapter11_pos_int = 6; //소원석 벽화
        else if (Chapter11_Progress >= 530 && Chapter11_Progress < 535) Chapter11_pos_int = 7;
        else if (Chapter11_Progress >= 535 && Chapter11_Progress < 540) Chapter11_pos_int = 8;
        else if (Chapter11_Progress >= 540 && Chapter11_Progress < 545) Chapter11_pos_int = 9;
        else if (Chapter11_Progress >= 545 && Chapter11_Progress < 550) Chapter11_pos_int = 10; //달의 포자 //소원석 통로 지나기 

        Debug.Log("Chapter11_pos_int : " + Chapter11_pos_int);

        EventTracks[Chapter11_pos_int - 1].SetActive(true);
        StartCoroutine(_Chapter11_event()); //맵 구간에 따른 이벤트 
        yield return null;
    }
    IEnumerator _Chapter11_event()
    {
        yield return new WaitForSeconds(1f);
        
        if (Chapter11_pos_int.Equals(1)) 
        {
            StartCoroutine(_TourchEvnet_Strat()); // 횟불대 이벤트 
        }
        //소원석 벽화 
        else if (Chapter11_pos_int.Equals(6)) { Game_TypeWriterEffect.instance.Show_EventStoryText(7); 
            RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("Mural11_2"); }
        //산소 부족 이벤트
        else if (Chapter11_pos_int.Equals(3)) { StartCoroutine(_Lack_oxygen()); }
        //소원석 통로 지나기 이벤트 
        else if (Chapter11_pos_int.Equals(10)) 
        {
            mission_500_505.SetActive(true);
            SoundFunction.Instance.Show_Mission_Popup();
            yield return new WaitForSeconds(3f);
            mission_500_505.SetActive(false);
            
        }
        //횟불대 이벤트 
        if (Chapter11_pos_int > 1)
        {
            Torch_ob.SetActive(true);
            RunnerPlayer1.instance.anim.SetBool("Grab_Torch", true);
        }
      
        yield return null;
    }
    IEnumerator _SporeZoonEvnet()
    {
        RunnerPlayer1.instance.StoryEventing = true;
        Game_TypeWriterEffect.instance.Show_EventStoryText(4);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        mission_545_550.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3f);
        mission_545_550.SetActive(false);
        RunnerPlayer1.instance.StoryEventing = false;
        RunnerPlayer1.instance.O2_eventCheck = true; //산소 1.2배
        // 초기화 위치 저장
        _event_Init_Position_moonDis = Game_DataManager.instance.moonDis + 0.01f;
        _event_Init_Position_stationDis = Game_DataManager.instance.spaceStationDis + 0.01f;
        float dontMoveSpeed = 10;
        float lefttime = 2f;
        RunToStart = true;
        while (RunToStart.Equals(true))
        {
            if (SpeedMeter.instance.velocity_f < 20f)
            {
                dontMoveSpeed -= Time.deltaTime;
                lefttime -= Time.deltaTime;
                //5초 이상 움직이지 않거나 달리지 않았을 경우,
                if (dontMoveSpeed < 0)
                {
                    //다시 시작
                    RunToStart = false;
                    StopCoroutine(_SporeZoonEvnet());

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
                    RunnerPlayer1.instance.O2_eventCheck = false; //산소 1.2배
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
    //산소 부족 이벤트
    IEnumerator _Lack_oxygen()
    {
        RunnerPlayer1.instance.StoryEventing = true;
        Game_TypeWriterEffect.instance.Show_EventStoryText(2);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing.Equals(false));
        mission_510_515.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3f);
        mission_510_515.SetActive(false);
        RunnerPlayer1.instance.O2_eventCheck = true;
        RunnerPlayer1.instance.StoryEventing = false;

        yield return null;
    }
    float tint = 0;
    float speed = 1;
    // 횟불대 이벤트  
    IEnumerator _TourchEvnet_Strat()
    {
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        Game_TypeWriterEffect.instance.Show_EventStoryText(3); // 어두운 통로를 갈때 도움이 될 것 같아 
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        Torch_ob.SetActive(true);
        RunnerPlayer1.instance.anim.SetBool("Grab_Torch", true);
        

        string color_cod_Start = ColorUtility.ToHtmlStringRGBA(_tunnelFx2.tintColor);
        string color_cod_End = ColorUtility.ToHtmlStringRGBA(Tunnel_BaseColor);
        while (color_cod_Start != color_cod_End)
        {
            color_cod_Start = ColorUtility.ToHtmlStringRGBA(_tunnelFx2.tintColor);
            tint += Time.deltaTime * speed;
            _tunnelFx2.tintColor = Color.Lerp(Tunnel_DarkColor, Tunnel_BaseColor, tint);
          _plank11_mat.color = Color.Lerp(Tunnel_DarkColor, Tunnel_BaseColor, tint);
            yield return null;
        }

        yield return null;
    }
    IEnumerator _Murall_Event()
    {
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("Mural11_1");
        yield return null;
    }
}



