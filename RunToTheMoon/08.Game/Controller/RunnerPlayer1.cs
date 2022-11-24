using System;
using UnityEngine;
using System.Collections;
using TunnelEffect;
using AmazingAssets.CurvedWorld;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
using UnityEngine.SceneManagement;

public class RunnerPlayer1 : MonoBehaviour
{
    [Header("Player")]
    // +- Values
    [Range(0f, 1.1f)] public float input_Speed;

    public float y_acceleration = 0.008f;
    //public float x_acceleration = 0.006f;
    public float booster = 0.003f;

    // Input Speed Values
    float speedDecrease = 2f;
    //float direction = 0f;

    //센서 연결
    public float[] mpu_a_value;
    public float[] mpu_q_value;
    float curr_rateX, curr_rateY, curr_rateZ;
    float _curr_rateX, _curr_rateY, _curr_rateZ;
    float before_rateX, before_rateY, before_rateZ;
    float gapX, gapY, gapZ;
    float timeNum;
    bool stop;

    public float speed = 0;

    public bool connetState;

    public bool StoryEventing = false; //2021.08.27 //이벤트 진행중 일 때 , 속도 0으로 만들음
    public bool Tutorial_ing = false;   //튜토리얼이 있을 경우, 튜토리얼 중인지 확인함
    // Player Front Speed Values
    float boosterState = 1.1f;
    float runState = 0.7f;
    float walkState = 0.4f;

    // 스피드 제어
    public Animator anim;

    // 포탈, 달 제어
    //[SerializeField] GameObject moonObPos;

    Transform moonDistance;
    Vector3 playerDistance;
    //플레이어 콜라이더
    public Collider playerCol;
    // Idle Option Time
    public float leftTime = 5f;
    float restTime = 0f;

    // current coin, stepcount
    int current_coin;
    int max_coin = 1000000;

    // Anim Exit Time
    float exitTime = 1.0f;

    // 실제 사람 보폭
    float realTime_Walk = 0.00004f;
    float realTime_Run = 0.00008f;
    float realTime_Spurt = 0.00010f;


    // 칼로리 계산용 시간
    float kcal;
    float current_Kcal;
    float mets = 4.8f;

    [Header("Pedometer")]
    float currentAcceleration = 0F;
    float averageAcceleration = 0F;

    //private Vector3 accel;
    private bool stateHigh = false;

    [Header("Player Cart")]
    public CinemachineDollyCart playerCart;
    public float playerPosition;
    public CameraViewPort _cameraView;
    [Header("Volume")]
    // Global Volume
    public Volume Object_Volume;
    public VolumeProfile Object_profile;

    [Header("Eyes")]
    public GameObject blinkEyes;
    Vignette vn;
    public Image fade_Image;
    public float FadeRate;
    float targetAlpha;

    [Header("Item Values")]
    public int spaceFlower;
    public int colorlessStar;
    public int bigOxygenTank;
    public int starDust;
    public int snack;
    public int canndedFood;

    [Header("Item Max Values")]
    public int maxSpaceFlower;
    public int maxColorlessStar;
    public int maxBigOxygenTank;
    public int maxStarDust;
    public int maxSnack;
    public int maxCanndedFood;

    public ItemDescriptionManager ItemDescriptionManager;
    [Header("Player State Material")]
    public SkinnedMeshRenderer player_StarDust_state;
    public Material[] _starDust_states;
    public GameObject StarDustParticle;

    public SkinnedMeshRenderer player_Ox_state;
    public Material[] _Ox_states;
    public SkinnedMeshRenderer player_OxBar_state;
    public Material[] _OxBar_states;
    //  public GameObject OxBarParticle;

    public bool use_starDust = false; //별가루 사용했는지 확인

    public bool use_moonPowder = false; //달가루 사용했는지 확인 , true: 파티클 활성화 - false : 파티클 비활성화
    public bool moonPowder_event_Section = false; //달가루를 주웠을 경우, 아이템 창에 활성화 시킴
    public bool moonPowder_useEvnet = false; //true : 달가루 사용 구간, false : 달가루 사용 불가능 한 구간, 알림창
    public GameObject MoonPowder_particle; //달가루 파티클 


    public bool O2_eventCheck = false;//독포자, 통로 등 산소 깎임 이벤트 진행 중인지 확인
    public bool stepJumpCheck = false; //캐릭터 점프 확인
    private bool jumpState = false;     // 점프 상태 계속받기위함
    private bool isOnce = true;

    public float stopTime;

    public Animator Rune_Animation;

    [SerializeField] private TMP_Text stateText1;
    [SerializeField] private TMP_Text stateText2;
    [SerializeField] private TMP_Text stateText3;
    [SerializeField] private TMP_Text stateText4;

    [SerializeField] private TMP_Text stateText5;
    [SerializeField] private TMP_Text stateText6;
    [SerializeField] private TMP_Text stateText7;

    //private bool isStep = false;
    //private int steps = 0;
    private Quaternion inverseQt;
    private Quaternion rawQt;
    [SerializeField] private Transform cube;

    [Header("Debug")]
    public Toggle debugToggle;
    public CinemachineSmoothPath cinemachineSmoothPath;

    //public CinemachineDollyCart cinemachineDollyCart;
    //Angular Velocity
    //Quaternion previousRotation; //전 프레임의 로테이션 값
    //Vector3 angularVelocity; //각속도를 관리할 변수

    public static RunnerPlayer1 instance { get; private set; }
    enum Idle_State
    {
        Idle_2,
        Idle_3,
        Behind
    }

