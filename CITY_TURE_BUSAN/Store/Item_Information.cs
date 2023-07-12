using Cysharp.Threading.Tasks;
using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Item_Information : MonoBehaviour {
    public int price;   //가격
    public string id;   //아이템 아이디
    public string style;  //아이템 이름 스타일 ex.꽃무늬 옷, 줄무늬 티
    public int itemNum; //아이템 종류의 인덱스 (ex. 상의 - 나시(0), 반팔(1), 긴팔(2))
    public Image img;
    public Text priceText;
    public Text nameText;
    public string folder;
    public string imgname;
    public bool buyState;   //구매여부
    public int indexNum;    //리스트 순서

    // 서버에 아이템 들어가는거로 다 작업
    private void OnEnable() {
        //SetupPopupCtrl.OnChangedLocalized += SetupPopupCtrl_OnChangedLocalized;
        SetupPopupCtrl.OnChangedLocalizedAsync += SetupPopupCtrl_OnChangedLocalizedAsync;
    }

    private UniTask SetupPopupCtrl_OnChangedLocalizedAsync(object sender, EventArgs e) {
        Separation_StringInt().Forget();
        return UniTask.NextFrame();
    }

    private void OnDisable() {
        //SetupPopupCtrl.OnChangedLocalized -= SetupPopupCtrl_OnChangedLocalized;
        SetupPopupCtrl.OnChangedLocalizedAsync -= SetupPopupCtrl_OnChangedLocalizedAsync;
    }

    private void OnDestroy() {        
        //SetupPopupCtrl.OnChangedLocalized -= SetupPopupCtrl_OnChangedLocalized;
        SetupPopupCtrl.OnChangedLocalizedAsync -= SetupPopupCtrl_OnChangedLocalizedAsync;
    }

    //private void SetupPopupCtrl_OnChangedLocalized(object sender, EventArgs e) {
    //    //await Separation_StringInt();
    //    Separation_StringInt().Forget();
    //}

    private void Start() {
        Separation_StringInt().Forget();
    }

    //문자숫자 분리 함수
    async UniTask Separation_StringInt() {
        string thisName = this.gameObject.name;

        string strTemp = Regex.Replace(thisName, @"\d", "");    //문자 추출
        string intTemp = Regex.Replace(thisName, @"\D", "");    //숫자 추출
        int num = int.Parse(intTemp);

        await UniTask.Delay(100);

        if (strTemp == "ExpStoreToggle") {
            ItemData2(num, 31);
        } else if (strTemp == "CoinStoreToggle") {
            ItemData2(num, 33);
        } else if (strTemp == "SpeedStoreToggle") {
            ItemData2(num, 34);
        }
    }

    private void ItemData2(int _num, int _indexGap) {
        price = ItemDataBase.instance.items[(_num + _indexGap)].price;
        id = ItemDataBase.instance.items[(_num + _indexGap)].itemId;
        style = ItemDataBase.instance.items[(_num + _indexGap)].itemStyle;
        itemNum = ItemDataBase.instance.items[(_num + _indexGap)].itemNum;
        folder = ItemDataBase.instance.items[(_num + _indexGap)].folderName;
        imgname = ItemDataBase.instance.items[(_num + _indexGap)].imgName;

        if (folder == "Exp" || folder == "Coin" || folder == "Speed")
            img.sprite = Resources.Load<Sprite>("Install Item/Item/" + folder + "/" + imgname);

        buyState = ItemDataBase.instance.items[(_num + _indexGap)].buyState;
        indexNum = ItemDataBase.instance.items[(_num + _indexGap)].listIndex;

        if (buyState) {
            GameObject test = this.transform.GetChild(1).gameObject;

            if (imgname == "ExpPlus" || imgname == "ExpUp" || imgname == "CoinUp" || imgname == "SpeedUp") {
                this.gameObject.GetComponent<Toggle>().interactable = true;
                test.SetActive(false);
            } else {
                this.gameObject.GetComponent<Toggle>().interactable = false;
                test.SetActive(true);
            }
        }

        nameText.text = style;

        Debug.Log("YAPYAP nameText.text : " + nameText.text);

        priceText.text = CommaText(price);
    }

    //숫자 콤마 찍는 함수
    public string CommaText(int _data) {
        if (_data != 0)
            return string.Format("{0:#,###}", _data);
        else
            return "0";
    }
}

