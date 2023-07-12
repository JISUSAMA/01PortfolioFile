using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class MapChoice_UIManager : MonoBehaviour
{
    private BusanMapStringClass stringClass;

    [SerializeField] private GameObject RedLineMapGroup;
   [SerializeField] private GameObject GreenLineMapGroup;
    [Header("Red Line ToggleGrup")]
    public ToggleGroup RedMapFirstToggleGroup;  //레드라인 선택 첫번째 나오는 모드선택 토글(노멀, 하드)
    public ToggleGroup RedMapTwoToggleGroup;   //레드라인 선택 두번째 나오는 모드선택 토글 판넬(노멀 하드)
    public ToggleGroup RedNormalCourseGroup;  //레드라인 모멀모드 선택토글
    public ToggleGroup RedHardCourseGroup; //레드라인 하드모드 선택토글

    [Header("Green Line ToggleGrup")]
    public ToggleGroup GreenMapFirstToggleGroup;  //그린라인 선택 첫번째 나오는 모드선택 토글(노멀, 하드)
    public ToggleGroup GreenMapTwoToggleGroup;   //그린라인 선택 두번째 나오는 모드선택 토글 판넬(노멀 하드)
    public ToggleGroup GreenNormalcourseGroup;  //그린라인 모멀모드 선택토글
    public ToggleGroup GreenHardCourseGroup; //그린라인 하드모드 선택토글

    //public ToggleGroup itemToggleGroup; //아이템 토글 그룹
    public Toggle[] itemToggle;

    public Image mapImg;  //지도 이미지

    public Text cointText;
    public Text expText;
    public Text speedText;

    public Button itemUseBtn;


    GameObject itemDataBase;
    GameObject sensorManager;
    Transform woman_player;
    WomanCtrl womanctrl_scrip;
    Transform man_player;
    ManCtrl manctrl_scrip;



    //[Header("String List Grup")]
    //private string BUSAN_PLAYER_WOMAN = "Woman";
    //private string BUSAN_PLAYER_MAN = "Man";
    //
    //private string SELECT_TOGGLE_NORMAL_1 = "NormalToggle2";
    //private string SELECT_TOGGLE_NORMAL_2 = "NormalToggle2";
    //private string SELECT_TOGGLE_HARD_1 = "HardToggle1";
    //private string SELECT_TOGGLE_HARD_2 = "HardToggle2";
    //private string SELECT_COURSE_TOGGLE_NORMAL_1 = "NormalCourseToggle1";
    //private string SELECT_COURSE_TOGGLE_NORMAL_2 = "NormalCourseToggle2";
    //private string SELECT_COURSE_TOGGLE_HARD_1 = "HardCourseToggle1";
    //private string SELECT_COURSE_TOGGLE_HARD_2 = "HardCourseToggle1";

    /// <summary>
    /// 부산 레드라인 관련 토글
    /// </summary>
    public Toggle RedNormalcourseGroupCurrentSeletion
    {
        get { return RedNormalCourseGroup.ActiveToggles().FirstOrDefault(); }
    }
    public Toggle RedHardCourseGroupCurrentSeletion
    {
        get { return RedHardCourseGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle RedFirstMapGroupCurrentSeletion
    {
        get { return RedMapFirstToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle RedTwoMapGroupCurrentSeletion
    {
        get { return RedMapTwoToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    /// <summary>
    /// 부산 그린라인 관련 토글
    /// </summary>

    public Toggle GreenNormalcourseGroupCurrentSeletion
    {
        get { return GreenNormalcourseGroup.ActiveToggles().FirstOrDefault(); }
    }
    public Toggle GreenHardCourseGroupCurrentSeletion
    {
        get { return GreenHardCourseGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle GreenFirstMapGroupCurrentSeletion
    {
        get { return GreenMapFirstToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle GreenTwoMapGroupCurrentSeletion
    {
        get { return GreenMapTwoToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    //public Toggle itemGroupCurrentSeletion
    //{
    //    get { return itemToggleGroup.ActiveToggles().FirstOrDefault(); }
    //}

    private void Awake()
    {
        stringClass = new BusanMapStringClass();
    }
    void Start()
    {

        Initialization();
        MapChoice_DataManager.instance.ItemInit(cointText, expText, speedText);
    }

    void Initialization()
    {
        //내 정보 - 모델링 위치 초기화
        if (PlayerPrefs.GetString("Busan_Player_Sex") == stringClass.BUSAN_PLAYER_WOMAN)
        {
            woman_player = GameObject.Find(stringClass.BUSAN_PLAYER_WOMAN).GetComponent<Transform>();
            womanctrl_scrip = woman_player.GetComponent<WomanCtrl>();

            //내 정보 - 모델링 위치 초기화
            woman_player.localPosition = new Vector3(15.4f, 7.4f, 7.5f);
            woman_player.localRotation = Quaternion.Euler(0f, -105f, 0f);
            woman_player.localScale = new Vector3(3f, 3f, 3f);
        }
        else if (PlayerPrefs.GetString("Busan_Player_Sex") ==stringClass.BUSAN_PLAYER_MAN)
        {
            man_player = GameObject.Find(stringClass.BUSAN_PLAYER_MAN).GetComponent<Transform>();
            manctrl_scrip = man_player.GetComponent<ManCtrl>();

            //내 정보 - 모델링 위치 초기화
            man_player.localPosition = new Vector3(15.4f, 7.4f, 7.5f);
            man_player.localRotation = Quaternion.Euler(0f, -105f, 0f);
            man_player.localScale = new Vector3(3f, 3f, 3f);
        }

        itemDataBase = GameObject.Find("DataManager");
        sensorManager = GameObject.Find("SensorManager");


        //MapColorChoice(255, 0, 0, 255, 255, 255, 255, 255, 255);
    }

    //아시아 두번째 페이지 노멀맵 코스 선택 토글
    public void NormalCourseToggleClick()
    {
        //레드라인
        if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_RED_LINE))
        {
            if (RedNormalCourseGroup.ActiveToggles().Any())
            {
                if (RedNormalcourseGroupCurrentSeletion.name == stringClass.SELECT_COURSE_TOGGLE_NORMAL_1)
                {
                    //MapColorChoice(255, 0, 0, 255, 255, 255, 255, 255, 255);
                    MapChoice_DataManager.instance.AsiaMapImageShow();  //코스 선택했을 때, 선택한 맵의 이미지 활성화
                }
                else if (RedNormalcourseGroupCurrentSeletion.name == stringClass.SELECT_COURSE_TOGGLE_NORMAL_2)
                {
                    //MapColorChoice(255, 255, 255, 255, 0, 0, 255, 255, 255);
                    MapChoice_DataManager.instance.AsiaMapImageShow();  //코스 선택했을 때, 선택한 맵의 이미지 활성화
                }
            }
        }
        //그린라인
        else if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_GREEN_LINE))
        {
            if (GreenNormalcourseGroup.ActiveToggles().Any())
            {
                if (GreenNormalcourseGroupCurrentSeletion.name == stringClass.SELECT_COURSE_TOGGLE_NORMAL_1)
                {
                    //MapColorChoice(255, 0, 0, 255, 255, 255, 255, 255, 255);
                    MapChoice_DataManager.instance.AsiaMapImageShow();  //코스 선택했을 때, 선택한 맵의 이미지 활성화
                }
                else if (GreenNormalcourseGroupCurrentSeletion.name == stringClass.SELECT_COURSE_TOGGLE_NORMAL_2)
                {
                    //MapColorChoice(255, 255, 255, 255, 0, 0, 255, 255, 255);
                    MapChoice_DataManager.instance.AsiaMapImageShow();  //코스 선택했을 때, 선택한 맵의 이미지 활성화
                }
            }
        }

    }
    //아시아 두번째 페이지 하드맵 코스 선택 토글
    public void HardCourseToggleClick()
    {
        if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_RED_LINE))
        {
            if (RedHardCourseGroup.ActiveToggles().Any())
            {
                if (RedHardCourseGroupCurrentSeletion.name == stringClass.SELECT_COURSE_TOGGLE_HARD_1)
                {
                    MapChoice_DataManager.instance.AsiaMapImageShow();  //코스 선택했을 때, 선택한 맵의 이미지 활성화
                }
                else if (RedHardCourseGroupCurrentSeletion.name == stringClass.SELECT_COURSE_TOGGLE_HARD_2)
                {
                    MapChoice_DataManager.instance.AsiaMapImageShow();  //코스 선택했을 때, 선택한 맵의 이미지 활성화
                }
            }
        }
        else if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_GREEN_LINE))
        {
            if (GreenHardCourseGroup.ActiveToggles().Any())
            {
                if (GreenHardCourseGroupCurrentSeletion.name == stringClass.SELECT_COURSE_TOGGLE_HARD_1)
                {
                    MapChoice_DataManager.instance.AsiaMapImageShow();
                }
                else if (GreenHardCourseGroupCurrentSeletion.name == stringClass.SELECT_COURSE_TOGGLE_HARD_2)
                {
                    MapChoice_DataManager.instance.AsiaMapImageShow();
                }
            }
        }

    }

    //아시아맵 모드 첫번째 페이지에서의 선택
    public void AsiaMapModeChoice()
    {
        if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_RED_LINE))
        {
            if (RedMapFirstToggleGroup.ActiveToggles().Any())
            {
                if (RedFirstMapGroupCurrentSeletion.name == stringClass.SELECT_TOGGLE_NORMAL_1)
                {
                    MapChoice_DataManager.instance.ChoiceMap_PanelMode(true, false);
                    MapChoice_DataManager.instance.HardToggleUnCheck();
                }
                else if (RedFirstMapGroupCurrentSeletion.name == stringClass.SELECT_TOGGLE_HARD_1)
                {
                    MapChoice_DataManager.instance.ChoiceMap_PanelMode(false, true);
                    MapChoice_DataManager.instance.HardToggleCheck();
                }
            }
        }
        else if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_GREEN_LINE))
        {

            if (GreenMapFirstToggleGroup.ActiveToggles().Any())
            {
                if (GreenFirstMapGroupCurrentSeletion.name == stringClass.SELECT_TOGGLE_NORMAL_1)
                {
                    MapChoice_DataManager.instance.ChoiceMap_PanelMode(true, false);
                    MapChoice_DataManager.instance.HardToggleUnCheck();
                }
                else if (GreenFirstMapGroupCurrentSeletion.name == stringClass.SELECT_TOGGLE_HARD_1)
                {
                    MapChoice_DataManager.instance.ChoiceMap_PanelMode(false, true);
                    MapChoice_DataManager.instance.HardToggleCheck();
                }
            }
        }

    }
    //아시아맵 모드 두번째 페이지에서의 선택
    public void AsiaMapModeTwoChoice()
    {
        if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_RED_LINE))
        {
            if (RedMapTwoToggleGroup.ActiveToggles().Any())
            {
                if (RedTwoMapGroupCurrentSeletion.name == stringClass.SELECT_TOGGLE_NORMAL_2)
                {
                    MapChoice_DataManager.instance.AsiaModeButtonFirstToggleIsOn(true, true);
                    MapChoice_DataManager.instance.AsiaMapTogglePanelShow(true, false); //첫번째 토글 선택되게 하기 위함
                }
                else if (RedTwoMapGroupCurrentSeletion.name == stringClass.SELECT_TOGGLE_HARD_2)
                {
                    MapChoice_DataManager.instance.AsiaModeButtonFirstToggleIsOn(true, true);
                    MapChoice_DataManager.instance.AsiaMapTogglePanelShow(false, true); //첫번째 토글 선택되게 하기 위함
                }
            }
        }
        else if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_GREEN_LINE))
        {
            if (GreenMapTwoToggleGroup.ActiveToggles().Any())
            {
                if (GreenTwoMapGroupCurrentSeletion.name == stringClass.SELECT_TOGGLE_NORMAL_2)
                {
                    MapChoice_DataManager.instance.AsiaModeButtonFirstToggleIsOn(true, true);
                    MapChoice_DataManager.instance.AsiaMapTogglePanelShow(true, false); //첫번째 토글 선택되게 하기 위함
                }
                else if (GreenTwoMapGroupCurrentSeletion.name == stringClass.SELECT_TOGGLE_HARD_2)
                {
                    MapChoice_DataManager.instance.AsiaModeButtonFirstToggleIsOn(true, true);
                    MapChoice_DataManager.instance.AsiaMapTogglePanelShow(false, true); //첫번째 토글 선택되게 하기 위함
                }
            }
        }
    }

    //아이템 사용 선택
    public void ItemUseChoice()
    {
        string toggleName = EventSystem.current.currentSelectedGameObject.name;

        Debug.Log("toggleName: " + toggleName);

        if (toggleName.Equals("CoinToggle"))
            PlayerPrefs.SetString("Busan_UseItemCoin", "Coin");

        if (toggleName.Equals("ExpToggle"))
            PlayerPrefs.SetString("Busan_UseItemExp", "Exp");

        if (toggleName.Equals("SpeedToggle"))
            PlayerPrefs.SetString("Busan_UseItemSpeed", "Speed");

        if (itemToggle[0].isOn.Equals(true) || itemToggle[1].isOn.Equals(true) || itemToggle[2].isOn.Equals(true))
        {
            Debug.Log("interactable = true");
            itemUseBtn.interactable = true;
        }
        else if (itemToggle[0].isOn.Equals(false) && itemToggle[1].isOn.Equals(false) && itemToggle[2].isOn.Equals(false))
        {
            Debug.Log("interactable = false");
            itemUseBtn.interactable = false;
        }



        //if (itemToggleGroup.ActiveToggles().Any())
        //{
        //    if(itemGroupCurrentSeletion.name == "CoinToggle")
        //    {
        //        PlayerPrefs.SetString("UseItemName", "Coin");
        //    }
        //    else if (itemGroupCurrentSeletion.name == "ExpToggle")
        //    {
        //        PlayerPrefs.SetString("UseItemName", "Exp");
        //    }
        //    else if (itemGroupCurrentSeletion.name == "SpeedToggle")
        //    {
        //        PlayerPrefs.SetString("UseItemName", "Speed");
        //    }
        //}
    }

    //아시아 노멀맵 선택 버튼
    public void Asia_NormalCorse1_ButtnOn()
    {
        if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_RED_LINE))
        { MapChoice_DataManager.instance.MapChoice_ClickPanelOb_active(true, false); }
        else if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_GREEN_LINE)) 
        { MapChoice_DataManager.instance.MapChoice_ClickPanelOb_active(false, true); }

        MapChoice_DataManager.instance.AisaNormalChoiceToggleIsOn(true, false, true, false);
        MapChoice_DataManager.instance.AsiaMapImageShow();    //코스 선택했을 때, 선택한 맵의 이미지 활성화
    }

    public void Asia_NormalCorse2_ButtnOn()
    {
        if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_RED_LINE))
        { MapChoice_DataManager.instance.MapChoice_ClickPanelOb_active(true, false); }
        else if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_GREEN_LINE))
        { MapChoice_DataManager.instance.MapChoice_ClickPanelOb_active(false, true); }

        MapChoice_DataManager.instance.AisaNormalChoiceToggleIsOn(true, false, false, true);
        MapChoice_DataManager.instance.AsiaMapImageShow();    //코스 선택했을 때, 선택한 맵의 이미지 활성화
    }


    //아시아 하드맵 선택 버튼
    public void Asia_HardCorse1_ButtonOn()
    {
        if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_RED_LINE))
        { MapChoice_DataManager.instance.MapChoice_ClickPanelOb_active(true, false); }
        else if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_GREEN_LINE))
        { MapChoice_DataManager.instance.MapChoice_ClickPanelOb_active(false, true); }

        MapChoice_DataManager.instance.AsiaHardChoiceToggleIsOn(false, true, true, false);
        MapChoice_DataManager.instance.AsiaMapImageShow();    //코스 선택했을 때, 선택한 맵의 이미지 활성화
    }

    public void Asia_HardCorse2_ButtonOn()
    {
        if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_RED_LINE))
        { MapChoice_DataManager.instance.MapChoice_ClickPanelOb_active(true, false); }
        else if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_GREEN_LINE))
        { MapChoice_DataManager.instance.MapChoice_ClickPanelOb_active(false, true); }

        MapChoice_DataManager.instance.AsiaHardChoiceToggleIsOn(false, true, false, true);
        MapChoice_DataManager.instance.AsiaMapImageShow();    //코스 선택했을 때, 선택한 맵의 이미지 활성화
    }


