using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;



public class MyInventory_DataManager : MonoBehaviour
{
    public static MyInventory_DataManager instance { get; private set; }

    public GameObject gameEndPopup; //게임종료팝업


    public ToggleGroup itemAllToggleGroup;
    public ToggleGroup expContentToggleGroup;
    public ToggleGroup coinContentToggleGroup;
    public ToggleGroup speedContentToggleGroup;

    
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
        Initialization();

        AllBuyItemList(31, 35, allItemContents, 0); //아이템 전체
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

                if (PlayerPrefs.GetString("Busan_Player_Sex") == "Woman")
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
                            if(invenCtrl_scrip.imgname == "ExpPlus")
                                _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("Busan_ExpPlusAmount").ToString();
                            else if(invenCtrl_scrip.imgname == "ExpUp")
                                _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("Busan_ExpUpAmount").ToString();

                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                        }
                        else if (invenCtrl_scrip.folder == "Coin")
                        {
                            _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("Busan_CoinUpAmount").ToString();
                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                        }
                        else if (invenCtrl_scrip.folder == "Speed")
                        {
                            _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("Busan_SpeedUpAmount").ToString();
                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                        }
                        else
                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                    }
                        
                }
                else if (PlayerPrefs.GetString("Busan_Player_Sex") == "Man")
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
                            if (invenCtrl_scrip.imgname == "ExpPlus")
                                _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("Busan_ExpPlusAmount").ToString();
                            else if (invenCtrl_scrip.imgname == "ExpUp")
                                _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("Busan_ExpUpAmount").ToString();

                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                        }
                        else if (invenCtrl_scrip.folder == "Coin")
                        {
                            _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("Busan_CoinUpAmount").ToString();
                            invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                        }
                        else if (invenCtrl_scrip.folder == "Speed")
                        {
                            _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("Busan_SpeedUpAmount").ToString();
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

                    if (PlayerPrefs.GetString("Busan_Player_Sex") == "Woman")
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
                                if (invenCtrl_scrip.imgname == "ExpPlus")
                                    _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("Busan_ExpPlusAmount").ToString();
                                else if (invenCtrl_scrip.imgname == "ExpUp")
                                    _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("Busan_ExpUpAmount").ToString();

                                invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                            }
                            else if (invenCtrl_scrip.folder == "Coin")
                            {
                                _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("Busan_CoinUpAmount").ToString();
                                invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                            }
                            else if (invenCtrl_scrip.folder == "Speed")
                            {
                                _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("Busan_SpeedUpAmount").ToString();
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
                    else if (PlayerPrefs.GetString("Busan_Player_Sex") == "Man")
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
                                if (invenCtrl_scrip.imgname == "ExpPlus")
                                    _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("Busan_ExpPlusAmount").ToString();
                                else if (invenCtrl_scrip.imgname == "ExpUp")
                                    _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("Busan_ExpUpAmount").ToString();

                                invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                            }
                            else if (invenCtrl_scrip.folder == "Coin")
                            {
                                _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("Busan_CoinUpAmount").ToString();
                                invenCtrl_scrip.img.sprite = Resources.Load<Sprite>("Install Item/Item/" + invenCtrl_scrip.folder + "/" + invenCtrl_scrip.imgname);
                            }
                            else if (invenCtrl_scrip.folder == "Speed")
                            {
                                //Debug.Log("SpeedUpAmount " + PlayerPrefs.GetInt("Busan_SpeedUpAmount"));
                                _content[index].transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = PlayerPrefs.GetInt("Busan_SpeedUpAmount").ToString();
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
        if (PlayerPrefs.GetString("Busan_Player_Sex") == "Woman")
        {
            womanPlayer = GameObject.Find("Woman");
            womanctrl_scrip = womanPlayer.GetComponent<WomanCtrl>();
        }
        else if (PlayerPrefs.GetString("Busan_Player_Sex") == "Man")
        {
            manPlayer = GameObject.Find("Man");
            manctrl_scrip = manPlayer.GetComponent<ManCtrl>();
        }
        //각 아이템 배열 넘버 저장 220804수정
        //id_hairNum = PlayerPrefs.GetInt("Busan_HairNumber");
        //id_bodyNum = PlayerPrefs.GetInt("Busan_BodyNumber");
        //id_jacketNum = PlayerPrefs.GetInt("Busan_JacketNumber");
        //id_pantsNum = PlayerPrefs.GetInt("Busan_PantsNumber");
        //id_shoesNum = PlayerPrefs.GetInt("Busan_ShoesNumber");
        //id_helmetNum = PlayerPrefs.GetInt("Busan_HelmetNumber");
        //id_glovesNum = PlayerPrefs.GetInt("Busan_GlovesNumber");
        ////id_bicycleNum = PlayerPrefs.GetInt("BicycleNumber");

        ////각 아이템 텍스쳐 저장
        //id_JacketStr = PlayerPrefs.GetString("Busan_Wear_JacketStyleName");
        //id_PantsStr = PlayerPrefs.GetString("Busan_Wear_PantsStyleName");
        //id_ShoesStr = PlayerPrefs.GetString("Busan_Wear_ShoesStyleName");
        //id_helmetStr = PlayerPrefs.GetString("Busan_Wear_HelmetStyleName");
        //id_glovesStr = PlayerPrefs.GetString("Busan_Wear_GlovesStyleName");
        //id_hairStr = PlayerPrefs.GetString("Busan_Wear_HairStyleName");
        ////id_bicycleStr = PlayerPrefs.GetString("Wear_BicycleStyleName");

        ////각 아이템 장착 아이디 저장
        //hairStyleStr = PlayerPrefs.GetString("Busan_Player_Hair");
        //wear_Jacketkind = PlayerPrefs.GetString("Busan_Wear_JacktKind");
        //wear_PantsKind = PlayerPrefs.GetString("Busan_Wear_PantsKind");
        //wear_ShoesKind = PlayerPrefs.GetString("Busan_Wear_ShoesKind");
        //wear_HelmetKind = PlayerPrefs.GetString("Busan_Wear_HelmetKind");
        //wear_GlovesKind = PlayerPrefs.GetString("Busan_Wear_GlovesKind");
        //wear_BicycleKind = PlayerPrefs.GetString("Busan_Wear_BicycleKind");

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


    void AllItemActionShow(int _number)
    {
        //invenCtrl_scrip = allEquipmentContents[_number - 1].GetComponent<InventoryContentCtrl>();

        string folderName = invenCtrl_scrip.folder;

        if (PlayerPrefs.GetString("Busan_Player_Sex") == "Woman")
        {
            if (folderName == "Exp")
            {
                id_bicycleNum = invenCtrl_scrip.itemNum;
                id_bicycleStr = invenCtrl_scrip.imgname;
                womanctrl_scrip.BicycleTextrueSetting(id_bicycleStr, id_bicycleNum);
            }
        }
        else if (PlayerPrefs.GetString("Busan_Player_Sex") == "Man")
        {
        }
    }



    public void CustomSettingSave()
    {
        PlayerPrefs.SetString("Busan_Player_Hair", hairStyleStr);
        //장착 아이템 번호 저장
        PlayerPrefs.SetInt("Busan_HairNumber", id_hairNum);
        PlayerPrefs.SetInt("Busan_BodyNumber", id_bodyNum);
        PlayerPrefs.SetInt("Busan_JacketNumber", id_jacketNum);
        PlayerPrefs.SetInt("Busan_PantsNumber", id_pantsNum);
        PlayerPrefs.SetInt("Busan_ShoesNumber", id_shoesNum);
        PlayerPrefs.SetInt("Busan_HelmetNumber", id_helmetNum);
        PlayerPrefs.SetInt("Busan_GlovesNumber", id_glovesNum);
        //PlayerPrefs.SetInt("BicycleNumber", id_bicycleNum);

        //장착 아이템 텍스쳐 저장
        PlayerPrefs.SetString("Busan_Wear_JacketStyleName", id_JacketStr);
        PlayerPrefs.SetString("Busan_Wear_PantsStyleName", id_PantsStr);
        PlayerPrefs.SetString("Busan_Wear_ShoesStyleName", id_ShoesStr);
        PlayerPrefs.SetString("Busan_Wear_HelmetStyleName", id_helmetStr);
        PlayerPrefs.SetString("Busan_Wear_GlovesStyleName", id_glovesStr);
        PlayerPrefs.SetString("Busan_Wear_HairStyleName", id_hairStr);
        //PlayerPrefs.SetString("Wear_BicycleStyleName", id_bicycleStr);


        //장착 아이템 아이디 저장
        PlayerPrefs.SetString("Busan_Wear_GlovesKind", wear_GlovesKind);
        PlayerPrefs.SetString("Busan_Wear_HelmetKind", wear_HelmetKind);
        PlayerPrefs.SetString("Busan_Wear_JacketKind", wear_Jacketkind);
        PlayerPrefs.SetString("Busan_Wear_PantsKind", wear_PantsKind);
        PlayerPrefs.SetString("Busan_Wear_ShoesKind", wear_ShoesKind);
        //PlayerPrefs.SetString("Wear_BicycleKind", wear_bicycleStr);
        // 장착 업데이트
        ServerManager.Instance.BuyItems_Update();
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