//using System.Collections;
//using System.Collections.Generic;z
//using System.Text.RegularExpressions;
//using UnityEngine;
//using UnityEngine.UI;

//public class Item_Information : MonoBehaviour
//{
//    public int price;   //가격
//    public string id;   //아이템 아이디
//    public string style;  //아이템 이름 스타일 ex.꽃무늬 옷, 줄무늬 티
//    public int itemNum; //아이템 종류의 인덱스 (ex. 상의 - 나시(0), 반팔(1), 긴팔(2))
//    public Image img;
//    public Text priceText;
//    public Text nameText;
//    public string folder;
//    public string imgname;
//    public bool buyState;   //구매여부
//    public int indexNum;    //리스트 순서

//    // 서버에 아이템 들어가는거로 다 작업

//    void Start()
//    {
//        Separation_StringInt();
//        //price = ItemDataBase.instance.items[0].price;
//        //Debug.Log("흠 " + ItemDataBase.instance.items[0].price);

//    }

//    //문자숫자 분리 함수
//    void Separation_StringInt()
//    {
//        string thisName = this.gameObject.name;

//        string strTamp = Regex.Replace(thisName, @"\d", "");    //문자 추출
//        string intTamp = Regex.Replace(thisName, @"\D", "");    //숫자 추출
//        int num = int.Parse(intTamp);
//        //Debug.Log(strTamp + " :: " + intTamp);

//        if(strTamp == "HairStoreToggle")
//        {
//            ItemData(num, 1);
//        }
//        else if(strTamp == "ShirtStoreToggle")
//        {
//            ItemData2(num, 3);
//        }
//        else if (strTamp == "PantsStoreToggle")
//        {
//            ItemData2(num, 10);
//        }
//        else if (strTamp == "ShoesStoreToggle")
//        {
//            ItemData2(num, 17);
//        }
//        else if (strTamp == "HelmetStoreToggle")
//        {
//            ItemData2(num, 21);
//        }
//        else if (strTamp == "GlovesStoreToggle")
//        {
//            ItemData2(num, 24);
//        }
//        else if(strTamp == "BicycleStoreToggle")
//        {
//            ItemData2(num, 27);
//        }
//        else if(strTamp == "ExpStoreToggle")
//        {
//            ItemData2(num, 31);
//        }
//        else if (strTamp == "CoinStoreToggle")
//        {
//            ItemData2(num, 33);
//        }
//        else if (strTamp == "SpeedStoreToggle")
//        {
//            ItemData2(num, 34);
//        }
//    }

//    void ItemData(int _num, int _indexGap)
//    {
//        //Debug.Log((_num - _indexGap));
//        price = ItemDataBase.instance.items[(_num - _indexGap)].price;
//        id = ItemDataBase.instance.items[(_num - _indexGap)].itemId;
//        style = ItemDataBase.instance.items[(_num - _indexGap)].itemStyle;
//        itemNum = ItemDataBase.instance.items[(_num - _indexGap)].itemNum;
//        folder = ItemDataBase.instance.items[(_num - _indexGap)].folderName;
//        imgname = ItemDataBase.instance.items[(_num - _indexGap)].imgName;

//        if (PlayerPrefs.GetString("Busan_Player_Sex") == "Woman")
//        {
//            if(folder == "Hair")
//                img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/Woman/" + imgname);
//            else if (folder == "Jacket")
//                if (imgname == "BasicNasi")
//                    img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/Woman/" + imgname);
//                else
//                    img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/" + imgname);
//            else
//            {
//                if (folder == "Exp" || folder == "Coin" || folder == "Speed")
//                    img.sprite = Resources.Load<Sprite>("Install Item/Item/" + folder + "/" + imgname);
//                else
//                    img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/" + imgname);
//            }
//        } 
//        else if (PlayerPrefs.GetString("Busan_Player_Sex") == "Man")
//        {
//            if (folder == "Hair")
//                img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/Man/" + imgname);
//            else if (folder == "Jacket")
//                if (imgname == "BasicNasi")
//                    img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/Man/" + imgname);
//                else
//                    img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/" + imgname);
//            else
//            {
//                if (folder == "Exp" || folder == "Coin" || folder == "Speed")
//                    img.sprite = Resources.Load<Sprite>("Install Item/Item/" + folder + "/" + imgname);
//                else
//                    img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/" + imgname);
//            }
//        }