//
//    void Asia_MapShow()
//    {
//        mapImg.gameObject.SetActive(true);
//    }
//


    //지도 색상 변경하는 함수
    void MapColorChoice(int _r, int _g, int _b)
    {
        mapImg.color = new Color(_r, _g, _b);
    }


    //백버튼 이벤트 - 로비 씬 이동
    public void BackButtonOn()
    {
        //Destroy(player.gameObject); //씬 전환 전 모델링 삭제(다시 들어게 되면 중복이 됨)
        //Destroy(sensorManager); //센서 모델링 삭제
        SceneManager.LoadScene("Lobby");
    }
    public void StartRacingBtn()
    {
        //다음 코스 오픈하기 위한 클리어 시간 저장, 맵 이름저장
        MapChoice_DataManager.instance.AsiaMapCourseChoiceOpenTime();

        if (PlayerPrefs.GetString("Busan_GameOnePlay").Equals("MissionOk"))
            PlayerPrefs.SetString("Busan_GameOnePlay", "MissionOk");
        else
            PlayerPrefs.SetString("Busan_GameOnePlay", "Yes");
        Loading_SceneManager.LoadScene("Loading");
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
    public void AsiaMapChoice_LeftBtn()
    {
        if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_RED_LINE))
        {
            SetActive_MapGrup(false, true);
            MapChoice_DataManager.instance.CurrentChoiceMap = stringClass.BUSAN_GREEN_LINE;
        }
        else if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_GREEN_LINE))
        {
            SetActive_MapGrup(true, false);
            MapChoice_DataManager.instance.CurrentChoiceMap = stringClass.BUSAN_RED_LINE;
        }
        PlayerPrefs.SetString("Busan_CurrentMap_Name", MapChoice_DataManager.instance.CurrentChoiceMap);
    }
    public void AsiaMapChoice_RightBtn()
    {
        if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_RED_LINE))
        {
            SetActive_MapGrup(false, true);
            MapChoice_DataManager.instance.CurrentChoiceMap = stringClass.BUSAN_GREEN_LINE;
          
        }
        else if (MapChoice_DataManager.instance.CurrentChoiceMap.Equals(stringClass.BUSAN_GREEN_LINE))
        {
            SetActive_MapGrup(true, false);
            MapChoice_DataManager.instance.CurrentChoiceMap = stringClass.BUSAN_RED_LINE;
        }
        PlayerPrefs.SetString("Busan_CurrentMap_Name", MapChoice_DataManager.instance.CurrentChoiceMap);
    }
   
    private void SetActive_MapGrup(bool red, bool green)
    {
        RedLineMapGroup.SetActive(red);
        GreenLineMapGroup.SetActive(green);
    }

}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using System.Linq;
//using UnityEngine.SceneManagement;
//using UnityEngine.EventSystems;

