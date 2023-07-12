using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewCharacterChoice_UI : MonoBehaviour
{
    public BusanMapStringClass stringClass;
    public static NewCharacterChoice_UI instance { get; private set; }

    [Header("플레이어")]
    public GameObject womanPlayer;
    public GameObject manPlayer;

    public ToggleGroup sexToggleGroup;  //성별 

    //로그인 상태 여부
    string loginState;


    [Header("[데이터 저장 변수]")]
    string sexStr;


    //성별
    public Toggle sexCurrentSelection
    {
        get { return sexToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        stringClass = new BusanMapStringClass();
        loginState = PlayerPrefs.GetString("Busan_Player_LoginState");    //로그인 상태 여부

        //성별 모델 초기화
        Sex_ModelLook(true, false);

        PlayerPrefabsReset();   //기존에 있던 플레이어프리팹의 정보를 초기화 시켜준다.
    }

    void PlayerPrefabsReset()
    {
        //로그인 상태 여부
        if (loginState == "GoogleNickName")
            PlayerPrefs.SetString("Busan_Player_LoginState", "GoogleCharacter");
        else if (loginState == "GatewaysNickName")
            PlayerPrefs.SetString("Busan_Player_LoginState", "GatewaysCharacter");


        //튜토리얼 
        PlayerPrefs.SetString("Busan_LobbyTutorial", "Start");
        PlayerPrefs.SetString("Busan_QuestTutorial", "Start");
        PlayerPrefs.SetString("Busan_InGameTutorial", "Start");
        PlayerPrefs.SetString("Busan_MapChoiceTutorial", "Start");

        //캐릭터 정보
        sexStr = "Woman"; //기본여자


        PlayerPrefs.SetString("Busan_Player_Sex", sexStr); 
        //PlayerPrefs.SetString("Busan_Player_Face", faceStr);
        //PlayerPrefs.SetString("Busan_Player_SkinColor", faceColorStr);
        //PlayerPrefs.SetString("Busan_Player_Hair", hairStyleStr);
        //PlayerPrefs.SetString("Busan_Player_HairColor", hairColorStr);
        PlayerPrefs.SetInt("Busan_Player_Profile", 1);

        //UI정보
        PlayerPrefs.SetInt("Busan_Player_Gold", 1000);
        PlayerPrefs.SetInt("Busan_Player_Level", 1);
        PlayerPrefs.SetInt("Busan_Player_CurrExp", 0);
        PlayerPrefs.SetInt("Busan_Player_TakeExp", 0);

        //기록정보
        PlayerPrefs.SetFloat("Busan_TodayMaxSpeed", 0f);
        PlayerPrefs.SetFloat("Busan_Record_MaxSpeed", 0f);
        PlayerPrefs.SetFloat("Busan_Record_TodayKcal", 0f);
        PlayerPrefs.SetFloat("Busan_Record_TodayKm", 0f);
        PlayerPrefs.SetFloat("Busan_Record_TotalKm", 0f);
        PlayerPrefs.SetString("Busan_OpenMap_CourseName", stringClass.BUSAN_MAP_COURSE_NOR1);    //현재 오픈되어있는 맵
        PlayerPrefs.SetString("Busan_OpenMap_CourseName_Green", stringClass.BUSAN_MAP_COURSE_NOR1); //그린라인 현재 오픈된 맵
        PlayerPrefs.SetInt("Busan_NewOpenMap_CourseNamber", 1);
        PlayerPrefs.SetInt("Busan_NewOpenMap_CourseNamber_Green", 1);

        //장착 아이템 스타일
        //PlayerPrefs.SetString("Busan_Wear_HairStyleName", hairStyleStr);
        //PlayerPrefs.SetString("Busan_Wear_JacketStyleName", "BasicNasi");
        //PlayerPrefs.SetString("Busan_Wear_PantsStyleName", "BasicShort");
        //PlayerPrefs.SetString("Busan_Wear_ShoesStyleName", "BasicSandal");
        //PlayerPrefs.SetString("Busan_Wear_BicycleStyleName", "BasicBicycle");
        //PlayerPrefs.SetString("Busan_Wear_HelmetStyleName", "No");
        //PlayerPrefs.SetString("Busan_Wear_GlovesStyleName", "No");
        //PlayerPrefs.SetString("Busan_Wear_BicycleStyleName", "BasicBicycle");

        //아이템 장착 종류
        //PlayerPrefs.SetString("Busan_Wear_HairKind", "ItemID_Hair");
        //PlayerPrefs.SetString("Busan_Wear_GlovesKind", "No");
        //PlayerPrefs.SetString("Busan_Wear_HelmetKind", "No");
        //PlayerPrefs.SetString("Busan_Wear_JacketKind", "ItemID_Nasi");
        //PlayerPrefs.SetString("Busan_Wear_PantsKind", "ItemID_Short");
        //PlayerPrefs.SetString("Busan_Wear_ShoesKind", "ItemID_Sandal");
        //PlayerPrefs.SetString("Busan_Wear_BicycleKind", "ItemID_Bicycle");

        //각 커스텀 숫자 저장
        //PlayerPrefs.SetInt("Busan_JacketNumber", 0);
        //PlayerPrefs.SetInt("Busan_PantsNumber", 0);
        //PlayerPrefs.SetInt("Busan_ShoesNumber", 0);
        //PlayerPrefs.SetInt("Busan_HelmetNumber", 100);
        //PlayerPrefs.SetInt("Busan_GlovesNumber", 100);
        //PlayerPrefs.SetInt("Busan_BicycleNumber", 0);

        //출석체크 형식
        PlayerPrefs.SetString("Busan_OneCennectDay", "");   //미션씬에서 체크
        PlayerPrefs.SetString("Busan_TwoCennedtDay", "");
        PlayerPrefs.SetString("Busan_OneCheck", "");    //로비에서 체크
        PlayerPrefs.SetString("Busan_TwoCheck", "");

        //경기 기록
        PlayerPrefs.SetString("Busan_Asia Normal 1Course", "");
        PlayerPrefs.SetString("Busan_Asia Normal 2Course", "");
        PlayerPrefs.SetString("Busan_Asia Normal 3Course", "");
        PlayerPrefs.SetString("Busan_Asia Hard 1Course", "");
        PlayerPrefs.SetString("Busan_Asia Hard 2Course", "");
        PlayerPrefs.SetString("Busan_Asia Hard 3Course", "");
        //그린라인, 경기 기록
        PlayerPrefs.SetString("Busan_Green Normal 1Course", "");
        PlayerPrefs.SetString("Busan_Green Normal 2Course", "");
        PlayerPrefs.SetString("Busan_Green Normal 3Course", "");
        PlayerPrefs.SetString("Busan_Green Hard 1Course", "");
        PlayerPrefs.SetString("Busan_Green Hard 2Course", "");
        PlayerPrefs.SetString("Busan_Green Hard 3Course", "");

        //----- 아이템 & 퀘스트---------
        //아이템 갯수
        PlayerPrefs.SetInt("Busan_ExpPlusAmount", 0);
        PlayerPrefs.SetInt("Busan_ExpUpAmount", 0);
        PlayerPrefs.SetInt("Busan_CoinUpAmount", 0);
        PlayerPrefs.SetInt("Busan_SpeedUpAmount", 0);

        //일일퀘스트 실행 여부
        PlayerPrefs.SetInt("Busan_TodayQuest1", 0); //접속하기
        PlayerPrefs.SetInt("Busan_TodayQuest2", 0); //방문하기
        PlayerPrefs.SetInt("Busan_TodayQuest3", 0); //게임미션
        PlayerPrefs.SetInt("Busan_TodayQuest4", 0);    //칼로리소모
        PlayerPrefs.SetInt("Busan_TodayQuest5", 0);    //최고속도

        //방문하기 퀘스트
        PlayerPrefs.SetString("Busan_StoreVisit", "No"); //No, Yes, MissionOk
        PlayerPrefs.SetString("Busan_InventoryVisit", "No");
        PlayerPrefs.SetString("Busan_RankJonVisit", "No");
        PlayerPrefs.SetString("Busan_MyInfoVisit", "No");

        //게임미션 퀘스트
        PlayerPrefs.SetString("Busan_ProfileChange", "No");   //프로필변경 //No, Yes, MissionOk
        PlayerPrefs.SetString("Busan_GameOnePlay", "No"); //게임한번하기
        PlayerPrefs.SetString("Busan_RankUp", "No");  //순위올리기
        PlayerPrefs.SetString("Busan_CustomChange", "No");    //커스텈변경
        PlayerPrefs.SetString("Busan_GoldUse", "No"); //코인사용
        PlayerPrefs.SetString("Busan_ItemUse", "No"); //아이템사용(소모품)
        PlayerPrefs.SetString("Busan_ItemPurchase", "No");    //아이템구매(소무품)

        //맵 퀘스트 완주형
        PlayerPrefs.SetInt("Busan_AsiaNormal1Finish", 0);    //퀘스트 성공 횟수 0, 1,2,3,4
        PlayerPrefs.SetInt("Busan_AsiaNormal2Finish", 0);
        PlayerPrefs.SetInt("Busan_AsiaNormal3Finish", 0);
        PlayerPrefs.SetInt("Busan_AsiaHard1Finish", 0);
        PlayerPrefs.SetInt("Busan_AsiaHard2Finish", 0);
        PlayerPrefs.SetInt("Busan_AsiaHard3Finish", 0);
        //그린라인, 맵 퀘스트 완주형
        PlayerPrefs.SetInt("Busan_GreenNormal1Finish", 0);    //퀘스트 성공 횟수 0, 1,2,3,4
        PlayerPrefs.SetInt("Busan_GreenNormal2Finish", 0);
        PlayerPrefs.SetInt("Busan_GreenNormal3Finish", 0);
        PlayerPrefs.SetInt("Busan_GreenHard1Finish", 0);
        PlayerPrefs.SetInt("Busan_GreenHard2Finish", 0);
        PlayerPrefs.SetInt("Busan_GreenHard3Finish", 0);

        //맵 퀘스트 완주 횟수저장
        PlayerPrefs.SetInt("Busan_AsiaNormal1FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_AsiaNormal2FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_AsiaNormal3FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_AsiaHard1FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_AsiaHard2FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_AsiaHard3FinishAmount", 0);
        //그린라인, 맵 퀘스트 완주 횟수저장
        PlayerPrefs.SetInt("Busan_GreenNormal1FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_GreenNormal2FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_GreenNormal3FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_GreenHard1FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_GreenHard2FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_GreenHard3FinishAmount", 0);

        //맵 퀘스트 시간 제한 완주형
        PlayerPrefs.SetInt("Busan_AsiaNormalTimeLimitFinish1", 0); // 0~13
        PlayerPrefs.SetInt("Busan_AsiaNormalTimeLimitFinish2", 0);
        PlayerPrefs.SetInt("Busan_AsiaNormalTimeLimitFinish3", 0);
        PlayerPrefs.SetInt("Busan_AsiaHardTimeLimitFinish1", 0);
        PlayerPrefs.SetInt("Busan_AsiaHardTimeLimitFinish2", 0);
        PlayerPrefs.SetInt("Busan_AsiaHardTimeLimitFinish3", 0);
        //그린라인, 맵 퀘스트 시간 제한 완주형
        PlayerPrefs.SetInt("Busan_GreenNormalTimeLimitFinish1", 0); // 0~13
        PlayerPrefs.SetInt("Busan_GreenNormalTimeLimitFinish2", 0);
        PlayerPrefs.SetInt("Busan_GreenNormalTimeLimitFinish3", 0);
        PlayerPrefs.SetInt("Busan_GreenHardTimeLimitFinish1", 0);
        PlayerPrefs.SetInt("Busan_GreenHardTimeLimitFinish2", 0);
        PlayerPrefs.SetInt("Busan_GreenHardTimeLimitFinish3", 0);
        //맵 퀘스트 - 커스텀 보상
        PlayerPrefs.SetString("Busan_AllOneFinish", "No");    //No, Yes, MissionOk
        PlayerPrefs.SetString("Busan_AllTenFinish", "No");
        PlayerPrefs.SetString("Busan_AllTwentyFinish", "No");
        PlayerPrefs.SetString("Busan_Distance500Km", "No");
        PlayerPrefs.SetString("Busan_Distance1000Km", "No");
        PlayerPrefs.SetString("Busan_Distance1500Km", "No");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //성별선택(남여)
    public void Sex_Choice()
    {
        if (sexToggleGroup.ActiveToggles().Any())
        {
            //여자
            if (sexCurrentSelection.name == "WomanToggle")
            {
                Debug.Log("여자");
                Sex_ModelLook(true, false);
                sexStr = "Woman";
            }
            //남자
            else if (sexCurrentSelection.name == "ManToggle")
            {
                Debug.Log("남자");
                Sex_ModelLook(false, true);
                sexStr = "Man";
            }
        }
    }

    //모델 활서오하
    void Sex_ModelLook(bool _woman, bool _man)
    {
        womanPlayer.SetActive(_woman);
        manPlayer.SetActive(_man);
    }


    //설정완료버튼 
    public void SettingEndSaveOnButton()
    {
        StartCoroutine(_SettingEndSaveOnButton());
    }

    IEnumerator _SettingEndSaveOnButton()
    {
        //로그인 상태 여부
        if (loginState == "GoogleCharacter")
            PlayerPrefs.SetString("Busan_Player_LoginState", "Google");
        else if (loginState == "GatewaysCharacter")
            PlayerPrefs.SetString("Busan_Player_LoginState", "Gateways");

        // 로그인 상태 서버에 업데이트
        ServerManager.Instance.UserLoginState_Update();

        //튜토리얼 
        PlayerPrefs.SetString("Busan_LobbyTutorial", "Start");
        PlayerPrefs.SetString("Busan_QuestTutorial", "Start");
        PlayerPrefs.SetString("Busan_InGameTutorial", "Start");
        PlayerPrefs.SetString("Busan_MapChoiceTutorial", "Start");

        //캐릭터 정보
        PlayerPrefs.SetString("Busan_Player_Sex", sexStr);
        //PlayerPrefs.SetString("Busan_Player_Face", faceStr);
        //PlayerPrefs.SetString("Busan_Player_SkinColor", faceColorStr);
        //PlayerPrefs.SetString("Busan_Player_Hair", hairStyleStr);
        //PlayerPrefs.SetString("Busan_Player_HairColor", hairColorStr);
        PlayerPrefs.SetInt("Busan_Player_Profile", 1); //프로필 기본 1

        //UI정보
        PlayerPrefs.SetInt("Busan_Player_Gold", 1000);
        PlayerPrefs.SetInt("Busan_Player_Level", 1);
        PlayerPrefs.SetInt("Busan_Player_CurrExp", 0);
        PlayerPrefs.SetInt("Busan_Player_TakeExp", 0);

        //기록정보
        PlayerPrefs.SetFloat("Busan_TodayMaxSpeed", 0f);
        PlayerPrefs.SetFloat("Busan_Record_MaxSpeed", 0f);
        PlayerPrefs.SetFloat("Busan_Record_TodayKcal", 0f);
        PlayerPrefs.SetFloat("Busan_Record_TodayKm", 0f);
        PlayerPrefs.SetFloat("Busan_Record_TotalKm", 0f);
        PlayerPrefs.SetString("Busan_OpenMap_CourseName", stringClass.BUSAN_MAP_COURSE_NOR1);    //현재 오픈되어있는 맵
        PlayerPrefs.SetString("Busan_OpenMap_CourseName_Green", stringClass.BUSAN_MAP_COURSE_NOR1); //그린라인 현재 오픈된 맵
        PlayerPrefs.SetInt("Busan_NewOpenMap_CourseNamber", 1);
        PlayerPrefs.SetInt("Busan_NewOpenMap_CourseNamber_Green", 1);

        //장착 아이템 스타일
        //PlayerPrefs.SetString("Busan_Wear_HairStyleName", hairStyleStr);
        //PlayerPrefs.SetString("Busan_Wear_JacketStyleName", "BasicNasi");
        //PlayerPrefs.SetString("Busan_Wear_PantsStyleName", "BasicShort");
        //PlayerPrefs.SetString("Busan_Wear_ShoesStyleName", "BasicSandal");
        //PlayerPrefs.SetString("Busan_Wear_BicycleStyleName", "BasicBicycle");
        //PlayerPrefs.SetString("Busan_Wear_HelmetStyleName", "No");
        //PlayerPrefs.SetString("Busan_Wear_GlovesStyleName", "No");

        //아이템 장착 종류
        //PlayerPrefs.SetString("Busan_Wear_HairKind", "ItemID_Hair");
        //PlayerPrefs.SetString("Busan_Wear_GlovesKind", "No");
        //PlayerPrefs.SetString("Busan_Wear_HelmetKind", "No");
        //PlayerPrefs.SetString("Busan_Wear_JacketKind", "ItemID_Nasi");
        //PlayerPrefs.SetString("Busan_Wear_PantsKind", "ItemID_Short");
        //PlayerPrefs.SetString("Busan_Wear_ShoesKind", "ItemID_Sandal");
        //PlayerPrefs.SetString("Busan_Wear_BicycleKind", "ItemID_Bicycle");

        //각 커스텀 숫자 저장
        //PlayerPrefs.SetInt("Busan_JacketNumber", 0);
        //PlayerPrefs.SetInt("Busan_PantsNumber", 0);
        //PlayerPrefs.SetInt("Busan_ShoesNumber", 0);
        //PlayerPrefs.SetInt("Busan_HelmetNumber", 100);
        //PlayerPrefs.SetInt("Busan_GlovesNumber", 100);
        //PlayerPrefs.SetInt("Busan_BicycleNumber", 0);

        //출석체크 형식 : 빈문자열 넣기
        PlayerPrefs.SetString("Busan_OneCennectDay", "");   //미션씬에서 체크
        PlayerPrefs.SetString("Busan_TwoCennedtDay", "");
        PlayerPrefs.SetString("Busan_OneCheck", "");    //로비에서 체크
        PlayerPrefs.SetString("Busan_TwoCheck", "");

        //----- 아이템 & 퀘스트---------
        //아이템 갯수
        PlayerPrefs.SetInt("Busan_ExpPlusAmount", 0);
        PlayerPrefs.SetInt("Busan_ExpUpAmount", 0);
        PlayerPrefs.SetInt("Busan_CoinUpAmount", 0);
        PlayerPrefs.SetInt("Busan_SpeedUpAmount", 0);

        //일일퀘스트 실행 여부
        PlayerPrefs.SetInt("Busan_TodayQuest1", 0); //접속하기
        PlayerPrefs.SetInt("Busan_TodayQuest2", 0); //방문하기
        PlayerPrefs.SetInt("Busan_TodayQuest3", 0); //게임미션
        PlayerPrefs.SetInt("Busan_TodayQuest4", 0);    //칼로리소모
        PlayerPrefs.SetInt("Busan_TodayQuest5", 0);    //최고속도

        //방문하기 퀘스트
        PlayerPrefs.SetString("Busan_StoreVisit", "No"); //No, Yes, MissionOk
        PlayerPrefs.SetString("Busan_InventoryVisit", "No");
        PlayerPrefs.SetString("Busan_RankJonVisit", "No");
        PlayerPrefs.SetString("Busan_MyInfoVisit", "No");

        //게임미션 퀘스트
        PlayerPrefs.SetString("Busan_ProfileChange", "No");   //프로필변경 //No, Yes, MissionOk
        PlayerPrefs.SetString("Busan_GameOnePlay", "No"); //게임한번하기
        PlayerPrefs.SetString("Busan_RankUp", "No");  //순위올리기
        PlayerPrefs.SetString("Busan_CustomChange", "No");    //커스텈변경
        PlayerPrefs.SetString("Busan_GoldUse", "No"); //코인사용
        PlayerPrefs.SetString("Busan_ItemUse", "No"); //아이템사용(소모품)
        PlayerPrefs.SetString("Busan_ItemPurchase", "No");    //아이템구매(소무품)

        //경기 기록
        PlayerPrefs.SetString("Busan_Asia Normal 1Course", "");
        PlayerPrefs.SetString("Busan_Asia Normal 2Course", "");
        PlayerPrefs.SetString("Busan_Asia Normal 3Course", "");
        PlayerPrefs.SetString("Busan_Asia Hard 1Course", "");
        PlayerPrefs.SetString("Busan_Asia Hard 2Course", "");
        PlayerPrefs.SetString("Busan_Asia Hard 3Course", "");

        //그린라인, 경기 기록
        PlayerPrefs.SetString("Busan_Green Normal 1Course", "");
        PlayerPrefs.SetString("Busan_Green Normal 2Course", "");
        PlayerPrefs.SetString("Busan_Green Normal 3Course", "");
        PlayerPrefs.SetString("Busan_Green Hard 1Course", "");
        PlayerPrefs.SetString("Busan_Green Hard 2Course", "");
        PlayerPrefs.SetString("Busan_Green Hard 3Course", "");

        //맵 퀘스트 완주형
        PlayerPrefs.SetInt("Busan_AsiaNormal1Finish", 0);    //퀘스트 성공 횟수 0, 1,2,3,4
        PlayerPrefs.SetInt("Busan_AsiaNormal2Finish", 0);
        PlayerPrefs.SetInt("Busan_AsiaNormal3Finish", 0);
        PlayerPrefs.SetInt("Busan_AsiaHard1Finish", 0);
        PlayerPrefs.SetInt("Busan_AsiaHard2Finish", 0);
        PlayerPrefs.SetInt("Busan_AsiaHard3Finish", 0);
        //그린라인, 맵 퀘스트 완주 횟수저장
        PlayerPrefs.SetInt("Busan_GreenNormal1FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_GreenNormal2FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_GreenNormal3FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_GreenHard1FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_GreenHard2FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_GreenHard3FinishAmount", 0);

        //맵 퀘스트 완주 횟수저장
        PlayerPrefs.SetInt("Busan_AsiaNormal1FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_AsiaNormal2FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_AsiaNormal3FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_AsiaHard1FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_AsiaHard2FinishAmount", 0);
        PlayerPrefs.SetInt("Busan_AsiaHard3FinishAmount", 0);
        //그린라인, 맵 퀘스트 완주형
        PlayerPrefs.SetInt("Busan_GreenNormal1Finish", 0);    //퀘스트 성공 횟수 0, 1,2,3,4
        PlayerPrefs.SetInt("Busan_GreenNormal2Finish", 0);
        PlayerPrefs.SetInt("Busan_GreenNormal3Finish", 0);
        PlayerPrefs.SetInt("Busan_GreenHard1Finish", 0);
        PlayerPrefs.SetInt("Busan_GreenHard2Finish", 0);
        PlayerPrefs.SetInt("Busan_GreenHard3Finish", 0);

        //맵 퀘스트 시간 제한 완주형
        PlayerPrefs.SetInt("Busan_AsiaNormalTimeLimitFinish1", 0); // 0~13
        PlayerPrefs.SetInt("Busan_AsiaNormalTimeLimitFinish2", 0);
        PlayerPrefs.SetInt("Busan_AsiaNormalTimeLimitFinish3", 0);
        PlayerPrefs.SetInt("Busan_AsiaHardTimeLimitFinish1", 0);
        PlayerPrefs.SetInt("Busan_AsiaHardTimeLimitFinish2", 0);
        PlayerPrefs.SetInt("Busan_AsiaHardTimeLimitFinish3", 0);
        //그린라인, 맵 퀘스트 시간 제한 완주형
        PlayerPrefs.SetInt("Busan_GreenNormalTimeLimitFinish1", 0); // 0~13
        PlayerPrefs.SetInt("Busan_GreenNormalTimeLimitFinish2", 0);
        PlayerPrefs.SetInt("Busan_GreenNormalTimeLimitFinish3", 0);
        PlayerPrefs.SetInt("Busan_GreenHardTimeLimitFinish1", 0);
        PlayerPrefs.SetInt("Busan_GreenHardTimeLimitFinish2", 0);
        PlayerPrefs.SetInt("Busan_GreenHardTimeLimitFinish3", 0);

        //맵 퀘스트 - 커스텀 보상
        PlayerPrefs.SetString("Busan_AllOneFinish", "No");    //No, Yes, MissionOk
        PlayerPrefs.SetString("Busan_AllTenFinish", "No");
        PlayerPrefs.SetString("Busan_AllTwentyFinish", "No");
        PlayerPrefs.SetString("Busan_Distance500Km", "No");
        PlayerPrefs.SetString("Busan_Distance1000Km", "No");
        PlayerPrefs.SetString("Busan_Distance1500Km", "No");

        // 캐릭터 정보 서버에 저장하기
        ServerManager.Instance.CharacterInfo_Reg();
        yield return new WaitUntil(() => ServerManager.Instance.isCharacterRegUpload);
        ServerManager.Instance.isCharacterRegUpload = false;

        // MyNotice에 공지 추가
        ServerManager.Instance.Reg_Notice();
        yield return new WaitUntil(() => ServerManager.Instance.isRegNoticeUpload);
        ServerManager.Instance.isRegNoticeUpload = false;

        yield return new WaitForSeconds(1f);

        //-------------------- 서버에 저장된 데이터 ServerManager에 불러오기 ---------------------
        ServerManager.Instance.userInfo.player_UID = PlayerPrefs.GetString("Busan_Player_UID");
        ServerManager.Instance.userInfo.player_ID = PlayerPrefs.GetString("Busan_Player_ID");
        ServerManager.Instance.userInfo.player_PW = PlayerPrefs.GetString("Busan_Player_PassWord");
        ServerManager.Instance.userInfo.player_NickName = PlayerPrefs.GetString("Busan_Player_NickName");
        ServerManager.Instance.userInfo.player_LoginState = PlayerPrefs.GetString("Busan_Player_LoginState");

        // 캐릭터 데이터 가져오기
        ServerManager.Instance.GetCharacterInfo(PlayerPrefs.GetString("Busan_Player_ID"));
        yield return new WaitUntil(() => ServerManager.Instance.isCharacterDataStackCompleted);
        ServerManager.Instance.isCharacterDataStackCompleted = false;

        // 계정의 출석체크 가져오기
        ServerManager.Instance.GetConnectedState(PlayerPrefs.GetString("Busan_Player_ID"));
        yield return new WaitUntil(() => ServerManager.Instance.isConnectedStateStackCompleted);
        ServerManager.Instance.isConnectedStateStackCompleted = false;

        // 일일 퀘스트 가져오기
        ServerManager.Instance.GetTodayQuest(PlayerPrefs.GetString("Busan_Player_ID"));
        yield return new WaitUntil(() => ServerManager.Instance.isTodayQuestStackCompleted);
        ServerManager.Instance.isTodayQuestStackCompleted = false;

        // 맵 퀘스트 가져오기
        ServerManager.Instance.GetMapQuest(PlayerPrefs.GetString("Busan_Player_ID"));
        yield return new WaitUntil(() => ServerManager.Instance.isMapQuestStackCompleted);
        ServerManager.Instance.isMapQuestStackCompleted = false;

        // 맵 퀘스트 보상 상태 가져오기
        ServerManager.Instance.GetQuestReward(PlayerPrefs.GetString("Busan_Player_ID"));
        yield return new WaitUntil(() => ServerManager.Instance.isQuestRewardStackCompleted);
        ServerManager.Instance.isQuestRewardStackCompleted = false;

        // 볼륨 설정.
        ServerManager.Instance.SettingInfo("SELECT", PlayerPrefs.GetString("Busan_Player_ID"));
        yield return new WaitUntil(() => ServerManager.Instance.isGetSettingCompleted);
        ServerManager.Instance.isGetSettingCompleted = false;

        SceneManager.LoadScene("Lobby");
    }
}
