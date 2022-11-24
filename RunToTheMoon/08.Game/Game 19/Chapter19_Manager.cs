using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class Chapter19_Manager : MonoBehaviour
{
    public static Chapter19_Manager instance { get; private set; }
    public GameObject portal_Gate;
    public GameObject[] EventTracks;

    public float Chapter19_Progress;
    public int Chapter19_pos_int = 0;

    //920-925
    public GameObject Rune_Blick;
    public GameObject Blue_soul; //푸른빛 영혼
    public GameObject Blue_soul_fake; //가짜 푸른빛 영혼
    public Animator BlueSoul_ani;
    public Animator BlueSoulNet_ani;

    public GameObject PlayerNetOb; //플레이어 잠자리채
    //
    public GameObject moonPice_white;
    //
    public bool getStarCheck = false;

    public GameObject mission_920_925;  //별 모으기
    public GameObject mission_945_950; //달 가루 뿌리기 
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

        StartCoroutine(_Chapter19_Progress());
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        Debug.Log(col.name);
        if (col.tag.Equals("Narration"))
        {
            int num = int.Parse(col.name);

            //920-925 파란빛 영혼 만나기 
            if (Chapter19_pos_int.Equals(5))
            {
                if (num.Equals(4))
                {
                    StartCoroutine(_Meet_BlueSoul());
                }
                //파란 빛 영혼을 만난 후
                if (num.Equals(5))
                {
                    StartCoroutine(_Help_BlueSoul());
                }
            }
            else
            {
                Game_TypeWriterEffect.instance.Show_EventStoryText(num);
            }
            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        //945-950 별모으기
        if (Chapter19_pos_int.Equals(10))
        {
            if (col.name.Equals("Last30Star"))
            {
                StartCoroutine(_PlayerGetStarAni(col.gameObject));
                StartCoroutine(_Get_30Star());
            }

        }
        if (col.name.Equals("GetStar"))
        {
            StartCoroutine(_PlayerGetStarAni(col.gameObject));
        }
        else if (col.name.Equals("MoonPices_White"))
        {
            RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("MoonPices_White");//조각 설명
            Game_DataManager.instance.CollectionNumber = 9;
        }

        if (col.name.Equals("GoalPos"))
        {
            if (RunnerPlayer1.instance.playerPosition > RunnerPlayer1.instance.cinemachineSmoothPath.PathLength)
            {
                portal_Gate.SetActive(true);
            }
        }
    }
    IEnumerator _PlayerGetStarAni(GameObject star)
    {
        //  RunnerPlayer1.instance.anim.SetBool("Grab",true);
        //  yield return new WaitForSeconds(1f);
        //  RunnerPlayer1.instance.anim.SetBool("Grab", false);
        //애니메이션 완료 후 실행되는 부분
        RunnerPlayer1.instance.anim.SetBool("Idle 2", false);
        RunnerPlayer1.instance.anim.SetBool("Idle 3", false);
        RunnerPlayer1.instance.anim.SetBool("Look_Behind", false);

        RunnerPlayer1.instance.speed = 0f;
        RunnerPlayer1.instance.anim.SetTrigger("GetStar");

        RunnerPlayer1.instance.CheckAnimationState();      // Anim State Check
        yield return new WaitUntil(() => getStarCheck.Equals(true));
        star.SetActive(false);
        getStarCheck = false;
        yield return null;
    }

    //5키로 마다 나오는 이벤트 구분
    IEnumerator _Chapter19_Progress()
    {
        Chapter19_Progress = 1000 - Game_DataManager.instance.moonDis;

        //현재 지점에 따른 이벤트 구간
        if (Chapter19_Progress >= 900 && Chapter19_Progress < 905) Chapter19_pos_int = 1; //룬의 몸 깜빡임
        else if (Chapter19_Progress >= 905 && Chapter19_Progress < 910) Chapter19_pos_int = 2;
        else if (Chapter19_Progress >= 910 && Chapter19_Progress < 915) Chapter19_pos_int = 3;
        else if (Chapter19_Progress >= 915 && Chapter19_Progress < 920) Chapter19_pos_int = 4;//별 모으기
        else if (Chapter19_Progress >= 920 && Chapter19_Progress < 925) Chapter19_pos_int = 5;//파란빛 영혼
        else if (Chapter19_Progress >= 925 && Chapter19_Progress < 930) Chapter19_pos_int = 6;
        else if (Chapter19_Progress >= 930 && Chapter19_Progress < 935) Chapter19_pos_int = 7;
        else if (Chapter19_Progress >= 935 && Chapter19_Progress < 940) Chapter19_pos_int = 8;
        else if (Chapter19_Progress >= 940 && Chapter19_Progress < 945) Chapter19_pos_int = 9;//달 가루 뿌리기 
        else if (Chapter19_Progress >= 945 && Chapter19_Progress < 950) Chapter19_pos_int = 10; // 별모으기 완료

        EventTracks[Chapter19_pos_int - 1].SetActive(true);
        StartCoroutine(_Chapter19_event()); //맵 구간에 따른 이벤트 
        yield return null;
    }
    IEnumerator _Chapter19_event()
    {
        //룬의 몸이 깜빡임!
        if (Chapter19_pos_int.Equals(1))
        {
            StartCoroutine(_BlickRune());
        }
        if (Chapter19_pos_int > 5)
        {
            PlayerNetOb.SetActive(true); // 잠자리채 활성화 시킴!
        }
        yield return null;
    }
    IEnumerator _BlickRune()
    {
       //타이틀 이벤트 끝나고 나면
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        Game_TypeWriterEffect.instance.Show_EventStoryText(1); //룬의 몸이 깜빡임
        Rune_Blick.GetComponent<RuneController>().Blick_Rune();
        yield return null;
    }
    //푸른 빛 영혼 도와주기
    IEnumerator _Help_BlueSoul()
    {
        RunnerPlayer1.instance.StoryEventing = true; //플레이어 멈춰!
        Game_UIManager.instance.Fade_in(); //화면 가리고 
        yield return new WaitForSeconds(1f);
        RunnerPlayer1.instance._cameraView.vCame[4].m_Priority = 12;
        Blue_soul_fake.SetActive(false);
        Blue_soul.SetActive(true);
        BlueSoul_ani.SetBool("action", false);
        BlueSoulNet_ani.SetBool("action", false);
        yield return new WaitForSeconds(2f);
        //yield return new WaiteForScense(1f);
        Game_UIManager.instance.Fade_out(); //화면 보이기
        BlueSoul_ani.SetTrigger("given");
        BlueSoulNet_ani.SetTrigger("given");
        Game_TypeWriterEffect.instance.Show_EventStoryText(5); //내가 도와줄께!
        //yield return new WaiteForScense(1f);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        RunnerPlayer1.instance._cameraView.vCame[4].m_Priority = 10;
        Blue_soul.SetActive(false); //파란 빛 영혼 사라져
        yield return new WaitForSeconds(1);
        PlayerNetOb.SetActive(true); //플레이어 잠자리채 활성화 하기 
        mission_920_925.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3);
        mission_920_925.SetActive(false);
        RunnerPlayer1.instance.StoryEventing = false; //플레이어 움직여!
    }
    IEnumerator _Meet_BlueSoul()
    {
        RunnerPlayer1.instance.StoryEventing = true; //플레이어 멈춰!
        Game_TypeWriterEffect.instance.Show_EventStoryText(4); //저기 누가 있어 !
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority = 12;
        float time = 5f;
        while (time > 0 || Game_TypeWriterEffect.instance.Event_ing == true)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority = 10;
        RunnerPlayer1.instance.StoryEventing = false; //플레이어 멈춰!
        yield return null;
    }

    //945-950 별 모으기 완료 이벤트
    IEnumerator _Get_30Star()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => getStarCheck == false);
        RunnerPlayer1.instance.StoryEventing = true; //플레이어 멈춰!
        RunnerPlayer1.instance.moonPowder_useEvnet = true;

        yield return new WaitForSeconds(1f);
        Blue_soul.SetActive(true);
        BlueSoul_ani.SetBool("action", false);
        BlueSoulNet_ani.SetBool("action", false);
        Game_TypeWriterEffect.instance.Show_EventStoryText(7); //30개 다모았어!
        RunnerPlayer1.instance._cameraView.vCame[4].m_Priority = 12;

        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        Game_UIManager.instance.Fade_in_out();
        Blue_soul.SetActive(false);

        RunnerPlayer1.instance._cameraView.vCame[4].m_Priority = 10;
        yield return new WaitForSeconds(1f);
        mission_945_950.SetActive(true); //달가루를 뿌려보자
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3f);
        mission_945_950.SetActive(false);

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

        yield return new WaitUntil(() => RunnerPlayer1.instance.use_moonPowder == true); //달가루 뿌리면
        SoundFunction.Instance.Mission_End_Sound();
        moonPice_white.SetActive(true);
        Game_TypeWriterEffect.instance.Show_EventStoryText(8); //달의 조각이야!
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        yield return new WaitForSeconds(1f);
        RunnerPlayer1.instance._cameraView.vCame[4].m_Priority = 12;
        Blue_soul.SetActive(true);
        BlueSoul_ani.SetBool("action", false);
        BlueSoulNet_ani.SetBool("action", false);
        Game_TypeWriterEffect.instance.Show_EventStoryText(9); //이 조각은 너가 가져
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        RunnerPlayer1.instance._cameraView.vCame[4].m_Priority = 10;
        RunnerPlayer1.instance.StoryEventing = false;
        RunnerPlayer1.instance.moonPowder_useEvnet = false;
        RunnerPlayer1.instance.use_moonPowder = false;

        Blue_soul.SetActive(false);
        yield return null;
    }

}

