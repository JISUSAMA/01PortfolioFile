using Cysharp.Threading.Tasks;
using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class InventoryContentCtrl : MonoBehaviour {

    [ContextMenu("AssignmentItemText")]
    public void AssignmentItemText() {
        Transform[] allChildren = GetComponentsInChildren<Transform>();

        foreach (Transform child in allChildren) {

            if (child.name == transform.name) {
                continue;
            } else if (child.name == "Text") {
                itemStyle = child.GetComponent<Text>();
            } else if (child.name == "AmountText") {
                amount = child.GetComponent<Text>();
            }
        }
    }

    [Header("View")]
    public Text itemStyle;
    public Text amount;

    [Space(10)]
    [Header("Data")]
    public int price;   //가격
    public string id;   //아이템 아이디
    public string style;  //아이템 이름 스타일 ex.꽃무늬 옷, 줄무늬 티
    public int itemNum; //아이템 종류의 인덱스 (ex. 상의 - 나시(0), 반팔(1), 긴팔(2))
    public Image img;
    public string folder;
    public string imgname;
    public bool buyState;   //구매여부
    public int indexNum;    //리스트 순서

    private void OnEnable() {
        //SetupPopupCtrl.OnChangedLocalized += SetupPopupCtrl_OnChangedLocalized;
        SetupPopupCtrl.OnChangedLocalizedAsync += SetupPopupCtrl_OnChangedLocalizedAsync;        
        UpdateString();
    }

    private UniTask SetupPopupCtrl_OnChangedLocalizedAsync(object sender, EventArgs e) {
        Debug.Log("SetupPopupCtrl_OnChangedLocalizedAsync");
        UpdateString();
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

    //private void SetupPopupCtrl_OnChangedLocalized(object sender, System.EventArgs e) {
    //    // 모든 텍스트 다 바꾸기
    //    UpdateString();
    //}

    private void UpdateString() {
        string thisName = this.gameObject.name;

        string getToggleName = Regex.Replace(thisName, @"\d", "");    //문자 추출
        string getToggleNumber = Regex.Replace(thisName, @"\D", "");    //숫자 추출
        int i_getToggleNumber = int.Parse(getToggleNumber);   // 해당 하이어라키상의 숫자를 들고옴. Toggle 의 수만큼..

        if (getToggleName == "ItemToggle") {
            UpdateInventoryString(32, 35, i_getToggleNumber);
        } else if (getToggleName == "ExpInvenToggle") {
            UpdateInventoryString(32, 32, i_getToggleNumber);   // ExpPlus 는 안보임 바로 사용됨 그래서 33 사용안함.
        } else if (getToggleName == "CoinInvenToggle") {
            UpdateInventoryString(34, 34, i_getToggleNumber);
        } else if (getToggleName == "SpeedInvenToggle") {
            UpdateInventoryString(35, 35, i_getToggleNumber);
        }
    }

    private void UpdateInventoryString(int min, int max, int toggleNumber) {
        int baughtItemOrder = 1;
        for (int i = min; i <= max; i++) {
            if (ItemDataBase.instance.items[i].buyState == true) {
                if (toggleNumber == baughtItemOrder) {

                    price = ItemDataBase.instance.items[i].price;
                    id = ItemDataBase.instance.items[i].itemId;
                    style = ItemDataBase.instance.items[i].itemStyle;
                    itemNum = ItemDataBase.instance.items[i].itemNum;
                    folder = ItemDataBase.instance.items[i].folderName;
                    imgname = ItemDataBase.instance.items[i].imgName;                    
                    buyState = ItemDataBase.instance.items[i].buyState;
                    indexNum = ItemDataBase.instance.items[i].listIndex;

                    itemStyle.text = style; // Localized Style Text

                    if (folder == "Exp") {
                        //if (imgname == "ExpPlus")
                        //    amount.text = PlayerPrefs.GetInt("Busan_ExpPlusAmount").ToString();
                        //else if (imgname == "ExpUp")
                        //    amount.text = PlayerPrefs.GetInt("Busan_ExpUpAmount").ToString();

                        if (imgname == "ExpUp") {
                            amount.text = PlayerPrefs.GetInt("Busan_ExpUpAmount").ToString();
                        }                        
                        img.sprite = Resources.Load<Sprite>("Install Item/Item/" + folder + "/" + imgname);

                    } else if (folder == "Coin") {
                        amount.text = PlayerPrefs.GetInt("Busan_CoinUpAmount").ToString();
                        img.sprite = Resources.Load<Sprite>("Install Item/Item/" + folder + "/" + imgname);
                    } else if (folder == "Speed") {
                        amount.text = PlayerPrefs.GetInt("Busan_SpeedUpAmount").ToString();
                        img.sprite = Resources.Load<Sprite>("Install Item/Item/" + folder + "/" + imgname);
                    } else {
                        img.sprite = Resources.Load<Sprite>("Install Item/" + folder + "/" + imgname);
                    }
                }

                ++baughtItemOrder;
            }
        }
    }
}
