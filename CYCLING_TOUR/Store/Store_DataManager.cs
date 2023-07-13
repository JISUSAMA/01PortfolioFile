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

    public ToggleGroup hairStoreToggleGroup;
    public ToggleGroup shirtStoreToggleGroup;
    public ToggleGroup pantsStoreToggleGroup;
    public ToggleGroup shoesStoreToggleGroup;
    public ToggleGroup helmetStoreToggleGroup;
    public ToggleGroup glovesStoreToggleGroup;
    public ToggleGroup bicycleStoreToggleGroup;
    public ToggleGroup expStoreToggleGroup;
    public ToggleGroup coinStoreToggleGroup;
    public ToggleGroup speedStoreToggleGroup;


    public GameObject[] hairObj;
    public GameObject[] shirtObj;
    public GameObject[] pantsObj;
    public GameObject[] shoesObj;
    public GameObject[] helmetObj;
    public GameObject[] glovesObj;
    public GameObject[] bicycleObj;
    public GameObject[] expObj;
    public GameObject[] coinObj;
    public GameObject[] speedObj;

    public Button buyBtn;   //구매버튼

    Item_Information item_info_scrip;


    //각 아이템 배열에 저장된 번호
    public int id_jacketNum, id_pantsNum, id_shoesNum, id_hairNum, id_bodyNum, id_helmetNum, id_glovesNum, id_bicycleNum;
    int init_jacketNum, init_pantsNum, init_shoesNum, init_hairNum, init_bodyNum, init_helmetNum, init_glovesNum, init_bicycleNum;    //처음 입은 옷
    string init_jacket_s, init_pants_s, init_shoes_s, init_helmet_s, init_gloves_s, init_bicycle_s; //처음 입은 옷 텍스쳐
    string hairStyleStr;    //머리 스타일
    //각 아이템 한번 클릭했는지 여부(클릭했을 때 장갑, 헬맷이 입혀짐)
    //bool touchItem_Hair, touchItem_Jacket, touchItem_Pants, touchItem_Shoes, touchItem_Helmet, touchItem_Gloves;
    //bool buy_Hair, buy_Jacket, buy_Pants, buy_Shoes, buy_Helmet, buy_Gloves;    //구매했는지 여부

    //여자 캐릭터
    GameObject womanPlayer;
    WomanCtrl womanctrl_scrip;

    //남자 캐릭터
    GameObject manPlayer;
    ManCtrl manctrl_scrip;


    public Toggle hairStoreCurrentSeletion
    {
        get { return hairStoreToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle shirtStoreCurrentSeletion
    {
        get { return shirtStoreToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle pantsStoreCurrentSeletion
    {
        get { return pantsStoreToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle shoesStoreCurrentSeletion
    {
        get { return shoesStoreToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle helmetStoreCurrentSeletion
    {
        get { return helmetStoreToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle glovesStoreCurrentSeletion
    {
        get { return glovesStoreToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle bicycleStoreCurrentSeletion
    {
        get { return bicycleStoreToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

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


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }


    void Start()
    {
        //Debug.Log("머리 --- " + PlayerPrefs.GetString("AT_Player_Hair"));
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
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            womanPlayer = GameObject.Find("Woman");
            womanctrl_scrip = womanPlayer.GetComponent<WomanCtrl>();
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            manPlayer = GameObject.Find("Man");
            manctrl_scrip = manPlayer.GetComponent<ManCtrl>();
        }
            


        //각 아이템 배열 넘버 저장
        id_hairNum = PlayerPrefs.GetInt("AT_HairNumber");
        id_bodyNum = PlayerPrefs.GetInt("AT_BodyNumber");
        id_jacketNum = PlayerPrefs.GetInt("AT_JacketNumber");
        id_pantsNum = PlayerPrefs.GetInt("AT_PantsNumber");
        id_shoesNum = PlayerPrefs.GetInt("AT_ShoesNumber");
        //if(PlayerPrefs.GetString("Wear_HelmetKind") == "ItemID_Helmet")
            id_helmetNum = PlayerPrefs.GetInt("AT_HelmetNumber");  
        //if(PlayerPrefs.GetString("Wear_GlovesKind") == "ItemID_Gloves")
            id_glovesNum = PlayerPrefs.GetInt("AT_GlovesNumber");

        //초기화 버튼 눌렀을 때 이벤트를 위해 초기값 유지 변수
        init_hairNum = PlayerPrefs.GetInt("AT_HairNumber");
        init_bodyNum = PlayerPrefs.GetInt("AT_BodyNumber");
        init_jacketNum = PlayerPrefs.GetInt("AT_JacketNumber");
        init_pantsNum = PlayerPrefs.GetInt("AT_PantsNumber");
        init_shoesNum = PlayerPrefs.GetInt("AT_ShoesNumber");
        //if (PlayerPrefs.GetString("Wear_HelmetKind") == "ItemID_Helmet")
            init_helmetNum = PlayerPrefs.GetInt("AT_HelmetNumber");
        //if (PlayerPrefs.GetString("Wear_GlovesKind") == "ItemID_Gloves")
            init_glovesNum = PlayerPrefs.GetInt("AT_GlovesNumber");
        init_bicycleNum = PlayerPrefs.GetInt("AT_BicycleNumber");

        hairStyleStr = PlayerPrefs.GetString("AT_Player_Hair");
        init_jacket_s = PlayerPrefs.GetString("AT_Wear_JacketStyleName");
        init_pants_s = PlayerPrefs.GetString("AT_Wear_PantsStyleName");
        init_shoes_s = PlayerPrefs.GetString("AT_Wear_ShoesStyleName");
        init_helmet_s = PlayerPrefs.GetString("AT_Wear_HelmetStyleName");
        init_gloves_s = PlayerPrefs.GetString("AT_Wear_GlovesStyleName");
        init_bicycle_s = PlayerPrefs.GetString("AT_Wear_BicycleStyleName");

        //Debug.Log(init_helmetNum + " _____________ " + init_glovesNum + " +++ " + init_pants_s);
    }


    //구매한 마크 표시
    public void HairInitBuyMarkShow()
    {
        for (int i = 0; i < hairObj.Length; i++)
        {
            item_info_scrip = hairObj[i].GetComponent<Item_Information>();
            if(item_info_scrip.buyState == true)
            {
                hairObj[i].GetComponent<Toggle>().interactable = false;
                hairObj[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void ShirtInitBuyMarkShow()
    {
        //레벨10일때 오픈
        if (PlayerPrefs.GetInt("AT_Player_Level") >= 10)
        {
            for(int i = 1; i < 4; i++)
            {
                shirtObj[i].GetComponent<Toggle>().interactable = true; //클릭할 수 있게
                shirtObj[i].transform.GetChild(2).gameObject.SetActive(false);  //오픈레벨이미지 비활성화
            }
        }

        //퀘스트 전제 한바퀴
        if(PlayerPrefs.GetString("AT_AllOneFinish").Equals("MissionOk"))
        {
            shirtObj[4].GetComponent<Toggle>().interactable = true; //클릭할 수 있게
            shirtObj[4].transform.GetChild(2).gameObject.SetActive(false);  //오픈레벨이미지 비활성화
        }
        //퀘스트 전제 10바퀴
        if (PlayerPrefs.GetString("AT_AllTenFinish").Equals("MissionOk"))
        {
            shirtObj[5].GetComponent<Toggle>().interactable = true; //클릭할 수 있게
            shirtObj[5].transform.GetChild(2).gameObject.SetActive(false);  //오픈레벨이미지 비활성화
        }
        //퀘스트 전체 20바퀴
        if (PlayerPrefs.GetString("AT_AllTwentyFinish").Equals("MissionOk"))
        {
            shirtObj[6].GetComponent<Toggle>().interactable = true; //클릭할 수 있게
            shirtObj[6].transform.GetChild(2).gameObject.SetActive(false);  //오픈레벨이미지 비활성화
        }


        for (int i = 0; i < shirtObj.Length; i++)
        {
            item_info_scrip = shirtObj[i].GetComponent<Item_Information>();
            if (item_info_scrip.buyState == true)
            {
                shirtObj[i].GetComponent<Toggle>().interactable = false;
                shirtObj[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void PantsInitBuyMarkShow()
    {
        //레벨20일때 오픈
        if (PlayerPrefs.GetInt("AT_Player_Level") >= 20)
        {
            for (int i = 1; i < 4; i++)
            {
                pantsObj[i].GetComponent<Toggle>().interactable = true; //클릭할 수 있게
                pantsObj[i].transform.GetChild(2).gameObject.SetActive(false);  //오픈레벨이미지 비활성화
            }
        }


        //퀘스트 총거리 500
        if (PlayerPrefs.GetString("AT_Distance500Km").Equals("MissionOk"))
        {
            pantsObj[4].GetComponent<Toggle>().interactable = true; //클릭할 수 있게
            pantsObj[4].transform.GetChild(2).gameObject.SetActive(false);  //오픈레벨이미지 비활성화
        }
        //퀘스트 총거리 1000
        if (PlayerPrefs.GetString("AT_Distance1000Km").Equals("MissionOk"))
        {
            pantsObj[5].GetComponent<Toggle>().interactable = true; //클릭할 수 있게
            pantsObj[5].transform.GetChild(2).gameObject.SetActive(false);  //오픈레벨이미지 비활성화
        }
        //퀘스트 총거리 1500
        if (PlayerPrefs.GetString("AT_Distance1500Km").Equals("MissionOk"))
        {
            pantsObj[6].GetComponent<Toggle>().interactable = true; //클릭할 수 있게
            pantsObj[6].transform.GetChild(2).gameObject.SetActive(false);  //오픈레벨이미지 비활성화
        }


        for (int i = 0; i < pantsObj.Length; i++)
        {
            item_info_scrip = pantsObj[i].GetComponent<Item_Information>();
            if (item_info_scrip.buyState == true)
            {
                pantsObj[i].GetComponent<Toggle>().interactable = false;
                pantsObj[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void ShoesInitBuyMarkShow()
    {
        //레벨40일때 오픈
        if (PlayerPrefs.GetInt("AT_Player_Level") >= 40)
        {
            for (int i = 1; i < 4; i++)
            {
                shoesObj[i].GetComponent<Toggle>().interactable = true; //클릭할 수 있게
                shoesObj[i].transform.GetChild(2).gameObject.SetActive(false);  //오픈레벨이미지 비활성화
            }
        }


        for (int i = 0; i < shoesObj.Length; i++)
        {
            item_info_scrip = shoesObj[i].GetComponent<Item_Information>();
            if (item_info_scrip.buyState == true)
            {
                shoesObj[i].GetComponent<Toggle>().interactable = false;
                shoesObj[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void HelmetInitBuyMarkShow()
    {
        //레벨30일때 오픈
        if (PlayerPrefs.GetInt("AT_Player_Level") >= 30)
        {
            for (int i = 0; i < 3; i++)
            {
                helmetObj[i].GetComponent<Toggle>().interactable = true; //클릭할 수 있게
                helmetObj[i].transform.GetChild(2).gameObject.SetActive(false);  //오픈레벨이미지 비활성화
            }
        }


        for (int i = 0; i < helmetObj.Length; i++)
        {
            item_info_scrip = helmetObj[i].GetComponent<Item_Information>();
            if (item_info_scrip.buyState == true)
            {
                helmetObj[i].GetComponent<Toggle>().interactable = false;
                helmetObj[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void GlovesInitBuyMarkShow()
    {
        //레벨40일때 오픈
        if (PlayerPrefs.GetInt("AT_Player_Level") >= 40)
        {
            for (int i = 0; i < 3; i++)
            {
                glovesObj[i].GetComponent<Toggle>().interactable = true; //클릭할 수 있게
                glovesObj[i].transform.GetChild(2).gameObject.SetActive(false);  //오픈레벨이미지 비활성화
            }
        }


        for (int i = 0; i < glovesObj.Length; i++)
        {
            item_info_scrip = glovesObj[i].GetComponent<Item_Information>();
            if (item_info_scrip.buyState == true)
            {
                glovesObj[i].GetComponent<Toggle>().interactable = false;
                glovesObj[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void BicycleInitBuyMarkShow()
    {
        //레벨30일때 오픈
        if (PlayerPrefs.GetInt("AT_Player_Level") >= 30)
        {
            for (int i = 1; i < 4; i++)
            {
                bicycleObj[i].GetComponent<Toggle>().interactable = true; //클릭할 수 있게
                bicycleObj[i].transform.GetChild(2).gameObject.SetActive(false);  //오픈레벨이미지 비활성화
            }
        }

        for (int i = 0; i < bicycleObj.Length; i++)
        {
            item_info_scrip = bicycleObj[i].GetComponent<Item_Information>();
            if(item_info_scrip.buyState.Equals(true))
            {
                bicycleObj[i].GetComponent<Toggle>().interactable = false;
                bicycleObj[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }


    public void HairItemChoiceClickOn()
    {
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            if (hairStoreToggleGroup.ActiveToggles().Any())
            {
                string intTamp = Regex.Replace(hairStoreCurrentSeletion.name, @"\D", "");    //숫자 추출
                PlayerPrefs.SetString("AT_BuyItemToggleName", hairStoreCurrentSeletion.name);  //클릭 토글 이름 저장
                PlayerPrefs.SetString("AT_BuyCompleteImageName", "HairStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

                if (hairStoreCurrentSeletion.name == "HairStoreToggle1")
                {
                    id_hairNum = 0; //touchItem_Hair = true;  //선택해봄
                    hairStyleStr = "Hair1";
                    //Debug.Log("touchItem_hair" + touchItem_hair);
                    //머리, 바디, 상의, 하의, 신발, 아이템(장갑, 헬맷)
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    HairCustomShow(id_hairNum);
                }
                else if (hairStoreCurrentSeletion.name == "HairStoreToggle2")
                {
                    id_hairNum = 1; //touchItem_Hair = true;  //선택해봄
                    hairStyleStr = "Hair2";
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    HairCustomShow(id_hairNum);
                }
                else if (hairStoreCurrentSeletion.name == "HairStoreToggle3")
                {
                    id_hairNum = 2; //touchItem_Hair = true;  //선택해봄
                    hairStyleStr = "Hair3";
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    HairCustomShow(id_hairNum);
                }

                buyBtn.interactable = true; //구매버튼 활성화
            }
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            if (hairStoreToggleGroup.ActiveToggles().Any())
            {
                string intTamp = Regex.Replace(hairStoreCurrentSeletion.name, @"\D", "");    //숫자 추출
                PlayerPrefs.SetString("AT_BuyItemToggleName", hairStoreCurrentSeletion.name);  //클릭 토글 이름 저장
                PlayerPrefs.SetString("AT_BuyCompleteImageName", "HairStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

                if (hairStoreCurrentSeletion.name == "HairStoreToggle1")
                {
                    id_hairNum = 0; //touchItem_Hair = true;  //선택해봄
                    hairStyleStr = "Hair1";
                    //Debug.Log("touchItem_hair" + touchItem_hair);
                    //머리, 바디, 상의, 하의, 신발, 아이템(장갑, 헬맷)
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    HairCustomShow(id_hairNum);
                }
                else if (hairStoreCurrentSeletion.name == "HairStoreToggle2")
                {
                    id_hairNum = 1; //touchItem_Hair = true;  //선택해봄
                    hairStyleStr = "Hair2";
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    HairCustomShow(id_hairNum);
                }
                else if (hairStoreCurrentSeletion.name == "HairStoreToggle3")
                {
                    id_hairNum = 2; //touchItem_Hair = true;  //선택해봄
                    hairStyleStr = "Hair3";
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    HairCustomShow(id_hairNum);
                }

                buyBtn.interactable = true; //구매버튼 활성화
            }
        }
    }

    public void ShirtItemChoiceClickOn()
    {
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            if (shirtStoreToggleGroup.ActiveToggles().Any())
            {
                string intTamp = Regex.Replace(shirtStoreCurrentSeletion.name, @"\D", "");    //숫자 추출
                PlayerPrefs.SetString("AT_BuyItemToggleName", shirtStoreCurrentSeletion.name);  //클릭 토글 이름 저장
                PlayerPrefs.SetString("AT_BuyCompleteImageName", "ShirtStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

                if (shirtStoreCurrentSeletion.name == "ShirtStoreToggle1")
                {
                    id_jacketNum = 0; //touchItem_Jacket = true; //선택함 
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShirtCustomShow(0, "BasicNasi", id_jacketNum);
                }
                else if (shirtStoreCurrentSeletion.name == "ShirtStoreToggle2")
                {
                    id_jacketNum = 1; //touchItem_Jacket = true; //선택함 
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShirtCustomShow(1, "Tshirt1", id_jacketNum);
                }
                else if (shirtStoreCurrentSeletion.name == "ShirtStoreToggle3")
                {
                    id_jacketNum = 1; //touchItem_Jacket = true; //선택함 
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShirtCustomShow(2, "Tshirt2", id_jacketNum);    //토글배치순서, 텍스쳐이름, 장착아이템번호
                }
                else if (shirtStoreCurrentSeletion.name == "ShirtStoreToggle4")
                {
                    id_jacketNum = 1; //touchItem_Jacket = true; //선택함 
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShirtCustomShow(3, "Tshirt3", id_jacketNum);
                }
                else if (shirtStoreCurrentSeletion.name == "ShirtStoreToggle5")
                {
                    id_jacketNum = 2; //touchItem_Jacket = true; //선택함 
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShirtCustomShow(4, "LongShirt1", id_jacketNum);
                }
                else if (shirtStoreCurrentSeletion.name == "ShirtStoreToggle6")
                {
                    id_jacketNum = 2; //touchItem_Jacket = true; //선택함 
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShirtCustomShow(5, "LongShirt2", id_jacketNum);
                }
                else if (shirtStoreCurrentSeletion.name == "ShirtStoreToggle7")
                {
                    id_jacketNum = 2; //touchItem_Jacket = true; //선택함 
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShirtCustomShow(6, "LongShirt3", id_jacketNum);
                }

                buyBtn.interactable = true; //구매버튼 활성화
            }
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            if (shirtStoreToggleGroup.ActiveToggles().Any())
            {
                string intTamp = Regex.Replace(shirtStoreCurrentSeletion.name, @"\D", "");    //숫자 추출
                PlayerPrefs.SetString("AT_BuyItemToggleName", shirtStoreCurrentSeletion.name);  //클릭 토글 이름 저장
                PlayerPrefs.SetString("AT_BuyCompleteImageName", "ShirtStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

                if (shirtStoreCurrentSeletion.name == "ShirtStoreToggle1")
                {
                    id_jacketNum = 0; //touchItem_Jacket = true; //선택함 
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShirtCustomShow(0, "BasicNasi", id_jacketNum);
                }
                else if (shirtStoreCurrentSeletion.name == "ShirtStoreToggle2")
                {
                    id_jacketNum = 1; //touchItem_Jacket = true; //선택함 
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShirtCustomShow(1, "Tshirt1", id_jacketNum);
                }
                else if (shirtStoreCurrentSeletion.name == "ShirtStoreToggle3")
                {
                    id_jacketNum = 1; //touchItem_Jacket = true; //선택함 
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShirtCustomShow(2, "Tshirt2", id_jacketNum);    //토글배치순서, 텍스쳐이름, 장착아이템번호
                }
                else if (shirtStoreCurrentSeletion.name == "ShirtStoreToggle4")
                {
                    id_jacketNum = 1; //touchItem_Jacket = true; //선택함 
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShirtCustomShow(3, "Tshirt3", id_jacketNum);
                }
                else if (shirtStoreCurrentSeletion.name == "ShirtStoreToggle5")
                {
                    id_jacketNum = 2; //touchItem_Jacket = true; //선택함 
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShirtCustomShow(4, "LongShirt1", id_jacketNum);
                }
                else if (shirtStoreCurrentSeletion.name == "ShirtStoreToggle6")
                {
                    id_jacketNum = 2; //touchItem_Jacket = true; //선택함 
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShirtCustomShow(5, "LongShirt2", id_jacketNum);
                }
                else if (shirtStoreCurrentSeletion.name == "ShirtStoreToggle7")
                {
                    id_jacketNum = 2; //touchItem_Jacket = true; //선택함 
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShirtCustomShow(6, "LongShirt3", id_jacketNum);
                }

                buyBtn.interactable = true; //구매버튼 활성화
            }
        }
    }

    public void PantsItemChoiceClickOn()
    {
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            if (pantsStoreToggleGroup.ActiveToggles().Any())
            {
                string intTamp = Regex.Replace(pantsStoreCurrentSeletion.name, @"\D", "");    //숫자 추출
                PlayerPrefs.SetString("AT_BuyItemToggleName", pantsStoreCurrentSeletion.name);  //클릭 토글 이름 저장
                PlayerPrefs.SetString("AT_BuyCompleteImageName", "PantsStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

                if (pantsStoreCurrentSeletion.name == "PantsStoreToggle1")
                {
                    id_pantsNum = 0; //touchItem_Pants = true;    //선택함
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    PantsCustomShow(0, "BasicShort", id_pantsNum);
                }
                else if (pantsStoreCurrentSeletion.name == "PantsStoreToggle2")
                {
                    id_pantsNum = 0; //touchItem_Pants = true;    //선택함
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    PantsCustomShow(1, "Short1", id_pantsNum);
                }
                else if (pantsStoreCurrentSeletion.name == "PantsStoreToggle3")
                {
                    id_pantsNum = 0; //touchItem_Pants = true;    //선택함
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    PantsCustomShow(2, "Short2", id_pantsNum);
                }
                else if (pantsStoreCurrentSeletion.name == "PantsStoreToggle4")
                {
                    id_pantsNum = 0; //touchItem_Pants = true;    //선택함
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    PantsCustomShow(3, "Short3", id_pantsNum);
                }
                else if (pantsStoreCurrentSeletion.name == "PantsStoreToggle5")
                {
                    id_pantsNum = 1; //touchItem_Pants = true;    //선택함
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    PantsCustomShow(4, "LongPants1", id_pantsNum);
                }
                else if (pantsStoreCurrentSeletion.name == "PantsStoreToggle6")
                {
                    id_pantsNum = 1; //touchItem_Pants = true;    //선택함
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    PantsCustomShow(5, "LongPants2", id_pantsNum);
                }
                else if (pantsStoreCurrentSeletion.name == "PantsStoreToggle7")
                {
                    id_pantsNum = 1; //touchItem_Pants = true;    //선택함
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    PantsCustomShow(6, "LongPants3", id_pantsNum);
                }

                buyBtn.interactable = true; //구매버튼 활성화
            }
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            if (pantsStoreToggleGroup.ActiveToggles().Any())
            {
                string intTamp = Regex.Replace(pantsStoreCurrentSeletion.name, @"\D", "");    //숫자 추출
                PlayerPrefs.SetString("AT_BuyItemToggleName", pantsStoreCurrentSeletion.name);  //클릭 토글 이름 저장
                PlayerPrefs.SetString("AT_BuyCompleteImageName", "PantsStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

                if (pantsStoreCurrentSeletion.name == "PantsStoreToggle1")
                {
                    id_pantsNum = 0; //touchItem_Pants = true;    //선택함
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    PantsCustomShow(0, "BasicShort", id_pantsNum);
                }
                else if (pantsStoreCurrentSeletion.name == "PantsStoreToggle2")
                {
                    id_pantsNum = 0; //touchItem_Pants = true;    //선택함
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    PantsCustomShow(1, "Short1", id_pantsNum);
                }
                else if (pantsStoreCurrentSeletion.name == "PantsStoreToggle3")
                {
                    id_pantsNum = 0; //touchItem_Pants = true;    //선택함
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    PantsCustomShow(2, "Short2", id_pantsNum);
                }
                else if (pantsStoreCurrentSeletion.name == "PantsStoreToggle4")
                {
                    id_pantsNum = 0; //touchItem_Pants = true;    //선택함
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    PantsCustomShow(3, "Short3", id_pantsNum);
                }
                else if (pantsStoreCurrentSeletion.name == "PantsStoreToggle5")
                {
                    id_pantsNum = 1; //touchItem_Pants = true;    //선택함
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    PantsCustomShow(4, "LongPants1", id_pantsNum);
                }
                else if (pantsStoreCurrentSeletion.name == "PantsStoreToggle6")
                {
                    id_pantsNum = 1; //touchItem_Pants = true;    //선택함
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    PantsCustomShow(5, "LongPants2", id_pantsNum);
                }
                else if (pantsStoreCurrentSeletion.name == "PantsStoreToggle7")
                {
                    id_pantsNum = 1; //touchItem_Pants = true;    //선택함
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    PantsCustomShow(6, "LongPants3", id_pantsNum);
                }

                buyBtn.interactable = true; //구매버튼 활성화
            }
        }
    }

    public void ShoesItemChoiceClickOn()
    {
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            if (shoesStoreToggleGroup.ActiveToggles().Any())
            {
                string intTamp = Regex.Replace(shoesStoreCurrentSeletion.name, @"\D", "");    //숫자 추출
                PlayerPrefs.SetString("AT_BuyItemToggleName", shoesStoreCurrentSeletion.name);  //클릭 토글 이름 저장
                PlayerPrefs.SetString("AT_BuyCompleteImageName", "ShoesStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

                if (shoesStoreCurrentSeletion.name == "ShoesStoreToggle1")
                {
                    id_shoesNum = 0; //touchItem_Shoes = true; //선택함
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShoesCustomShow(0, "BasicSandal", 0);
                }
                else if (shoesStoreCurrentSeletion.name == "ShoesStoreToggle2")
                {
                    id_shoesNum = 1; //touchItem_Shoes = true; //선택함
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShoesCustomShow(1, "Shoes1", 2); //2 -> 신발 메테리어 숫자
                }
                else if (shoesStoreCurrentSeletion.name == "ShoesStoreToggle3")
                {
                    id_shoesNum = 1; //touchItem_Shoes = true; //선택함
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShoesCustomShow(2, "Shoes2", 2); //2 -> 신발 메테리어 숫자
                }
                else if (shoesStoreCurrentSeletion.name == "ShoesStoreToggle4")
                {
                    id_shoesNum = 1; //touchItem_Shoes = true; //선택함
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShoesCustomShow(3, "Shoes3", 2); //2 -> 신발 메테리어 숫자
                }

                buyBtn.interactable = true; //구매버튼 활성화
            }
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            if (shoesStoreToggleGroup.ActiveToggles().Any())
            {
                string intTamp = Regex.Replace(shoesStoreCurrentSeletion.name, @"\D", "");    //숫자 추출
                PlayerPrefs.SetString("AT_BuyItemToggleName", shoesStoreCurrentSeletion.name);  //클릭 토글 이름 저장
                PlayerPrefs.SetString("AT_BuyCompleteImageName", "ShoesStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

                if (shoesStoreCurrentSeletion.name == "ShoesStoreToggle1")
                {
                    id_shoesNum = 0; //touchItem_Shoes = true; //선택함
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShoesCustomShow(0, "BasicSandal", 0);
                }
                else if (shoesStoreCurrentSeletion.name == "ShoesStoreToggle2")
                {
                    id_shoesNum = 1; //touchItem_Shoes = true; //선택함
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShoesCustomShow(1, "Shoes1", 2);    //2 -> 신발 메테리어 숫자
                }
                else if (shoesStoreCurrentSeletion.name == "ShoesStoreToggle3")
                {
                    id_shoesNum = 1; //touchItem_Shoes = true; //선택함
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShoesCustomShow(2, "Shoes2", 2); //2 -> 신발 메테리어 숫자
                }
                else if (shoesStoreCurrentSeletion.name == "ShoesStoreToggle4")
                {
                    id_shoesNum = 1; //touchItem_Shoes = true; //선택함
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    ShoesCustomShow(3, "Shoes3", 2); //2 -> 신발 메테리어 숫자
                }

                buyBtn.interactable = true; //구매버튼 활성화
            }
        }
        //Debug.Log("id_shoesNum --- >  "+ id_shoesNum);
    }

    public void HelmetItemChoiceClickOn()
    {
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            if (helmetStoreToggleGroup.ActiveToggles().Any())
            {
                string intTamp = Regex.Replace(helmetStoreCurrentSeletion.name, @"\D", "");    //숫자 추출
                PlayerPrefs.SetString("AT_BuyItemToggleName", helmetStoreCurrentSeletion.name);  //클릭 토글 이름 저장
                PlayerPrefs.SetString("AT_BuyCompleteImageName", "HelmetStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

                if (helmetStoreCurrentSeletion.name == "HelmetStoreToggle1")
                {
                    id_helmetNum = 0; //touchItem_Helmet = true; //아이템 선택
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    HelmetCustomShow(0, "Helmet1", id_helmetNum);
                }
                else if (helmetStoreCurrentSeletion.name == "HelmetStoreToggle2")
                {
                    id_helmetNum = 0; //touchItem_Helmet = true; //아이템 선택
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    HelmetCustomShow(1, "Helmet2", id_helmetNum);
                }
                else if (helmetStoreCurrentSeletion.name == "HelmetStoreToggle3")
                {
                    id_helmetNum = 0; //touchItem_Helmet = true; //아이템 선택
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    HelmetCustomShow(2, "Helmet3", id_helmetNum);
                }

                buyBtn.interactable = true; //구매버튼 활성화
            }
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            if (helmetStoreToggleGroup.ActiveToggles().Any())
            {
                string intTamp = Regex.Replace(helmetStoreCurrentSeletion.name, @"\D", "");    //숫자 추출
                PlayerPrefs.SetString("AT_BuyItemToggleName", helmetStoreCurrentSeletion.name);  //클릭 토글 이름 저장
                PlayerPrefs.SetString("AT_BuyCompleteImageName", "HelmetStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

                if (helmetStoreCurrentSeletion.name == "HelmetStoreToggle1")
                {
                    id_helmetNum = 0; //touchItem_Helmet = true; //아이템 선택
                    if (id_hairNum == 1)
                    {
                        id_hairNum = 3; //헬맷 머리로 변경 
                        PlayerPrefs.SetString("AT_Player_Hair", "HelmetHair");
                        manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                        manctrl_scrip.HairSetting(false, false, false, true);
                    }
                    else
                        manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    HelmetCustomShow(0, "Helmet1", id_helmetNum);
                }
                else if (helmetStoreCurrentSeletion.name == "HelmetStoreToggle2")
                {
                    id_helmetNum = 0; //touchItem_Helmet = true; //아이템 선택
                    if (id_hairNum == 1)
                    {
                        id_hairNum = 3; //헬맷 머리로 변경 
                        PlayerPrefs.SetString("AT_Player_Hair", "HelmetHair");
                        manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                        manctrl_scrip.HairSetting(false, false, false, true);
                    }
                    else
                        manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    HelmetCustomShow(1, "Helmet2", id_helmetNum);
                }
                else if (helmetStoreCurrentSeletion.name == "HelmetStoreToggle3")
                {
                    id_helmetNum = 0; //touchItem_Helmet = true; //아이템 선택
                    if (id_hairNum == 1)
                    {
                        id_hairNum = 3; //헬맷 머리로 변경 
                        PlayerPrefs.SetString("AT_Player_Hair", "HelmetHair");
                        manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                        manctrl_scrip.HairSetting(false, false, false, true);
                    }
                    else
                        manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    HelmetCustomShow(2, "Helmet3", id_helmetNum);
                }

                buyBtn.interactable = true; //구매버튼 활성화
            }
        }
    }

    public void GlovesItemChoiceClickOn()
    {
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            if (glovesStoreToggleGroup.ActiveToggles().Any())
            {
                string intTamp = Regex.Replace(glovesStoreCurrentSeletion.name, @"\D", "");    //숫자 추출
                PlayerPrefs.SetString("AT_BuyItemToggleName", glovesStoreCurrentSeletion.name);  //클릭 토글 이름 저장
                PlayerPrefs.SetString("AT_BuyCompleteImageName", "GlovesStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

                if (glovesStoreCurrentSeletion.name == "GlovesStoreToggle1")
                {
                    id_glovesNum = 0; //touchItem_Gloves = true;  //아이템 선택
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    GlovesCustomShow(0, "Gloves1", id_glovesNum);
                }
                else if (glovesStoreCurrentSeletion.name == "GlovesStoreToggle2")
                {
                    id_glovesNum = 0; //touchItem_Gloves = true;  //아이템 선택
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    GlovesCustomShow(1, "Gloves2", id_glovesNum);
                }
                else if (glovesStoreCurrentSeletion.name == "GlovesStoreToggle3")
                {
                    id_glovesNum = 0; //touchItem_Gloves = true;  //아이템 선택
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    GlovesCustomShow(2, "Gloves3", id_glovesNum);
                }

                buyBtn.interactable = true; //구매버튼 활성화
            }
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            if (glovesStoreToggleGroup.ActiveToggles().Any())
            {
                string intTamp = Regex.Replace(glovesStoreCurrentSeletion.name, @"\D", "");    //숫자 추출
                PlayerPrefs.SetString("AT_BuyItemToggleName", glovesStoreCurrentSeletion.name);  //클릭 토글 이름 저장
                PlayerPrefs.SetString("AT_BuyCompleteImageName", "GlovesStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

                if (glovesStoreCurrentSeletion.name == "GlovesStoreToggle1")
                {
                    id_glovesNum = 0; //touchItem_Gloves = true;  //아이템 선택
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    GlovesCustomShow(0, "Gloves1", id_glovesNum);
                }
                else if (glovesStoreCurrentSeletion.name == "GlovesStoreToggle2")
                {
                    id_glovesNum = 0; //touchItem_Gloves = true;  //아이템 선택
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    GlovesCustomShow(1, "Gloves2", id_glovesNum);
                }
                else if (glovesStoreCurrentSeletion.name == "GlovesStoreToggle3")
                {
                    id_glovesNum = 0; //touchItem_Gloves = true;  //아이템 선택
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    GlovesCustomShow(2, "Gloves3", id_glovesNum);
                }

                buyBtn.interactable = true; //구매버튼 활성화
            }
        }
    }

    public void BicycleItemChoiceClickOn()
    {
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            if (bicycleStoreToggleGroup.ActiveToggles().Any())
            {
                string intTamp = Regex.Replace(bicycleStoreCurrentSeletion.name, @"\D", "");    //숫자 추출
                PlayerPrefs.SetString("AT_BuyItemToggleName", bicycleStoreCurrentSeletion.name);  //클릭 토글 이름 저장
                PlayerPrefs.SetString("AT_BuyCompleteImageName", "BicycleStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

                if (bicycleStoreCurrentSeletion.name == "BicycleStoreToggle1")
                {
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    BicycleCustomShow(0, "BasicBicycle", 0);
                }
                else if (bicycleStoreCurrentSeletion.name == "BicycleStoreToggle2")
                {
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    BicycleCustomShow(1, "Bicycle1", 0);
                }
                else if (bicycleStoreCurrentSeletion.name == "BicycleStoreToggle3")
                {
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    BicycleCustomShow(2, "Bicycle2", 0);
                }
                else if (bicycleStoreCurrentSeletion.name == "BicycleStoreToggle4")
                {
                    womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    BicycleCustomShow(3, "Bicycle3", 0);
                }

                buyBtn.interactable = true; //구매버튼 활성화
            }
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            if (bicycleStoreToggleGroup.ActiveToggles().Any())
            {
                string intTamp = Regex.Replace(bicycleStoreCurrentSeletion.name, @"\D", "");    //숫자 추출
                PlayerPrefs.SetString("AT_BuyItemToggleName", bicycleStoreCurrentSeletion.name);  //클릭 토글 이름 저장
                PlayerPrefs.SetString("AT_BuyCompleteImageName", "BicycleStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

                if (bicycleStoreCurrentSeletion.name == "BicycleStoreToggle1")
                {
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    BicycleCustomShow(0, "BasicBicycle", 0);
                }
                else if (bicycleStoreCurrentSeletion.name == "BicycleStoreToggle2")
                {
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    BicycleCustomShow(1, "Bicycle1", 0);
                }
                else if (bicycleStoreCurrentSeletion.name == "BicycleStoreToggle3")
                {
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    BicycleCustomShow(2, "Bicycle2", 0);
                }
                else if (bicycleStoreCurrentSeletion.name == "BicycleStoreToggle4")
                {
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    BicycleCustomShow(3, "Bicycle3", 0);
                }

                buyBtn.interactable = true; //구매버튼 활성화
            }
        }
    }
    
    public void ExpChoiceClickOn()
    {
        if(expStoreToggleGroup.ActiveToggles().Any())
        {
            string intTamp = Regex.Replace(expStoreCurrentSeletion.name, @"\D", "");    //숫자 추출
            PlayerPrefs.SetString("AT_BuyItemToggleName", expStoreCurrentSeletion.name);  //클릭 토글 이름 저장
            PlayerPrefs.SetString("AT_BuyCompleteImageName", "ItemStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

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
            PlayerPrefs.SetString("AT_BuyItemToggleName", coinStoreCurrentSeletion.name);  //클릭 토글 이름 저장
            PlayerPrefs.SetString("AT_BuyCompleteImageName", "ItemStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

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
            PlayerPrefs.SetString("AT_BuyItemToggleName", speedStoreCurrentSeletion.name);  //클릭 토글 이름 저장
            PlayerPrefs.SetString("AT_BuyCompleteImageName", "ItemStoreImage" + intTamp);    //클릭 판매완료 이미지 활성화

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




    void HairCustomShow(int _index)
    {
        item_info_scrip = hairObj[_index].GetComponent<Item_Information>();

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            if (item_info_scrip.itemNum == 0)
                womanctrl_scrip.HairSetting(true, false, false);
            else if (item_info_scrip.itemNum == 1)
                womanctrl_scrip.HairSetting(false, true, false);
            else if (item_info_scrip.itemNum == 2)
                womanctrl_scrip.HairSetting(false, false, true);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            if (item_info_scrip.itemNum == 0)
                manctrl_scrip.HairSetting(true, false, false, false);
            else if (item_info_scrip.itemNum == 1)
                manctrl_scrip.HairSetting(false, true, false, false);
            else if (item_info_scrip.itemNum == 2)
                manctrl_scrip.HairSetting(false, false, true, false);
            else if (item_info_scrip.itemNum == 3)  //헬맷썻을 때 머리
                manctrl_scrip.HairSetting(false, false, false, true);
        }

        PlayerPrefs.SetInt("AT_StoreBuy_Price", item_info_scrip.price);
        PlayerPrefs.SetString("AT_StoreBuy_FolderName", item_info_scrip.folder);
        PlayerPrefs.SetString("AT_StoreBuy_ImageName", item_info_scrip.imgname);
        PlayerPrefs.SetString("AT_StoreBuyTitle", item_info_scrip.style);
    }

    void ShirtCustomShow(int _index, string _style, int _num)
    {
        item_info_scrip = shirtObj[_index].GetComponent<Item_Information>();

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            if (item_info_scrip.itemNum == 1)
                womanctrl_scrip.JacketSetting(false, true, false);
            else if (item_info_scrip.itemNum == 2)
                womanctrl_scrip.JacketSetting(false, false, true);

            womanctrl_scrip.JacketTextrueSetting(_style, _num);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            if (item_info_scrip.itemNum == 1)
                manctrl_scrip.JacketSetting(false, true, false);
            else if (item_info_scrip.itemNum == 2)
                manctrl_scrip.JacketSetting(false, false, true);

            manctrl_scrip.JacketTextrueSetting(_style, _num);
        }
        

        PlayerPrefs.SetInt("AT_StoreBuy_Price", item_info_scrip.price);
        PlayerPrefs.SetString("AT_StoreBuy_FolderName", item_info_scrip.folder);
        PlayerPrefs.SetString("AT_StoreBuy_ImageName", item_info_scrip.imgname);
        PlayerPrefs.SetString("AT_StoreBuyTitle", item_info_scrip.style);
    }
    
    void PantsCustomShow(int _index, string _style, int _num)
    {
        item_info_scrip = pantsObj[_index].GetComponent<Item_Information>();

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            if (item_info_scrip.itemNum == 0)
                womanctrl_scrip.PantsSetting(true, false);
            else if (item_info_scrip.itemNum == 1)
                womanctrl_scrip.PantsSetting(false, true);

            womanctrl_scrip.PantsTextrueSetting(_style, _num);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            if (item_info_scrip.itemNum == 0)
                manctrl_scrip.PantsSetting(true, false);
            else if (item_info_scrip.itemNum == 1)
                manctrl_scrip.PantsSetting(false, true);

            manctrl_scrip.PantsTextrueSetting(_style, _num);
        }


        PlayerPrefs.SetInt("AT_StoreBuy_Price", item_info_scrip.price);
        PlayerPrefs.SetString("AT_StoreBuy_FolderName", item_info_scrip.folder);
        PlayerPrefs.SetString("AT_StoreBuy_ImageName", item_info_scrip.imgname);
        PlayerPrefs.SetString("AT_StoreBuyTitle", item_info_scrip.style);
    }

    void ShoesCustomShow(int _index, string _style, int _textrueIndex)
    {
        item_info_scrip = shoesObj[_index].GetComponent<Item_Information>();

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            if (item_info_scrip.itemNum == 0)
                womanctrl_scrip.ShoesSetting(true, false);
            else if (item_info_scrip.itemNum == 1)
                womanctrl_scrip.ShoesSetting(false, true);

            womanctrl_scrip.ShoesTextrueSetting(_style, _textrueIndex);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            if (item_info_scrip.itemNum == 0)
                manctrl_scrip.ShoesSetting(true, false);
            else if (item_info_scrip.itemNum == 1)
                manctrl_scrip.ShoesSetting(false, true);

            //Debug.Log("_style " + _style + "_textrueIndex " + _textrueIndex);
            manctrl_scrip.ShoesTextrueSetting(_style, _textrueIndex);
        }

        PlayerPrefs.SetInt("AT_StoreBuy_Price", item_info_scrip.price);
        PlayerPrefs.SetString("AT_StoreBuy_FolderName", item_info_scrip.folder);
        PlayerPrefs.SetString("AT_StoreBuy_ImageName", item_info_scrip.imgname);
        PlayerPrefs.SetString("AT_StoreBuyTitle", item_info_scrip.style);
    }

    void HelmetCustomShow(int _index, string _style, int _num)
    {
        item_info_scrip = helmetObj[_index].GetComponent<Item_Information>();

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            if (item_info_scrip.itemNum == 0)
                womanctrl_scrip.HelmetSetting(true);

            womanctrl_scrip.HelmetTextrueSetting(_style, _num);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            if (item_info_scrip.itemNum == 0)
                manctrl_scrip.HelmetSetting(true);

            manctrl_scrip.HelmetTextrueSetting(_style, _num);
        }

        PlayerPrefs.SetInt("AT_StoreBuy_Price", item_info_scrip.price);
        PlayerPrefs.SetString("AT_StoreBuy_FolderName", item_info_scrip.folder);
        PlayerPrefs.SetString("AT_StoreBuy_ImageName", item_info_scrip.imgname);
        PlayerPrefs.SetString("AT_StoreBuyTitle", item_info_scrip.style);
    }

    void GlovesCustomShow(int _index, string _style, int _num)
    {
        item_info_scrip = glovesObj[_index].GetComponent<Item_Information>();

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            if (item_info_scrip.itemNum == 0)
                womanctrl_scrip.GlovesSetting(true);

            womanctrl_scrip.GlovesTextrueSetting(_style, _num);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            if (item_info_scrip.itemNum == 0)
                manctrl_scrip.GlovesSetting(true);

            manctrl_scrip.GlovesTextrueSetting(_style, _num);
        }
        PlayerPrefs.SetInt("AT_StoreBuy_Price", item_info_scrip.price);
        PlayerPrefs.SetString("AT_StoreBuy_FolderName", item_info_scrip.folder);
        PlayerPrefs.SetString("AT_StoreBuy_ImageName", item_info_scrip.imgname);
        PlayerPrefs.SetString("AT_StoreBuyTitle", item_info_scrip.style);
    }

    void BicycleCustomShow(int _index, string _style, int _num)
    {
        item_info_scrip = bicycleObj[_index].GetComponent<Item_Information>();

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            womanctrl_scrip.BicycleTextrueSetting(_style, _num);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            manctrl_scrip.BicycleTextrueSetting(_style, _num);
        }
        PlayerPrefs.SetInt("AT_StoreBuy_Price", item_info_scrip.price);
        PlayerPrefs.SetString("AT_StoreBuy_FolderName", item_info_scrip.folder);
        PlayerPrefs.SetString("AT_StoreBuy_ImageName", item_info_scrip.imgname);
        PlayerPrefs.SetString("AT_StoreBuyTitle", item_info_scrip.style);
    }

    void ExpCustomShowBuy(int _index)
    {
        item_info_scrip = expObj[_index].GetComponent<Item_Information>();

        PlayerPrefs.SetInt("AT_StoreBuy_Price", item_info_scrip.price);
        PlayerPrefs.SetString("AT_StoreBuy_FolderName", item_info_scrip.folder);
        PlayerPrefs.SetString("AT_StoreBuy_ImageName", item_info_scrip.imgname);
        PlayerPrefs.SetString("AT_StoreBuyTitle", item_info_scrip.style);
    }

    void CoinCustomShowBuy(int _index)
    {
        item_info_scrip = coinObj[_index].GetComponent<Item_Information>();

        PlayerPrefs.SetInt("AT_StoreBuy_Price", item_info_scrip.price);
        PlayerPrefs.SetString("AT_StoreBuy_FolderName", item_info_scrip.folder);
        PlayerPrefs.SetString("AT_StoreBuy_ImageName", item_info_scrip.imgname);
        PlayerPrefs.SetString("AT_StoreBuyTitle", item_info_scrip.style);
    }

    void SpeedCustomShowBuy(int _index)
    {
        item_info_scrip = speedObj[_index].GetComponent<Item_Information>();

        PlayerPrefs.SetInt("AT_StoreBuy_Price", item_info_scrip.price);
        PlayerPrefs.SetString("AT_StoreBuy_FolderName", item_info_scrip.folder);
        PlayerPrefs.SetString("AT_StoreBuy_ImageName", item_info_scrip.imgname);
        PlayerPrefs.SetString("AT_StoreBuyTitle", item_info_scrip.style);
    }

    //구매한 상품 계산하는 함수(구매 팝업에서)
    public void BuyItem_Interactable(string _itemName)
    {
        //Debug.Log("구매 이름 : " + PlayerPrefs.GetString("AT_StoreBuy_ImageName"));
        GameObject test = GameObject.Find(PlayerPrefs.GetString("AT_BuyItemToggleName"));  //어떤 오브젝트 클릭한지 각 토글이름
        item_info_scrip = test.GetComponent<Item_Information>();
        ItemDataBase.instance.items[item_info_scrip.indexNum].buyState = true; 
        

        //Debug.Log(PlayerPrefs.GetString("AT_BuyCompleteImageName"));
        GameObject image = test.transform.Find(PlayerPrefs.GetString("AT_BuyCompleteImageName")).gameObject;

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

        WearItem(PlayerPrefs.GetString("AT_BuyItemToggleName"));
        WearItemStyleSave(PlayerPrefs.GetString("AT_StoreBuy_FolderName"), PlayerPrefs.GetString("AT_StoreBuy_ImageName"));
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
            PlayerPrefs.SetInt("AT_HairNumber", id_hairNum);
            PlayerPrefs.SetString("AT_Player_Hair", hairStyleStr);
            CustomChange(); //커스텀했는지 여부    
        } 
        else if(_buyItemToggleName == "ShirtStoreToggle7" || _buyItemToggleName == "ShirtStoreToggle2" || _buyItemToggleName == "ShirtStoreToggle3" ||
            _buyItemToggleName == "ShirtStoreToggle4" || _buyItemToggleName == "ShirtStoreToggle5" || _buyItemToggleName == "ShirtStoreToggle6")
        {
            PlayerPrefs.SetInt("AT_JacketNumber", id_jacketNum);
            if (id_jacketNum == 1)
                PlayerPrefs.SetString("AT_Wear_JacketKind", "ItemID_Tshirt");
            else if(id_jacketNum == 2)
                PlayerPrefs.SetString("AT_Wear_JacketKind", "ItemID_LongShirt");

            CustomChange(); //커스텀했는지 여부    
        } 
        else if (_buyItemToggleName == "PantsStoreToggle7" || _buyItemToggleName == "PantsStoreToggle2" || _buyItemToggleName == "PantsStoreToggle3" ||
            _buyItemToggleName == "PantsStoreToggle4" || _buyItemToggleName == "PantsStoreToggle5" || _buyItemToggleName == "PantsStoreToggle6")
        {
            PlayerPrefs.SetInt("AT_PantsNumber", id_pantsNum);
            if (id_pantsNum == 0)
                PlayerPrefs.SetString("AT_Wear_PantsKind", "ItemID_Short");
            else if (id_pantsNum == 1)
                PlayerPrefs.SetString("AT_Wear_PantsKind", "ItemID_LongPants");

            CustomChange(); //커스텀했는지 여부    
        } 
        else if (_buyItemToggleName == "ShoesStoreToggle2" || _buyItemToggleName == "ShoesStoreToggle3" || _buyItemToggleName == "ShoesStoreToggle4")
        {
            //Debug.Log("AT_ShoesNumber " + PlayerPrefs.GetInt("AT_ShoesNumber"));
            PlayerPrefs.SetInt("AT_ShoesNumber", id_shoesNum);
            //Debug.Log("22AT_ShoesNumber " + PlayerPrefs.GetInt("AT_ShoesNumber"));
            if (id_shoesNum == 1)
                PlayerPrefs.SetString("AT_Wear_ShoesKind", "ItemID_Shoes");

            CustomChange(); //커스텀했는지 여부    
        }
        else if (_buyItemToggleName == "HelmetStoreToggle1" || _buyItemToggleName == "HelmetStoreToggle2" || _buyItemToggleName == "HelmetStoreToggle3")
        {
            PlayerPrefs.SetInt("AT_HelmetNumber", id_helmetNum);   
            PlayerPrefs.SetString("AT_Wear_HelmetKind", "ItemID_Helmet");  //헬맷 착용

            CustomChange(); //커스텀했는지 여부    
        }
        else if (_buyItemToggleName == "GlovesStoreToggle1" || _buyItemToggleName == "GlovesStoreToggle2" || _buyItemToggleName == "GlovesStoreToggle3")
        {
            PlayerPrefs.SetInt("AT_GlovesNumber", id_glovesNum);
            PlayerPrefs.SetString("AT_Wear_GlovesKind", "ItemID_Gloves");  //헬맷 착용

            CustomChange(); //커스텀했는지 여부    
        }
        else if(_buyItemToggleName.Equals("BicycleStoreToggle1") || _buyItemToggleName.Equals("BicycleStoreToggle2") || _buyItemToggleName.Equals("BicycleStoreToggle3") ||
            _buyItemToggleName.Equals("BicycleStoreToggle4"))
        {
            PlayerPrefs.SetInt("AT_BicycleNumber", id_bicycleNum);
            PlayerPrefs.SetString("AT_Wear_BicycleKind", "ItemID_Bicycle");  //자전거 기본 -텍스쳐만변경
            CustomChange(); //커스텀했는지 여부    
        }
    }

    void CustomChange()
    {
        //Debug.Log("내가삿다 CustomChange");

        if (PlayerPrefs.GetString("AT_CustomChange").Equals("MissionOk"))
            PlayerPrefs.SetString("AT_CustomChange", "MissionOk");
        else
            PlayerPrefs.SetString("AT_CustomChange", "Yes");

       // Debug.Log("----------- " + PlayerPrefs.GetString("AT_CustomChange"));
    }

    public void WearItemStyleSave(string _folder, string _imgName)
    {
        if(_folder == "Jacket")
        {
            PlayerPrefs.SetString("AT_Wear_JacketStyleName", _imgName);
        }
        else if (_folder == "Pants")
        {
            PlayerPrefs.SetString("AT_Wear_PantsStyleName", _imgName);
        }
        else if (_folder == "Shoes")
        {
            PlayerPrefs.SetString("AT_Wear_ShoesStyleName", _imgName);
        }
        else if (_folder == "Helmet")
        {
            PlayerPrefs.SetString("AT_Wear_HelmetStyleName", _imgName);
        }
        else if (_folder == "Gloves")
        {
            PlayerPrefs.SetString("AT_Wear_GlovesStyleName", _imgName);
        }
        else if(_folder.Equals("Bicycle"))
        {
            PlayerPrefs.SetString("AT_Wear_BicycleStyleName", _imgName);
        }
    }
    
   
    public void InitData_ButtonOn()
    {
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            if (init_shoesNum == 0)
                womanctrl_scrip.Animator_Initialization(init_hairNum, init_bodyNum, init_jacketNum, init_pantsNum, init_shoesNum);
            else if (init_shoesNum == 2)
                womanctrl_scrip.Animator_Initialization(init_hairNum, init_bodyNum, init_jacketNum, init_pantsNum, init_shoesNum-1);

            //머리 초기화
            if (init_hairNum == 0)
                womanctrl_scrip.HairSetting(true, false, false);
            else if (init_hairNum == 1)
                womanctrl_scrip.HairSetting(false, true, false);
            else if (init_hairNum == 2)
                womanctrl_scrip.HairSetting(false, false, true);

            //상의 초기화
            if (init_jacketNum == 0)
                womanctrl_scrip.JacketSetting(true, false, false);
            else if (init_jacketNum == 1)
                womanctrl_scrip.JacketSetting(false, true, false);
            else if (init_jacketNum == 2)
                womanctrl_scrip.JacketSetting(false, false, true);

            womanctrl_scrip.JacketTextrueSetting(init_jacket_s, init_jacketNum);

            //하의 초기화
            if (init_pantsNum == 0)
                womanctrl_scrip.PantsSetting(true, false);
            else if (init_pantsNum == 1)
                womanctrl_scrip.PantsSetting(false, true);

            womanctrl_scrip.PantsTextrueSetting(init_pants_s, init_pantsNum);

            //신발 초기화
            if (init_shoesNum == 0)
                womanctrl_scrip.ShoesSetting(true, false);
            else if (init_shoesNum == 2)
                womanctrl_scrip.ShoesSetting(false, true);

            womanctrl_scrip.ShoesTextrueSetting(init_shoes_s, init_shoesNum);

            //헬맷 초기화
            if (init_helmetNum == 0)
            {
                womanctrl_scrip.HelmetSetting(true);
                womanctrl_scrip.HelmetTextrueSetting(init_helmet_s, init_helmetNum);
            }
            else if(init_helmetNum == 100)
                womanctrl_scrip.HelmetSetting(false);

            //장갑 초기화
            if (init_glovesNum == 0)
            {
                womanctrl_scrip.GlovesSetting(true);
                womanctrl_scrip.GlovesTextrueSetting(init_gloves_s, init_glovesNum);
            }
            else if(init_glovesNum == 100)
                womanctrl_scrip.GlovesSetting(false);

            womanctrl_scrip.BicycleTextrueSetting(init_bicycle_s, init_bicycleNum);
        }
        else if(PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            if (init_shoesNum == 0)
                manctrl_scrip.Animator_Initialization(init_hairNum, init_bodyNum, init_jacketNum, init_pantsNum, init_shoesNum);
            else if (init_shoesNum == 2)
                manctrl_scrip.Animator_Initialization(init_hairNum, init_bodyNum, init_jacketNum, init_pantsNum, init_shoesNum-1);

            //머리 초기화
            if (init_hairNum == 0)
                manctrl_scrip.HairSetting(true, false, false, false);
            else if (init_hairNum == 1)
                manctrl_scrip.HairSetting(false, true, false, false);
            else if (init_hairNum == 2)
                manctrl_scrip.HairSetting(false, false, true, false);
            else if (init_hairNum == 3)
                manctrl_scrip.HairSetting(false, false, false, true);

            //상의 초기화
            if (init_jacketNum == 0)
                manctrl_scrip.JacketSetting(true, false, false);
            else if (init_jacketNum == 1)
                manctrl_scrip.JacketSetting(false, true, false);
            else if (init_jacketNum == 2)
                manctrl_scrip.JacketSetting(false, false, true);

            manctrl_scrip.JacketTextrueSetting(init_jacket_s, init_jacketNum);

            //하의 초기화
            if (init_pantsNum == 0)
                manctrl_scrip.PantsSetting(true, false);
            else if (init_pantsNum == 1)
                manctrl_scrip.PantsSetting(false, true);

            manctrl_scrip.PantsTextrueSetting(init_pants_s, init_pantsNum);

            //신발 초기화
            if (init_shoesNum == 0)
                manctrl_scrip.ShoesSetting(true, false);
            else if (init_shoesNum == 2)
                manctrl_scrip.ShoesSetting(false, true);

            manctrl_scrip.ShoesTextrueSetting(init_shoes_s, init_shoesNum);

            //헬맷 초기화
            if (init_helmetNum == 0)
            {
                manctrl_scrip.HelmetSetting(true);
                manctrl_scrip.HelmetTextrueSetting(init_helmet_s, init_helmetNum);
            }
            else if (init_helmetNum == 100)
                manctrl_scrip.HelmetSetting(false);

            //장갑 초기화
            if (init_glovesNum == 0)
            {
                manctrl_scrip.GlovesSetting(true);
                manctrl_scrip.GlovesTextrueSetting(init_gloves_s, init_glovesNum);
            }
            else if (init_glovesNum == 100)
                manctrl_scrip.GlovesSetting(false);

            manctrl_scrip.BicycleTextrueSetting(init_bicycle_s, init_bicycleNum);
        }

        id_hairNum = init_hairNum;
        id_bodyNum = init_bodyNum;
        id_jacketNum = init_jacketNum;
        id_pantsNum = init_pantsNum;
        id_shoesNum = init_shoesNum;
        id_helmetNum = init_helmetNum;
        id_glovesNum = init_glovesNum;
        id_bicycleNum = init_bicycleNum;
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
