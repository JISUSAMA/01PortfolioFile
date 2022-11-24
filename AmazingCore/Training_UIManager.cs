using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Training_UIManager : MonoBehaviour
{
    [Header("Training Video Clip")]
    public Image BackGroundIMG;
    public Sprite[] BackSprite;

    public GameObject TutorialRaw_ob; 
    public GameObject TrainingRaw_ob;
    public GameObject IndicatorBox;
    public GameObject IndicatorAngle;
    public VideoClip[] mVideoClip_training;  //���� Ĩ
    public VideoClip[] mVideoClip_tutorial;  //���� Ĩ

    public GameObject Finish_VideoPopup;
    public VideoClip mVideoClip_finish;

    [Header("Training UI")]
    [Header("Training Description Text")]
    public GameObject pDescription_ob; //� ������ ��ġ 

    public Image PostureDescription_img;
    public Sprite[] PostureDescription_sp;

    [SerializeField] List<Dictionary<string, string>> pDescription_strList;
    

    public Image TrainingName_img;
    public Sprite[] TrainingName_sp;
    public Text pDescriotion_stringTxt; //� ����

    public Text TrainingTimer; 
    [Header("Break Time Grup")]
    public GameObject BreakTimeGrup_ob;
    public float breakTimer;
    public Text BreakTimer_text;

    [Header("Exit Popup")]
    [SerializeField] GameObject ExitPopupGrup_ob;
    Button YesBtn, NoBtn;

    [Header("Count Down")]
    public Image CountDown_img;
    public Sprite[] countDown_sp;

    //���� ������ ��, Video Handle �ȿ��ִ� mVideoPlayer, mVideoRawImage �����ϱ� 
  // [Header("Training Toutorial Video")]

    public void Tutorial_VideoPlay(int video_num)
    {
        VideoHandle.instance.Tutorial_VideoPlayer.clip = mVideoClip_tutorial[video_num];
        TutorialRaw_ob.gameObject.SetActive(true);
        VideoFinishTime_tutorial();
    }
    //Ʈ���̴� ���� �÷���
    public void Training_VideoPlay(int video_num)
    {
        VideoHandle.instance.Training_VideoPlayer.clip = mVideoClip_training[video_num];
        TrainingName_img.sprite = TrainingName_sp[Training_AppManager.instance.DataManager.Total_Training_count];
        TrainingRaw_ob.gameObject.SetActive(true);

        if (video_num == 6 || video_num == 7 || video_num == 8)
        {
            IndicatorBox.SetActive(false);
            IndicatorAngle.SetActive(true);
            // �ʱ�ȭ
            SensorManager.instance.resetRotation();
        }
        else
        {
            IndicatorBox.SetActive(true);
            IndicatorAngle.SetActive(false);
            // �ʱ�ȭ
            IndicatorCursor.distance = 0;
            SensorManager.instance.resetRotation();
        }

        VideoFinishTime_Training();       
    }


    //Ʃ�丮�� ������ ������ Ÿ�̹� Ȯ��
    public void VideoFinishTime_tutorial()
    {
        StopCoroutine(_VideoFinishTime_tutorial());
        StartCoroutine(_VideoFinishTime_tutorial());
    }
    IEnumerator _VideoFinishTime_tutorial()
    {
        VideoHandle.instance.Tutorial_VideoPlayer.Play();
        while (true)
        {
            long playerCurrentFrame = VideoHandle.instance.Tutorial_VideoPlayer.frame;
            long playerFrameCount = Convert.ToInt64(VideoHandle.instance.Tutorial_VideoPlayer.frameCount);
            if (playerCurrentFrame < playerFrameCount - 10) { }
            else
            {
                print("VIDEO IS OVER");
                yield return new WaitForSeconds(1f);
                TutorialRaw_ob.SetActive(false);
                Training_AppManager.instance.DataManager.TutorialPlayOver = true;
                break;
            }
            yield return null;
        }
    }
    //Ʈ���̴� ������ ������ Ÿ�̹� Ȯ��
    public void VideoFinishTime_Training()
    {
        StopCoroutine(_VideoFinishTime_Training());
        StartCoroutine(_VideoFinishTime_Training());
    }
    IEnumerator _VideoFinishTime_Training()
    {
        VideoHandle.instance.Training_PlayVideo();
        VideoHandle.instance.Training_VideoPlayer.Pause();

        TrainingTimer.text = getParseTime((float)VideoHandle.instance.Training_VideoPlayer.clip.length);
     pDescriotion_stringTxt.text
       = (int)VideoHandle.instance.Training_VideoPlayer.clip.length + "�� ���� ��Ȯ�� "
       + Training_AppManager.instance.DataManager.training_Name[Training_AppManager.instance.DataManager.Total_Training_count] + " �ڼ��� �������ּ���.";
        
        //ī��Ʈ �ٿ�
        CountDown_img.gameObject.SetActive(true);
        for (int i =0; i<4; i++)
        {
            CountDown_img.sprite = countDown_sp[i];
            yield return new WaitForSeconds(1f);
        }
        CountDown_img.gameObject.SetActive(false);
        print("Restart Video");
        VideoHandle.instance.Training_VideoPlayer.Play();
     
        while (true)
        {
            long playerCurrentFrame = VideoHandle.instance.Training_VideoPlayer.frame;
            long playerFrameCount = Convert.ToInt64(VideoHandle.instance.Training_VideoPlayer.frameCount);

            TrainingTimer.text = getParseTime((float)VideoHandle.instance.Training_VideoPlayer.clip.length 
                - (float)VideoHandle.instance.Training_VideoPlayer.time).ToString();
            
            if (playerCurrentFrame < playerFrameCount-10)
            {
                // print("VIDEO IS PLAYING");
               // print("Timer :" + VideoHandle.instance.Training_VideoPlayer.time);
               // print("cilp.length :" + VideoHandle.instance.Training_VideoPlayer.clip.length);
                //Debug.Log("playerCurrentFrame : " + playerCurrentFrame + " " + "playerFrameCount : " + playerFrameCount);
            }
            else
            {
                print("VIDEO IS OVER");
               // yield return new WaitForSeconds(1f);
               // VideoHandle.instance.ClearOutRenderTexture(VideoHandle.instance.Training_VideoRawImage);
                StopCoroutine(_Play_Training_VdieoCtrl());
                StartCoroutine(_Play_Training_VdieoCtrl());
                break;
            }
            yield return null;
        }
     
    }
   
    //Ʈ���̴� ���� 
    IEnumerator _Play_Training_VdieoCtrl()
    {
        //��Ʈ���� ���� ������ ���̰� Ǯ�������� �ݺ����� �ʰ� ������ ��� Ʈ���̴� ����
        //���̵� �ʱ�, ������ �ѹ��� �÷����ϰ� �������� �Ѿ

        if (GameManager.instance.CoreLevel_str.Equals("Beginner"))
        {
            print("VIDEO IS OVER-0-------------------------4");
            //Ʈ���̴� Ƚ�� Ȯ�� ���̵� ���� Ƚ���� �ٸ� �ʱ� 1ȸ, �߱� 2ȸ, ��� 3ȸ
            Training_AppManager.instance.DataManager.TrainingRound_count += 1;
            if (Training_AppManager.instance.DataManager.TrainingRound_count == 1)
            {
             //  print("TrainingRound_count :" + Training_AppManager.instance.DataManager.TrainingRound_count);
                Training_AppManager.instance.DataManager.Total_Training_count += 1;
                Training_AppManager.instance.DataManager.TrainingPlayOver = true;
                TrainingRaw_ob.SetActive(false);
            }
        }
        //�߱� 2Round
        else if (GameManager.instance.CoreLevel_str.Equals("intermediate"))
        {
            Training_AppManager.instance.DataManager.TrainingRound_count += 1;
            //print("TrainingRound_count 1:" + Training_AppManager.instance.DataManager.TrainingRound_count);
            //��Ʈ���� �ƴѰ�� ���� ������ �ݺ�
            if (!Training_AppManager.instance.DataManager.Total_Training_count.Equals(6))
            {
                if (Training_AppManager.instance.DataManager.TrainingRound_count == 2)
                {
                    Training_AppManager.instance.DataManager.Total_Training_count += 1;
                    Training_AppManager.instance.DataManager.TrainingPlayOver = true;
                    Training_AppManager.instance.DataManager.VideoPlayOver = true;
                    TrainingRaw_ob.SetActive(false);
                }
                else
                {
                    print("VIDEO IS OVER-0-------------------------5");
                    //�޽Ľð�
                    BreakTime_Timer();//���� �ð�
                    VideoHandle.instance.Training_StopVideo();
                    TrainingRaw_ob.gameObject.SetActive(false);
                    yield return new WaitUntil(() => Training_AppManager.instance.DataManager.BreakTimeOver == true);
                    Training_AppManager.instance.DataManager.BreakTimeOver = false;
                    //�޽Ľð� ������ ���� �ٽ� ���� ����
                    Training_VideoPlay(Training_AppManager.instance.DataManager.Total_Training_count);

                }
            }
            else
            {
                //��Ʈ�� ����
                 print("VIDEO IS OVER-0-------------------------4");
                 print("TrainingRound_count :" + Training_AppManager.instance.DataManager.TrainingRound_count);
                 Training_AppManager.instance.DataManager.Total_Training_count += 1;
                 Training_AppManager.instance.DataManager.TrainingPlayOver = true;
            }
       
        
        }
        //��� 3Round
        else if (GameManager.instance.CoreLevel_str.Equals("Advanced"))
        {
            Training_AppManager.instance.DataManager.TrainingRound_count += 1;
            //print("TrainingRound_count 3:" + Training_AppManager.instance.DataManager.TrainingRound_count);
            if (!Training_AppManager.instance.DataManager.Total_Training_count.Equals(6))
            {

                if (Training_AppManager.instance.DataManager.TrainingRound_count == 3)
                {
                    Training_AppManager.instance.DataManager.Total_Training_count += 1;
                    Training_AppManager.instance.DataManager.TrainingPlayOver = true;
                    Training_AppManager.instance.DataManager.VideoPlayOver = true;
                    TrainingRaw_ob.SetActive(false);
                }
                else
                {
                    //�޽Ľð�
                    BreakTime_Timer();//���� �ð�
                    VideoHandle.instance.Training_StopVideo();
                    yield return new WaitUntil(() => Training_AppManager.instance.DataManager.BreakTimeOver == true);
                    Training_AppManager.instance.DataManager.BreakTimeOver = false;
                    //�޽Ľð� ������ ���� �ٽ� ���� ����
                    //VideoHandle.instance.Training_PlayVideo();
                    Training_AppManager.instance.UIManager.Training_VideoPlay(Training_AppManager.instance.DataManager.Total_Training_count);
                }
            }
            else
            {
                //��Ʈ�� ����
                print("VIDEO IS OVER-0-------------------------4");
                print("TrainingRound_count :" + Training_AppManager.instance.DataManager.TrainingRound_count);
                Training_AppManager.instance.DataManager.Total_Training_count += 1;
                Training_AppManager.instance.DataManager.TrainingPlayOver = true;
            }
            yield return null;
        }
   
        yield return null;
    }

    //Ʈ���̴� �Ʒ� ��� ������ ������ ����
    public void TrainingEnd_VideoPlay()
    {
        StopCoroutine(_TrainingEnd_VideoPlay());
        StartCoroutine(_TrainingEnd_VideoPlay());
    }
    IEnumerator _TrainingEnd_VideoPlay()
    {
        //Ʃ�丮�� RenderRaw �ȿ� Finish���� �־���
        //VideoHandle.instance.Training_VideoPlayer.clip = mVideoClip_finish;
        //VideoHandle.instance.Training_PlayVideo(); //���� �����ϱ�
        Finish_VideoPopup.SetActive(true); //Ʃ�丮�� �˾� Ȱ��ȭ 

        VideoHandle.instance.Finish_VideoPlayer.Play();
        while (true)
        {
            long playerCurrentFrame = VideoHandle.instance.Finish_VideoPlayer.frame;
            long playerFrameCount = Convert.ToInt64(VideoHandle.instance.Finish_VideoPlayer.frameCount);
            if (playerCurrentFrame < playerFrameCount - 10)
            {

            }
            else
            {
                print("VIDEO IS OVER");
                GameManager.instance.LoadSceneName("02.ChooseMode");
                break;
            }
            yield return null;
        }
    }
    public void TrainingProgress_Save()
    {
        if (GameManager.instance.CoreLevel_str.Equals("Beginner"))
        {
            PlayerPrefs.SetInt("BeginnerClear", 1);
        }
        //�߱� 2Round
        else if (GameManager.instance.CoreLevel_str.Equals("intermediate"))
        {
            PlayerPrefs.SetInt("intermediateClear", 1);
        }
        //��� 3Round
        else if (GameManager.instance.CoreLevel_str.Equals("Advanced"))
        {
            PlayerPrefs.SetInt("AdvancedClear", 1);
        }
    }
    //////////////////////////////////////////////////////////////////////////////////
    //����� �˾� ��ŵ ���� ��
    public void DescriptionOver_Btn()
    {
        SoundCtrl.Instance.ClickButton_Sound();
        pDescription_ob.SetActive(false);
        Training_AppManager.instance.DataManager.DescriptionOver = true;
    }
    public void BreakTime_Timer()
    {
        StopCoroutine(_BreakTime_Timer());
        StartCoroutine(_BreakTime_Timer());
    }
    IEnumerator _BreakTime_Timer()
    {
        //VideoHandle.instance.Training_VideoRawImage.Release();
        BreakTimeGrup_ob.SetActive(true);
        breakTimer = 3f;
        while (breakTimer > 0)
        {
            breakTimer -= Time.deltaTime;
            BreakTimer_text.text = getParseTime(breakTimer);
            yield return null;
        }
        BreakTimer_text.text = "0";
        BreakTimeGrup_ob.SetActive(false);
        Training_AppManager.instance.DataManager.BreakTimeOver = true;
        yield return null;
    }
    //������ ��ư Ŭ��
    public void OnClick_ExitBtn()
    {
        SoundCtrl.Instance.ClickButton_Sound();
        //���� �˾� ���� 
        Time.timeScale = 0;
        ExitPopupGrup_ob.SetActive(true);
    }
    //�����ϱ�
    public void OnClick_ExitBtn_Yes()
    {
        SoundCtrl.Instance.ClickButton_Sound();
        //���� �˾� ���� 
        Time.timeScale = 1;
        GameManager.instance.LoadSceneName("02.ChooseMode");

    }
    //�̾��ϱ�
    public void OnClick_ExitBtn_No()
    {
        SoundCtrl.Instance.ClickButton_Sound();
        //���� �˾� ���� 
        Time.timeScale = 1;
        ExitPopupGrup_ob.SetActive(false);
    }
    public void OnClick_Tutorial_SkipBtn()
    {
        SoundCtrl.Instance.ClickButton_Sound();
        VideoHandle.instance.Tutorial_VideoPlayer.Stop();
        TutorialRaw_ob.SetActive(false);
        Training_AppManager.instance.DataManager.TutorialPlayOver = true;
    }
    //�ð� �� ���� ��ȯ
    public string getParseTime(float time)
    {
        string t = TimeSpan.FromSeconds(time).ToString("mm\\:ss");
        string[] tokens = t.Split(':');
        return tokens[0] + ':' + tokens[1];
    }
}
