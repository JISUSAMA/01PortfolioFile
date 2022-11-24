using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Linq;



public class Touch_GameManager : MonoBehaviour
{
    //이름명, 생년월일, 기록날짜
    public string game_kind;
    public List<string> QuestionStringList; //게임씬 이름 저장
    [Header("Brain")]
    public string[] Brain_gameName = { "BrainGame1", "BrainGame2", "BrainGame3", "BrainGame4", "BrainGame5", "BrainGame6", "BrainGame7", "BrainGame8" };
    public int Brain_QusetionCount = 8; //전체 문제 종류
    [Header("Dementia")]
    public string[] Dementia_gameName = { "DementiaGame1", "DementiaGame2", "DementiaGame3", "DementiaGame4", "DementiaGame5", "DementiaGame6", "DementiaGame7", "DementiaGame8" };
    public int Dementia_QusetionCount = 8; //전체 문제 종류
 
    public int CurrentQusetionNumber = 0; //문제 번호
    public int CurrentGameScore = 0;

    public List<Dictionary<string, object>> Save_CSVData;

    public string info_Name; 
    public string info_Birth; 
    public string info_PlayDate; 
    public string info_GameKind; 
    public string info_Score;

    public static Touch_GameManager instance { get; private set; }


    void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
        DontDestroyOnLoad(this.gameObject);

    }
    private void Start()
    {
        UserDataInit();
    }
    public List<T> GetShuffleList<T>(List<T> _list)
    {
        for (int i = _list.Count - 1; i > 0; i--)
        {
            int rnd = UnityEngine.Random.Range(0, i);

            T temp = _list[i];
            _list[i] = _list[rnd];
            _list[rnd] = temp;
        }
        return _list;
    }

    public void Game_LoadScene()
    {
        if (CurrentQusetionNumber < QuestionStringList.Count)
        {
            if(CurrentQusetionNumber.Equals(0))
                SceneManager.LoadScene(QuestionStringList[CurrentQusetionNumber]);
            else
            {
                if (SceneManager.GetActiveScene().name.Equals("TouchGame_WaitingZoon"))
                    SceneManager.LoadScene(QuestionStringList[CurrentQusetionNumber]);
                else
                    SceneManager.LoadScene("TouchGame_WaitingZoon");
            }
        }
        else
        {
            SceneManager.LoadScene("TouchGame_Finish");
            //끝!
            FinishGame();
        }
    }
    public void UserDataInit()
    {
        PlayerPrefs.SetString("UserName", "박지수");
        PlayerPrefs.SetString("UserBirth", "1996/02/18");

        info_Name = PlayerPrefs.GetString("UserName"); //플레이어 이름
        info_Birth = PlayerPrefs.GetString("UserBirth"); //플레이어 생일

        Save_CSVData = CSVReader.Read_file("SaveDataList");

        for(int i =0; i<Save_CSVData.Count; i++)
        {
            //현재 사용하고있는 유저와 이름과 생일이 같은 데이터의 자료를 가져옴
            if (Save_CSVData[i]["Name"].Equals(PlayerPrefs.GetString("UserName")) 
                && Save_CSVData[i]["Birth"].Equals(PlayerPrefs.GetString("UserBirth")))
            {
           
            }
        }
    }

    public void FinishGame()
    {
        info_Name = PlayerPrefs.GetString("UserName"); //플레이어 이름
        info_Birth = PlayerPrefs.GetString("UserBirth"); //플레이어 생일
     
        //오늘 날짜
        DateTime today = DateTime.Now;
        info_PlayDate = today.ToString("MM/dd/yyyy HH:mm");
        
        info_GameKind = game_kind;  //게임 플레이 종류
        info_Score = CurrentGameScore.ToString();

        Set_SaveDataFile(info_Name, info_Birth, info_PlayDate, info_GameKind, info_Score);

    }
    public void Set_SaveDataFile(string name, string birth, string playdate, string gamekind, string score)
    {
        string path = Application.persistentDataPath + "/SaveDataList.csv";
        Debug.Log(path);
        // This text is added only once to the file.
        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                string n = "Name,Birth,PlayDate,GameKind,Score";
                sw.WriteLine(n);
            }
        }
        else
        {
            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(path))
            {
                string n = info_Name + "," + info_Birth + "," + info_PlayDate + "," + info_GameKind + "," + info_Score;
                sw.WriteLine(n);
            }
        }
        // Open the file to read from.
        using (StreamReader sr = File.OpenText(path))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                Debug.Log(line);
            }
        }
    }
}
