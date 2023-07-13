using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//아이템 속성 스크립트 
[System.Serializable]

public class Item 
{
    public string itemId;   //아이템 아이디
    public string itemStyle; //아이템 이름 스타일 ex.꽃무늬 옷, 줄무늬 티
    public int price;   //가격
    public int itemNum; //아이템 종류의 인덱스 (ex. 상의 - 나시(0), 반팔(1), 긴팔(2))
    public string folderName;   //폴더명
    public string imgName;  //이미지 이름
    public Texture2D itemIcon;  //이미지
    public bool buyState;   //구매여부
    public int listIndex;  //리스트 순서


    public Item()
    {

    }

    public Item(string _itemId, string _itemStyle, int _price, int _itemNum, string _folderName ,string _imgName, bool _buyState, int _listIndex)
    {
        itemId = _itemId;
        itemStyle = _itemStyle;
        price = _price;
        itemNum = _itemNum;
        folderName = _folderName;
        imgName = _imgName;
        itemIcon = Resources.Load<Texture2D>("Install Item/" + folderName + "/" + imgName);
        buyState = _buyState;
        listIndex = _listIndex;
    }
}
