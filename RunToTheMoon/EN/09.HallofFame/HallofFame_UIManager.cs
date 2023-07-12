using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class HallofFame_UIManager : MonoBehaviour
{
    public GameObject ReachPointPanel; //달 도착, 비석 화면
    public GameObject HallOfFamePanel; //명예의 전당, 100인

    [Header("100위 까지의 랭킹,오브젝트 관리 ")]
    public GameObject RankPrefeb; //생성하는 프리팹
    public GameObject RankPanel;    //랭킹판넬
    public Transform SpawnPos; //판넬을 생성하는 위치

    public Text ReachPointPanel_nickName; //기록되는 닉네임  
    public Text Hof_myRank_t; //현재 비석에 등록된

    public Button steleBtn; //비석 버튼

    public double hof_quotient; // 명예의 전당에 오른 사람 /7 의 몫
    public double hof_rest;    //명예의 전당에 오른 사람 /7 의 나머지

    //생성된 판넬
    public List<GameObject> RankLists = new List<GameObject>(); //한 판넬에 7명씩 들어있는 판넬 오브젝트 리스트
    public List<GameObject> RankALL = new List<GameObject>(); //RankLists 안에 들어있는 순위정보 오브젝트 리스트
    //public List<Rank_> rankingList = new List<Rank_>(); //저장된 랭킹 정보 리스트
    public List<Rank> rankingList = new List<Rank>(); //저장된 랭킹 정보 리스트

    public int RankerList = 0; //명예의 전당에 오른 사람의 수

    [Header("명에의 전당 리스트")]
    public int currentPage = 1; //현재 
    [SerializeField] GameObject LeftBtn, RightBtn;
    [SerializeField] Text currentPage_txt;
    [SerializeField] Text MyRecordConnectDayTxt; // 20**년 도착
    [SerializeField] Text MyRecordTotalTxt; //**일 동안

    public string userNickName; //나의 닉네임

    public int myRank_int;//나의 순위
    public string TodayDate_str;
    public string TodayDatInfo_str;

    public Volume _ui_Volume;
    public VolumeProfile _ui_Profile;
    Bloom bl;

    public GameObject Fireworks_sound_ob;
    public static HallofFame_UIManager instance { get; private set; }
    private void Awake()
    {
        hof_quotient = 1;
        currentPage = 1;
        //RankerList = 11; //명예의 전당에 오른 사람의 수

        if (instance != null)
            Destroy(this);
        else instance = this;

        ///////////// 게임 시작할 때, 사용되는 postProcess//////////////
        _ui_Volume.profile = _ui_Profile;
        bl = (Bloom)_ui_Volume.profile.components[0];
        bl.intensity.value = 3f;
        Reach_Moon();
        /////////////////////////////////////////////////////////////
        ServerManager.Instance.Reg_Ranking(PlayerPrefs.GetString("Player_NickName"));
        //명예의 전당에 도착했을 떄 받아야하는 정보
        //총 걸린 시간, 해당 유저 닉네임, 랭킹순위 , 도달한 날짜
        TodayDate_str = DateTime.Now.ToString("yyyy년 MM월 dd일"); //오늘 날짜

        PlayerPrefs.SetString("RecordDate", TodayDate_str);
        ReachPointPanel_nickName.text = PlayerPrefs.GetString("Player_NickName");
        MyRecordTotalTxt.text = PlayerPrefs.GetInt("Player_ConnectDay") + "DAYS"; //**일동안
        MyRecordConnectDayTxt.text = PlayerPrefs.GetString("RecordDate");//기록하는 날
        userNickName = PlayerPrefs.GetString("Player_NickName");

    }

    IEnumerator Get_Ranking()
    {
        ServerManager.Instance.Get_Ranking();
        yield return new WaitUntil(() => ServerManager.Instance.isGetRankingCompleted);
        ServerManager.Instance.isGetRankingCompleted = false;

        for (int index = 0; index < ServerManager.Instance.Ranking.Count; index++)
        {
            rankingList.Add(ServerManager.Instance.Ranking[index]);
        }

        RankerList = ServerManager.Instance.Ranking.Count; //명예의 전당에 오른 사람의 수

        //---------------------------------------------------------//

        //명예의전당 인원수에 따른 판넬 생성 갯수
        hof_quotient = System.Math.Truncate((double)RankerList / 7); // 몫
        if (hof_quotient != 0)
        {
            hof_rest = RankerList % 7; //나머지
            Debug.Log("hof_quotient :" + hof_quotient);
            //랭커의 수가 7*n 수 일 경우, 
            if (hof_rest.Equals(0))
            {
                if (!hof_quotient.Equals(1))
                {
                    for (int i = 0; i < hof_quotient; i++)
                    {
                        SpawnList(); //판넬 생성
                    }
                }
                else
                {
                    SpawnList();
                }

            }
            else
            {
                for (int i = 0; i < hof_quotient + 1; i++)
                {
                    SpawnList(); //판넬 생성
                }
            }

        }
        else
        {
            SpawnList(); //판넬 생성

        }
        Set_Data_List(); //오브젝트의 순서에 맞게 랭킹,이름,날짜 입력
 
    }

    public void Reach_Moon()
    {
        StartCoroutine(_Reach_Moon());
    }
    IEnumerator _Reach_Moon()
    {
        bl = (Bloom)_ui_Volume.profile.components[0];
        while (bl.intensity.value > 0)
        {
            bl.intensity.value -= Time.deltaTime * 2;
            //     Debug.Log(" 1 :" + bl.intensity.value);
            yield return null;
        }
    }
    private void Start()
    {
        StartCoroutine(Get_Ranking());
        SoundManager.Instance.PlayBGM("3_HonorLists_v3"); //명예의 전당 사운드
        StartCoroutine(_FireworkSoundPlayDelay()); //폭죽 사운드 딜레이
    }
    IEnumerator _FireworkSoundPlayDelay()
    {
        yield return new WaitForSeconds(2f);
        Fireworks_sound_ob.SetActive(true);
        yield return null;
    }
    private void Update()
    {
        ///////////////////// 판넬 페이지에 따른 버튼 활성화/ 현재 페이지 텍스트 ///////////////////////
        //첫 페이지,왼쪽 버튼 지우기
        if (currentPage.Equals(1))
        {
            LeftBtn.SetActive(false);
            RightBtn.SetActive(true);
        }
        //마지막 페이지, 오른쪽 버튼 지우기
        else if (currentPage.Equals(RankLists.Count))
        {
            RightBtn.SetActive(false);
            LeftBtn.SetActive(true);
        }
        else
        {
            LeftBtn.SetActive(true);
            RightBtn.SetActive(true);
        }

        if (RankLists.Count != 0) { RankLists[currentPage - 1].gameObject.SetActive(true); }
        currentPage_txt.text = currentPage.ToString();
        /////////////////////////////////////////////////////////////////////////////////////
    }
    //오브젝트의 순서에 맞게 랭킹,이름,날짜 입력
    private void Set_Data_List()
    {
        //나머지가 없으면
        if (hof_rest.Equals(0))
        {
            for(int i =0; i<hof_quotient; i++)
            {
                for(int j =0; j<7; j++)
                {
                    Debug.Log("----------------------------------여기 몇번?1");
                    RankALL.Add(RankLists[i].GetComponent<HallofFame_RankDataManager>().RankBarOb[j].gameObject);
                    RankLists[i].GetComponent<HallofFame_RankDataManager>().GrupData(i, j); //1번째 7번, 2번째 3번
                }
              
            }
        }
        else
        {
            for(int i =0; i<hof_quotient+1; i++)
            {
                if (i < hof_quotient)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        Debug.Log("----------------------------------여기 몇번?1");
                        RankALL.Add(RankLists[i].GetComponent<HallofFame_RankDataManager>().RankBarOb[j].gameObject);
                        RankLists[i].GetComponent<HallofFame_RankDataManager>().GrupData(i, j); //1번째 7번, 2번째 3번
                    }
                }
                else
                {
                    //나머지 부분
                    for (int a = 0; a < hof_rest; a++)
                    {
                        RankALL.Add(RankLists[i].GetComponent<HallofFame_RankDataManager>().RankBarOb[a].gameObject);
                        RankLists[i].GetComponent<HallofFame_RankDataManager>().GrupData(i, a); //1번째 7번, 2번째 3번
                    }
                } 
            }
        }
        MyRank_Find_();//전체 오브젝트에서 내 아이디를 가진 오브젝트를 찾음
    }
    //전체 오브젝트에서 내 아이디를 가진 오브젝트를 찾음
    private void MyRank_Find_()
    {
        Debug.Log("RankerList"+ RankerList);
        RankALL[RankerList-1].GetComponent<RankData>().Line.SetActive(true);
        RankALL[RankerList-1].GetComponent<RankData>().RankNum.GetComponent<Text>().color = new Color(44 / 255f, 82 / 255f, 255 / 255f);
        RankALL[RankerList-1].GetComponent<RankData>().RankName.GetComponent<Text>().color = new Color(44 / 255f, 82 / 255f, 255 / 255f);
        RankALL[RankerList-1].GetComponent<RankData>().RankDate.GetComponent<Text>().color = new Color(44 / 255f, 82 / 255f, 255 / 255f);
        currentPage = RankALL[RankerList-1].GetComponent<RankData>().PanelNum;
        //for (int i = 0; i < RankALL.Count; i++)
        //{
        //    if (i.Equals(RankALL.Count))
        //    {
        //        Debug.Log("------판텔");
        //        //등록하는 닉네임과 등록된 날짜를 비교 
        //        RankALL[i].GetComponent<RankData>().Line.SetActive(true);
        //        RankALL[i].GetComponent<RankData>().RankNum.GetComponent<Text>().color = new Color(44 / 255f, 82 / 255f, 255 / 255f);
        //        RankALL[i].GetComponent<RankData>().RankName.GetComponent<Text>().color = new Color(44 / 255f, 82 / 255f, 255 / 255f);
        //        RankALL[i].GetComponent<RankData>().RankDate.GetComponent<Text>().color = new Color(44 / 255f, 82 / 255f, 255 / 255f);
        //        currentPage = RankALL[i].GetComponent<RankData>().PanelNum;
        //    }
        //}
    }
    private void SpawnList()
    {
        GameObject RankPrefeb_t = Instantiate(RankPrefeb, SpawnPos); //랭킹판넬 생성
        RankLists.Add(RankPrefeb_t);
        RankPrefeb_t.transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    //비석을 클릭했을 때, 명예의 전당으로 이동
    public void OnClick_Record()
    {
        ReachPointPanel.SetActive(false);
        HallOfFamePanel.SetActive(true);
        Hof_myRank_t.text = "Congratulations, " + PlayerPrefs.GetString("Player_NickName") + ".\n" +
            "You are the " + RankerList +
            " person to leave your name on this monument.";

    }
    public void OnClick_LeftBtn()
    {
        currentPage -= 1;
        RankLists[currentPage].gameObject.SetActive(false);
    }
    public void OnClick_RigthBtn()
    {
        RankLists[currentPage - 1].gameObject.SetActive(false);
        currentPage += 1;
    }
    public void OnClick_ExitBtn()
    {
        SceneManager.LoadScene("Lobby");
    }
}
