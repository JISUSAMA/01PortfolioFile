using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Main_UIManager : MonoBehaviour
{

    /////////////////////////////////////////////////
    public GameObject[] VideoPanels;  //영상 이미지 판넬 오브젝트(0:인트로 /1:타이틀)
    public VideoPlayer _introVideo; //인트로 영상
    public VideoPlayer _titleVideo; //타이틀 영상

    [SerializeField] GameObject IntroSkipBtnOB;
    public RenderTexture[] VideoPanelsRenderTexture;

    [SerializeField] GameObject PlayStartBtnOB;
    [SerializeField] Button StartBtn;

    [SerializeField] SetupBundle setupBundle;
    string loginState = "";  //로그인 상태
    private void Awake()
    {
        //PlayerPrefs.SetString("Player_LoginState", ""); //로그인 한 적 없을 경우,
        //PlayerPrefs.DeleteKey("Player_ID"); //로그인 한 적 없을 경우,
        //PlayerPrefs.SetString("IntroState", "True");
        //PlayerPrefs.SetString("NoticeState", "True");
        //PlayerPrefs.SetString("PushState", "True");

        //기존 플레이어 아이디가 존재하는 경우,
        if (PlayerPrefs.HasKey("Player_ID"))
        {
            loginState = PlayerPrefs.GetString("Player_LoginState"); //로그인 상태

            if (!loginState.Equals(""))
            {
                ServerManager.Instance.userInfo.player_ID = PlayerPrefs.GetString("Player_ID");
                ServerManager.Instance.userInfo.player_PW = PlayerPrefs.GetString("Player_Password");
                ServerManager.Instance.userInfo.player_UID = PlayerPrefs.GetString("Player_UID");
                ServerManager.Instance.userInfo.player_LoginState = PlayerPrefs.GetString("Player_LoginState");
                ServerManager.Instance.userInfo.player_NickName = PlayerPrefs.GetString("Player_NickName");

                if (loginState.Equals("Again"))
                {
                    StartBtn.onClick.AddListener(() => { OnCall_LoginScene(); SoundFunction.Instance.ButtonClick_Sound(); });
                }
                else if (loginState.Equals("Gateways"))
                {
                    string player_id = PlayerPrefs.GetString("Player_ID");
                    StartCoroutine(_ServerMyInformation(player_id)); //서버에서 정보 들고오기
                    StartBtn.onClick.AddListener(() => { OnCall_LobbyScene(); SoundFunction.Instance.ButtonClick_Sound(); });
                }
                else if (loginState.Equals("GatewaysNickName"))
                {
                    /* string player_id = PlayerPrefs.GetString("Player_ID");
                     StartCoroutine(_ServerMyInformation(player_id)); //서버에서 정보 들고오기
                     StartBtn.onClick.AddListener(() => { OnCall_NickNameScene(); });*/
                    StartBtn.onClick.AddListener(() => { OnCall_LoginScene(); SoundFunction.Instance.ButtonClick_Sound(); });
                }
                //구글
                if (loginState.Equals("Google"))
                {
                    StartCoroutine(_ServerMyInformation(Social.localUser.id));
                    StartBtn.onClick.AddListener(() => { OnCall_LobbyScene(); SoundFunction.Instance.ButtonClick_Sound(); });
                }
                else if (loginState.Equals("GoogleNickName"))
                {
                    /* StartCoroutine(_ServerMyInformation(Social.localUser.id));
                     StartBtn.onClick.AddListener(() => { OnCall_NickNameScene(); });*/
                    StartBtn.onClick.AddListener(() => { OnCall_LoginScene(); SoundFunction.Instance.ButtonClick_Sound(); });
                }
                else if (loginState.Equals("TermsJoinAgree"))
                {
                    StartBtn.onClick.AddListener(() => { OnCall_LoginScene(); SoundFunction.Instance.ButtonClick_Sound(); });
                }
                Debug.Log("loginState" + loginState);
            }
            else
            {
                StartBtn.onClick.AddListener(() => { OnCall_LoginScene(); SoundFunction.Instance.ButtonClick_Sound(); });
            }
        }
        else
        {
            StartBtn.onClick.AddListener(() => { OnCall_LoginScene(); SoundFunction.Instance.ButtonClick_Sound(); });
        }
    }
    private void OnEnable()
    {
        for (int i = 0; i < VideoPanelsRenderTexture.Length; i++)
            VideoPanelsRenderTexture[i].Release();
    }
    // Update is called once per frame
    void Update()
    {
             VideoPlayCheck(); //인트로 영상 플레이 체크
    }
    public void VideoPlayCheck()
    {
      //  Debug.Log("IntroState" + PlayerPrefs.GetString("IntroState"));
        //인트로 스킵 버튼이 있는 경우, 
        if (PlayerPrefs.HasKey("IntroState") && !loginState.Equals("Again") || !loginState.Equals(""))
        {

            //인트로 재생 활성화 중, 
            if (PlayerPrefs.GetString("IntroState").Equals("True"))
            {
                //인트로 영상이 재생중이면
                if (VideoPanels[0].activeSelf.Equals(true))
                {
                    long playerCurrentFrame = _introVideo.GetComponent<VideoPlayer>().frame;
                    long playerFrameCount = Convert.ToInt64(_introVideo.GetComponent<VideoPlayer>().frameCount);
                    if (playerCurrentFrame < playerFrameCount - 100)
                    {
                        //  print("VIDEO IS PLAYING");
                    }
                    else
                    {
                        // print("VIDEO IS OVER");
                        //Do w.e you want to do for when the video is done playing.
                        for (int i = 0; i < VideoPanelsRenderTexture.Length; i++)
                            VideoPanelsRenderTexture[i].Release();
                        IntroSkipBtnOB.SetActive(false);

                        if (setupBundle.isReady)
                        {
                            PlayStartBtnOB.SetActive(true);
                        }
                        //PlayStartBtnOB.SetActive(true);
                        //Cancel Invoke since video is no longer playing
                        CancelInvoke("VideoPlayCheck");
                    }
                }
                //타이틀 영상이 재생중이면
                if (VideoPanels[1].activeSelf.Equals(true))
                {
                    long playerCurrentFrame = _titleVideo.GetComponent<VideoPlayer>().frame;
                    long playerFrameCount = Convert.ToInt64(_titleVideo.GetComponent<VideoPlayer>().frameCount);
                    if (playerCurrentFrame < playerFrameCount - 100)
                    {
                        //  print("VIDEO IS PLAYING");
                    }
                    else
                    {
                        // print("VIDEO IS OVER");
                        //Do w.e you want to do for when the video is done playing.
                        for (int i = 0; i < VideoPanelsRenderTexture.Length; i++)
                            VideoPanelsRenderTexture[i].Release();

                        if (setupBundle.isReady)
                        {
                            PlayStartBtnOB.SetActive(true);
                        }
                        //PlayStartBtnOB.SetActive(true);
                        //Cancel Invoke since video is no longer playing
                        CancelInvoke("VideoPlayCheck");
                    }
                }
            }
            //인트로 비활성화 중
            else
            {
                VideoPanels[0].SetActive(false);
                VideoPanels[1].SetActive(true);
                //타이틀 영상이 재생중이면
                if (VideoPanels[1].activeSelf.Equals(true))
                {
                    long playerCurrentFrame = _titleVideo.GetComponent<VideoPlayer>().frame;
                    long playerFrameCount = Convert.ToInt64(_titleVideo.GetComponent<VideoPlayer>().frameCount);
                    if (playerCurrentFrame < playerFrameCount - 100)
                    {
                        //  print("VIDEO IS PLAYING");
                    }
                    else
                    {
                        // print("VIDEO IS OVER");
                        //Do w.e you want to do for when the video is done playing.
                        for (int i = 0; i < VideoPanelsRenderTexture.Length; i++)
                            VideoPanelsRenderTexture[i].Release();

                        if (setupBundle.isReady)
                        {
                            PlayStartBtnOB.SetActive(true);
                        }
                        //PlayStartBtnOB.SetActive(true);
                        //Cancel Invoke since video is no longer playing
                        CancelInvoke("VideoPlayCheck");
                    }
                }
            }
        }
        else
        {
            //인트로 영상이 재생중이면
            if (VideoPanels[0].activeSelf.Equals(true))
            {
                long playerCurrentFrame = _introVideo.GetComponent<VideoPlayer>().frame;
                long playerFrameCount = Convert.ToInt64(_introVideo.GetComponent<VideoPlayer>().frameCount);
                if (playerCurrentFrame < playerFrameCount - 100)
                {
                    //  print("VIDEO IS PLAYING");
                }
                else
                {
                    // print("VIDEO IS OVER");
                    //Do w.e you want to do for when the video is done playing.
                    for (int i = 0; i < VideoPanelsRenderTexture.Length; i++)
                        VideoPanelsRenderTexture[i].Release();
                    IntroSkipBtnOB.SetActive(false);

                    if (setupBundle.isReady)
                    {
                        PlayStartBtnOB.SetActive(true);
                    }
                    //PlayStartBtnOB.SetActive(true);
                    //Cancel Invoke since video is no longer playing
                    CancelInvoke("VideoPlayCheck");
                }
            }
            //타이틀 영상이 재생중이면
            else if (VideoPanels[1].activeSelf.Equals(true))
            {
                long playerCurrentFrame = _titleVideo.GetComponent<VideoPlayer>().frame;
                long playerFrameCount = Convert.ToInt64(_titleVideo.GetComponent<VideoPlayer>().frameCount);
                if (playerCurrentFrame < playerFrameCount - 100)
                {
                    //  print("VIDEO IS PLAYING");
                }
                else
                {
                    // print("VIDEO IS OVER");
                    //Do w.e you want to do for when the video is done playing.
                    for (int i = 0; i < VideoPanelsRenderTexture.Length; i++)
                        VideoPanelsRenderTexture[i].Release();

                    if (setupBundle.isReady)
                    {
                        PlayStartBtnOB.SetActive(true);
                    }
                    //PlayStartBtnOB.SetActive(true);
                    //Cancel Invoke since video is no longer playing
                    CancelInvoke("VideoPlayCheck");
                }
            }
        }

    }
    //스킵 버튼
    public void OnClick_IntroSkipBtn()
    {
        VideoPanels[0].SetActive(false); //인트로영상
        VideoPanels[1].SetActive(true); //타이틀 영상
        SoundFunction.Instance.ButtonClick_Sound();
    }
    //여정시작 버튼

    void OnCall_LoginScene()
    {
        Debug.Log("로그인씬");
        SceneManager.LoadScene("Login"); //로그인 씬으로 이동
    }
    void OnCall_LobbyScene()
    {
        SceneManager.LoadScene("Lobby");
    }
    void OnCall_NickNameScene()
    {
        SceneManager.LoadScene("NickName");
    }
    IEnumerator _ServerMyInformation(string _userID)
    {
        // 가져오는 데이터 //

        // 접속일 / 코인

        // 닉네임 / 로그인 상태
        // 현재구간
        // 월별 걸은거리 / 걸음수 / 걸음시간 / 소모칼로리
        // 총 걸은거리 / 걸음수 / 걸은시간 / 소모칼로리
        // 달까지 남은 거리
        // 소비 아이템 : 작은공기통, 큰공기통, 에너지드링크

        // 서버에서 계정 데이터 가져오기
        ServerManager.Instance.Get_MyProfileData(_userID);

        yield return new WaitUntil(() => ServerManager.Instance.isGetMyProfileDataCompleted);

        ServerManager.Instance.isGetMyProfileDataCompleted = false;

        PlayerPrefs.SetString("Player_LoginState", ServerManager.Instance.userInfo.player_LoginState); //서버에 저장된 로그인 상태를 들고와서 저장

        PlayerPrefs.SetString("Player_NickName", ServerManager.Instance.userInfo.player_NickName);
        PlayerPrefs.SetString("Player_LoginState", ServerManager.Instance.userInfo.player_LoginState);

        PlayerPrefs.SetInt("Player_Coin", ServerManager.Instance.lobbyInfo.player_Coin);
        PlayerPrefs.SetInt("Player_ConnectDay", ServerManager.Instance.lobbyInfo.player_ConnectDate);
        PlayerPrefs.SetInt("Current_Section", ServerManager.Instance.lobbyInfo.player_Current_Section);

        PlayerPrefs.SetFloat("Total_Distance", ServerManager.Instance.totalData_To_Date.total_Distance);
        PlayerPrefs.SetString("Total_StepTime", ServerManager.Instance.totalData_To_Date.total_StepTime);
        PlayerPrefs.SetFloat("Total_Kcal", ServerManager.Instance.totalData_To_Date.total_Kcal);
        PlayerPrefs.SetInt("Total_StepCount", ServerManager.Instance.totalData_To_Date.total_StepCount);

        PlayerPrefs.SetInt("Item_SmallAirTank", ServerManager.Instance.consumable_Item.Item_SmallAirTank);
        PlayerPrefs.SetInt("Item_BigAirTank", ServerManager.Instance.consumable_Item.Item_BigAirTank);
        PlayerPrefs.SetInt("Item_EnergyDrink", ServerManager.Instance.consumable_Item.Item_EnergyDrink);

        //--------------------------------------------------------------------------------------------------

        // 서버에서 레코드(그래프) 데이터 가져오기
        // 오늘 날짜 확인 후 없으면 기본값 프리팹에 저장 있으면 가져오기 ( 일, 월, 년 )
        string[] todayArr = new string[3];
        todayArr[0] = DateTime.Now.ToString("yyyy");
        todayArr[1] = DateTime.Now.ToString("MM");
        todayArr[2] = DateTime.Now.ToString("dd");

        // 오늘 날짜 있든 없든 가져옴
        // 없으면 없는거 확인
        //ServerManager.Instance.Get_DateData(int.Parse(todayArr[2]), int.Parse(todayArr[2]), int.Parse(todayArr[1]), int.Parse(todayArr[0]));
        ServerManager.Instance.Get_DateData(int.Parse(todayArr[0]), int.Parse(todayArr[1]), int.Parse(todayArr[2]), int.Parse(todayArr[2]));
        yield return new WaitUntil(() => ServerManager.Instance.isGetDateDataCompleted);
        ServerManager.Instance.isGetDateDataCompleted = false;

        ServerManager.Instance.Get_MonthData(int.Parse(todayArr[0]), int.Parse(todayArr[1]), int.Parse(todayArr[1]));
        yield return new WaitUntil(() => ServerManager.Instance.isGetMonthDataCompleted);
        ServerManager.Instance.isGetMonthDataCompleted = false;

        PlayerPrefs.SetFloat("Today_Distance", ServerManager.Instance.dataByDate[0].today_Distance);  //일별 걸은거리
        PlayerPrefs.SetInt("Today_StepCount", ServerManager.Instance.dataByDate[0].today_StepCount);   //일별 걸음수
        PlayerPrefs.SetString("Today_StepTime", ServerManager.Instance.dataByDate[0].today_StepTime);    //일별 걸은시간
        PlayerPrefs.SetFloat("Today_Kcal", ServerManager.Instance.dataByDate[0].today_Kcal);  //일별 칼로리

        PlayerPrefs.SetFloat("Month_Distance", ServerManager.Instance.dataByMonth[0].month_Distance);  //월별 걸은거리
        PlayerPrefs.SetInt("Month_StepCount", ServerManager.Instance.dataByMonth[0].month_StepCount);   //월별 걸음수
        PlayerPrefs.SetString("Month_StepTime", ServerManager.Instance.dataByMonth[0].month_StepTime);    //월별 걸은시간
        PlayerPrefs.SetFloat("Month_Kcal", ServerManager.Instance.dataByMonth[0].month_Kcal);  //월별 칼로리

        PlayerPrefs.SetFloat("BGM",20);  //BGM 소리
        PlayerPrefs.SetFloat("SFX", 20);  //SFX 소리

        PlayerPrefs.SetFloat("Moon_Distance", ServerManager.Instance.totalData_To_Date.moon_dist);   //달까지 남은 거리
    }
}

