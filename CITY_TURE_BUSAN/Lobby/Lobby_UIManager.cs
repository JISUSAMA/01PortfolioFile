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
        //Debug.Log("---CustomChange " + PlayerPrefs.GetString("Busan_CustomChange"));
        //Debug.Log("---GoldUse " + PlayerPrefs.GetString("Busan_GoldUse"));
        //Debug.Log("---ProfileChange " + PlayerPrefs.GetString("Busan_ProfileChange"));
        //Debug.Log("---GameOnePlay " + PlayerPrefs.GetString("Busan_GameOnePlay"));
        //Debug.Log("---ItemUse " + PlayerPrefs.GetString("Busan_ItemUse"));
        //Debug.Log("---ItemPurchase " + PlayerPrefs.GetString("Busan_ItemPurchase"));


        if (PlayerPrefs.GetString("Busan_Player_Sex") == "Woman")
        {
            woman = GameObject.Find("Woman").GetComponent<Transform>();
            man = GameObject.Find("Man").GetComponent<Transform>();
            woman.gameObject.SetActive(true);

            Destroy(man.gameObject);
            //man.gameObject.SetActive(false);

            //로비 -모델링 위치 초기화
            woman.localPosition = new Vector3(8.7f, 4.1f, 12.6f);
            woman.localRotation = Quaternion.Euler(0.0f, 108f, 0.0f);
            woman.localScale = new Vector3(12f, 12f, 12f);
        }
        else if(PlayerPrefs.GetString("Busan_Player_Sex") == "Man")
        {
            man = GameObject.Find("Man").GetComponent<Transform>();
            woman = GameObject.Find("Woman").GetComponent<Transform>();
            man.gameObject.SetActive(true);

            Destroy(woman.gameObject);
            //woman.gameObject.SetActive(false);

            //로비 -모델링 위치 초기화
            man.localPosition = new Vector3(8.5f, 3.4f, 14f);
            man.localRotation = Quaternion.Euler(0.0f, 112.4f, 0.0f);
            man.localScale = new Vector3(12f, 12f, 12f);
        }
        
    }

    void Start()
    {
        
        Initialization();
    }

    void Initialization()
    {
        SetExp_Coin();
    }

    void SetExp_Coin()
    {
        nameText.text = PlayerPrefs.GetString("Busan_Player_NickName");
        expText.text = PlayerPrefs.GetInt("Busan_Player_CurrExp").ToString();
        currExp_i = PlayerPrefs.GetInt("Busan_Player_CurrExp");        
        coinText.text = CommaText(NewLobby_DataManager.instance.coin_i);
        levelText.text = "Lv." + PlayerPrefs.GetInt("Busan_Player_Level").ToString();
        level_i = PlayerPrefs.GetInt("Busan_Player_Level");
        expFull_i = level_i * (level_i + 1) * 25;

        //Debug.Log(currExp_i +"::: "   + expFull_i);
        expSlider.value = (float)currExp_i / (float)expFull_i;

        //Lobby_DataManager.instance.ProfileImageChange(profileImg);
        NewLobby_DataManager.instance.ProfileImageChange(profileImg);
    }

    public string CommaText(int _data) {
        if (_data != 0)
            return string.Format("{0:#,###}", _data);
        else
            return "0";
    }

    //프로필 버튼 클릭 이벤트
    public void MyInformationButtonOn()
    {
        if (PlayerPrefs.GetString("Busan_MyInfoVisit").Equals("MissionOk"))
            PlayerPrefs.SetString("Busan_MyInfoVisit", "MissionOk");    //미션끝
        else
        {
            PlayerPrefs.SetString("Busan_MyInfoVisit", "Yes");    //방문했다
            ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
        }

        SceneManager.LoadScene("MyInformation");    //내정보 씬 이동
    }

    //내배낭 버튼 클릭 이벤트
    public void MyInventoryButtonOn()
    {
        if(PlayerPrefs.GetString("Busan_InventoryVisit").Equals("MissionOk"))
            PlayerPrefs.SetString("Busan_InventoryVisit", "MissionOk"); //방문했다
        else
        {
            PlayerPrefs.SetString("Busan_InventoryVisit", "Yes"); //방문했다
            ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
        }

        SceneManager.LoadScene("MyInventory");  //내배낭 씬 이동
    }

    //스토어 버튼 클릭 이벤트
    public void StoreButtonOn()
    {
        SoundMaixerManager.instance.StoreBGMPlay();
        if(PlayerPrefs.GetString("Busan_StoreVisit").Equals("MissionOk"))
            PlayerPrefs.SetString("Busan_StoreVisit", "MissionOk"); //방문했다
        else
        {
            PlayerPrefs.SetString("Busan_StoreVisit", "Yes"); //방문했다
            ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
        }
            
        SceneManager.LoadScene("Store");    //스토어 씬 이동
    }

    //랭킹존버튼 클릭 이벤트
    public void RankingJonButtonOn()
    {
        if (PlayerPrefs.GetString("Busan_RankJonVisit").Equals("MissionOk"))
            PlayerPrefs.SetString("Busan_RankJonVisit", "MissionOk");   //방문했다
        else
        {
            PlayerPrefs.SetString("Busan_RankJonVisit", "Yes");   //방문했다
            ServerManager.Instance.Update_TodayQuest("TodayQuest2", "Yes");
        }
            
        SceneManager.LoadScene("RanKingJon");   //랭킹존 씬 이동
    }

    //퀘스트 버튼 클릭 이벤트
    public void QuestButtonOn()
    {
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
