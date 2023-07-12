using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Chapter15_Manager : MonoBehaviour
{
    public static Chapter15_Manager instance { get; private set; }
    public GameObject portal_Gate;
    public GameObject[] EventTracks;

    public float Chapter15_Progress;
    public int Chapter15_pos_int = 0;

    [Header("잃어버린 토끼의 목걸이 이벤트")]
    bool give_necklace = false;
    public GameObject Rabbit_ob; //토끼의 목걸이
    public GameObject RabbitNecklace_ob; //토끼의 목걸이
    public Animator RabbitAni;  //토끼
    public Animator RabbitNeckAni; //목걸이
    public Transform RabbitTrans;

    public GameObject FakeRabbit_ob;
    public CinemachineDollyCart FakeRabbit_cart_sc;
    [Header("Pice")]
    public GameObject ParticleOB;
    public GameObject BaseFx;
    public GameObject BaseFx_Flare;
    public GameObject ObjectFx;

    public GameObject mission_740_745;// 잃어버린 목걸이 주인
    public GameObject mission_745_750;//달의 목걸이
   
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
        StartCoroutine(_Chapter15_Progress());
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
      
        if (col.tag.Equals("Narration"))
        {
            int num = int.Parse(col.name);
            Game_TypeWriterEffect.instance.Show_EventStoryText(num);
            col.gameObject.SetActive(false); //이벤트 발생 콜라이더 지우기
        }

        else if (col.name.Equals("Lost_Necklace"))
        {
            StartCoroutine(_Lost_Necklace());
            col.gameObject.SetActive(false);
        }
        else if (col.name.Equals("RabbitEvent"))
        {
            RunnerPlayer1.instance.StoryEventing = true;
            StartCoroutine(_Rabbit_LostNecklace_Event());
        }
        //달려가는 토끼
        else if (col.name.Equals("FakeRabbitEvent"))
        {
            StartCoroutine(_Follow_Rabbit());
        }
        else if (col.name.Equals("MoonPices_Purple"))
        {
            RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("MoonPices_Purple");//조각 설명
            Game_DataManager.instance.CollectionNumber = 7;
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
    IEnumerator _Chapter15_Progress()
    {
        Chapter15_Progress = 1000 - Game_DataManager.instance.moonDis;

        //현재 지점에 따른 이벤트 구간
        if (Chapter15_Progress >= 700 && Chapter15_Progress < 705) Chapter15_pos_int = 1;
        else if (Chapter15_Progress >= 705 && Chapter15_Progress < 710) Chapter15_pos_int = 2;
        else if (Chapter15_Progress >= 710 && Chapter15_Progress < 715) Chapter15_pos_int = 3;
        else if (Chapter15_Progress >= 715 && Chapter15_Progress < 720) Chapter15_pos_int = 4;
        else if (Chapter15_Progress >= 720 && Chapter15_Progress < 725) Chapter15_pos_int = 5;
        else if (Chapter15_Progress >= 725 && Chapter15_Progress < 730) Chapter15_pos_int = 6;
        else if (Chapter15_Progress >= 730 && Chapter15_Progress < 735) Chapter15_pos_int = 7;
        else if (Chapter15_Progress >= 735 && Chapter15_Progress < 740) Chapter15_pos_int = 8;
        else if (Chapter15_Progress >= 740 && Chapter15_Progress < 745) Chapter15_pos_int = 9;
        else if (Chapter15_Progress >= 745 && Chapter15_Progress < 750) Chapter15_pos_int = 10;

        EventTracks[Chapter15_pos_int - 1].SetActive(true);
        StartCoroutine(_Chapter15_event()); //맵 구간에 따른 이벤트 
        yield return null;
    }
    IEnumerator _Chapter15_event()
    {
      
        yield return null;
    }
    IEnumerator _Lost_Necklace()
    {
        RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("Necklace");
        yield return new WaitUntil
            (() => RunnerPlayer1.instance.ItemDescriptionManager.ItemDescription_ob.activeSelf == false);
        RunnerPlayer1.instance.StoryEventing = true;
        Game_TypeWriterEffect.instance.Show_EventStoryText(6);
        yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
        mission_740_745.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3);
        mission_740_745.SetActive(false);
        RunnerPlayer1.instance.StoryEventing = false;

        yield return null;
    }
    IEnumerator _Follow_Rabbit()
    {
        RunnerPlayer1.instance.StoryEventing = true;
        RunnerPlayer1.instance.moonPowder_useEvnet = true; //달가루 사용 가능함
        yield return new WaitForSeconds(1f);
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority = 12;
        FakeRabbit_cart_sc.m_Speed = 5f;
        FakeRabbit_ob.GetComponent<Animator>().SetBool("run", true);
        yield return new WaitForSeconds(3f);
        RunnerPlayer1.instance._cameraView.vCame[3].m_Priority = 10;
      
        mission_745_750.SetActive(true);
        SoundFunction.Instance.Show_Mission_Popup();
        yield return new WaitForSeconds(3);
        mission_745_750.SetActive(false);
        //달가루 사용 이펙트! 추가 수정
        Game_UIManager.instance.myItemPopup_sc.Use_MoonPowder(); //달가루 자동으로 사용됨
        RunnerPlayer1.instance.StoryEventing = false;
        SoundFunction.Instance.Mission_End_Sound();
        yield return null;
    }
    IEnumerator _Rabbit_LostNecklace_Event()
    {
        RunnerPlayer1.instance.StoryEventing = true; //Player 멈춰!
        Game_UIManager.instance.Fade_in_out();
        yield return new WaitForSeconds(2f);
        //목걸이 주기
        RabbitNecklace_ob.SetActive(true);
     
        Rabbit_animation();
        //
        yield return new WaitUntil(() => give_necklace == true);
        Debug.LogError(give_necklace);
       // Game_UIManager.instance.Fade_in();
        Rabbit_ob.transform.rotation = Quaternion.Euler(0, 0, 0);
        RabbitAni.SetBool("run", true);
        RabbitNeckAni.SetBool("run", true);
        StartCoroutine(_Start_DropPice());
        yield return new WaitForSeconds(3f);
        Game_UIManager.instance.Fade_in_out();
        Rabbit_ob.SetActive(false);
        RunnerPlayer1.instance.StoryEventing = false; //Player 멈춰!
      
    }
    public void Rabbit_animation()
    {
        StartCoroutine(_Rabbit_animation());
    }
    IEnumerator _Rabbit_animation()
    {
        RabbitNeckAni.SetTrigger("affairs");
        RabbitAni.SetTrigger("affairs");
        bool animationTrue = true;
        while (!RabbitAni.GetCurrentAnimatorStateInfo(0).IsName("Rabbit_affairs"))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.8f);
        animationTrue = false;
    
        //애니메이션 완료 후 실행되는 부 분
        give_necklace = true;
      
        while (RunnerPlayer1.instance.StoryEventing.Equals(true))
        {
            RabbitTrans.transform.Translate(Vector3.forward * 1.0f * Time.deltaTime * 2, Space.Self);
            yield return null;
        }
        yield return null;
    }
    IEnumerator _y_rotation()
    {
        while (true)
        {
            ParticleOB.transform.Rotate(new Vector3(0f, 30f, 0f) * Time.deltaTime, Space.World);
            yield return null;
        }
    }
    IEnumerator _Start_DropPice()
    {
        ParticleOB.SetActive(true);
        yield return new WaitForSeconds(1f);
        BaseFx_Flare.SetActive(true); //폭죽 터짐
        yield return new WaitForSeconds(0.3f);
        ObjectFx.SetActive(true);
       // StartCoroutine(_y_rotation());
        yield return null;
    }

}