//        buyState = ItemDataBase.instance.items[(_num - _indexGap)].buyState;
//        indexNum = ItemDataBase.instance.items[(_num - _indexGap)].listIndex;

//        if (buyState)
//        {
//            GameObject test = this.transform.GetChild(1).gameObject;

//            if (imgname == "ExpPlus" || imgname == "ExpUp" || imgname == "CoinUp" || imgname == "SpeedUp")
//            {
//                this.gameObject.GetComponent<Toggle>().interactable = true;
//                test.SetActive(false);
//            }
//            else
//            {
//                this.gameObject.GetComponent<Toggle>().interactable = false;
//                test.SetActive(true);
//            }
//        }

//        if(style != "헬멧헤어")
//        {
//            nameText.text = style;
//            priceText.text = CommaText(price); //price.ToString();
//        }
//    }

//    void ItemData2(int _num, int _indexGap)
//    {
//        //Debug.Log(_num + " _indexGap " + (_num + _indexGap));
//        price = ItemDataBase.instance.items[(_num + _indexGap)].price;
//        id = ItemDataBase.instance.items[(_num + _indexGap)].itemId;
//        style = ItemDataBase.instance.items[(_num + _indexGap)].itemStyle;
//        itemNum = ItemDataBase.instance.items[(_num + _indexGap)].itemNum;
//        folder = ItemDataBase.instance.items[(_num + _indexGap)].folderName;
//        imgname = ItemDataBase.instance.items[(_num + _indexGap)].imgName;
//        if (PlayerPrefs.GetString("Busan_Player_Sex") == "Woman")
//        {
//            if (folder == "Hair")
//                img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/Woman/" + imgname);
//            else if(folder == "Jacket")
//                if(imgname == "BasicNasi")
//                    img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/Woman/" + imgname);
//                else
//                    img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/" + imgname);
//            else
//            {
//                if(folder == "Exp" || folder == "Coin" || folder == "Speed")
//                    img.sprite = Resources.Load<Sprite>("Install Item/Item/" + folder + "/" + imgname);
//                else
//                    img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/" + imgname);
//            }
//        }
//        else if (PlayerPrefs.GetString("Busan_Player_Sex") == "Man")
//        {
//            if (folder == "Hair")
//                img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/Man/" + imgname);
//            else if (folder == "Jacket")
//                if (imgname == "BasicNasi")
//                    img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/Man/" + imgname);
//                else
//                    img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/" + imgname);
//            else
//            {
//                //Debug.Log("folder " + folder);
//                if (folder == "Exp" || folder == "Coin" || folder == "Speed")
//                    img.sprite = Resources.Load<Sprite>("Install Item/Item/" + folder + "/" + imgname);
//                else
//                    img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/" + imgname);
//            }
//        }


//        buyState = ItemDataBase.instance.items[(_num + _indexGap)].buyState;
//        indexNum = ItemDataBase.instance.items[(_num + _indexGap)].listIndex;

//        if (buyState)
//        {
//            GameObject test = this.transform.GetChild(1).gameObject;

//            if (imgname == "ExpPlus" || imgname == "ExpUp" || imgname == "CoinUp" || imgname == "SpeedUp")
//            {
//                this.gameObject.GetComponent<Toggle>().interactable = true;
//                test.SetActive(false);
//            }
//            else
//            {
//                this.gameObject.GetComponent<Toggle>().interactable = false;
//                test.SetActive(true);
//            }
//        }

//        nameText.text = style;
//        priceText.text = CommaText(price);// price.ToString();
//    }

//    //숫자 콤마 찍는 함수
//    public string CommaText(int _data)
//    {
//        if (_data != 0)
//            return string.Format("{0:#,###}", _data);
//        else
//            return "0";
//    }
//}
