using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;


public class MyInventory_UIManager : MonoBehaviour
{
    [Header("토글 그룹")]
    public ToggleGroup downItemTapToggleGroup;  //하단 아이템 탭 토글


    [Header("활성/비활성 오브젝트")]
    public GameObject itemPanel;    //아이템 판넬
    
    public GameObject allItemView;
    public GameObject expView;
    public GameObject coinView;
    public GameObject speedView;

    public Text expText;
    public Text coinText;

    GameObject itemDataBase;
    GameObject sensorManager;
    Transform playerWoman;
    Transform playerMan;

    int exp, coin;



    public Toggle downItemTapToggleCurrentSelection
    {
        get { return downItemTapToggleGroup.ActiveToggles().FirstOrDefault(); }
    }


    private void Awake()
    {
        exp = PlayerPrefs.GetInt("Busan_Player_CurrExp");
        coin = PlayerPrefs.GetInt("Busan_Player_Gold");
        expText.text = exp.ToString();
        coinText.text = MyInventory_DataManager.instance.CommaText(coin);


        //내 정보 - 모델링 위치 초기화
        if (PlayerPrefs.GetString("Busan_Player_Sex") == "Woman")
        {
            playerWoman = GameObject.Find("Woman").GetComponent<Transform>();

            //내 정보 - 모델링 위치 초기화
            playerWoman.localPosition = new Vector3(-5.5f, 7.5f, 7.6f);
            playerWoman.localRotation = Quaternion.Euler(-1.8f, 105f, -1.5f);
            playerWoman.localScale = new Vector3(2.3f, 2.3f, 2.3f);
        }
        else if (PlayerPrefs.GetString("Busan_Player_Sex") == "Man")
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
