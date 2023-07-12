using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Chapter13_Manager : MonoBehaviour
{
    public static Chapter13_Manager instance { get; private set; }
    public GameObject portal_Gate;
    public GameObject[] EventTracks;

public float Chapter13_Progress;
    public int Chapter13_pos_int = 0;

    //public Animator Rune_ani;
    //public RuntimeAnimatorController Rune_ctrl;
    public GameObject[] Rune_ob; //룬
    public GameObject[] Rune_ob_fake; //룬
    public GameObject RuneShineParticleOb; //변하는 룬
    public GameObject MoonPice;
    public GameObject MeetRuneOb;

    public GameObject mission_600_605; //우주꽃 모으기
    public GameObject mission_645_650; //달의 포자 피하기

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
        StartCoroutine(_Chapter13_Progress());

    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        Debug.Log(col.name);
        if (col.tag.Equals("Narration"))
        {
            int num = int.Parse(col.name);
            Game_TypeWriterEffect.instance.Show_EventStoryText(num);
            //룬과의 만남
            if (Chapter13_pos_int.Equals(1))
            {
                if (num.Equals(1))
                {
                    MeetRuneOb.GetComponent<Animator>().SetTrigger("hello"); //인사하는 룬
                    StartCoroutine(_MeetSoul_StartEvent());
                }
            }
            col.SetActive(false); //이벤트 발생 콜라이더 지우기
        }
        //우주꽃 모으기
        else if (Chapter13_pos_int.Equals(4))
        {

            if (col.name.Equals("SpaceFlowerLast"))
            {
                StartCoroutine(_SpaceFlower_EventEnd());
            }
        }
        else if (Chapter13_pos_int.Equals(10))
        {
            if (col.name.Equals("SporePointStart"))
            {
                StartCoroutine(_SporeZoonEvnet());
            }
            else if (col.name.Equals("SporePointEnd"))
            {
                RunnerPlayer1.instance.O2_eventCheck = false; //이벤트 끝! 원래 상태로
                SoundFunction.Instance.Mission_End_Sound();
            }
        }
        else if (col.name.Equals("MoonPices_Pink"))
        {
            RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("MoonPices_Pink");//조각 설명
            Game_DataManager.instance.CollectionNumber = 6;
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
    IEnumerator _Chapter13_Progress()
    {
        Chapter13_Progress = 1000 - Game_DataManager.instance.moonDis;

        //현재 지점에 따른 이벤트 구간
        if (Chapter13_Progress >= 600 && Chapter13_Progress < 605) Chapter13_pos_int = 1; //소울이와의 만남 //우주 꽃 모으기
        else if (Chapter13_Progress >= 605 && Chapter13_Progress < 610) Chapter13_pos_int = 2;
        else if (Chapter13_Progress >= 610 && Chapter13_Progress < 615) Chapter13_pos_int = 3;
        else if (Chapter13_Progress >= 615 && Chapter13_Progress < 620) Chapter13_pos_int = 4; //우주 꽃 모으기 끝
        else if (Chapter13_Progress >= 620 && Chapter13_Progress < 625) Chapter13_pos_int = 5;
        else if (Chapter13_Progress >= 625 && Chapter13_Progress < 630) Chapter13_pos_int = 6;
        else if (Chapter13_Progress >= 630 && Chapter13_Progress < 635) Chapter13_pos_int = 7;
        else if (Chapter13_Progress >= 635 && Chapter13_Progress < 640) Chapter13_pos_int = 8;
        else if (Chapter13_Progress >= 640 && Chapter13_Progress < 645) Chapter13_pos_int = 9;
        else if (Chapter13_Progress >= 645 && Chapter13_Progress < 650) Chapter13_pos_int = 10;

        EventTracks[Chapter13_pos_int - 1].SetActive(true);
        StartCoroutine(_Chapter13_event()); //맵 구간에 따른 이벤트
        yield return null;
    }
    IEnumerator _Chapter13_event()
    {
        ///////// 룬이 변하기 전까지는 다른 애니메이션이라 상황에 따라서 컴포넌트 받아서 애니메이션 바꿔줘야함 //////////
        //룬을 만나기 전
        if (Chapter13_pos_int.Equals(1))
        {
            for (int i = 0; i < Rune_ob.Length; i++) { Rune_ob[i].SetActive(false); }
            for (int i = 0; i < Rune_ob_fake.Length; i++) { Rune_ob_fake[i].SetActive(false); } //미로 옆의 룬 안보이게
                                                                                                //우주꽃 이벤트 시작 초기화!
            RunnerPlayer1.instance.spaceFlower = 0;
        }
        //룬을 만나고 난 후 2- 4 chapter
        //우주꽃 모으고 룬의 색이 변함
        else if (Chapter13_pos_int > 1 && Chapter13_pos_int < 5)
        {
            Rune_ob[0].SetActive(true); //빛을 잃은 룬
            Rune_ob[1].SetActive(false); //빛이 나는 룬
            for (int i = 0; i < Rune_ob_fake.Length; i++) { Rune_ob_fake[i].SetActive(false); } //가짜 룬 안보이게
        }
        //5chapter부터 룬과 동행
        else if (Chapter13_pos_int > 5)
        {
            Rune_ob[0].SetActive(false); //빛을 잃은 룬
            Rune_ob[1].SetActive(true); //빛이 나는 룬
            for (int i = 0; i < Rune_ob_fake.Length; i++) { Rune_ob_fake[i].SetActive(false); } //가짜 룬 안보이게
        }

        yield return null;
    }
    //룬과 만나는 이벤트 시작
    IEnumerator _MeetSoul_StartEvent()
    {
        RunnerPlayer1.instance.StoryEventing = true; //이벤트 중에는 안움직임
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        MeetRuneOb.GetComponent<Animator>().SetTrigger("sad"); //우는 룬
        Game_TypeWriterEffect.instance.Show_EventStoryText(2); // 좋은 생각 났다구!!!!!!!!!
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        Game_UIManager.instance.Fade_in();
        yield return new WaitForSeconds(2f);
        MeetRuneOb.SetActive(false);//룬사라짐
        Rune_ob[0].SetActive(true);
        yield return new WaitForSeconds(2f);
        Game_UIManager.instance.Fade_out();
        Game_TypeWriterEffect.instance.Show_EventStoryText(3);
        Rune_ob[0].GetComponent<RuneController>().Random_CheckAnimationState(); // 빛을 잃은 룬 애니메이션 동작 시키기 Rune[0]
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        mission_600_605.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3f);
        mission_600_605.SetActive(false);
        RunnerPlayer1.instance.StoryEventing = false;
        yield return null;

    }
    //우주꽃 모으기 이벤트
    IEnumerator _SpaceFlower_EventEnd()
    {
        RunnerPlayer1.instance.StoryEventing = true; //플레이어 멈춰!
        Game_UIManager.instance.Fade_in();

        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < Rune_ob.Length; i++) { Rune_ob[i].SetActive(false); } //미로 옆의 룬 안보이게
        Rune_ob_fake[0].SetActive(true); //빛을 잃은 룬

        yield return new WaitForSeconds(1.5f);
        Game_UIManager.instance.Fade_out();

        Rune_ob_fake[0].GetComponent<Animator>().SetTrigger("hello"); //인사 하는 룬
        Game_TypeWriterEffect.instance.Show_EventStoryText(5); //우주꽃 10송이를 다모았어
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        Game_UIManager.instance.Fade_in();
        yield return new WaitForSeconds(1.5f);
        //룬의 몸이 빛나는 이벤트
        RuneShineParticleOb.SetActive(true); //파티클 생성
        Rune_ob_fake[0].SetActive(false); ; //빛을 잃은 룬
        Rune_ob_fake[1].SetActive(true); //빛나는 룬
        SoundFunction.Instance.Mission_End_Sound();
        yield return new WaitForSeconds(1.5f);
        Game_UIManager.instance.Fade_out();
        Game_TypeWriterEffect.instance.Show_EventStoryText(6); //네게서 빛이 나기 시작했어!
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        Game_UIManager.instance.Fade_in();
        yield return new WaitForSeconds(1.5f);
        Rune_ob_fake[0].SetActive(false); ; //빛을 잃은 룬
        Rune_ob_fake[1].SetActive(true); //빛나는 룬
        RuneShineParticleOb.SetActive(false);
        for (int i = 0; i < Rune_ob_fake.Length; i++) { Rune_ob_fake[i].SetActive(false); } //미로 옆의 룬 안보이게
        Rune_ob[1].SetActive(true);
        MoonPice.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Game_UIManager.instance.Fade_out();
        yield return new WaitForSeconds(1.5f);
        RunnerPlayer1.instance.StoryEventing = false; //플레이어 움직여!
        yield return null;
    }
    IEnumerator _SporeZoonEvnet()
    {
        RunnerPlayer1.instance.StoryEventing = true;
        Game_TypeWriterEffect.instance.Show_EventStoryText(9);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        mission_645_650.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3);
        mission_645_650.SetActive(false);
        RunnerPlayer1.instance.O2_eventCheck = true; //달의 포자 구간 진입, 산소 빨리 깎임
        RunnerPlayer1.instance.StoryEventing = false;
        RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("MoonSpore");//달의 포자
        yield return null;
    }
}

