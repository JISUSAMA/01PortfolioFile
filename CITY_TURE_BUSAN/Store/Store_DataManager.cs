using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text.RegularExpressions;

public class Store_DataManager : MonoBehaviour
{
    public static Store_DataManager instance { get; private set; }

    public GameObject gameEndPopup; //게임종료팝업

    public ToggleGroup expStoreToggleGroup;
    public ToggleGroup coinStoreToggleGroup;
    public ToggleGroup speedStoreToggleGroup;
    public ToggleGroup marketingToggleGroup;


    public GameObject[] expObj;
    public GameObject[] coinObj;
    public GameObject[] speedObj;

    public Button buyBtn;   //구매버튼

    Item_Information item_info_scrip;


    //각 아이템 배열에 저장된 번호
    public int id_jacketNum, id_pantsNum, id_shoesNum, id_hairNum, id_bodyNum, id_helmetNum, id_glovesNum;
    int init_jacketNum, init_pantsNum, init_shoesNum, init_hairNum, init_bodyNum, init_helmetNum, init_glovesNum;    //처음 입은 옷
    string init_jacket_s, init_pants_s, init_shoes_s, init_helmet_s, init_gloves_s; //처음 입은 옷 텍스쳐
    string hairStyleStr;    //머리 스타일
    //각 아이템 한번 클릭했는지 여부(클릭했을 때 장갑, 헬맷이 입혀짐)
    //bool touchItem_Hair, touchItem_Jacket, touchItem_Pants, touchItem_Shoes, touchItem_Helmet, touchItem_Gloves;
    //bool buy_Hair, buy_Jacket, buy_Pants, buy_Shoes, buy_Helmet, buy_Gloves;    //구매했는지 여부

    //여자 캐릭터
    GameObject womanPlayer;
    //WomanCtrl womanctrl_scrip;  //220804수정
    NewWomanCtrl womanctrl_scrip;   //220804수정

    //남자 캐릭터
    GameObject manPlayer;
    //ManCtrl manctrl_scrip;  //220804수정
    NewManCtrl manctrl_scrip;  //220804수정


