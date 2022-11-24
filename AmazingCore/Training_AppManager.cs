using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training_AppManager : MonoBehaviour
{
    public Training_DataManager DataManager;
    public Training_UIManager UIManager;
    public GameObject bleDisconnectAlert;
    public static Training_AppManager instance { get; private set; }

    public void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
#if !UNITY_EDITOR && UNITY_ANDROID
        if (!SensorManager.instance._connected)
        {
            SoundCtrl.Instance.ClickButton_Sound();
            Time.timeScale = 0;
            bleDisconnectAlert.SetActive(true);
        }
#endif
        DataManager.Initialization(); //�ʱ�ȭ
    }

    private void OnEnable()
    {
        SensorManager.instance.connectState += ConnectState;
    }

    private void OnDisable()
    {
        SensorManager.instance.connectState -= ConnectState;
    }

    private void ConnectState(SensorManager.States reciever)
    {
        Debug.Log($"ConnectState is {reciever} in Training_AppManager");

        // ������ > ������� �˶� ���
        if (SensorManager.States.None == reciever && !SensorManager.instance._connected)
        {
            SoundCtrl.Instance.ClickButton_Sound();
            Time.timeScale = 0;
            bleDisconnectAlert.SetActive(true);
        }
    }

    private void Start()
    {
        VideoHandle.instance.ClearOutRenderTexture(VideoHandle.instance.Tutorial_VideoRawImage);
        VideoHandle.instance.ClearOutRenderTexture(VideoHandle.instance.Training_VideoRawImage);

        Training_Start();
    }
    public void Training_Start()
    {
        StopCoroutine(_Training_Start());
        StartCoroutine(_Training_Start());
    }
    IEnumerator _Training_Start()
    {
        //30�� ���� ��Ȯ�� �÷�ũ �ڼ��� �������ּ���.
        //�ʱ� : Beginner �߱�:  intermediate ���: Advanced
        //�ʱ� 1Round
        //��ü � 7���� 
        while (DataManager.Total_Training_count < 7)
        {
            if (!DataManager.Total_Training_count.Equals(6))
            {
                TrainingRoutine();
            }
            //��Ʈ��
            else
            {
                if (GameManager.instance.CoreLevel_str.Equals("Beginner"))
                {
                    JetUp_TrainingRoutine(6);
                }
                //�߱� 2Round
                else if (GameManager.instance.CoreLevel_str.Equals("intermediate"))
                {
                    JetUp_TrainingRoutine(7);
                }
                //��� 3Round
                else if (GameManager.instance.CoreLevel_str.Equals("Advanced"))
                {
                    JetUp_TrainingRoutine(8);
                }
            }
            yield return new WaitUntil(() => DataManager.TrainingPlayOver == true);

          
            UIManager.CountDown_img.sprite = UIManager.countDown_sp[4]; //finish
            UIManager.CountDown_img.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            UIManager.CountDown_img.gameObject.SetActive(false);
            DataManager.Check_PlayVideo_init(); //bool �� �ʱ�ȭ
            yield return null;
        }
        //2022.11.16
        GameManager.instance.CoreTraing_Count += 1;
        PlayerPrefs.SetInt("CoreTraing_Count", GameManager.instance.CoreTraing_Count);
        //

        UIManager.TrainingRaw_ob.gameObject.SetActive(false);
        //���� ��! ��� ���� �� ������
        UIManager.TrainingEnd_VideoPlay();
        UIManager.TrainingProgress_Save(); // �÷��� ������ ���� 
        yield return null;
    }
    //���� - Ʃ�丮�� - Ʈ���̴� ������ ��ƾ
    void TrainingRoutine()
    {
        StopCoroutine(_TraingRoutine());
        StartCoroutine(_TraingRoutine());
    }
    IEnumerator _TraingRoutine()
    {
        Debug.Log("Total_Training_count : " + DataManager.Total_Training_count);
        Debug.Log("TrainingRound_count : " + DataManager.TrainingRound_count);
       
        UIManager.BackGroundIMG.sprite = UIManager.BackSprite[DataManager.Total_Training_count];
        //���� ��� ���� ����
        UIManager.PostureDescription_img.sprite = UIManager.PostureDescription_sp[DataManager.Total_Training_count];
        UIManager.TrainingRaw_ob.SetActive(false);
        UIManager.pDescription_ob.SetActive(true);
        Debug.Log("------------------------------------------1");
        yield return new WaitUntil(() => DataManager.DescriptionOver == true);
        DataManager.Training_step_str = "DescriptionOver";
        //���� ��� ���� Ʃ�丮�� ����
        UIManager.Tutorial_VideoPlay(DataManager.Total_Training_count);
        Debug.Log("------------------------------------------2");
        yield return new WaitUntil(() => DataManager.TutorialPlayOver == true);
        DataManager.Training_step_str = "TutorialPlayOver";
        //���� ��� ���� Ʈ���̴� ����
        UIManager.Training_VideoPlay(DataManager.Total_Training_count);

        Debug.Log("------------------------------------------3");
        yield return new WaitUntil(() => DataManager.TrainingPlayOver == true);
        DataManager.Training_step_str = "TrainingPlayOver";

        //� �� �ð� ���ϱ� 
        float playT = PlayerPrefs.GetFloat("TotalPlayTime") + (int)VideoHandle.instance.Training_VideoPlayer.clip.length;
        PlayerPrefs.SetFloat("TotalPlayTime", playT);
        GameManager.instance.TotalPlay_Time = PlayerPrefs.GetFloat("TotalPlayTime");
        Debug.Log("playT :" + playT);
        Debug.Log("GetFloat :" + PlayerPrefs.GetFloat("TotalPlayTime"));

        Debug.Log("------------------------------------------4");
        yield return new WaitUntil(() => DataManager.VideoPlayOver == true);

     

    }
    //���� - Ʃ�丮�� - Ʈ���̴� ��Ʈ�� ��ƾ : ��, ��, �� ������ �ٴٸ�
    void JetUp_TrainingRoutine(int videoNum)
    {
        StopCoroutine(_TraingRoutine());
        StartCoroutine(_JetUp_TrainingRoutine(videoNum));
    }
    IEnumerator _JetUp_TrainingRoutine(int videoNum)
    {
        Debug.Log("Total_Training_count : " + DataManager.Total_Training_count);
        Debug.Log("TrainingRound_count : " + DataManager.TrainingRound_count);
        UIManager.BackGroundIMG.sprite = UIManager.BackSprite[DataManager.Total_Training_count];
        //���� ��� ���� ����
        UIManager.PostureDescription_img.sprite = UIManager.PostureDescription_sp[DataManager.Total_Training_count];
        UIManager.TrainingRaw_ob.SetActive(false);
        UIManager.pDescription_ob.SetActive(true);
        Debug.Log("------------------------------------------1");
        yield return new WaitUntil(() => DataManager.DescriptionOver == true);
        DataManager.Training_step_str = "DescriptionOver";
        //���� ��� ���� Ʃ�丮�� ����
        UIManager.Tutorial_VideoPlay(videoNum);
        Debug.Log("------------------------------------------2");
        yield return new WaitUntil(() => DataManager.TutorialPlayOver == true);
        DataManager.Training_step_str = "TutorialPlayOver";
        //���� ��� ���� Ʈ���̴� ����
        UIManager.Training_VideoPlay(videoNum);

        Debug.Log("------------------------------------------3");
        yield return new WaitUntil(() => DataManager.TrainingPlayOver == true);
        DataManager.Training_step_str = "TrainingPlayOver";

        //� �� �ð� ���ϱ� 
        float playT = PlayerPrefs.GetFloat("TotalPlayTime") + (int)VideoHandle.instance.Training_VideoPlayer.clip.length;
        PlayerPrefs.SetFloat("TotalPlayTime", playT);
        GameManager.instance.TotalPlay_Time = PlayerPrefs.GetFloat("TotalPlayTime");
    }
 
}
