using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class InventoryContentCtrl : MonoBehaviour
{
    public int price;   //가격
    public string id;   //아이템 아이디
    public string style;  //아이템 이름 스타일 ex.꽃무늬 옷, 줄무늬 티
    public int itemNum; //아이템 종류의 인덱스 (ex. 상의 - 나시(0), 반팔(1), 긴팔(2))
    public Image img;
    public string folder;
    public string imgname;
    public bool buyState;   //구매여부
    public int indexNum;    //리스트 순서



    void Start()
    {
        //Separation_StringInt();
    }

    //문자숫자 분리 함수
    void Separation_StringInt()
    {
        string thisName = this.gameObject.name;

        string strTamp = Regex.Replace(thisName, @"\d", "");    //문자 추출
        string intTamp = Regex.Replace(thisName, @"\D", "");    //숫자 추출
        int num = int.Parse(intTamp);
        //Debug.Log(strTamp + " :: " + intTamp);

        if (strTamp == "HairInvenToggle")
        {
            ItemData(num, 1, "Hair");
        }
        else if (strTamp == "ShirtInvenToggle")
        {
            ItemData2(num, 2, "Jacket");
        }
        else if (strTamp == "PantsInvenToggle")
        {
            ItemData2(num, 9, "Pants");
        }
        else if (strTamp == "ShoesInvenToggle")
        {
            ItemData2(num, 16, "Shoes");
        }
        else if (strTamp == "HelmetInvenToggle")
        {
            ItemData2(num, 20, "Helmet");
        }
        else if (strTamp == "GlovesInvenToggle")
        {
            ItemData2(num, 23, "Gloves");
        }
    }

    void ItemData(int _num, int _indexGap, string _folder)
    {
        //Debug.Log((_num - _indexGap));
        if (ItemDataBase.instance.items[(_num - _indexGap)].folderName == _folder)
        {
            if(ItemDataBase.instance.items[(_num - _indexGap)].buyState == true)
            {
                price = ItemDataBase.instance.items[(_num - _indexGap)].price;
                id = ItemDataBase.instance.items[(_num - _indexGap)].itemId;
                style = ItemDataBase.instance.items[(_num - _indexGap)].itemStyle;
                itemNum = ItemDataBase.instance.items[(_num - _indexGap)].itemNum;
                folder = ItemDataBase.instance.items[(_num - _indexGap)].folderName;
                imgname = ItemDataBase.instance.items[(_num - _indexGap)].imgName;
                img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/" + imgname);
                buyState = ItemDataBase.instance.items[(_num - _indexGap)].buyState;
                indexNum = ItemDataBase.instance.items[(_num - _indexGap)].listIndex;
            }
            
        }
    }

    void ItemData2(int _num, int _indexGap, string _folder)
    {
        Debug.Log((_num - _indexGap));
        if (ItemDataBase.instance.items[(_num + _indexGap)].folderName == _folder)
        {
            if (ItemDataBase.instance.items[(_num + _indexGap)].buyState == true)
            {
                price = ItemDataBase.instance.items[(_num + _indexGap)].price;
                id = ItemDataBase.instance.items[(_num + _indexGap)].itemId;
                style = ItemDataBase.instance.items[(_num + _indexGap)].itemStyle;
                itemNum = ItemDataBase.instance.items[(_num + _indexGap)].itemNum;
                folder = ItemDataBase.instance.items[(_num + _indexGap)].folderName;
                imgname = ItemDataBase.instance.items[(_num + _indexGap)].imgName;
                img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/" + imgname);
                buyState = ItemDataBase.instance.items[(_num + _indexGap)].buyState;
                indexNum = ItemDataBase.instance.items[(_num + _indexGap)].listIndex;
            }

        }
    }
}
