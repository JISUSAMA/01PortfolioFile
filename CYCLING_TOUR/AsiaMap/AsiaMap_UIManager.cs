using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System;



//[Obsolete]
public class AsiaMap_UIManager : MonoBehaviour
{
    public static AsiaMap_UIManager instance { get; private set; }

    public Image profileImg;    //프로필사진
    public Text levelText;  //레벨
    public Slider levelSlider;   //경험치
    public Text nickNameText;   //닉네임
    public Text timeText;   //시간
    public Text courseText; //코스
    public Text distanceText;   //거리
    public Text speedText;  //속력
    public Text miniMapText;    //미니맵 총 맵 거리
    public Text kcalText;   //칼로리
    public Text missionText;    //타임미션
    public GameObject[] itemUseIcon;    //아이템

    public GameObject[] hardModeObj;    //하드모드 장애물
    public Image[] miniMapImg;  //미니맵 이미지

    int expFull_i, expCurr_i;

    public bool gameStartState; //게임 시작 여부
    public bool gameEnd;    //게임 골인 여부
    public GameObject[] finishLine; //피니쉬라인 이미지 0:500, 1:300, 2:100


    //센서 쪽 변수
    GameObject itemDataBase;
    GameObject sensorManager;
    Transform woman_player;
    WomanCtrl womanctrl_scrip;
    Transform man_player;
    ManCtrl manctrl_scrip;

    public GameObject[] endPoint;   //도착 지점

    public GameObject parentObj;    //플레이어가 들어갈 부모

    public CinemachineVirtualCamera virtualCameraStop;
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineVirtualCamera virtualCamera_1;
    public CinemachineVirtualCamera virtualCamera_2;
    public CinemachineVirtualCamera virtualCamera2;
    public CinemachineVirtualCamera virtualCamera3;
    public CinemachineVirtualCamera virtualCamera_CourseEnd1;
    public CinemachineVirtualCamera virtualCamera_CourseEnd2;
    public CinemachineVirtualCamera virtualCamera_CourseEnd2_Goalin;
    public CinemachineVirtualCamera virtualCamera_MountainView1;

    CinemachineBasicMultiChannelPerlin virtualCamera_1Parlin;
    CinemachineBasicMultiChannelPerlin virtualCameraPerlin;
    CinemachineBasicMultiChannelPerlin virtualCamera_2Perlin;
    CinemachineBasicMultiChannelPerlin virtualCamera2Parlin;
    CinemachineBasicMultiChannelPerlin virtualCamera3Parlin;

    public NoiseSettings shake6D;
    public NoiseSettings handheld_tele_strong;

    public CinemachineDollyCart track_Course;   //트랙따라가는 카트
    public CinemachineSmoothPath[] sommthPath;  //트랙종류

    //카운트다운
    public Image CountDown_img;
    public Sprite[] CountDown_sprite;


    float cal;  //칼로리 계산을 위한 지수
    float currKcal; //현재 칼로리
    float a, b, finishKcal; //칼로리 들어오는 값 비교 a,b / 최종 칼로리
    bool oneState;  //한번만 들어가기 위한 bool변수
    bool Goal_ = false;

    ParticleSystem dust_particle;    //자전거 먼저 파티클
    public GameObject Goal_ob;
    public GameObject GoalCanvas_ob;

    

    public float speed;
    public bool mountainUpstate;    //오름막길 진입 여부
    public bool mountainDownState;  //내리막길 진입 여부
    public bool stonFarmState;  //자갈밭 진입 여부
    public bool puddleState;    //웅덩이 진입 여부
    public bool sandState;  //모래 진입 여부


    //public Text test;
    //public Text test2;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }


    void Start()
    {
        Resources.UnloadUnusedAssets();
        PlayBGM();  //BGM시작
        virtualCamera_1Parlin = virtualCamera_1.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        virtualCameraPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        virtualCamera_2Perlin = virtualCamera_2.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        virtualCamera2Parlin = virtualCamera2.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        virtualCamera3Parlin = virtualCamera3.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();


        PlayerPrefs.SetFloat("AT_CurrKcal", 0);    //현재 칼로리 초기화
        Initialization();

        if(PlayerPrefs.GetString("AT_InGameTutorial").Equals("End"))
            GameStart();


        SpeedCameraViewChange();
    }

    //BGM 시작
    void PlayBGM()
    {
        SoundMaixerManager.instance.InGameBGMPlay();
    }