//public class MapChoice_UIManager : MonoBehaviour
//{
//    public ToggleGroup asiaMapFirstToggleGroup;  //아시아맵 선택 첫번째 나오는 모드선택 토글(노멀, 하드)
//    public ToggleGroup asiaMapTwoToggleGroup;   //아시아맵 선택 두번째 나오는 모드선택 토글 판넬(노멀 하드)
//    public ToggleGroup asiaNormalcourseGroup;  //아시아 모멀모드 선택토글
//    public ToggleGroup asiaHardCourseGroup; //아시아하드모드 선택토글

//    //public ToggleGroup itemToggleGroup; //아이템 토글 그룹
//    public Toggle[] itemToggle;

//    public Image mapImg;  //지도 이미지

//    public Text cointText;
//    public Text expText;
//    public Text speedText;

//    public Button itemUseBtn;


//    GameObject itemDataBase;
//    GameObject sensorManager;
//    Transform woman_player;
//    WomanCtrl womanctrl_scrip;
//    Transform man_player;
//    ManCtrl manctrl_scrip;




//    public Toggle asiaNormalcourseGroupCurrentSeletion
//    {
//        get { return asiaNormalcourseGroup.ActiveToggles().FirstOrDefault(); }
//    }
//    public Toggle asiaHardCourseGroupCurrentSeletion
//    {
//        get { return asiaHardCourseGroup.ActiveToggles().FirstOrDefault(); }
//    }

