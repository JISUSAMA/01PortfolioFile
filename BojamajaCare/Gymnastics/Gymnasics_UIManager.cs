using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gymnasics_UIManager : MonoBehaviour
{
    public static Gymnasics_UIManager instance { get; private set; }

    public RawImage display;
    WebCamTexture camTexture;
    private int currentIndex = 0;

    public Button VideoPlayButton;
    public Button VideoPauseButton;
    public RawImage VideoRawImage;
    public Text ExerciseTitleText; //운동 제목 
    public Text ExerciseNameText; //운동 설명 

    public int execiseOrder;    //키넥트운동 순서

    public Text PlayTimeText;
    public float PlayTime_f;
    public Slider PlayTime_Slider;
    public bool ExercisePlay_b = false;

    public Slider ExerciseOrder_Slider;
    public Text ExerciseOrder_text;

    List<Dictionary<string, object>> data;

    public GameObject FinishPopup;


    public Image FillSlider;
    float coolTime = 5f;
    float updateTime = 1f;
    public Text TimerText;


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }
    private void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log(devices[i].name);
        }
        AppSoundManager.Instance.StopBGM();
        //Show_WebCam();
        Exercise_SetUIdata();
        AppSoundManager.Instance.MainBGM_SoundPuase();
    }
    void Show_WebCam()
    {
        if (camTexture != null)
        {
            display.texture = null;
            camTexture.Stop();
            camTexture = null;
        }

        for (int i = 0; i < WebCamTexture.devices.Length; i++)
        {
            Debug.Log(i + "  " + WebCamTexture.devices[i].name);
            if (WebCamTexture.devices[i].name.Equals("Integrated Webcam"))
                currentIndex = i;
        }

        WebCamDevice device = WebCamTexture.devices[currentIndex];
        camTexture = new WebCamTexture(device.name);
        display.texture = camTexture;
        camTexture.Play();
    }
    //키넥트 운동종류 이름 반환
    public void Exercise_SetUIdata()
    {
        StopCoroutine(_Exercise_SetUIdata());
        StartCoroutine(_Exercise_SetUIdata());
    }
    IEnumerator _Exercise_SetUIdata()
    {
        data = CSVReader.Read_("GymnasicsData");
        Debug.Log("--------------------------------" + data);
        ExerciseOrder_Slider.value = execiseOrder + 1;
        ExerciseOrder_text.text = (execiseOrder + 1).ToString();
        //손가락 접기,손가락 체조를 따라해보세요.
        if (execiseOrder.Equals(0))
        {
            AppSoundManager.Instance.PlayGYM(execiseOrder.ToString());
            Debug.Log("--------------------------------" + execiseOrder);
            ExerciseTitleText.text = data[execiseOrder]["title"].ToString();
            ExerciseNameText.text = data[execiseOrder]["subtitle"].ToString();
            VideoHandler.instance.LoadVideo("Gymnasics", execiseOrder);

            VideoHandler.instance.PlayVideo();
            PlayVideo_ForTime(data[execiseOrder]["playtime"].ToString());

        }
        else if (execiseOrder.Equals(1))
        {
            AppSoundManager.Instance.PlayGYM(execiseOrder.ToString());
            ExerciseTitleText.text = data[execiseOrder]["title"].ToString();
            ExerciseNameText.text = data[execiseOrder]["subtitle"].ToString();
            VideoHandler.instance.LoadVideo("Gymnasics", execiseOrder);

            VideoHandler.instance.PlayVideo();
            PlayVideo_ForTime(data[execiseOrder]["playtime"].ToString());

        }
        else if (execiseOrder.Equals(2))
        {
            AppSoundManager.Instance.PlayGYM(execiseOrder.ToString());
            ExerciseTitleText.text = data[execiseOrder]["title"].ToString();
            ExerciseNameText.text = data[execiseOrder]["subtitle"].ToString();
            VideoHandler.instance.LoadVideo("Gymnasics", execiseOrder);
            VideoHandler.instance.PlayVideo();

            PlayVideo_ForTime(data[execiseOrder]["playtime"].ToString());

        }
        else if (execiseOrder.Equals(3))
        {
            AppSoundManager.Instance.PlayGYM(execiseOrder.ToString());

            ExerciseTitleText.text = data[execiseOrder]["title"].ToString();
            ExerciseNameText.text = data[execiseOrder]["subtitle"].ToString();
            VideoHandler.instance.LoadVideo("Gymnasics", execiseOrder);

            VideoHandler.instance.PlayVideo();
            PlayVideo_ForTime(data[execiseOrder]["playtime"].ToString());

        }
        else if (execiseOrder.Equals(4))
        {
            AppSoundManager.Instance.PlayGYM(execiseOrder.ToString());

            ExerciseTitleText.text = data[execiseOrder]["title"].ToString();
            ExerciseNameText.text = data[execiseOrder]["subtitle"].ToString();
            VideoHandler.instance.LoadVideo("Gymnasics", execiseOrder);

            VideoHandler.instance.PlayVideo();
            PlayVideo_ForTime(data[execiseOrder]["playtime"].ToString());

        }
        else if (execiseOrder.Equals(5))
        {
            AppSoundManager.Instance.PlayGYM(execiseOrder.ToString());

            ExerciseTitleText.text = data[execiseOrder]["title"].ToString();
            ExerciseNameText.text = data[execiseOrder]["subtitle"].ToString();
            VideoHandler.instance.LoadVideo("Gymnasics", execiseOrder);

            VideoHandler.instance.PlayVideo();
            PlayVideo_ForTime(data[execiseOrder]["playtime"].ToString());

        }
        else if (execiseOrder.Equals(6))
        {
            AppSoundManager.Instance.PlayGYM(execiseOrder.ToString());
            ExerciseTitleText.text = data[execiseOrder]["title"].ToString();
            ExerciseNameText.text = data[execiseOrder]["subtitle"].ToString();
            VideoHandler.instance.LoadVideo("Gymnasics", execiseOrder);
            VideoHandler.instance.PlayVideo();
            PlayVideo_ForTime(data[execiseOrder]["playtime"].ToString());

        }
        else if (execiseOrder.Equals(7))
        {
            AppSoundManager.Instance.PlayGYM(execiseOrder.ToString());

            ExerciseTitleText.text = data[execiseOrder]["title"].ToString();
            ExerciseNameText.text = data[execiseOrder]["subtitle"].ToString();
            VideoHandler.instance.LoadVideo("Gymnasics", execiseOrder);

            VideoHandler.instance.PlayVideo();
            PlayVideo_ForTime(data[execiseOrder]["playtime"].ToString());

        }
        else if (execiseOrder.Equals(8))
        {
            AppSoundManager.Instance.PlayGYM(execiseOrder.ToString());

            ExerciseTitleText.text = data[execiseOrder]["title"].ToString();
            ExerciseNameText.text = data[execiseOrder]["subtitle"].ToString();
            VideoHandler.instance.LoadVideo("Gymnasics", execiseOrder);
            VideoHandler.instance.PlayVideo();
            PlayVideo_ForTime(data[execiseOrder]["playtime"].ToString());

        }
        else if (execiseOrder.Equals(9))
        {
            AppSoundManager.Instance.PlayGYM(execiseOrder.ToString());

            ExerciseTitleText.text = data[execiseOrder]["title"].ToString();
            ExerciseNameText.text = data[execiseOrder]["subtitle"].ToString();
            VideoHandler.instance.LoadVideo("Gymnasics", execiseOrder);
            VideoHandler.instance.PlayVideo();
            PlayVideo_ForTime(data[execiseOrder]["playtime"].ToString());

        }
        else
        {
            FinishPopup.SetActive(true);
            Waiting_SecTime();
        }

        yield return null;
    }
    public void PlayVideo_ForTime(string timer)
    {
        int timer_int = int.Parse(timer);
        StopCoroutine(_PlayVideo_ForTime(timer_int));
        StartCoroutine(_PlayVideo_ForTime(timer_int));
    }
    IEnumerator _PlayVideo_ForTime(float timer)
    {
        PlayTime_Slider.value = 0;
        ExercisePlay_b = true;
        while (ExercisePlay_b)
        {
            VideoHandler.instance.ExcerciseTime(PlayTimeText);
            float seconds = (float)VideoHandler.instance.mVideoPlayer.time;
            PlayTime_Slider.maxValue = timer;
            PlayTime_Slider.value = seconds;
            if (PlayTime_Slider.value.Equals(PlayTime_Slider.maxValue))
            {
                ExercisePlay_b = false;
                break;
            }
            yield return null;
        }
        execiseOrder += 1;
        Exercise_SetUIdata();
    }

    //재생버튼
    public void PlayButtonClick()
    {
        VideoPlayButton.gameObject.SetActive(false);
        VideoPauseButton.gameObject.SetActive(true);

        VideoHandler.instance.PauseVideo(); //영상 일시정지
        Time.timeScale = 0; //일시정지
    }

    //일시정지 버튼
    public void PauseButtonClick()
    {
        VideoPauseButton.gameObject.SetActive(false);
        VideoPlayButton.gameObject.SetActive(true);

        VideoHandler.instance.PlayVideo();  //영상 재생
        Time.timeScale = 1; //재생
    }

    public void HomeButtonClick()
    {
      //  camTexture.Stop();
        Time.timeScale = 1; //재생
        SceneManager.LoadScene("0.Main");

    }
    public void Waiting_SecTime()
    {
        coolTime = 5;
        StartCoroutine("_Waiting_SecTime");
    }
    IEnumerator _Waiting_SecTime()
    {
        while (coolTime > updateTime)
        {
            updateTime += Time.deltaTime;
            FillSlider.fillAmount = 1 - (Mathf.Lerp(0, 100, (updateTime / coolTime) / 100));
            TimerText.text = (coolTime - updateTime).ToString("N0");
            yield return null;
        }

        updateTime = 1f;
        FillSlider.fillAmount = 0.0f;

        yield return new WaitForSeconds(1);
       // camTexture.Stop();
        SceneManager.LoadScene("0.Main"); //로그인 화면으로 이동
    }
}