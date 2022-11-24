using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public string UserName;
    public string UserAge;
    public string UserHeight;
    public string UserWeight;

    public string StartExerciseDay;
    public string LastExerciseDay;

    [Header("� ����Ʈ")]
    public int CoreTraing_Count; 
    public int CoreGame_Count;
    public float TotalPlay_Time;
    //�ھ� Ʈ���̴� ���
    //�ʱ���, �߱���, �����
    // 
    [Header("Core Training")]
    public int ProfileLevel; //Ʈ���̴� ����
    public string CoreLevel_str;// �ʱ�, �߱�, ���
    public string CoreTrain_Name_str; //�ھ� Ʈ���̴� ��
   // public int TrainingLevel;

    //�ھ� ���� ���
    //Tower Maker, Delivery Man, Wire Walking
    public string CoreGame_Name_str; //�ھ� ���� ��
    public int CoreGame_Stage_i; //�ھ� ���� ����

    public int Tower_Clear_level;
    public int DeliveryMan_Clear_level;
    public int WireWalk_Clear_level;

    public static GameManager instance { get; private set; }

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        //PlayerPrefs.DeleteAll();
        Data_Initialization();
    }
    public void Data_Initialization()
    {
        if (PlayerPrefs.HasKey("Tower_Clear"))
        {
            Tower_Clear_level = PlayerPrefs.GetInt("Tower_Clear");
            DeliveryMan_Clear_level = PlayerPrefs.GetInt("DeliveryMan_Clear");
            WireWalk_Clear_level = PlayerPrefs.GetInt("WireWalking_Clear");
        }
        else
        {
            PlayerPrefs.SetInt("Tower_Clear", 0);
            PlayerPrefs.SetInt("DeliveryMan_Clear", 0);
            PlayerPrefs.SetInt("WireWalking_Clear", 0);
            Tower_Clear_level = 0;
            DeliveryMan_Clear_level = 0;
            WireWalk_Clear_level = 0;

        }
        //����� ���� ����
        UserName = PlayerPrefs.GetString("UserName");
        UserAge = PlayerPrefs.GetString("UserAge");
        UserHeight = PlayerPrefs.GetString("UserHeight");
        UserWeight = PlayerPrefs.GetString("UserWeight");
        StartExerciseDay= PlayerPrefs.GetString("StartExerciseDay");
        LastExerciseDay = PlayerPrefs.GetString("LastExerciseDay");
        //2022.11.11
        CoreTraing_Count = PlayerPrefs.GetInt("CoreTraing_Count");
        CoreGame_Count = PlayerPrefs.GetInt("CoreGameCount");
        TotalPlay_Time = PlayerPrefs.GetFloat("TotalPlayTime");
        //
        //Training Level 
        if (!PlayerPrefs.HasKey("BeginnerClear")) { PlayerPrefs.SetInt("BeginnerClear", 0); }
        if (!PlayerPrefs.HasKey("intermediateClear")) { PlayerPrefs.SetInt("intermediateClear", 0); }
        if (!PlayerPrefs.HasKey("AdvancedClear")) { PlayerPrefs.SetInt("AdvancedClear", 0); }

        ProfileLevel = PlayerPrefs.GetInt("BeginnerClear") + PlayerPrefs.GetInt("intermediateClear") + PlayerPrefs.GetInt("AdvancedClear");

    }
    public void LoadSceneName(string name)
    {
        SceneManager.LoadScene(name);
    }

}
