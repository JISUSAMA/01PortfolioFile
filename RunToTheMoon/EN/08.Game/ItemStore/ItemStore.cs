using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemStore : MonoBehaviour
{
    public Text coin_Text;  //코인
    public Text amount_Text;    //갯수
    public Text itemName_Text;  //아이템 이름
    public Text itemConect_Text;    //아이템 내용
    public Text itemPrice_Text;    //아이템 가격

    public Text smallAmount_Text;   //소형아이템 갯수
    public Text bigAmount_Text; //대형아이템 갯수
    public Text energyAmount_Text;  //에너지아이템 갯수
    //public Text StoreSignPopup_Text;

    public GameObject noCoinPopup;  //코인 부족 팝업
    public GameObject buyPanel; //구매 팝업
   // public GameObject StoreSignPopup;

    public Image ItemImage;
    public Sprite[] ItemOb_img;


    string itemKind;  //아이템 종류
    int coin_i; //코인
    int itemAmount_i = 1;   //아이템 갯수
    int smallAmount_i = 0, bigAmount_i = 0, energyAmount_i = 0; //아이템들 갯수
    int smallPrice_i = 10000, bigPrice_i =15000, energyPrice_i =20000;
    private void Start()
    {
        ItemAmountLook();
    }
    private void OnEnable()
    {
        Game_UIManager.instance.BlurCameraCanvas_ob.SetActive(false);
    }
    //아이템 갯수 보여주는 함수
    void ItemAmountLook()
    {
        coin_i = PlayerPrefs.GetInt("Player_Coin");
       // coin_i = 100000;
        smallAmount_i = PlayerPrefs.GetInt("Item_SmallAirTank");
        bigAmount_i = PlayerPrefs.GetInt("Item_BigAirTank");
        energyAmount_i = PlayerPrefs.GetInt("Item_EnergyDrink");

        coin_Text.text = string.Format("{0:#,###}", coin_i);
        smallAmount_Text.text = smallAmount_i.ToString();
        bigAmount_Text.text = bigAmount_i.ToString();
        energyAmount_Text.text = energyAmount_i.ToString();
    }

    //소형산소통 구매 버튼 
    public void SmallItemButtonOn()
    {
        SoundFunction.Instance.ButtonClick_Sound();
        ItemImage.sprite = ItemOb_img[0];
        itemKind = "Small";
        itemAmount_i = 1;   //1로 초기화
        amount_Text.text = itemAmount_i.ToString(); //텍스트에 뿌려줌
        itemPrice_Text.text = (smallPrice_i * itemAmount_i).ToString();
        itemName_Text.text = "Small oxygen tank";
        itemConect_Text.text = "Can be recharged to grant 10 minutes of oxygen.";
    }
    //대형산소통 구매 버튼
    public void BigItemButtonOn()
    {
        SoundFunction.Instance.ButtonClick_Sound();
        ItemImage.sprite = ItemOb_img[1];
        itemKind = "Big";
        itemAmount_i = 1;
        amount_Text.text = itemAmount_i.ToString();
        itemPrice_Text.text = (bigPrice_i * itemAmount_i).ToString();
        itemName_Text.text = "Large oxygen tank";
        itemConect_Text.text = "Can be recharged to grant 20 minutes of oxygen.";
    }
    //에너지바 구매버튼
    public void EnergyItemButtonOn()
    {
        SoundFunction.Instance.ButtonClick_Sound();
        ItemImage.sprite = ItemOb_img[2];
        itemKind = "Energy";
        itemAmount_i = 1;   //갯수 초기화
        amount_Text.text = itemAmount_i.ToString(); //텍스트 뿌리기
        itemPrice_Text.text = (energyPrice_i * itemAmount_i).ToString();
        itemName_Text.text = "Stardust";
        itemConect_Text.text = "Doubles the walking speed for 10 minutes.";
    }

    //갯수 플러스 눌렀을 때
    public void PlusButtonOn()
    {
        SoundFunction.Instance.ButtonClick_Sound();
        itemAmount_i++;
        amount_Text.text = itemAmount_i.ToString();
        if (itemKind.Equals("Small"))
            itemPrice_Text.text = (smallPrice_i * itemAmount_i).ToString();
        else if (itemKind.Equals("Big"))
            itemPrice_Text.text = (bigPrice_i * itemAmount_i).ToString();
        else if(itemKind.Equals("Energy"))
        itemPrice_Text.text = (energyPrice_i * itemAmount_i).ToString();
    }

    //갯수 마이너스 눌렀을 때
    public void SubButtonOn()
    {
        SoundFunction.Instance.ButtonClick_Sound();
        if (itemAmount_i > 0)
            itemAmount_i--;

        amount_Text.text = itemAmount_i.ToString();
        if (itemKind.Equals("Small"))
            itemPrice_Text.text = (smallPrice_i * itemAmount_i).ToString();
        else if (itemKind.Equals("Big"))
            itemPrice_Text.text = (bigPrice_i * itemAmount_i).ToString();
        else if (itemKind.Equals("Energy"))
            itemPrice_Text.text = (energyPrice_i * itemAmount_i).ToString();
    }

    //구매 버튼 이벤트
    public void BuyItemButtonOn()
    {
        int itemCount = 0;  //아이템 갯수
        int price = 0;  //가격
        if(itemKind == "Small")
        {
            price = itemAmount_i * smallPrice_i; //가격 측정

            //구매액이 보유액보다 작거나 같으면
            if(coin_i >= price)
            {
                itemCount = PlayerPrefs.GetInt("Item_SmallAirTank");
                itemCount += itemAmount_i;
                PlayerPrefs.SetInt("Item_SmallAirTank", itemCount);    //아이템 갯수 갱신
                coin_i -= price;    //코인 차감
                PlayerPrefs.SetInt("Player_Coin", coin_i);  //코인 갱신

                ItemAmountLook();//데이터 뿌리기
                buyPanel.SetActive(false);//구매팝업 비활성화
                Game_UIManager.instance.BlurCameraCanvas_ob.SetActive(false);
                Game_UIManager.instance.StoreSignPopup.SetActive(true);
                Game_UIManager.instance.StoreSignPopup_Text.text = "Purchase has been completed.";
                SoundFunction.Instance.ItemBuy_Sound();
                // 서버에 저장
                ServerManager.Instance.Update_Item(PlayerPrefs.GetString("Player_ID"), itemKind, itemCount);
                ServerManager.Instance.Update_Coin("-", price);
            }
            else
            {
                Game_UIManager.instance.StoreSignPopup.SetActive(true);
                Game_UIManager.instance.StoreSignPopup_Text.text = "Not enough coins.";
                SoundFunction.Instance.Warning_Sound();
            }
        }
        else if(itemKind == "Big")
        {
            price = itemAmount_i * bigPrice_i;

            //구매액이 보유액보다 작거나 같으면
            if (coin_i >= price)
            {
                itemCount = PlayerPrefs.GetInt("Item_BigAirTank");
                itemCount += itemAmount_i;
                PlayerPrefs.SetInt("Item_BigAirTank", itemCount);
                coin_i -= price;    //코인 차감
                PlayerPrefs.SetInt("Player_Coin", coin_i);  //코인 갱신

                ItemAmountLook();
                buyPanel.SetActive(false);//구매팝업 비활성화
                Game_UIManager.instance.BlurCameraCanvas_ob.SetActive(false);
                Game_UIManager.instance.StoreSignPopup.SetActive(true);
                Game_UIManager.instance.StoreSignPopup_Text.text = " Purchase has been completed.";
                SoundFunction.Instance.ItemBuy_Sound();
                // 서버에 저장
                ServerManager.Instance.Update_Item(PlayerPrefs.GetString("Player_ID"), itemKind, itemCount);
                ServerManager.Instance.Update_Coin("-", price);
            }
            else
            {
                SoundFunction.Instance.Warning_Sound();
                Game_UIManager.instance.StoreSignPopup.SetActive(true);
                Game_UIManager.instance.StoreSignPopup_Text.text = "Not enough coins.";
            }

        }
        else if (itemKind == "Energy")
        {
            price = itemAmount_i * energyPrice_i;

            //구매액이 보유액보다 작거나 같으면
            if (coin_i >= price)
            {
                itemCount = PlayerPrefs.GetInt("Item_EnergyDrink");
                itemCount += itemAmount_i;
                PlayerPrefs.SetInt("Item_EnergyDrink", itemCount);
                coin_i -= price;    //코인 차감
                PlayerPrefs.SetInt("Player_Coin", coin_i);  //코인 갱신

                ItemAmountLook();
                buyPanel.SetActive(false);//구매팝업 비활성화
                Game_UIManager.instance.BlurCameraCanvas_ob.SetActive(false);
                Game_UIManager.instance.StoreSignPopup.SetActive(true);
                Game_UIManager.instance.StoreSignPopup_Text.text = "Purchase has been completed.";
                SoundFunction.Instance.ItemBuy_Sound();

                // 서버에 저장
                ServerManager.Instance.Update_Item(PlayerPrefs.GetString("Player_ID"), itemKind, itemCount);
                ServerManager.Instance.Update_Coin("-", price);
            }
            else
            {
                SoundFunction.Instance.Warning_Sound();
                Game_UIManager.instance.StoreSignPopup.SetActive(true);
                Game_UIManager.instance.StoreSignPopup_Text.text = "Not enough coins.";
            }
        }
    }
}
