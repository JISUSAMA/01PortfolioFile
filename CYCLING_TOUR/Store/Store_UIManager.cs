using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;


public class Store_UIManager : MonoBehaviour
{
    [Header("토글 그룹")]
    public ToggleGroup topTapToggleGroup;   //상단 탭 토글
    public ToggleGroup downClothesTapToggleGroup;  //하단 의상 탭 토글
    public ToggleGroup downEquipmentTapToggleGroup; //하단 장비 탭 토글
    public ToggleGroup downItemTapToggleGroup;  //하단 아이템 탭 토글

    [Header("활성/비활성 오브젝트")]
    public GameObject clothesPanel; //의상 판넬
    public GameObject equipmentPanel;   //장비 판넬
    public GameObject itemPanel;    //아이템 팔넬
    public GameObject hairView;
    public GameObject shirtView;
    public GameObject pantsView;
    public GameObject shoesView;
    public GameObject helmetView;
    public GameObject glovesView;
    public GameObject bicycleView;
    public GameObject expView;
    public GameObject coinView;
    public GameObject speedView;

    [Header("기본 설정 오브젝트")]
    public Toggle hairToggle;   
    public Toggle bicycleToggle;

    public Text expText;
    public Text coinText;

    public Button buyBtn;   //구매버튼

    //각 아이템 배열에 저장된 번호
    int id_jacketNum, id_pantsNum, id_shoesNum, id_hairNum, id_bodyNum, id_helmetNum, id_glovesNum;

    GameObject itemDataBase;
    GameObject sensorManager;
    Transform woman_player;
    WomanCtrl womanctrl_scrip;
    Transform man_player;
    ManCtrl manctrl_scrip;
    int exp, coin;