    private void DebuggingMode(Toggle m_toggle)
    {
        if (m_toggle.isOn)
        {
            realTime_Walk = 0.00004f * 5f;
            realTime_Run = 0.00008f * 10f;
            realTime_Spurt = 0.00010f * 10f;
        }
        else
        {
            realTime_Walk = 0.00004f;
            realTime_Run = 0.00008f;
            realTime_Spurt = 0.00010f;
        }
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        //Fall_asleep();
        //WakeUp();

        debugToggle.onValueChanged.AddListener(delegate
        {
            DebuggingMode(debugToggle);
        });

        if (debugToggle.isOn)
        {
            realTime_Walk = 0.00004f * 5f;
            realTime_Run = 0.00008f * 10f;
            realTime_Spurt = 0.00010f * 10f;
        }
        else
        {
            realTime_Walk = 0.00004f;
            realTime_Run = 0.00008f;
            realTime_Spurt = 0.00010f;
        }

        //#if __DEBUG__
        //    //// 실제 사람 보폭
        //    //float realTime_Walk = 0.00004f;
        //    //float realTime_Run = 0.00008f;
        //    //float realTime_Spurt = 0.00010f;
        //    realTime_Walk = 0.00004f * 5f;
        //    realTime_Run = 0.00008f * 10f;
        //    realTime_Spurt = 0.00010f * 10f;
        //#endif

        stepJumpCheck = false;

        mpu_q_value = new float[4];     // 4개 초기화

        for (int i = 0; i < mpu_q_value.Length; i++)
        {
            mpu_q_value[i] = 99f;
        }
        //cube = GameObject.Find("Cube001").GetComponent<Transform>();
        //cinemachineDollyCart = GameObject.Find("PlayerCart").GetComponent<CinemachineDollyCart>();
        //cinemachineSmoothPath = GameObject.Find("PlayerTrack").GetComponent<CinemachineSmoothPath>();

        inverseQt = Quaternion.identity;

        //steps = 0;

        //accel = new Vector3(mpu_q_value[0], mpu_q_value[1], mpu_q_value[2]);

        //mpu_value[0~2] : -2000 ~ 0 ~ 2000 사이값

        // 원본
        //curr_rateX = Mathf.InverseLerp(-32768f, 32768f, mpu_value[0] - 4096f);
        //curr_rateY = Mathf.InverseLerp(-32768f, 32768f, mpu_value[1] - 4096f);
        //curr_rateZ = Mathf.InverseLerp(-32768f, 32768f, mpu_value[2] - 4096f);

        //before_rateX = (float)(System.Math.Truncate(curr_rateX * 1000) / 1000);  //소수점 이하 3자리 버리기
        //before_rateY = (float)(System.Math.Truncate(curr_rateY * 1000) / 1000);
        //before_rateZ = (float)(System.Math.Truncate(curr_rateZ * 1000) / 1000);

        // 수정중 ////////////////
        //curr_rateX = mpu_q_value[0];
        //curr_rateY = mpu_q_value[1];
        //curr_rateZ = mpu_q_value[2];
        //before_rateX = curr_rateX;
        //before_rateY = curr_rateY;
        //before_rateZ = curr_rateZ;
        //////////////////////////

        current_coin = Game_DataManager.instance.coin;
        current_Kcal = Game_DataManager.instance.todayKcal;
        //averageAcceleration = accel.magnitude;

        //Object_Volume.profile = Object_profile;
        
        if (SceneManager.GetActiveScene().name == "Game 17")
        {
            /*/
            realTime_Walk = 0.00004f;
            realTime_Run = 0.00008f;
            realTime_Spurt = 0.00010f;*/

            realTime_Walk = 0.00002f;
            realTime_Run = 0.00004f;
            realTime_Spurt = 0.00005f;
        }
    }

    int test1;
    int test2;
    int test3;
    int test4;

    public void resetRotation()
    {
        inverseQt = Quaternion.Inverse(rawQt);
    }

    [SerializeField]
    private Vector3 _estimatedVelocity = Vector3.zero;

    [SerializeField]
    private Vector3 _estimatedAngularVelocity = Vector3.zero;

    [SerializeField]
    private float _estimatedAvgAngularVelocity = 0f;

    private Vector3 _positionPrevious = Vector3.zero;
    private Quaternion _rotationPrevious = Quaternion.identity;

    public Vector3 EstimatedVelocity
    {
        get { return _estimatedVelocity; }
    }

    public Vector3 EstimatedAngularVelocity
    {
        get { return _estimatedAngularVelocity; }
    }

    private float AverageAngularVelocity
    {
        get { return _estimatedAvgAngularVelocity; }
    }

    private void FixedUpdate()
    {
        if (!Game_DataManager.instance.runEndState && Game_DataManager.instance.gamePlaying)
        {
            _estimatedVelocity = (cube.position - _positionPrevious) / Time.deltaTime;

            _positionPrevious = cube.position;

            Quaternion deltaRotation = Quaternion.Inverse(_rotationPrevious) * cube.rotation;

            deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

            float angularSpeed = (angle * Mathf.Deg2Rad) / Time.deltaTime;
            _estimatedAngularVelocity = axis * angularSpeed;

            //_estimatedAvgAngularVelocity = (Mathf.Abs(_estimatedAngularVelocity.x) + Mathf.Abs(_estimatedAngularVelocity.y) + Mathf.Abs(_estimatedAngularVelocity.z)) / 3f;
            _estimatedAvgAngularVelocity = Mathf.Sqrt(_estimatedAngularVelocity.x * _estimatedAngularVelocity.x + _estimatedAngularVelocity.y * _estimatedAngularVelocity.y + _estimatedAngularVelocity.z * _estimatedAngularVelocity.z);

            _rotationPrevious = cube.rotation;
        }
    }