//    public Toggle asiaFirstMapGroupCurrentSeletion
//    {
//        get { return asiaMapFirstToggleGroup.ActiveToggles().FirstOrDefault(); }
//    }

//    public Toggle asiaTwoMapGroupCurrentSeletion
//    {
//        get { return asiaMapTwoToggleGroup.ActiveToggles().FirstOrDefault(); }
//    }

//    //public Toggle itemGroupCurrentSeletion
//    //{
//    //    get { return itemToggleGroup.ActiveToggles().FirstOrDefault(); }
//    //}


//    void Start()
//    {
//        Initialization();
//        MapChoice_DataManager.instance.ItemInit(cointText, expText, speedText);
//    }

//    void Initialization()
//    {
//        //내 정보 - 모델링 위치 초기화
//        if (PlayerPrefs.GetString("Busan_Player_Sex") == "Woman")
//        {
//            woman_player = GameObject.Find("Woman").GetComponent<Transform>();
//            womanctrl_scrip = woman_player.GetComponent<WomanCtrl>();

//            //내 정보 - 모델링 위치 초기화
//            woman_player.localPosition = new Vector3(15.4f, 7.4f, 7.5f);
//            woman_player.localRotation = Quaternion.Euler(0f, -105f, 0f);
//            woman_player.localScale = new Vector3(3f, 3f, 3f);
//        }
//        else if (PlayerPrefs.GetString("Busan_Player_Sex") == "Man")
//        {
//            man_player = GameObject.Find("Man").GetComponent<Transform>();
//            manctrl_scrip = man_player.GetComponent<ManCtrl>();

