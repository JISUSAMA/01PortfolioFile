using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MapChoice_UIManager : MonoBehaviour
{
    public ToggleGroup asiaMapFirstToggleGroup;  //아시아맵 선택 첫번째 나오는 모드선택 토글(노멀, 하드)
    public ToggleGroup asiaMapTwoToggleGroup;   //아시아맵 선택 두번째 나오는 모드선택 토글 판넬(노멀 하드)
    public ToggleGroup asiaNormalcourseGroup;  //아시아 모멀모드 선택토글
    public ToggleGroup asiaHardCourseGroup; //아시아하드모드 선택토글

    //public ToggleGroup itemToggleGroup; //아이템 토글 그룹
    public Toggle[] itemToggle;

    public Image[] mapImg;  //지도 이미지

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


    

    public Toggle asiaNormalcourseGroupCurrentSeletion
    {
        get { return asiaNormalcourseGroup.ActiveToggles().FirstOrDefault(); }
    }
    public Toggle asiaHardCourseGroupCurrentSeletion
    {
        get { return asiaHardCourseGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle asiaFirstMapGroupCurrentSeletion
    {
        get { return asiaMapFirstToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle asiaTwoMapGroupCurrentSeletion
    {
        get { return asiaMapTwoToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    //public Toggle itemGroupCurrentSeletion
    //{
    //    get { return itemToggleGroup.ActiveToggles().FirstOrDefault(); }
    //}


    void Start()
    {
        Initialization();
        MapChoice_DataManager.instance.ItemInit(cointText, expText, speedText);
    }

    void Initialization()
    {
        //내 정보 - 모델링 위치 초기화
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            woman_player = GameObject.Find("Woman").GetComponent<Transform>();
            womanctrl_scrip = woman_player.GetComponent<WomanCtrl>();

            //내 정보 - 모델링 위치 초기화
            woman_player.localPosition = new Vector3(15.4f, 7.4f, 7.5f);
            woman_player.localRotation = Quaternion.Euler(0f, -105f, 0f);
            woman_player.localScale = new Vector3(3f, 3f, 3f);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            man_player = GameObject.Find("Man").GetComponent<Transform>();
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
    public void AsiaNoramlCourseToggleClick()
    {
        if(asiaNormalcourseGroup.ActiveToggles().Any())
        {
            if(asiaNormalcourseGroupCurrentSeletion.name == "NormalCourseToggle1")
            {
                Debug.Log("노멀코스로 들어옴");
                //MapColorChoice(255, 0, 0, 255, 255, 255, 255, 255, 255);
                MapChoice_DataManager.instance.AsiaMapImageShow(true, false, false);
            }
            else if (asiaNormalcourseGroupCurrentSeletion.name == "NormalCourseToggle2")
            {
                //MapColorChoice(255, 255, 255, 255, 0, 0, 255, 255, 255);
                MapChoice_DataManager.instance.AsiaMapImageShow(false, true, false);
            }
            else if (asiaNormalcourseGroupCurrentSeletion.name == "NormalCourseToggle3")
            {
                //MapColorChoice(255, 255, 255, 255, 255, 255, 255, 0, 0);
                MapChoice_DataManager.instance.AsiaMapImageShow(false, false, true);
            }
        }
    }
    //아시아 두번째 페이지 하드맵 코스 선택 토글
    public void AsiaHardCourseToggleClick()
    {
        if(asiaHardCourseGroup.ActiveToggles().Any())
        {
            if(asiaHardCourseGroupCurrentSeletion.name == "HardCourseToggle1")
            {
                MapChoice_DataManager.instance.AsiaMapImageShow(true, false, false);
            }
            else if(asiaHardCourseGroupCurrentSeletion.name == "HardCourseToggle2")
            {
                MapChoice_DataManager.instance.AsiaMapImageShow(false, true, false);
            }
            else if(asiaHardCourseGroupCurrentSeletion.name == "HardCourseToggle3")
            {
                MapChoice_DataManager.instance.AsiaMapImageShow(false, false, true);
            }
        }
    }

    //아시아맵 모드 첫번째 페이지에서의 선택
    public void AsiaMapModeChoice()
    {
        if(asiaMapFirstToggleGroup.ActiveToggles().Any())
        {
            if(asiaFirstMapGroupCurrentSeletion.name == "NormalToggle1")
            {
                MapChoice_DataManager.instance.AsiaMapPanelShow(true, false);
                MapChoice_DataManager.instance.HardToggleUnCheck();
            }
            else if(asiaFirstMapGroupCurrentSeletion.name == "HardToggle1")
            {
                MapChoice_DataManager.instance.AsiaMapPanelShow(false, true);
                MapChoice_DataManager.instance.HardToggleCheck();
            }
        }
    }
    //아시아맵 모드 두번째 페이지에서의 선택
    public void AsiaMapModeTwoChoice()
    {
        if(asiaMapTwoToggleGroup.ActiveToggles().Any())
        {
            if(asiaTwoMapGroupCurrentSeletion.name == "NormalToggle2")
            {
                MapChoice_DataManager.instance.AsiaModeButtonFirstToggleIsOn(true, true);
                    MapChoice_DataManager.instance.AsiaMapTogglePanelShow(true, false); //첫번째 토글 선택되게 하기 위함
            }
            else if(asiaTwoMapGroupCurrentSeletion.name == "HardToggle2")
            {
                MapChoice_DataManager.instance.AsiaModeButtonFirstToggleIsOn(true, true);
                MapChoice_DataManager.instance.AsiaMapTogglePanelShow(false, true); //첫번째 토글 선택되게 하기 위함
            }
        }
    }

    //아이템 사용 선택
    public void ItemUseChoice()
    {
        string toggleName = EventSystem.current.currentSelectedGameObject.name;

        if (toggleName.Equals("CoinToggle"))
            PlayerPrefs.SetString("AT_UseItemCoin", "Coin");

        if (toggleName.Equals("ExpToggle"))
            PlayerPrefs.SetString("AT_UseItemExp", "Exp");

        if (toggleName.Equals("SpeedToggle"))
            PlayerPrefs.SetString("AT_UseItemSpeed", "Speed");

        if (itemToggle[0].isOn.Equals(true) || itemToggle[1].isOn.Equals(true) || itemToggle[2].isOn.Equals(true))
            itemUseBtn.interactable = true;
        else if (itemToggle[0].isOn.Equals(false) && itemToggle[1].isOn.Equals(false) && itemToggle[2].isOn.Equals(false))
            itemUseBtn.interactable = false;

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
        MapChoice_DataManager.instance.AisaNormalChoiceToggleIsOn(true, false, true, false, false);
        Asia_MapShow(true, false, false);
    }

    public void Asia_NormalCorse2_ButtnOn()
    {
        MapChoice_DataManager.instance.AisaNormalChoiceToggleIsOn(true, false, false, true, false);
        Asia_MapShow(false, true, false);
    }

    public void Asia_NormalCorse3_ButtonOn()
    {
        MapChoice_DataManager.instance.AisaNormalChoiceToggleIsOn(true, false, false, false, true);
        Asia_MapShow(false, false, true);
    }

    //아시아 하드맵 선택 버튼
    public void Asia_HardCorse1_ButtonOn()
    {
        MapChoice_DataManager.instance.AsiaHardChoiceToggleIsOn(false, true, true, false, false);
        Asia_MapShow(true, false, false);
    }

    public void Asia_HardCorse2_ButtonOn()
    {
        MapChoice_DataManager.instance.AsiaHardChoiceToggleIsOn(false, true, false, true, false);
        Asia_MapShow(false, true, false);
    }

    public void Asia_HardCorse3_ButtonOn()
    {
        MapChoice_DataManager.instance.AsiaHardChoiceToggleIsOn(false, true, false, false, true);
        Asia_MapShow(false, false, true);
    }



    void Asia_MapShow(bool _img1, bool _img2, bool _img3)
    {
        mapImg[0].gameObject.SetActive(_img1);
        mapImg[1].gameObject.SetActive(_img2);
        mapImg[2].gameObject.SetActive(_img3);
    }



    //지도 색상 변경하는 함수
    void MapColorChoice(int _r, int _g, int _b, int _r2, int _g2, int _b2, int _r3, int _g3, int _b3)
    {
        mapImg[0].color = new Color(_r, _g, _b);
        mapImg[1].color = new Color(_r2, _g2, _b2);
        mapImg[2].color = new Color(_r3, _g3, _b3);
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

        if (PlayerPrefs.GetString("AT_GameOnePlay").Equals("MissionOk"))
            PlayerPrefs.SetString("AT_GameOnePlay", "MissionOk");
        else
            PlayerPrefs.SetString("AT_GameOnePlay", "Yes");
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
}
