using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase instance { get; private set; }


    public List<Item> items = new List<Item>();


    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        // 서버에서 Item 저장
        ServerManager.Instance.GetStoreItemList();

        // items.Add(new Item("ItemID_Hair", "묶음머리", 1000, 0, "Hair", "Hair1", true, 0));
        // 마지막 두개 데이터는 구매여부

        // Item(string _itemId, string _itemStyle, int _price, int _itemNum, string _folderName ,string _imgName, bool _buyState, int _listIndex) // 
        //아이디, 스타일, 가격 ,인텍스, 폴더 이름,이미지이름, 구매여부, 아이템순서
        //머리 아이템 상점(0 ~ 2) / ( 0 ~ 9 )
        //items.Add(new Item("ItemID_Hair", "묶음머리", 1000, 0, "Hair", "Hair1", true, 0));
        //items.Add(new Item("ItemID_Hair", "올림머리", 1000, 1, "Hair", "Hair2", false, 1));
        //items.Add(new Item("ItemID_Hair", "단발머리", 1000, 2, "Hair", "Hair3", false, 2));

        ////상의 아이템 상점 (3 ~ 9 ) / ( 10 ~ 19 )
        //items.Add(new Item("ItemID_Nais", "기본나시", 500, 0, "Jacket", "BasicNasi", true, 3));
        //items.Add(new Item("ItemID_Tshirt", "빨간반팔", 2000, 1, "Jacket", "Tshirt1", false, 4));
        //items.Add(new Item("ItemID_Tshirt", "파란반팔", 2000, 1, "Jacket", "Tshirt2", false, 5));
        //items.Add(new Item("ItemID_Tshirt", "노란반팔", 2000, 1, "Jacket", "Tshirt3", false, 6));
        //items.Add(new Item("ItemID_LongShirt", "빨간긴팔", 3000, 2, "Jacket", "LongShirt1", false, 7));
        //items.Add(new Item("ItemID_LongShirt", "파란긴팔", 3000, 2, "Jacket", "LongShirt2", false, 8));
        //items.Add(new Item("ItemID_LongShirt", "노란긴팔", 3000, 2, "Jacket", "LongShirt3", false, 9));

        ////하의 아이템 상점 ( 10 ~ 16 ) / (20 ~ 29 )
        //items.Add(new Item("ItemID_Short", "기본바지", 500, 0, "Pants", "BasicShort", true, 10));
        //items.Add(new Item("ItemID_Short", "빨간반바지", 2000, 0, "Pants", "Short1", false, 11));
        //items.Add(new Item("ItemID_Short", "파란반바지", 2000, 0, "Pants", "Short2", false, 12));
        //items.Add(new Item("ItemID_Short", "노란반바지", 2000, 0, "Pants", "Short3", false, 13));
        //items.Add(new Item("ItemID_LongPants", "빨간긴바지", 3000, 1, "Pants", "LongPants1", false, 14));
        //items.Add(new Item("ItemID_LongPants", "파란긴바지", 3000, 1, "Pants", "LongPants2", false, 15));
        //items.Add(new Item("ItemID_LongPants", "노란긴바지", 3000, 1, "Pants", "LongPants3", false, 16));

        ////신발 아이템 상점 ( 17 ~ 20 ) / ( 30 ~ 39 )
        //items.Add(new Item("ItemID_Sandal", "기본샌들", 500, 1, "Shoes", "BasicSandal", true, 17));
        //items.Add(new Item("ItemID_Shoes", "빨간신발", 3000, 1, "Shoes", "Shoes1", false, 18));
        //items.Add(new Item("ItemID_Shoes", "파란신발", 3000, 1, "Shoes", "Shoes2", false, 19));
        //items.Add(new Item("ItemID_Shoes", "노란신발", 3000, 1, "Shoes", "Shoes3", false, 20));

        ////헬맷 아이템 상점 ( 21 ~ 23 ) / ( 40 ~ 49 )
        //items.Add(new Item("ItemID_Helmet", "빨간헬맷", 3000, 0, "Helmet", "Helmet1", false, 21));
        //items.Add(new Item("ItemID_Helmet", "파란헬맷", 3000, 0, "Helmet", "Helmet2", false, 22));
        //items.Add(new Item("ItemID_Helmet", "노란헬맷", 3000, 0, "Helmet", "Helmet3", false, 23));

        ////장갑 아이템 상점 ( 24 ~ 26 ) / ( 50 ~ 59 )
        //items.Add(new Item("ItemID_Gloves", "빨간장갑", 3000, 0, "Gloves", "Gloves1", false, 24));
        //items.Add(new Item("ItemID_Gloves", "파란장갑", 3000, 0, "Gloves", "Gloves2", false, 25));
        //items.Add(new Item("ItemID_Gloves", "노란장갑", 3000, 0, "Gloves", "Gloves3", false, 26));

        //items.Add(new Item("ItemID_Bicycle", "기본자전거", 3000, 0, "Bicycle", "BasicBicycle", true, 27));
        //items.Add(new Item("ItemID_Bicycle", "파란자전거", 10000, 0, "Bicycle", "Bicycle1", false, 28));
        //items.Add(new Item("ItemID_Bicycle", "하얀자전거", 10000, 0, "Bicycle", "Bicycle2", false, 29));
        //items.Add(new Item("ItemID_Bicycle", "노란자전거", 10000, 0, "Bicycle", "Bicycle3", false, 30));

        //items.Add(new Item("ItemID_Item", "경험치 x 2", 1000, 0, "Exp", "ExpUp", false, 31));
        //items.Add(new Item("ItemID_Item", "경험치1000EXP", 10000, 0, "Exp", "ExpPlus", false, 32));
        //items.Add(new Item("ItemID_Item", "코인 x 2", 500, 0, "Coin", "CoinUp", false, 33));
        //items.Add(new Item("ItemID_Item", "스피드 x 0.5", 3000, 0, "Speed", "SpeedUp", false, 34));


    }

}