//            //내 정보 - 모델링 위치 초기화
//            man_player.localPosition = new Vector3(15.4f, 7.4f, 7.5f);
//            man_player.localRotation = Quaternion.Euler(0f, -105f, 0f);
//            man_player.localScale = new Vector3(3f, 3f, 3f);
//        }

//        itemDataBase = GameObject.Find("DataManager");
//        sensorManager = GameObject.Find("SensorManager");


//        //MapColorChoice(255, 0, 0, 255, 255, 255, 255, 255, 255);
//    }

//    //아시아 두번째 페이지 노멀맵 코스 선택 토글
//    public void AsiaNoramlCourseToggleClick()
//    {
//        if(asiaNormalcourseGroup.ActiveToggles().Any())
//        {
//            if(asiaNormalcourseGroupCurrentSeletion.name == "NormalCourseToggle1")
//            {
//                //MapColorChoice(255, 0, 0, 255, 255, 255, 255, 255, 255);
//                MapChoice_DataManager.instance.AsiaMapImageShow();
//            }
//            else if (asiaNormalcourseGroupCurrentSeletion.name == "NormalCourseToggle2")
//            {
//                //MapColorChoice(255, 255, 255, 255, 0, 0, 255, 255, 255);
//                MapChoice_DataManager.instance.AsiaMapImageShow();
//            }
//        }
//    }
//    //아시아 두번째 페이지 하드맵 코스 선택 토글
//    public void AsiaHardCourseToggleClick()
//    {
//        if(asiaHardCourseGroup.ActiveToggles().Any())
//        {
//            if(asiaHardCourseGroupCurrentSeletion.name == "HardCourseToggle1")
//            {
//                MapChoice_DataManager.instance.AsiaMapImageShow();
//            }
//            else if(asiaHardCourseGroupCurrentSeletion.name == "HardCourseToggle2")
//            {
//                MapChoice_DataManager.instance.AsiaMapImageShow();
//            }
//        }
//    }

