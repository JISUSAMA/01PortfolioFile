using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;


public class Store_UIManager : MonoBehaviour
{

    [Header("토글 그룹")]
    public ToggleGroup topTapToggleGroup;   //상단 탭 토글
    public ToggleGroup downItemTapToggleGroup;  //하단 아이템 탭 토글

    [Header("활성/비활성 오브젝트")]
    public GameObject itemPanel;    //아이템 팔넬
    
    public GameObject expView;
    public GameObject coinView;
    public GameObject speedView;
    public GameObject marketView;

    public const string DOWN_ITEM_EXP_TOGGLE_NAME ="ExpToggle";
    public const string DOWN_ITEM_COIN_TOGGLE_NAME = "CoinToggle";
    public const string DOWN_ITEM_SPEED_TOGGLE_NAME = "SpeedToggle";
    public const string DOWN_ITEM_MARKET_TOGGLE_NAME = "MarketToggle";

    public Text expText;
    public Text coinText;

    public Button buyBtn;   //구매버튼

    //각 아이템 배열에 저장된 번호
    //int id_jacketNum, id_pantsNum, id_shoesNum, id_hairNum, id_bodyNum, id_helmetNum, id_glovesNum;

    //GameObject itemDataBase;
    //GameObject sensorManager;
    Transform woman_player;
    //WomanCtrl womanctrl_scrip;
    Transform man_player;
    //ManCtrl manctrl_scrip;
    int exp, coin;



    public Toggle topTapToggleCurrentSelection
    {
        get { return topTapToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle downItemTapToggleCurrentSelection
    {
        get { return downItemTapToggleGroup.ActiveToggles().FirstOrDefault(); }
    }


    private void Awake()
    {
        Initialization();

        //내 정보 - 모델링 위치 초기화
        if (PlayerPrefs.GetString("Busan_Player_Sex") == "Woman")
        {
            woman_player = GameObject.Find("Woman").GetComponent<Transform>();
            //womanctrl_scrip = woman_player.GetComponent<WomanCtrl>();

            //내 정보 - 모델링 위치 초기화
            woman_player.localPosition = new Vector3(5.5f, 7.4f, 7.5f);
            woman_player.localRotation = Quaternion.Euler(0f, -105f, 0f);
            woman_player.localScale = new Vector3(3f, 3f, 3f);
        }
        else if (PlayerPrefs.GetString("Busan_Player_Sex") == "Man")
        {
            man_player = GameObject.Find("Man").GetComponent<Transform>();
            //manctrl_scrip = man_player.GetComponent<ManCtrl>();

            //내 정보 - 모델링 위치 초기화
            man_player.localPosition = new Vector3(5.5f, 7.4f, 7.5f);
            man_player.localRotation = Quaternion.Euler(0f, -105f, 0f);
            man_player.localScale = new Vector3(3f, 3f, 3f);
        }

        //itemDataBase = GameObject.Find("DataManager");
        //sensorManager = GameObject.Find("SensorManager");
        
        
    }

    void Initialization()
    {
        buyBtn.interactable = false;    //구매버튼 비활성화

        exp = PlayerPrefs.GetInt("Busan_Player_CurrExp");
        coin = PlayerPrefs.GetInt("Busan_Player_Gold");
        expText.text = exp.ToString();
        coinText.text = Store_DataManager.instance.CommaText(coin);

        //각 아이템 배열 넘버 저장
        //id_hairNum = PlayerPrefs.GetInt("Busan_HairNumber");
        //id_bodyNum = PlayerPrefs.GetInt("Busan_BodyNumber");
        //id_jacketNum = PlayerPrefs.GetInt("Busan_JacketNumber");
        //id_pantsNum = PlayerPrefs.GetInt("Busan_PantsNumber");
        //id_shoesNum = PlayerPrefs.GetInt("Busan_ShoesNumber");
        //id_helmetNum = PlayerPrefs.GetInt("Busan_HelmetNumber");
        //id_glovesNum = PlayerPrefs.GetInt("Busan_GlovesNumber");
    }


    public void DownItemTapToggleChoice()
    {
        if(downItemTapToggleGroup.ActiveToggles().Any())
        {
            if(downItemTapToggleCurrentSelection.name == DOWN_ITEM_EXP_TOGGLE_NAME)
            {
                Item_ActiveShow(true, false, false);
            }
            else if (downItemTapToggleCurrentSelection.name == DOWN_ITEM_COIN_TOGGLE_NAME)
            {
                Item_ActiveShow(false, true, false);
            }
            else if (downItemTapToggleCurrentSelection.name == DOWN_ITEM_SPEED_TOGGLE_NAME)
            {
                Item_ActiveShow(false, false, true);
            }
            buyBtn.interactable = false;    //구매버튼 비활성화
        }
    }


    //하단 판넬 황성화 비활성화
    void Item_ActiveShow(bool _exp, bool _coin, bool _speed)
    {
        expView.SetActive(_exp);
        coinView.SetActive(_coin);
        speedView.SetActive(_speed);
    }

    public void InitButtonOnClick()
    {
        Store_DataManager.instance.InitData_ButtonOn();
    }

    //백버튼 - 로비씬
    public void BackButtonOn()
    {

        //PlayerPrefs.SetInt("HairNumber", Store_DataManager.instance.id_bodyNum);
        //PlayerPrefs.SetInt("BodyNumber", Store_DataManager.instance.id_bodyNum);
        //PlayerPrefs.SetInt("JacketNumber", Store_DataManager.instance.id_jacketNum);
        //PlayerPrefs.SetInt("PantsNumber", Store_DataManager.instance.id_pantsNum);
        //PlayerPrefs.SetInt("ShoesNumber", Store_DataManager.instance.id_shoesNum);

        SoundMaixerManager.instance.OutGameBGMPlay();
        SceneManager.LoadScene("Lobby");
    }

    //BGM 사운드
    public void BGM_SliderSound()
    {
        SoundMaixerManager.instance.AudioControl();
    }

    //효과음 사운드
    public void Effect_SliderSound()
    {
        SoundMaixerManager.instance.SFXAudioControl();
    }
}
