using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
   // public GameObject fireworks;
    public Score scoreManager;    //����
    public Timer timerManager; //Ÿ�̸�

    public Score RankUI_score;
    public Timer RankUI_timer;

    public Score ClassicUI_score;
    public Timer ClassicUI_timer;

    int TotalScore = 0;
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else Instance = this;

        if (GameManager.RankMode.Equals(true))
        {
            scoreManager = RankUI_score.GetComponent<Score>();
            timerManager = RankUI_timer.GetComponent<Timer>();
        }
        else
        {
            scoreManager = ClassicUI_score.GetComponent<Score>();
            timerManager = ClassicUI_timer.GetComponent<Timer>();
        }
    }
    public IEnumerator OnRoundStart()
    {
     //   fireworks.SetActive(false); //clear Particle ��Ȱ��ȭ
        timerManager.StartTimer(); // Ÿ�̸� ����
        scoreManager.Set(0);    // ���� �ʱ�ȭ    
        TotalScore = PlayerPrefs.GetInt("GameTotalScore");
        scoreManager.UpdateStarRating();
        yield return null;
    }
    public IEnumerator OnRoundEnd()
    {
        if (scoreManager.score > 0)
        {
         //   fireworks.SetActive(true); //clear Particle Ȱ��ȭ                                 
        }
        SaveDataScore();

        yield return null;
    }
    //����, ȹ������, ��������̸�, ���纰���� ����
    public void SaveDataScore()
    {
        //��ŷ ��� �� ���,
        if (GameManager.RankMode.Equals(true))
        {
  
                PlayerPrefs.SetInt("GameTotalScore", TotalScore + scoreManager.score); //total���� ����
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Score", scoreManager.score); //ȹ�� ���� ����
                PlayerPrefs.SetString(SceneManager.GetActiveScene().name, SceneManager.GetActiveScene().name); //���� ���� �̸� ����
                PlayerPrefs.Save();
                Debug.Log("Total Score : " + PlayerPrefs.GetInt("GameTotalScore"));
        
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Score_Classic", scoreManager.score); //ȹ�� ���� ����
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name, SceneManager.GetActiveScene().name); //���� ���� �̸� ����
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star_Classic", scoreManager.starPick); //�� ���� 
            PlayerPrefs.Save();
        }
    }


    // // debug fps
    // float time;
    // float fps;
    // float highest;
    // float lowest = Mathf.Infinity;
    // void OnGUI()
    // {
    //     GUIStyle style = new GUIStyle();
    //     style.fontSize = 80;
    //     style.normal.textColor = Color.white;
    //     GUI.Label(new Rect(20, Screen.height - 300, 100, 100), $"<color=red><b>{lowest.ToString("F2")}</b></color>", style);
    //     GUI.Label(new Rect(20, Screen.height - 200, 100, 100), $"<b>{fps.ToString("F2")}</b>", style);
    //     GUI.Label(new Rect(20, Screen.height - 100, 100, 100), $"<color=green><b>{highest.ToString("F2")}</b></color>", style);
    //     if (time > 0f)
    //     {
    //         time -= Time.deltaTime;
    //         return;
    //     }
    //     fps = 1f / Time.deltaTime;
    //     if (fps > highest) highest = fps;
    //     if (fps < lowest) lowest = fps;


    //     time = 0.5f;
    // }
}
