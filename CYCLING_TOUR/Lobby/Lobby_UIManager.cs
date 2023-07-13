using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lobby_UIManager : MonoBehaviour
{
    public static Lobby_UIManager instance { get; private set; }


    //-----상점 모델링 변경 테스트----------///
    //public Transform parent;
    //public GameObject bicycle;

    public Text expText;
    public Text coinText;
    public Text levelText;
    public Text nameText;
    public Slider expSlider;

    public Image profileImg;    //프로필 이미지
    public GameObject eventNoticPopup;  //이벤트 및 공지사항 팝업



    int expFull_i, currExp_i, level_i;

    Transform woman;    //여자 모델링
    Transform man;  //남자 모델링

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        //Debug.Log("StoreVisit " + PlayerPrefs.GetString("StoreVisit"));
        //Debug.Log("MyInfoVisit " + PlayerPrefs.GetString("MyInfoVisit"));
        //Debug.Log("InventoryVisit " + PlayerPrefs.GetString("InventoryVisit"));
        //Debug.Log("RankJonVisit " + PlayerPrefs.GetString("RankJonVisit"));
        //Debug.Log("---CustomChange " + PlayerPrefs.GetString("CustomChange"));
        //Debug.Log("---GoldUse " + PlayerPrefs.GetString("GoldUse"));
        //Debug.Log("---ProfileChange " + PlayerPrefs.GetString("ProfileChange"));
        //Debug.Log("---GameOnePlay " + PlayerPrefs.GetString("GameOnePlay"));
        //Debug.Log("---ItemUse " + PlayerPrefs.GetString("ItemUse"));
        //Debug.Log("---ItemPurchase " + PlayerPrefs.GetString("ItemPurchase"));

        //EventPopupShow();   //공지가 있는지 여부에 따른 팝업 활성/비활성

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            woman = GameObject.Find("Woman").GetComponent<Transform>();
            man = GameObject.Find("Man").GetComponent<Transform>();
            woman.gameObject.SetActive(true);

            Destroy(man.gameObject);
            //man.gameObject.SetActive(false);

            //로비 -모델링 위치 초기화
            woman.localPosition = new Vector3(7f, 3f, 20f);
            woman.localRotation = Quaternion.Euler(0.0f, 126f, 0.0f);
            woman.localScale = new Vector3(12f, 12f, 12f);
        }
        else if(PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            man = GameObject.Find("Man").GetComponent<Transform>();
            woman = GameObject.Find("Woman").GetComponent<Transform>();
            man.gameObject.SetActive(true);

            Destroy(woman.gameObject);
            //woman.gameObject.SetActive(false);

            //로비 -모델링 위치 초기화
            man.localPosition = new Vector3(8f, 3f, 20f);
            man.localRotation = Quaternion.Euler(0.0f, 126f, 0.0f);
            man.localScale = new Vector3(12f, 12f, 12f);
        }
    }

    void Start()
    {
        Initialization();
    }

    //void EventPopupShow()
    //{
    //    //공지가 있으니 팝업을 띄우시게나!
    //    if(PlayerPrefs.GetString("AT_NoticState").Equals("Yes"))
    //    {
    //        eventNoticPopup.SetActive(true);
    //    }
    //    else
    //    {
    //        eventNoticPopup.SetActive(false);
    //    }
    //}

    void Initialization()
    {
        SetExp_Coin();
    }

    void SetExp_Coin()
    {
        nameText.text = PlayerPrefs.GetString("AT_Player_NickName");
        expText.text = PlayerPrefs.GetInt("AT_Player_CurrExp").ToString();
        currExp_i = PlayerPrefs.GetInt("AT_Player_CurrExp");
        coinText.text = Lobby_DataManager.instance.CommaText(Lobby_DataManager.instance.coin_i);
        levelText.text = "Lv." + PlayerPrefs.GetInt("AT_Player_Level").ToString();
        level_i = PlayerPrefs.GetInt("AT_Player_Level");
        expFull_i = level_i * (level_i + 1) * 25;

        //Debug.Log(currExp_i +"::: "   + expFull_i);
        expSlider.value = (float)currExp_i / (float)expFull_i;

        Lobby_DataManager.instance.ProfileImageChange(profileImg);
    }


    //프로필 버튼 클릭 이벤트
    public void MyInformationButtonOn()
    {
        if (PlayerPrefs.GetString("AT_MyInfoVisit").Equals("MissionOk"))
            PlayerPrefs.SetString("AT_MyInfoVisit", "MissionOk");    //미션끝
        else
        {
            PlayerPrefs.SetString("AT_MyInfoVisit", "Yes");    //방문했다
            ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
        }

        SceneManager.LoadScene("MyInformation");    //내정보 씬 이동
    }

    //내배낭 버튼 클릭 이벤트
    public void MyInventoryButtonOn()
    {
        if(PlayerPrefs.GetString("AT_InventoryVisit").Equals("MissionOk"))
            PlayerPrefs.SetString("AT_InventoryVisit", "MissionOk"); //방문했다
        else
        {
            PlayerPrefs.SetString("AT_InventoryVisit", "Yes"); //방문했다
            ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
        }

        SceneManager.LoadScene("MyInventory");  //내배낭 씬 이동
    }

    //스토어 버튼 클릭 이벤트
    public void StoreButtonOn()
    {
        SoundMaixerManager.instance.StoreBGMPlay();

        if(PlayerPrefs.GetString("AT_StoreVisit").Equals("MissionOk"))
            PlayerPrefs.SetString("AT_StoreVisit", "MissionOk"); //방문했다
        else
        {
            PlayerPrefs.SetString("AT_StoreVisit", "Yes"); //방문했다
            ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
        }
            
        SceneManager.LoadScene("Store");    //스토어 씬 이동
    }

    //랭킹존버튼 클릭 이벤트
    public void RankingJonButtonOn()
    {
        SoundMaixerManager.instance.RankBGMPlay();

        if (PlayerPrefs.GetString("AT_RankJonVisit").Equals("MissionOk"))
            PlayerPrefs.SetString("AT_RankJonVisit", "MissionOk");   //방문했다
        else
        {
            PlayerPrefs.SetString("AT_RankJonVisit", "Yes");   //방문했다
            ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
        }
            
        SceneManager.LoadScene("RanKingJon");   //랭킹존 씬 이동
    }

    //퀘스트 버튼 클릭 이벤트
    public void QuestButtonOn()
    {
        SoundMaixerManager.instance.QusetBGMPlay();

        SceneManager.LoadScene("Quest");
    }

    //게임 맵 선택 화면 이동
    public void MapChoiceButtonOn()
    {
        SceneManager.LoadScene("MapChoice");
    }

    //BGM 사운드
    public void BGM_SliderSound()
    {
        SoundMaixerManager.instance.AudioControl();
    }

    //효과음 사운드
    public void Effect_SliderSound()
    {
        SoundMaixerManager.instance.SFXAudioControl();
    }

    


    /// <summary>
    /// 리소스에서 모델링 들고와서 변경(상점)
    /// </summary>
    public void GameStartOnBtn()
    {
        //bicycle.SetActive(false);
        //GameObject test = Instantiate(Resources.Load<GameObject>("woman_white_Bicycle"));
        //test.transform.parent = parent;
    }
}
