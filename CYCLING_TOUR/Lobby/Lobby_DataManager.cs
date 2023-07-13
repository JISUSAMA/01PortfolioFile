using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_DataManager : MonoBehaviour
{
    public static Lobby_DataManager instance { get; private set; }


    //각 아이템 배열에 저장된 번호
    public int id_jacketNum, id_pantsNum, id_shoesNum, id_hairNum, id_bodyNum, id_helmetNum, id_glovesNum;
    public string wear_Jacketkind, wear_PantsKind, wear_HelmetKind, wear_GlovesKind, wear_ShoesKind, wear_BicycleKind;
    public string wear_BicycleStyleName;
    public string skinColor, hairColor;
    public int profile;
    public int coin_i;

    public GameObject gameEndPopup; //게임종료팝업

    //여자 캐릭터
    GameObject womanPlayer;
    WomanCtrl womanctrl_scrip;
    GameObject manPlayer;
    ManCtrl manctrl_scrip;

    public string[] toDayArr;   //오늘 날짜 배열
    string oneCheck, twoCheck;  //처음접속, 두번째접속

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;


        GetTodayInitialization();
        MissionDataInit_AttendanceCheck();
        coin_i = PlayerPrefs.GetInt("AT_Player_Gold");   //코인
    }

    void Start()
    {
        //Debug.Log("왜 ???" + PlayerPrefs.GetString("AT_Player_Sex"));
        //Debug.Log("id_bicycleNum " + PlayerPrefs.GetString("AT_BicycleNumber") + " id_bicycleStr " + PlayerPrefs.GetString("AT_Wear_BicycleStyleName"));
        //Debug.Log("갯수 :: " + PlayerPrefs.GetInt("AsiaNormal1Finish"));
        //Debug.Log("---CustomChange " + PlayerPrefs.GetString("CustomChange"));
        //Debug.Log("---GoldUse " + PlayerPrefs.GetString("GoldUse"));
        //Debug.Log("---ProfileChange " + PlayerPrefs.GetString("ProfileChange"));
        //Debug.Log("---GameOnePlay " + PlayerPrefs.GetString("GameOnePlay"));
        //Debug.Log("---ItemUse " + PlayerPrefs.GetString("ItemUse"));
        //Debug.Log("---ItemPurchase " + PlayerPrefs.GetString("ItemPurchase"));
        //Debug.Log("로그인 상태 : " + PlayerPrefs.GetString("Player_LoginState"));

        //PlayerPrefs.SetString("Wear_GlovesStyleName", "");
        //PlayerPrefs.SetString("Wear_HelmetStyleName", "");
        //PlayerPrefs.SetInt("GlovesNumber", 100);
        //PlayerPrefs.SetInt("HelmetNumber", 100);
        //PlayerPrefs.SetString("Wear_GlovesKind", "No");
        //PlayerPrefs.SetString("Wear_HelmetKind", "No");
        //PlayerPrefs.SetString("Wear_ShoesKind", "ItemID_Sandal");
        //PlayerPrefs.SetInt("ShoesNumber", 0);

        Initialization();
        //Debug.Log("wear_HelmetKind " + wear_HelmetKind + " wear_GlovesKind " + wear_GlovesKind);

        //Debug.Log("2---CustomChange " + PlayerPrefs.GetString("CustomChange"));
       // Debug.Log("2---GoldUse " + PlayerPrefs.GetString("GoldUse"));
        //Debug.Log("2---ProfileChange " + PlayerPrefs.GetString("ProfileChange"));
        //Debug.Log("2---GameOnePlay " + PlayerPrefs.GetString("GameOnePlay"));
        //Debug.Log("2---ItemUse " + PlayerPrefs.GetString("ItemUse"));
        //Debug.Log("2---ItemPurchase " + PlayerPrefs.GetString("ItemPurchase"));

        MapOpen();
    }



    void MapOpen()
    {
        //Debug.Log("맵 초기화!!!!" + PlayerPrefs.GetString("AT_OpenMap_CourseName"));
        if (PlayerPrefs.GetString("AT_OpenMap_CourseName").Equals("Normal-1"))
        {
            PlayerPrefs.SetInt("AT_NewOpenMap_CourseNamber", 1);
        }
        else if (PlayerPrefs.GetString("AT_OpenMap_CourseName").Equals("Normal-2"))
        {
            PlayerPrefs.SetInt("AT_NewOpenMap_CourseNamber", 2);
        }
        else if (PlayerPrefs.GetString("AT_OpenMap_CourseName").Equals("Normal-3"))
        {
            PlayerPrefs.SetInt("AT_NewOpenMap_CourseNamber", 3);
        }
        else if (PlayerPrefs.GetString("AT_OpenMap_CourseName").Equals("Hard-1"))
        {
            PlayerPrefs.SetInt("AT_NewOpenMap_CourseNamber", 4);
        }
        else if (PlayerPrefs.GetString("AT_OpenMap_CourseName").Equals("Hard-2"))
        {
            PlayerPrefs.SetInt("AT_NewOpenMap_CourseNamber", 5);
        }
        else if (PlayerPrefs.GetString("AT_OpenMap_CourseName").Equals("Hard-3"))
        {
            PlayerPrefs.SetInt("AT_NewOpenMap_CourseNamber", 6);
        }
    }



    //미션 초기화 - 
    void MissionDataInit_AttendanceCheck()
    {
        oneCheck = PlayerPrefs.GetString("AT_OneCheck");

        //접속했다는 기록이 없으면 처음 로그인
        if (oneCheck == "")
        {
            PlayerPrefs.SetString("AT_OneCheck", toDayArr[1] + toDayArr[2]);
            oneCheck = PlayerPrefs.GetString("AT_OneCheck");
            //Debug.Log("OneCheck----------------" + oneCheck);
        }
        else if (oneCheck != "")
        {
            PlayerPrefs.SetString("AT_TwoCheck", toDayArr[1] + toDayArr[2]);
            twoCheck = PlayerPrefs.GetString("AT_TwoCheck");
            //Debug.Log("twoCheck----------------" + twoCheck);
        }

        //처음 접속 날짜와 두번째 접속 날짜가 다를 경우 - 출첵
        if (oneCheck != twoCheck)
        {
            //Debug.Log("처음 들어옴---------------------------");
            PlayerPrefs.SetString("AT_NewOpenMap", "No");
            //MapOpen();

            //--퀘스트를 위한 프리팹----------
            PlayerPrefs.SetString("AT_PlayQuestState", "No");  //플레이 했는지
            PlayerPrefs.SetString("AT_CurrPlayTime1", "F123/99:99:99"); //게임시간을 따로 하나 저장함-퀘스트미션완료를 위한 프리팹
            PlayerPrefs.SetString("AT_CurrPlayTime2", "F123/99:99:99");
            PlayerPrefs.SetString("AT_CurrPlayTime3", "F123/99:99:99");
            PlayerPrefs.SetString("AT_CurrPlayTime4", "F123/99:99:99");
            PlayerPrefs.SetString("AT_CurrPlayTime5", "F123/99:99:99");
            PlayerPrefs.SetString("AT_CurrPlayTime6", "F123/99:99:99");

            PlayerPrefs.SetFloat("AT_TodayMaxSpeed", 0);   //오늘 최고 속도
            PlayerPrefs.SetFloat("AT_Record_TodayKcal", 0);    //오늘의 칼로리 초기화
            PlayerPrefs.SetFloat("AT_Record_TodayKm", 0); //오늘 주행거리
            PlayerPrefs.SetString("AT_KcalBurnUp", "No");
            PlayerPrefs.SetString("AT_MaxSpeedToday", "No");

            PlayerPrefs.SetString("AT_StoreVisit", "No");
            PlayerPrefs.SetString("AT_InventoryVisit", "No");
            PlayerPrefs.SetString("AT_RankJonVisit", "No");
            PlayerPrefs.SetString("AT_MyInfoVisit", "No");

            PlayerPrefs.SetString("AT_ProfileChange", "No");
            PlayerPrefs.SetString("AT_GameOnePlay", "No");
            PlayerPrefs.SetString("AT_RankUp", "No");
            PlayerPrefs.SetString("AT_CustomChange", "No");
            PlayerPrefs.SetString("AT_GoldUse", "No");
            PlayerPrefs.SetString("AT_ItemUse", "No");
            PlayerPrefs.SetString("AT_ItemPurchase", "No");

            PlayerPrefs.SetString("AT_TwoCheck", toDayArr[1] + toDayArr[2]);
            twoCheck = PlayerPrefs.GetString("AT_TwoCheck");

            //두번째 접속한 날짜를 앞으로 옮겨준다. 그래야 다음 들어왔을 때 또 비교가능
            PlayerPrefs.SetString("AT_OneCheck", PlayerPrefs.GetString("AT_TwoCheck"));

            //Debug.Log("11OneCheck: " + PlayerPrefs.GetString("AT_OneCheck") + " TwoCheck " + PlayerPrefs.GetString("AT_OneCheck"));
            //Debug.Log("1 : " + oneCheck + " :2: " + twoCheck);
            // 날짜 데이터 서버 저장
            // OneCheck : 01-01 , TwoCheck : "" > 맨 처음 로그인
            // OneCheck : 01-01 , TwoCheck : 01-02 > 처음 이후 로그인 >>> OneCheck : 01-02 , TwoCheck : 01-02 저장
            ServerManager.Instance.UpdateConnectedState("TwoCheck", twoCheck);
            ServerManager.Instance.connectedStateCheck.oneCheck = oneCheck;
            ServerManager.Instance.connectedStateCheck.twoCheck = twoCheck;
        }
        else
        {
            //Debug.Log("22OneCheck: " + PlayerPrefs.GetString("AT_OneCheck") + " TwoCheck " + PlayerPrefs.GetString("AT_OneCheck"));
            //Debug.Log("11 : " + oneCheck + " :2: " + twoCheck);
            //Debug.Log("로비 데이터에 문제임????");
            //현재는 없음
            ServerManager.Instance.UpdateConnectedState("TwoCheck", twoCheck);
            ServerManager.Instance.connectedStateCheck.oneCheck = oneCheck;
            ServerManager.Instance.connectedStateCheck.twoCheck = twoCheck;
            //ServerManager.Instance.GetTodayQuest(PlayerPrefs.GetString("Player_ID"));
            

            StartCoroutine(_Get_MissionData());
        }
    }

    IEnumerator _Get_MissionData()
    {
        yield return new WaitUntil(() => ServerManager.Instance.isTodayQuestStackCompleted);
        ServerManager.Instance.isTodayQuestStackCompleted = false;

        int useNum = PlayerPrefs.GetInt("AT_TodayQuest3");
        if(useNum == 0)
            PlayerPrefs.SetString("AT_ProfileChange", ServerManager.Instance.myTodayQuest[2].quest_State);
        else if (useNum == 1)
            PlayerPrefs.SetString("AT_GameOnePlay", ServerManager.Instance.myTodayQuest[2].quest_State);
        else if (useNum == 2)
            PlayerPrefs.SetString("AT_RankUp", ServerManager.Instance.myTodayQuest[2].quest_State);
        else if (useNum == 3)
            PlayerPrefs.SetString("AT_CustomChange", ServerManager.Instance.myTodayQuest[2].quest_State);
        else if (useNum == 4)
            PlayerPrefs.SetString("AT_GoldUse", ServerManager.Instance.myTodayQuest[2].quest_State);
        else if (useNum == 5)
            PlayerPrefs.SetString("AT_ItemUse", ServerManager.Instance.myTodayQuest[2].quest_State);
        else if (useNum == 6)
            PlayerPrefs.SetString("AT_ItemPurchase", ServerManager.Instance.myTodayQuest[2].quest_State);


        int visitNum = PlayerPrefs.GetInt("AT_TodayQuest2");
        if (visitNum == 0)
            PlayerPrefs.SetString("AT_StoreVisit", ServerManager.Instance.myTodayQuest[1].quest_State);
        else if (visitNum == 0)
            PlayerPrefs.SetString("AT_MyInfoVisit", ServerManager.Instance.myTodayQuest[1].quest_State);
        else if (visitNum == 0)
            PlayerPrefs.SetString("AT_InventoryVisit", ServerManager.Instance.myTodayQuest[1].quest_State);
        else if (visitNum == 0)
            PlayerPrefs.SetString("AT_RankJonVisit", ServerManager.Instance.myTodayQuest[1].quest_State);

    }

    //오늘 날짜 초기화
    void GetTodayInitialization()
    {
        //toDay = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        toDayArr = new string[4];
        toDayArr[0] = DateTime.Now.ToString("yyyy");
        toDayArr[1] = DateTime.Now.ToString("MM");
        toDayArr[2] = DateTime.Now.ToString("dd");
        toDayArr[3] = DateTime.Now.ToString("HH-mm-ss");
    }


    void Initialization()
    {
        if(PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            womanPlayer = GameObject.Find("Woman");
            womanctrl_scrip = womanPlayer.GetComponent<WomanCtrl>();
        }
        else if(PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            manPlayer = GameObject.Find("Man");
            manctrl_scrip = manPlayer.GetComponent<ManCtrl>();
        }

        //총 주행거리
        PlayerPrefs.SetFloat("AT_Record_TotalKm", ServerManager.Instance.records.record_TotalKm);


        //프로필 이미지
        profile = PlayerPrefs.GetInt("AT_Player_Profile");

        //피부 색 및 머리 색
        skinColor = PlayerPrefs.GetString("AT_Player_SkinColor");
        hairColor = PlayerPrefs.GetString("AT_Player_HairColor");

        //장착 아이템 
        wear_GlovesKind = PlayerPrefs.GetString("AT_Wear_GlovesKind");
        wear_HelmetKind = PlayerPrefs.GetString("AT_Wear_HelmetKind");
        wear_Jacketkind = PlayerPrefs.GetString("AT_Wear_JacketKind");
        wear_PantsKind = PlayerPrefs.GetString("AT_Wear_PantsKind");
        wear_ShoesKind = PlayerPrefs.GetString("AT_Wear_ShoesKind");
        wear_BicycleKind = PlayerPrefs.GetString("AT_Wear_BicycleKind");//자전거
        //Debug.Log(wear_BicycleKind + " wear_HelmetKind " + wear_HelmetKind + " wear_GlovesKind " + wear_GlovesKind);
        

        //각 아이템 배열 넘버 저장
        id_hairNum = PlayerPrefs.GetInt("AT_HairNumber");
        id_bodyNum = PlayerPrefs.GetInt("AT_BodyNumber");
        id_jacketNum = PlayerPrefs.GetInt("AT_JacketNumber");
        id_pantsNum = PlayerPrefs.GetInt("AT_PantsNumber");
        id_shoesNum = PlayerPrefs.GetInt("AT_ShoesNumber");
        id_helmetNum = PlayerPrefs.GetInt("AT_HelmetNumber");
        id_glovesNum = PlayerPrefs.GetInt("AT_GlovesNumber");
        //Debug.Log("id_bodyNum " + id_bodyNum);

        //Debug.Log(id_hairNum + "  JacketNumber " + id_jacketNum + " PantsNumber " + id_pantsNum + " wear_HelmetKind " + id_helmetNum + " wear_GlovesKind " + id_glovesNum);

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            //로비씬 들어올때마다 플레이어 초기화
            womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
            womanctrl_scrip.PlayerSkinColorInit(skinColor);
            womanctrl_scrip.PlayerHairColorInit(hairColor);

            //헬맷 - 장갑 착용 여부
            if (id_helmetNum == 100)
                womanctrl_scrip.HelmetSetting(false);
            else
                womanctrl_scrip.HelmetSetting(true);

            if (id_glovesNum == 100)
                womanctrl_scrip.GlovesSetting(false);
            else
                womanctrl_scrip.GlovesSetting(true);

            WomanPlayerItemActionShow();
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            //로비씬 들어올때마다 플레이어 초기화
            manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
            manctrl_scrip.PlayerSkinColorInit(skinColor);
            manctrl_scrip.PlayerHairColorInit(hairColor);

            //헬맷 - 장갑 착용 여부
            if (id_helmetNum == 100)
                manctrl_scrip.HelmetSetting(false);
            else
                manctrl_scrip.HelmetSetting(true);

            if (id_glovesNum == 100)
                manctrl_scrip.GlovesSetting(false);
            else
                manctrl_scrip.GlovesSetting(true);

            ManPlayerItemActionShow();
        }
    }

    //여자 플레이어 장착 활성화/비활성화
    void WomanPlayerItemActionShow()
    {
        //Debug.Log("로비에 들어오면 옷 변경");

        //머리
        if (id_hairNum == 0)
            womanctrl_scrip.HairSetting(true, false, false);
        else if (id_hairNum == 1)
            womanctrl_scrip.HairSetting(false, true, false);
        else if (id_hairNum == 2)
            womanctrl_scrip.HairSetting(false, false, true);

        //상의
        if (id_jacketNum == 0)
        {
            womanctrl_scrip.JacketSetting(true, false, false);
            womanctrl_scrip.JacketTextrueSetting(PlayerPrefs.GetString("AT_Wear_JacketStyleName"), 0);
        } 
        else if (id_jacketNum == 1)
        {
            //Debug.Log("Wear_JacketStyleName " + PlayerPrefs.GetString("AT_Wear_JacketStyleName"));
            womanctrl_scrip.JacketSetting(false, true, false);
            womanctrl_scrip.JacketTextrueSetting(PlayerPrefs.GetString("AT_Wear_JacketStyleName"), 1);
        }
        else if (id_jacketNum == 2)
        {
            womanctrl_scrip.JacketSetting(false, false, true);
            womanctrl_scrip.JacketTextrueSetting(PlayerPrefs.GetString("AT_Wear_JacketStyleName"), 2);
        }

        //하의
        if (id_pantsNum == 0)
        {
            womanctrl_scrip.PantsSetting(true, false);
            womanctrl_scrip.PantsTextrueSetting(PlayerPrefs.GetString("AT_Wear_PantsStyleName"), 0);
        }
        else if (id_pantsNum == 1)
        {
            womanctrl_scrip.PantsSetting(false, true);
            womanctrl_scrip.PantsTextrueSetting(PlayerPrefs.GetString("AT_Wear_PantsStyleName"), 1);
        }  

        //신발
        if (id_shoesNum == 0)
        {
            womanctrl_scrip.ShoesSetting(true, false);
            womanctrl_scrip.ShoesTextrueSetting(PlayerPrefs.GetString("AT_Wear_ShoesStyleName"), 0);
        }
        else if (id_shoesNum == 1)
        {
            womanctrl_scrip.ShoesSetting(false, true);
            womanctrl_scrip.ShoesTextrueSetting(PlayerPrefs.GetString("AT_Wear_ShoesStyleName"), 2);
        }

        //헬맷
        if (id_helmetNum == 0)
        {
            womanctrl_scrip.HelmetSetting(true);
            womanctrl_scrip.HelmetTextrueSetting(PlayerPrefs.GetString("AT_Wear_HelmetStyleName"), 0);
        }

        //장갑
        if (id_glovesNum == 0)
        {
            womanctrl_scrip.GlovesSetting(true);
            womanctrl_scrip.GlovesTextrueSetting(PlayerPrefs.GetString("AT_Wear_GlovesStyleName"), 0);
        }
        //Debug.Log("도대체 _ : " + PlayerPrefs.GetString("AT_Wear_BicycleStyleName"));
        //자전거
        womanctrl_scrip.BicycleTextrueSetting(PlayerPrefs.GetString("AT_Wear_BicycleStyleName"), 0);
    }

    //남자 플레이어 장착 활성화/비활성화
    void ManPlayerItemActionShow()
    {
        //Debug.Log("로비에 들어오면 옷 변경" + id_hairNum);

        //머리
        if (id_hairNum == 0)
            manctrl_scrip.HairSetting(true, false, false, false);
        else if (id_hairNum == 1)
            manctrl_scrip.HairSetting(false, true, false, false);
        else if (id_hairNum == 2)
            manctrl_scrip.HairSetting(false, false, true, false);
        else if (id_hairNum == 3)
            manctrl_scrip.HairSetting(false, false, false, true);

        //상의
        if (id_jacketNum == 0)
        {
            manctrl_scrip.JacketSetting(true, false, false);
            manctrl_scrip.JacketTextrueSetting(PlayerPrefs.GetString("AT_Wear_JacketStyleName"), 0);
        }
        else if (id_jacketNum == 1)
        {
            //Debug.Log("Wear_JacketStyleName " + PlayerPrefs.GetString("AT_Wear_JacketStyleName"));
            manctrl_scrip.JacketSetting(false, true, false);
            manctrl_scrip.JacketTextrueSetting(PlayerPrefs.GetString("AT_Wear_JacketStyleName"), 1);
        }
        else if (id_jacketNum == 2)
        {
            manctrl_scrip.JacketSetting(false, false, true);
            manctrl_scrip.JacketTextrueSetting(PlayerPrefs.GetString("AT_Wear_JacketStyleName"), 2);
        }

        //하의
        if (id_pantsNum == 0)
        {
            manctrl_scrip.PantsSetting(true, false);
            manctrl_scrip.PantsTextrueSetting(PlayerPrefs.GetString("AT_Wear_PantsStyleName"), 0);
        }
        else if (id_pantsNum == 1)
        {
            manctrl_scrip.PantsSetting(false, true);
            manctrl_scrip.PantsTextrueSetting(PlayerPrefs.GetString("AT_Wear_PantsStyleName"), 1);
        }

        //신발
        if (id_shoesNum == 0)
        {
            manctrl_scrip.ShoesSetting(true, false);
            manctrl_scrip.ShoesTextrueSetting(PlayerPrefs.GetString("AT_Wear_ShoesStyleName"), 0);
        }
        else if (id_shoesNum == 1)
        {
            manctrl_scrip.ShoesSetting(false, true);
            manctrl_scrip.ShoesTextrueSetting(PlayerPrefs.GetString("AT_Wear_ShoesStyleName"), 2);
        }

        //헬맷
        if (id_helmetNum == 0)
        {
            manctrl_scrip.HelmetSetting(true);
            manctrl_scrip.HelmetTextrueSetting(PlayerPrefs.GetString("AT_Wear_HelmetStyleName"), 0);
        }

        //장갑
        if (id_glovesNum == 0)
        {
            manctrl_scrip.GlovesSetting(true);
            manctrl_scrip.GlovesTextrueSetting(PlayerPrefs.GetString("AT_Wear_GlovesStyleName"), 0);
        }

        //자전거
        manctrl_scrip.BicycleTextrueSetting(PlayerPrefs.GetString("AT_Wear_BicycleStyleName"), 0);
    }

    public void ProfileImageChange(Image _profileImg)
    {
        _profileImg.sprite = Resources.Load<Sprite>("Profile/" + profile);
    }


    //숫자 콤마 찍는 함수
    public string CommaText(int _data)
    {
        if (_data != 0)
            return string.Format("{0:#,###}", _data);
        else
            return "0";
    }

    //게임 종료
    public void GameEndButtonOn()
    {
        Application.Quit(); //게임 종료
    }

    private void Update()
    {
        if (Application.platform.Equals(RuntimePlatform.Android))
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                gameEndPopup.SetActive(true);
            }
        }
    }
}