    void Initialization()
    {
        AsiaMap_DataManager.instance.ProfileImageChange(profileImg);

        if (PlayerPrefs.GetString("AT_UseItemCoin").Equals("Coin"))
            itemUseIcon[0].SetActive(true);
        else itemUseIcon[0].SetActive(false);

        if (PlayerPrefs.GetString("AT_UseItemExp").Equals("Exp"))
            itemUseIcon[1].SetActive(true);
        else itemUseIcon[1].SetActive(false);

        if (PlayerPrefs.GetString("AT_UseItemSpeed").Equals("Speed"))
            itemUseIcon[2].SetActive(true);
        else itemUseIcon[2].SetActive(false);

        virtualCameraStop.Priority = 11;
        virtualCamera.Priority = 10;
        virtualCamera_2.Priority = 10;
        virtualCamera_1.Priority = 10;

        //하드모드 시 장애물 활성화
        if(PlayerPrefs.GetString("AT_MapCourseName").Equals("Hard-1"))
        {
            hardModeObj[0].SetActive(true);
        }
        else if(PlayerPrefs.GetString("AT_MapCourseName").Equals("Hard-2"))
        {
            hardModeObj[1].SetActive(true);
        }
        else if(PlayerPrefs.GetString("AT_MapCourseName").Equals("Hard-3"))
        {
            hardModeObj[2].SetActive(true);
        }


        if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Normal-1") || PlayerPrefs.GetString("AT_MapCourseName").Equals("Hard-1"))
        {
            miniMapImg[0].color = Color.yellow; //미니맵 노란색 변경
            track_Course.m_Path = sommthPath[0];
            //Debug.Log(virtualCamera.gameObject.transform.position);
            Goal_ob.transform.position = new Vector3(4, 0.02f, 80); //위치 조정 필요
            Goal_ob.transform.Rotate(0, -11.142f, 0);

        }
        else if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Normal-2") || PlayerPrefs.GetString("AT_MapCourseName").Equals("Hard-2"))
        {
            miniMapImg[1].color = Color.yellow;
            track_Course.m_Path = sommthPath[1];
            //virtualCameraStop.gameObject.transform.position = new Vector3(-0.1848512f, 2.311464f, 83.56913f);// (-6.320894f, 3.844836f, 88.88612f);
            //Debug.Log(virtualCamera.gameObject.transform.position);

            //골인 지점 위치 변경
            Goal_ob.transform.position = new Vector3(-34, 0.02f, -236);
            Goal_ob.transform.Rotate(0, 90f, 0);

            StartCoroutine(_CameraSet());

        }
        else if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Normal-3") || PlayerPrefs.GetString("AT_MapCourseName").Equals("Hard-3"))
        {
            //Debug.Log(" ---------- " + PlayerPrefs.GetString("AT_MapCourseName"));
            miniMapImg[2].color = Color.yellow; //미니맵 노란색 변경
            track_Course.m_Path = sommthPath[2];
            //virtualCamera.gameObject.transform.position = new Vector3(-29.2344f, 2.286102f, -234.9653f);// (-21.16165f, 3.85f, -233.0987f);

            //골인 지점 위치 변경
            Goal_ob.transform.position = new Vector3(57.68f, 220.13f, -460.93f);
            Goal_ob.transform.Rotate(-7.48f, 167.9f, 8.6f);

            StartCoroutine(_CameraSet());
        }

        //내 정보 - 모델링 위치 초기화
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            woman_player = GameObject.Find("Woman").GetComponent<Transform>();
            womanctrl_scrip = woman_player.GetComponent<WomanCtrl>();

            //내 정보 - 모델링 위치 초기화
            woman_player.localPosition = new Vector3(-257.5f, 0.05f, 37f);
            woman_player.localRotation = Quaternion.Euler(0f, 0f, 0f);
            woman_player.localScale = new Vector3(1f, 1f, 1f);

            virtualCamera.Follow = woman_player.transform;
            virtualCamera.LookAt = woman_player.transform.GetChild(0).transform;
            virtualCamera_1.Follow = woman_player.transform;
            virtualCamera_1.LookAt = woman_player.transform.GetChild(0).transform;
            virtualCamera_2.Follow = woman_player.transform;
            virtualCamera_2.LookAt = woman_player.transform.GetChild(0).transform;
            virtualCameraStop.Follow = woman_player.transform;
            virtualCameraStop.LookAt = woman_player.transform.GetChild(0).transform;

            virtualCamera2.Follow = woman_player.transform;
            virtualCamera2.LookAt = woman_player.transform;

            virtualCamera3.Follow = woman_player.transform;
            virtualCamera3.LookAt = woman_player.transform;

            virtualCamera_CourseEnd1.Follow = woman_player.transform;
            virtualCamera_CourseEnd1.LookAt = woman_player.transform;

            virtualCamera_CourseEnd2.Follow = woman_player.transform;
            virtualCamera_CourseEnd2.LookAt = woman_player.transform.GetChild(0).transform;

            virtualCamera_MountainView1.Follow = woman_player.transform;
            virtualCamera_MountainView1.LookAt = woman_player.transform;

            virtualCamera_CourseEnd2_Goalin.LookAt = woman_player.transform;

            //Debug.Log(PlayerPrefs.GetString("AT_MapCourseName"));

            

            woman_player.transform.parent = parentObj.transform;

            dust_particle = woman_player.transform.GetChild(7).GetComponent<ParticleSystem>();
            dust_particle.gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            man_player = GameObject.Find("Man").GetComponent<Transform>();
            manctrl_scrip = man_player.GetComponent<ManCtrl>();
            

            //내 정보 - 모델링 위치 초기화
            man_player.localPosition = new Vector3(-257.5f, 0.05f, 37f);
            man_player.localRotation = Quaternion.Euler(0f, 0f, 0f);
            man_player.localScale = new Vector3(1f, 1f, 1f);

            virtualCamera.Follow = man_player.transform;
            virtualCamera.LookAt = man_player.transform.GetChild(0).transform;
            virtualCamera_1.Follow = man_player.transform;
            virtualCamera_1.LookAt = man_player.transform.GetChild(0).transform;
            virtualCamera_2.Follow = man_player.transform;
            virtualCamera_2.LookAt = man_player.transform.GetChild(0).transform;
            virtualCameraStop.Follow = man_player.transform;
            virtualCameraStop.LookAt = man_player.transform.GetChild(0).transform;

            virtualCamera2.Follow = man_player.transform;
            virtualCamera2.LookAt = man_player.transform;

            virtualCamera3.Follow = man_player.transform;
            virtualCamera3.LookAt = man_player.transform;

            virtualCamera_CourseEnd1.Follow = man_player.transform;
            virtualCamera_CourseEnd1.LookAt = man_player.transform.GetChild(0).transform;

            virtualCamera_CourseEnd2.Follow = man_player.transform;
            virtualCamera_CourseEnd2.LookAt = man_player.transform.GetChild(0).transform;

            virtualCamera_MountainView1.Follow = man_player.transform;
            virtualCamera_MountainView1.LookAt = man_player.transform;

            virtualCamera_CourseEnd2_Goalin.LookAt = man_player.transform;

            

            man_player.transform.parent = parentObj.transform;

            dust_particle = man_player.transform.GetChild(8).GetComponent<ParticleSystem>();
            dust_particle.gameObject.SetActive(true);
            //DontDestroyOnLoad(man_player);
        }

