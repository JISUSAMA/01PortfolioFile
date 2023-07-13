using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class CharacterChoice_UIManager2 : MonoBehaviour
{
    public static CharacterChoice_UIManager2 instance { get; private set; }

    [Header("플레이어")]
    public GameObject womanPlayer;
    public GameObject manPlayer;
    ManCtrl manctrl_scrip;
    WomanCtrl womanctrl_scrip;


    [Header("토글 그룹")]
    //토글 탭 그룹
    public ToggleGroup bigTapToggleGroup;   //큰탭
    public ToggleGroup sexToggleGroup;  //성별 
    public ToggleGroup faceWomanToggleGroup; //여자얼굴
    public ToggleGroup faceManToggleGroup;  //남자얼굴
    public ToggleGroup hairStyleWomanToggleGroup; //여자머리스타일
    public ToggleGroup hairStyleManToggleGroup; //남자머리스타일
    public ToggleGroup faceColorToggleGroup;    //얼굴색상
    public ToggleGroup hairColorToggleGroup;    //머리색상

    [Header("인종 선택 시 머리 isOn")]
    public Toggle[] womanHairTogg;
    public Toggle[] manHairTogg;

    [Header("[큰 탭]")]
    //큰탭
    public GameObject sexPanel;
    public GameObject facePanel;
    public GameObject hairPanel;

    [Header("[얼굴/머리 선택 판넬]")]
    //성별 속성 판넬
    public GameObject womanPanel;   //여자얼굴 판넬
    public GameObject manPanel; //남자얼굴 판넬
    public GameObject womanHairScroll;  //여자머리선택
    public GameObject manHairScroll;    //남자머리선택

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
    //여자얼굴
    public Toggle faceWomanCurrentSelection
    {
        get { return faceWomanToggleGroup.ActiveToggles().FirstOrDefault(); }
    }
    //남자얼굴
    public Toggle faceManCurrentSelection
    {
        get { return faceManToggleGroup.ActiveToggles().FirstOrDefault(); }
    }
    //여자머리스타일
    public Toggle hairStyleWomanCurrentSelection
    {
        get { return hairStyleWomanToggleGroup.ActiveToggles().FirstOrDefault(); }
    }
    //남자머리스타일
    public Toggle hairStyleManCurrentSelection
    {
        get { return hairStyleManToggleGroup.ActiveToggles().FirstOrDefault(); }
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

        loginState = PlayerPrefs.GetString("AT_Player_LoginState");    //로그인 상태 여부
        propertyBlock = new MaterialPropertyBlock();
    }


    void Start()
    {
        manctrl_scrip = manPlayer.GetComponent<ManCtrl>();
        womanctrl_scrip = womanPlayer.GetComponent<WomanCtrl>();
        Initialization();
    }


    //초기화
    public void Initialization()
    {
        //판넬 초기화 - 기본으로 성별선택화면
        BigTapChoiceShowPanel(true, false, false);

        //성별 얼굴 선택 판넬 초기화 - 
        SexFaceChoiceShowPanel(true, false);
        SexHairChoiceShowPanel(true, false);   //머리 선택 기본 판 초기화
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

        PlayerPrefs.SetInt("AT_BodyNumber", 0); PlayerPrefs.SetInt("AT_HairNumber", 0);

        PlayerPrefabsReset();   //기존에 있던 플레이어프리팹의 정보를 초기화 시켜준다.
    }

    void PlayerPrefabsReset()
    {
        //로그인 상태 여부
        if (loginState == "GoogleNickName")
            PlayerPrefs.SetString("AT_Player_LoginState", "GoogleCharacter");
        else if (loginState == "GatewaysNickName")
            PlayerPrefs.SetString("AT_Player_LoginState", "GatewaysCharacter");



        //튜토리얼 
        PlayerPrefs.SetString("AT_LobbyTutorial", "Start");
        PlayerPrefs.SetString("AT_QuestTutorial", "Start");
        PlayerPrefs.SetString("AT_InGameTutorial", "Start");
        PlayerPrefs.SetString("AT_MapChoiceTutorial", "Start");

        //캐릭터 정보
        PlayerPrefs.SetString("AT_Player_Sex", sexStr);
        PlayerPrefs.SetString("AT_Player_Face", faceStr);
        PlayerPrefs.SetString("AT_Player_SkinColor", faceColorStr);
        PlayerPrefs.SetString("AT_Player_Hair", hairStyleStr);
        PlayerPrefs.SetString("AT_Player_HairColor", hairColorStr);
        PlayerPrefs.SetInt("AT_Player_Profile", 1);

        //UI정보
        PlayerPrefs.SetInt("AT_Player_Gold", 1000);
        PlayerPrefs.SetInt("AT_Player_Level", 1);
        PlayerPrefs.SetInt("AT_Player_CurrExp", 0);
        PlayerPrefs.SetInt("AT_Player_TakeExp", 0);

        //기록정보
        PlayerPrefs.SetFloat("AT_TodayMaxSpeed", 0f);
        PlayerPrefs.SetFloat("AT_Record_MaxSpeed", 0f);
        PlayerPrefs.SetFloat("AT_Record_TodayKcal", 0f);
        PlayerPrefs.SetFloat("AT_Record_TodayKm", 0f);
        PlayerPrefs.SetFloat("AT_Record_TotalKm", 0f);
        PlayerPrefs.SetString("AT_OpenMap_CourseName", "Normal-1");    //현재 오픈되어있는 맵
        PlayerPrefs.SetInt("AT_NewOpenMap_CourseNamber", 1);

        //장착 아이템 스타일
        PlayerPrefs.SetString("AT_Wear_HairStyleName", hairStyleStr);
        PlayerPrefs.SetString("AT_Wear_JacketStyleName", "BasicNasi");
        PlayerPrefs.SetString("AT_Wear_PantsStyleName", "BasicShort");
        PlayerPrefs.SetString("AT_Wear_ShoesStyleName", "BasicSandal");
        PlayerPrefs.SetString("AT_Wear_BicycleStyleName", "BasicBicycle");
        PlayerPrefs.SetString("AT_Wear_HelmetStyleName", "No");
        PlayerPrefs.SetString("AT_Wear_GlovesStyleName", "No");
        PlayerPrefs.SetString("AT_Wear_BicycleStyleName", "BasicBicycle");

        //아이템 장착 종류
        PlayerPrefs.SetString("AT_Wear_HairKind", "ItemID_Hair");
        PlayerPrefs.SetString("AT_Wear_GlovesKind", "No");
        PlayerPrefs.SetString("AT_Wear_HelmetKind", "No");
        PlayerPrefs.SetString("AT_Wear_JacketKind", "ItemID_Nasi");
        PlayerPrefs.SetString("AT_Wear_PantsKind", "ItemID_Short");
        PlayerPrefs.SetString("AT_Wear_ShoesKind", "ItemID_Sandal");
        PlayerPrefs.SetString("AT_Wear_BicycleKind", "ItemID_Bicycle");

        //각 커스텀 숫자 저장
        PlayerPrefs.SetInt("AT_JacketNumber", 0);
        PlayerPrefs.SetInt("AT_PantsNumber", 0);
        PlayerPrefs.SetInt("AT_ShoesNumber", 0);
        PlayerPrefs.SetInt("AT_HelmetNumber", 100);
        PlayerPrefs.SetInt("AT_GlovesNumber", 100);
        PlayerPrefs.SetInt("AT_BicycleNumber", 0);

        //출석체크 형식
        PlayerPrefs.SetString("AT_OneCennectDay", "");   //미션씬에서 체크
        PlayerPrefs.SetString("AT_TwoCennedtDay", "");
        PlayerPrefs.SetString("AT_OneCheck", "");    //로비에서 체크
        PlayerPrefs.SetString("AT_TwoCheck", "");

        //경기 기록
        PlayerPrefs.SetString("AT_Asia Normal 1Course", "");
        PlayerPrefs.SetString("AT_Asia Normal 2Course", "");
        PlayerPrefs.SetString("AT_Asia Normal 3Course", "");
        PlayerPrefs.SetString("AT_Asia Hard 1Course", "");
        PlayerPrefs.SetString("AT_Asia Hard 2Course", "");
        PlayerPrefs.SetString("AT_Asia Hard 3Course", "");


        //----- 아이템 & 퀘스트---------
        //아이템 갯수
        PlayerPrefs.SetInt("AT_ExpPlusAmount", 0);
        PlayerPrefs.SetInt("AT_ExpUpAmount", 0);
        PlayerPrefs.SetInt("AT_CoinUpAmount", 0);
        PlayerPrefs.SetInt("AT_SpeedUpAmount", 0);

        //일일퀘스트 실행 여부
        PlayerPrefs.SetInt("AT_TodayQuest1", 0); //접속하기
        PlayerPrefs.SetInt("AT_TodayQuest2", 0); //방문하기
        PlayerPrefs.SetInt("AT_TodayQuest3", 0); //게임미션
        PlayerPrefs.SetInt("AT_TodayQuest4", 0);    //칼로리소모
        PlayerPrefs.SetInt("AT_TodayQuest5", 0);    //최고속도

        //방문하기 퀘스트
        PlayerPrefs.SetString("AT_StoreVisit", "No"); //No, Yes, MissionOk
        PlayerPrefs.SetString("AT_InventoryVisit", "No");
        PlayerPrefs.SetString("AT_RankJonVisit", "No");
        PlayerPrefs.SetString("AT_MyInfoVisit", "No");

        //게임미션 퀘스트
        PlayerPrefs.SetString("AT_ProfileChange", "No");   //프로필변경 //No, Yes, MissionOk
        PlayerPrefs.SetString("AT_GameOnePlay", "No"); //게임한번하기
        PlayerPrefs.SetString("AT_RankUp", "No");  //순위올리기
        PlayerPrefs.SetString("AT_CustomChange", "No");    //커스텈변경
        PlayerPrefs.SetString("AT_GoldUse", "No"); //코인사용
        PlayerPrefs.SetString("AT_ItemUse", "No"); //아이템사용(소모품)
        PlayerPrefs.SetString("AT_ItemPurchase", "No");    //아이템구매(소무품)

        //맵 퀘스트 완주형
        PlayerPrefs.SetInt("AT_AsiaNormal1Finish", 0);    //퀘스트 성공 횟수 0, 1,2,3,4
        PlayerPrefs.SetInt("AT_AsiaNormal2Finish", 0);
        PlayerPrefs.SetInt("AT_AsiaNormal3Finish", 0);
        PlayerPrefs.SetInt("AT_AsiaHard1Finish", 0);
        PlayerPrefs.SetInt("AT_AsiaHard2Finish", 0);
        PlayerPrefs.SetInt("AT_AsiaHard3Finish", 0);
        //맵 퀘스트 완주 횟수저장
        PlayerPrefs.SetInt("AT_AsiaNormal1FinishAmount", 0);
        PlayerPrefs.SetInt("AT_AsiaNormal2FinishAmount", 0);
        PlayerPrefs.SetInt("AT_AsiaNormal3FinishAmount", 0);
        PlayerPrefs.SetInt("AT_AsiaHard1FinishAmount", 0);
        PlayerPrefs.SetInt("AT_AsiaHard2FinishAmount", 0);
        PlayerPrefs.SetInt("AT_AsiaHard3FinishAmount", 0);

        //맵 퀘스트 시간 제한 완주형
        PlayerPrefs.SetInt("AT_AsiaNormalTimeLimitFinish1", 0); // 0~13
        PlayerPrefs.SetInt("AT_AsiaNormalTimeLimitFinish2", 0);
        PlayerPrefs.SetInt("AT_AsiaNormalTimeLimitFinish3", 0);
        PlayerPrefs.SetInt("AT_AsiaHardTimeLimitFinish1", 0);
        PlayerPrefs.SetInt("AT_AsiaHardTimeLimitFinish2", 0);
        PlayerPrefs.SetInt("AT_AsiaHardTimeLimitFinish3", 0);

        //맵 퀘스트 - 커스텀 보상
        PlayerPrefs.SetString("AT_AllOneFinish", "No");    //No, Yes, MissionOk
        PlayerPrefs.SetString("AT_AllTenFinish", "No");
        PlayerPrefs.SetString("AT_AllTwentyFinish", "No");
        PlayerPrefs.SetString("AT_Distance500Km", "No");
        PlayerPrefs.SetString("AT_Distance1000Km", "No");
        PlayerPrefs.SetString("AT_Distance1500Km", "No");
    }



    //큰 탭에 해당하는 판넬을 활성화/비활성화
    void BigTapChoiceShowPanel(bool _sexActive, bool _faceActive, bool _hairActive)
    {
        sexPanel.SetActive(_sexActive);
        facePanel.SetActive(_faceActive);
        hairPanel.SetActive(_hairActive);
    }

    //성별에 따른 얼굴 선택 판 활성화/비활성화
    void SexFaceChoiceShowPanel(bool _womanFace, bool _manFace)
    {
        womanPanel.SetActive(_womanFace);
        manPanel.SetActive(_manFace);
    }

    //성별에 따른 머리 선택 판 활성화/비활성화
    void SexHairChoiceShowPanel(bool _womanHair, bool _manHair)
    {
        womanHairScroll.SetActive(_womanHair);
        manHairScroll.SetActive(_manHair);
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

                if (sexStr == "Woman")
                {
                    //Debug.Log(" womanbody_i " + womanbody_i);
                    if (womanbody_i == 0)
                        womanHairTogg[0].isOn = true;
                    else if (womanbody_i == 1)
                        womanHairTogg[1].isOn = true;
                    else if (womanbody_i == 2)
                        womanHairTogg[2].isOn = true;
                }
                else if (sexStr == "Man")
                {
                    //Debug.Log(" manbody_i " + manbody_i);
                    if (manbody_i == 0)
                        manHairTogg[0].isOn = true;
                    else if (manbody_i == 1)
                        manHairTogg[1].isOn = true;
                    else if (manbody_i == 2)
                        manHairTogg[2].isOn = true;
                }
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
                //Debug.Log("여자");
                Sex_ModelLook(true, false);
                SexFaceChoiceShowPanel(true, false);    //여자얼굴판넬 활성화
                SexHairChoiceShowPanel(true, false);    //여자머리판넬 활성화
                sexStr = "Woman";
            }
            //남자
            else if(sexCurrentSelection.name == "ManToggle")
            {
                //Debug.Log("남자");
                Sex_ModelLook(false, true);
                SexFaceChoiceShowPanel(false, true);    //남자얼굴판넬 활성화
                SexHairChoiceShowPanel(false, true);    //남자머리판넬 활성화
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

    //여자얼굴선택(얼굴색상)
    public void WomanFace_Choice()
    {
        if(faceWomanToggleGroup.ActiveToggles().Any())
        {
            //여자 얼굴
            if(faceWomanCurrentSelection.name == "Woman_Toggle1")
            {
                //Debug.Log("여자1");
                //애니메이터 초기화 //자전거/몸/머리/나시/바지/샌들
                womanbody_i = 0; womanhair_i = 0;
                womanctrl_scrip.Animator_Initialization(womanbody_i, womanhair_i, 0, 0, 0);
                womanctrl_scrip.HairSetting(true, false, false);
                womanctrl_scrip.BodySetting(true, false, false);
                
                woman_Skin_Mat.color = new Color(1f, 0.6f, 0.45f);  //살색
                woman_hair_Mat.color = new Color(0.2f, 0.1f, 0.08f);   //갈색

                propertyBlock.SetColor("_BaseColor", new Color(1f, 0.6f, 0.45f));
                woman_Skin_Rend[0].SetPropertyBlock(propertyBlock);
                woman_Body_Rend[0].SetPropertyBlock(propertyBlock);
                propertyBlock.SetColor("_BaseColor", new Color(0.2f, 0.1f, 0.08f));
                woman_Hair_Rend[0].SetPropertyBlock(propertyBlock);
                woman_Hair_Rend[1].SetPropertyBlock(propertyBlock);


                faceStr = "Asian"; faceColorStr = "FaceApricot"; womanRace = 0;//동양인
                PlayerPrefs.SetInt("AT_BodyNumber", womanRace);

                //동양인의 기본 머리 세팅
                hairStyleStr = "Hair1"; PlayerPrefs.SetInt("AT_HairNumber", 0);
            }
            else if(faceWomanCurrentSelection.name == "Woman_Toggle2")
            {
                //애니메이터 초기화 //자전거/몸/머리/나시/바지/샌들
                womanbody_i = 1; womanhair_i = 1;
                womanctrl_scrip.Animator_Initialization(womanbody_i, womanhair_i, 0, 0, 0);
                womanctrl_scrip.HairSetting(false, true, false);
                womanctrl_scrip.BodySetting(false, true, false);   //서양인 활성화

                woman_Skin_Mat.color = new Color(1f, 0.7f, 0.6f);   //백색
                woman_hair_Mat.color = new Color(0.95f, 0.65f, 0.26f);  //노란색

                propertyBlock.SetColor("_BaseColor", new Color(1f, 0.7f, 0.6f));
                woman_Skin_Rend[1].SetPropertyBlock(propertyBlock);
                woman_Body_Rend[1].SetPropertyBlock(propertyBlock);
                propertyBlock.SetColor("_BaseColor", new Color(0.95f, 0.65f, 0.26f));
                woman_Hair_Rend[2].SetPropertyBlock(propertyBlock);
                woman_Hair_Rend[3].SetPropertyBlock(propertyBlock);


                faceStr = "White"; faceColorStr = "FaceWhite"; womanRace = 1;//서양인
                PlayerPrefs.SetInt("AT_BodyNumber", womanRace);

                //서양인의 기본 머리 세팅
                hairStyleStr = "Hair2"; PlayerPrefs.SetInt("AT_HairNumber", 1);
            }
            else if (faceWomanCurrentSelection.name == "Woman_Toggle3")
            {
                //애니메이터 초기화 //자전거/몸/머리/나시/바지/샌들
                womanbody_i = 2; womanhair_i = 2;
                womanctrl_scrip.Animator_Initialization(womanbody_i, womanhair_i, 0, 0, 0);
                womanctrl_scrip.HairSetting(false, false, true);
                womanctrl_scrip.BodySetting(false, false, true);   //흑인 활성화

                woman_Skin_Mat.color = new Color(0.2f, 0.1f, 0.09f);    //흑색
                woman_hair_Mat.color = new Color(0.07f, 0.07f, 0.07f);  //검은색

                propertyBlock.SetColor("_BaseColor", new Color(0.2f, 0.1f, 0.09f));
                woman_Skin_Rend[2].SetPropertyBlock(propertyBlock);
                woman_Body_Rend[2].SetPropertyBlock(propertyBlock);
                propertyBlock.SetColor("_BaseColor", new Color(0.07f, 0.07f, 0.07f));
                woman_Hair_Rend[4].SetPropertyBlock(propertyBlock);


                faceStr = "Black"; faceColorStr = "FaceBlack"; womanRace = 2;//흑인
                PlayerPrefs.SetInt("AT_BodyNumber", womanRace);

                //흑양인의 기본 머리 세팅
                hairStyleStr = "Hair3"; PlayerPrefs.SetInt("AT_HairNumber", 2);
            }
            else if (faceWomanCurrentSelection.name == "Woman_Toggle4")
            {
                
            }
            else if (faceWomanCurrentSelection.name == "Woman_Toggle5")
            {
                
            }
            else if (faceWomanCurrentSelection.name == "Woman_Toggle6")
            {
                
            }
            else if (faceWomanCurrentSelection.name == "Woman_Toggle7")
            {
                
            }
            else if (faceWomanCurrentSelection.name == "Woman_Toggle8")
            {
                
            }
            else if (faceWomanCurrentSelection.name == "Woman_Toggle9")
            {
                
            }
        }
    }


    //남자얼굴선택(얼굴색상)
    public void ManFace_Choice()
    {
        if (faceManToggleGroup.ActiveToggles().Any())
        {
            //여자 얼굴
            if (faceManCurrentSelection.name == "Man_Toggle1")
            {
                //Debug.Log("남자1");
                //애니메이터 초기화 //자전거/몸/머리/나시/바지/샌들
                manbody_i = 0; manhair_i = 0;
                manctrl_scrip.Animator_Initialization(manbody_i, manhair_i, 0, 0, 0);
                manctrl_scrip.HairSetting(true, false, false, false) ;
                manctrl_scrip.FaceSetting(true, false, false);   //동양인 활성화

                man_Skin_Mat.color = new Color(1f, 0.6f, 0.45f);  //살색
                man_hair_Mat.color = new Color(0.2f, 0.1f, 0.08f);   //갈색

                propertyBlock.SetColor("_BaseColor", new Color(1f, 0.6f, 0.45f));
                man_Skin_Rend[0].SetPropertyBlock(propertyBlock);
                man_Body_Rend.SetPropertyBlock(propertyBlock);
                propertyBlock.SetColor("_BaseColor", new Color(0.2f, 0.1f, 0.08f));
                man_Hair_Rend[0].SetPropertyBlock(propertyBlock);


                faceStr = "Asian"; faceColorStr = "FaceSpricot"; manRace = 0;//동양인
                PlayerPrefs.SetInt("AT_BodyNumber", manRace);

                //동양인의 기본 머리 세팅
                hairStyleStr = "Hair1"; PlayerPrefs.SetInt("AT_HairNumber", 0);
            }
            else if (faceManCurrentSelection.name == "Man_Toggle2")
            {
                //Debug.Log("남2");
                //애니메이터 초기화 //자전거/몸/머리/나시/바지/샌들
                manbody_i = 1; manhair_i = 1;
                manctrl_scrip.Animator_Initialization(manbody_i, manhair_i, 0, 0, 0);
                manctrl_scrip.HairSetting(false, true, false, false);
                manctrl_scrip.FaceSetting(false, true, false);   //서양인 활성화

                man_Skin_Mat.color = new Color(1f, 0.7f, 0.6f);   //백색
                man_hair_Mat.color = new Color(0.95f, 0.65f, 0.26f);  //노란색

                propertyBlock.SetColor("_BaseColor", new Color(1f, 0.7f, 0.6f));
                man_Skin_Rend[1].SetPropertyBlock(propertyBlock);
                man_Body_Rend.SetPropertyBlock(propertyBlock);
                propertyBlock.SetColor("_BaseColor", new Color(0.95f, 0.65f, 0.26f));
                man_Hair_Rend[1].SetPropertyBlock(propertyBlock);


                faceStr = "White"; faceColorStr = "FaceWhite"; manRace = 1;//서양인
                PlayerPrefs.SetInt("AT_BodyNumber", manRace);

                //서양인의 기본 머리 세팅
                hairStyleStr = "Hair2"; PlayerPrefs.SetInt("AT_HairNumber", 1);
            }
            else if (faceManCurrentSelection.name == "Man_Toggle3")
            {
                //애니메이터 초기화 //자전거/몸/머리/나시/바지/샌들
                manbody_i = 2; manhair_i = 2;
                manctrl_scrip.Animator_Initialization(manbody_i, manhair_i, 0, 0, 0);
                manctrl_scrip.HairSetting(false, false, true, false);
                manctrl_scrip.FaceSetting(false, false, true);   //흑인 활성화

                man_Skin_Mat.color = new Color(0.2f, 0.1f, 0.09f);    //흑색
                man_hair_Mat.color = new Color(0.07f, 0.07f, 0.07f);  //검은색

                propertyBlock.SetColor("_BaseColor", new Color(0.2f, 0.1f, 0.09f));
                man_Skin_Rend[2].SetPropertyBlock(propertyBlock);
                man_Body_Rend.SetPropertyBlock(propertyBlock);
                propertyBlock.SetColor("_BaseColor", new Color(0.07f, 0.07f, 0.07f));
                man_Hair_Rend[2].SetPropertyBlock(propertyBlock);


                faceStr = "Black"; faceColorStr = "FaceBlack"; manRace = 2;//흑인
                PlayerPrefs.SetInt("AT_BodyNumber", manRace);

                //흑인의 기본 머리 세팅
                hairStyleStr = "Hair3"; PlayerPrefs.SetInt("AT_HairNumber", 2);
            }
            else if (faceManCurrentSelection.name == "Man_Toggle4")
            {
                
            }
            else if (faceManCurrentSelection.name == "Man_Toggle5")
            {
                
            }
            else if (faceManCurrentSelection.name == "Man_Toggle6")
            {
                
            }
            else if (faceManCurrentSelection.name == "Man_Toggle7")
            {
                
            }
            else if (faceManCurrentSelection.name == "Man_Toggle8")
            {
                
            }
            else if (faceManCurrentSelection.name == "Man_Toggle9")
            {
                
            }
        }
    }


    //여자머리 스타일 선택
    public void WomanHairStyle_Choice()
    {
        if(hairStyleWomanToggleGroup.ActiveToggles().Any())
        {
            if(hairStyleWomanCurrentSelection.name == "HairToggle1")
            {
                hairStyleStr = "Hair1";
                //Debug.Log("1 : " + hairStyleStr);
                hair = 0;
                PlayerPrefs.SetInt("AT_HairNumber", 0);
                womanctrl_scrip.Animator_Initialization(0, womanRace, 0, 0, 0);
                womanctrl_scrip.HairSetting(true, false, false);
            }
            else if(hairStyleWomanCurrentSelection.name == "HairToggle2")
            {
                hairStyleStr = "Hair2";
                //Debug.Log("2 : " + hairStyleStr);
                hair = 2;
                //애니메이터 초기화 
                PlayerPrefs.SetInt("AT_HairNumber", 1);
                womanctrl_scrip.Animator_Initialization(1, womanRace, 0, 0, 0);
                womanctrl_scrip.HairSetting(false, true, false);
            }
            else if(hairStyleWomanCurrentSelection.name == "HairToggle3")
            {
                hairStyleStr = "Hair3";
                //Debug.Log("3 : " + hairStyleStr);
                hair = 4;
                //애니메이터 초기화
                PlayerPrefs.SetInt("AT_HairNumber", 2);
                womanctrl_scrip.Animator_Initialization(2, womanRace, 0, 0, 0);
                womanctrl_scrip.HairSetting(false, false, true);
            }
        }
    }

    //남자머리 스타일 선택
    public void ManHairStyle_Choice()
    {
        if (hairStyleManToggleGroup.ActiveToggles().Any())
        {
            if (hairStyleManCurrentSelection.name == "HairToggle1")
            {
                hairStyleStr = "Hair1";
                //Debug.Log("1 : " + hairStyleStr + " race " + manRace);
                hair = 0;
                //애니메이터 초기화
                PlayerPrefs.SetInt("AT_HairNumber", 0);
                manctrl_scrip.Animator_Initialization(0, manRace, 0, 0, 0);
                manctrl_scrip.HairSetting(true, false, false, false); 
            }
            else if (hairStyleManCurrentSelection.name == "HairToggle2")
            {
                hairStyleStr = "Hair2";
                //Debug.Log("2 : " + hairStyleStr + " race " + manRace);
                hair = 1;
                //애니메이터 초기화
                PlayerPrefs.SetInt("AT_HairNumber", 1);
                manctrl_scrip.Animator_Initialization(1, manRace, 0, 0, 0);
                manctrl_scrip.HairSetting(false, true, false, false);
            }
            else if (hairStyleManCurrentSelection.name == "HairToggle3")
            {
                hairStyleStr = "Hair3";
                //Debug.Log("3 : " + hairStyleStr + " race " + manRace);
                hair = 2;
                //애니메이터 초기화
                PlayerPrefs.SetInt("AT_HairNumber", 2);
                manctrl_scrip.Animator_Initialization(2, manRace, 0, 0, 0);
                manctrl_scrip.HairSetting(false, false, true, false);
            }
        }
    }

    //머리 색상 선택
    public void HairColor_Choice()
    {
        if(hairColorToggleGroup.ActiveToggles().Any())
        {
            if(hairColorCurrentSelection.name == "ColorToggle1")
            {
                hairColorStr = "HairBlack";
                propertyBlock.SetColor("_BaseColor", new Color(0.07f, 0.07f, 0.07f));
                if (sexStr == "Woman")
                {
                    woman_hair_Mat.color = new Color(0.07f, 0.07f, 0.07f);  //검은색
                    if(hair == 0 || hair == 2)
                    {
                        woman_Hair_Rend[hair].SetPropertyBlock(propertyBlock);
                        woman_Hair_Rend[hair + 1].SetPropertyBlock(propertyBlock);
                    }
                    else if(hair == 4)
                    {
                        woman_Hair_Rend[hair].SetPropertyBlock(propertyBlock);
                    }
                }
                else if (sexStr == "Man")
                {
                    man_hair_Mat.color = new Color(0.07f, 0.07f, 0.07f);  //검은색

                    man_Hair_Rend[hair].SetPropertyBlock(propertyBlock);
                } 
            }
            else if(hairColorCurrentSelection.name == "ColorToggle2")
            {
                hairColorStr = "HairRedBrown";
                propertyBlock.SetColor("_BaseColor", new Color(0.46f, 0.06f, 0.08f));
                if (sexStr == "Woman")
                {
                    woman_hair_Mat.color = new Color(0.46f, 0.06f, 0.08f);   //붉은갈색

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
                    man_hair_Mat.color = new Color(0.46f, 0.06f, 0.08f);   //붉은갈색

                    man_Hair_Rend[hair].SetPropertyBlock(propertyBlock);
                }  
            }
            else if(hairColorCurrentSelection.name == "ColorToggle3")
            {
                hairColorStr = "HairBrown";
                propertyBlock.SetColor("_BaseColor", new Color(0.2f, 0.1f, 0.08f));
                if (sexStr == "Woman")
                {
                    woman_hair_Mat.color = new Color(0.2f, 0.1f, 0.08f);   //갈색

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
                    man_hair_Mat.color = new Color(0.2f, 0.1f, 0.08f);   //갈색

                    man_Hair_Rend[hair].SetPropertyBlock(propertyBlock);
                }  
            }
            else if(hairColorCurrentSelection.name == "ColorToggle4")
            {
                hairColorStr = "HairYellowBrown";
                propertyBlock.SetColor("_BaseColor", new Color(0.39f, 0.27f, 0.14f));
                if (sexStr == "Woman")
                {
                    woman_hair_Mat.color = new Color(0.39f, 0.27f, 0.14f);  //똥색

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
                    man_hair_Mat.color = new Color(0.39f, 0.27f, 0.14f);  //똥색

                    man_Hair_Rend[hair].SetPropertyBlock(propertyBlock);
                }    
            }
            else if(hairColorCurrentSelection.name == "ColorToggle5")
            {
                hairColorStr = "HairYellow";
                propertyBlock.SetColor("_BaseColor", new Color(0.95f, 0.65f, 0.26f));
                if (sexStr == "Woman")
                {
                    woman_hair_Mat.color = new Color(0.95f, 0.65f, 0.26f);  //노란색

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
                    man_hair_Mat.color = new Color(0.95f, 0.65f, 0.26f);  //노란색

                    man_Hair_Rend[hair].SetPropertyBlock(propertyBlock);
                } 
            }
        }
    }

    //얼굴 색상 선택
    public void FaceColor_Choice()
    {
        if (faceColorToggleGroup.ActiveToggles().Any())
        {
            if (faceColorCurrentSelection.name == "ColorToggle1")
            {
                faceColorStr = "FaceBlack"; //검은색
                propertyBlock.SetColor("_BaseColor", new Color(0.2f, 0.1f, 0.09f));
                if (sexStr == "Woman")
                {
                    woman_Skin_Mat.color = new Color(0.2f, 0.1f, 0.09f);    //흑색

                    woman_Skin_Rend[womanRace].SetPropertyBlock(propertyBlock);
                    woman_Body_Rend[womanRace].SetPropertyBlock(propertyBlock);
                }
                else if(sexStr == "Man")
                {
                    man_Skin_Mat.color = new Color(0.2f, 0.1f, 0.09f);    //흑색

                    man_Skin_Rend[manRace].SetPropertyBlock(propertyBlock);
                    man_Body_Rend.SetPropertyBlock(propertyBlock);
                }
            }
            else if (faceColorCurrentSelection.name == "ColorToggle2")
            {
                faceColorStr = "FaceBrown"; //갈색
                propertyBlock.SetColor("_BaseColor", new Color(0.43f, 0.31f, 0.24f));
                if (sexStr == "Woman")
                {
                    woman_Skin_Mat.color = new Color(0.43f, 0.31f, 0.24f);  //갈색

                    woman_Skin_Rend[womanRace].SetPropertyBlock(propertyBlock);
                    woman_Body_Rend[womanRace].SetPropertyBlock(propertyBlock);
                }
                else if (sexStr == "Man")
                {
                    man_Skin_Mat.color = new Color(0.43f, 0.31f, 0.24f);  //갈색

                    man_Skin_Rend[manRace].SetPropertyBlock(propertyBlock);
                    man_Body_Rend.SetPropertyBlock(propertyBlock);
                } 
            }
            else if (faceColorCurrentSelection.name == "ColorToggle3")
            {
                faceColorStr = "FacePaleBrown"; //연갈색
                propertyBlock.SetColor("_BaseColor", new Color(0.71f, 0.47f, 0.37f));
                if (sexStr == "Woman")
                {
                    woman_Skin_Mat.color = new Color(0.71f, 0.47f, 0.37f);  //연갈색

                    woman_Skin_Rend[womanRace].SetPropertyBlock(propertyBlock);
                    woman_Body_Rend[womanRace].SetPropertyBlock(propertyBlock);
                }
                else if (sexStr == "Man")
                {
                    man_Skin_Mat.color = new Color(0.71f, 0.47f, 0.37f);  //연갈색

                    man_Skin_Rend[manRace].SetPropertyBlock(propertyBlock);
                    man_Body_Rend.SetPropertyBlock(propertyBlock);
                }
            }
            else if (faceColorCurrentSelection.name == "ColorToggle4")
            {
                faceColorStr = "FaceApricot";   //살구색
                propertyBlock.SetColor("_BaseColor", new Color(1f, 0.6f, 0.45f));
                if (sexStr == "Woman")
                {
                    woman_Skin_Mat.color = new Color(1f, 0.6f, 0.45f);  //살색

                    woman_Skin_Rend[womanRace].SetPropertyBlock(propertyBlock);
                    woman_Body_Rend[womanRace].SetPropertyBlock(propertyBlock);
                }  
                else if (sexStr == "Man")
                {
                    man_Skin_Mat.color = new Color(1f, 0.6f, 0.45f);  //살색

                    man_Skin_Rend[manRace].SetPropertyBlock(propertyBlock);
                    man_Body_Rend.SetPropertyBlock(propertyBlock);
                }
            }
            else if (faceColorCurrentSelection.name == "ColorToggle5")
            {
                faceColorStr = "FaceWhite"; //흰색
                propertyBlock.SetColor("_BaseColor", new Color(1f, 0.7f, 0.6f));

                if (sexStr == "Woman")
                {
                    woman_Skin_Mat.color = new Color(1f, 0.7f, 0.6f);   //백색

                    woman_Skin_Rend[womanRace].SetPropertyBlock(propertyBlock);
                    woman_Body_Rend[womanRace].SetPropertyBlock(propertyBlock);
                }  
                else if (sexStr == "Man")
                {
                    man_Skin_Mat.color = new Color(1f, 0.7f, 0.6f);   //백색

                    man_Skin_Rend[manRace].SetPropertyBlock(propertyBlock);
                    man_Body_Rend.SetPropertyBlock(propertyBlock);
                }  
            }
        }
    }



    //설정완료버튼 
    public void SettingEndSaveOnButton()
    {
        Debug.Log("#####로비로 왜 안가노");
        StartCoroutine(_SettingEndSaveOnButton());
        Debug.Log("*********로비로 왜 안가노");
    }

    IEnumerator _SettingEndSaveOnButton()
    {
        Debug.Log("111111로비로 왜 안가노");
        //로그인 상태 여부
        if (loginState == "GoogleCharacter")
            PlayerPrefs.SetString("AT_Player_LoginState", "Google");
        else if (loginState == "GatewaysCharacter")
            PlayerPrefs.SetString("AT_Player_LoginState", "Gateways");

        // 로그인 상태 서버에 업데이트
        ServerManager.Instance.UserLoginState_Update();

        //튜토리얼 
        PlayerPrefs.SetString("AT_LobbyTutorial", "Start");
        PlayerPrefs.SetString("AT_QuestTutorial", "Start");
        PlayerPrefs.SetString("AT_InGameTutorial", "Start");
        PlayerPrefs.SetString("AT_MapChoiceTutorial", "Start");

        //캐릭터 정보
        PlayerPrefs.SetString("AT_Player_Sex", sexStr);
        PlayerPrefs.SetString("AT_Player_Face", faceStr);
        PlayerPrefs.SetString("AT_Player_SkinColor", faceColorStr);
        PlayerPrefs.SetString("AT_Player_Hair", hairStyleStr);
        PlayerPrefs.SetString("AT_Player_HairColor", hairColorStr);
        PlayerPrefs.SetInt("AT_Player_Profile", 1); //프로필 기본 1

        //UI정보
        PlayerPrefs.SetInt("AT_Player_Gold", 1000);
        PlayerPrefs.SetInt("AT_Player_Level", 1);
        PlayerPrefs.SetInt("AT_Player_CurrExp", 0);
        PlayerPrefs.SetInt("AT_Player_TakeExp", 0);

        //기록정보
        PlayerPrefs.SetFloat("AT_TodayMaxSpeed", 0f);
        PlayerPrefs.SetFloat("AT_Record_MaxSpeed", 0f);
        PlayerPrefs.SetFloat("AT_Record_TodayKcal", 0f);
        PlayerPrefs.SetFloat("AT_Record_TodayKm", 0f);
        PlayerPrefs.SetFloat("AT_Record_TotalKm", 0f);
        PlayerPrefs.SetString("AT_OpenMap_CourseName", "Normal-1");    //현재 오픈되어있는 맵
        PlayerPrefs.SetInt("AT_NewOpenMap_CourseNamber", 1);

        //장착 아이템 스타일 - imgname이름으로
        PlayerPrefs.SetString("AT_Wear_HairStyleName", hairStyleStr);
        PlayerPrefs.SetString("AT_Wear_JacketStyleName", "BasicNasi");
        PlayerPrefs.SetString("AT_Wear_PantsStyleName", "BasicShort");
        PlayerPrefs.SetString("AT_Wear_ShoesStyleName", "BasicSandal");
        PlayerPrefs.SetString("AT_Wear_BicycleStyleName", "BasicBicycle");
        PlayerPrefs.SetString("AT_Wear_HelmetStyleName", "No");
        PlayerPrefs.SetString("AT_Wear_GlovesStyleName", "No");

        //아이템 장착 종류
        PlayerPrefs.SetString("AT_Wear_HairKind", "ItemID_Hair");
        PlayerPrefs.SetString("AT_Wear_GlovesKind", "No");
        PlayerPrefs.SetString("AT_Wear_HelmetKind", "No");
        PlayerPrefs.SetString("AT_Wear_JacketKind", "ItemID_Nasi");
        PlayerPrefs.SetString("AT_Wear_PantsKind", "ItemID_Short");
        PlayerPrefs.SetString("AT_Wear_ShoesKind", "ItemID_Sandal");
        PlayerPrefs.SetString("AT_Wear_BicycleKind", "ItemID_Bicycle");

        //각 커스텀 숫자 저장
        PlayerPrefs.SetInt("AT_JacketNumber", 0);
        PlayerPrefs.SetInt("AT_PantsNumber", 0);
        PlayerPrefs.SetInt("AT_ShoesNumber", 0);
        PlayerPrefs.SetInt("AT_HelmetNumber", 100);
        PlayerPrefs.SetInt("AT_GlovesNumber", 100);
        PlayerPrefs.SetInt("AT_BicycleNumber", 0);

        //출석체크 형식 : 빈문자열 넣기
        PlayerPrefs.SetString("AT_OneCennectDay", "");   //미션씬에서 체크
        PlayerPrefs.SetString("AT_TwoCennedtDay", "");
        PlayerPrefs.SetString("AT_OneCheck", "");    //로비에서 체크
        PlayerPrefs.SetString("AT_TwoCheck", "");

        //----- 아이템 & 퀘스트---------
        //아이템 갯수
        PlayerPrefs.SetInt("AT_ExpPlusAmount", 0);
        PlayerPrefs.SetInt("AT_ExpUpAmount", 0);
        PlayerPrefs.SetInt("AT_CoinUpAmount", 0);
        PlayerPrefs.SetInt("AT_SpeedUpAmount", 0);

        //일일퀘스트 실행 여부
        PlayerPrefs.SetInt("AT_TodayQuest1", 0); //접속하기
        PlayerPrefs.SetInt("AT_TodayQuest2", 0); //방문하기
        PlayerPrefs.SetInt("AT_TodayQuest3", 0); //게임미션
        PlayerPrefs.SetInt("AT_TodayQuest4", 0);    //칼로리소모
        PlayerPrefs.SetInt("AT_TodayQuest5", 0);    //최고속도

        //방문하기 퀘스트
        PlayerPrefs.SetString("AT_StoreVisit", "No"); //No, Yes, MissionOk
        PlayerPrefs.SetString("AT_InventoryVisit", "No");
        PlayerPrefs.SetString("AT_RankJonVisit", "No");
        PlayerPrefs.SetString("AT_MyInfoVisit", "No");

        //게임미션 퀘스트
        PlayerPrefs.SetString("AT_ProfileChange", "No");   //프로필변경 //No, Yes, MissionOk
        PlayerPrefs.SetString("AT_GameOnePlay", "No"); //게임한번하기
        PlayerPrefs.SetString("AT_RankUp", "No");  //순위올리기
        PlayerPrefs.SetString("AT_CustomChange", "No");    //커스텈변경
        PlayerPrefs.SetString("AT_GoldUse", "No"); //코인사용
        PlayerPrefs.SetString("AT_ItemUse", "No"); //아이템사용(소모품)
        PlayerPrefs.SetString("AT_ItemPurchase", "No");    //아이템구매(소무품)

        //경기 기록
        PlayerPrefs.SetString("AT_Asia Normal 1Course", "");
        PlayerPrefs.SetString("AT_Asia Normal 2Course", "");
        PlayerPrefs.SetString("AT_Asia Normal 3Course", "");
        PlayerPrefs.SetString("AT_Asia Hard 1Course", "");
        PlayerPrefs.SetString("AT_Asia Hard 2Course", "");
        PlayerPrefs.SetString("AT_Asia Hard 3Course", "");


        //맵 퀘스트 완주형
        PlayerPrefs.SetInt("AT_AsiaNormal1Finish", 0);    //퀘스트 성공 횟수 0, 1,2,3,4
        PlayerPrefs.SetInt("AT_AsiaNormal2Finish", 0);
        PlayerPrefs.SetInt("AT_AsiaNormal3Finish", 0);
        PlayerPrefs.SetInt("AT_AsiaHard1Finish", 0);
        PlayerPrefs.SetInt("AT_AsiaHard2Finish", 0);
        PlayerPrefs.SetInt("AT_AsiaHard3Finish", 0);
        //맵 퀘스트 완주 횟수저장
        PlayerPrefs.SetInt("AT_AsiaNormal1FinishAmount", 0);
        PlayerPrefs.SetInt("AT_AsiaNormal2FinishAmount", 0);
        PlayerPrefs.SetInt("AT_AsiaNormal3FinishAmount", 0);
        PlayerPrefs.SetInt("AT_AsiaHard1FinishAmount", 0);
        PlayerPrefs.SetInt("AT_AsiaHard2FinishAmount", 0);
        PlayerPrefs.SetInt("AT_AsiaHard3FinishAmount", 0);

        //맵 퀘스트 시간 제한 완주형
        PlayerPrefs.SetInt("AT_AsiaNormalTimeLimitFinish1", 0); // 0~13
        PlayerPrefs.SetInt("AT_AsiaNormalTimeLimitFinish2", 0);
        PlayerPrefs.SetInt("AT_AsiaNormalTimeLimitFinish3", 0);
        PlayerPrefs.SetInt("AT_AsiaHardTimeLimitFinish1", 0);
        PlayerPrefs.SetInt("AT_AsiaHardTimeLimitFinish2", 0);
        PlayerPrefs.SetInt("AT_AsiaHardTimeLimitFinish3", 0);

        //맵 퀘스트 - 커스텀 보상
        PlayerPrefs.SetString("AT_AllOneFinish", "No");    //No, Yes, MissionOk
        PlayerPrefs.SetString("AT_AllTenFinish", "No");
        PlayerPrefs.SetString("AT_AllTwentyFinish", "No");
        PlayerPrefs.SetString("AT_Distance500Km", "No");
        PlayerPrefs.SetString("AT_Distance1000Km", "No");
        PlayerPrefs.SetString("AT_Distance1500Km", "No");

        Debug.Log("-------로비로 왜 안가노");

        // 캐릭터 정보 서버에 저장하기
        ServerManager.Instance.CharacterInfo_Reg();
        yield return new WaitUntil(() => ServerManager.Instance.isCharacterRegUpload);
        ServerManager.Instance.isCharacterRegUpload = false;

        // MyNotice에 공지 추가
        ServerManager.Instance.Reg_Notice();
        yield return new WaitUntil(() => ServerManager.Instance.isRegNoticeUpload);
        ServerManager.Instance.isRegNoticeUpload = false;

        yield return new WaitForSeconds(1f);

        Debug.Log("+++++++++로비로 왜 안가노");


        //-------------------- 서버에 저장된 데이터 ServerManager에 불러오기 ---------------------
        ServerManager.Instance.userInfo.player_UID = PlayerPrefs.GetString("AT_Player_UID");
        ServerManager.Instance.userInfo.player_ID = PlayerPrefs.GetString("AT_Player_ID");
        ServerManager.Instance.userInfo.player_PW = PlayerPrefs.GetString("AT_Player_PassWord");
        ServerManager.Instance.userInfo.player_NickName = PlayerPrefs.GetString("AT_Player_NickName");
        ServerManager.Instance.userInfo.player_LoginState = PlayerPrefs.GetString("AT_Player_LoginState");

        // 캐릭터 데이터 가져오기
        ServerManager.Instance.GetCharacterInfo(PlayerPrefs.GetString("AT_Player_ID"));
        Debug.Log("11: "+ServerManager.Instance.isCharacterDataStackCompleted);
        yield return new WaitUntil(() => ServerManager.Instance.isCharacterDataStackCompleted);
        ServerManager.Instance.isCharacterDataStackCompleted = false;

        // 계정의 출석체크 가져오기
        ServerManager.Instance.GetConnectedState(PlayerPrefs.GetString("AT_Player_ID"));
        Debug.Log("22: " + ServerManager.Instance.isConnectedStateStackCompleted);
        yield return new WaitUntil(() => ServerManager.Instance.isConnectedStateStackCompleted);
        ServerManager.Instance.isConnectedStateStackCompleted = false;

        // 일일 퀘스트 가져오기
        Debug.Log("이름   " + PlayerPrefs.GetString("AT_Player_ID"));
        ServerManager.Instance.GetTodayQuest(PlayerPrefs.GetString("AT_Player_ID"));
        Debug.Log("33: " + ServerManager.Instance.isTodayQuestStackCompleted + "   " + PlayerPrefs.GetString("AT_Player_ID"));
        yield return new WaitUntil(() => ServerManager.Instance.isTodayQuestStackCompleted);
        ServerManager.Instance.isTodayQuestStackCompleted = false;

        // 맵 퀘스트 가져오기
        ServerManager.Instance.GetMapQuest(PlayerPrefs.GetString("AT_Player_ID"));
        Debug.Log("44: " + ServerManager.Instance.isMapQuestStackCompleted);
        yield return new WaitUntil(() => ServerManager.Instance.isMapQuestStackCompleted);
        ServerManager.Instance.isMapQuestStackCompleted = false;

        // 맵 퀘스트 보상 상태 가져오기
        ServerManager.Instance.GetQuestReward(PlayerPrefs.GetString("AT_Player_ID"));
        Debug.Log("55: " + ServerManager.Instance.isQuestRewardStackCompleted);
        yield return new WaitUntil(() => ServerManager.Instance.isQuestRewardStackCompleted);
        ServerManager.Instance.isQuestRewardStackCompleted = false;

        // 경기기록 불러오기


        // 볼륨 설정.
        ServerManager.Instance.SettingInfo("SELECT", PlayerPrefs.GetString("AT_Player_ID"));
        yield return new WaitUntil(() => ServerManager.Instance.isGetSettingCompleted);
        ServerManager.Instance.isGetSettingCompleted = false;

        Debug.Log("로비로 왜 안가노");
        SceneManager.LoadScene("Lobby");
    }
}
