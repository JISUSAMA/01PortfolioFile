using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    public GameObject fireworks;
    public Score scoreManager;    //����
    public Timer timerManager; //Ÿ�̸�

    public Score RankUI_score;
    public Timer RankUI_timer;

    public Score ClassicUI_score;
    public Timer ClassicUI_timer;

    float TotalScore = 0;
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
        fireworks.SetActive(false); //clear Particle ��Ȱ��ȭ
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
            fireworks.SetActive(true); //clear Particle Ȱ��ȭ                                 
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
            if (GameManager.HaveChance.Equals(true))
            {
                PlayerPrefs.SetInt("GameTotalScore", (int)(TotalScore + scoreManager.score)); //total���� ����
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_Score", scoreManager.score); //ȹ�� ���� ����
                PlayerPrefs.SetString(SceneManager.GetActiveScene().name, SceneManager.GetActiveScene().name); //���� ���� �̸� ����
                PlayerPrefs.Save();
            }
            //���� Ÿ��! 1.5�� �߰� ����
            else
            {
                PlayerPrefs.SetInt("GameTotalScore", (int)(TotalScore + scoreManager.score * 1.5f)); //total���� ����
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_Score", scoreManager.score * 1.5f); //ȹ�� ���� ����
                PlayerPrefs.SetString(SceneManager.GetActiveScene().name, SceneManager.GetActiveScene().name); //���� ���� �̸� ����
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_Score_Classic", scoreManager.score); //ȹ�� ���� ����
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name, SceneManager.GetActiveScene().name); //���� ���� �̸� ����
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star_Classic", scoreManager.starPick); //�� ���� 
            PlayerPrefs.Save();
        }
    }
}
