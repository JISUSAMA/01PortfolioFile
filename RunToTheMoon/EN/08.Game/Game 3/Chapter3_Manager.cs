using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter3_Manager : MonoBehaviour
{
    public GameObject[] EventTracks;
    public float Chapter3_Progress; //전체 진행정도
    public int Chapter3_pos_int = 0; //현재 맵에서의 진행 정도
    public static Chapter3_Manager instance { get; private set; }
    public GameObject portal_Gate;
    public GameObject MoonPices_Sky_ob;

    public GameObject Mission130_135; //터널 속 산소 부족
    public GameObject Mission145_150; //달의 비밀 통로 지나가기
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        RunnerPlayer1.instance.cinemachineSmoothPath = GameObject.Find("PlayerTrack").GetComponent<CinemachineSmoothPath>();
        portal_Gate.SetActive(false);
        StartCoroutine(_Chapter3_Progress());
    }
    //5키로 마다 나오는 이벤트 구분
    IEnumerator _Chapter3_Progress()
    {
        Chapter3_Progress = 1000 - Game_DataManager.instance.moonDis;

        //현재 지점에 따른 이벤트 구간
        if (Chapter3_Progress >= 100 && Chapter3_Progress < 105) Chapter3_pos_int = 1;
        else if (Chapter3_Progress >= 105 && Chapter3_Progress < 110) Chapter3_pos_int = 2;
        else if (Chapter3_Progress >= 110 && Chapter3_Progress < 115) Chapter3_pos_int = 3;
        else if (Chapter3_Progress >= 115 && Chapter3_Progress < 120) Chapter3_pos_int = 4;
        else if (Chapter3_Progress >= 120 && Chapter3_Progress < 125) Chapter3_pos_int = 5;
        else if (Chapter3_Progress >= 125 && Chapter3_Progress < 130) Chapter3_pos_int = 6;
        else if (Chapter3_Progress >= 130 && Chapter3_Progress < 135) Chapter3_pos_int = 7;
        else if (Chapter3_Progress >= 135 && Chapter3_Progress < 140) Chapter3_pos_int = 8; //산소 닳음
        else if (Chapter3_Progress >= 140 && Chapter3_Progress < 145) Chapter3_pos_int = 9;
        else if (Chapter3_Progress >= 145 && Chapter3_Progress < 150) Chapter3_pos_int = 10;

        EventTracks[Chapter3_pos_int - 1].SetActive(true);
        StartCoroutine(_Chapter3_event()); //맵 구간에 따른 이벤트 
        yield return null;
    }
    IEnumerator _Chapter3_event()
    {
        yield return new WaitForSeconds(10f);
        //달과 지구의 왕래 벽화
        if (Chapter3_pos_int.Equals(1)){ Game_TypeWriterEffect.instance.Show_EventStoryText(3); RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("Mural3_1"); }
        //조각함의 전설 벽화
        else if (Chapter3_pos_int.Equals(6)) {Game_TypeWriterEffect.instance.Show_EventStoryText(4); RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("Mural3_2"); }
        //산소 빨리 닳음
        else if (Chapter3_pos_int.Equals(8)) 
        {
            RunnerPlayer1.instance.StoryEventing = true; //멈춰~!
            //////////////////////////////////////////////////////////////////////////////////////
            Mission130_135.SetActive(true);// 미션 팝업
            SoundFunction.Instance.Show_Mission_Popup();
            yield return new WaitForSeconds(3f);
            Mission130_135.SetActive(false);// 미션 팝업
            RunnerPlayer1.instance.StoryEventing = false;
             ////////////////////////////////////////////////////////////////////////////////////////////
             yield return new WaitUntil(() => Mission130_135.activeSelf == false); //팝업이 사라지면
            Game_TypeWriterEffect.instance.Show_EventStoryText(5);
            RunnerPlayer1.instance.Use_Oxygenbar_red(); //산소통 빨간불 들어옴
        }
        //달의 비밀통로 지나가기 -> 달의 조각 이벤트 
        else if (Chapter3_pos_int.Equals(10))
        {
            RunnerPlayer1.instance.StoryEventing = true; //멈춰~!
            Mission145_150.SetActive(true);
            SoundFunction.Instance.Show_Mission_Popup();
            yield return new WaitForSeconds(3f);
            Mission145_150.SetActive(false);// 미션 팝업
            RunnerPlayer1.instance.StoryEventing = false;

        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        if (col.name.Equals("GoalPos"))
        {
            portal_Gate.SetActive(true);
        }
        else if (col.name.Equals("PiceEvent"))
        {
            MoonPices_Sky_ob.SetActive(true); 
        }
        else if (col.name.Equals("MoonPices_Sky"))
        {
            RunnerPlayer1.instance.ItemDescriptionManager.Show_ItemDescription("MoonPices_Sky");//조각 설명
            Game_DataManager.instance.CollectionNumber = 1;
        }
    }
}
