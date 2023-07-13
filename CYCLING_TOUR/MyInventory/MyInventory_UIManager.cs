using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;


public class MyInventory_UIManager : MonoBehaviour
{
    [Header("토글 그룹")]
    public ToggleGroup topTapToggleGroup;   //상단 탭 토글
    public ToggleGroup downClothesTapToggleGroup;  //하단 의상 탭 토글
    public ToggleGroup downEquipmentTapToggleGroup; //하단 장비 탭 토글
    public ToggleGroup downItemTapToggleGroup;  //하단 아이템 탭 토글

    public ToggleGroup helmetToggleGroup;
    public ToggleGroup glovesToggleGroup;

    [Header("활성/비활성 오브젝트")]
    public GameObject clothesPanel; //의상 판넬
    public GameObject equipmentPanel;   //장비 판넬
    public GameObject itemPanel;    //아이템 판넬
    public GameObject allClothseView;
    public GameObject hairView;
    public GameObject shirtView;
    public GameObject pantsView;
    public GameObject shoesView;
    public GameObject helmetView;
    public GameObject glovesView;
    public GameObject allEquipmentView;
    public GameObject bicycleView;
    public GameObject allItemView;
    public GameObject expView;
    public GameObject coinView;
    public GameObject speedView;

    public Toggle clothesAllToggle;
    public Toggle equipmentAllToggle;
    public Toggle itemAllToggle;

    public Toggle hairToggle;
    public Toggle bicycleToggle;

    public Text expText;
    public Text coinText;

    GameObject itemDataBase;
    GameObject sensorManager;
    Transform playerWoman;
    Transform playerMan;

    int exp, coin;


