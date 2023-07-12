using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class OBJCtrl : MonoBehaviour
{
    public static OBJCtrl instance { get; private set; }

    public float[] mpu_value;

    float min, max; //최대값, 최소값
    float minGap, maxGap;       //최대값 최소값에 대한 차이
    int minIndex, maxIndex; //최대값 최소값 해당하는 인덱스값
    int margin; //두 수의 차이 값
    public Text text;
    public Text text1;
    public Text text2;
    public Text text3;

    private float pre_rate;
    private float cur_rate, _cur_rate;
    private float delta_rate;
    public float speed;

    public Animator animator;
    public Animator animator_parent;
    public Animator longArmAnimator;
    public bool connetState;
    bool stage1, stage2, stage3, stage4, stage5;

    public NavMeshAgent navAgent;
    public Text speeText;

    float timeNum;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        //text.text = "";
        //text1.text = "";
        //text2.text = "";
        //text3.text = "";
    }

    void Start()
    {
        mpu_value = new float[3];
        cur_rate = Mathf.InverseLerp(-32768f, 32768f, mpu_value[1] - 4096f);
        pre_rate = (float)(System.Math.Truncate(cur_rate * 1000) / 1000);
    }

    // Update is called once per frame
    void Update()
    {
        //text.text = "????" + mpu_value[0].ToString() + " ," + mpu_value[1].ToString() + " , " + mpu_value[2].ToString();

        // 초기설정값을 받음 : 시작
        //if (mpu_value[1] >= 3000 && mpu_value[1] <= 5999 || mpu_value[1] >= -3000 && mpu_value[1] <= -5999)
        //if (connetState)
        {
            if (mpu_value[1] >= -32768 && mpu_value[1] <= 32768)
            {
                // 1. 값의 변화량이 크면 속도가 빨라진다. > 값의 변화량을 받아오려면 현재값 - 이전값 = 변화량 시간마다 측정
                // 2. 다리의 각도는 쿼터니언으로 받아온다. > 페달을 뒤로 돌리냐 앞으로 돌리냐 체크
                //Mathf.InverseLerp(-32768f, 32768f, mpu_value[1]);

                //text1.text = "값에 따른 비율 : " + delta_rate;  // 0.061 == 4096
                //speed = 0.0f;
                cur_rate = Mathf.InverseLerp(-32768f, 32768f, mpu_value[1] - 4096f);
                _cur_rate = (float)(System.Math.Truncate(cur_rate * 1000) / 1000);

                delta_rate = pre_rate - _cur_rate;

                //text2.text = "delta :: " + Mathf.Abs(delta_rate).ToString();
                text.text = "cur :: " + _cur_rate.ToString();
                //text3.text = "움직임 " + connetState;
                //if (connetState)
                {
                    if ((Mathf.Abs(delta_rate) < 0.003f && stage2 == false))// || Data_Manager.instance.GameEnd == true)
                    {
                        text3.text = " 멈춤___delta_rate: " + delta_rate.ToString();
                        //speed = 0f;
                        if (speed < 0f)
                            speed = 0f;
                        else if (speed > 0f)
                            speed -= 0.3f;
                        else if (speed == 0f)
                            speed = 0f;

                        speeText.text = speed.ToString();
                        navAgent.speed = speed;
                        //animator_parent.SetFloat("Speed", speed);
                        //animator.SetFloat("Speed", speed);
                        //longArmAnimator.SetFloat("Speed", speed);

                        pre_rate = _cur_rate;
                        //text1.text = " 멈춤: " + pre_rate.ToString() + " : " + stage2;

                    }
                    else if (Mathf.Abs(delta_rate) > 0.003f)
                    {
                        timeNum = 0;
                        text3.text = " ___delta_rate: " + Mathf.Abs(delta_rate).ToString();
                        stage2 = true; //움직이고 있는 상태

                        if (Mathf.Abs(delta_rate) > 0.003f && Mathf.Abs(delta_rate) < 0.1f)
                        {
                            if (speed < 3.6f)
                                speed += 0.3f;
                            else if (speed > 3.6f)
                                speed -= 0.3f;
                            else if (speed == 3.6f)
                                speed = 3.6f;

                            speeText.text = speed.ToString();
                            navAgent.speed = speed;
                            //animator.SetBool("SpeedUP", false);
                            //animator.SetFloat("Speed", speed);
                            //animator_parent.SetFloat("Speed", speed);
                            //longArmAnimator.SetBool("SpeedUP", false);
                            //longArmAnimator.SetFloat("Speed", speed);
                            pre_rate = _cur_rate;
                            //text1.text = " 1_달려 " + pre_rate.ToString() + " : " + stage2;
                        }
                        else if(Mathf.Abs(delta_rate) >= 0.1f && Mathf.Abs(delta_rate) <=0.4f)
                        {
                            if (speed < 7.2f)
                                speed += 0.3f;
                            else if (speed > 7.2f)
                                speed -= 0.3f;
                            else if (speed == 7.2f)
                                speed = 7.2f;

                            speeText.text = speed.ToString();
                            navAgent.speed = speed;
                            //animator.SetBool("SpeedUP", true);
                            //animator.SetFloat("Speed", speed);
                            //animator_parent.SetFloat("Speed", speed);
                            //longArmAnimator.SetBool("SpeedUP", true);
                            //longArmAnimator.SetFloat("Speed", speed);
                            pre_rate = _cur_rate;
                            //text1.text = " 2_달려 " + pre_rate.ToString() + " : " + stage2;
                        }



                        ////------여기 됩니다. 테스트 완료 버전1 ------///////
                        //if (speed < 3f)
                        //    speed += 0.1f;
                        //else if (speed >= 3f)
                        //    speed = 3f;

                        //text1.text = " ___pre_rate: " + pre_rate.ToString() + "__" + speed;
                        //animator.SetFloat("Speed", speed);
                        //animator_parent.SetFloat("Speed", speed);
                        //stage2 = true; //움직이고 있는 상태
                        //pre_rate = _cur_rate;

                    }
                    else if(Mathf.Abs(delta_rate) < 0.003f && stage2 == true)
                    {
                        timeNum += 1f;
                        //text3.text = " 슬로우: " + timeNum;
                        if (timeNum <= 50 && timeNum >= 10)
                        {
                            if(speed >= 0)
                                speed -= 0.5f;

                            speeText.text = speed.ToString();
                            navAgent.speed = speed;
                            //animator.SetFloat("Speed", speed);
                            //animator_parent.SetFloat("Speed", speed);
                            //longArmAnimator.SetFloat("Speed", speed);
                        }
                        if (timeNum >= 50)
                        {
                            stage2 = false;
                        }
                            
                    }
                }

                    
            }
        }

    }

}