    public Toggle expStoreCurrentSeletion
    {
        get { return expStoreToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle coinStoreCurrentSeletion
    {
        get { return coinStoreToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle speedStoreCurrentSeletion
    {
        get { return speedStoreToggleGroup.ActiveToggles().FirstOrDefault(); }
    }
    public Toggle marketStoreCurrentSeletion
    {
        get { return marketingToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }


    void Start()
    {
        //Debug.Log("머리 --- " + PlayerPrefs.GetString("Busan_Player_Hair"));
        Initialization();
    }

    //숫자 콤마 찍는 함수
    public string CommaText(int _data)
    {
        if (_data != 0)
            return string.Format("{0:#,###}", _data);
        else
            return "0";
    }

    void Initialization()
    {
        //내 정보 - 모델링 위치 초기화
        if (PlayerPrefs.GetString("Busan_Player_Sex") == "Woman")
        {
            womanPlayer = GameObject.Find("Woman");
            //womanctrl_scrip = womanPlayer.GetComponent<WomanCtrl>();    //220804수정
            womanctrl_scrip = womanPlayer.GetComponent<NewWomanCtrl>();    //220804수정
        }
        else if (PlayerPrefs.GetString("Busan_Player_Sex") == "Man")
        {
            manPlayer = GameObject.Find("Man");
            //manctrl_scrip = manPlayer.GetComponent<ManCtrl>();  //220804수정
            manctrl_scrip = manPlayer.GetComponent<NewManCtrl>();  //220804수정
        }
            


        //각 아이템 배열 넘버 저장
        //id_hairNum = PlayerPrefs.GetInt("Busan_HairNumber");
        //id_bodyNum = PlayerPrefs.GetInt("Busan_BodyNumber");
        //id_jacketNum = PlayerPrefs.GetInt("Busan_JacketNumber");
        //id_pantsNum = PlayerPrefs.GetInt("Busan_PantsNumber");
        //id_shoesNum = PlayerPrefs.GetInt("Busan_ShoesNumber");
        ////if(PlayerPrefs.GetString("Wear_HelmetKind") == "ItemID_Helmet")
        //    id_helmetNum = PlayerPrefs.GetInt("Busan_HelmetNumber");  
        ////if(PlayerPrefs.GetString("Wear_GlovesKind") == "ItemID_Gloves")
        //    id_glovesNum = PlayerPrefs.GetInt("Busan_GlovesNumber");

        ////초기화 버튼 눌렀을 때 이벤트를 위해 초기값 유지 변수
        //init_hairNum = PlayerPrefs.GetInt("Busan_HairNumber");
        //init_bodyNum = PlayerPrefs.GetInt("Busan_BodyNumber");
        //init_jacketNum = PlayerPrefs.GetInt("Busan_JacketNumber");
        //init_pantsNum = PlayerPrefs.GetInt("Busan_PantsNumber");
        //init_shoesNum = PlayerPrefs.GetInt("Busan_ShoesNumber");
        ////if (PlayerPrefs.GetString("Wear_HelmetKind") == "ItemID_Helmet")
        //    init_helmetNum = PlayerPrefs.GetInt("Busan_HelmetNumber");
        ////if (PlayerPrefs.GetString("Wear_GlovesKind") == "ItemID_Gloves")
        //    init_glovesNum = PlayerPrefs.GetInt("Busan_GlovesNumber");

        //hairStyleStr = PlayerPrefs.GetString("Busan_Player_Hair");
        //init_jacket_s = PlayerPrefs.GetString("Busan_Wear_JacketStyleName");
        //init_pants_s = PlayerPrefs.GetString("Busan_Wear_PantsStyleName");
        //init_shoes_s = PlayerPrefs.GetString("Busan_Wear_ShoesStyleName");
        //init_helmet_s = PlayerPrefs.GetString("Busan_Wear_HelmetStyleName");
        //init_gloves_s = PlayerPrefs.GetString("Busan_Wear_GlovesStyleName");

        //Debug.Log(init_helmetNum + " _____________ " + init_glovesNum + " +++ " + init_pants_s);
    }

    
    public void ExpChoiceClickOn()
    {
        if(expStoreToggleGroup.ActiveToggles().Any())
        {
            string intTamp = Regex.Replace(expStoreCurrentSeletion.name, @"\D", "");    //숫자 추출
            PlayerPrefs.SetString("Busan_BuyItemToggleName", expStoreCurrentSeletion.name);  //클릭 토글 이름 저장
            PlayerPrefs.SetString("Busan_BuyCompleteImageName", "ItemStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

            if (expStoreCurrentSeletion.name == "ExpStoreToggle1")
            {
                ExpCustomShowBuy(0);
            }
            else if (expStoreCurrentSeletion.name == "ExpStoreToggle2")
            {
                ExpCustomShowBuy(1);
            }
            else if (expStoreCurrentSeletion.name == "ExpStoreToggle3")
            {
                ExpCustomShowBuy(2);
            }
            else if (expStoreCurrentSeletion.name == "ExpStoreToggle4")
            {

            }
            buyBtn.interactable = true; //구매버튼 활성화
        }
    }

    public void CoinChoiceClickOn()
    {
        if (coinStoreToggleGroup.ActiveToggles().Any())
        {
            string intTamp = Regex.Replace(coinStoreCurrentSeletion.name, @"\D", "");    //숫자 추출
            PlayerPrefs.SetString("Busan_BuyItemToggleName", coinStoreCurrentSeletion.name);  //클릭 토글 이름 저장
            PlayerPrefs.SetString("Busan_BuyCompleteImageName", "ItemStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

            if (coinStoreCurrentSeletion.name == "CoinStoreToggle1")
            {
                CoinCustomShowBuy(0);
            }
            else if (coinStoreCurrentSeletion.name == "CoinStoreToggle2")
            {
                CoinCustomShowBuy(1);
            }
            else if (coinStoreCurrentSeletion.name == "CoinStoreToggle3")
            {
                CoinCustomShowBuy(2);
            }
            else if (coinStoreCurrentSeletion.name == "CoinStoreToggle4")
            {

            }
            buyBtn.interactable = true; //구매버튼 활성화
        }
    }

    public void SpeedChoiceClickOn()
    {
        if (speedStoreToggleGroup.ActiveToggles().Any())
        {
            string intTamp = Regex.Replace(speedStoreCurrentSeletion.name, @"\D", "");    //숫자 추출
            PlayerPrefs.SetString("Busan_BuyItemToggleName", speedStoreCurrentSeletion.name);  //클릭 토글 이름 저장
            PlayerPrefs.SetString("Busan_BuyCompleteImageName", "ItemStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

            if (speedStoreCurrentSeletion.name == "SpeedStoreToggle1")
            {
                SpeedCustomShowBuy(0);
            }
            else if (speedStoreCurrentSeletion.name == "SpeedStoreToggle2")
            {
                SpeedCustomShowBuy(1);
            }
            else if (speedStoreCurrentSeletion.name == "SpeedStoreToggle3")
            {
                SpeedCustomShowBuy(2);
            }
            else if (speedStoreCurrentSeletion.name == "SpeedStoreToggle4")
            {

            }
            buyBtn.interactable = true; //구매버튼 활성화
        }
    }


    void ExpCustomShowBuy(int _index)
    {
        item_info_scrip = expObj[_index].GetComponent<Item_Information>();

        PlayerPrefs.SetInt("Busan_StoreBuy_Price", item_info_scrip.price);
        PlayerPrefs.SetString("Busan_StoreBuy_FolderName", item_info_scrip.folder);
        PlayerPrefs.SetString("Busan_StoreBuy_ImageName", item_info_scrip.imgname);
        PlayerPrefs.SetString("Busan_StoreBuyTitle", item_info_scrip.style);
    }

    void CoinCustomShowBuy(int _index)
    {
        item_info_scrip = coinObj[_index].GetComponent<Item_Information>();

        PlayerPrefs.SetInt("Busan_StoreBuy_Price", item_info_scrip.price);
        PlayerPrefs.SetString("Busan_StoreBuy_FolderName", item_info_scrip.folder);
        PlayerPrefs.SetString("Busan_StoreBuy_ImageName", item_info_scrip.imgname);
        PlayerPrefs.SetString("Busan_StoreBuyTitle", item_info_scrip.style);
    }

    void SpeedCustomShowBuy(int _index)
    {
        item_info_scrip = speedObj[_index].GetComponent<Item_Information>();

        PlayerPrefs.SetInt("Busan_StoreBuy_Price", item_info_scrip.price);
        PlayerPrefs.SetString("Busan_StoreBuy_FolderName", item_info_scrip.folder);
        PlayerPrefs.SetString("Busan_StoreBuy_ImageName", item_info_scrip.imgname);
        PlayerPrefs.SetString("Busan_StoreBuyTitle", item_info_scrip.style);
    }

    //구매한 상품 계산하는 함수(구매 팝업에서)
    public void BuyItem_Interactable(string _itemName)
    {
        //Debug.Log("구매 이름 : " + PlayerPrefs.GetString("Busan_StoreBuy_ImageName"));
        GameObject test = GameObject.Find(PlayerPrefs.GetString("Busan_BuyItemToggleName"));  //어떤 오브젝트 클릭한지 각 토글이름
        item_info_scrip = test.GetComponent<Item_Information>();
        ItemDataBase.instance.items[item_info_scrip.indexNum].buyState = true; 
        

        //Debug.Log(PlayerPrefs.GetString("Busan_BuyCompleteImageName"));
        GameObject image = test.transform.Find(PlayerPrefs.GetString("Busan_BuyCompleteImageName")).gameObject;

        if(_itemName == "ExpPlus" || _itemName == "ExpUp" || _itemName == "CoinUp" || _itemName == "SpeedUp")
        {
            test.gameObject.GetComponent<Toggle>().interactable = true;    
            image.SetActive(false);  //구매했다고 판매완료이미지 나오는거
        }
        else
        {
            test.gameObject.GetComponent<Toggle>().interactable = false;    //선택 못하게(구매 했다)
            image.SetActive(true);  //구매했다고 판매완료이미지 나오는거
        }

        //WearItem(PlayerPrefs.GetString("Busan_BuyItemToggleName"));
        //WearItemStyleSave(PlayerPrefs.GetString("Busan_StoreBuy_FolderName"), PlayerPrefs.GetString("Busan_StoreBuy_ImageName"));
    }

    //구매한 아이템 데이터 저장 - 장착아이템종류/장착아이템 넘버
    public void WearItem(string _buyItemToggleName)
    {
        //id_hairNum = PlayerPrefs.GetInt("HairNumber");
        //id_jacketNum = PlayerPrefs.GetInt("JacketNumber");
        //id_pantsNum = PlayerPrefs.GetInt("PantsNumber");
        //id_shoesNum = PlayerPrefs.GetInt("ShoesNumber");
        //Debug.Log("내가삿다 " + _buyItemToggleName);
        if (_buyItemToggleName == "HairStoreToggle1" || _buyItemToggleName == "HairStoreToggle2" || _buyItemToggleName == "HairStoreToggle3")
        {
            PlayerPrefs.SetInt("Busan_HairNumber", id_hairNum);
            PlayerPrefs.SetString("Busan_Player_Hair", hairStyleStr);
            CustomChange(); //커스텀했는지 여부    
        } 
        else if(_buyItemToggleName == "ShirtStoreToggle7" || _buyItemToggleName == "ShirtStoreToggle2" || _buyItemToggleName == "ShirtStoreToggle3" ||
            _buyItemToggleName == "ShirtStoreToggle4" || _buyItemToggleName == "ShirtStoreToggle5" || _buyItemToggleName == "ShirtStoreToggle6")
        {
            PlayerPrefs.SetInt("Busan_JacketNumber", id_jacketNum);
            if (id_jacketNum == 1)
                PlayerPrefs.SetString("Busan_Wear_JacketKind", "ItemID_Tshirt");
            else if(id_jacketNum == 2)
                PlayerPrefs.SetString("Busan_Wear_JacketKind", "ItemID_LongShirt");

            CustomChange(); //커스텀했는지 여부    
        } 
        else if (_buyItemToggleName == "PantsStoreToggle7" || _buyItemToggleName == "PantsStoreToggle2" || _buyItemToggleName == "PantsStoreToggle3" ||
            _buyItemToggleName == "PantsStoreToggle4" || _buyItemToggleName == "PantsStoreToggle5" || _buyItemToggleName == "PantsStoreToggle6")
        {
            PlayerPrefs.SetInt("Busan_PantsNumber", id_pantsNum);
            if (id_pantsNum == 0)
                PlayerPrefs.SetString("Busan_Wear_PantsKind", "ItemID_Short");
            else if (id_pantsNum == 1)
                PlayerPrefs.SetString("Busan_Wear_PantsKind", "ItemID_LongPants");

            CustomChange(); //커스텀했는지 여부    
        } 
        else if (_buyItemToggleName == "ShoesStoreToggle3" || _buyItemToggleName == "ShoesStoreToggle2" || _buyItemToggleName == "ShoesStoreToggle3")
        {
            PlayerPrefs.SetInt("Busan_ShoesNumber", id_shoesNum);
            if (id_shoesNum == 1)
                PlayerPrefs.SetString("Busan_Wear_ShoesKind", "ItemID_Shoes");

            CustomChange(); //커스텀했는지 여부    
        }
        else if (_buyItemToggleName == "HelmetStoreToggle1" || _buyItemToggleName == "HelmetStoreToggle2" || _buyItemToggleName == "HelmetStoreToggle3")
        {
            PlayerPrefs.SetInt("Busan_HelmetNumber", id_helmetNum);   
            PlayerPrefs.SetString("Busan_Wear_HelmetKind", "ItemID_Helmet");  //헬맷 착용

            CustomChange(); //커스텀했는지 여부    
        }
        else if (_buyItemToggleName == "GlovesStoreToggle1" || _buyItemToggleName == "GlovesStoreToggle2" || _buyItemToggleName == "GlovesStoreToggle3")
        {
            PlayerPrefs.SetInt("Busan_GlovesNumber", id_glovesNum);
            PlayerPrefs.SetString("Busan_Wear_GlovesKind", "ItemID_Gloves");  //헬맷 착용

            CustomChange(); //커스텀했는지 여부    
        }
    }

    void CustomChange()
    {
        //Debug.Log("내가삿다 CustomChange");

        if (PlayerPrefs.GetString("Busan_CustomChange").Equals("MissionOk"))
            PlayerPrefs.SetString("Busan_CustomChange", "MissionOk");
        else
            PlayerPrefs.SetString("Busan_CustomChange", "Yes");

        //Debug.Log("----------- " + PlayerPrefs.GetString("Busan_CustomChange"));
    }

    public void WearItemStyleSave(string _folder, string _imgName)
    {
        if(_folder == "Jacket")
        {
            PlayerPrefs.SetString("Busan_Wear_JacketStyleName", _imgName);
        }
        else if (_folder == "Pants")
        {
            PlayerPrefs.SetString("Busan_Wear_PantsStyleName", _imgName);
        }
        else if (_folder == "Shoes")
        {
            PlayerPrefs.SetString("Busan_Wear_ShoesStyleName", _imgName);
        }
        else if (_folder == "Helmet")
        {
            PlayerPrefs.SetString("Busan_Wear_HelmetStyleName", _imgName);
        }
        else if (_folder == "Gloves")
        {
            PlayerPrefs.SetString("Busan_Wear_GlovesStyleName", _imgName);
        }
    }
    
   
    public void InitData_ButtonOn()
    {
        //if (PlayerPrefs.GetString("Busan_Player_Sex") == "Woman")
        //{
        //    if (init_shoesNum == 0)
        //        womanctrl_scrip.Animator_Initialization(init_hairNum, init_bodyNum, init_jacketNum, init_pantsNum, init_shoesNum);
        //    else if (init_shoesNum == 2)
        //        womanctrl_scrip.Animator_Initialization(init_hairNum, init_bodyNum, init_jacketNum, init_pantsNum, init_shoesNum-1);

        //    //머리 초기화
        //    if (init_hairNum == 0)
        //        womanctrl_scrip.HairSetting(true, false, false);
        //    else if (init_hairNum == 1)
        //        womanctrl_scrip.HairSetting(false, true, false);
        //    else if (init_hairNum == 2)
        //        womanctrl_scrip.HairSetting(false, false, true);

        //    //상의 초기화
        //    if (init_jacketNum == 0)
        //        womanctrl_scrip.JacketSetting(true, false, false);
        //    else if (init_jacketNum == 1)
        //        womanctrl_scrip.JacketSetting(false, true, false);
        //    else if (init_jacketNum == 2)
        //        womanctrl_scrip.JacketSetting(false, false, true);

        //    womanctrl_scrip.JacketTextrueSetting(init_jacket_s, init_jacketNum);

        //    //하의 초기화
        //    if (init_pantsNum == 0)
        //        womanctrl_scrip.PantsSetting(true, false);
        //    else if (init_pantsNum == 1)
        //        womanctrl_scrip.PantsSetting(false, true);

        //    womanctrl_scrip.PantsTextrueSetting(init_pants_s, init_pantsNum);

        //    //신발 초기화
        //    if (init_shoesNum == 0)
        //        womanctrl_scrip.ShoesSetting(true, false);
        //    else if (init_shoesNum == 2)
        //        womanctrl_scrip.ShoesSetting(false, true);

        //    womanctrl_scrip.ShoesTextrueSetting(init_shoes_s, init_shoesNum);

        //    //헬맷 초기화
        //    if (init_helmetNum == 0)
        //    {
        //        womanctrl_scrip.HelmetSetting(true);
        //        womanctrl_scrip.HelmetTextrueSetting(init_helmet_s, init_helmetNum);
        //    }
        //    else if(init_helmetNum == 100)
        //        womanctrl_scrip.HelmetSetting(false);

        //    //장갑 초기화
        //    if (init_glovesNum == 0)
        //    {
        //        womanctrl_scrip.GlovesSetting(true);
        //        womanctrl_scrip.GlovesTextrueSetting(init_gloves_s, init_glovesNum);
        //    }
        //    else if(init_glovesNum == 100)
        //        womanctrl_scrip.GlovesSetting(false);
        //}
        //else if(PlayerPrefs.GetString("Busan_Player_Sex") == "Man")
        //{
        //    if (init_shoesNum == 0)
        //        manctrl_scrip.Animator_Initialization(init_hairNum, init_bodyNum, init_jacketNum, init_pantsNum, init_shoesNum);
        //    else if (init_shoesNum == 2)
        //        manctrl_scrip.Animator_Initialization(init_hairNum, init_bodyNum, init_jacketNum, init_pantsNum, init_shoesNum-1);

        //    //머리 초기화
        //    if (init_hairNum == 0)
        //        manctrl_scrip.HairSetting(true, false, false, false);
        //    else if (init_hairNum == 1)
        //        manctrl_scrip.HairSetting(false, true, false, false);
        //    else if (init_hairNum == 2)
        //        manctrl_scrip.HairSetting(false, false, true, false);
        //    else if (init_hairNum == 3)
        //        manctrl_scrip.HairSetting(false, false, false, true);

        //    //상의 초기화
        //    if (init_jacketNum == 0)
        //        manctrl_scrip.JacketSetting(true, false, false);
        //    else if (init_jacketNum == 1)
        //        manctrl_scrip.JacketSetting(false, true, false);
        //    else if (init_jacketNum == 2)
        //        manctrl_scrip.JacketSetting(false, false, true);

        //    manctrl_scrip.JacketTextrueSetting(init_jacket_s, init_jacketNum);

        //    //하의 초기화
        //    if (init_pantsNum == 0)
        //        manctrl_scrip.PantsSetting(true, false);
        //    else if (init_pantsNum == 1)
        //        manctrl_scrip.PantsSetting(false, true);

        //    manctrl_scrip.PantsTextrueSetting(init_pants_s, init_pantsNum);

        //    //신발 초기화
        //    if (init_shoesNum == 0)
        //        manctrl_scrip.ShoesSetting(true, false);
        //    else if (init_shoesNum == 2)
        //        manctrl_scrip.ShoesSetting(false, true);

        //    manctrl_scrip.ShoesTextrueSetting(init_shoes_s, init_shoesNum);

        //    //헬맷 초기화
        //    if (init_helmetNum == 0)
        //    {
        //        manctrl_scrip.HelmetSetting(true);
        //        manctrl_scrip.HelmetTextrueSetting(init_helmet_s, init_helmetNum);
        //    }
        //    else if (init_helmetNum == 100)
        //        manctrl_scrip.HelmetSetting(false);

        //    //장갑 초기화
        //    if (init_glovesNum == 0)
        //    {
        //        manctrl_scrip.GlovesSetting(true);
        //        manctrl_scrip.GlovesTextrueSetting(init_gloves_s, init_glovesNum);
        //    }
        //    else if (init_glovesNum == 100)
        //        manctrl_scrip.GlovesSetting(false);
        //}

        //id_hairNum = init_hairNum;
        //id_bodyNum = init_bodyNum;
        //id_jacketNum = init_jacketNum;
        //id_pantsNum = init_pantsNum;
        //id_shoesNum = init_shoesNum;
        //id_helmetNum = init_helmetNum;
        //id_glovesNum = init_glovesNum;
    }


    public void ConnectButtonOn()
    {
        GameObject sensor = GameObject.Find("SensorManager");
        ArduinoHM10Test2 sensor_Script = sensor.GetComponent<ArduinoHM10Test2>();

        sensor_Script.StartProcess();
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
