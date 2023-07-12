using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewManMove : MonoBehaviour
{
    public LayerMask LAYERMASK_PLAYER;
    public static NewManMove instance { get; private set; }

    public NewManCtrl manctrl;

    //센서 연결
    public float[] mpu_value;
    float curr_rateX, curr_rateY, curr_rateZ;
    float _curr_rateX, _curr_rateY, _curr_rateZ;
    float before_rateX, before_rateY, before_rateZ;
    float gapX, gapY, gapZ;
    float timeNum;
    bool stop;
    public float speed;
    public float bikeMaxSpeed;  //바이크 최고 속도
    public bool connetState;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        this.gameObject.layer = Mathf.RoundToInt(Mathf.Log(LAYERMASK_PLAYER.value, 2));

        mpu_value = new float[3];   //3개 초기화
        curr_rateX = Mathf.InverseLerp(-32768f, 32768f, mpu_value[0] - 4096f);
        curr_rateY = Mathf.InverseLerp(-32768f, 32768f, mpu_value[1] - 4096f);
        curr_rateZ = Mathf.InverseLerp(-32768f, 32768f, mpu_value[2] - 4096f);

        before_rateX = (float)(System.Math.Truncate(curr_rateX * 1000) / 1000);  //소수점 이하 3자리 버리기
        before_rateY = (float)(System.Math.Truncate(curr_rateY * 1000) / 1000);
        before_rateZ = (float)(System.Math.Truncate(curr_rateZ * 1000) / 1000);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("BusanMapMorning")|| SceneManager.GetActiveScene().name.Equals("BusanMapNight")
        || SceneManager.GetActiveScene().name.Equals("BusanGreenLineMorning")|| SceneManager.GetActiveScene().name.Equals("BusanGreenLineNight"))
        {
            if (mpu_value[0] >= -32768f && mpu_value[0] <= 32768 || 
                mpu_value[1] >= -32768f && mpu_value[1] <= 32768 || 
                mpu_value[2] >= -32768f && mpu_value[2] <= 32768)
            {
                curr_rateX = Mathf.InverseLerp(-32768f, 32768f, mpu_value[0] - 4096f);
                _curr_rateX = (float)(System.Math.Truncate(curr_rateX * 1000) / 1000);

                curr_rateY = Mathf.InverseLerp(-32768f, 32768f, mpu_value[1] - 4096f);
                _curr_rateY = (float)(System.Math.Truncate(curr_rateY * 1000) / 1000);

                curr_rateZ = Mathf.InverseLerp(-32768f, 32768f, mpu_value[2] - 4096f);
                _curr_rateZ = (float)(System.Math.Truncate(curr_rateZ * 1000) / 1000);

                gapX = before_rateX - _curr_rateX;
                gapY = before_rateY - _curr_rateY;
                gapZ = before_rateZ - _curr_rateZ;

                if (connetState && Busan_UIManager.instance.gameStartState.Equals(true))
                {
                    if ((Mathf.Abs(gapX) < 0.002f && Mathf.Abs(gapY) < 0.002f && 
                        Mathf.Abs(gapZ) < 0.002f && stop == false))// || AsiaMap_UIManager.instance.gameEnd.Equals(true))
                    {
                        if (Busan_UIManager.instance.speed < 0f)
                        {
                            Busan_UIManager.instance.speed = 0f;

                            if (speed < 0f)
                            {
                                speed = 0f;
                                this.manctrl.Animator_Speed(false, false, 1);
                            }
                            else if (speed > 0f)
                            {
                                speed -= 0.05f;
                                this.manctrl.Animator_Speed(true, false, speed);
                            }
                            else if (speed == 0f)
                            {
                                speed = 0f;
                                this.manctrl.Animator_Speed(false, false, 1);
                            }

                            before_rateX = _curr_rateX;
                            before_rateY = _curr_rateY;
                            before_rateZ = _curr_rateZ;
                        }
                        else if (Busan_UIManager.instance.speed > 0f)
                        {
                            Busan_UIManager.instance.speed -= 0.5f;

                            if (speed < 0f)
                            {
                                speed = 0f;
                                this.manctrl.Animator_Speed(false, false, 1);
                            }
                            else if (speed > 0f)
                            {
                                speed -= 0.05f;
                                this.manctrl.Animator_Speed(true, false, speed);
                            }
                            else if (speed == 0f)
                            {
                                speed = 0f;
                                this.manctrl.Animator_Speed(false, false, 1);
                            }

                            before_rateX = _curr_rateX;
                            before_rateY = _curr_rateY;
                            before_rateZ = _curr_rateZ;
                        }
                        else if (Busan_UIManager.instance.speed == 0f)
                        {
                            Busan_UIManager.instance.speed = 0f;

                            if (speed < 0f)
                            {
                                speed = 0f;
                                this.manctrl.Animator_Speed(false, false, 1);
                            }
                            else if (speed > 0f)
                            {
                                speed -= 0.05f;
                                this.manctrl.Animator_Speed(true, false, speed);
                            }
                            else if (speed == 0f)
                            {
                                speed = 0f;
                                this.manctrl.Animator_Speed(false, false, 1);
                            }

                            before_rateX = _curr_rateX;
                            before_rateY = _curr_rateY;
                            before_rateZ = _curr_rateZ;
                        }
                    }
                    else if (Mathf.Abs(gapX) >= 0.002f || Mathf.Abs(gapY) >= 0.002f || Mathf.Abs(gapZ) >= 0.002f)
                    {
                        timeNum = 0;
                        stop = true;
                        BusanMap_GameTime.instance.chainBrakeState = false; //브레이크 놉

                        if (Mathf.Abs(gapX) >= 0.002f && Mathf.Abs(gapX) < 0.003f || Mathf.Abs(gapY) >= 0.002f && Mathf.Abs(gapY) < 0.003f ||
                                Mathf.Abs(gapZ) >= 0.002f && Mathf.Abs(gapZ) < 0.003f)
                        {
                            PlaySpeed(6f, 0.25f, 0.05f, 0.8f, true, false);
                        }
                        else if (Mathf.Abs(gapX) >= 0.003f && Mathf.Abs(gapX) < 0.004f || Mathf.Abs(gapY) >= 0.003f && Mathf.Abs(gapY) < 0.004f ||
                            Mathf.Abs(gapZ) >= 0.003f && Mathf.Abs(gapZ) < 0.004f)
                        {
                            PlaySpeed(10f, 0.25f, 0.05f, 1.0f, true, false);
                        }
                        else if (Mathf.Abs(gapX) >= 0.004f && Mathf.Abs(gapX) < 0.005f || Mathf.Abs(gapY) >= 0.004f && Mathf.Abs(gapY) < 0.005f ||
                            Mathf.Abs(gapZ) >= 0.004f && Mathf.Abs(gapZ) < 0.005f)
                        {
                            PlaySpeed(12f, 0.25f, 0.05f, 1.2f, true, false);
                        }
                        else if (Mathf.Abs(gapX) >= 0.005f && Mathf.Abs(gapX) < 0.006f || Mathf.Abs(gapY) >= 0.005f && Mathf.Abs(gapY) < 0.006f ||
                            Mathf.Abs(gapZ) >= 0.005f && Mathf.Abs(gapZ) < 0.006f)
                        {
                            PlaySpeed(13f, 0.25f, 0.05f, 1.4f, true, false);
                        }
                        else if (Mathf.Abs(gapX) >= 0.006f && Mathf.Abs(gapX) < 0.007f || Mathf.Abs(gapY) >= 0.006f && Mathf.Abs(gapY) < 0.007f ||
                            Mathf.Abs(gapZ) >= 0.006f && Mathf.Abs(gapZ) < 0.007f)
                        {
                            PlaySpeed(14f, 0.25f, 0.05f, 1.6f, true, false);
                        }
                        else if (Mathf.Abs(gapX) >= 0.007f && Mathf.Abs(gapX) < 0.008f || Mathf.Abs(gapY) >= 0.007f && Mathf.Abs(gapY) < 0.008f ||
                            Mathf.Abs(gapZ) >= 0.007f && Mathf.Abs(gapZ) < 0.008f)
                        {
                            PlaySpeed(17f, 0.25f, 0.05f, 1.8f, true, false);
                        }
                        else if (Mathf.Abs(gapX) >= 0.008f && Mathf.Abs(gapX) < 0.009f || Mathf.Abs(gapY) >= 0.008f && Mathf.Abs(gapY) < 0.009f ||
                            Mathf.Abs(gapZ) >= 0.008f && Mathf.Abs(gapZ) < 0.009f)
                        {
                            PlaySpeed(19f, 0.25f, 0.05f, 2.0f, true, false);
                        }
                        else if (Mathf.Abs(gapX) >= 0.009f && Mathf.Abs(gapX) < 0.015f || Mathf.Abs(gapY) >= 0.009f && Mathf.Abs(gapY) < 0.015f ||
                            Mathf.Abs(gapZ) >= 0.009f && Mathf.Abs(gapZ) < 0.015f)
                        {
                            PlaySpeed(22f, 0.5f, 0.05f, 2.4f, true, true);
                        }
                        else if (Mathf.Abs(gapX) >= 0.015f || Mathf.Abs(gapY) >= 0.015f || Mathf.Abs(gapZ) >= 0.015f)
                        {
                            PlaySpeed(25f, 0.5f, 0.05f, 2.4f, true, true);
                        }
                    }
                    else if (Mathf.Abs(gapX) < 0.002f && Mathf.Abs(gapY) < 0.002f && Mathf.Abs(gapZ) < 0.002f && stop == true)
                    {
                        timeNum += Time.deltaTime;

                        if (timeNum <= 1.5f && timeNum >= 1f)
                        {
                            if (Busan_UIManager.instance.speed > 0)
                            {
                                Busan_UIManager.instance.speed -= 2f;
                            }
                            else if (Busan_UIManager.instance.speed <= 0)
                            {
                                Busan_UIManager.instance.speed = 0;
                            }


                            if (speed > 0)
                            {
                                speed -= 0.2f;
                                this.manctrl.Animator_Speed(true, false, speed);
                            }
                            else if (speed <= 0)
                            {
                                speed = 0;
                                this.manctrl.Animator_Speed(true, false, speed);
                                stop = false;
                            }
                        }

                        if (timeNum >= 1.5f)
                        {
                            timeNum = 1.5f;
                            stop = false;
                        }
                    }
                }
            }
        }
    }


    void PlaySpeed(float _standardSpeed, float _bikeSpeed, float _animSpeed, float _maxSpeed, bool _normalState, bool _speedState)
    {
        bikeMaxSpeed = BikeMaxSpeed(_standardSpeed);

        if (Busan_UIManager.instance.speed < bikeMaxSpeed)//_standardSpeed) //3
        {
            Busan_UIManager.instance.speed += _bikeSpeed; //0.25
            if (speed < _maxSpeed)
                speed += _animSpeed; //0.05
            else if (speed > _maxSpeed)
                speed -= _animSpeed;
            else if (speed == _maxSpeed)
                speed = _maxSpeed; //2.2
        }
        else if (Busan_UIManager.instance.speed > bikeMaxSpeed)//_standardSpeed)
        {
            Busan_UIManager.instance.speed -= _bikeSpeed;
            if (speed < _maxSpeed)
                speed += _animSpeed; //0.05
            else if (speed > _maxSpeed)
                speed -= _animSpeed;
            else if (speed == _maxSpeed)
                speed = _maxSpeed; //2.2
        }
        else if (Busan_UIManager.instance.speed == bikeMaxSpeed)//_standardSpeed)
        {
            Busan_UIManager.instance.speed = bikeMaxSpeed;// _standardSpeed;
            if (speed < _maxSpeed)
                speed += _animSpeed; //0.05
            else if (speed > _maxSpeed)
                speed -= _animSpeed;
            else if (speed == _maxSpeed)
                speed = _maxSpeed; //2.2
        }

        this.manctrl.Animator_Speed(_normalState, _speedState, speed);

        //text5.text = " AsiaMap_UIManager " + AsiaMap_UIManager.instance.speed;
        before_rateX = _curr_rateX;
        before_rateY = _curr_rateY;
        before_rateZ = _curr_rateZ;
    }


    public float BikeMaxSpeed(float _standardSpeed)
    {
        if (PlayerPrefs.GetString("Busan_UseItemSpeed").Equals("Speed"))
        {
            if (Busan_UIManager.instance.mountainUpstate.Equals(true))   //오르막길
                _standardSpeed = (_standardSpeed) * 1.3f;
            else if (Busan_UIManager.instance.mountainDownState.Equals(true))    //내리막실
                _standardSpeed = (_standardSpeed) * 1.4f * 1.4f;
            else if (Busan_UIManager.instance.stonFarmState.Equals(true))    //자갈밭
                _standardSpeed = (_standardSpeed) * 1.3f;
            else if (Busan_UIManager.instance.sandState.Equals(true))    //모래밭
                _standardSpeed = (_standardSpeed) * 1.3f;
            else
                _standardSpeed = (_standardSpeed) * 1.3f;
        }
        else
        {
            if (Busan_UIManager.instance.mountainUpstate.Equals(true))   //오르막길
                _standardSpeed = (_standardSpeed);
            else if (Busan_UIManager.instance.mountainDownState.Equals(true))    //내리막길
                _standardSpeed = (_standardSpeed) * 1.4f;
            else if (Busan_UIManager.instance.stonFarmState.Equals(true))    //자갈밭
                _standardSpeed = (_standardSpeed);
            else if (Busan_UIManager.instance.sandState.Equals(true))    //모래밭
                _standardSpeed = (_standardSpeed);
            else
                _standardSpeed = _standardSpeed;
        }
        return _standardSpeed;
    }
}