    public Toggle topTapToggleCurrentSelection
    {
        get { return topTapToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle downClothesTapToggleCurrentSelection
    {
        get { return downClothesTapToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle downEquipmentTapToggleCurrentSelection
    {
        get { return downEquipmentTapToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle downItemTapToggleCurrentSelection
    {
        get { return downItemTapToggleGroup.ActiveToggles().FirstOrDefault(); }
    }


    private void Awake()
    {
        Initialization();

        //내 정보 - 모델링 위치 초기화
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            woman_player = GameObject.Find("Woman").GetComponent<Transform>();
            womanctrl_scrip = woman_player.GetComponent<WomanCtrl>();

            //내 정보 - 모델링 위치 초기화
            woman_player.localPosition = new Vector3(5.5f, 7.4f, 7.5f);
            woman_player.localRotation = Quaternion.Euler(0f, -105f, 0f);
            woman_player.localScale = new Vector3(3f, 3f, 3f);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            man_player = GameObject.Find("Man").GetComponent<Transform>();
            manctrl_scrip = man_player.GetComponent<ManCtrl>();

            //내 정보 - 모델링 위치 초기화
            man_player.localPosition = new Vector3(5.5f, 7.4f, 7.5f);
            man_player.localRotation = Quaternion.Euler(0f, -105f, 0f);
            man_player.localScale = new Vector3(3f, 3f, 3f);
        }

        itemDataBase = GameObject.Find("DataManager");
        sensorManager = GameObject.Find("SensorManager");
        
        
    }

    void Initialization()
    {
        buyBtn.interactable = false;    //구매버튼 비활성화

        exp = PlayerPrefs.GetInt("AT_Player_CurrExp");
        coin = PlayerPrefs.GetInt("AT_Player_Gold");
        expText.text = exp.ToString();
        coinText.text = Store_DataManager.instance.CommaText(coin);

        
        ToggleGroup_ActiveShow(true, false, false);    //의상토글그룹 활성화 - 장비토글그룹 비활성화
        Clothes_and_Equipment_ActiveShow(true, false, false); //의상 판넬 활성화 - 장비 비활성화 - 아이템 비활성화
        Clothes_ActiveShow(true, false, false, false, false, false);

        
        //각 아이템 배열 넘버 저장
        id_hairNum = PlayerPrefs.GetInt("AT_HairNumber");
        id_bodyNum = PlayerPrefs.GetInt("AT_BodyNumber");
        id_jacketNum = PlayerPrefs.GetInt("AT_JacketNumber");
        id_pantsNum = PlayerPrefs.GetInt("AT_PantsNumber");
        id_shoesNum = PlayerPrefs.GetInt("AT_ShoesNumber");
        id_helmetNum = PlayerPrefs.GetInt("AT_HelmetNumber");
        id_glovesNum = PlayerPrefs.GetInt("AT_GlovesNumber");
    }


    void ToggleGroup_ActiveShow(bool _clothes, bool _equipment, bool _item)
    {
        downClothesTapToggleGroup.gameObject.SetActive(_clothes);
        downEquipmentTapToggleGroup.gameObject.SetActive(_equipment);
        downItemTapToggleGroup.gameObject.SetActive(_item);
    }

    public void TopTapToggleChoice()
    {
        if (topTapToggleGroup.ActiveToggles().Any())
        {
            if (topTapToggleCurrentSelection.name == "ClothesToggle")
            {
                //의상 판넬 활성화 - 장비 비활성화
                Clothes_and_Equipment_ActiveShow(true, false, false);
                hairToggle.isOn = true; //기본 헤어토글이 선택되어 있게하기 위해
                //기본 - 머리뷰 활성화, 나머지 비활성화
                Clothes_ActiveShow(true, false, false, false, false, false);
                ToggleGroup_ActiveShow(true, false, false);    //의상토글그룹 활성화 - 장비토글그룹 비활성화
            }
            else if (topTapToggleCurrentSelection.name == "EquipmentToggle")
            {
                //의상 판넬 비활성화 - 장비 판넬 활성화
                Clothes_and_Equipment_ActiveShow(false, true, false);
                bicycleToggle.isOn = true;  //기본 자전거토글이 선택되어 있게 하기 위해
                Equipment_ActiveShow(true); //기본 - 자전거 뷰 활성화 (추후-추가 시 늘어남)
                ToggleGroup_ActiveShow(false, true, false);    //의상토글그룹 비활성화 - 장비토글그룹 활성화

                //자전거 레벨 달성 시 구매 오픈
                Store_DataManager.instance.BicycleInitBuyMarkShow();
            }
            else if(topTapToggleCurrentSelection.name == "ItemToggle")
            {
                Clothes_and_Equipment_ActiveShow(false, false, true);   //아이템 판넬 활성화 
                Item_ActiveShow(true, false, false);  //기본 - 아이템 뷰 황성화(추후 - 추가 시 늘어남)
                ToggleGroup_ActiveShow(false, false, true); //의상, 장비토글그룹 비활성화
            }

            buyBtn.interactable = false;    //구매버튼 비활성화
        }
    }

    //큰탭 의상-장비 활성화 비활성화
    void Clothes_and_Equipment_ActiveShow(bool _colothes, bool _equipment, bool _item)
    {
        clothesPanel.SetActive(_colothes);
        equipmentPanel.SetActive(_equipment);
        itemPanel.SetActive(_item);
    }

    //의상 하단탭 눌렀을 때 이벤트
    public void DownClothesTapToggleChoice()
    {
        //각 아이템 배열 넘버 저장
        id_hairNum = Store_DataManager.instance.id_hairNum;
        id_bodyNum = Store_DataManager.instance.id_bodyNum;
        id_jacketNum = Store_DataManager.instance.id_jacketNum;
        id_pantsNum = Store_DataManager.instance.id_pantsNum;
        id_shoesNum = Store_DataManager.instance.id_shoesNum;
       // Debug.Log("머리 " + PlayerPrefs.GetInt("AT_HairNumber"));

        //내 정보 - 모델링 위치 초기화
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            //의상 하단탭 눌렀을 때 - 머리, 상의, 하의, 신발, 핼맷, 장갑 순서
            if (downClothesTapToggleGroup.ActiveToggles().Any())
            {
                if (downClothesTapToggleCurrentSelection.name == "HairToggle")
                {
                    Store_DataManager.instance.HairInitBuyMarkShow();   //구매한거 표시
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    Clothes_ActiveShow(true, false, false, false, false, false);
                    //Debug.Log(id_hairNum + " : " + id_bodyNum + " : " + id_jacketNum + " : " + id_pantsNum + " : " + id_shoesNum);

                    //Debug.Log(Store_DataManager.instance.id_hairNum + " : " + Store_DataManager.instance.id_bodyNum + " : " + Store_DataManager.instance.id_jacketNum + " : " +
                    //    Store_DataManager.instance.id_pantsNum + " : " + Store_DataManager.instance.id_shoesNum);
                }
                else if (downClothesTapToggleCurrentSelection.name == "ShirtToggle")
                {
                    Store_DataManager.instance.ShirtInitBuyMarkShow();
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    Clothes_ActiveShow(false, true, false, false, false, false);
                }
                else if (downClothesTapToggleCurrentSelection.name == "PantsToggle")
                {
                    Store_DataManager.instance.PantsInitBuyMarkShow();
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    Clothes_ActiveShow(false, false, true, false, false, false);
                }
                else if (downClothesTapToggleCurrentSelection.name == "ShoesToggle")
                {
                    Store_DataManager.instance.ShoesInitBuyMarkShow();
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    Clothes_ActiveShow(false, false, false, true, false, false);
                }
                else if (downClothesTapToggleCurrentSelection.name == "HelmetToggle")
                {
                    Store_DataManager.instance.HelmetInitBuyMarkShow();
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    Clothes_ActiveShow(false, false, false, false, true, false);
                }
                else if (downClothesTapToggleCurrentSelection.name == "GlovesToggle")
                {
                    Store_DataManager.instance.GlovesInitBuyMarkShow();
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    Clothes_ActiveShow(false, false, false, false, false, true);
                    //womanctrl_scrip.Animator_Initialization(Store_DataManager.instance.id_hairNum, Store_DataManager.instance.id_bodyNum,
                    //    Store_DataManager.instance.id_jacketNum, Store_DataManager.instance.id_pantsNum, Store_DataManager.instance.id_shoesNum);
                }

                buyBtn.interactable = false;    //구매버튼 비활성화
            }
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            //의상 하단탭 눌렀을 때 - 머리, 상의, 하의, 신발, 핼맷, 장갑 순서
            if (downClothesTapToggleGroup.ActiveToggles().Any())
            {
                if (downClothesTapToggleCurrentSelection.name == "HairToggle")
                {
                    Store_DataManager.instance.HairInitBuyMarkShow();   //구매한거 표시
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    Clothes_ActiveShow(true, false, false, false, false, false);
                    //Debug.Log(id_hairNum + " : " + id_bodyNum + " : " + id_jacketNum + " : " + id_pantsNum + " : " + id_shoesNum);

                    //Debug.Log(Store_DataManager.instance.id_hairNum + " : " + Store_DataManager.instance.id_bodyNum + " : " + Store_DataManager.instance.id_jacketNum + " : " +
                    //    Store_DataManager.instance.id_pantsNum + " : " + Store_DataManager.instance.id_shoesNum);
                }
                else if (downClothesTapToggleCurrentSelection.name == "ShirtToggle")
                {
                    Store_DataManager.instance.ShirtInitBuyMarkShow();
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    Clothes_ActiveShow(false, true, false, false, false, false);
                }
                else if (downClothesTapToggleCurrentSelection.name == "PantsToggle")
                {
                    Store_DataManager.instance.PantsInitBuyMarkShow();
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    Clothes_ActiveShow(false, false, true, false, false, false);
                }
                else if (downClothesTapToggleCurrentSelection.name == "ShoesToggle")
                {
                    Store_DataManager.instance.ShoesInitBuyMarkShow();
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    Clothes_ActiveShow(false, false, false, true, false, false);
                }
                else if (downClothesTapToggleCurrentSelection.name == "HelmetToggle")
                {
                    Store_DataManager.instance.HelmetInitBuyMarkShow();
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    Clothes_ActiveShow(false, false, false, false, true, false);
                }
                else if (downClothesTapToggleCurrentSelection.name == "GlovesToggle")
                {
                    Store_DataManager.instance.GlovesInitBuyMarkShow();
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    Clothes_ActiveShow(false, false, false, false, false, true);
                    //womanctrl_scrip.Animator_Initialization(Store_DataManager.instance.id_hairNum, Store_DataManager.instance.id_bodyNum,
                    //    Store_DataManager.instance.id_jacketNum, Store_DataManager.instance.id_pantsNum, Store_DataManager.instance.id_shoesNum);
                }

                buyBtn.interactable = false;    //구매버튼 비활성화
            }
        }
    }

    //의상 활성화 비활성화
    public void Clothes_ActiveShow(bool _hair, bool _shirt, bool _pants, bool _shoes, bool _helmet, bool _gloves)
    {
        hairView.SetActive(_hair);  //머리
        shirtView.SetActive(_shirt);    //윗옷
        pantsView.SetActive(_pants);    //아랫옷
        shoesView.SetActive(_shoes);    //신발
        helmetView.SetActive(_helmet);  //헬맷
        glovesView.SetActive(_gloves);  //장갑
    }

    //하단 장비탭 눌렀을 때
    public void DownEquipmentTapToggleChoice()
    {
        //하단 장비탭 - 자전거
        if (downEquipmentTapToggleGroup.ActiveToggles().Any())
        {
            if (downEquipmentTapToggleCurrentSelection.name == "BicycleToggle")
            {
                Equipment_ActiveShow(true);
                Store_DataManager.instance.BicycleInitBuyMarkShow();
            }
            buyBtn.interactable = false;    //구매버튼 비활성화
        }
    }

    public void DownItemTapToggleChoice()
    {
        if(downItemTapToggleGroup.ActiveToggles().Any())
        {
            if(downItemTapToggleCurrentSelection.name == "ExpToggle")
            {
                Item_ActiveShow(true, false, false);
            }
            else if (downItemTapToggleCurrentSelection.name == "CoinToggle")
            {
                Item_ActiveShow(false, true, false);
            }
            else if (downItemTapToggleCurrentSelection.name == "SpeedToggle")
            {
                Item_ActiveShow(false, false, true);
            }
            buyBtn.interactable = false;    //구매버튼 비활성화
        }
    }

    //하단 판넬 활성화 비활성화
    void Equipment_ActiveShow(bool _bicycle)
    {
        bicycleView.SetActive(_bicycle);    //자전거판넬
    }

    //하단 판넬 황성화 비활성화
    void Item_ActiveShow(bool _exp, bool _coin, bool _speed)
    {
        expView.SetActive(_exp);
        coinView.SetActive(_coin);
        speedView.SetActive(_speed);
    }

    public void InitButtonOnClick()
    {
        Store_DataManager.instance.InitData_ButtonOn();
    }

    //백버튼 - 로비씬
    public void BackButtonOn()
    {

        //PlayerPrefs.SetInt("HairNumber", Store_DataManager.instance.id_bodyNum);
        //PlayerPrefs.SetInt("BodyNumber", Store_DataManager.instance.id_bodyNum);
        //PlayerPrefs.SetInt("JacketNumber", Store_DataManager.instance.id_jacketNum);
        //PlayerPrefs.SetInt("PantsNumber", Store_DataManager.instance.id_pantsNum);
        //PlayerPrefs.SetInt("ShoesNumber", Store_DataManager.instance.id_shoesNum);

        SoundMaixerManager.instance.OutGameBGMPlay();
        SceneManager.LoadScene("Lobby");
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
