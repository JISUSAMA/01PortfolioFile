using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StoreBuyPopupCtrl : MonoBehaviour
{
    public GameObject buyPopup; //구매팝업
    public GameObject noticPopup;   //코인 부족 알림팝업
    public Button buyBtn;   //구매하기버튼
    public Text noticText;  //코인부족 텍스트
    public Text titleText;  //상품 샀을 때 팝업 제목
    public Text myGold; //내가 가지고 있는 코인
    public Text priceText;  //상품 가격
    public Image itemImg;   //선택한 상품 이미지

    int price_i, myGold_i;  //상품 가격, 내 코인
    string folderName_s;    //상품이미지가 잇는 폴더 이름
    string imgName_s;   //상품이미지 이름



    void Start()
    {
        
        Initialization();
    }

    public void Initialization()
    {
        titleText.text = PlayerPrefs.GetString("AT_StoreBuyTitle") + " 스타일";
        myGold_i = int.Parse(myGold.text.Replace(",",""));
        price_i = PlayerPrefs.GetInt("AT_StoreBuy_Price");
        folderName_s = PlayerPrefs.GetString("AT_StoreBuy_FolderName");
        imgName_s = PlayerPrefs.GetString("AT_StoreBuy_ImageName");
        priceText.text = string.Format("{0:#,###}", price_i);   //가격 콤마 변환

        //Debug.Log("장갑 : " + imgName_s);

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            RectTransform rectTran = itemImg.GetComponent<RectTransform>();
            if (folderName_s == "Hair")
            {
                rectTran.sizeDelta = new Vector2(199, 237);
                itemImg.sprite = Resources.Load<Sprite>("Install Item/" + folderName_s + "/Woman/" + imgName_s);
            }
            else if (folderName_s == "Jacket")
            {
                rectTran.sizeDelta = new Vector2(482, 347);
                if (imgName_s == "BasicNasi")
                    itemImg.sprite = Resources.Load<Sprite>("Install Item/" + folderName_s + "/Woman/" + imgName_s);
                else
                    itemImg.sprite = Resources.Load<Sprite>("Install Item/" + folderName_s + "/" + imgName_s);
            } 
            else
            {
                if (folderName_s == "Exp" || folderName_s == "Coin" || folderName_s == "Speed")
                {
                    rectTran.sizeDelta = new Vector2(198, 275);
                    itemImg.sprite = Resources.Load<Sprite>("Install Item/Item/" + folderName_s + "/" + imgName_s);
                }  
                else
                {
                    rectTran.sizeDelta = new Vector2(482, 347);
                    itemImg.sprite = Resources.Load<Sprite>("Install Item/" + folderName_s + "/" + imgName_s);
                }
            } 
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            RectTransform rectTran = itemImg.GetComponent<RectTransform>();
            

            if (folderName_s == "Hair")
            {
                rectTran.sizeDelta = new Vector2(294, 347);
                itemImg.sprite = Resources.Load<Sprite>("Install Item/" + folderName_s + "/Man/" + imgName_s);
            }
            else if (folderName_s == "Jacket")
            {
                rectTran.sizeDelta = new Vector2(482, 347);
                if (imgName_s == "BasicNasi")
                    itemImg.sprite = Resources.Load<Sprite>("Install Item/" + folderName_s + "/Man/" + imgName_s);
                else
                    itemImg.sprite = Resources.Load<Sprite>("Install Item/" + folderName_s + "/" + imgName_s);
            } 
            else
            {
                if (folderName_s == "Exp" || folderName_s == "Coin" || folderName_s == "Speed")
                {
                    rectTran.sizeDelta = new Vector2(198, 275);
                    itemImg.sprite = Resources.Load<Sprite>("Install Item/Item/" + folderName_s + "/" + imgName_s);
                }
                else
                {
                    rectTran.sizeDelta = new Vector2(482, 347);
                    itemImg.sprite = Resources.Load<Sprite>("Install Item/" + folderName_s + "/" + imgName_s);
                }
                   
            }
                
        }
    }


    public void BuySaveButtonOn()
    {
        //Debug.Log("구매 상점이 열렸습니당");
        //구매가격이 현재 보유 금액보다 작거나 같아야
        if (price_i <= myGold_i)
        {
            //Debug.Log("구매한다구요 ?!?!?!?");
            myGold_i = myGold_i - price_i;  //가격 계산

            if(myGold_i != 0)
            {
                myGold.text = string.Format("{0:#,###}", myGold_i);  //가격 텍스트에 뿌리기
                PlayerPrefs.SetInt("AT_Player_Gold", myGold_i);
            }
            else if(myGold_i.Equals(0))
            {
                myGold.text = "0";  //가격 텍스트에 뿌리기
                PlayerPrefs.SetInt("AT_Player_Gold", 0);
            }

            if (imgName_s == "ExpUp")
            {
                int amount = PlayerPrefs.GetInt("AT_ExpUpAmount");
                amount += 1;
                PlayerPrefs.SetInt("AT_ExpUpAmount", amount);  //갯수를 추가해준다.

                if (PlayerPrefs.GetString("AT_ItemPurchase").Equals("MissionOk"))
                    PlayerPrefs.SetString("AT_ItemPurchase", "MissionOk");   //아이템 구매여부
                else
                    PlayerPrefs.SetString("AT_ItemPurchase", "Yes");   //아이템 구매여부
            }
            else if(imgName_s == "ExpPlus")
            {
                //int amount = PlayerPrefs.GetInt("ExpPlusAmount");
                //amount += 1;
                //PlayerPrefs.SetInt("ExpPlusAmount", amount);  //갯수를 추가해준다.
                Level_Exp_Up(200);  //경험치 올리기

                if (PlayerPrefs.GetString("AT_ItemPurchase").Equals("MissionOk"))
                    PlayerPrefs.SetString("AT_ItemPurchase", "MissionOk");   //아이템 구매여부
                else
                    PlayerPrefs.SetString("AT_ItemPurchase", "Yes");   //아이템 구매여부
            }
            else if (imgName_s == "CoinUp")
            {
                int amount = PlayerPrefs.GetInt("AT_CoinUpAmount");
                amount += 1;
                PlayerPrefs.SetInt("AT_CoinUpAmount", amount);  //갯수를 추가해준다.

                if (PlayerPrefs.GetString("AT_ItemPurchase").Equals("MissionOk"))
                    PlayerPrefs.SetString("AT_ItemPurchase", "MissionOk");   //아이템 구매여부
                else
                    PlayerPrefs.SetString("AT_ItemPurchase", "Yes");   //아이템 구매여부
            }
            else if (imgName_s == "SpeedUp")
            {
                int amount = PlayerPrefs.GetInt("AT_SpeedUpAmount");
                amount += 1;
                PlayerPrefs.SetInt("AT_SpeedUpAmount", amount);  //갯수를 추가해준다.

                if (PlayerPrefs.GetString("AT_ItemPurchase").Equals("MissionOk"))
                    PlayerPrefs.SetString("AT_ItemPurchase", "MissionOk");   //아이템 구매여부
                else
                    PlayerPrefs.SetString("AT_ItemPurchase", "Yes");   //아이템 구매여부
            }

            //if (imgName_s != ("ExpUp") || imgName_s != ("ExpPlus") || imgName_s != ("CoinUp") ||
            //    imgName_s != ("SpeedUp"))
            //    PlayerPrefs.SetString("CustomChange", "Yes");   //아이템 구매가 아닌 것은 입혀져서.

            if(PlayerPrefs.GetString("AT_GoldUse").Equals("MissionOk"))
                PlayerPrefs.SetString("AT_GoldUse", "MissionOk");    //코인 사용 여부
            else
                PlayerPrefs.SetString("AT_GoldUse", "Yes");    //코인 사용 여부
            Store_DataManager.instance.BuyItem_Interactable(imgName_s);

            // 아이템 구매 / 경험치 사는거 빼고
            if (imgName_s != "ExpPlus")
                ServerManager.Instance.PurchasingItems(imgName_s);

            StartCoroutine(_BuyItems_Update());
        }
        else
        {
            //Debug.Log("돈이 모잘라요!");
            buyPopup.SetActive(false);
            noticPopup.SetActive(true);
            noticText.text = "코인이 부족합니다." + "\n" + "코인을 획득하신 후 다시 사용하시길 바랍니다.";
        }
        buyBtn.interactable = false;
    }

    IEnumerator _BuyItems_Update()
    {
        ServerManager.Instance.BuyItems_Update();

        yield return new WaitUntil(() => ServerManager.Instance.isBuyItemUpdateCompleted);

        ServerManager.Instance.isBuyItemUpdateCompleted = false;

        buyPopup.SetActive(false);
    }

    //최종 경험치 더하고, 레벨업함.
    public void Level_Exp_Up(int _takeExp)
    {
        int level = PlayerPrefs.GetInt("AT_Player_Level");
        int takeExp = _takeExp;
        int currExp = PlayerPrefs.GetInt("AT_Player_CurrExp");
        int maxExp = level * (level + 1) * 25;
        int remainExp = Mathf.Abs(maxExp - currExp);    //남은 경험치

        //Debug.Log("currExp " + currExp + " 경험치 " + takeExp + " level " + level);

        //획득한 경험치가 있을때
        while (takeExp != 0)
        {
            //획득한 경험치가 남은 경험치보다 작은 경우
            if (takeExp < remainExp)
            {
                maxExp = (level * (level + 1) * 25);
                level = level;  //레벨 그대로
                currExp += takeExp; //현재경험치 + 획득한 경험치
                remainExp -= takeExp;   //남은경험치 - 획득한 경험치
                takeExp = 0;    //획득한 경험치 초기화
            }
            //획득한 경험치와 남은 경험치가 동일한 경우
            else if (takeExp == remainExp)
            {
                level += 1; //레벨 1상승
                maxExp = (level * (level + 1) * 25);
                currExp = 0; //현재 경험치 0(레벨이 올라서)
                remainExp = 0;  //남은 경험치 0(레벨이 올라서)
                takeExp = 0;    //획득한 경험치 초기화
            }
            //획득한 경험치가 남은 경험치보다 큰 경우
            else if (takeExp > remainExp)
            {
                level += 1; //레벨 1상승
                takeExp -= remainExp;   //획득한 경험치 - 남은 경험치(레벨업 되기 전 남아있는)
                currExp = 0;    //레벨업이 되어서 현재 경험치는 0
                maxExp = level * (level + 1) * 25;
                remainExp = maxExp; //레벨업이 되어서 남은 경험치도 풀 초기화
            }
        }

        PlayerPrefs.SetInt("AT_Player_Level", level);
        PlayerPrefs.SetInt("AT_Player_TakeExp", takeExp);
        PlayerPrefs.SetInt("AT_Player_CurrExp", currExp);

        // 서버에 저장
        ServerManager.Instance.Update_Level_Data(level, currExp, takeExp);

        //Debug.Log("level " + level + " currExp " + currExp + " totalExpText " + (level * (level + 1) * 25).ToString());
    }
}
