using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Game_UIManager : MonoBehaviour
{

    public static Game_UIManager instance { get; private set; }
    public Camera[] cameraOb;
    public Text nickName_Text;
    public Text playDay_Text;
    public Text todayTime_Text;
    public Text todayKcal_Text;
    public Text coin_Text;
    public Text o2_Text;
    public Slider o2_Slider;
    public Text moonDis_Text;   //달까지 남은 거리
    public Text spaceStationDis_Text;

    public MyItemPopup myItemPopup_sc;

    [Header("PopupUI")]
    public GameObject minutePopup_ob; //산소 5분남음
    public GameObject o2EndPopup_ob; //산소 소진 완료, 게임 끝

    public GameObject checkPointPopup_ob; //다시 걷기
    public Text rCheckPoint_t; //현재 체크 포인트
    public Text rDistance_t; //현재 지점 위치
    public Text rMoonDis_Text;//달까지 남은 거리 
    public Text rSpaceStationDis_Text;

    public GameObject spaceStationPopup_ob; //우주 정거장 도착 팝업
    public GameObject BlurCameraCanvas_ob; //팝업 블러
    public GameObject GameClosePopup_ob;    // 게임종료 팝업

    public GameObject StroyOb_Background;
    public Volume Potal_Volume;
    public VolumeProfile Potal_profile;

    public GameObject StoreSignPopup; //알림 팝업 창
    public Text StoreSignTitle_Text;    //알림 팝업 텍스트
    public Text StoreSignPopup_Text;    //알림 팝업 텍스트

    Bloom bl;

    [Header("fade in/out")]
    public Image FadePanel;
    public Image AlertImage;
    float time = 0;
    float F_time = 1f;
    public void Fade_in_out()
    {
        StartCoroutine(_Fade_in_out());
    }
    public void Fade_out()
    {
        StartCoroutine(_Fade_out());
    }
    public void Fade_in()
    {
        StartCoroutine(_Fade_in());
    }
    IEnumerator _Fade_in_out()
    {
        time = 0; F_time = 1; //초기화
        FadePanel.gameObject.SetActive(true);
        Color alpha = FadePanel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            FadePanel.color = alpha;
            yield return null;
        }
        StartCoroutine(_Fade_out());
        yield return null;
    }
    IEnumerator _Fade_in()
    {
        time = 0; F_time = 1; //초기화
        FadePanel.gameObject.SetActive(true);
        Color alpha = FadePanel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            FadePanel.color = alpha;
            yield return null;
        }
        yield return null;
    }
    IEnumerator _Fade_out()
    {
        time = 0; F_time = 1; //초기화
        FadePanel.gameObject.SetActive(true);
        Color alpha = FadePanel.color;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            FadePanel.color = alpha;
            yield return null;
        }
        yield return null;
    }

    public void Alert()
    {
        SoundFunction.Instance.WarringSound_alert();
        StartCoroutine(_Alert());
    }

    IEnumerator _Alert()
    {
        time = 0; F_time = 1f; //초기화
        AlertImage.gameObject.SetActive(true);
        Color alpha = AlertImage.color;
        while (alpha.a < 0.3f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 0.3f, time);
            AlertImage.color = alpha;
            yield return null;
        }

        time = 0; F_time = 1; //초기화
        //AlertImage.gameObject.SetActive(true);
        Color _alpha = AlertImage.color;

        while (_alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            _alpha.a = Mathf.Lerp(0.3f, 0, time);
            AlertImage.color = _alpha;
            yield return null;
        }

        yield return null;
    }
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
        QualitySettings.pixelLightCount = 50;

        // 프레임 고정
        Application.targetFrameRate = 40;
    }

    private void Start()
    {
        //데이터 초기화
        Game_DataManager.instance.SetPlayData(nickName_Text, playDay_Text, coin_Text, todayTime_Text, todayKcal_Text, moonDis_Text, spaceStationDis_Text);
        Potal_Volume.profile = Potal_profile;
    }
    private void OnDisable()
    {
        bl = (Bloom)Potal_Volume.profile.components[0];
        bl.intensity.value = 0;
    }
    private void Update()
    {
        //UI에 게임 정보 뿌려줌
        if (Game_DataManager.instance.runEndState.Equals(false))
        {
            if (RunnerPlayer1.instance.StoryEventing.Equals(false))
            {
                // Debug.Log("spaceStationDis" + Game_DataManager.instance.spaceStationDis);
                Game_DataManager.instance.PlayUIData(nickName_Text, playDay_Text, coin_Text, todayTime_Text, todayKcal_Text, moonDis_Text, spaceStationDis_Text);
                //목표 거리까지 도달 했을 경우, 
                if (Game_DataManager.instance.spaceStationDis <= 0)
                {
                    //  Debug.Log("우주정거장 활성화!" );
                    if (Game_DataManager.instance.moonDis <= 0)
                    {
                        //달에 도착했을 경우, 랭킹 등록 후 캐릭터 씬 이동
                        RunnerPlayer1.instance.speed = 0f;
                  
                        SceneManager.LoadScene("HallofFame");
                    }
                    else
                    {
                        //우주 정거장 도착
                        // 21.08.10 여기서 우주정거장 업데이트 계속됨 수정해야함..
                        // 멈춤
                        if (SceneManager.GetActiveScene().name.Equals("Game 16"))
                        {
                            if (Chapter16_Manager.instance.Chapter16_pos_int.Equals(10))
                            {
                                //16챕터 마지막 달의 포자 이벤트 끝,쓰러지면서 마무리!
                            }
                            else
                            {
                                RunnerPlayer1.instance.speed = 0f;
                                Reach_SpaceStation();
                            }
                        }
                        else
                        {
                            RunnerPlayer1.instance.speed = 0f;
                            Reach_SpaceStation();
                        }

                        //Debug_Reach_SpaceStation();
                    }
                }
            }

        }
    }

    //종료하기 버튼 클릭
    public void RunEndButtonOn()
    {
        BlurCameraCanvas_ob.SetActive(true);
        SoundFunction.Instance.ButtonClick_Sound(); //종료하기 버튼 클릭!
        Time.timeScale = 0;
        Game_DataManager.instance.runEndState = true;     // 멈춤
    }
    // 종료하기 버튼의 팝업, 종료하기 버튼
    public void RunEndButton_Finish()
    {
        Time.timeScale = 1;
        SoundFunction.Instance.ButtonClick_Sound();
        Game_DataManager.instance.runEndState = true;     // 멈춤
        Game_DataManager.instance.gamePlaying = false;    // 게임 끝
        PlayerPrefs.SetFloat("Today_StepTime_f", Game_DataManager.instance.todayTime_f);//오늘 걸은 시간
        // 도중에 끝냄
        Game_DataManager.instance.SaveProcessingData();
     
    }
    // 종료하기 버튼의 팝업, 계속하기 버튼
    public void RunButtonOn_Again()
    {
        SoundFunction.Instance.ButtonClick_Sound();
        BlurCameraCanvas_ob.SetActive(false);
        Time.timeScale = 1;
        Game_DataManager.instance.runEndState = false;    // 달리는 중
        Game_DataManager.instance.gamePlaying = true;    // 다시걷기
                                                         //  O2Timer.instance.Timer();

    }
    public void Debug_Reach_SpaceStation()
    {
        SoundManager.Instance.PlayBGM("2_SpaceStop");
        //Game_DataManager.instance.currentSection += 1;
        Game_DataManager.instance.runEndState = true;

        Game_DataManager.instance.moonDis = 1000f;

        Game_DataManager.instance.SavePlayData();
        StroyOb_Background.SetActive(false);
        spaceStationPopup_ob.SetActive(true);
        BlurCameraCanvas_ob.SetActive(true);
        Time.timeScale = 0;
    }
    //우주정거장 도착!
    public void Reach_SpaceStation()
    {
        SoundManager.Instance.PlayBGM("2_SpaceStop");
        Game_DataManager.instance.currentSection += 1;
        Game_DataManager.instance.runEndState = true;
        Game_DataManager.instance.moonDis = 1000 - Game_DataManager.instance.currentSection * 5;
        Game_DataManager.instance.SavePlayData();
        StroyOb_Background.SetActive(false);
        spaceStationPopup_ob.SetActive(true);
        BlurCameraCanvas_ob.SetActive(true);
        Time.timeScale = 0;
    }
    public void O2EndPopup_RestartBtn()
    {
        Time.timeScale = 0;
        checkPointPopup_ob_On(); // 체크포인트 지점 확인하기
    }
    public void checkPointPopup_ob_On()
    {
        checkPointPopup_ob.SetActive(true);
        SoundFunction.Instance.ButtonClick_Sound();
        BlurCameraCanvas_ob.SetActive(true);
        Game_DataManager.instance.moonDis = 1000 - Game_DataManager.instance.currentSection * 5;
        float way = 1000 - Game_DataManager.instance.moonDis;
        rCheckPoint_t.text = Game_DataManager.instance.currentSection.ToString() + "Check Point";
        rDistance_t.text = way.ToString("N0") + " Km 지점";
        rMoonDis_Text.text = Game_DataManager.instance.moonDis.ToString("N0");
        Game_DataManager.instance.spaceStationDis = 5;


        rSpaceStationDis_Text.text = Game_DataManager.instance.spaceStationDis.ToString();
    }

    public void RunRestartButtonOn()
    {
        Time.timeScale = 1;
        BlurCameraCanvas_ob.SetActive(false);
        SoundFunction.Instance.ButtonClick_Sound();
        int way = 1000 - (int)Game_DataManager.instance.moonDis;

        // 나중에 지우기
        //PlayerPrefs.SetFloat("Moon_Distance", 1000f);

        int wayPoint = (way / 50) + 1; //맵차트 1,2,- 20
        string nextScene = "";

        if (wayPoint.Equals(1)) nextScene = "Game 1"; //1 0-50
        else if (wayPoint.Equals(2)) nextScene = "Game 2";//2 51-100
        else if (wayPoint.Equals(3)) nextScene = "Game 3";//3
        else if (wayPoint.Equals(4)) nextScene = "Game 4";//4
        else if (wayPoint.Equals(5)) nextScene = "Game 5"; //5
        else if (wayPoint.Equals(6)) nextScene = "Game 6"; //6
        else if (wayPoint.Equals(7)) nextScene = "Game 7"; //7
        else if (wayPoint.Equals(8)) nextScene = "Game 8"; //8
        else if (wayPoint.Equals(9)) nextScene = "Game 9"; //9
        else if (wayPoint.Equals(10)) nextScene = "Game 10";   //10
        else if (wayPoint.Equals(11)) nextScene = "Game 11";   //11
        else if (wayPoint.Equals(12)) nextScene = "Game 12"; //12
        else if (wayPoint.Equals(13)) nextScene = "Game 13";    //13
        else if (wayPoint.Equals(14)) nextScene = "Game 14";  //14
        else if (wayPoint.Equals(15)) nextScene = "Game 15";   //15
        else if (wayPoint.Equals(16)) nextScene = "Game 16";  //16
        else if (wayPoint.Equals(17)) nextScene = "Game 17"; //17
        else if (wayPoint.Equals(18)) nextScene = "Game 18";   //18
        else if (wayPoint.Equals(19)) nextScene = "Game 19";   //19
        else if (wayPoint.Equals(20)) nextScene = "Game 20"; //20

        // 센서 리셋
        RunnerPlayer1.instance.resetRotation();
        SceneManager.LoadScene("Loading");
       // SceneManager.LoadScene(nextScene);
    }
    public void Popup_Open()
    {
        BlurCameraCanvas_ob.SetActive(true);
        if (Game_DataManager.instance.gamePlaying)
        {
            Time.timeScale = 0;//시간 멈춤
        }
    }
    public void Popup_Open(GameObject ob)
    {
        SoundFunction.Instance.ButtonClick_Sound();
        BlurCameraCanvas_ob.SetActive(true);
        ob.SetActive(true);
        if (Game_DataManager.instance.gamePlaying)
        {
            Time.timeScale = 0;//시간 멈춤
        }
    }
    public void Popup_Close()
    {
        BlurCameraCanvas_ob.SetActive(false);
        if (Game_DataManager.instance.gamePlaying)
        {
            Time.timeScale = 1;//게임 재개
        }

    }
    public void Popup_Close(GameObject ob)
    {
        SoundFunction.Instance.CloseClick_Sound();
        BlurCameraCanvas_ob.SetActive(false);
        ob.SetActive(false);
        if (Game_DataManager.instance.gamePlaying)
        {
            Time.timeScale = 1;//게임 재개
        }

    }
    public void Potal_Bloom()
    {
        StartCoroutine(_Potal_Bloom());
    }
    IEnumerator _Potal_Bloom()
    {
        bl = (Bloom)Potal_Volume.profile.components[0];
        while (bl.intensity.value < 100)
        {
            bl.intensity.value += Time.deltaTime * 100f;
            //    Debug.Log(" 1 :" + bl.intensity.value);
            yield return null;
        }

        /*   while (bl.intensity.value > 0)
             {
                 bl.intensity.value -= Time.deltaTime * 200f;
                 Debug.Log(" 2 :" + bl.intensity.value);
                 yield return null;
             }*/
        yield return new WaitForSeconds(0.2f);
        // Reach_SpaceStation();
        SceneManager.LoadScene("HallofFame");//명예의 전당
        //씬 넘어가기
        yield return null;
    }

    //일반 - 센서 연결
    public void SensorConnect()
    {
        SoundFunction.Instance.ButtonClick_Sound();
        L_ESP32BLEApp.instance.StartProcess();
    }
    public void SensorCancel()
    {
        L_ESP32BLEApp.instance.NonState();
        SoundFunction.Instance.ButtonClick_Sound();
    }
    //게임 종료하기 팝업
    public void GameExit_YesBtn()
    {
        SoundFunction.Instance.CloseClick_Sound();
        //게임 종료
        Application.Quit();
    }
    public void GameExit_NoBtn()
    {
        SoundFunction.Instance.ButtonClick_Sound();
        GameClosePopup_ob.SetActive(false);
        BlurCameraCanvas_ob.SetActive(false);
    }
  
}