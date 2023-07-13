using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;



public class MyInventory_DataManager : MonoBehaviour
{
    public static MyInventory_DataManager instance { get; private set; }

    public GameObject gameEndPopup; //게임종료팝업

    public ToggleGroup clothesAllToggleGroup;
    public ToggleGroup hairContentToggleGroup;
    public ToggleGroup shirtContentToggleGroup;
    public ToggleGroup pantsContentToggleGroup;
    public ToggleGroup shoesContentToggleGroup;
    public ToggleGroup helmetContentToggleGroup;
    public ToggleGroup glovesContentToggleGroup;

    public ToggleGroup equipmentAllToggleGroup;
    public ToggleGroup bikeContentToggleGroup;

    public ToggleGroup itemAllToggleGroup;
    public ToggleGroup expContentToggleGroup;
    public ToggleGroup coinContentToggleGroup;
    public ToggleGroup speedContentToggleGroup;

    public GameObject[] allClothesContents;    //전체 토글
    public GameObject[] hairContents;   // 각각의 토글들
    public GameObject[] shirtContents;
    public GameObject[] pantsContents;
    public GameObject[] shoesContents;
    public GameObject[] helmetContents;
    public GameObject[] glovesContents;
    public GameObject[] allEquipmentContents;   //장비전체 토글
    public GameObject[] bikeContents;   //자전거
    public GameObject[] allItemContents;
    public GameObject[] expContents;
    public GameObject[] coinContents;
    public GameObject[] speedContents;

    InventoryContentCtrl invenCtrl_scrip;

    

    GameObject womanPlayer;
    WomanCtrl womanctrl_scrip;
    GameObject manPlayer;
    ManCtrl manctrl_scrip;

    //스타일-텍스쳐
    string id_JacketStr, id_PantsStr, id_ShoesStr, id_hairStr, id_glovesStr, id_helmetStr, id_bicycleStr;
    //장착 아이디
    string wear_HairKind, wear_GlovesKind, wear_HelmetKind, wear_Jacketkind, wear_PantsKind, wear_ShoesKind, wear_BicycleKind;
    string hairStyleStr;    //머리 스타일

    //각 아이템 배열에 저장된 번호
    int id_jacketNum, id_pantsNum, id_shoesNum, id_hairNum, id_bodyNum, id_helmetNum, id_glovesNum, id_bicycleNum;
    //구매한 데이터를 하나하나 넣기위해 배열 인덱스값(판넬에 저장 저장을 위한 인덱스)
    int jacketCount_i = 0, pantsCount_i = 0, shoesCount_i = 0, hairCount_i = 0, glovesCount_i = 0, helmetCount_i = 0;

    //각 활성화된 토글이 몇개 있는지 확인하는 변수
    int hairCount, jacketCount, pantsCount, shoesCount, helmetCount, glovesCount;

