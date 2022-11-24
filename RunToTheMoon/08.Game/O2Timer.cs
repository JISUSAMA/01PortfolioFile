using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class O2Timer : MonoBehaviour
{
    public static O2Timer instance { get; private set; }

    public Text o2Text;
    public Slider o2Slider;
    public float currTime = 0f;

    public string[] o2TimeArr;  //산소통 시간 분 초 나눈거
    int[] o2Time_i; //산소 시간 분 초 나눈거 인트로
    int o2Time; //산소 초로 합친거

    int o2Full;
    float value = 0;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        TimeInit();
    }

    public void TimeInit()
    {
        string o2 = "00:40:00"; // 40분
        //string o2 = "0:01:00";
        char sp = ':';
        o2TimeArr = o2.Split(sp); // 00 40 00 분할
        o2Time_i = new int[o2TimeArr.Length];
        o2Time_i[0] = int.Parse(o2TimeArr[0]) * 60 * 60;    //시간을 초로
        o2Time_i[1] = int.Parse(o2TimeArr[1]) * 60; //분을 초로

        o2Time = o2Time_i[0] + o2Time_i[1] + o2Time_i[2];   //시간을 초로 합침 

        currTime = o2Time;
        o2Full = o2Time;
    }

    //소형 산소통 사용했을 경우
    public void SmallItemUse_OneTimePlus()
    {
        //    int _10minutes = 1 * 10 * 60;   // 10분
        //    int _40minutes = 1 * 40 * 60;   //40분
        //
        //    //남은 시간 + 10분이 40분보다 작거나 같으면
        //    if(currTime + (float)_10minutes <= _40minutes)
        //    {
        //        currTime += (float)_10minutes;
        //     }
        //    //남은 시간 + 10분이 40분 보다 크면
        //    else if (currTime + (float)_10minutes > _40minutes)
        //    {
        //        currTime = _40minutes;
        //    }
        GainO2("Small");
        RunnerPlayer1.instance.Use_Oxygen_Small(); //소형 산소통으로 변경
    }
    //대형 산소통 사용했을 경우,
    public void BigItemUse_OneTimePlus()
    {
        //  int _20minutes = 1 * 20 * 60;   //20분
        //  int _40minutes = 1 * 40 * 60;   //40분
        //
        //  //남은 시간 + 20분이 40분보다 작거나 같으면
        //  if (currTime + (float)_20minutes <= _40minutes)
        //  {
        //      currTime += (float)_40minutes;
        //  }
        //  //남은 시간 + 20분이 40분보다 크면
        //  else if (currTime + (float)_20minutes > _40minutes)
        //  {
        //      currTime = _40minutes;
        //  }
        GainO2("Big");
        RunnerPlayer1.instance.Use_Oxygen_Big(); //대형산소통으로 변경
    }

    void Start()
    {
        Timer();
        StartCoroutine(_Check5min());
    }

    public void Timer()
    {
        StartCoroutine(_Timer());
    }
    public void StopTimer()
    {
        StopCoroutine(_Timer());
    }
    IEnumerator _Timer()
    {
        while (currTime > 0f && Game_DataManager.instance.runEndState == false)
        {
       //     Debug.Log("dddddddddddddddddddddddddddddddddddddd");
            while (RunnerPlayer1.instance.StoryEventing.Equals(false))
            {
                // Scene 3, 11 터널 존재하면 1.5배 빨리 닳음
                if (SceneManager.GetActiveScene().name == "Game 3" && Chapter3_Manager.instance.Chapter3_pos_int.Equals(8)
                    || SceneManager.GetActiveScene().name == "Game 11" && Chapter11_Manager.instance.Chapter11_pos_int.Equals(3))
                {
                    currTime -= Time.deltaTime * 1.5f;
                    RunnerPlayer1.instance.Use_Oxygenbar_red();
                }
                //산소 1.5배 빨리 닳음
                else if (SceneManager.GetActiveScene().name == "Game 18" && Chapter18_Manager.instance.Chapter18_pos_int.Equals(10)
                  || SceneManager.GetActiveScene().name == "Game 6" && Chapter6_Manager.instance.Chapter6_pos_int.Equals(9))
                {
                    if (RunnerPlayer1.instance.O2_eventCheck)
                    {
                        currTime -= Time.deltaTime * 1.5f;
                        RunnerPlayer1.instance.Use_Oxygenbar_red();
                    }
                    //이벤트중 아님!
                    else
                    {
                        currTime -= Time.deltaTime;
                        RunnerPlayer1.instance.Use_OxygenBar_blue();
                    }
                }
                //달의 포자 구간 산소 1.2배 빨리 닳음 / 산소부족
                else if (SceneManager.GetActiveScene().name == "Game 8" && Chapter8_Manager.instance.Chapter8_pos_int.Equals(3))
                {
                    RunnerPlayer1.instance.Use_Oxygenbar_red();
                    currTime -= Time.deltaTime * 1.2f;
                }
                //달의 포자 잠깐 등장!
                else if (SceneManager.GetActiveScene().name == "Game 8" && Chapter8_Manager.instance.Chapter8_pos_int.Equals(9)
                    || SceneManager.GetActiveScene().name == "Game 11" && Chapter11_Manager.instance.Chapter11_pos_int.Equals(10)
                    || SceneManager.GetActiveScene().name == "Game 16" && Chapter16_Manager.instance.Chapter16_pos_int.Equals(10)
                    || SceneManager.GetActiveScene().name == "Game 10" && Chapter10_Manager.instance.Chapter10_pos_int.Equals(10))
                {
                    //달의 포자 지나가기 이벤트중!
                    if (RunnerPlayer1.instance.O2_eventCheck)
                    {
                        currTime -= Time.deltaTime * 1.2f;
                        RunnerPlayer1.instance.Use_Oxygenbar_red();
                    }
                    //이벤트중 아님!
                    else
                    {
                        currTime -= Time.deltaTime;
                        RunnerPlayer1.instance.Use_OxygenBar_blue();
                    }
                }

                else
                {
                    currTime -= Time.deltaTime;
                }

                int hours = (int)(currTime / 3600f);
                int minute = (int)(currTime % 3600 / 60);
                int second = (int)(currTime % 3600 % 60);
                o2Text.text = string.Format("{0:00} : {1:00}", minute, second); o2Slider.value = currTime / o2Full;

                yield return null;
            }
            if (currTime <= 0)
            {
                Time.timeScale = 0;
                Game_UIManager.instance.o2EndPopup_ob.SetActive(true);
            }
            yield return new WaitForEndOfFrame();
        }
         
    }
    IEnumerator _Check5min()
    {
        yield return new WaitUntil(() => currTime <= 300);
        Time.timeScale = 0;
        Game_UIManager.instance.minutePopup_ob.SetActive(true);
        yield return new WaitUntil(() => currTime <= 0);
        Time.timeScale = 0;
        Game_UIManager.instance.o2EndPopup_ob.SetActive(true);
        yield return null;
    }
    public void GainO2(string item)
    {
        if (item.Equals("Small"))
        {
         
            StartCoroutine(_Gain_O2(1 * 10 * 60));
        }
        if (item.Equals("Big"))
        {
            StartCoroutine(_Gain_O2(1 * 20 * 60));
        }

    }
    // 산소 획득시, 점점 올라가도록

    IEnumerator _Gain_O2(float plusTime)
    {
        value = 0f;
        float BaseTimeValue = currTime; //변하기 전의 현재 시간을 저장
        float PlusTimeValue = currTime + plusTime; //변하기 전의 현재 시간을 저장
        SoundFunction.Instance.ItemUse_Oxygen_Tankt();
        if (PlusTimeValue > 1 * 40 * 60)
        {
            PlusTimeValue = 1 * 40 * 60;
        }
        while (value < 1)
        {
            value += Time.deltaTime * 0.2f;
            //Debug.Log(value);
            o2Slider.value = Mathf.Lerp(BaseTimeValue / o2Full, PlusTimeValue / o2Full, value);
            currTime = Mathf.Lerp(BaseTimeValue, PlusTimeValue, value);
            Debug.Log("PlusTimeValue : " + PlusTimeValue);
            yield return null;
        }
        yield return null;
    }
}