//    //아시아맵 모드 첫번째 페이지에서의 선택
//    public void AsiaMapModeChoice()
//    {
//        if(asiaMapFirstToggleGroup.ActiveToggles().Any())
//        {
//            if(asiaFirstMapGroupCurrentSeletion.name == "NormalToggle1")
//            {
//                MapChoice_DataManager.instance.AsiaMapPanelShow(true, false);
//                MapChoice_DataManager.instance.HardToggleUnCheck();
//            }
//            else if(asiaFirstMapGroupCurrentSeletion.name == "HardToggle1")
//            {
//                MapChoice_DataManager.instance.AsiaMapPanelShow(false, true);
//                MapChoice_DataManager.instance.HardToggleCheck();
//            }
//        }
//    }
//    //아시아맵 모드 두번째 페이지에서의 선택
//    public void AsiaMapModeTwoChoice()
//    {
//        if(asiaMapTwoToggleGroup.ActiveToggles().Any())
//        {
//            if(asiaTwoMapGroupCurrentSeletion.name == "NormalToggle2")
//            {
//                MapChoice_DataManager.instance.AsiaModeButtonFirstToggleIsOn(true, true);
//                    MapChoice_DataManager.instance.AsiaMapTogglePanelShow(true, false); //첫번째 토글 선택되게 하기 위함
//            }
//            else if(asiaTwoMapGroupCurrentSeletion.name == "HardToggle2")
//            {
//                MapChoice_DataManager.instance.AsiaModeButtonFirstToggleIsOn(true, true);
//                MapChoice_DataManager.instance.AsiaMapTogglePanelShow(false, true); //첫번째 토글 선택되게 하기 위함
//            }
//        }
//    }

