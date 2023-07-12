using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
//using UnityEngine.UIElements;

public class CharacterChoice_UIManager2 : MonoBehaviour
{
    public BusanMapStringClass stringClass;
    public static CharacterChoice_UIManager2 instance { get; private set; }

    [Header("플레이어")]
    public GameObject womanPlayer;
    public GameObject manPlayer;
    NewManCtrl manctrl_scrip;
    NewWomanCtrl womanctrl_scrip;


    [Header("토글 그룹")]
    //토글 탭 그룹
    public ToggleGroup bigTapToggleGroup;   //큰탭
    public ToggleGroup sexToggleGroup;  //성별 
    public ToggleGroup faceColorToggleGroup;    //얼굴색상
    public ToggleGroup hairColorToggleGroup;    //머리색상


    [Header("[큰 탭]")]
    //큰탭
    public GameObject sexPanel;
    public GameObject facePanel;
    public GameObject hairPanel;


    [Header("[색상 판넬]")]
    public GameObject faceColorPanel;   //얼굴색상 판넬
    public GameObject hairColorPanel;   //색상 판넬

    [Header("[모델링 메테리얼]")]
    public Material woman_Skin_Mat; //여자 피부색
    public Material woman_hair_Mat; //여자 머리색
    public Material man_Skin_Mat;   //남자 피부색
    public Material man_hair_Mat;   //남자 머리색

    //--------------------
    public Renderer[] woman_Body_Rend;  //여자 몸
    public Renderer[] woman_Skin_Rend;  //여자 피부
    public Renderer[] woman_Hair_Rend;  //여자 머리 //0,1:아시아 / 2,3:서양 / 4:흑인
    public Renderer man_Body_Rend;    //남자 몸
    public Renderer[] man_Skin_Rend;    //남자 피부
    public Renderer[] man_Hair_Rend;    //남자 머리

    MaterialPropertyBlock propertyBlock;
    


    [Header("[데이터 저장 변수]")]
    string sexStr, faceStr, faceColorStr, hairStyleStr, hairColorStr;   //성별, 바디스타일, 바디색, 머리스타일, 머리색
    public int womanbody_i, manbody_i, womanhair_i, manhair_i;
    int womanRace, manRace;   //인종(0:동양/1:서양/2:흑인)
    int hair;   //머리스타일 (동서흑)

    public float r, g, b;

    //로그인 상태 여부
    string loginState;
    

    //큰탭 
    public Toggle bigTapCurrentSelection
    {
        get { return bigTapToggleGroup.ActiveToggles().FirstOrDefault(); }
    }
    //성별
    public Toggle sexCurrentSelection
    {
        get { return sexToggleGroup.ActiveToggles().FirstOrDefault(); }
    }
    