    public Toggle clothesAllCurrentSeletion
    {
        get { return clothesAllToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle hairContentCurrentSeletion
    {
        get { return hairContentToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle shirtContentCurrentSeletion
    {
        get { return shirtContentToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle pantsContentCurrentSeletion
    {
        get { return pantsContentToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle shoesContentCurrentSeletion
    {
        get { return shoesContentToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle helmetContentCurrentSeletion
    {
        get { return helmetContentToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle glovesContentCurrentSeletion
    {
        get { return glovesContentToggleGroup.ActiveToggles().FirstOrDefault(); }
    }
    public Toggle equipmentAllCureentSeletion
    {
        get { return equipmentAllToggleGroup.ActiveToggles().FirstOrDefault(); }
    }
    public Toggle bikeCureentSeletion
    {
        get { return bikeContentToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle itemAllCurrentSeletion
    {
        get { return itemAllToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle expCurrentSeletion
    {
        get { return expContentToggleGroup.ActiveToggles().FirstOrDefault(); }
    }
    
    public Toggle coinCurrentSeletion
    {
        get { return coinContentToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle speedCurrentSeletion
    {
        get { return speedContentToggleGroup.ActiveToggles().FirstOrDefault(); }
    }





    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }


    void Start()
    {
        //Debug.Log("왜 ???" + PlayerPrefs.GetString("AT_Player_Sex"));
        Initialization();
        AllBuyItemList(0, 27, allClothesContents, 0);  //옷전체    
        EachBuyItemList(hairContents, 0, "Hair");   //남자머리가 4개라서 
        EachBuyItemList(shirtContents, 0, "Jacket");
        EachBuyItemList(pantsContents, 0, "Pants");
        EachBuyItemList(shoesContents, 0, "Shoes");
        EachBuyItemList(helmetContents, 0, "Helmet");
        EachBuyItemList(glovesContents, 0, "Gloves");

        AllBuyItemList(28, 31, allEquipmentContents, 0);    //장비전체
        EachBuyItemList(bikeContents, 0, "Bicycle");

        AllBuyItemList(32, 35, allItemContents, 0); //아이템 전체
        EachBuyItemList(expContents, 0, "Exp");
        EachBuyItemList(coinContents, 0, "Coin");
        EachBuyItemList(speedContents, 0, "Speed");
    }

    //숫자 콤마 찍는 함수
    public string CommaText(int _data)
    {
        if (_data != 0)
            return string.Format("{0:#,###}", _data);
        else
            return "0";
    }


    void AllBuyItemList(int _min, int _max, GameObject[] _content, int _plusIndex)
    {
        //for(int i = _min; i <= _max; i++)
        //{
        //    if (ItemDataBase.instance.items[i].buyState == true) //구매한 목록 들고오기
        //    {
        //        if (invenCtrl_scrip.folder == "Exp")
        //        {
        //            if (invenCtrl_scrip.imgname == "ExpPlus")
        //            {
        //                if (PlayerPrefs.GetInt("ExpPlusAmount") == 0)
        //                    ItemDataBase.instance.items[i].buyState = false;    //구매하지 않은걸로 변경
        //            }
        //            else if (invenCtrl_scrip.imgname == "ExpUp")
        //            {
        //                if (PlayerPrefs.GetInt("ExpUpAmount") == 0)
        //                    ItemDataBase.instance.items[i].buyState = false;    //구매하지 않은걸로 변경
        //            }
        //        }
        //        else if (invenCtrl_scrip.folder == "Coin")
        //        {
        //            if (PlayerPrefs.GetInt("CoinUpAmount") == 0)
        //            {
        //                ItemDataBase.instance.items[i].buyState = false;    //구매하지 않은걸로 변경
        //            }
        //        }
        //        else if (invenCtrl_scrip.folder == "Speed")
        //        {
        //            if (PlayerPrefs.GetInt("SpeedUpAmount") == 0)
        //            {
        //                ItemDataBase.instance.items[i].buyState = false;    //구매하지 않은걸로 변경
        //            }
        //        }
                
        //    }
        //}

        //구매한 상품을 전체 진열
        for(int i = _min; i <= _max; i++)
        {
            if(ItemDataBase.instance.items[i].buyState == true) //구매한 목록 들고오기
            {
                int index = _plusIndex++;
                invenCtrl_scrip = _content[index].GetComponent<InventoryContentCtrl>();
                invenCtrl_scrip.price = ItemDataBase.instance.items[i].price;
                invenCtrl_scrip.id = ItemDataBase.instance.items[i].itemId;
                invenCtrl_scrip.style = ItemDataBase.instance.items[i].itemStyle;
                invenCtrl_scrip.itemNum = ItemDataBase.instance.items[i].itemNum;
                invenCtrl_scrip.folder = ItemDataBase.instance.items[i].folderName;
                invenCtrl_scrip.imgname = ItemDataBase.instance.items[i].imgName;
                invenCtrl_scrip.buyState = ItemDataBase.instance.items[i].buyState;
                invenCtrl_scrip.indexNum = ItemDataBase.instance.items[i].listIndex;

                if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
                {
                    RectTransform rectTran = invenCtrl_scrip.img.GetComponent<RectTransform>();

                    if (invenCtrl_scrip.folder == "Hair")
                    {
                        _content[index].transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = invenCtrl_scrip.style;
                        rectTran.sizeDelta = new Vector2(199, 237);
                        invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/" + invenCtrl_scrip.folder + "/Woman/" + invenCtrl_scrip.imgname);
                    }
                    else if (invenCtrl_scrip.folder == "Jacket")
                    {
                        if (invenCtrl_scrip.imgname == "BasicNasi")
                        {
                            _content[index].transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = invenCtrl_scrip.style;
                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/" + invenCtrl_scrip.folder + "/Woman/" + invenCtrl_scrip.imgname);
                        }
                        else
                        {
                            _content[index].transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = invenCtrl_scrip.style;
                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                        }
                    }
                    else
                    {
                        _content[index].transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = invenCtrl_scrip.style;

                        if (invenCtrl_scrip.folder == "Exp")
                        {
                            //if(invenCtrl_scrip.imgname == "ExpPlus")
                            //    _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("AT_ExpPlusAmount").ToString();
                            if(invenCtrl_scrip.imgname == "ExpUp")
                                _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("AT_ExpUpAmount").ToString();

                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                        }
                        else if (invenCtrl_scrip.folder == "Coin")
                        {
                            _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("AT_CoinUpAmount").ToString();
                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                        }
                        else if (invenCtrl_scrip.folder == "Speed")
                        {
                            _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("AT_SpeedUpAmount").ToString();
                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                        }
                        else
                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                    }
                        
                }
                else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
                {
                    RectTransform rectTran = invenCtrl_scrip.img.GetComponent<RectTransform>();

                    if (invenCtrl_scrip.folder == "Hair")
                    {
                        _content[index].transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = invenCtrl_scrip.style;
                        rectTran.sizeDelta = new Vector2(294, 347);
                        invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/" + invenCtrl_scrip.folder + "/Man/" + invenCtrl_scrip.imgname);
                    }
                    else if (invenCtrl_scrip.folder == "Jacket")
                    {
                        if (invenCtrl_scrip.imgname == "BasicNasi")
                        {
                            _content[index].transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = invenCtrl_scrip.style;
                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/" + invenCtrl_scrip.folder + "/Man/" + invenCtrl_scrip.imgname);
                        }
                        else
                        {
                            _content[index].transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = invenCtrl_scrip.style;
                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                        }
                    } 
                    else
                    {
                        _content[index].transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = invenCtrl_scrip.style;
                        if (invenCtrl_scrip.folder == "Exp")
                        {
                            //if (invenCtrl_scrip.imgname == "ExpPlus")
                            //    _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("AT_ExpPlusAmount").ToString();
                            if (invenCtrl_scrip.imgname == "ExpUp")
                                _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("AT_ExpUpAmount").ToString();

                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                        }
                        else if (invenCtrl_scrip.folder == "Coin")
                        {
                            _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("AT_CoinUpAmount").ToString();
                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                        }
                        else if (invenCtrl_scrip.folder == "Speed")
                        {
                            _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("AT_SpeedUpAmount").ToString();
                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                        }
                        else
                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                    } 
                }
            }
        }


        for(int i = _plusIndex; i <= 27; i++)
        {
            _content[i].GetComponent<Toggle>().interactable = false; //토글 클릭 금지
            _content[i].transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);    //이미지 비활성화
        }
    }

    void EachBuyItemList(GameObject[] _content, int _plusIndex, string _folderName)
    {
        for(int i = 0; i <= 35; i++)
        {
            //아이템 리스트 중에 해당 폴더에 있는 아이템
            if(ItemDataBase.instance.items[i].folderName == _folderName)
            {
                //그중에 내가 구매한 아이템
                if(ItemDataBase.instance.items[i].buyState == true)
                {
                    int index = _plusIndex++;
                    invenCtrl_scrip = _content[index].GetComponent<InventoryContentCtrl>();
                    invenCtrl_scrip.price = ItemDataBase.instance.items[i].price;
                    invenCtrl_scrip.id = ItemDataBase.instance.items[i].itemId;
                    invenCtrl_scrip.style = ItemDataBase.instance.items[i].itemStyle;
                    invenCtrl_scrip.itemNum = ItemDataBase.instance.items[i].itemNum;
                    invenCtrl_scrip.folder = ItemDataBase.instance.items[i].folderName;
                    invenCtrl_scrip.imgname = ItemDataBase.instance.items[i].imgName;
                    //invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                    invenCtrl_scrip.buyState = ItemDataBase.instance.items[i].buyState;
                    invenCtrl_scrip.indexNum = ItemDataBase.instance.items[i].listIndex;

                    if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
                    {
                        RectTransform rectTran = invenCtrl_scrip.img.GetComponent<RectTransform>();

                        if (invenCtrl_scrip.folder == "Hair")
                        {
                            _content[index].transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = invenCtrl_scrip.style;
                            rectTran.sizeDelta = new Vector2(199, 237);
                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/" + invenCtrl_scrip.folder + "/Woman/" + invenCtrl_scrip.imgname);
                            hairCount = index;
                        }
                        else if (invenCtrl_scrip.folder == "Jacket")
                        {
                            if (invenCtrl_scrip.imgname == "BasicNasi")
                            {
                                _content[index].transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = invenCtrl_scrip.style;
                                invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/" + invenCtrl_scrip.folder + "/Woman/" + invenCtrl_scrip.imgname);
                                jacketCount = index;
                            }
                            else
                            {
                                _content[index].transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = invenCtrl_scrip.style;
                                invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                                jacketCount = index;
                            } 
                        }
                        else
                        {
                            _content[index].transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = invenCtrl_scrip.style;
                            if (invenCtrl_scrip.folder == "Exp")
                            {
                                //if (invenCtrl_scrip.imgname == "ExpPlus")
                                //    _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("AT_ExpPlusAmount").ToString();
                               if (invenCtrl_scrip.imgname == "ExpUp")
                                    _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("AT_ExpUpAmount").ToString();

                                invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                            }
                            else if (invenCtrl_scrip.folder == "Coin")
                            {
                                _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("AT_CoinUpAmount").ToString();
                                invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                            }
                            else if (invenCtrl_scrip.folder == "Speed")
                            {
                                _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("AT_SpeedUpAmount").ToString();
                                invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                            }
                            else
                                invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);

                            if (invenCtrl_scrip.folder == "Pants")
                                pantsCount = index;
                            else if (invenCtrl_scrip.folder == "Shoes")
                                shoesCount = index;
                            else if (invenCtrl_scrip.folder == "Helmet")
                                helmetCount = index;
                            else if (invenCtrl_scrip.folder == "Gloves")
                                glovesCount = index;
                        }
                    }
                    else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
                    {
                        RectTransform rectTran = invenCtrl_scrip.img.GetComponent<RectTransform>();

                        if (invenCtrl_scrip.folder == "Hair")
                        {
                            _content[index].transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = invenCtrl_scrip.style;
                            rectTran.sizeDelta = new Vector2(294, 347);
                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/" + invenCtrl_scrip.folder + "/Man/" + invenCtrl_scrip.imgname);
                            hairCount = index;
                        }
                        else if (invenCtrl_scrip.folder == "Jacket")
                        {
                            if (invenCtrl_scrip.imgname == "BasicNasi")
                            {
                                _content[index].transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = invenCtrl_scrip.style;
                                invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/" + invenCtrl_scrip.folder + "/Man/" + invenCtrl_scrip.imgname);
                                jacketCount = index;
                            }
                            else
                            {
                                _content[index].transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = invenCtrl_scrip.style;
                                invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                                jacketCount = index;
                            }
                        }
                        else
                        {
                            _content[index].transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = invenCtrl_scrip.style;
                            if (invenCtrl_scrip.folder == "Exp")
                            {
                                //if (invenCtrl_scrip.imgname == "ExpPlus")
                                //    _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("AT_ExpPlusAmount").ToString();
                                if (invenCtrl_scrip.imgname == "ExpUp")
                                    _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("AT_ExpUpAmount").ToString();

                                invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                            }
                            else if (invenCtrl_scrip.folder == "Coin")
                            {
                                _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("AT_CoinUpAmount").ToString();
                                invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                            }
                            else if (invenCtrl_scrip.folder == "Speed")
                            {
                                //Debug.Log("SpeedUpAmount " + PlayerPrefs.GetInt("AT_SpeedUpAmount"));
                                _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("AT_SpeedUpAmount").ToString();
                                invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                            }
                            else
                                invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);

                            if (invenCtrl_scrip.folder == "Pants")
                                pantsCount = index;
                            else if (invenCtrl_scrip.folder == "Shoes")
                                shoesCount = index;
                            else if (invenCtrl_scrip.folder == "Helmet")
                                helmetCount = index;
                            else if (invenCtrl_scrip.folder == "Gloves")
                                glovesCount = index;
                        }
                    }
                }
            }
        }

        for(int i = _plusIndex; i <= 11; i++)
        {
            _content[i].GetComponent<Toggle>().interactable = false; //토글 클릭 금지
            _content[i].transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);    //이미지 비활성화
        }
    }


    void Initialization()
    {
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
        id_helmetNum = PlayerPrefs.GetInt("AT_HelmetNumber");
        id_glovesNum = PlayerPrefs.GetInt("AT_GlovesNumber");
        id_bicycleNum = PlayerPrefs.GetInt("AT_BicycleNumber");

        //각 아이템 텍스쳐 저장
        id_JacketStr = PlayerPrefs.GetString("AT_Wear_JacketStyleName");
        id_PantsStr = PlayerPrefs.GetString("AT_Wear_PantsStyleName");
        id_ShoesStr = PlayerPrefs.GetString("AT_Wear_ShoesStyleName");
        id_helmetStr = PlayerPrefs.GetString("AT_Wear_HelmetStyleName");
        id_glovesStr = PlayerPrefs.GetString("AT_Wear_GlovesStyleName");
        id_hairStr = PlayerPrefs.GetString("AT_Wear_HairStyleName");
        id_bicycleStr = PlayerPrefs.GetString("AT_Wear_BicycleStyleName");

        //각 아이템 장착 아이디 저장
        hairStyleStr = PlayerPrefs.GetString("AT_Player_Hair");
        wear_Jacketkind = PlayerPrefs.GetString("AT_Wear_JacktKind");
        wear_PantsKind = PlayerPrefs.GetString("AT_Wear_PantsKind");
        wear_ShoesKind = PlayerPrefs.GetString("AT_Wear_ShoesKind");
        wear_HelmetKind = PlayerPrefs.GetString("AT_Wear_HelmetKind");
        wear_GlovesKind = PlayerPrefs.GetString("AT_Wear_GlovesKind");
        wear_BicycleKind = PlayerPrefs.GetString("AT_Wear_BicycleKind");

        //Debug.Log("wear_HelmetKind " + wear_HelmetKind + " wear_GlovesKind " + wear_GlovesKind);
    }

    
    void ItmeInit(int _num, GameObject[] _obj, string[] _str)
    {

    }

    //서버에서 각 아이템이 몇개 있는지 분류해서 들고와야함.
    //void ItemNumberGet()
    //{
    //    for(int i = 0; i < serverItemFullNum; i++)
    //    {
    //        if("서버에 있는 ID" == "ID_Hair")
    //        {
    //            hairNum_i++;    //카운터해줌.. 요러식으로
    //        }
    //    }
    //}

    public void ClothesAllToggleClick()
    {
        if (clothesAllToggleGroup.ActiveToggles().Any())
        {
            if (clothesAllCurrentSeletion.name == "AllToggle1")
                AllClothesActionShow(1);
            else if (clothesAllCurrentSeletion.name == "AllToggle2")
                AllClothesActionShow(2);
            else if (clothesAllCurrentSeletion.name == "AllToggle3")
                AllClothesActionShow(3);
            else if (clothesAllCurrentSeletion.name == "AllToggle4")
                AllClothesActionShow(4);
            else if (clothesAllCurrentSeletion.name == "AllToggle5")
                AllClothesActionShow(5);
            else if (clothesAllCurrentSeletion.name == "AllToggle6")
                AllClothesActionShow(6);
            else if (clothesAllCurrentSeletion.name == "AllToggle7")
                AllClothesActionShow(7);
            else if (clothesAllCurrentSeletion.name == "AllToggle8")
                AllClothesActionShow(8);
            else if (clothesAllCurrentSeletion.name == "AllToggle9")
                AllClothesActionShow(9);
            else if (clothesAllCurrentSeletion.name == "AllToggle10")
                AllClothesActionShow(10);
            else if (clothesAllCurrentSeletion.name == "AllToggle11")
                AllClothesActionShow(11);
            else if (clothesAllCurrentSeletion.name == "AllToggle12")
                AllClothesActionShow(12);
            else if (clothesAllCurrentSeletion.name == "AllToggle13")
                AllClothesActionShow(13);
            else if (clothesAllCurrentSeletion.name == "AllToggle14")
                AllClothesActionShow(14);
            else if (clothesAllCurrentSeletion.name == "AllToggle15")
                AllClothesActionShow(15);
            else if (clothesAllCurrentSeletion.name == "AllToggle16")
                AllClothesActionShow(16);
            else if (clothesAllCurrentSeletion.name == "AllToggle17")
                AllClothesActionShow(17);
            else if (clothesAllCurrentSeletion.name == "AllToggle18")
                AllClothesActionShow(18);
            else if (clothesAllCurrentSeletion.name == "AllToggle19")
                AllClothesActionShow(19);
            else if (clothesAllCurrentSeletion.name == "AllToggle20")
                AllClothesActionShow(20);
            else if (clothesAllCurrentSeletion.name == "AllToggle21")
                AllClothesActionShow(21);
            else if (clothesAllCurrentSeletion.name == "AllToggle22")
                AllClothesActionShow(22);
            else if (clothesAllCurrentSeletion.name == "AllToggle23")
                AllClothesActionShow(23);
            else if (clothesAllCurrentSeletion.name == "AllToggle24")
                AllClothesActionShow(24);
            else if (clothesAllCurrentSeletion.name == "AllToggle25")
                AllClothesActionShow(25);
            else if (clothesAllCurrentSeletion.name == "AllToggle26")
                AllClothesActionShow(26);
            else if (clothesAllCurrentSeletion.name == "AllToggle27")
                AllClothesActionShow(27);
            else if (clothesAllCurrentSeletion.name == "AllToggle28")
                AllClothesActionShow(28);
        }
    }


    public void HairInvenContentToggleClick()
    {
        if (hairContentToggleGroup.ActiveToggles().Any())
        {
            if (hairContentCurrentSeletion.name == "HairInvenToggle1")
                HairActionShow(1);
            else if (hairContentCurrentSeletion.name == "HairInvenToggle2")
                HairActionShow(2);
            else if (hairContentCurrentSeletion.name == "HairInvenToggle3")
                HairActionShow(3);
            else if (hairContentCurrentSeletion.name == "HairInvenToggle4")
                HairActionShow(4);
            else if (hairContentCurrentSeletion.name == "HairInvenToggle5")
                HairActionShow(5);
            else if (hairContentCurrentSeletion.name == "HairInvenToggle6")
                HairActionShow(6);
            else if (hairContentCurrentSeletion.name == "HairInvenToggle7")
                HairActionShow(7);
            else if (hairContentCurrentSeletion.name == "HairInvenToggle8")
                HairActionShow(8);
            else if (hairContentCurrentSeletion.name == "HairInvenToggle9")
                HairActionShow(9);
            else if (hairContentCurrentSeletion.name == "HairInvenToggle10")
                HairActionShow(10);
            else if (hairContentCurrentSeletion.name == "HairInvenToggle11")
                HairActionShow(11);
            else if (hairContentCurrentSeletion.name == "HairInvenToggle12")
                HairActionShow(12);
        }
    }


    //상의 판넬에 아이템 선택 시 이벤트 
    public void ShirtInvenContentToggleClick()
    {
        if(shirtContentToggleGroup.ActiveToggles().Any())
        {
            if(shirtContentCurrentSeletion.name == "ShirtInvenToggle1")
                ShirtActionShow(1);
            else if(shirtContentCurrentSeletion.name == "ShirtInvenToggle2")
                ShirtActionShow(2);
            else if (shirtContentCurrentSeletion.name == "ShirtInvenToggle3")
                ShirtActionShow(3);
            else if (shirtContentCurrentSeletion.name == "ShirtInvenToggle4")
                ShirtActionShow(4);
            else if (shirtContentCurrentSeletion.name == "ShirtInvenToggle5")
                ShirtActionShow(5);
            else if (shirtContentCurrentSeletion.name == "ShirtInvenToggle6")
                ShirtActionShow(6);
            else if (shirtContentCurrentSeletion.name == "ShirtInvenToggle7")
                ShirtActionShow(7);
            else if (shirtContentCurrentSeletion.name == "ShirtInvenToggle8")
                ShirtActionShow(8);
            else if (shirtContentCurrentSeletion.name == "ShirtInvenToggle9")
                ShirtActionShow(9);
            else if (shirtContentCurrentSeletion.name == "ShirtInvenToggle10")
                ShirtActionShow(10);
            else if (shirtContentCurrentSeletion.name == "ShirtInvenToggle11")
                ShirtActionShow(11);
            else if (shirtContentCurrentSeletion.name == "ShirtInvenToggle12")
                ShirtActionShow(12);
        }
    }
    

    //하의 판넬에 아이템 선택 시
    public void PantsInvenContentToggleClick()
    {
        if (pantsContentToggleGroup.ActiveToggles().Any())
        {
            if(pantsContentCurrentSeletion.name == "PantsInvenToggle1")
                PantsActionShow(1);
            else if (pantsContentCurrentSeletion.name == "PantsInvenToggle2")
                PantsActionShow(2);
            else if (pantsContentCurrentSeletion.name == "PantsInvenToggle3")
                PantsActionShow(3);
            else if (pantsContentCurrentSeletion.name == "PantsInvenToggle4")
                PantsActionShow(4);
            else if (pantsContentCurrentSeletion.name == "PantsInvenToggle5")
                PantsActionShow(5);
            else if (pantsContentCurrentSeletion.name == "PantsInvenToggle6")
                PantsActionShow(6);
            else if (pantsContentCurrentSeletion.name == "PantsInvenToggle7")
                PantsActionShow(7);
            else if (pantsContentCurrentSeletion.name == "PantsInvenToggle8")
                PantsActionShow(8);
            else if (pantsContentCurrentSeletion.name == "PantsInvenToggle9")
                PantsActionShow(9);
            else if (pantsContentCurrentSeletion.name == "PantsInvenToggle10")
                PantsActionShow(10);
            else if (pantsContentCurrentSeletion.name == "PantsInvenToggle11")
                PantsActionShow(11);
            else if (pantsContentCurrentSeletion.name == "PantsInvenToggle12")
                PantsActionShow(12);
        }
    }

    //신발 판넬에 아이템 선택 시
    public void ShoesInvenConentToggleClick()
    {
        if(shoesContentToggleGroup.ActiveToggles().Any())
        {
            if (shoesContentCurrentSeletion.name == "ShoesInvenToggle1")
                ShoesActionShow(1);
            else if (shoesContentCurrentSeletion.name == "ShoesInvenToggle2")
                ShoesActionShow(2);
            else if (shoesContentCurrentSeletion.name == "ShoesInvenToggle3")
                ShoesActionShow(3);
            else if (shoesContentCurrentSeletion.name == "ShoesInvenToggle4")
                ShoesActionShow(4);
            else if (shoesContentCurrentSeletion.name == "ShoesInvenToggle5")
                ShoesActionShow(5);
            else if (shoesContentCurrentSeletion.name == "ShoesInvenToggle6")
                ShoesActionShow(6);
            else if (shoesContentCurrentSeletion.name == "ShoesInvenToggle7")
                ShoesActionShow(7);
            else if (shoesContentCurrentSeletion.name == "ShoesInvenToggle8")
                ShoesActionShow(8);
            else if (shoesContentCurrentSeletion.name == "ShoesInvenToggle9")
                ShoesActionShow(9);
            else if (shoesContentCurrentSeletion.name == "ShoesInvenToggle10")
                ShoesActionShow(10);
            else if (shoesContentCurrentSeletion.name == "ShoesInvenToggle11")
                ShoesActionShow(11);
            else if (shoesContentCurrentSeletion.name == "ShoesInvenToggle12")
                ShoesActionShow(12);
        }
    }

    //헬맷 판넬에 아이템 선택 시
    public void HelmetInvenContentToggleClick()
    {
        if (helmetContentToggleGroup.ActiveToggles().Any())
        {
            if (helmetContentCurrentSeletion.name == "HelmetInvenToggle1")
                HelmetActionShow(1);
            else if (helmetContentCurrentSeletion.name == "HelmetInvenToggle2")
                HelmetActionShow(2);
            else if (helmetContentCurrentSeletion.name == "HelmetInvenToggle3")
                HelmetActionShow(3);
            else if (helmetContentCurrentSeletion.name == "HelmetInvenToggle4")
                HelmetActionShow(4);
            else if (helmetContentCurrentSeletion.name == "HelmetInvenToggle5")
                HelmetActionShow(5);
            else if (helmetContentCurrentSeletion.name == "HelmetInvenToggle6")
                HelmetActionShow(6);
            else if (helmetContentCurrentSeletion.name == "HelmetInvenToggle7")
                HelmetActionShow(7);
            else if (helmetContentCurrentSeletion.name == "HelmetInvenToggle8")
                HelmetActionShow(8);
            else if (helmetContentCurrentSeletion.name == "HelmetInvenToggle9")
                HelmetActionShow(9);
            else if (helmetContentCurrentSeletion.name == "HelmetInvenToggle10")
                HelmetActionShow(10);
            else if (helmetContentCurrentSeletion.name == "HelmetInvenToggle11")
                HelmetActionShow(11);
            else if (helmetContentCurrentSeletion.name == "HelmetInvenToggle12")
                HelmetActionShow(12);
        }
    }

    //장갑 판넬에 아이템 선택 시
    public void GlovesInvenContentToggleClick()
    {
        if(glovesContentToggleGroup.ActiveToggles().Any())
        {
            if (glovesContentCurrentSeletion.name == "GlovesInvenToggle1")
                GlovesActionShow(1);
            else if (glovesContentCurrentSeletion.name == "GlovesInvenToggle2")
                GlovesActionShow(2);
            else if (glovesContentCurrentSeletion.name == "GlovesInvenToggle3")
                GlovesActionShow(3);
            else if (glovesContentCurrentSeletion.name == "GlovesInvenToggle4")
                GlovesActionShow(4);
            else if (glovesContentCurrentSeletion.name == "GlovesInvenToggle5")
                GlovesActionShow(5);
            else if (glovesContentCurrentSeletion.name == "GlovesInvenToggle6")
                GlovesActionShow(6);
            else if (glovesContentCurrentSeletion.name == "GlovesInvenToggle7")
                GlovesActionShow(7);
            else if (glovesContentCurrentSeletion.name == "GlovesInvenToggle8")
                GlovesActionShow(8);
            else if (glovesContentCurrentSeletion.name == "GlovesInvenToggle9")
                GlovesActionShow(9);
            else if (glovesContentCurrentSeletion.name == "GlovesInvenToggle10")
                GlovesActionShow(10);
            else if (glovesContentCurrentSeletion.name == "GlovesInvenToggle11")
                GlovesActionShow(11);
            else if (glovesContentCurrentSeletion.name == "GlovesInvenToggle12")
                GlovesActionShow(12);
        }
    }

    //장비전체 판넬에 아이템 선택 시
    public void EquipmentAllToggleClilck()
    {
        if (equipmentAllToggleGroup.ActiveToggles().Any())
        {
            if (equipmentAllCureentSeletion.name == "AllEquipmentToggle1")
                AllEquipmentActionShow(0);
            else if (equipmentAllCureentSeletion.name == "AllEquipmentToggle2")
                AllEquipmentActionShow(1);
            else if (equipmentAllCureentSeletion.name == "AllEquipmentToggle3")
                AllEquipmentActionShow(2);
            else if (equipmentAllCureentSeletion.name == "AllEquipmentToggle4")
                AllEquipmentActionShow(3);
            else if (equipmentAllCureentSeletion.name == "AllEquipmentToggle5")
                AllEquipmentActionShow(4);
        }
    }


    //자전거 판넬에 아이템 선택 시
    public void BikeInvenContentToggleClick()
    {
        if (bikeContentToggleGroup.ActiveToggles().Any())
        {
            if (bikeCureentSeletion.name == "BicycleInvenToggle1")
                BicycleActionShow(1);
            else if (bikeCureentSeletion.name == "BicycleInvenToggle2")
                BicycleActionShow(2);
            else if (bikeCureentSeletion.name == "BicycleInvenToggle3")
                BicycleActionShow(3);
            else if (bikeCureentSeletion.name == "BicycleInvenToggle4")
                BicycleActionShow(4);
            else if (bikeCureentSeletion.name == "BicycleInvenToggle5")
                BicycleActionShow(5);
            else if (bikeCureentSeletion.name == "BicycleInvenToggle6")
                BicycleActionShow(6);
            else if (bikeCureentSeletion.name == "BicycleInvenToggle7")
                BicycleActionShow(7);
            else if (bikeCureentSeletion.name == "BicycleInvenToggle8")
                BicycleActionShow(8);
            else if (bikeCureentSeletion.name == "BicycleInvenToggle9")
                BicycleActionShow(9);
            else if (bikeCureentSeletion.name == "BicycleInvenToggle10")
                BicycleActionShow(10);
            else if (bikeCureentSeletion.name == "BicycleInvenToggle11")
                BicycleActionShow(11);
            else if (bikeCureentSeletion.name == "BicycleInvenToggle12")
                BicycleActionShow(12);
        }
    }

    void AllClothesActionShow(int _number)
    {
        invenCtrl_scrip = allClothesContents[_number - 1].GetComponent<InventoryContentCtrl>();

        string folderName = invenCtrl_scrip.folder;
        //Debug.Log("왜 ???" + PlayerPrefs.GetString("AT_Player_Sex"));

        PlayerPrefs.SetString("AT_CustomChange", "Yes");   //커스텀 변경 여부

        if(PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            if(folderName == "Hair")
            {
                id_hairNum = invenCtrl_scrip.itemNum;
                id_hairStr = invenCtrl_scrip.imgname;
                womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

                if (id_hairNum == 0)
                    womanctrl_scrip.HairSetting(true, false, false);
                else if (id_hairNum == 1)
                    womanctrl_scrip.HairSetting(false, true, false);
                else if (id_hairNum == 2)
                    womanctrl_scrip.HairSetting(false, false, true);
            }
            else if(folderName == "Jacket")
            {
                id_jacketNum = invenCtrl_scrip.itemNum;
                id_JacketStr = invenCtrl_scrip.imgname;
                wear_Jacketkind = invenCtrl_scrip.id;
                womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

                if (id_jacketNum == 0)
                    womanctrl_scrip.JacketSetting(true, false, false);
                else if (id_jacketNum == 1)
                    womanctrl_scrip.JacketSetting(false, true, false);
                else if (id_jacketNum == 2)
                    womanctrl_scrip.JacketSetting(false, false, true);
                womanctrl_scrip.JacketTextrueSetting(id_JacketStr, id_jacketNum);
            }
            else if(folderName == "Pants")
            {
                id_pantsNum = invenCtrl_scrip.itemNum;
                id_PantsStr = invenCtrl_scrip.imgname;
                wear_PantsKind = invenCtrl_scrip.id;
                womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

                if (invenCtrl_scrip.itemNum == 0)
                    womanctrl_scrip.PantsSetting(true, false);
                else if (invenCtrl_scrip.itemNum == 1)
                    womanctrl_scrip.PantsSetting(false, true);

                womanctrl_scrip.PantsTextrueSetting(id_PantsStr, id_pantsNum);
            }
            else if(folderName == "Shoes")
            {
                id_shoesNum = invenCtrl_scrip.itemNum;
                id_ShoesStr = invenCtrl_scrip.imgname;
                wear_ShoesKind = invenCtrl_scrip.id;
                womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

                if (invenCtrl_scrip.itemNum == 0)
                    womanctrl_scrip.ShoesSetting(true, false);
                else if (invenCtrl_scrip.itemNum == 1)
                    womanctrl_scrip.ShoesSetting(false, true);

                womanctrl_scrip.ShoesTextrueSetting(id_ShoesStr, id_shoesNum);
            }
            else if(folderName == "Helmet")
            {
                id_helmetNum = invenCtrl_scrip.itemNum;
                id_helmetStr = invenCtrl_scrip.imgname;
                wear_HelmetKind = invenCtrl_scrip.id;
                womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

                if (invenCtrl_scrip.itemNum == 0)
                    womanctrl_scrip.HelmetSetting(true);
                else
                    womanctrl_scrip.HelmetSetting(false);

                womanctrl_scrip.HelmetTextrueSetting(id_helmetStr, id_helmetNum);
            }
            else if(folderName == "Gloves")
            {
                id_glovesNum = invenCtrl_scrip.itemNum;
                id_glovesStr = invenCtrl_scrip.imgname;
                wear_GlovesKind = invenCtrl_scrip.id;
                womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

                if (invenCtrl_scrip.itemNum == 0)
                    womanctrl_scrip.GlovesSetting(true);
                else
                    womanctrl_scrip.GlovesSetting(false);

                womanctrl_scrip.GlovesTextrueSetting(id_glovesStr, id_glovesNum);
            }
        }
        else if(PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            //Debug.Log("id_hairNum " + id_hairNum + " id_bodyNum " + id_bodyNum + " id_jacketNum " + id_jacketNum + " id_pantsNum " + id_pantsNum + " id_shoesNum " + id_shoesNum);
            if (folderName == "Hair")
            {
                id_hairNum = invenCtrl_scrip.itemNum;
                id_hairStr = invenCtrl_scrip.imgname;
                manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

                if (id_hairNum == 0)
                    manctrl_scrip.HairSetting(true, false, false, false);
                else if (id_hairNum == 1)
                    manctrl_scrip.HairSetting(false, true, false, false);
                else if (id_hairNum == 2)
                    manctrl_scrip.HairSetting(false, false, true, false);
                else if (id_hairNum == 3)
                    manctrl_scrip.HairSetting(false, false, false, true);
            }
            else if (folderName == "Jacket")
            {
                id_jacketNum = invenCtrl_scrip.itemNum;
                id_JacketStr = invenCtrl_scrip.imgname;
                wear_Jacketkind = invenCtrl_scrip.id;
                manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

                if (id_jacketNum == 0)
                    manctrl_scrip.JacketSetting(true, false, false);
                else if (id_jacketNum == 1)
                    manctrl_scrip.JacketSetting(false, true, false);
                else if (id_jacketNum == 2)
                    manctrl_scrip.JacketSetting(false, false, true);
                manctrl_scrip.JacketTextrueSetting(id_JacketStr, id_jacketNum);
            }
            else if (folderName == "Pants")
            {
                id_pantsNum = invenCtrl_scrip.itemNum;
                id_PantsStr = invenCtrl_scrip.imgname;
                wear_PantsKind = invenCtrl_scrip.id;
                manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

                if (invenCtrl_scrip.itemNum == 0)
                    manctrl_scrip.PantsSetting(true, false);
                else if (invenCtrl_scrip.itemNum == 1)
                    manctrl_scrip.PantsSetting(false, true);

                manctrl_scrip.PantsTextrueSetting(id_PantsStr, id_pantsNum);
            }
            else if (folderName == "Shoes")
            {
                id_shoesNum = invenCtrl_scrip.itemNum;
                id_ShoesStr = invenCtrl_scrip.imgname;
                wear_ShoesKind = invenCtrl_scrip.id;
                manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

                if (invenCtrl_scrip.itemNum == 0)
                    manctrl_scrip.ShoesSetting(true, false);
                else if (invenCtrl_scrip.itemNum == 1)
                    manctrl_scrip.ShoesSetting(false, true);

                manctrl_scrip.ShoesTextrueSetting(id_ShoesStr, id_shoesNum);
            }
            else if (folderName == "Helmet")
            {
                id_helmetNum = invenCtrl_scrip.itemNum;
                id_helmetStr = invenCtrl_scrip.imgname;
                wear_HelmetKind = invenCtrl_scrip.id;
                manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

                if (invenCtrl_scrip.itemNum == 0)
                    manctrl_scrip.HelmetSetting(true);
                else
                    manctrl_scrip.HelmetSetting(false);

                manctrl_scrip.HelmetTextrueSetting(id_helmetStr, id_helmetNum);
            }
            else if (folderName == "Gloves")
            {
                id_glovesNum = invenCtrl_scrip.itemNum;
                id_glovesStr = invenCtrl_scrip.imgname;
                wear_GlovesKind = invenCtrl_scrip.id;
                manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

                if (invenCtrl_scrip.itemNum == 0)
                    manctrl_scrip.GlovesSetting(true);
                else
                    manctrl_scrip.GlovesSetting(false);

                manctrl_scrip.GlovesTextrueSetting(id_glovesStr, id_glovesNum);
            }
        }
    }

    void AllEquipmentActionShow(int _number)
    {
        invenCtrl_scrip = allEquipmentContents[_number - 1].GetComponent<InventoryContentCtrl>();

        string folderName = invenCtrl_scrip.folder;
        PlayerPrefs.SetString("AT_CustomChange", "Yes");   //커스텀 변경 여부

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            if (folderName == "Bicycle")
            {
                id_bicycleNum = invenCtrl_scrip.itemNum;
                id_bicycleStr = invenCtrl_scrip.imgname;
                womanctrl_scrip.BicycleTextrueSetting(id_bicycleStr, id_bicycleNum);
            }
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            if (folderName == "Bicycle")
            {
                id_bicycleNum = invenCtrl_scrip.itemNum;
                id_bicycleStr = invenCtrl_scrip.imgname;
                manctrl_scrip.BicycleTextrueSetting(id_bicycleStr, id_bicycleNum);
            }
        }
    }

    void AllItemActionShow(int _number)
    {
        invenCtrl_scrip = allEquipmentContents[_number - 1].GetComponent<InventoryContentCtrl>();

        string folderName = invenCtrl_scrip.folder;

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            if (folderName == "Exp")
            {
                id_bicycleNum = invenCtrl_scrip.itemNum;
                id_bicycleStr = invenCtrl_scrip.imgname;
                womanctrl_scrip.BicycleTextrueSetting(id_bicycleStr, id_bicycleNum);
            }
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
        }
    }

    //머리 교체
    void HairActionShow(int _number)
    {
        if(PlayerPrefs.GetString("AT_CustomChange").Equals("MissionOk"))
            PlayerPrefs.SetString("AT_CustomChange", "MissionOk");   //커스텀 변경 여부
        else
            PlayerPrefs.SetString("AT_CustomChange", "Yes");   //커스텀 변경 여부

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            invenCtrl_scrip = hairContents[_number - 1].GetComponent<InventoryContentCtrl>();
            id_hairNum = invenCtrl_scrip.itemNum;
            id_hairStr = invenCtrl_scrip.imgname;
            womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

            if (id_hairNum == 0)
            {
                hairStyleStr = "Hair1";
                womanctrl_scrip.HairSetting(true, false, false);
            }
            else if (id_hairNum == 1)
            {
                hairStyleStr = "Hair2";
                womanctrl_scrip.HairSetting(false, true, false);
            }
            else if (id_hairNum == 2)
            {
                hairStyleStr = "Hair3";
                womanctrl_scrip.HairSetting(false, false, true);
            }
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            invenCtrl_scrip = hairContents[_number - 1].GetComponent<InventoryContentCtrl>();
            id_hairNum = invenCtrl_scrip.itemNum;
            id_hairStr = invenCtrl_scrip.imgname;
            

            if (invenCtrl_scrip.itemNum == 0)
            {
                hairStyleStr = "Hair1";
                manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                manctrl_scrip.HairSetting(true, false, false, false);
            }
            else if (invenCtrl_scrip.itemNum == 1)
            {
                //헬맷을 착용하고 있을 경우
                if(id_helmetNum == 0)
                {
                    id_hairNum = 3;
                    hairStyleStr = "HelmetHair";
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    manctrl_scrip.HairSetting(false, false, false, true);
                }
                else
                {
                    hairStyleStr = "Hair2";
                    manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                    manctrl_scrip.HairSetting(false, true, false, false);
                }
            }
            else if (invenCtrl_scrip.itemNum == 2)
            {
                hairStyleStr = "Hair3";
                manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                manctrl_scrip.HairSetting(false, false, true, false);
            }
            //else if (invenCtrl_scrip.itemNum == 3)
            //{
            //    hairStyleStr = "HelmetHair";
            //    manctrl_scrip.HairSetting(false, false, false, true);
            //}
                
        }
    }

    //상의 교체
    void ShirtActionShow(int _number)
    {
        //Debug.Log("_number " + _number);
        if (PlayerPrefs.GetString("AT_CustomChange").Equals("MissionOk"))
            PlayerPrefs.SetString("AT_CustomChange", "MissionOk");   //커스텀 변경 여부
        else
            PlayerPrefs.SetString("AT_CustomChange", "Yes");   //커스텀 변경 여부

        invenCtrl_scrip = shirtContents[_number-1].GetComponent<InventoryContentCtrl>();
        id_jacketNum = invenCtrl_scrip.itemNum; Debug.Log("id_jacketNum " + id_jacketNum);
        id_JacketStr = invenCtrl_scrip.imgname;
        wear_Jacketkind = invenCtrl_scrip.id;

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

            if (invenCtrl_scrip.itemNum == 0)
            {
                womanctrl_scrip.JacketSetting(true, false, false);
            }
            else if (invenCtrl_scrip.itemNum == 1)
            {
                womanctrl_scrip.JacketSetting(false, true, false);
            }
            else if (invenCtrl_scrip.itemNum == 2)
            {
                womanctrl_scrip.JacketSetting(false, false, true);
            }
            womanctrl_scrip.JacketTextrueSetting(id_JacketStr, id_jacketNum);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

            if (invenCtrl_scrip.itemNum == 0)
            {
                manctrl_scrip.JacketSetting(true, false, false);
            }
            else if (invenCtrl_scrip.itemNum == 1)
            {
                manctrl_scrip.JacketSetting(false, true, false);
            }
            else if (invenCtrl_scrip.itemNum == 2)
            {
                manctrl_scrip.JacketSetting(false, false, true);
            }
            manctrl_scrip.JacketTextrueSetting(id_JacketStr, id_jacketNum);
        }
    }

    //바지 교체 - 아이디에 해당하는 활성화 비활성화
    void PantsActionShow(int _number)
    {
        if (PlayerPrefs.GetString("AT_CustomChange").Equals("MissionOk"))
            PlayerPrefs.SetString("AT_CustomChange", "MissionOk");   //커스텀 변경 여부
        else
            PlayerPrefs.SetString("AT_CustomChange", "Yes");   //커스텀 변경 여부

        invenCtrl_scrip = pantsContents[_number - 1].GetComponent<InventoryContentCtrl>();
        id_pantsNum = invenCtrl_scrip.itemNum;
        id_PantsStr = invenCtrl_scrip.imgname;
        wear_PantsKind = invenCtrl_scrip.id;


        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

            if (invenCtrl_scrip.itemNum == 0)
                womanctrl_scrip.PantsSetting(true, false);
            else if (invenCtrl_scrip.itemNum == 1)
                womanctrl_scrip.PantsSetting(false, true);

            womanctrl_scrip.PantsTextrueSetting(id_PantsStr, id_pantsNum);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

            if (invenCtrl_scrip.itemNum == 0)
                manctrl_scrip.PantsSetting(true, false);
            else if (invenCtrl_scrip.itemNum == 1)
                manctrl_scrip.PantsSetting(false, true);

            manctrl_scrip.PantsTextrueSetting(id_PantsStr, id_pantsNum);
        }

    }

    //신발 교체 - 아이디에 해당하는 활성화 비활성화
    void ShoesActionShow(int _number)
    {
        if (PlayerPrefs.GetString("AT_CustomChange").Equals("MissionOk"))
            PlayerPrefs.SetString("AT_CustomChange", "MissionOk");   //커스텀 변경 여부
        else
            PlayerPrefs.SetString("AT_CustomChange", "Yes");   //커스텀 변경 여부

        invenCtrl_scrip = shoesContents[_number - 1].GetComponent<InventoryContentCtrl>();
        id_shoesNum = invenCtrl_scrip.itemNum;
        id_ShoesStr = invenCtrl_scrip.imgname;
        wear_ShoesKind = invenCtrl_scrip.id;

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

            if (invenCtrl_scrip.itemNum == 0)
                womanctrl_scrip.ShoesSetting(true, false);
            else if (invenCtrl_scrip.itemNum == 1)
                womanctrl_scrip.ShoesSetting(false, true);

            womanctrl_scrip.ShoesTextrueSetting(id_ShoesStr, id_shoesNum);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

            if (invenCtrl_scrip.itemNum == 0)
                manctrl_scrip.ShoesSetting(true, false);
            else if (invenCtrl_scrip.itemNum == 1)
                manctrl_scrip.ShoesSetting(false, true);

            manctrl_scrip.ShoesTextrueSetting(id_ShoesStr, id_shoesNum);
        }

    }
    //헬맷교체 - 아이디에 해당하는 활성화 비활성화
    void HelmetActionShow(int _number)
    {
        if (PlayerPrefs.GetString("AT_CustomChange").Equals("MissionOk"))
            PlayerPrefs.SetString("AT_CustomChange", "MissionOk");   //커스텀 변경 여부
        else
            PlayerPrefs.SetString("AT_CustomChange", "Yes");   //커스텀 변경 여부

        PlayerPrefs.SetString("AT_Wear_HelmetKind", "ItemID_Helmet");  //헬맷을 착용했다는거 저장
        invenCtrl_scrip = helmetContents[_number - 1].GetComponent<InventoryContentCtrl>();
        id_helmetNum = invenCtrl_scrip.itemNum;
        id_helmetStr = invenCtrl_scrip.imgname;
        wear_HelmetKind = invenCtrl_scrip.id;

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

            if (invenCtrl_scrip.itemNum == 0)
                womanctrl_scrip.HelmetSetting(true);
            else
                womanctrl_scrip.HelmetSetting(false);

            womanctrl_scrip.HelmetTextrueSetting(id_helmetStr, id_helmetNum);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            if (id_hairNum == 1)
            {
                id_hairNum = 3; //헬맷 머리로 변경 
                hairStyleStr = "HelmetHair";
                PlayerPrefs.SetString("AT_Player_Hair", "HelmetHair");
                manctrl_scrip.HelmetSetting(true);
                manctrl_scrip.HairSetting(false, false, false, true);
                manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                //Debug.Log("머리스타일 --- " + id_hairNum);
            }
            else
            {
                //Debug.Log("머리스타일 --- " + id_hairNum);
                manctrl_scrip.HelmetSetting(true);
                manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
            }
                


            //if (invenCtrl_scrip.itemNum == 0)
            //    manctrl_scrip.HelmetSetting(true);
            //else
            //    manctrl_scrip.HelmetSetting(false);

            /////Debug.Log("id_helmetStr " + id_helmetStr + " id_helmetNum " + id_helmetNum);
            manctrl_scrip.HelmetTextrueSetting(id_helmetStr, id_helmetNum);
        }
        
    }
    //장갑교체 - 아이디에 해당하는 활성화 비활성화
    void GlovesActionShow(int _number)
    {
        if (PlayerPrefs.GetString("AT_CustomChange").Equals("MissionOk"))
            PlayerPrefs.SetString("AT_CustomChange", "MissionOk");   //커스텀 변경 여부
        else
            PlayerPrefs.SetString("AT_CustomChange", "Yes");   //커스텀 변경 여부

        PlayerPrefs.SetString("AT_Wear_GlovesKind", "ItemID_Gloves");  //장갑을 착용했다는거 저장
        invenCtrl_scrip = glovesContents[_number - 1].GetComponent<InventoryContentCtrl>();
        id_glovesNum = invenCtrl_scrip.itemNum;
        id_glovesStr = invenCtrl_scrip.imgname;
        wear_GlovesKind = invenCtrl_scrip.id;

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            womanctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

            if (invenCtrl_scrip.itemNum == 0)
                womanctrl_scrip.GlovesSetting(true);
            else
                womanctrl_scrip.GlovesSetting(false);

            womanctrl_scrip.GlovesTextrueSetting(id_glovesStr, id_glovesNum);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);

            if (invenCtrl_scrip.itemNum == 0)
                manctrl_scrip.GlovesSetting(true);
            else
                manctrl_scrip.GlovesSetting(false);

            manctrl_scrip.GlovesTextrueSetting(id_glovesStr, id_glovesNum);
        }
    }

    void BicycleActionShow(int _number)
    {
        if (PlayerPrefs.GetString("AT_CustomChange").Equals("MissionOk"))
            PlayerPrefs.SetString("AT_CustomChange", "MissionOk");   //커스텀 변경 여부
        else
            PlayerPrefs.SetString("AT_CustomChange", "Yes");   //커스텀 변경 여부

        PlayerPrefs.SetString("AT_Wear_BicycleKind", "ItemID_Bicycle");  //자전거 종류 기본 베이스
        invenCtrl_scrip = bikeContents[_number - 1].GetComponent<InventoryContentCtrl>();
        id_bicycleNum = invenCtrl_scrip.itemNum;
        id_bicycleStr = invenCtrl_scrip.imgname;
        wear_BicycleKind = invenCtrl_scrip.id;

        //Debug.Log("id_bicycleNum " + id_bicycleNum + " id_bicycleStr " + id_bicycleStr);

        if(PlayerPrefs.GetString("AT_Player_Sex").Equals("Woman"))
        {
            womanctrl_scrip.BicycleTextrueSetting(id_bicycleStr, id_bicycleNum);
        }
        else if(PlayerPrefs.GetString("AT_Player_Sex").Equals("Man"))
        {
            manctrl_scrip.BicycleTextrueSetting(id_bicycleStr, id_bicycleNum);
        }
    }



    //착용한 아이템에 해당하는 토글이 선택되게 하는 이벤트
    public void HairToggleIsOn()
    {
        for (int i = 0; i <= hairCount; i++)
        {
            invenCtrl_scrip = hairContents[i].GetComponent<InventoryContentCtrl>();

            //헬맷을 쓴 상태였으면 - 임의로 변경해준다. 해당 이미지에 맞는 모델링인지 토글에서 확인하고 위함 작업
            if (id_helmetNum == 0)
            {
                id_hairNum = 1;
                hairStyleStr = "Hair2";
            }

            //Debug.Log(id_hairNum + " itemNum " + invenCtrl_scrip.itemNum + " id_hairNum : " + hairStyleStr + " invenCtrl_scrip.imgname " + invenCtrl_scrip.imgname);
            if (invenCtrl_scrip.itemNum == id_hairNum && invenCtrl_scrip.imgname == hairStyleStr)
            {
                hairContents[i].GetComponent<Toggle>().isOn = true;
                //Debug.Log("토글 이름 : " + hairContents[i].GetComponent<Toggle>().name);
            }
            else
            {
                hairContents[i].GetComponent<Toggle>().isOn = false;
            }
                
        }
    }

    public void ShirtToggleIsOn()
    {
        for(int i = 0; i <= jacketCount; i++)
        {
            invenCtrl_scrip = shirtContents[i].GetComponent<InventoryContentCtrl>();

            //Debug.Log(id_jacketNum + " itemNum " + invenCtrl_scrip.itemNum + " id_jacketNum " + PlayerPrefs.GetInt("id_jacketNum"));
            if (invenCtrl_scrip.itemNum == id_jacketNum && invenCtrl_scrip.imgname == id_JacketStr)
            {
                shirtContents[i].GetComponent<Toggle>().isOn = true;
            }
            else
                shirtContents[i].GetComponent<Toggle>().isOn = false;
        }
    }

    public void PantsToggleIsOn()
    {
        for(int i = 0; i <= pantsCount; i++)
        {
            invenCtrl_scrip = pantsContents[i].GetComponent<InventoryContentCtrl>();

            if (invenCtrl_scrip.itemNum == id_pantsNum && invenCtrl_scrip.imgname == id_PantsStr)
                pantsContents[i].GetComponent<Toggle>().isOn = true;
            else
                pantsContents[i].GetComponent<Toggle>().isOn = false;
        }
    }
    
    public void ShoesToggleIsOn()
    {
        for(int i = 0; i <= shoesCount; i++)
        {
            invenCtrl_scrip = shoesContents[i].GetComponent<InventoryContentCtrl>();

            if (invenCtrl_scrip.itemNum == id_shoesNum && invenCtrl_scrip.imgname == id_ShoesStr)
                shoesContents[i].GetComponent<Toggle>().isOn = true;
            else
                shoesContents[i].GetComponent<Toggle>().isOn = false;
        }
    }

    public void HelmetToggleIsOn()
    {
        for(int i = 0; i <= helmetCount; i++)
        {
            invenCtrl_scrip = helmetContents[i].GetComponent<InventoryContentCtrl>();

            //Debug.Log(id_helmetNum + " itemNum " + invenCtrl_scrip.itemNum + " HelmetNumber " + PlayerPrefs.GetInt("AT_HelmetNumber"));
            if (invenCtrl_scrip.itemNum == id_helmetNum && invenCtrl_scrip.imgname == id_helmetStr)
                helmetContents[i].GetComponent<Toggle>().isOn = true;
            else
                helmetContents[i].GetComponent<Toggle>().isOn = false;
        }
    }

    public void GlovesToggleIsOn()
    {
        for(int i = 0; i <= glovesCount; i++)
        {
            invenCtrl_scrip = glovesContents[i].GetComponent<InventoryContentCtrl>();

            //Debug.Log(id_glovesNum + " itemNum " + invenCtrl_scrip.itemNum + " HelmetNumber " + PlayerPrefs.GetInt("AT_GlovesNumber"));
            if (invenCtrl_scrip.itemNum == id_glovesNum && invenCtrl_scrip.imgname == id_glovesStr)
                glovesContents[i].GetComponent<Toggle>().isOn = true;
            else
                glovesContents[i].GetComponent<Toggle>().isOn = false;
        }
    }



    //헬맷벗기 이벤트
    public void HelmetOutButtonOn()
    {
        if (PlayerPrefs.GetString("AT_CustomChange").Equals("MissionOk"))
            PlayerPrefs.SetString("AT_CustomChange", "MissionOk");   //커스텀 변경 여부
        else
            PlayerPrefs.SetString("AT_CustomChange", "Yes");   //커스텀 변경 여부

        id_helmetNum = 100; //착용하지 않았다는 숫자
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            womanctrl_scrip.HelmetSetting(false);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            manctrl_scrip.HelmetSetting(false);

            if (hairStyleStr == "HelmetHair")
            {
                hairStyleStr = "Hair2";
                id_hairNum = 1;
                manctrl_scrip.Animator_Initialization(id_hairNum, id_bodyNum, id_jacketNum, id_pantsNum, id_shoesNum);
                manctrl_scrip.HairSetting(false, true, false, false);
                
            }
        }
        wear_HelmetKind = "No";
        PlayerPrefs.SetString("AT_Wear_HelmetKind", wear_HelmetKind);
    }
    //장갑벗기 이벤트
    public void GlovesOutButtonOn()
    {
        if (PlayerPrefs.GetString("AT_CustomChange").Equals("MissionOk"))
            PlayerPrefs.SetString("AT_CustomChange", "MissionOk");   //커스텀 변경 여부
        else
            PlayerPrefs.SetString("AT_CustomChange", "Yes");   //커스텀 변경 여부

        id_glovesNum = 100; //착용하지 않았다는 숫자
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            womanctrl_scrip.GlovesSetting(false);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            manctrl_scrip.GlovesSetting(false);
        }
        
        wear_GlovesKind = "No";
        PlayerPrefs.SetString("AT_Wear_GlovesKind", wear_GlovesKind);
    }
    

    public void CustomSettingSave()
    {
        PlayerPrefs.SetString("AT_Player_Hair", hairStyleStr);
        //장착 아이템 번호 저장
        PlayerPrefs.SetInt("AT_HairNumber", id_hairNum);
        PlayerPrefs.SetInt("AT_BodyNumber", id_bodyNum);
        PlayerPrefs.SetInt("AT_JacketNumber", id_jacketNum);
        PlayerPrefs.SetInt("AT_PantsNumber", id_pantsNum);
        PlayerPrefs.SetInt("AT_ShoesNumber", id_shoesNum);
        PlayerPrefs.SetInt("AT_HelmetNumber", id_helmetNum);
        PlayerPrefs.SetInt("AT_GlovesNumber", id_glovesNum);
        PlayerPrefs.SetInt("AT_BicycleNumber", id_bicycleNum);

        //장착 아이템 텍스쳐 저장
        PlayerPrefs.SetString("AT_Wear_JacketStyleName", id_JacketStr);
        PlayerPrefs.SetString("AT_Wear_PantsStyleName", id_PantsStr);
        PlayerPrefs.SetString("AT_Wear_ShoesStyleName", id_ShoesStr);
        PlayerPrefs.SetString("AT_Wear_HelmetStyleName", id_helmetStr);
        PlayerPrefs.SetString("AT_Wear_GlovesStyleName", id_glovesStr);
        PlayerPrefs.SetString("AT_Wear_HairStyleName", id_hairStr);
        PlayerPrefs.SetString("AT_Wear_BicycleStyleName", id_bicycleStr);


        //장착 아이템 아이디 저장
        PlayerPrefs.SetString("AT_Wear_GlovesKind", wear_GlovesKind);
        PlayerPrefs.SetString("AT_Wear_HelmetKind", wear_HelmetKind);
        PlayerPrefs.SetString("AT_Wear_JacketKind", wear_Jacketkind);
        PlayerPrefs.SetString("AT_Wear_PantsKind", wear_PantsKind);
        PlayerPrefs.SetString("AT_Wear_ShoesKind", wear_ShoesKind);
        PlayerPrefs.SetString("AT_Wear_BicycleKind", wear_BicycleKind);
        // 장착 업데이트
        ServerManager.Instance.BuyItems_Update();

        //Debug.Log("id_bicycleNum " + PlayerPrefs.GetString("AT_BicycleNumber") + " id_bicycleStr " + PlayerPrefs.GetString("AT_Wear_BicycleStyleName"));
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