        itemDataBase = GameObject.Find("DataManager");
        sensorManager = GameObject.Find("SensorManager");


        //UI초기화
        expCurr_i = PlayerPrefs.GetInt("AT_Player_CurrExp");
        expFull_i = PlayerPrefs.GetInt("AT_Player_Level") * (PlayerPrefs.GetInt("AT_Player_Level") + 1) * 25;

        levelText.text = PlayerPrefs.GetInt("AT_Player_Level").ToString();
        levelSlider.value = (float)expCurr_i / (float)expFull_i;
        nickNameText.text = PlayerPrefs.GetString("AT_Player_NickName");
        timeText.text = "00:00:00.00";
        courseText.text = PlayerPrefs.GetString("AT_MapCourseName");
        miniMapText.text = PlayerPrefs.GetFloat("AT_MapCourseLength").ToString();
        speedText.text = "0km";
        kcalText.text = "0kcal 소모";
        missionText.text = AsiaMap_DataManager.instance.NextCourseOpenTimeMission() + ":00 완주달성";
    }

    public void ParticleSystemOff()
    {
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
            AsiaMap_DataManager.instance.ParticleSystemOff(dust_particle);
        else if(PlayerPrefs.GetString("AT_Player_Sex") == "Man")
            AsiaMap_DataManager.instance.ParticleSystemOff(dust_particle);

    }

    private void Update()
    {
        particle_action();
    
    }

    private void OnDestroy()
    {
        Resources.UnloadUnusedAssets();
    }

    //부모자식 분리. 다음 씬에 플레이어 보여주기 위함
    public void Parent_Division()
    {
        parentObj.transform.GetChild(1).transform.parent = null;

        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
            DontDestroyOnLoad(woman_player);
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
            DontDestroyOnLoad(man_player);
    }
    
    //메인카메라 변경 - 속도에 따른
    public void SpeedCameraViewChange()
    {
        if(track_Course.m_Speed == 0)
        {
            virtualCameraStop.gameObject.SetActive(true);
            virtualCamera.gameObject.SetActive(false);
            virtualCamera_1.gameObject.SetActive(false);
            virtualCamera_2.gameObject.SetActive(false);
        }
        else if(track_Course.m_Speed <= 5.0f)
        {
            virtualCameraStop.gameObject.SetActive(false);
            virtualCamera.gameObject.SetActive(false);
            virtualCamera_1.gameObject.SetActive(true);
            virtualCamera_2.gameObject.SetActive(false);
        }
        else if(track_Course.m_Speed <= 10f && track_Course.m_Speed > 5.0f)
        {
            virtualCameraStop.gameObject.SetActive(false);
            virtualCamera.gameObject.SetActive(true);
            virtualCamera_1.gameObject.SetActive(false);
            virtualCamera_2.gameObject.SetActive(false);
        }
        else if(track_Course.m_Speed > 10.0f)
        {
            virtualCameraStop.gameObject.SetActive(false);
            virtualCamera.gameObject.SetActive(false);
            virtualCamera_1.gameObject.SetActive(false);
            virtualCamera_2.gameObject.SetActive(true);
        }
    }


    //////////////////////////////// 게임 스타트 ///////////////////////////////////     
    IEnumerator _CameraSet()
    {
        virtualCameraStop.enabled = false;
        virtualCamera.enabled = false;
        virtualCamera_1.enabled = false;
        virtualCamera_2.enabled = false;
        virtualCamera2.enabled = false;
        virtualCamera3.enabled = false;
        virtualCamera_CourseEnd1.enabled = false;
        virtualCamera_CourseEnd2.enabled = false;
        virtualCamera_CourseEnd2_Goalin.enabled = false;
        virtualCamera_MountainView1.enabled = false;

        yield return new WaitForSeconds(0.01f);
        virtualCameraStop.enabled = true;
        virtualCamera.enabled = true;
        virtualCamera_1.enabled = true;
        virtualCamera_2.enabled = true;
        virtualCamera2.enabled = true;
        virtualCamera3.enabled = true;
        virtualCamera_CourseEnd1.enabled = true;
        virtualCamera_CourseEnd2.enabled = true;
        virtualCamera_CourseEnd2_Goalin.enabled = true;
        virtualCamera_MountainView1.enabled = true;

        yield return null;
    }

    public void GameStart()
    {
        StartCoroutine(_GameStart());
        StartCoroutine(_EndPoint());
    }

    IEnumerator _GameStart()
    {
        CountDown_img.gameObject.SetActive(true);
        SoundMaixerManager.instance.OneTwoThreeSoundPlay();


        for (int i = 0; i < CountDown_sprite.Length; i++)
        {
            if (i.Equals(3))
                CountDown_img.rectTransform.sizeDelta = new Vector2(689, 192);

            CountDown_img.sprite = CountDown_sprite[i];
            yield return new WaitForSeconds(1f);
        }
        CountDown_img.gameObject.SetActive(false);

        
        gameStartState = true;  //시작 시작 여부 
        //Debug.Log("타임시작 !!! " + gameStartState);
        GameTime.instance.PlayTime();

        yield return null;
    }


    //일시정지 시 스피드 = 0
    public void GamePlayStop()
    {
        speed = 0; //멈춰!
    }

    
    public void GamePlay()
    {
        //speed = 10;
    }

    //끝포인트 도착 
    public void EndPoint()
    {
        StartCoroutine(_EndPoint());
    }

    IEnumerator _EndPoint()
    {
        string str = PlayerPrefs.GetString("AT_MapCourseName");

        //Debug.Log(str );

        if (str.Equals("Normal-1") || str.Equals("Hard-1"))
        {
            yield return new WaitUntil(() => gameEnd.Equals(true));
            GoalCanvas_ob.SetActive(true); //goaltext UI활성화
            speed = 0; //멈춰!
            //track_Course.m_Speed = 0; //멈춰!
            gameStartState = false;     //게임 시작 여부(끝)
            CurrKcalSave();  //현재 칼로리 저장
        }
        else if (str.Equals("Normal-2") || str.Equals("Hard-2"))
        {
            endPoint[0].SetActive(false);   //코스1 도착 지점 비활성화

            yield return new WaitUntil(() => gameEnd.Equals(true));
            GoalCanvas_ob.SetActive(true); //goaltext UI활성화
            speed = 0; //멈춰!
            //track_Course.m_Speed = 0; //멈춰!
            gameStartState = false;     //게임 시작 여부(끝)
            CurrKcalSave();  //현재 칼로리 저장
        }
        else if (str.Equals("Normal-3") || str.Equals("Hard-3"))
        {
            endPoint[1].SetActive(false);   //코스2 도착 지점 비활성화

            yield return new WaitUntil(() => gameEnd.Equals(true));
            GoalCanvas_ob.SetActive(true); //goaltext UI활성화
            speed = 0; //멈춰!
            //track_Course.m_Speed = 0; //멈춰!
            gameStartState = false;     //게임 시작 여부(끝)
            CurrKcalSave();  //현재 칼로리 저장
        }

        string gameResult = GameTime.instance.Clear_FailAnim();


        if (gameResult.Equals("Clear"))
        {
            if (PlayerPrefs.GetString("AT_Player_Sex").Equals("Woman"))
            {
                womanctrl_scrip.AnimClear_Fail(1f, true, false, true, false, PlayerPrefs.GetInt("AT_HairNumber"), PlayerPrefs.GetInt("AT_BodyNumber"),
                 PlayerPrefs.GetInt("AT_JacketNumber"), PlayerPrefs.GetInt("AT_PantsNumber"), PlayerPrefs.GetInt("AT_ShoesNumber"));
            }
            else if(PlayerPrefs.GetString("AT_Player_Sex").Equals("Man"))
            {
                manctrl_scrip.AnimClear_Fail(1f, true, false, true, false, PlayerPrefs.GetInt("AT_HairNumber"), PlayerPrefs.GetInt("AT_BodyNumber"),
                 PlayerPrefs.GetInt("AT_JacketNumber"), PlayerPrefs.GetInt("AT_PantsNumber"), PlayerPrefs.GetInt("AT_ShoesNumber"));
            }
        }
        else if(gameResult.Equals("Fail"))
        {
            if (PlayerPrefs.GetString("AT_Player_Sex").Equals("Woman"))
            {
                womanctrl_scrip.AnimClear_Fail(1f, true, false, true, false, PlayerPrefs.GetInt("AT_HairNumber"), PlayerPrefs.GetInt("AT_BodyNumber"),
                 PlayerPrefs.GetInt("AT_JacketNumber"), PlayerPrefs.GetInt("AT_PantsNumber"), PlayerPrefs.GetInt("AT_ShoesNumber"));
            }
            else if (PlayerPrefs.GetString("AT_Player_Sex").Equals("Man"))
            {
                manctrl_scrip.AnimClear_Fail(1f, true, false, true, false, PlayerPrefs.GetInt("AT_HairNumber"), PlayerPrefs.GetInt("AT_BodyNumber"),
                 PlayerPrefs.GetInt("AT_JacketNumber"), PlayerPrefs.GetInt("AT_PantsNumber"), PlayerPrefs.GetInt("AT_ShoesNumber"));
            }
        }

        yield return null;
    }

    //현재 스피드
    public string MySpeed(float _time)
    {
        if (PlayerPrefs.GetString("AT_UseItemSpeed").Equals("Speed"))
        {
            if (mountainUpstate.Equals(true) && stonFarmState.Equals(true))  //오르막길 + 자갈밭
                track_Course.m_Speed = (speed * 1.2f) * 0.7f;   //속도 반감
            else if(mountainUpstate.Equals(true))    //산 진입
                track_Course.m_Speed = (speed * 1.2f) * 0.7f;   //속도 반감
            else if(mountainDownState.Equals(true)) //내릿막길
                track_Course.m_Speed = (speed * 1.2f) * 1.05f;   //속도 업
            else if(stonFarmState.Equals(true)) //자갈밭진입
                track_Course.m_Speed = (speed * 1.2f) * 0.7f;   //속도 반감
            else if(puddleState.Equals(true))   //웅덩이 진입
                track_Course.m_Speed = (speed * 1.2f) * 0.8f;   //속도 반감
            else if(sandState.Equals(true)) //모래 진입
                track_Course.m_Speed = (speed * 1.2f) * 0.6f;   //속도 반감
            else
                track_Course.m_Speed = speed * 1.2f;

            //test.text = "track_Course.m_Speed " + track_Course.m_Speed + " speed " + speed;

            //자갈밭, 웅덩이 진입 놉
            if (stonFarmState.Equals(false) && puddleState.Equals(false))
            {
                StonFarmSectionEffect(0);   //셋중에 아무거나 해도 상관없음
            }
            else if(stonFarmState.Equals(true))    //자갈밭
            {
                StonFarmSectionEffect(0.8f);
            }
            else if (puddleState.Equals(true)) //웅덩이
            {
                PuddleSectionEffect(0.4f);
            }
            else if (sandState.Equals(true)) //모래
            {
                SandSectionEffect(1f);
            }
        }
        else
        {
            if (mountainUpstate.Equals(true) && stonFarmState.Equals(true)) //오르막길 + 자갈밭
                track_Course.m_Speed = (speed* 0.5f) * 0.7f;   //속도 반감
            else if (mountainUpstate.Equals(true))    //산 진입
                track_Course.m_Speed = speed * 0.5f;   //속도 반감
            else if (mountainDownState.Equals(true)) //내릿막길
                track_Course.m_Speed = speed * 1.05f;   //속도 업
            else if (stonFarmState.Equals(true)) //자갈밭진입
                track_Course.m_Speed = speed* 0.7f;   //속도 반감
            else if (puddleState.Equals(true))   //웅덩이 진입
                track_Course.m_Speed = speed * 0.8f;   //속도 반감
            else if (sandState.Equals(true)) //모래 진입
                track_Course.m_Speed = speed * 0.6f;   //속도 반감
            else
                track_Course.m_Speed = speed;

            //test.text = "track_Course.m_Speed " + track_Course.m_Speed + " speed " + speed;

            if (stonFarmState.Equals(false) && puddleState.Equals(false) && sandState.Equals(false))
            {
                StonFarmSectionEffect(0);   //셋중에 아무거나 해도 상관없음
            }
            else if (stonFarmState.Equals(true))    //자갈밭
            {
                StonFarmSectionEffect(0.8f);
            }
            else if (puddleState.Equals(true)) //웅덩이
            {
                PuddleSectionEffect(0.4f);
            }
            else if(sandState.Equals(true)) //모래
            {
                //Debug.Log("여기 들어옵니다.");
                SandSectionEffect(1f);
            }
        }
        //현재 스피드
        float currSpeed = AsiaMap_DataManager.instance.MySpeed(track_Course.m_Speed);
        string speedStr = currSpeed.ToString("N2");

        AsiaMap_DataManager.instance.MaxSpeed(speedStr);    //최고속도 저장

        //평균 스피드 구하기
        float _averSpeed = track_Course.m_Position / _time;
        averSpeed = AsiaMap_DataManager.instance.MySpeed(_averSpeed);

        //칼로리 구하기
        cal = Mathf.Lerp(0, 0.46f, Mathf.InverseLerp(0, 80f, averSpeed));
        //test.text = "칼로리 :: " + cal + " 스피드 " + averSpeed +"   : " + currSpeed;
        return speedStr;
    }

    //모래구간 효과
    void SandSectionEffect(float _size)
    {
        if (track_Course.m_Speed <= 5.0f)
        {
            //Debug.Log("1여기 들어옵니다.");
            virtualCamera_1Parlin.m_NoiseProfile = handheld_tele_strong;
            virtualCamera2Parlin.m_NoiseProfile = handheld_tele_strong;
            virtualCamera3Parlin.m_NoiseProfile = handheld_tele_strong;

            virtualCamera_1Parlin.m_AmplitudeGain = _size;
            virtualCamera2Parlin.m_AmplitudeGain = _size;
            virtualCamera3Parlin.m_AmplitudeGain = _size;
        }
        else if (track_Course.m_Speed <= 10.0f && track_Course.m_Speed > 5.0f)
        {
            //Debug.Log("2여기 들어옵니다.");
            virtualCameraPerlin.m_NoiseProfile = handheld_tele_strong;
            virtualCamera2Parlin.m_NoiseProfile = handheld_tele_strong;
            virtualCamera3Parlin.m_NoiseProfile = handheld_tele_strong;

            virtualCameraPerlin.m_AmplitudeGain = _size;
            virtualCamera2Parlin.m_AmplitudeGain = _size;
            virtualCamera3Parlin.m_AmplitudeGain = _size;
        }
        else if (track_Course.m_Speed > 10.0f)
        {
            //Debug.Log("3여기 들어옵니다.");
            virtualCamera_2Perlin.m_NoiseProfile = handheld_tele_strong;
            virtualCamera2Parlin.m_NoiseProfile = handheld_tele_strong;
            virtualCamera3Parlin.m_NoiseProfile = handheld_tele_strong;

            virtualCamera_2Perlin.m_AmplitudeGain = _size;
            virtualCamera2Parlin.m_AmplitudeGain = _size;
            virtualCamera3Parlin.m_AmplitudeGain = _size;
        }
    }

    //웅덩이구간 효과
    void PuddleSectionEffect(float _size)
    {
        if (track_Course.m_Speed <= 5.0f)
        {
            virtualCamera_1Parlin.m_NoiseProfile = shake6D;
            virtualCamera2Parlin.m_NoiseProfile = shake6D;
            virtualCamera3Parlin.m_NoiseProfile = shake6D;

            virtualCamera_1Parlin.m_AmplitudeGain = _size;
            virtualCamera2Parlin.m_AmplitudeGain = _size;
            virtualCamera3Parlin.m_AmplitudeGain = _size;
        }
        else if (track_Course.m_Speed <= 10.0f && track_Course.m_Speed > 5.0f)
        {
            virtualCameraPerlin.m_NoiseProfile = shake6D;
            virtualCamera2Parlin.m_NoiseProfile = shake6D;
            virtualCamera3Parlin.m_NoiseProfile = shake6D;

            virtualCameraPerlin.m_AmplitudeGain = _size;
            virtualCamera2Parlin.m_AmplitudeGain = _size;
            virtualCamera3Parlin.m_AmplitudeGain = _size;
        }
        else if (track_Course.m_Speed > 10.0f)
        {
            virtualCamera_2Perlin.m_NoiseProfile = shake6D;
            virtualCamera2Parlin.m_NoiseProfile = shake6D;
            virtualCamera3Parlin.m_NoiseProfile = shake6D;

            virtualCamera_2Perlin.m_AmplitudeGain = _size;
            virtualCamera2Parlin.m_AmplitudeGain = _size;
            virtualCamera3Parlin.m_AmplitudeGain = _size;
        }
    }

    //자갈밭구간 효과
    void StonFarmSectionEffect(float _size)
    {
        if (track_Course.m_Speed <= 5.0f)
        {
            virtualCamera_1Parlin.m_NoiseProfile = shake6D;
            virtualCamera2Parlin.m_NoiseProfile = shake6D;
            virtualCamera3Parlin.m_NoiseProfile = shake6D;

            virtualCamera_1Parlin.m_AmplitudeGain = _size;
            virtualCamera2Parlin.m_AmplitudeGain = _size;
            virtualCamera3Parlin.m_AmplitudeGain = _size;
        }
        else if (track_Course.m_Speed <= 10.0f && track_Course.m_Speed > 5.0f)
        {
            virtualCameraPerlin.m_NoiseProfile = shake6D;
            virtualCamera2Parlin.m_NoiseProfile = shake6D;
            virtualCamera3Parlin.m_NoiseProfile = shake6D;

            virtualCameraPerlin.m_AmplitudeGain = _size;
            virtualCamera2Parlin.m_AmplitudeGain = _size;
            virtualCamera3Parlin.m_AmplitudeGain = _size;
        }
        else if (track_Course.m_Speed > 10.0f)
        {
            virtualCamera_2Perlin.m_NoiseProfile = shake6D;
            virtualCamera2Parlin.m_NoiseProfile = shake6D;
            virtualCamera3Parlin.m_NoiseProfile = shake6D;

            virtualCamera_2Perlin.m_AmplitudeGain = _size;
            virtualCamera2Parlin.m_AmplitudeGain = _size;
            virtualCamera3Parlin.m_AmplitudeGain = _size;
        }
    }

    float averSpeed;


    //칼로리 소모
    public void PlayKcal(float _time)
    {
        string t = TimeSpan.FromSeconds(_time).ToString("mm\\:ss\\:ff");
        string[] tokens = t.Split(':');
        float time = float.Parse(tokens[0] + "." + tokens[1]);
        currKcal = (float)((55 + 10) * cal * time);

        //currKcal = (float)((100) * cal * ((track_Course.m_Position * 0.001)));
        //currKcal = (float)((100) * cal * ((averSpeed * 0.05f)));
        currKcal = Mathf.FloorToInt(currKcal);
        //test2.text = "cal :: " + cal + " speed " + speed + "   : " + currKcal;
        a = currKcal;

        if (a > b && speed != 0 && oneState.Equals(false))
        {
            //Debug.Log("--- 들어옴");
            //test2.text = "1 들어옴";
            b = a;
            oneState = true;
        }
        else if (a == b && oneState == true && speed != 0)
        {
            //Debug.Log("--- 한번만");
            //test.text = "2 들어옴";
            finishKcal += 1;
            oneState = false;
        }
        //실시간으로 미션 클리어됫는지 확인하기 위함
        AsiaMap_DataManager.instance.TodayMapQuestKcalBurnUp(finishKcal);
        //게임 종류 시 퀘스트 결과를 저장한다.
        PlayerPrefs.SetString("AT_AfterQuestKcal", AsiaMap_DataManager.instance.TodayMapQuestKcalBurnUp(finishKcal));
        kcalText.text = finishKcal.ToString() + "kcal 소모";

    }

    //현재 칼로리 저장
    public void CurrKcalSave()
    {
        PlayerPrefs.SetFloat("AT_CurrKcal", finishKcal);
        kcalText.text = PlayerPrefs.GetFloat("AT_CurrKcal").ToString("N0") + "kcal 소모";
    }


    //플레이 거리
    public void PlayDistance()
    {
        distanceText.text = (track_Course.m_Position * 0.001).ToString("N2") + "km";

        float mapDistance = 0;

        if(PlayerPrefs.GetString("AT_MapCourseName") == "Normal-1" || PlayerPrefs.GetString("AT_MapCourseName") == "Hard-1")
        {
            mapDistance = 1.93f;
        }
        else if (PlayerPrefs.GetString("AT_MapCourseName") == "Normal-2" || PlayerPrefs.GetString("AT_MapCourseName") == "Hard-2")
        {
            mapDistance = 1.92f;
        }
        else if (PlayerPrefs.GetString("AT_MapCourseName") == "Normal-3" || PlayerPrefs.GetString("AT_MapCourseName") == "Hard-3")
        {
            mapDistance = 1.97f;
        }


        if (track_Course.m_Position * 0.001 >= (mapDistance - 0.5f)  && track_Course.m_Position * 0.001 < (mapDistance - 0.5f) + 0.03f)
        {
            FinishLineShow(true, false, false); //500미터 남았다.
        }
        else if(track_Course.m_Position * 0.001 >= (mapDistance - 0.5f) + 0.03f && track_Course.m_Position * 0.001 < (mapDistance - 0.3f))
        {
            FinishLineShow(false, false, false); // 500미터 표시 사라짐
        }
        else if(track_Course.m_Position * 0.001 >= (mapDistance - 0.3f) && track_Course.m_Position * 0.001 < (mapDistance - 0.3f) + 0.03f)
        {
            FinishLineShow(false, true, false); //300미터 남았다.
        }
        else if(track_Course.m_Position * 0.001 >= (mapDistance - 0.3f) + 0.03f && track_Course.m_Position * 0.001 < (mapDistance - 0.1f))
        {
            FinishLineShow(false, false, false); // 300미터 표시 사라짐
        }
        else if(track_Course.m_Position * 0.001 >= (mapDistance - 0.1f) && track_Course.m_Position * 0.001 < (mapDistance - 0.1f) + 0.03f)
        {
            FinishLineShow(false, false, true); //100미터 표시
        }
        else if(track_Course.m_Position * 0.001 >= (mapDistance - 0.1f) + 0.03f)
        {
            FinishLineShow(false, false, false); // 100미터 표시 사라짐
        }
    }

    //도착지점까지 남은 거리 보여주는 함수
    void FinishLineShow(bool _finish500, bool _finish300, bool _finish100)
    {
        finishLine[0].SetActive(_finish500);
        finishLine[1].SetActive(_finish300);
        finishLine[2].SetActive(_finish100);
    }


    //속도에 따른 파티클 조절
    public void particle_action()
    {
        if (sandState.Equals(true)) //모래 진입
        {
            dust_particle.startSize = 15f;
        }
        else
        {
            if (track_Course.m_Speed > 15f)
            {
                //    dust_particle.gameObject.SetActive(true); //파티클 활성화 시킴
                dust_particle.startSize = 10f;
            }
            else if (track_Course.m_Speed > 10f)
            {
                // dust_particle.gameObject.SetActive(true); //파티클 활성화 시킴
                dust_particle.startSize = 6f;
            }
            else if (track_Course.m_Speed == 0)
            {
                dust_particle.startSize = 0f;
            }
            else
            {
                dust_particle.startSize = 2f;
            }
        }
    }
}