    //머리색상
    public Toggle hairColorCurrentSelection
    {
        get { return hairColorToggleGroup.ActiveToggles().FirstOrDefault(); }
    }
    //얼굴색상
    public Toggle faceColorCurrentSelection
    {
        get { return faceColorToggleGroup.ActiveToggles().FirstOrDefault(); }
    }



    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        loginState = PlayerPrefs.GetString("Busan_Player_LoginState");    //로그인 상태 여부
        propertyBlock = new MaterialPropertyBlock();
    }


    void Start()
    {
        stringClass = new BusanMapStringClass();
        manctrl_scrip = manPlayer.GetComponent<NewManCtrl>();
        womanctrl_scrip = womanPlayer.GetComponent<NewWomanCtrl>();
        Initialization();
    }


    //초기화
    public void Initialization()
    {
        //판넬 초기화 - 기본으로 성별선택화면
        BigTapChoiceShowPanel(true, false, false);

        //성별 모델 초기화
        Sex_ModelLook(true, false);

        ColorChoiceShowPanel(false, false);    //색상판넬 비활성화

        //여자 모델링 기본 초기화 - 동양인
        //WomanModelChoiceShow(true, true, false, false, false, false);
        //살색 기본 초기화
        woman_Skin_Mat.color = new Color(1f, 0.6f, 0.45f);  //살색
        woman_hair_Mat.color = new Color(0.2f, 0.1f, 0.08f); ;  //갈색
        man_Skin_Mat.color = new Color(1f, 0.6f, 0.45f); //살색 
        man_hair_Mat.color = new Color(0.2f, 0.1f, 0.08f); ;  //갈색

        //유니벌스렌더러 색상 변경 하기 위함
        propertyBlock.SetColor("_BaseColor", new Color(1f, 0.6f, 0.45f));
        woman_Skin_Rend[0].SetPropertyBlock(propertyBlock);
        woman_Body_Rend[0].SetPropertyBlock(propertyBlock);
        man_Skin_Rend[0].SetPropertyBlock(propertyBlock);
        man_Body_Rend.SetPropertyBlock(propertyBlock);

        propertyBlock.SetColor("_BaseColor", new Color(0.2f, 0.1f, 0.08f));
        man_Hair_Rend[0].SetPropertyBlock(propertyBlock);
        woman_Hair_Rend[0].SetPropertyBlock(propertyBlock);
        woman_Hair_Rend[1].SetPropertyBlock(propertyBlock);



        //동양인 기본값 - 초기화 세팅
        womanRace = 0; manRace = 0; womanbody_i = 0; manbody_i = 0; womanhair_i = 0; manhair_i = 0; sexStr = "Woman";
        faceStr = "Asian"; faceColorStr = "FaceApricot";
        hairStyleStr = "Hair1"; hairColorStr = "HairBrown";

        PlayerPrefs.SetInt("Busan_BodyNumber", 0); PlayerPrefs.SetInt("Busan_HairNumber", 0);

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
        PlayerPrefs.SetString("Busan_Player_Sex", sexStr);
        PlayerPrefs.SetString("Busan_Player_Face", faceStr);
        PlayerPrefs.SetString("Busan_Player_SkinColor", faceColorStr);
        PlayerPrefs.SetString("Busan_Player_Hair", hairStyleStr);
        PlayerPrefs.SetString("Busan_Player_HairColor", hairColorStr);
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
        PlayerPrefs.SetString("Busan_Wear_HairStyleName", hairStyleStr);
        PlayerPrefs.SetString("Busan_Wear_JacketStyleName", "BasicNasi");
        PlayerPrefs.SetString("Busan_Wear_PantsStyleName", "BasicShort");
        PlayerPrefs.SetString("Busan_Wear_ShoesStyleName", "BasicSandal");
        PlayerPrefs.SetString("Busan_Wear_BicycleStyleName", "BasicBicycle");
        PlayerPrefs.SetString("Busan_Wear_HelmetStyleName", "No");
        PlayerPrefs.SetString("Busan_Wear_GlovesStyleName", "No");
        PlayerPrefs.SetString("Busan_Wear_BicycleStyleName", "BasicBicycle");

        //아이템 장착 종류
        PlayerPrefs.SetString("Busan_Wear_HairKind", "ItemID_Hair");
        PlayerPrefs.SetString("Busan_Wear_GlovesKind", "No");
        PlayerPrefs.SetString("Busan_Wear_HelmetKind", "No");
        PlayerPrefs.SetString("Busan_Wear_JacketKind", "ItemID_Nasi");
        PlayerPrefs.SetString("Busan_Wear_PantsKind", "ItemID_Short");
        PlayerPrefs.SetString("Busan_Wear_ShoesKind", "ItemID_Sandal");
        PlayerPrefs.SetString("Busan_Wear_BicycleKind", "ItemID_Bicycle");

        //각 커스텀 숫자 저장
        PlayerPrefs.SetInt("Busan_JacketNumber", 0);
        PlayerPrefs.SetInt("Busan_PantsNumber", 0);
        PlayerPrefs.SetInt("Busan_ShoesNumber", 0);
        PlayerPrefs.SetInt("Busan_HelmetNumber", 100);
        PlayerPrefs.SetInt("Busan_GlovesNumber", 100);
        PlayerPrefs.SetInt("Busan_BicycleNumber", 0);

        //출석체크 형식
        PlayerPrefs.SetString("Busan_OneCennectDay", "");   //미션씬에서 체크
        PlayerPrefs.SetString("Busan_TwoCennedtDay", "");
        PlayerPrefs.SetString("Busan_OneCheck", "");    //로비에서 체크
        PlayerPrefs.SetString("Busan_TwoCheck", "");

        //레드라인, 경기 기록
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

        //레드라인,맵 퀘스트 완주형
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

        //레드라인, 맵 퀘스트 완주 횟수저장
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



    //큰 탭에 해당하는 판넬을 활성화/비활성화
    void BigTapChoiceShowPanel(bool _sexActive, bool _faceActive, bool _hairActive)
    {
        sexPanel.SetActive(_sexActive);
        facePanel.SetActive(_faceActive);
        hairPanel.SetActive(_hairActive);
    }

   

    //색상선택 판 활성화/비활성화
    void ColorChoiceShowPanel(bool _faceColor, bool _hairColor)
    {
        faceColorPanel.SetActive(_faceColor);
        hairColorPanel.SetActive(_hairColor);
    }

    
    //탭 선택하기(성별, 얼굴, 머리색)
    public void BigTap_Choice()
    {
        if(bigTapToggleGroup.ActiveToggles().Any())
        {
            //성별선택
            if(bigTapCurrentSelection.name == "SexToggle")
            {
                BigTapChoiceShowPanel(true, false, false);
                ColorChoiceShowPanel(false, false);   //색상판넬 비활성화
            }
            //얼굴선택
            else if(bigTapCurrentSelection.name == "FaceToggle")
            {
                BigTapChoiceShowPanel(false, true, false);
                ColorChoiceShowPanel(true, false);    //얼굴활성, 머리비활성
            }
            //머리색선택
            else if(bigTapCurrentSelection.name == "HairToggle")
            {
                BigTapChoiceShowPanel(false, false, true);
                ColorChoiceShowPanel(false, true);    //얼굴비활성, 머리활성
            }
        }
    }

    //성별선택(남여)
    public void Sex_Choice()
    {
        if(sexToggleGroup.ActiveToggles().Any())
        {
            //여자
            if(sexCurrentSelection.name == "WomanToggle")
            {
                Debug.Log("여자");
                Sex_ModelLook(true, false);
                sexStr = "Woman";
            }
            //남자
            else if(sexCurrentSelection.name == "ManToggle")
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

    
    //머리 색상 선택
    public void HairColor_Choice()
    {
        if(hairColorToggleGroup.ActiveToggles().Any())
        {
            if(hairColorCurrentSelection.name == "ColorToggle1")        //노란색
            {
                hairColorStr = "HairYellow";
                propertyBlock.SetColor("_BaseColor", new Color(1f, 0.72f, 0.37f));
                float _r = 1f; float _g = 0.72f; float _b = 0.37f;
                _HairColorSelection(_r, _g, _b);
            }
            else if(hairColorCurrentSelection.name == "ColorToggle2")   //황토색
            {
                hairColorStr = "HairRedBrown";
                propertyBlock.SetColor("_BaseColor", new Color(1.1f, 0.6f, 0.3f));
                float _r = 1.1f; float _g = 0.6f; float _b = 0.3f;
                _HairColorSelection(_r, _g, _b);
            }
            else if(hairColorCurrentSelection.name == "ColorToggle3")   //연갈색
            {
                hairColorStr = "HairPaleBrown";
                propertyBlock.SetColor("_BaseColor", new Color(0.5f, 0.31f, 0.1f));
                float _r = 0.5f; float _g = 0.31f; float _b = 0.1f;
                _HairColorSelection(_r, _g, _b);
            }
            else if(hairColorCurrentSelection.name == "ColorToggle4")   //어두운주황색
            {
                hairColorStr = "HairDarkOrange";
                propertyBlock.SetColor("_BaseColor", new Color(0.95f, 0.26f, 0.16f));
                float _r = 0.95f; float _g = 0.26f; float _b = 0.16f;
                _HairColorSelection(_r, _g, _b);
            }
            else if(hairColorCurrentSelection.name == "ColorToggle5")   //진갈색
            {
                hairColorStr = "HairDarkBrown";
                propertyBlock.SetColor("_BaseColor", new Color(0.77f, 0.45f, 0.22f));
                float _r = 0.77f; float _g = 0.45f; float _b = 0.22f;
                _HairColorSelection(_r, _g, _b);
            }
            else if (hairColorCurrentSelection.name == "ColorToggle6")  //고등색
            {
                hairColorStr = "HairReddishBrown";
                propertyBlock.SetColor("_BaseColor", new Color(0.65f, 0.32f, 0.25f));
                float _r = 0.65f; float _g = 0.32f; float _b = 0.25f;
                _HairColorSelection(_r, _g, _b);
            }
            else if (hairColorCurrentSelection.name == "ColorToggle7")  //진고동색
            {
                hairColorStr = "HairDarkReddishBrown";
                propertyBlock.SetColor("_BaseColor", new Color(0.42f, 0.2f, 0.15f));
                float _r = 0.42f; float _g = 0.2f; float _b = 0.15f;
                _HairColorSelection(_r, _g, _b);
            }
            else if (hairColorCurrentSelection.name == "ColorToggle8")  //커피색
            {
                hairColorStr = "HairCoffee";
                propertyBlock.SetColor("_BaseColor", new Color(0.3f, 0.17f, 0.11f));
                float _r = 0.3f; float _g = 0.17f; float _b = 0.11f;
                _HairColorSelection(_r, _g, _b);
            }
            else if (hairColorCurrentSelection.name == "ColorToggle9")  //연회색
            {
                hairColorStr = "HairLightGray";
                propertyBlock.SetColor("_BaseColor", new Color(0.91f, 1f, 1f));
                float _r = 0.91f; float _g = 1f; float _b = 1f;
                _HairColorSelection(_r, _g, _b);
            }
            else if (hairColorCurrentSelection.name == "ColorToggle10") //회색
            {
                hairColorStr = "HairGray";
                propertyBlock.SetColor("_BaseColor", new Color(0.6f, 0.6f, 0.6f));
                float _r = 0.6f; float _g = 0.6f; float _b = 0.6f;
                _HairColorSelection(_r, _g, _b);
            }
            else if (hairColorCurrentSelection.name == "ColorToggle11") //찐회색
            {
                hairColorStr = "HairDarkGray";
                propertyBlock.SetColor("_BaseColor", new Color(0.4f, 0.4f, 0.4f));
                float _r = 0.4f; float _g = 0.4f; float _b = 0.4f;
                _HairColorSelection(_r, _g, _b);
            }
            else if (hairColorCurrentSelection.name == "ColorToggle12") //검은색
            {
                hairColorStr = "HairBlack";
                propertyBlock.SetColor("_BaseColor", new Color(0.2f, 0.2f, 0.2f));
                float _r = 0.2f; float _g = 0.2f; float _b = 0.2f;
                _HairColorSelection(_r, _g, _b);
            }
        }
    }

    void _HairColorSelection(float _r, float _g, float _b)
    {
        if (sexStr == "Woman")
        {
            woman_hair_Mat.color = new Color(_r, _g, _b);  //노란색

            if (hair == 0 || hair == 2)
            {
                woman_Hair_Rend[hair].SetPropertyBlock(propertyBlock);
                woman_Hair_Rend[hair + 1].SetPropertyBlock(propertyBlock);
            }
            else if (hair == 4)
            {
                woman_Hair_Rend[hair].SetPropertyBlock(propertyBlock);
            }
        }
        else if (sexStr == "Man")
        {
            man_hair_Mat.color = new Color(_r, _g, _b);  //노란색

            man_Hair_Rend[hair].SetPropertyBlock(propertyBlock);
        }
    }

    //얼굴 색상 선택
    public void FaceColor_Choice()
    {
        if (faceColorToggleGroup.ActiveToggles().Any())
        {
            if (faceColorCurrentSelection.name == "ColorToggle1")   //흰색

            {
                faceColorStr = "FaceWhite"; //흰색
                propertyBlock.SetColor("_BaseColor", new Color(1.05f, 0.95f, 0.9f));

                float _r = 1.05f; float _g = 0.95f; float _b = 0.9f;
                _FaceColorSelection(_r, _g, _b);
            }
            else if (faceColorCurrentSelection.name == "ColorToggle2")  //연회색
            {
                faceColorStr = "FaceLightGray"; 
                propertyBlock.SetColor("_BaseColor", new Color(1.05f, 0.91f, 0.88f));
                float _r = 1f; float _g = 0.91f; float _b = 0.88f;
                _FaceColorSelection(_r, _g, _b);
            }
            else if (faceColorCurrentSelection.name == "ColorToggle3")  //연살색
            {
                faceColorStr = "FaceLightSkin"; 
                propertyBlock.SetColor("_BaseColor", new Color(1.03f, 0.84f, 0.76f));
                float _r = 1.03f; float _g = 0.84f; float _b = 0.76f;
                _FaceColorSelection(_r, _g, _b);
            }
            else if (faceColorCurrentSelection.name == "ColorToggle4")  //연누런색
            {
                faceColorStr = "FaceReddishCoffee";   
                propertyBlock.SetColor("_BaseColor", new Color(1.09f, 0.9f, 0.76f));
                float _r = 1.09f; float _g = 0.9f; float _b = 0.76f;
                _FaceColorSelection(_r, _g, _b);
            }
            else if (faceColorCurrentSelection.name == "ColorToggle5")  //연갈색
            {
                faceColorStr = "FacePaleBrown"; 
                propertyBlock.SetColor("_BaseColor", new Color(1.09f, 0.87f, 0.7f));
                float _r = 1.09f; float _g = 0.87f; float _b = 0.7f;
                _FaceColorSelection(_r, _g, _b); man_Body_Rend.SetPropertyBlock(propertyBlock);
            }
            else if (faceColorCurrentSelection.name == "ColorToggle6")  //진살색
            {
                faceColorStr = "FaceDarkSkin"; 
                propertyBlock.SetColor("_BaseColor", new Color(1.21f, 0.84f, 0.76f));
                float _r = 1.21f; float _g = 0.84f; float _b = 0.76f;
                _FaceColorSelection(_r, _g, _b);
            }
            else if (faceColorCurrentSelection.name == "ColorToggle7")  //살색
            {
                faceColorStr = "FaceSkin"; 
                propertyBlock.SetColor("_BaseColor", new Color(1.4f, 0.96f, 0.73f));
                float _r = 1.4f; float _g = 0.96f; float _b = 0.73f;
                _FaceColorSelection(_r, _g, _b);
            }
            else if (faceColorCurrentSelection.name == "ColorToggle8")  //베이지색
            {
                faceColorStr = "FaceBeige"; 
                propertyBlock.SetColor("_BaseColor", new Color(1.1f, 0.9f, 0.7f));
                float _r = 1.1f; float _g = 0.9f; float _b = 0.7f;
                _FaceColorSelection(_r, _g, _b);
            }
            else if (faceColorCurrentSelection.name == "ColorToggle9")  //황토색
            {
                faceColorStr = "FaceRedClay"; 
                propertyBlock.SetColor("_BaseColor", new Color(0.9f, 0.7f, 0.6f));
                float _r = 0.9f; float _g = 0.7f; float _b = 0.6f;
                _FaceColorSelection(_r, _g, _b);
            }
            else if (faceColorCurrentSelection.name == "ColorToggle10") //고등색
            {
                faceColorStr = "FaceReddishBrown"; 
                propertyBlock.SetColor("_BaseColor", new Color(0.8f, 0.6f, 0.5f));
                float _r = 0.8f; float _g = 0.6f; float _b = 0.5f;
                _FaceColorSelection(_r, _g, _b);
            }
            else if (faceColorCurrentSelection.name == "ColorToggle11") //찐갈색
            {
                faceColorStr = "FaceDarkBrown"; 
                propertyBlock.SetColor("_BaseColor", new Color(0.6f, 0.4f, 0.36f));
                float _r = 0.6f; float _g = 0.4f; float _b = 0.36f;
                _FaceColorSelection(_r, _g, _b);
            }
            else if (faceColorCurrentSelection.name == "ColorToggle12") //검은색
            {
                faceColorStr = "FaceBlack"; 
                propertyBlock.SetColor("_BaseColor", new Color(0.2f, 0.1f, 0.09f));

                float _r = 0.2f; float _g = 0.1f; float _b = 0.09f;
                _FaceColorSelection(_r, _g, _b);
            }
        }
    }

    void _FaceColorSelection(float _r, float _g, float _b)
    {
        if (sexStr == "Woman")
        {
            woman_Skin_Mat.color = new Color(_r, _g, _b);    //흑색

            woman_Skin_Rend[womanRace].SetPropertyBlock(propertyBlock);
            woman_Body_Rend[womanRace].SetPropertyBlock(propertyBlock);
        }
        else if (sexStr == "Man")
        {
            man_Skin_Mat.color = new Color(_r, _g, _b);    //흑색

            man_Skin_Rend[manRace].SetPropertyBlock(propertyBlock);
            man_Body_Rend.SetPropertyBlock(propertyBlock);
        }
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
        PlayerPrefs.SetString("Busan_Player_Face", faceStr);
        PlayerPrefs.SetString("Busan_Player_SkinColor", faceColorStr);
        PlayerPrefs.SetString("Busan_Player_Hair", hairStyleStr);
        PlayerPrefs.SetString("Busan_Player_HairColor", hairColorStr);
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
        PlayerPrefs.SetString("Busan_Wear_HairStyleName", hairStyleStr);
        PlayerPrefs.SetString("Busan_Wear_JacketStyleName", "BasicNasi");
        PlayerPrefs.SetString("Busan_Wear_PantsStyleName", "BasicShort");
        PlayerPrefs.SetString("Busan_Wear_ShoesStyleName", "BasicSandal");
        PlayerPrefs.SetString("Busan_Wear_BicycleStyleName", "BasicBicycle");
        PlayerPrefs.SetString("Busan_Wear_HelmetStyleName", "No");
        PlayerPrefs.SetString("Busan_Wear_GlovesStyleName", "No");

        //아이템 장착 종류
        PlayerPrefs.SetString("Busan_Wear_HairKind", "ItemID_Hair");
        PlayerPrefs.SetString("Busan_Wear_GlovesKind", "No");
        PlayerPrefs.SetString("Busan_Wear_HelmetKind", "No");
        PlayerPrefs.SetString("Busan_Wear_JacketKind", "ItemID_Nasi");
        PlayerPrefs.SetString("Busan_Wear_PantsKind", "ItemID_Short");
        PlayerPrefs.SetString("Busan_Wear_ShoesKind", "ItemID_Sandal");
        PlayerPrefs.SetString("Busan_Wear_BicycleKind", "ItemID_Bicycle");

        //각 커스텀 숫자 저장
        PlayerPrefs.SetInt("Busan_JacketNumber", 0);
        PlayerPrefs.SetInt("Busan_PantsNumber", 0);
        PlayerPrefs.SetInt("Busan_ShoesNumber", 0);
        PlayerPrefs.SetInt("Busan_HelmetNumber", 100);
        PlayerPrefs.SetInt("Busan_GlovesNumber", 100);
        PlayerPrefs.SetInt("Busan_BicycleNumber", 0);

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

        //맵 퀘스트 시간 제한 완주형
        PlayerPrefs.SetInt("Busan_AsiaNormalTimeLimitFinish1", 0); // 0~13
        PlayerPrefs.SetInt("Busan_AsiaNormalTimeLimitFinish2", 0);
        PlayerPrefs.SetInt("Busan_AsiaNormalTimeLimitFinish3", 0);
        PlayerPrefs.SetInt("Busan_AsiaHardTimeLimitFinish1", 0);
        PlayerPrefs.SetInt("Busan_AsiaHardTimeLimitFinish2", 0);
        PlayerPrefs.SetInt("Busan_AsiaHardTimeLimitFinish3", 0);

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

        // 경기기록 불러오기


        // 볼륨 설정.
        ServerManager.Instance.SettingInfo("SELECT", PlayerPrefs.GetString("Busan_Player_ID"));
        yield return new WaitUntil(() => ServerManager.Instance.isGetSettingCompleted);
        ServerManager.Instance.isGetSettingCompleted = false;

        SceneManager.LoadScene("Lobby");
    }
}