    // 플레이어 애니, 타일, 터널 제어
    private void Update()
    {
        Game_DataManager.instance.coin = Game_DataManager.instance.before_coin + ((int)Game_DataManager.instance.once_Kcal * 500);
        if (!Game_DataManager.instance.runEndState && Game_DataManager.instance.gamePlaying)
        {

            //////////////////////////////////////////////////////////////////////////////////
            //uiCanvas = GameObject.Find("UICanvas");

            // 발을 밝을 때 +
            // 뗄때 -
            rawQt = new Quaternion(mpu_q_value[0], mpu_q_value[1], mpu_q_value[2], mpu_q_value[3]);

            cube.rotation = rawQt * inverseQt;

            //accel = new Vector3(mpu_q_value[0], mpu_q_value[1], mpu_q_value[2]);

            //currentAcceleration = Mathf.Lerp(currentAcceleration, accel.magnitude, Time.deltaTime * 10.0F);
            //averageAcceleration = Mathf.Lerp(averageAcceleration, accel.magnitude, Time.deltaTime * 0.1F);

            //float delta = currentAcceleration - averageAcceleration; // Gets the acceleration pulses.

            //// 걸음수 체크
            //if (!stateHigh)
            //{
            //    if (input_Speed >= 0.4)
            //    {
            //        if (delta > 0)
            //        {
            //            stateHigh = true;
            //            steps++; // Counts the steps when the comparator goes to high.
            //            Game_DataManager.instance.once_stepCount = steps;
            //        }
            //    }
            //}
            //else
            //{
            //    if (delta < 0)
            //    {
            //        stateHigh = false;
            //    }
            //}

            // 입력값 처리 : 들어오는지 안오는지
            if (mpu_q_value[0] >= -1 && mpu_q_value[0] <= 1 || mpu_q_value[1] >= -1 && mpu_q_value[1] <= 1 || mpu_q_value[2] >= -1 && mpu_q_value[2] <= 1)
            {

                Debug.Log("0 : " + mpu_q_value[0] + "1 : " + mpu_q_value[1]);
                //stateText1.text = "cube velocity x : " + EstimatedAngularVelocity.x;
                //stateText2.text = "cube velocity y : " + EstimatedAngularVelocity.y;
                //stateText3.text = "cube velocity z : " + EstimatedAngularVelocity.z;

                if (AverageAngularVelocity > 0f && AverageAngularVelocity <= 2f)
                {
                    //stateText1.text = "1 : " + test1++ + "/ velocity : " + AverageAngularVelocity;

                    // Stop : 어떠한 상태든 점차 속도가 줄어야함

                    stopTime += Time.deltaTime;

                    if (stopTime > 0.5f)
                    {
                        input_Speed -= 0.05f;
                    }

                    if (input_Speed < 0) { input_Speed = 0; stopTime = 0f; }

                    Debug.Log("speed : " + speed);
                }
                else if (AverageAngularVelocity > 2f && AverageAngularVelocity <= 6f)
                {
                    // Moving - Walk
                    // 걷기만 되어야함
                    // Stop->Walk , Spurt->Run->Walk, Run->Walk

                    //stateText2.text = "2 : " + test2++ + "/ velocity : " + AverageAngularVelocity;
                    input_Speed = 0.4f;
                }
                else if (AverageAngularVelocity > 6f && AverageAngularVelocity <= 10f)
                {
                    // Run
                    //stateText3.text = "3 : " + test3++ + "/ velocity : " + AverageAngularVelocity;

                    input_Speed = 0.7f;

                    if (input_Speed >= 1.1) { input_Speed = 1.0f; }
                }
                else if (AverageAngularVelocity > 10f)
                {
                    // Spurt
                    input_Speed = 1.1f;

                    //stateText4.text = "4 : " + test4++ + "/ velocity : " + AverageAngularVelocity;
                }
            }

            //2021-11-19
            if (StoryEventing.Equals(true))
            {
                //  Game_DataManager.instance.runEndState = true;     // 멈춤
                input_Speed = 0f; //이벤트 진행중 일때 안움직임
                speed = 0f;
                stepJumpCheck = false;
            }
            else
            {
                // Game_DataManager.instance.runEndState = false;     // 달리는중
            }

            if (!stepJumpCheck)
            {
                // Walk : 0.4 , Run : 0.7, Booster : 1.1
                if (input_Speed >= 0f)    // 센서 입력값 받음, 딱 0 이 안나올수 있음. 걷기부터 시작
                {
                    // 걸을 때 ---------------------------------------------------------------------------------------------------------------------------------------
                    if (input_Speed >= 0.4f && input_Speed < 0.7f)  
                    {
                        kcal += Time.deltaTime;

                        speed = speed <= walkState ? speed += y_acceleration :
                            speed = walkState < speed && speed <= runState ? speed -= y_acceleration : speed;

                        //playerCart.m_Speed = speed;

                        //별가루 사용 중 아닐경우,
                        if (!use_starDust)
                        {
                            if (Game_DataManager.instance.moonDis > 0)
                                Game_DataManager.instance.moonDis -= realTime_Walk;
                            else
                                Game_DataManager.instance.moonDis = 0f;

                            if (Game_DataManager.instance.spaceStationDis > 0)
                                Game_DataManager.instance.spaceStationDis -= realTime_Walk;
                            else
                                Game_DataManager.instance.spaceStationDis = 0f;

                        }
                        //별가루 사용 중 일 경우, 걸음의 2배 적용 
                        else
                        {
                            if (Game_DataManager.instance.moonDis > 0)
                                Game_DataManager.instance.moonDis -= realTime_Walk * 2;
                            else
                                Game_DataManager.instance.moonDis = 0f;

                            if (Game_DataManager.instance.spaceStationDis > 0)
                                Game_DataManager.instance.spaceStationDis -= realTime_Walk * 2;
                            else
                                Game_DataManager.instance.spaceStationDis = 0f;

                        }

                        // Debug.Log("1. 달까지 거리 : "+Game_DataManager.instance.spaceStationDis);
                        // 비율에 맞춘 거리
                        if (SceneManager.GetActiveScene().name == "Game 3" ||
                            SceneManager.GetActiveScene().name == "Game 11")
                        {
                            playerPosition = Mathf.Lerp(0f, cinemachineSmoothPath.PathLength, Mathf.InverseLerp(0f, 5f, 5f - Game_DataManager.instance.spaceStationDis));
                            playerCart.m_Position = playerPosition;
                        }
                        else
                        {
                            playerPosition = Mathf.Lerp(0f, cinemachineSmoothPath.PathLength * 2, Mathf.InverseLerp(0f, 5f, 5f - Game_DataManager.instance.spaceStationDis));
                            playerCart.m_Position = playerPosition;
                        }

                        // 소비 에너지 (㎉) = 1.05 x 엑서사이즈 (METs x 시간) x 체중 (㎏)
                        float kcal_calc = 1.05f * (mets * (kcal / 3600)) * 65;
                        //Game_DataManager.instance.todayKcal = current_Kcal + kcal_calc;

                        // 코인 - 나중에 걸음수로 바꾸기
                        //Game_DataManager.instance.coin = current_coin + Game_DataManager.instance.today_stepCount_inGame;
                        Game_DataManager.instance.once_Kcal = kcal_calc;
                    }
                    // 뛸 때 ----------------------------------------------------------------------------------------------------------------------------------------------------
                    else if (input_Speed >= 0.7f && input_Speed < 1.1f) 
                    {
                        kcal += Time.deltaTime * 1.5f;

                        speed = speed <= runState ? speed += y_acceleration :
                            speed = runState < speed && speed <= boosterState ? speed -= y_acceleration : speed;


                        //playerCart.m_Speed = speed * 5f;
                        //별가루 사용 중 아닐경우,
                        if (!use_starDust)
                        {
                            if (Game_DataManager.instance.moonDis > 0)
                                Game_DataManager.instance.moonDis -= realTime_Run;
                            else
                                Game_DataManager.instance.moonDis = 0f;

                            if (Game_DataManager.instance.spaceStationDis > 0)
                                Game_DataManager.instance.spaceStationDis -= realTime_Run;
                            else
                                Game_DataManager.instance.spaceStationDis = 0f;
                        }
                        //별가루 사용 중 일 경우, 걸음의 2배 적용 
                        else
                        {
                            if (Game_DataManager.instance.moonDis > 0)
                                Game_DataManager.instance.moonDis -= realTime_Run * 2;
                            else
                                Game_DataManager.instance.moonDis = 0f;

                            if (Game_DataManager.instance.spaceStationDis > 0)
                                Game_DataManager.instance.spaceStationDis -= realTime_Run * 2;
                            else
                                Game_DataManager.instance.spaceStationDis = 0f;
                        }

                        if (SceneManager.GetActiveScene().name == "Game 3" ||
                            SceneManager.GetActiveScene().name == "Game 11")
                        {
                            playerPosition = Mathf.Lerp(0f, cinemachineSmoothPath.PathLength, Mathf.InverseLerp(0f, 5f, 5f - Game_DataManager.instance.spaceStationDis));
                            playerCart.m_Position = playerPosition;
                        }
                        else
                        {
                            playerPosition = Mathf.Lerp(0f, cinemachineSmoothPath.PathLength * 2, Mathf.InverseLerp(0f, 5f, 5f - Game_DataManager.instance.spaceStationDis));
                            playerCart.m_Position = playerPosition;
                        }

                        // 소비 에너지 (㎉) = 1.05 x 엑서사이즈 (METs x 시간) x 체중 (㎏)
                        float kcal_calc = 1.05f * (mets * (kcal / 3600)) * 65;
                        //Game_DataManager.instance.todayKcal = current_Kcal + kcal_calc;

                        // 코인 - 나중에 걸음수로 바꾸기
                        // Game_DataManager.instance.coin = current_coin + Game_DataManager.instance.today_stepCount_inGame;
                        Game_DataManager.instance.once_Kcal = kcal_calc;
                    }
                    // 전력질주 ------------------------------------------------------------------------------------------------------------------------------------------------------
                    else if (input_Speed >= 1.1f)  
                    {
                        kcal += Time.deltaTime * 2f;

                        // Booster
                        speed += (y_acceleration + booster);

                        //playerCart.m_Speed = speed * 7f;

                        //별가루 사용 중 아닐경우,
                        if (!use_starDust)
                        {
                            if (Game_DataManager.instance.moonDis > 0)
                                Game_DataManager.instance.moonDis -= realTime_Spurt;
                            else
                                Game_DataManager.instance.moonDis = 0;

                            if (Game_DataManager.instance.spaceStationDis > 0)
                                Game_DataManager.instance.spaceStationDis -= realTime_Spurt;
                            else
                                Game_DataManager.instance.spaceStationDis = 0;
                        }
                        //별가루 사용 중 일 경우, 걸음의 2배 적용 
                        else
                        {
                            if (Game_DataManager.instance.moonDis > 0)
                                Game_DataManager.instance.moonDis -= realTime_Spurt * 2;
                            else
                                Game_DataManager.instance.moonDis = 0;

                            if (Game_DataManager.instance.spaceStationDis > 0)
                                Game_DataManager.instance.spaceStationDis -= realTime_Spurt * 2;
                            else
                                Game_DataManager.instance.spaceStationDis = 0;
                        }
                        //달가루 사용 이벤트가 진행 중이 아닐 경우, 
                        //달가루 사용 했을 경우, 파티클 활성화 
                        if (!use_moonPowder)
                        {
                            MoonPowder_particle.SetActive(false);
                        }
                        else
                        {
                            MoonPowder_particle.SetActive(true);
                        }

                        //Debug.Log("3. 달까지 거리 : " + Game_DataManager.instance.spaceStationDis);
                        if (SceneManager.GetActiveScene().name == "Game 3" ||
                            SceneManager.GetActiveScene().name == "Game 11")
                        {
                            playerPosition = Mathf.Lerp(0f, cinemachineSmoothPath.PathLength, Mathf.InverseLerp(0f, 5f, 5f - Game_DataManager.instance.spaceStationDis));
                            playerCart.m_Position = playerPosition;
                        }
                        else
                        {
                            playerPosition = Mathf.Lerp(0f, cinemachineSmoothPath.PathLength * 2, Mathf.InverseLerp(0f, 5f, 5f - Game_DataManager.instance.spaceStationDis));
                            playerCart.m_Position = playerPosition;
                        }

                        // 소비 에너지 (㎉) = 1.05 x 엑서사이즈 (METs x 시간) x 체중 (㎏)
                        float kcal_calc = 1.05f * (mets * (kcal / 3600)) * 65;
                        //Game_DataManager.instance.todayKcal = current_Kcal + kcal_calc;
                        Game_DataManager.instance.once_Kcal = kcal_calc;

                        // 코인 - 나중에 걸음수로 바꾸기
                       // Game_DataManager.instance.coin = current_coin + Game_DataManager.instance.`;
                    

                    }
                    else
                    {
                        //kcal = 0f; // 수정

                        speed -= y_acceleration * speedDecrease * 2f;

                        if (speed < 0f) { speed = 0f; }
                    }
                }
                else
                {
                    //kcal = 0f; // 수정

                    speed -= y_acceleration * speedDecrease * 2f;

                    if (speed < 0f) { speed = 0f; }
                }
            }
            else
            {
                // 징검다리 구간, 땅에 내려오기 전까지 Spurt 상태 여야함 --------------------------------------------------------------------------------------------------------------------
                if (input_Speed == boosterState || jumpState)
                {

                    jumpState = true;

                    kcal += Time.deltaTime * 2f;

                    // 애니메이션 idle 상태 speed -> 0
                    speed = 0f;

                    if (isOnce)
                    {
                        anim.SetTrigger("Jump");
                        isOnce = false;
                    }
                    else
                    {

                        // Data Process - Booster
                        //별가루 사용 중 아닐경우,
                        if (!use_starDust)
                        {
                            if (Game_DataManager.instance.moonDis > 0)
                                Game_DataManager.instance.moonDis -= realTime_Spurt;
                            else
                                Game_DataManager.instance.moonDis = 0;

                            if (Game_DataManager.instance.spaceStationDis > 0)
                                Game_DataManager.instance.spaceStationDis -= realTime_Spurt;
                            else
                                Game_DataManager.instance.spaceStationDis = 0;
                        }
                        //별가루 사용 중 일 경우, 걸음의 2배 적용 
                        else
                        {
                            if (Game_DataManager.instance.moonDis > 0)
                                Game_DataManager.instance.moonDis -= realTime_Spurt * 2;
                            else
                                Game_DataManager.instance.moonDis = 0;

                            if (Game_DataManager.instance.spaceStationDis > 0)
                                Game_DataManager.instance.spaceStationDis -= realTime_Spurt * 2;
                            else
                                Game_DataManager.instance.spaceStationDis = 0;
                        }
                        /*
                                                if (Game_DataManager.instance.moonDis > 0)
                                                    Game_DataManager.instance.moonDis -= realTime_Spurt;  // Run >> 0.00035f
                                                else
                                                    Game_DataManager.instance.moonDis = 0;

                                                if (Game_DataManager.instance.spaceStationDis > 0)
                                                    Game_DataManager.instance.spaceStationDis -= realTime_Spurt;
                                                else
                                                    Game_DataManager.instance.spaceStationDis = 0;*/

                        if (SceneManager.GetActiveScene().name == "Game 3" ||
                            SceneManager.GetActiveScene().name == "Game 11")
                        {
                            playerPosition = Mathf.Lerp(0f, cinemachineSmoothPath.PathLength, Mathf.InverseLerp(0f, 5f, 5f - Game_DataManager.instance.spaceStationDis));
                            playerCart.m_Position = playerPosition;
                        }
                        else
                        {
                            playerPosition = Mathf.Lerp(0f, cinemachineSmoothPath.PathLength * 2, Mathf.InverseLerp(0f, 5f, 5f - Game_DataManager.instance.spaceStationDis));
                            playerCart.m_Position = playerPosition;
                        }

                        // 소비 에너지 (㎉) = 1.05 x 엑서사이즈 (METs x 시간) x 체중 (㎏)
                        float kcal_calc = 1.05f * (mets * (kcal / 3600)) * 65;
                        //Game_DataManager.instance.todayKcal = current_Kcal + kcal_calc;
                        Game_DataManager.instance.once_Kcal = kcal_calc;
                        // 코인 - 나중에 걸음수로 바꾸기
                        //Game_DataManager.instance.coin = current_coin + Game_DataManager.instance.today_stepCount_inGame;
                
                    }
                }
                else
                {
                    speed -= y_acceleration * speedDecrease * 2f;

                    if (speed < 0f) { speed = 0f; }
                }
            }
        }

        //stateText2.text = Game_DataManager.instance.spaceStationDis.ToString();
        //stateText3.text = Game_DataManager.instance.moonDis.ToString();
        //stateText5.text = playerCart.m_Position.ToString();

        speed = Mathf.Clamp(speed, 0, 1.1f);    // Y 최소, 최대값
        //direction = Mathf.Clamp(direction, -1.1f, 1.1f);    // X 최소, 최대값

        // Idle Option Timer
        if (speed.Equals(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            restTime += Time.deltaTime;

            if (restTime > leftTime)
            {
                Change_IdleAnim(RandomEnum<Idle_State>());  // Enum Random
                CheckAnimationState();      // Anim State Check
            }
        }
        else if (speed > 0)
        {         //애니메이션 완료 후 실행되는 부분
            anim.SetBool("Idle 2", false);
            anim.SetBool("Idle 3", false);
            anim.SetBool("Look_Behind", false);
        }
        anim.SetFloat("MoveY", speed);      // Y 값 적용
    }

    // 애니메이션 Idle 선택
    private void Change_IdleAnim(int _idle)
    {
        switch (_idle)
        {
            case 0:
                anim.SetBool("Idle 2", true);
                break;
            case 1:
                anim.SetBool("Idle 3", true);
                break;
            case 2:
                anim.SetBool("Look_Behind", true);
                break;
            default:
                break;
        }
    }

    public void CheckAnimationState()
    {
        StartCoroutine(_CheckAnimationState());
    }
    // 애니메이션 상태 체크
    IEnumerator _CheckAnimationState()
    {
        restTime = 0f;

        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            //전환 중일 때 실행되는 부분
            yield return null;
        }

        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= exitTime)
        {
            //애니메이션 재생 중 실행되는 부분
            yield return null;
        }