//    //아이템 사용 선택
//    public void ItemUseChoice()
//    {
//        string toggleName = EventSystem.current.currentSelectedGameObject.name;

//        if(toggleName.Equals("CoinToggle"))
//            PlayerPrefs.SetString("Busan_UseItemCoin", "Coin");

//        if (toggleName.Equals("ExpToggle"))
//            PlayerPrefs.SetString("Busan_UseItemExp", "Exp");

//        if (toggleName.Equals("SpeedToggle"))
//            PlayerPrefs.SetString("Busan_UseItemSpeed", "Speed");

//        if (itemToggle[0].isOn.Equals(true) || itemToggle[1].isOn.Equals(true) || itemToggle[2].isOn.Equals(true))
//            itemUseBtn.interactable = true;
//        else if (itemToggle[0].isOn.Equals(false) && itemToggle[1].isOn.Equals(false) && itemToggle[2].isOn.Equals(false))
//            itemUseBtn.interactable = false;


//        //if (itemToggleGroup.ActiveToggles().Any())
//        //{
//        //    if(itemGroupCurrentSeletion.name == "CoinToggle")
//        //    {
//        //        PlayerPrefs.SetString("UseItemName", "Coin");
//        //    }
//        //    else if (itemGroupCurrentSeletion.name == "ExpToggle")
//        //    {
//        //        PlayerPrefs.SetString("UseItemName", "Exp");
//        //    }
//        //    else if (itemGroupCurrentSeletion.name == "SpeedToggle")
//        //    {
//        //        PlayerPrefs.SetString("UseItemName", "Speed");
//        //    }
//        //}
//    }

//    //아시아 노멀맵 선택 버튼
//    public void Asia_NormalCorse1_ButtnOn()
//    {
//        MapChoice_DataManager.instance.AisaNormalChoiceToggleIsOn(true, false, true, false);
//        Asia_MapShow();
//    }

//    public void Asia_NormalCorse2_ButtnOn()
//    {
//        MapChoice_DataManager.instance.AisaNormalChoiceToggleIsOn(true, false, false, true);
//        Asia_MapShow();
//    }


//    //아시아 하드맵 선택 버튼
//    public void Asia_HardCorse1_ButtonOn()
//    {
//        MapChoice_DataManager.instance.AsiaHardChoiceToggleIsOn(false, true, true, false);
//        Asia_MapShow();
//    }

//    public void Asia_HardCorse2_ButtonOn()
//    {
//        MapChoice_DataManager.instance.AsiaHardChoiceToggleIsOn(false, true, false, true);
//        Asia_MapShow();
//    }



//    void Asia_MapShow()
//    {
//        mapImg.gameObject.SetActive(true);
//    }



//    //지도 색상 변경하는 함수
//    void MapColorChoice(int _r, int _g, int _b)
//    {
//        mapImg.color = new Color(_r, _g, _b);
//    }


//    //백버튼 이벤트 - 로비 씬 이동
//    public void BackButtonOn()
//    {
//        //Destroy(player.gameObject); //씬 전환 전 모델링 삭제(다시 들어게 되면 중복이 됨)
//        //Destroy(sensorManager); //센서 모델링 삭제
//        SceneManager.LoadScene("Lobby");
//    }
//    public void StartRacingBtn()
//    {
//        //다음 코스 오픈하기 위한 클리어 시간 저장, 맵 이름저장
//        MapChoice_DataManager.instance.AsiaMapCourseChoiceOpenTime();

//        if (PlayerPrefs.GetString("Busan_GameOnePlay").Equals("MissionOk"))
//            PlayerPrefs.SetString("Busan_GameOnePlay", "MissionOk");
//        else
//            PlayerPrefs.SetString("Busan_GameOnePlay", "Yes");
//        Loading_SceneManager.LoadScene("Loading");
//    }


//    //BGM 사운드
//    public void BGM_SliderSound()
//    {
//        SoundMaixerManager.instance.AudioControl();
//    }

//    //효과음 사운드
//    public void Effect_SliderSound()
//    {
//        SoundMaixerManager.instance.SFXAudioControl();
//    }
//}
