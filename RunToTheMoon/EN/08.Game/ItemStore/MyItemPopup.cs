using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyItemPopup : MonoBehaviour
{
    public Button itemSmallBtn;    //작은 산소통 버튼
    public Button itemBigBtn;  //큰 산소통 버튼
    public Button itemEnergyBtn;   //에너지 아이템 버튼
    public Button itemMoonPowderBtn;   //에너지 아이템 버튼
   
    public Text Subtitle_text; //부 내용
    public Text smallAmount_Text;
    public Text bigAmount_Text;
    public Text energyAmount_Text;
    //   public Text moonPowder_Text;

    string clickState;    //클릭 상태
    [SerializeField] bool clickState_b = false;
    int smallAmount_i, bigAmount_i, energyAmount_i; //아이템 갯수

    public GameObject[] ClickStateImg; //작은산소, 큰산소, 별가루, 달가루

    private void OnEnable()
    {
        DataInit();
        Time.timeScale = 0;
        RunnerPlayer1.instance.StoryEventing = true;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        RunnerPlayer1.instance.StoryEventing = false;
        CheckStateMarker(false, false, false, false);
    }
    void Start()
    {
        DataInit();
    }

    void DataInit()
    {
        clickState_b = false;
        clickState = "";
        smallAmount_i = PlayerPrefs.GetInt("Item_SmallAirTank");
        bigAmount_i = PlayerPrefs.GetInt("Item_BigAirTank");
        energyAmount_i = PlayerPrefs.GetInt("Item_EnergyDrink");

        smallAmount_Text.text = smallAmount_i.ToString();
        bigAmount_Text.text = bigAmount_i.ToString();
        energyAmount_Text.text = energyAmount_i.ToString();

        CheckStateMarker(true, true, true, true);
        //갯수가 0이면 버튼 비활성화
        if (smallAmount_i <= 0)
        { itemSmallBtn.interactable = false; }
        if (bigAmount_i <= 0)
        { itemBigBtn.interactable = false; }
        if (energyAmount_i <= 0)
        { itemEnergyBtn.interactable = false; }

      
        if (!RunnerPlayer1.instance.moonPowder_event_Section)
            itemMoonPowderBtn.interactable = false;
        else itemMoonPowderBtn.interactable = true;
        Subtitle_text.text = "Items purchased from the space rest area.\nCan be purchased space rest area.";
        CheckStateImgMarker(itemSmallBtn.interactable, itemBigBtn.interactable, itemEnergyBtn.interactable, itemMoonPowderBtn.interactable);
    }


    public void SmallItemButtonOn()
    {
        if (!clickState_b)
        {
            clickState = "Small";
            CheckStateMarker(true, false, false, false);
            CheckStateImgMarker(true, false, false, false);
            Subtitle_text.text = "Use ‘Small’ in inventory immediately?";
            clickState_b = true; //버튼 누름 
        }
        else
        {
            DataInit();
        }

    }

    public void BigItemButtonOn()
    {
        if (!clickState_b)
        {
            clickState = "Big";
            CheckStateMarker(false, true, false, false);
            CheckStateImgMarker(false, true, false, false);
            Subtitle_text.text = "Use ‘Big’ in inventory immediately?";
            clickState_b = true; //버튼 누름 
        }
        else
        {
            DataInit();

        }
      

    }

    public void EnergyItemButtonOn()
    {
        if (!clickState_b)
        {
            clickState = "Energy";
            CheckStateMarker(false, false, true, false);
            CheckStateImgMarker(false, false, true, false);
            Subtitle_text.text = "Use ‘Stardust’ in inventory immediately?";
            clickState_b = true; //버튼 누름 
        }
        else
        {
            DataInit();

        }
     
    }
    public void MoonItemButtonOn()
    {
        if (!clickState_b)
        {
            clickState = "MoonPowder";
            CheckStateMarker(false, false, false, true);
            CheckStateImgMarker(false, false, false, true);
            Subtitle_text.text = "Use ‘Moondust’ in inventory immediately?";
            clickState_b = true; //버튼 누름 
        }
        else
        {
            DataInit();
        }
    
    }
    void CheckStateMarker(bool s, bool b, bool e, bool m)
    {
        itemSmallBtn.interactable = s;
        itemBigBtn.interactable = b;
        itemEnergyBtn.interactable = e;
        itemMoonPowderBtn.interactable = m;
     
    }
    void CheckStateImgMarker(bool s, bool b, bool e, bool m)
    {
        ClickStateImg[0].SetActive(s);
        ClickStateImg[1].SetActive(b);
        ClickStateImg[2].SetActive(e);
        ClickStateImg[3].SetActive(m);
    }
    //이벤트 용! 달의 가루
    public void Use_MoonPowder()
    {
        Debug.LogError("달가루 사용했음!!!!!!!!!!!!!!");
        //달가루 이벤트가 진행 중이면, 달가루 사용 가능 하도록
        if (RunnerPlayer1.instance.moonPowder_useEvnet)
        {
            RunnerPlayer1.instance.use_moonPowder = true; //달가루 사용했음을 확인
            Game_UIManager.instance.BlurCameraCanvas_ob.SetActive(false); //블러 없애기 
        }
        else
        {
            RunnerPlayer1.instance.Show_Sign("Cnt_MoonPowder");
        }

    }

    //아이템 사용하기 버튼 클릭 시
    public void UseButtonOn()
    {
        SoundFunction.Instance.ButtonClick_Sound();
        if (clickState_b)
        {
            if (clickState == "Small")
            {
                smallAmount_i -= 1;
                smallAmount_Text.text = smallAmount_i.ToString();
                PlayerPrefs.SetInt("Item_SmallAirTank", smallAmount_i);

                if (smallAmount_i <= 0)
                    itemSmallBtn.interactable = false;

                O2Timer.instance.SmallItemUse_OneTimePlus();    //산소통 시간 충전
                RunnerPlayer1.instance.Use_Oxygen_Small();

                // 서버에 저장
                ServerManager.Instance.Update_Item(PlayerPrefs.GetString("Player_ID"), clickState, smallAmount_i);

                // 아이템 사용 상태 유무
            }
            else if (clickState == "Big")
            {
                bigAmount_i -= 1;
                bigAmount_Text.text = bigAmount_i.ToString();
                PlayerPrefs.SetInt("Item_BigAirTank", bigAmount_i);

                if (bigAmount_i <= 0)
                    itemBigBtn.interactable = false;

                O2Timer.instance.BigItemUse_OneTimePlus();
                RunnerPlayer1.instance.Use_Oxygen_Big();

                //  O2Timer.instance.TimeInit(); //산소통 시간 충전

                // 서버에 저장
                ServerManager.Instance.Update_Item(PlayerPrefs.GetString("Player_ID"), clickState, bigAmount_i);
            }
            else if (clickState == "Energy")
            {
                energyAmount_i -= 1;
                energyAmount_Text.text = energyAmount_i.ToString();
                PlayerPrefs.SetInt("Item_EnergyDrink", energyAmount_i);


                if (energyAmount_i <= 0)
                    itemEnergyBtn.interactable = false;

                RunnerPlayer1.instance.Use_StarDust();
                ServerManager.Instance.Update_Item(PlayerPrefs.GetString("Player_ID"), clickState, energyAmount_i);
            }
            else if (clickState == "MoonPowder")
            {
                Use_MoonPowder();
            }

            if (smallAmount_i <= 0)
                itemSmallBtn.interactable = false;
            if (bigAmount_i <= 0)
                itemBigBtn.interactable = false;
            if (energyAmount_i <= 0 || RunnerPlayer1.instance.use_starDust == true)
                itemEnergyBtn.interactable = false;

            Game_UIManager.instance.BlurCameraCanvas_ob.SetActive(false); //블러 없애기 
            this.gameObject.SetActive(false);
        }
        else
        {
            Game_UIManager.instance.StoreSignPopup.gameObject.SetActive(true);
            Game_UIManager.instance.StoreSignTitle_Text.text = "Notification";
            Game_UIManager.instance.StoreSignPopup_Text.text = "Please select an item to use.";
        }
      
    }

}