        //애니메이션 완료 후 실행되는 부분
        anim.SetBool("Idle 2", false);
        anim.SetBool("Idle 3", false);
        anim.SetBool("Look_Behind", false);
        anim.SetBool("Pickup", false);
    }

    private int RandomEnum<T>()
    {
        Array values = Enum.GetValues(typeof(T));

        return (int)values.GetValue(new System.Random().Next(0, values.Length));
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        //Debug.Log(col.name);
        if (col.name.Equals("TestStation"))
        {
            Game_DataManager.instance.spaceStationDis = 0;
        }
        if (col.name.Equals("MoonPos"))
        {
            anim.SetFloat("MoveY", 0);      // Y 값 적용
            //   controller.enabled = false;
            Game_UIManager.instance.Potal_Bloom();

        }
        if (col.name.Equals("Wakeup"))
        {
            // 꿈에서 깨기 시작 > 달리기 : 나중에 거리로 체크
            StartCoroutine(_WakeUp());
        }
        //징검다리 뛰기 조건! 1.1이상의 속도로 달려야함
        if (col.CompareTag("StartStepCondition"))
        {
            stepJumpCheck = true;
        }
        else if (col.CompareTag("EndStepCondition"))
        {
            stepJumpCheck = false;
            jumpState = false;
            speed = 0f;
            input_Speed = 0f;
            isOnce = true;
        }

        if (col.tag == "Item")
        {
            // Debug.LogError(col.tag);
            Item item = col.GetComponent<Item>();
            switch (item.type)
            {

                //별가루 줍기
                case Item.Type.StarDust:
                    int StarDustCount = PlayerPrefs.GetInt("Item_EnergyDrink");
                    StarDustCount += 1;
                    PlayerPrefs.SetInt("Item_EnergyDrink", StarDustCount);    //아이템 갯수 갱신

                    if (starDust > maxStarDust)
                    {
                        starDust = maxStarDust;
                    }
                    else
                    {
                        //애니메이션 완료 후 실행되는 부분
                        anim.SetBool("Idle 2", false);
                        anim.SetBool("Idle 3", false);
                        anim.SetBool("Look_Behind", false);

                        // 별가루 먹기
                        speed = 0f;
                        anim.SetBool("Pickup", true);
                        CheckAnimationState();     // Anim State Check

                        // 텍스트 이벤트 시작
                        StartCoroutine(_pickUpTextEvent(col));
                    }

                    break;
                //비어있는 별가루
                case Item.Type.EmptyStarDust:
                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);

                    // 노랑 스낵 먹기
                    speed = 0f;
                    anim.SetBool("Pickup", true);

                    CheckAnimationState();      // Anim State Check

                    // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));
                    break;

                //달의 목걸이 
                case Item.Type.Necklace:
                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);

                    // 목걸이 줍기
                    speed = 0f;
                    anim.SetBool("Pickup", true);

                    CheckAnimationState();      // Anim State Check

                    // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));
                    break;
                //달의 조각
                case Item.Type.Sculpture:
                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);

                    // 조각 줍기
                    speed = 0f;
                    anim.SetBool("Pickup", true);

                    CheckAnimationState();      // Anim State Check

                    // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));
                    break;
                //보석함
                case Item.Type.JewlBox:
                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);

                    // 조각 줍기
                    speed = 0f;
                    anim.SetBool("Pickup", true);

                    CheckAnimationState();      // Anim State Check

                    // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));
                    break;

                //우주 꽃
                case Item.Type.SpaceFlower:
                    // 최대 10개
                    spaceFlower += item.value;
                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);

                    // 조각 줍기
                    speed = 0f;
                    anim.SetBool("Pickup", true);

                    CheckAnimationState();      // Anim State Check
                    //처음으로 주웠을 때, 설명 팝업 띄워줌
                    if (spaceFlower.Equals(1))
                    {
                        // 텍스트 이벤트 시작
                        StartCoroutine(_pickUpTextEvent(col));
                    }
                    else
                    {
                        Debug.Log("우주꽃 획득" + spaceFlower);
                        col.SetActive(false);
                    }
                    break;
                //달 가루 
                case Item.Type.MoonPowder:
                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);

                    speed = 0f;
                    anim.SetBool("Pickup", true);

                    CheckAnimationState();      // Anim State Check


                    // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));
                    moonPowder_event_Section = true; //달가루 주우면 해당구간에서 사용 될 것
                    break;

                //소형 산소통 일 경우
                case Item.Type.OxygenTank_small:

                    int OxygenSmallTankCount = PlayerPrefs.GetInt("Item_SmallAirTank");
                    OxygenSmallTankCount += 1;
                    PlayerPrefs.SetInt("Item_SmallAirTank", OxygenSmallTankCount);    //아이템 갯수 갱신
                                                                                      // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));

                    break;
                //대형 산소통 일 경우
                case Item.Type.OxygenTank_big:
                    int OxygenBigTankCount = PlayerPrefs.GetInt("Item_BigAirTank");
                    OxygenBigTankCount += 1;
                    PlayerPrefs.SetInt("Item_BigAirTank", OxygenBigTankCount);    //아이템 갯수 갱신
                                                                                  // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));
                    break;
                //낡은 소형 산소통 줍기
                case Item.Type.Old_OxygenTank_small:
                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);

                    speed = 0f;
                    anim.SetBool("Pickup", true);

                    CheckAnimationState();      // Anim State Check

                    // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));
                    break;
                //낡은 대형 산소통 줍기
                case Item.Type.Old_OxygenTank_big:
                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);
                    speed = 0f;
                    anim.SetBool("Pickup", true);

                    CheckAnimationState();      // Anim State Check

                    // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));
                    break;

                //우주 라디오
                case Item.Type.Radio:
                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);
                    speed = 0f;
                    anim.SetBool("Pickup", true);
                    CheckAnimationState();      // Anim State Check

                    // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));
                    break;

                /////////////////////// 과자 / 통조림 /////////////////////////////////////////////
                case Item.Type.YELLOW_Snack:

                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);

                    // 노랑 스낵 먹기
                    speed = 0f;
                    anim.SetBool("Pickup", true);

                    CheckAnimationState();     // Anim State Check

                    // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));

                    break;
                case Item.Type.BLUE_Snack:
                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);

                    // 파랑 스낵 먹기
                    speed = 0f;
                    anim.SetBool("Pickup", true);

                    CheckAnimationState();     // Anim State Check

                    // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));

                    break;
                case Item.Type.GREEN_Snack:
                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);

                    // 초록 스낵 먹기
                    speed = 0f;
                    anim.SetBool("Pickup", true);

                    CheckAnimationState();     // Anim State Check

                    // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));
                    break;
                case Item.Type.ORANGE_Snack:
                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);

                    // 주황 스낵 먹기
                    speed = 0f;
                    anim.SetBool("Pickup", true);

                    CheckAnimationState();     // Anim State Check

                    // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));
                    break;
                case Item.Type.PURPLE_Snack:
                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);

                    // 보라 스낵 먹기
                    speed = 0f;
                    anim.SetBool("Pickup", true);

                    CheckAnimationState();     // Anim State Check

                    // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));
                    break;
                case Item.Type.Canned_YELLOW:
                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);

                    // 노랑 캔 먹기
                    speed = 0f;
                    anim.SetBool("Pickup", true);

                    CheckAnimationState();   // Anim State Check

                    // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));
                    break;
                case Item.Type.Canned_BLUE:
                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);

                    // 파란 캔 먹기
                    speed = 0f;
                    anim.SetBool("Pickup", true);

                    CheckAnimationState();      // Anim State Check

                    // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));

                    break;
                case Item.Type.Canned_ORANGE:

                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);

                    // 주황 캔 먹기
                    speed = 0f;
                    anim.SetBool("Pickup", true);

                    CheckAnimationState();   // Anim State Check

                    // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));

                    break;
                case Item.Type.Canned_GREEN:

                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);

                    // 초록 캔 먹기
                    speed = 0f;
                    anim.SetBool("Pickup", true);

                    CheckAnimationState();    // Anim State Check

                    // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));

                    break;
                case Item.Type.Canned_PURPLE:

                    //애니메이션 완료 후 실행되는 부분
                    anim.SetBool("Idle 2", false);
                    anim.SetBool("Idle 3", false);
                    anim.SetBool("Look_Behind", false);

                    // 보라 캔 먹기
                    speed = 0f;
                    anim.SetBool("Pickup", true);

                    CheckAnimationState();      // Anim State Check

                    // 텍스트 이벤트 시작
                    StartCoroutine(_pickUpTextEvent(col));

                    break;
            }
            // Debug.LogError(item.type);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
    }

    IEnumerator _pickUpTextEvent(GameObject _colObj)
    {
        yield return new WaitUntil(() => !Game_TypeWriterEffect.instance.Event_ing);
        Debug.LogError("pickUpText" + _colObj);
        _colObj.GetComponent<Item>().GetText();
        Debug.Log("-------------------------------------------1 ");
        string GetItem_type = _colObj.GetComponent<Item>().type.ToString();

        //튜토리얼 중이 아닌경우에는 아이템 설명을 부여해준다.
        if (!Tutorial_ing)
        {
            yield return new WaitUntil(() => Game_TypeWriterEffect.instance.Event_ing == false);
            Debug.Log("-------------------------------------------3 ");

            if (GetItem_type.Equals("Canned_BLUE")) { ItemDescriptionManager.Show_ItemDescription("Can_blue"); }
            else if (GetItem_type.Equals("Canned_ORANGE")) { ItemDescriptionManager.Show_ItemDescription("Can_orange"); }
            else if (GetItem_type.Equals("Canned_GREEN")) { ItemDescriptionManager.Show_ItemDescription("Can_green"); }
            else if (GetItem_type.Equals("Canned_PURPLE")) { ItemDescriptionManager.Show_ItemDescription("Can_purple"); }
            else if (GetItem_type.Equals("Canned_YELLOW")) { ItemDescriptionManager.Show_ItemDescription("Can_yellow"); }
            //
            else if (GetItem_type.Equals("BLUE_Snack")) { ItemDescriptionManager.Show_ItemDescription("Snack_blue"); }
            else if (GetItem_type.Equals("ORANGE_Snack")) { ItemDescriptionManager.Show_ItemDescription("Snack_orange"); }
            else if (GetItem_type.Equals("GREEN_Snack")) { ItemDescriptionManager.Show_ItemDescription("Snack_green"); }
            else if (GetItem_type.Equals("PURPLE_Snack")) { ItemDescriptionManager.Show_ItemDescription("Snack_purple"); }
            else if (GetItem_type.Equals("YELLOW_Snack")) { ItemDescriptionManager.Show_ItemDescription("Snack_yellow"); }
            //작은 산소통
            else if (GetItem_type.Equals("OxygenTank_small")) { ItemDescriptionManager.Show_ItemDescription("OxygenTank_small_New"); }
            else if (GetItem_type.Equals("Old_OxygenTank_small")) { ItemDescriptionManager.Show_ItemDescription("OxygenTank_small_Old"); }
            //큰 산소통
            else if (GetItem_type.Equals("OxygenTank_big")) { ItemDescriptionManager.Show_ItemDescription("OxygenTank_big_New");}
            else if (GetItem_type.Equals("Old_OxygenTank_big")) { ItemDescriptionManager.Show_ItemDescription("OxygenTank_big_Old"); }
            //별 가루 
            else if (GetItem_type.Equals("StarDust")) { ItemDescriptionManager.Show_ItemDescription("StarDust_New"); }
            else if (GetItem_type.Equals("EmptyStarDust")) { ItemDescriptionManager.Show_ItemDescription("StarDust_Empty"); }
            //우주 꽃
            else if (GetItem_type.Equals("SpaceFlower")) { ItemDescriptionManager.Show_ItemDescription("SpaceFlower"); }
            else if (GetItem_type.Equals("MoonPowder")) { ItemDescriptionManager.Show_ItemDescription("MoonPowder"); }

 
        }
        yield return null;
    }

    public void Grab(bool _state)
    {
        anim.SetBool("Grab", _state);
    }

    public void Fall_asleep()
    {
        StartCoroutine(_Fall_asleep());
    }
    IEnumerator _Fall_asleep()
    {
        vn = (Vignette)Object_Volume.profile.components[1];
        while (vn.intensity.value <= 1 && vn.smoothness.value < 1)
        {
            vn.intensity.value += Time.deltaTime;
            vn.smoothness.value += Time.deltaTime * 0.7f;

            yield return null;
        }

        while (vn.smoothness.value > 0.25f)
        {
            vn.smoothness.value -= Time.deltaTime * 5f;
            yield return null;
        }

        while (vn.smoothness.value < 1f)
        {
            vn.smoothness.value += Time.deltaTime * 0.7f;
            if (vn.intensity.value > 0.6f && !blinkEyes.activeSelf)
            {
                blinkEyes.SetActive(true);

                FadeIn(1.8f);
            }

            yield return null;
        }

        yield return new WaitForSeconds(2f);

        FadeOut(1.8f);

        //씬 넘어가기
        yield return null;
    }
    public void WakeUp()
    {
        StartCoroutine(_WakeUp());
    }
    IEnumerator _WakeUp()
    {
        vn = (Vignette)Object_Volume.profile.components[1];

        Debug.Log("vn.intensity.value" + vn.intensity.value);
        Debug.Log("vn.smoothness.value" + vn.smoothness.value);

        // 눈 감긴 상태
        Color curColor = fade_Image.color;
        curColor.a = 1.0f;
        fade_Image.color = curColor;
        vn.intensity.value = 1f;
        vn.smoothness.value = 1f;

        // 눈 감긴상태에서 떠짐
        // 0.75 > 1 > 0.75 > 1 > 0

        yield return new WaitForSeconds(1f);

        // 눈 뜸
        FadeOut(1.8f);
        while (vn.intensity.value >= 0.75f)
        {
            vn.intensity.value -= Time.deltaTime * 0.8f;

            yield return null;
        }

        FadeIn(1.8f);
        while (vn.intensity.value < 1)
        {
            vn.intensity.value += Time.deltaTime;
            yield return null;
        }


        yield return new WaitForSeconds(0.4f);

        FadeOut(2.4f);
        while (vn.intensity.value >= 0.75f)
        {
            vn.intensity.value -= Time.deltaTime;
            yield return null;
        }

        FadeIn(2.4f);
        while (vn.intensity.value < 1)
        {
            vn.intensity.value += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        FadeOut(2.4f);
        while (vn.intensity.value >= 0.75f)
        {
            vn.intensity.value -= Time.deltaTime;
            yield return null;
        }

        FadeIn(2.4f);
        while (vn.intensity.value < 1)
        {
            vn.intensity.value += Time.deltaTime;
            yield return null;
        }

        FadeOut(1.2f);
        while (vn.intensity.value > 0.001f)
        {
            vn.intensity.value -= Time.deltaTime * 0.7f;
            yield return null;
        }

        vn.intensity.value = 0f;

        //씬 넘어가기
        yield return null;
    }
    private void FadeIn(float _time)
    {
        StartCoroutine(_FadeIn(_time));
    }

    IEnumerator _FadeIn(float _time)
    {
        targetAlpha = 1.0f;
        Color curColor = fade_Image.color;
        while (Mathf.Abs(curColor.a - targetAlpha) > 0.01f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, Time.deltaTime * _time);
            fade_Image.color = curColor;
            yield return null;
        }
        curColor.a = 1.0f;
        fade_Image.color = curColor;

        Debug.Log("in");

        yield return null;
    }

    private void FadeOut(float _time)
    {
        StartCoroutine(_FadeOut(_time));
    }

    IEnumerator _FadeOut(float _time)
    {
        targetAlpha = 0.0f;
        Color curColor = fade_Image.color;

        while (Mathf.Abs(curColor.a) > 0.005f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, Time.deltaTime * _time);
            fade_Image.color = curColor;
            //vn.intensity.value = 0f;
            yield return null;
        }
        curColor.a = 0.0f;
        fade_Image.color = curColor;

        Debug.Log("out");

        yield return null;
    }
    //////////////////////////////////////////////////////////////////////////////////////////


    // 소형 산소통 사용
    public void Use_Oxygen_Small()
    {
        Show_Sign("Oxygen_Small");
    }
    //대형 산소통 사용
    public void Use_Oxygen_Big()
    {
        Show_Sign("Oxygen_Big");
    }
    //별가루 사용
    public void Use_StarDust()
    {
        Show_Sign("StarDust");
    }
    ///상태 체크에 따라 부스터
    //별가루 사용 효과 적용, 파티클 
    IEnumerator _Use_StarDust_Particle()
    {
        StarDustParticle.SetActive(true); // 가방에 파티클 생김
        float useItem = 10 * 60f; // 10분
        while (useItem > 0)
        {
            Debug.Log(useItem);
            use_starDust = true;
            useItem -= Time.deltaTime;
            player_StarDust_state.material = _starDust_states[1]; //빛나는 노란색
            yield return null;
        }
        player_StarDust_state.material = _starDust_states[0]; //기본
        use_starDust = false;
        StarDustParticle.SetActive(false); // 가방에 파티클 꺼짐
        yield return null;
    }
    ///아이템 사용에 따라 플레이어의 머티리얼 상태 변화
    //산소 충족 지역
    public void Use_OxygenBar_blue()
    {
        player_OxBar_state.material = _OxBar_states[0];
    }
    //산소 부족 지역
    public void Use_Oxygenbar_red()
    {
        player_OxBar_state.material = _OxBar_states[1];
    }
    //아이템 사용하고 팝업창 텍스트 적용
    public void Show_Sign(string name)
    {
        StartCoroutine(_Show_Sign(name));
    }
    IEnumerator _Show_Sign(string name)
    {
        Game_UIManager.instance.StoreSignPopup.gameObject.SetActive(true);


        if (name.Equals("Cnt_MoonPowder"))
        {
            Game_UIManager.instance.StoreSignTitle_Text.text = "알  림";
            Game_UIManager.instance.StoreSignPopup_Text.text = "해당 아이템은 특정 구간에서만 사용 가능합니다.";
        }
        else if (name.Equals("Oxygen_Small"))
        {
            player_Ox_state.material = _Ox_states[0];
            Game_UIManager.instance.StoreSignTitle_Text.text = "소형 산소통 사용";
            Game_UIManager.instance.StoreSignPopup_Text.text = "산소 10분이 충전 됩니다.";

        }
        else if (name.Equals("Oxygen_Big"))
        {
            player_Ox_state.material = _Ox_states[1];
            Game_UIManager.instance.StoreSignTitle_Text.text = "대형 산소통 사용";
            Game_UIManager.instance.StoreSignPopup_Text.text = "산소 20분이 충전 됩니다.";
        }
        else if (name.Equals("StarDust"))
        {
            Game_UIManager.instance.StoreSignTitle_Text.text = "별가루 사용";
            Game_UIManager.instance.StoreSignPopup_Text.text = "10분 동안 걸음이 2배 빨라집니다.";
        }
        yield return new WaitForSeconds(2f);
        Game_UIManager.instance.StoreSignPopup.SetActive(false);
        //별가루 파티클 적용하기 
        if (name.Equals("StarDust"))
        {
            yield return new WaitUntil(() => Game_UIManager.instance.StoreSignPopup.activeSelf == false);
            yield return new WaitForSeconds(1f);
            SoundFunction.Instance.ItemUse_StarDust(); // 별가루 사용 사운드
            StartCoroutine(_Use_StarDust_Particle()); //파티클 적용! 별가루 효과 적용!
        }
        yield return null;
    }
    //아이템 사용했을 때, 몇초후에 팝업 사라짐

    //Chapter13 우주를 떠도는 영혼 - Chapter20 까지 사용됨
    /* public void Rune_CheckAnimationState()
     {
         //룬 랜덤으로 움직이도록 설정
         StartCoroutine(_Rune_CheckAnimationState());
     }
     IEnumerator _Rune_CheckAnimationState()
     {
         while (!Rune_Animation.GetCurrentAnimatorStateInfo(0).IsName("Idle") || !Rune_Animation.GetCurrentAnimatorStateInfo(0).IsName("Flapping") ||
             !Rune_Animation.GetCurrentAnimatorStateInfo(0).IsName("LolyTurn") || !Rune_Animation.GetCurrentAnimatorStateInfo(0).IsName("Turn")
             || !Rune_Animation.GetCurrentAnimatorStateInfo(0).IsName("Point"))
         {
             //전환 중일 때 실행되는 부분
             yield return null;
         }

         while (Rune_Animation.GetCurrentAnimatorStateInfo(0)
         .normalizedTime < exitTime)
         {
             //애니메이션 재생 중 실행되는 부분
             yield return null;
         }
         //애니메이션 완료 후 실행되는 부분
         int RandomAnimationSet = UnityEngine.Random.Range(0, 4);
         Rune_Animation.SetInteger("MoveRand", RandomAnimationSet);
     }*/
    ///////////////////////////////////////////////////////////////////////////////////

    //public Vector3 nextPlayerVec;
    //public void DistanceCheck()
    //{
    //    StartCoroutine(_DistanceCheck());
    //}
    //IEnumerator _DistanceCheck()
    //{
    //    //float _distance = Game_DataManager.instance.moonDis - 0.001f;
    //    //nextPlayerVec = new Vector3(PlayerOb.transform.localPosition.x, PlayerOb.transform.localPosition.y, 50);
    //    //Debug.Log(" _distance  " + _distance);
    //    //Debug.Log(" Game_DataManager.instance.moonDis  " + Game_DataManager.instance.moonDis);
    //    while (Game_DataManager.instance.gamePlaying.Equals(true))
    //    {
    //        if (Game_DataManager.instance.moonDis <= 0) // 달 도착
    //        {
    //            Game_DataManager.instance.moonDis = 0;
    //            // 기록 저장
    //            ServerManager.Instance.Reg_Ranking(PlayerPrefs.GetString("Player_NickName"));
    //        }

    //        //Debug.Log(" _distance------------1  " + _distance);
    //        //5키로를 달렸을 경우, 달로 달려가도록 !
    //        //if (Game_DataManager.instance.moonDis <= 0)
    //        //{
    //        //    PlayerOb.transform.localPosition = Vector3.Lerp(PlayerOb.transform.localPosition, nextPlayerVec, Time.deltaTime);
    //        //    anim.SetFloat("MoveY", 3);      // Y 값 적용
    //        //    //Debug.Log("  PlayerOb.transform.position  " + PlayerOb.transform.localPosition);
    //        //}
    //        //else
    //        //{
    //        //    if(Game_DataManager.instance.moonDis <= 2.5f)
    //        //    {
    //        //        //if (fx.fallOff > 0.18)
    //        //        //{
    //        //        //    fx.fallOff -=Time.deltaTime*0.5f;
    //        //        //}
    //        //    }
    //        //    if (Input.GetKey(KeyCode.W))
    //        //    {
    //        //        PlayerOb.transform.localPosition = Vector3.Lerp(PlayerOb.transform.localPosition, nextPlayerVec, Time.deltaTime*0.009f);
    //        //        CameraOb.transform.Rotate(new Vector3(0f, 0.001f, 0f));

    //        //    }
    //        //        Debug.Log("  PlayerOb.transform.position  " + PlayerOb.gameObject.transform.localPosition);
    //        //    yield return null;
    //        //}

    //        yield return null;
    //    }

    //}

}
