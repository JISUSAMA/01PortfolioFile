using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class Lobby_UIManager : MonoBehaviour
{
    public static Lobby_UIManager instance { get; private set; }
    public GameObject firstLoginCoinPopup;  //첫로그인팝업
    public GameObject nickNameChangePopup;  //닉네임변경팝업
    public GameObject coinShortPopup;   //코인부족 팝업
    public GameObject errorPopup;   //알림 팝업
    public GameObject BlurCamera_ob;
    public GameObject GameClosePopup_ob;

    public Button Sculpted_Btn; //조각함 버튼
    public int PiceCount;
    public InputField nick_Field;   //닉네임 
    public Text overlapText;    //닉네임 설정 시 멘트

    [Header("UI Text")]
    public Text connectDay_Text; //접속 날짜
    public string connectDay_title;
    public Text coin_Text;  //코인
    public Text nickName_Text;  //이름
    public Text checkPoint_Text;    //체크포인트
    public Text totalDistance_Text; //총걸음수
    public Text totalTime_Text; //총시간
    public Text totalKcal_Text; //총칼로리
    public Text errer_Text; //알림말 텍스트
    public Text sensorAddr_Text;

#if __DEBUG__
    public GameObject DebugMenu;
#endif

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }
    void Start()
    {
#if __DEBUG__
        DebugMenu.SetActive(true);
#endif

        FirstLoginCoinPopupAction();

        Initialization();
        Debug.Log("............" + Lobby_DataManager.instance.CommaText(1));
        SoundManager.Instance.PlayBGM("1_MainTheme");
    }
    void Update()
    {
        //게임 종료 할래요?
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                BlurCamera_ob.SetActive(true);
                GameClosePopup_ob.SetActive(true);
            }
        }

    }
    void Initialization()
    {
        Lobby_DataManager.instance.MyDateSave();
        CennectDay_MapName(); //타이틀 명
        // 1. 여정의 시작, 2. .. , 3.
        connectDay_Text.text = (Lobby_DataManager.instance.accessDay_i).ToString() + " DAY : " + connectDay_title;
        coin_Text.text = Lobby_DataManager.instance.CommaText(Lobby_DataManager.instance.coin_i);
        nickName_Text.text = Lobby_DataManager.instance.nickName_s;
        checkPoint_Text.text = Lobby_DataManager.instance.currSection_i.ToString() + " Check Point";
        PiceCount = PlayerPrefs.GetInt("MoonPieceCount"); //획득한 조각 갯수 
        if (PiceCount > 0)
        {
            Sculpted_Btn.interactable = true;
        }
        else
        {
            Sculpted_Btn.interactable = false;
        }
        //totalDistance_Text.text = Lobby_DataManager.instance.totalDistance.ToString();
        totalDistance_Text.text = (Math.Truncate(Lobby_DataManager.instance.totalDistance * 100f) / 100f).ToString() + "m";
        totalTime_Text.text = Lobby_DataManager.instance.totalTime_s;
        totalKcal_Text.text = (Math.Truncate(Lobby_DataManager.instance.totalKcal_f * 10f) / 10f).ToString() + "Kcal";
    }

    void CennectDay_MapName()
    {
 
        float distance = PlayerPrefs.GetFloat("Moon_Distance");
        float way = 1000 - distance;
        int wayPoint = (int)(way / 50) + 1; //맵차트 1,2,- 20
        Debug.Log("moondist : " + wayPoint);
        if (way.Equals(1000)) connectDay_title = " 명예의 전당";  //명예의 전당
        else
        {
            if (wayPoint.Equals(1)) connectDay_title = " 여정의 시작"; //1 0-50
            else if (wayPoint.Equals(2)) connectDay_title = " 목적없는 발걸음";//2 51-100
            else if (wayPoint.Equals(3)) connectDay_title = " 달의 비밀"; //3
            else if (wayPoint.Equals(4)) connectDay_title = " 희망의 끈"; //4
            else if (wayPoint.Equals(5)) connectDay_title = " 길을 잃은 아기별"; //5
            else if (wayPoint.Equals(6)) connectDay_title = " 맴도는 공허함"; //6
            else if (wayPoint.Equals(7)) connectDay_title = " 빛의 무리"; //7
            else if (wayPoint.Equals(8)) connectDay_title = " 수상한 빛"; //8
            else if (wayPoint.Equals(9)) connectDay_title = "  나를 도와줘"; //9
            else if (wayPoint.Equals(10)) connectDay_title = " 불꽃놀이";   //10
            else if (wayPoint.Equals(11)) connectDay_title = " 소원석";    //11
            else if (wayPoint.Equals(12)) connectDay_title = " 발버둥 치는 마음";  //12
            else if (wayPoint.Equals(13)) connectDay_title = " 우주를 떠도는 영혼";    //13
            else if (wayPoint.Equals(14)) connectDay_title = " 함께하는 여정";    //14
            else if (wayPoint.Equals(15)) connectDay_title = " 목걸이의 주인";    //15
            else if (wayPoint.Equals(16)) connectDay_title = " 안녕, 별자리";    //16
            else if (wayPoint.Equals(17)) connectDay_title = " 몽환의 세계"; //17
            else if (wayPoint.Equals(18)) connectDay_title = " 점점 더 가까이";   //18
            else if (wayPoint.Equals(19)) connectDay_title = " 색을 잃은 별";    //19
            else if (wayPoint.Equals(20)) connectDay_title = " 달의 신전";  //20
        }
    }
    //첫 로그인 코인 받기
    void FirstLoginCoinPopupAction()
    {
        if (PlayerPrefs.GetString("FirstLoginTime") == "")
        {
            firstLoginCoinPopup.SetActive(true);

            PlayerPrefs.SetInt("Player_Coin", 1000);
            ServerManager.Instance.Update_Coin("+", 1000);
            Lobby_DataManager.instance.coin_i = PlayerPrefs.GetInt("Player_Coin");
            coin_Text.text = Lobby_DataManager.instance.CommaText(Lobby_DataManager.instance.coin_i);
            ///////////////////////   처음 로그인한 날짜 저장 //////////////////////////////////////////////
            PlayerPrefs.SetString("FirstLoginTime", DateTime.Now.ToString("yyyy-MM-dd"));

            ServerManager.Instance.UserLoginState_Update();
        }
        else
        {
            firstLoginCoinPopup.SetActive(false);
        }
    }

    public void NickName_OverLap_CheckBtn()
    {
        Lobby_DataManager.instance.NickName_OverLap_Check(nick_Field.text, overlapText);
    }
    //여행자이름 변경 버튼 클릭
    public void NickNameChangeButtonOn()
    {
        Lobby_DataManager.instance.NickName_OverLap_Check(nick_Field.text, overlapText);
        if (overlapText.text == "사용가능한 닉네임 입니다.")
        {
            Lobby_DataManager.instance.SetCoinSub(1000);

            string str = coin_Text.text;
            str = str.Replace(",", ""); //코인에서 , 없애기
            int coin = int.Parse(str);

            if (coin == PlayerPrefs.GetInt("Player_Coin"))
            {
                BlurCamera_ob.SetActive(true);
                coinShortPopup.SetActive(true);
            }
            else if (coin > PlayerPrefs.GetInt("Player_Coin"))
            {
                /////////////////////// 코인의 값과 닉네임이 변경되어야함 /////////////////////////////////
                coin_Text.text = PlayerPrefs.GetInt("Player_Coin").ToString();
                Lobby_DataManager.instance.coin_i = PlayerPrefs.GetInt("Player_Coin");
                PlayerPrefs.SetString("Player_NickName", nick_Field.text);
                nickName_Text.text = PlayerPrefs.GetString("Player_NickName");
                Lobby_DataManager.instance.nickName_s = PlayerPrefs.GetString("Player_NickName");

                ErrorPopup_Sign(); //변경완료 안내

            }
            // BlurCamera_ob.SetActive(false);
            nickNameChangePopup.SetActive(false);

        }
    }


    void ErrorPopup_Sign()
    {
        ServerManager.Instance.Update_Coin("-", 1000); //닉네임을 변경하고 남은 코인,서버업데이트
        ServerManager.Instance.Update_NickName(PlayerPrefs.GetString("Player_NickName"));

        errorPopup.SetActive(true);
        BlurCamera_ob.SetActive(true);
        errer_Text.text = "닉네임 변경에 성공하셨습니다.";
    }
    //각 [자세히보기]버튼 씬 전환
    public void TotalStepDistanceButton()
    {
        PlayerPrefs.SetString("StartView", "Distance");
        SceneManager.LoadScene("ExerciseRecord");
    }
    public void TotalStepTimeButton()
    {
        PlayerPrefs.SetString("StartView", "Time");
        SceneManager.LoadScene("ExerciseRecord");
    }
    public void TotalStepKcalButton()
    {
        PlayerPrefs.SetString("StartView", "Kcal");
        SceneManager.LoadScene("ExerciseRecord");
    }
    public void StepCountButton()
    {
        PlayerPrefs.SetString("StartView", "StepCount");
        SceneManager.LoadScene("ExerciseRecord");
    }
    //센서값 초기화
    public void SensorAddrInit()
    {
        //센서값 초기값

        PlayerPrefs.SetString("SensorAddr", "");
        sensorAddr_Text.text = PlayerPrefs.GetString("SensorAddr");
    }

    //센서값 등록 - 셋업에서
    public void SensorAddrSave()
    {
        StartCoroutine(_SensorAddrSave());
    }

    IEnumerator _SensorAddrSave()
    {
        //GameObject sensorManger = GameObject.Find("SensorManager");
        //L_ESP32BLEApp sensorScrip = sensorManger.GetComponent<L_ESP32BLEApp>();

        L_ESP32BLEApp.instance.StartProcess();


        yield return new WaitForSeconds(2f);

        //센서스트립트에 있는 startProcess()로 연결 시도하고
        //몇 초후에 연결되었으면 텍스트로 뿌려준다.
        sensorAddr_Text.text = PlayerPrefs.GetString("SensorAddr");
    }


    //걷기 모드 설정 씬 이동
    public void RunStartButtonOn()
    {
#if __DEBUG__
        if(DropdownManager.instance.isReady)
        {
            Debug.Log("isReady : " + DropdownManager.instance.isReady);
            SceneManager.LoadScene("Loading");
        }
#elif UNITY_EDITOR        
            SceneManager.LoadScene("Loading");
#elif UNITY_ANDROID
        // 연결 확인 체크
        if (L_ESP32BLEApp.instance.connecting)
        {
            SceneManager.LoadScene("Loading");
        }
        else
        {
            L_ESP32BLEApp.instance.StartProcess();
        }
#endif
    }
    //일반 - 센서 연결
    public void SensorConnect()
    {        
        L_ESP32BLEApp.instance.StartProcess();
        SoundFunction.Instance.ButtonClick_Sound();
    }
    public void SensorCancel()
    {
        L_ESP32BLEApp.instance.NonState();
        SoundFunction.Instance.ButtonClick_Sound();
    }
    public void Popup_Close(GameObject ob)
    {
        SoundFunction.Instance.CloseClick_Sound();
        BlurCamera_ob.SetActive(false);
        ob.SetActive(false);
    

    }
    //달에 도달했을 때, 기록 저장하고 나머지 데이터 초기화 시킴
    void PlayerDataRest()
    {
        PlayerPrefs.SetInt("Current_Section", 1);   //현재 구간

        PlayerPrefs.SetInt("Total_StepCount", 0);   //총 걸음수
        PlayerPrefs.SetString("Total_StepTime", "");    //총 걸은시간

        PlayerPrefs.SetFloat("Total_Kcal", 0);  //총 칼로리s
        PlayerPrefs.SetFloat("Total_Distance", 0);  //총걸은거리

        PlayerPrefs.SetFloat("Moon_Distance", 0);   //달까지 남은 거리
    }
    //게임 종료하기 팝업
    public void GameExit_YesBtn()
    {
        SoundFunction.Instance.ButtonClick_Sound();
        //게임 종료
        Application.Quit();
    }
    public void GameExit_NoBtn()
    {
        SoundFunction.Instance.CloseClick_Sound();
        GameClosePopup_ob.SetActive(false);
        BlurCamera_ob.SetActive(false);

    }

}