    public Toggle topTapToggleCurrentSelection
    {
        get { return topTapToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle downClothesTapToggleCurrentSelection
    {
        get { return downClothesTapToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle downEquipmentTapToggleCurrentSelection
    {
        get { return downEquipmentTapToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public Toggle downItemTapToggleCurrentSelection
    {
        get { return downItemTapToggleGroup.ActiveToggles().FirstOrDefault(); }
    }


    private void Awake()
    {
        exp = PlayerPrefs.GetInt("AT_Player_CurrExp");
        coin = PlayerPrefs.GetInt("AT_Player_Gold");
        expText.text = exp.ToString();
        coinText.text = MyInventory_DataManager.instance.CommaText(coin);


        //내 정보 - 모델링 위치 초기화
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            playerWoman = GameObject.Find("Woman").GetComponent<Transform>();

            //내 정보 - 모델링 위치 초기화
            playerWoman.localPosition = new Vector3(-5.5f, 7.5f, 7.6f);
            playerWoman.localRotation = Quaternion.Euler(-1.8f, 105f, -1.5f);
            playerWoman.localScale = new Vector3(2.3f, 2.3f, 2.3f);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            playerMan = GameObject.Find("Man").GetComponent<Transform>();

            //내 정보 - 모델링 위치 초기화
            playerMan.localPosition = new Vector3(-5.5f, 7.5f, 7.6f);
            playerMan.localRotation = Quaternion.Euler(-1.8f, 105f, -1.5f);
            playerMan.localScale = new Vector3(2.3f, 2.3f, 2.3f);
        }

        itemDataBase = GameObject.Find("DataManager");
        sensorManager = GameObject.Find("SensorManager");
        Initialization();
    }

    void Initialization()
    {
        
        Clothes_and_Equipment_ActiveShow(true, false, false); //의상 판넬 활성화 - 장비 비활성화
        Clothes_ActiveShow(true, false, false, false, false, false, false); ;// 초기화 해당 판넬
    }



    public void TopTapToggleChoice()
    {
        if(topTapToggleGroup.ActiveToggles().Any())
        {
            if(topTapToggleCurrentSelection.name == "ClothesToggle")
            {
                //의상 판넬 활성화 - 장비 비활성화 - 아이템 비활성화
                Clothes_and_Equipment_ActiveShow(true, false, false);
                clothesAllToggle.isOn = true;
                //hairToggle.isOn = true; //기본 헤어토글이 선택되어 있게하기 위해
                //기본 - 머리뷰 활성화, 나머지 비활성화
                Clothes_ActiveShow(true, false, false, false, false, false, false);
            }
            else if(topTapToggleCurrentSelection.name == "EquipmentToggle")
            {
                //의상 판넬 비활성화 - 장비 판넬 활성화 - 아이템 비활성화
                Clothes_and_Equipment_ActiveShow(false, true, false);
                equipmentAllToggle.isOn = true;
                //bicycleToggle.isOn = true;  //기본 자전거토글이 선택되어 있게 하기 위해
                Equipment_ActiveShow(true, false); //기본 - 자전거 뷰 활성화 (추후-추가 시 늘어남)
            }
            else if(topTapToggleCurrentSelection.name == "ItemToggle")
            {
                //의상 판넬 비활성화 - 장비 판넬 비활성화 - 아이템 활성화
                 Clothes_and_Equipment_ActiveShow(false, false, true);
                itemAllToggle.isOn = true;
                Item_ActiveShow(true, false, false, false);
            }
        }
    }

    //큰탭 의상-장비 활성화 비활성화 
    void Clothes_and_Equipment_ActiveShow(bool _colothes, bool _equipment, bool _item)
    {
        clothesPanel.SetActive(_colothes);
        equipmentPanel.SetActive(_equipment);
        itemPanel.SetActive(_item);
    }

    //의상 하단탭 눌렀을 때 이벤트
    public void DownClothesTapToggleChoice()
    {
        //의상 하단탭 눌렀을 때 - 머리, 상의, 하의, 신발, 핼맷, 장갑 순서
        if(downClothesTapToggleGroup.ActiveToggles().Any())
        {
            if (downClothesTapToggleCurrentSelection.name == "AllToggle")
            {
                Clothes_ActiveShow(true, false, false, false, false, false, false);
            }
            else if(downClothesTapToggleCurrentSelection.name == "HairToggle")
            {
                MyInventory_DataManager.instance.HairToggleIsOn();
                Clothes_ActiveShow(false, true, false, false, false, false, false);
            }
            else if (downClothesTapToggleCurrentSelection.name == "ShirtToggle")
            {
                MyInventory_DataManager.instance.ShirtToggleIsOn();
                Clothes_ActiveShow(false, false, true, false, false, false, false);
            }
            else if (downClothesTapToggleCurrentSelection.name == "PantsToggle")
            {
                MyInventory_DataManager.instance.PantsToggleIsOn();
                Clothes_ActiveShow(false, false, false, true, false, false, false);
            }
            else if (downClothesTapToggleCurrentSelection.name == "ShoesToggle")
            {
                MyInventory_DataManager.instance.ShoesToggleIsOn();
                Clothes_ActiveShow(false, false, false, false, true, false, false);
            }
            else if (downClothesTapToggleCurrentSelection.name == "HelmetToggle")
            {
                if(PlayerPrefs.GetString("AT_Wear_HelmetKind") == "No")
                {
                    if(helmetToggleGroup.AnyTogglesOn())
                    {
                        helmetToggleGroup.allowSwitchOff = true;
                    }
                        
                    Clothes_ActiveShow(false, false, false, false, false, true, false);
                }
                else
                {
                    MyInventory_DataManager.instance.HelmetToggleIsOn();
                    Clothes_ActiveShow(false, false, false, false, false, true, false);
                }
                
            }
            else if (downClothesTapToggleCurrentSelection.name == "GlovesToggle")
            {
                if(PlayerPrefs.GetString("AT_Wear_GlovesKind") == "No")
                {
                    if(glovesToggleGroup.AnyTogglesOn())
                    {
                        glovesToggleGroup.allowSwitchOff = true;
                    }
                    Clothes_ActiveShow(false, false, false, false, false, false, true);
                }
                else
                {
                    MyInventory_DataManager.instance.GlovesToggleIsOn();
                    Clothes_ActiveShow(false, false, false, false, false, false, true);
                }
                
            }
        }
    }

    //장비 소 탭 눌럿을 때 이벤트
    public void EqipmentTapToggleChoice()
    {
        if(downEquipmentTapToggleGroup.ActiveToggles().Any())
        {
            if(downEquipmentTapToggleCurrentSelection.name == "AllToggle")
            {
                Equipment_ActiveShow(true, false);
            }
            else if (downEquipmentTapToggleCurrentSelection.name == "BicycleToggle")
            {
                Equipment_ActiveShow(false, true);
            }
        }
    }

    //아이템 소 탭 눌렀으 ㄹ때 이벤트
    public void ItemTapToggleChoice()
    {
        if(downItemTapToggleGroup.ActiveToggles().Any())
        {
            if(downItemTapToggleCurrentSelection.name == "AllToggle")
            {
                Item_ActiveShow(true, false, false, false);
            }
            else if (downItemTapToggleCurrentSelection.name == "ExpToggle")
            {
                Item_ActiveShow(false, true, false, false);
            }
            else if (downItemTapToggleCurrentSelection.name == "CoinToggle")
            {
                Item_ActiveShow(false, false, true, false);
            }
            else if (downItemTapToggleCurrentSelection.name == "SpeedToggle")
            {
                Item_ActiveShow(false, false, false, true);
            }
        }
    }

    //의상 활성화 비활성화
    void Clothes_ActiveShow(bool _all, bool _hair, bool _shirt, bool _pants, bool _shoes, bool _helmet, bool _gloves)
    {
        allClothseView.SetActive(_all);
        hairView.SetActive(_hair);  //머리
        shirtView.SetActive(_shirt);    //윗옷
        pantsView.SetActive(_pants);    //아랫옷
        shoesView.SetActive(_shoes);    //신발
        helmetView.SetActive(_helmet);  //헬맷
        glovesView.SetActive(_gloves);  //장갑
    }

    void Equipment_ActiveShow(bool _all, bool _bicycle)
    {
        allEquipmentView.SetActive(_all);
        bicycleView.SetActive(_bicycle);
    }

    void Item_ActiveShow(bool _all, bool _exp, bool _coin, bool _speed)
    {
        allItemView.SetActive(_all);
        expView.SetActive(_exp);
        coinView.SetActive(_coin);
        speedView.SetActive(_speed);
    }


    //백버튼 - 로비씬
    public void BackButtonOn()
    {
        StartCoroutine(_BackButtonOn());
    }

    IEnumerator _BackButtonOn()
    {
        MyInventory_DataManager.instance.CustomSettingSave();   //커스텀 저장 데이터(아이템 번호)

        yield return new WaitUntil(() => ServerManager.Instance.isBuyItemUpdateCompleted);

        ServerManager.Instance.isBuyItemUpdateCompleted = false;

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
