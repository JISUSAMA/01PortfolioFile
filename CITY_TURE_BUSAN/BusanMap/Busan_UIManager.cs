
using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Busan_UIManager : MonoBehaviour
{
    private BusanMapStringClass stringClass;
    public static Busan_UIManager instance { get; private set; }

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

    public GameObject lightObj; //조명 햇빛

    public GameObject[] hardModeObj;    //하드모드 장애물

    int expFull_i, expCurr_i;

    public bool gameStartState; //게임 시작 여부
    public bool gameEnd;    //게임 골인 여부
    public GameObject[] finishLine; //피니쉬라인 이미지 0:500, 1:300, 2:100

    //센서 쪽 변수
    GameObject itemDataBase;
    GameObject sensorManager;
    [SerializeField] Transform woman_player;
   //WomanCtrl womanctrl_scrip;    //220804수정
    [SerializeField] NewWomanCtrl womanctrl_scrip;   //220804수정
    [SerializeField] Transform man_player;
    //ManCtrl manctrl_scrip;    //220804수정
    NewManCtrl manctrl_scrip;   //220804수정

    public GameObject endPoint;   //도착 지점
    public GameObject parentObj;    //플레이어가 들어갈 부모

    public CinemachineVirtualCamera virtualCamera;
    public CinemachineVirtualCamera virtualCamera2;
    public CinemachineVirtualCamera virtualCamera3;

    CinemachineBasicMultiChannelPerlin virtualCameraPerlin;
    CinemachineBasicMultiChannelPerlin virtualCamera2Parlin;
    CinemachineBasicMultiChannelPerlin virtualCamera3Parlin;

    public NoiseSettings shake6D;
    public NoiseSettings handheld_tele_strong;

    public CinemachineDollyCart track_Course;   //트랙따라가는 카트
    public CinemachineSmoothPath sommthPath;  //트랙종류

    ParticleSystem dust_particle;    //자전거 먼저 파티클
    GameObject lightBeam;   //자전거 불빛 

    //카운트다운
    public Image CountDown_img;
    public Sprite[] CountDown_sprite;

    float cal;  //칼로리 계산을 위한 지수
    float currKcal; //현재 칼로리
    float a, b, finishKcal; //칼로리 들어오는 값 비교 a,b / 최종 칼로리
    bool oneState;  //한번만 들어가기 위한 bool변수

    public float speed;
    public bool mountainUpstate;    //오름막길 진입 여부
    public bool mountainDownState;  //내리막길 진입 여부
    public bool stonFarmState;  //자갈밭 진입 여부
    public bool puddleState;    //웅덩이 진입 여부
    public bool sandState;  //모래 진입 여부

    float averSpeed;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;


    }

    void Start()
    {
        stringClass = new BusanMapStringClass();
        Resources.UnloadUnusedAssets();
        PlayBGM();  //BGM시작
        virtualCameraPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        virtualCamera2Parlin = virtualCamera2.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        virtualCamera3Parlin = virtualCamera3.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        PlayerPrefs.SetFloat("Busan_CurrKcal", 0);    //현재 칼로리 초기화
        Initialization();

        Debug.Log("TUTORIAL : "+PlayerPrefs.GetString("Busan_InGameTutorial"));
        if (PlayerPrefs.GetString("Busan_InGameTutorial").Equals("End"))
            GameStart();
    }

    public void GameStart()
    {
        StartCoroutine(_GameStart());
        StartCoroutine(_EndPoint());
    }

    IEnumerator _GameStart()
    {
        Debug.Log("시작한다!!!!!");
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

        BusanMap_GameTime.instance.PlayTime();

        yield return null;
    }

    //BGM 시작
    void PlayBGM()
    {
        if (PlayerPrefs.GetString("Busan_MapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_NOR1) 
            || PlayerPrefs.GetString("Busan_MapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_HARD1))
            SoundMaixerManager.instance.InGameBGMPlay();
        else if (PlayerPrefs.GetString("Busan_MapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_NOR2) 
            || PlayerPrefs.GetString("Busan_MapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_HARD2))
            SoundMaixerManager.instance.InGameBGMNightPlay();
    }

    void Initialization()
    {
        Busan_DataManager.instance.ProfileImageChange(profileImg);

        if (PlayerPrefs.GetString("Busan_UseItemCoin").Equals("Coin"))
            itemUseIcon[0].SetActive(true);
        else itemUseIcon[0].SetActive(false);

        if (PlayerPrefs.GetString("Busan_UseItemExp").Equals("Exp"))
            itemUseIcon[1].SetActive(true);
        else itemUseIcon[1].SetActive(false);

        if (PlayerPrefs.GetString("Busan_UseItemSpeed").Equals("Speed"))
            itemUseIcon[2].SetActive(true);
        else itemUseIcon[2].SetActive(false);

        virtualCamera.Priority = 11;


        //하드모드 시 장애물 활성화
        if (PlayerPrefs.GetString("Busan_MapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_HARD1))
        {
            hardModeObj[0].SetActive(true);
        }
        else if (PlayerPrefs.GetString("Busan_MapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_HARD2))
        {
            hardModeObj[1].SetActive(true);
        }
        //Debug.LogError("PlayerPrefs :" + PlayerPrefs.GetString("Busan_Player_Sex"));
        //Debug.LogError("BUSAN_PLAYER_WOMAN :" + stringClass.BUSAN_PLAYER_WOMAN);
        //내정보 - 모델링 위치 초기화
        if (PlayerPrefs.GetString("Busan_Player_Sex") == stringClass.BUSAN_PLAYER_WOMAN)
        {
            Debug.Log("-----------------------------여자 모델링");
            woman_player = GameObject.Find(stringClass.BUSAN_PLAYER_WOMAN).GetComponent<Transform>();
            //womanctrl_scrip = woman_player.GetComponent<WomanCtrl>();
            womanctrl_scrip = woman_player.GetComponent<NewWomanCtrl>();    //220804수정

            woman_player.transform.parent = parentObj.transform;

            //내 정보 - 모델링 위치 초기화
            woman_player.localPosition = new Vector3(0.01f, -0.124f, -0.781f);
            woman_player.localRotation = Quaternion.Euler(0f, 0f, 0f);
            woman_player.localScale = new Vector3(1f, 1f, 1f);

            virtualCamera.Follow = woman_player.transform;
            virtualCamera.LookAt = woman_player.transform.GetChild(0).transform;

            virtualCamera2.Follow = woman_player.transform;
            virtualCamera2.LookAt = woman_player.transform.GetChild(0).transform;

            virtualCamera3.Follow = woman_player.transform;
            virtualCamera3.LookAt = woman_player.transform.GetChild(0).transform;

            //연기 활성화 220804수정 , 원래 7
            dust_particle = woman_player.transform.GetChild(3).GetComponent<ParticleSystem>();
            dust_particle.gameObject.SetActive(true);

            //라이트 활성화
            if(SceneManager.GetActiveScene().name.Equals("BusanMapNight"))
            {
                lightBeam = woman_player.transform.GetChild(4).gameObject;
                lightBeam.SetActive(true);
            }
        }
        else if (PlayerPrefs.GetString("Busan_Player_Sex") ==stringClass.BUSAN_PLAYER_MAN)
        {
            Debug.Log("-----------------------------남자 모델링");
            man_player = GameObject.Find(stringClass.BUSAN_PLAYER_MAN).GetComponent<Transform>();
            //manctrl_scrip = man_player.GetComponent<ManCtrl>();   //220804수정
            manctrl_scrip = man_player.GetComponent<NewManCtrl>();  //220804수정

            man_player.transform.parent = parentObj.transform;

            //내 정보 - 모델링 위치 초기화
            man_player.localPosition = new Vector3(0.01f,-0.124f, -0.781f);
            man_player.localRotation = Quaternion.Euler(0f, 0f, 0f);
            man_player.localScale = new Vector3(1f, 1f, 1f);

            virtualCamera.Follow = man_player.transform;
            virtualCamera.LookAt = man_player.transform.GetChild(0).transform;

            virtualCamera2.Follow = man_player.transform;
            virtualCamera2.LookAt = man_player.transform.GetChild(0).transform;

            virtualCamera3.Follow = man_player.transform;
            virtualCamera3.LookAt = man_player.transform.GetChild(0).transform;

            //연기 활성화 220804수정 , 원래 8
            dust_particle = man_player.transform.GetChild(3).GetComponent<ParticleSystem>();
            dust_particle.gameObject.SetActive(true);

            //라이트 활성화
            if (SceneManager.GetActiveScene().name.Equals("BusanMapNight"))
            {
                lightBeam = man_player.transform.GetChild(4).gameObject;
                lightBeam.SetActive(true);
            }
        }

        itemDataBase = GameObject.Find("DataManager");
        sensorManager = GameObject.Find("SensorManager");

        //UI초기화
        expCurr_i = PlayerPrefs.GetInt("Busan_Player_CurrExp");
        expFull_i = PlayerPrefs.GetInt("Busan_Player_Level") * (PlayerPrefs.GetInt("Busan_Player_Level") + 1) * 25;

        levelText.text = PlayerPrefs.GetInt("Busan_Player_Level").ToString();
        levelSlider.value = (float)expCurr_i / (float)expFull_i;
        //Debug.Log("로고 : : " + PlayerPrefs.GetString("Busan_Player_NickName"));
        nickNameText.text = PlayerPrefs.GetString("Busan_Player_NickName");
        timeText.text = "00:00:00.00";
        if (PlayerPrefs.GetString("Busan_CurrentMap_Name").Equals(stringClass.BUSAN_RED_LINE))
        {
            courseText.text = PlayerPrefs.GetString("Busan_MapCourseName");
        }
        else if (PlayerPrefs.GetString("Busan_CurrentMap_Name").Equals(stringClass.BUSAN_GREEN_LINE))
        {
            courseText.text = PlayerPrefs.GetString("Busan_GreenMapCourseName");
        }
        miniMapText.text = PlayerPrefs.GetFloat("Busan_MapCourseLength").ToString();
        speedText.text = "0km";
        kcalText.text = "0kcal";
        missionText.text = Busan_DataManager.instance.NextCourseOpenTimeMission() + ":00";
    }

    public void ParticleSystemOff()
    {
        if (PlayerPrefs.GetString("Busan_Player_Sex") == stringClass.BUSAN_PLAYER_WOMAN)
        {
            Busan_DataManager.instance.ParticleSystemOff(dust_particle);
            if(SceneManager.GetActiveScene().name.Equals("BusanMapNight"))
                Busan_DataManager.instance.LightBeamOff(lightBeam);
        } 
        else if (PlayerPrefs.GetString("Busan_Player_Sex") == stringClass.BUSAN_PLAYER_MAN)
        {
            Busan_DataManager.instance.ParticleSystemOff(dust_particle);
            if (SceneManager.GetActiveScene().name.Equals("BusanMapNight"))
                Busan_DataManager.instance.LightBeamOff(lightBeam);
        }
    }


    // Update is called once per frame
    void Update()
    {
        Particle_action();
    }

    //부모자식 분리. 다음 씬에 플레이어 보여주기 위함
    public void Parent_Division()
    {
        parentObj.transform.GetChild(1).transform.parent = null;

        if (PlayerPrefs.GetString("Busan_Player_Sex") == stringClass.BUSAN_PLAYER_WOMAN)
            DontDestroyOnLoad(woman_player);
        else if (PlayerPrefs.GetString("Busan_Player_Sex") == stringClass.BUSAN_PLAYER_MAN)
            DontDestroyOnLoad(man_player);
    }

    //메인카메라 변경 - 속도에 따른
    //public void SpeedCameraViewChange()
    //{
    //    if (track_Course.m_Speed > 10.0f)
    //    {
    //        virtualCamera.gameObject.SetActive(false);
    //    }
    //}

    //일시정지 시 스피드 = 0
    public void GamePlayStop()
    {
        speed = 0; //멈춰!
    }
    public void GamePlay()
    {
     //   speed = 10;
    }

    IEnumerator _EndPoint()
    {
        string str;
        if (PlayerPrefs.GetString("Busan_CurrentMap_Name").Equals(stringClass.BUSAN_RED_LINE))
        {
             str = PlayerPrefs.GetString("Busan_MapCourseName");
        }
        else if (PlayerPrefs.GetString("Busan_CurrentMap_Name").Equals(stringClass.BUSAN_GREEN_LINE))
        {
            str = PlayerPrefs.GetString("Busan_GreenMapCourseName");
        }
        //Debug.Log(str );

        yield return new WaitUntil(() => gameEnd.Equals(true));


        speed = 0; //멈춰!
        gameStartState = false;     //게임 시작 여부(끝)
        CurrKcalSave();  //현재 칼로리 저장


        string gameResult = BusanMap_GameTime.instance.Clear_FailAnim();


        if (gameResult.Equals("Clear"))
        {
            if (PlayerPrefs.GetString("Busan_Player_Sex").Equals(stringClass.BUSAN_PLAYER_WOMAN))
            {
                //womanctrl_scrip.AnimClear_Fail(1f, true, false, true, false, PlayerPrefs.GetInt("Busan_HairNumber"), PlayerPrefs.GetInt("Busan_BodyNumber"),
                // PlayerPrefs.GetInt("Busan_JacketNumber"), PlayerPrefs.GetInt("Busan_PantsNumber"), PlayerPrefs.GetInt("Busan_ShoesNumber"));
                womanctrl_scrip.AnimClear_Fail(1f, true, false, true, false);   //220804수정
            }
            else if (PlayerPrefs.GetString("Busan_Player_Sex").Equals(stringClass.BUSAN_PLAYER_MAN))
            {
                //manctrl_scrip.AnimClear_Fail(1f, true, false, true, false, PlayerPrefs.GetInt("Busan_HairNumber"), PlayerPrefs.GetInt("Busan_BodyNumber"),
                // PlayerPrefs.GetInt("Busan_JacketNumber"), PlayerPrefs.GetInt("Busan_PantsNumber"), PlayerPrefs.GetInt("Busan_ShoesNumber"));
                manctrl_scrip.AnimClear_Fail(1f, true, false, true, false); //220804수정
            }
        }
        else if (gameResult.Equals("Fail"))
        {
            if (PlayerPrefs.GetString("Busan_Player_Sex").Equals(stringClass.BUSAN_PLAYER_WOMAN))
            {
                //womanctrl_scrip.AnimClear_Fail(1f, true, false, true, false, PlayerPrefs.GetInt("Busan_HairNumber"), PlayerPrefs.GetInt("Busan_BodyNumber"),
                // PlayerPrefs.GetInt("Busan_JacketNumber"), PlayerPrefs.GetInt("Busan_PantsNumber"), PlayerPrefs.GetInt("Busan_ShoesNumber"));
                womanctrl_scrip.AnimClear_Fail(1f, true, false, true, false);   //220804수정
            }
            else if (PlayerPrefs.GetString("Busan_Player_Sex").Equals(stringClass.BUSAN_PLAYER_MAN))
            {
                //manctrl_scrip.AnimClear_Fail(1f, true, false, true, false, PlayerPrefs.GetInt("Busan_HairNumber"), PlayerPrefs.GetInt("Busan_BodyNumber"),
                // PlayerPrefs.GetInt("Busan_JacketNumber"), PlayerPrefs.GetInt("Busan_PantsNumber"), PlayerPrefs.GetInt("Busan_ShoesNumber"));
                manctrl_scrip.AnimClear_Fail(1f, true, false, true, false); //220804수정
            }
        }
        yield return null;
    }



    //현재 칼로리 저장
    public void CurrKcalSave()
    {
        PlayerPrefs.SetFloat("Busan_CurrKcal", finishKcal);
        kcalText.text = PlayerPrefs.GetFloat("Busan_CurrKcal").ToString("N0") + "kcal";
    }


    //현재 스피드
    public string MySpeed(float _time)
    {
        //if (PlayerPrefs.GetString("Busan_Player_Sex").Equals("Woman"))
        //    speed = Woman_Move.instance.speed;
        //else if (PlayerPrefs.GetString("Busan_Player_Sex").Equals("Mon"))
        //    speed = Man_Move.instance.speed;

        //Text text1 = GameObject.Find("Text (1)").GetComponent<Text>();
        //text1.text = "시피드 ---- " + speed;

        

        if (PlayerPrefs.GetString("Busan_UseItemSpeed").Equals("Speed"))
        {
            if (mountainUpstate.Equals(true))    //산 진입
            {
                track_Course.m_Speed = (speed * 1.2f) * 0.7f;   //속도 반감
            }
            else if (mountainDownState.Equals(true)) //내릿막길
            {
                track_Course.m_Speed = (speed * 1.2f) * 1.05f;   //속도 업
            }
            else if (stonFarmState.Equals(true)) //자갈밭진입
            {
                track_Course.m_Speed = (speed * 1.2f) * 0.7f;   //속도 반감
            }
            else if (puddleState.Equals(true))   //웅덩이 진입
            {
                track_Course.m_Speed = (speed * 1.2f) * 0.8f;   //속도 반감
            }
            else if (sandState.Equals(true)) //모래 진입
            {
                track_Course.m_Speed = (speed * 1.2f) * 0.7f;   //속도 반감
            }
            else
                track_Course.m_Speed = speed * 1.2f;

            //test.text = "track_Course.m_Speed " + track_Course.m_Speed + " speed " + speed;

            //자갈밭, 웅덩이 진입 놉
            if (stonFarmState.Equals(false) && puddleState.Equals(false))
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
            else if (sandState.Equals(true)) //모래
            {
                SandSectionEffect(1f);
            }
        }
        else
        {
            if (mountainUpstate.Equals(true))    //산 진입
                track_Course.m_Speed = speed * 0.7f;   //속도 반감
            else if (mountainDownState.Equals(true)) //내릿막길
                track_Course.m_Speed = speed * 1.05f;   //속도 업
            else if (stonFarmState.Equals(true)) //자갈밭진입
                track_Course.m_Speed = speed * 0.7f;   //속도 반감
            else if (puddleState.Equals(true))   //웅덩이 진입
                track_Course.m_Speed = speed * 0.8f;   //속도 반감
            else if (sandState.Equals(true)) //모래 진입
                track_Course.m_Speed = speed * 0.7f;   //속도 반감
            else
            {
                track_Course.m_Speed = speed;
            }
                


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
            else if (sandState.Equals(true)) //모래
            {
                //Debug.Log("여기 들어옵니다.");
                SandSectionEffect(1f);
            }
        }


        //현재 스피드
        float currSpeed = Busan_DataManager.instance.MySpeed(track_Course.m_Speed);
        string speedStr = currSpeed.ToString("N2");

        Busan_DataManager.instance.MaxSpeed(speedStr);    //최고속도 저장


        //Debug.Log("콜로리 : " + track_Course.m_Position + "  시간 : " + _time + " 합 ; " + track_Course.m_Position / _time);
        //평균 스피드 구하기
        float _averSpeed = track_Course.m_Position / _time;
        averSpeed = Busan_DataManager.instance.MySpeed(_averSpeed);

        //칼로리 구하기
        cal = Mathf.Lerp(0, 0.46f, Mathf.InverseLerp(0, 80f, averSpeed));
        //test.text = "칼로리 :: " + cal + " 스피드 " + averSpeed +"   : " + currSpeed;
        return speedStr;
    }

    //모래구간 효과
    void SandSectionEffect(float _size)
    {
        virtualCameraPerlin.m_NoiseProfile = handheld_tele_strong;
        virtualCamera2Parlin.m_NoiseProfile = handheld_tele_strong;
        virtualCamera3Parlin.m_NoiseProfile = handheld_tele_strong;

        virtualCameraPerlin.m_AmplitudeGain = _size;
        virtualCamera2Parlin.m_AmplitudeGain = _size;
        virtualCamera3Parlin.m_AmplitudeGain = _size;
    }

    //웅덩이구간 효과
    void PuddleSectionEffect(float _size)
    {
        virtualCameraPerlin.m_NoiseProfile = shake6D;
        virtualCamera2Parlin.m_NoiseProfile = shake6D;
        virtualCamera3Parlin.m_NoiseProfile = shake6D;

        virtualCameraPerlin.m_AmplitudeGain = _size;
        virtualCamera2Parlin.m_AmplitudeGain = _size;
        virtualCamera3Parlin.m_AmplitudeGain = _size;
    }

    //자갈밭구간 효과
    void StonFarmSectionEffect(float _size)
    {
        virtualCameraPerlin.m_NoiseProfile = shake6D;
        virtualCamera2Parlin.m_NoiseProfile = shake6D;
        virtualCamera3Parlin.m_NoiseProfile = shake6D;

        virtualCameraPerlin.m_AmplitudeGain = _size;
        virtualCamera2Parlin.m_AmplitudeGain = _size;
        virtualCamera3Parlin.m_AmplitudeGain = _size;
    }

    //칼로리 소모
    public void PlayKcal(float _time)
    {
        string t = TimeSpan.FromSeconds(_time).ToString("mm\\:ss\\:ff");
        string[] tokens = t.Split(':');
        float time = float.Parse(tokens[0] + "." + tokens[1]);

        //Debug.Log("시간 : " + tokens[0] + " : " + tokens[1] + " : " + tokens[2] + " :: " + time);
        currKcal = (float)((55 + 10) * cal * time);

        //currKcal = (float)((100) * cal * ((track_Course.m_Position * 0.001)));
        //currKcal = (float)((100) * cal * ((averSpeed * 0.05f)));
        currKcal = Mathf.FloorToInt(currKcal);

        
        //test2.text = "cal :: " + cal + " speed " + speed + "   : " + currKcal;
        a = currKcal;

        //Debug.Log("currKcal : " + currKcal + "a : " + a + " b : "+ b);
        if (a > b && speed != 0 && oneState.Equals(false))
        {
            //Debug.Log("--- 들어옴");
            //test2.text = "1 들어옴";
            b = a;
            oneState = true;
        }
        else if (a == b && oneState == true && speed != 0)
        {
            finishKcal += 1;
            oneState = false;
        }
        //Debug.Log("---------------- " + finishKcal);
        //실시간으로 미션 클리어됫는지 확인하기 위함
        Busan_DataManager.instance.TodayMapQuestKcalBurnUp(finishKcal);
        //게임 종류 시 퀘스트 결과를 저장한다.
        PlayerPrefs.SetString("Busan_AfterQuestKcal", Busan_DataManager.instance.TodayMapQuestKcalBurnUp(finishKcal));
        kcalText.text = finishKcal.ToString() + "kcal";
    }


    //플레이 거리
    public void PlayDistance()
    {
        distanceText.text = (track_Course.m_Position * 0.001).ToString("N2") + "km";

        float mapDistance = 2.05f;



        if (track_Course.m_Position * 0.001 >= (mapDistance - 0.5f) && track_Course.m_Position * 0.001 < (mapDistance - 0.5f) + 0.03f)
        {
            FinishLineShow(true, false, false); //500미터 남았다.
        }
        else if (track_Course.m_Position * 0.001 >= (mapDistance - 0.5f) + 0.03f && track_Course.m_Position * 0.001 < (mapDistance - 0.3f))
        {
            FinishLineShow(false, false, false); // 500미터 표시 사라짐
        }
        else if (track_Course.m_Position * 0.001 >= (mapDistance - 0.3f) && track_Course.m_Position * 0.001 < (mapDistance - 0.3f) + 0.03f)
        {
            FinishLineShow(false, true, false); //300미터 남았다.
        }
        else if (track_Course.m_Position * 0.001 >= (mapDistance - 0.3f) + 0.03f && track_Course.m_Position * 0.001 < (mapDistance - 0.1f))
        {
            FinishLineShow(false, false, false); // 300미터 표시 사라짐
        }
        else if (track_Course.m_Position * 0.001 >= (mapDistance - 0.1f) && track_Course.m_Position * 0.001 < (mapDistance - 0.1f) + 0.03f)
        {
            FinishLineShow(false, false, true); //100미터 표시
        }
        else if (track_Course.m_Position * 0.001 >= (mapDistance - 0.1f) + 0.03f)
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

    //햇빛 방향 조정
    public void LightRotionMove(float _x, float _y, float _z)
    {
        lightObj.transform.localRotation = Quaternion.Euler(_x, _y, _z);
    }

    private void OnDestroy()
    {
        Resources.UnloadUnusedAssets();
    }

    //속도에 따른 파티클 조절
    public void Particle_action()
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
