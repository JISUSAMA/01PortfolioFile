using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class FinishManager : MonoBehaviour
{

    public Sprite[] Checks_img; //실패 성공 이미지 변경 스프라이트
    public Sprite[] Checks_LEAP_img; //실패 성공 이미지 변경 스프라이트
    public Button HomeButton;

    public GameObject[] KindGrup_ob; //touch /Leap /Kinect 에 따른 점수판
    [Header("Touch_Brain")]
    public Image[] BrainGrup_img; //게임 종류 base_back 이미지 
    public Text[] Brain_GameTitle_text; //게임의 이름
    public Text[] Brain_GameScore_text; //게임의 점수 

    [Header("Touch_Dementia")]
    public Image[] DementiaGrup_img; //게임 종류 base_back 이미지 
    public Text[] Dementia_GameTitle_text; //게임의 이름
    public Text[] Dementia_GameScore_text; //게임의 점수 

    [Header("Real")]
    public Image[] LeapGrup_img; //게임 종류 base_back 이미지 
    public Text[] Leap_GameTitle_text; //게임의 이름
    public Text[] Leap_GameScore_text; //게임의 점수 

    string nowTime_Str;
    private void Awake()
    {
        if (GameAppManager.instance.GameKind.Equals("Brain"))
            HomeButton.onClick.AddListener(() => OnClick_Touch_BackHome());
        else if (GameAppManager.instance.GameKind.Equals("Dementia"))
            HomeButton.onClick.AddListener(() => OnClick_Touch_BackHome());
        else if (GameAppManager.instance.GameKind.Equals("Real"))
            HomeButton.onClick.AddListener(() => OnClick_Leap_BackHome());
    }
    private void Start()
    {
        switch (GameAppManager.instance.GameKind)
        {
            case "Brain":
                KindGrup_ob[0].SetActive(true);
                for (int i = 0; i < 7; i++)
                {
                    Brain_GameTitle_text[i].text = GameAppManager.instance.BrainName[GameAppManager.instance.rand_GameNumber[i]]; //게임 한글명
                    if (!GameAppManager.instance.GamePlayScore[i].Equals(0))
                        Brain_GameScore_text[i].text = GameAppManager.instance.GamePlayScore[i].ToString() + "점";
                    else
                    {
                        BrainGrup_img[i].sprite = Checks_img[1]; //실패 판넬로 변경
                        Brain_GameScore_text[i].text = "실패";
                    }
                }
                break;
            case "Dementia":
                KindGrup_ob[1].SetActive(true);
                for (int i = 0; i < 8; i++)
                {
                    Dementia_GameTitle_text[i].text = GameAppManager.instance.DementiaName[GameAppManager.instance.rand_GameNumber[i]]; //게임 한글명
                    if (!GameAppManager.instance.GamePlayScore[i].Equals(0))
                        Dementia_GameScore_text[i].text = GameAppManager.instance.GamePlayScore[i].ToString() + "점";
                    else
                    {
                        DementiaGrup_img[i].sprite = Checks_img[1]; //실패 판넬로 변경
                        Dementia_GameScore_text[i].text = "실패";
                    }
                }
                break;
            case "Real":
                KindGrup_ob[2].SetActive(true);
                for (int i = 0; i < 5; i++)
                {
                    Leap_GameTitle_text[i].text = GameAppManager.instance.LeapName[GameAppManager.instance.LeapMotion_PlayNumList[i]]; //게임 한글명
                    if (!GameAppManager.instance.GamePlayScore[i].Equals(0))
                        Leap_GameScore_text[i].text = GameAppManager.instance.GamePlayScore[i].ToString() + "점";
                    else
                    {
                        LeapGrup_img[i].sprite = Checks_LEAP_img[1]; //실패 판넬로 변경
                        Leap_GameScore_text[i].text = "실패";
                    }
                }
                break;
        }
        //게스트가 아닌경우, 데이터 저장!
        if (PlayerPrefs.GetString("CARE_PlayMode") != "GuestMode")
        {
            DataFileSave();
            DayDataFileSave();
        }
     
    }
    //운동 파일에 저장하기
    public void DataFileSave()
    {
        StartCoroutine(_DataFileSave());
    }

    IEnumerator _DataFileSave()
    {
       
        string name = 
            PlayerPrefs.GetString("EP_UserNAME") + "_" + PlayerPrefs.GetString("EP_UserBrithDay")+ "_" + GameAppManager.instance.GameKind;
        string newFilePath = Application.persistentDataPath + "/Date/KinectExercise/" + name + ".csv";
        FileInfo fileinfo = new FileInfo(newFilePath);
        Debug.Log("Data File Save name :" + name);
        //파일 있는지 체크
        if (!fileinfo.Exists)
        {
            //파일 없음
            File.Create(newFilePath);   // 파일 생성
        }

        yield return new WaitForSeconds(4);

        List<Dictionary<string, object>> data;
        data = CSVReader.Read_("KinectExercise/" + name);

        StreamWriter writer = new StreamWriter(newFilePath);
        Debug.Log("플레이 횟수!!!!!!!!!!! :" + data.Count);
        //뇌게임
        if (GameAppManager.instance.GameKind.Equals("Brain"))
        {
            Debug.Log("플레이 횟수!!!Brain :" + data.Count);
            writer.WriteLine("횟수,운동1,운동2,운동3,운동4,운동5,운동6,운동7,총점");
            for (int i = 0; i <= data.Count + 1; i++)
            {
                //기존 갯수만큼 데이터 써준다
                if (i <= data.Count - 1)
                {
                    writer.WriteLine(data[i]["횟수"] + "," +
                        data[i]["운동1"] + "," +
                        data[i]["운동2"] + "," +
                        data[i]["운동3"] + "," +
                        data[i]["운동4"] + "," +
                        data[i]["운동5"] + "," +
                        data[i]["운동6"] + "," +
                        data[i]["운동7"] + "," +
                        data[i]["총점"]);
                }
                //마지막 새로운 데이터를 써준다.
                else if (i.Equals(data.Count))
                {
                    writer.WriteLine((i + 1).ToString() + "," +
                    GameAppManager.instance.GamePlayScore[0] + "," +
                      GameAppManager.instance.GamePlayScore[1] + "," +
                      GameAppManager.instance.GamePlayScore[2] + "," +
                      GameAppManager.instance.GamePlayScore[3] + "," +
                      GameAppManager.instance.GamePlayScore[4] + "," +
                      GameAppManager.instance.GamePlayScore[5] + "," +
                      GameAppManager.instance.GamePlayScore[6] + "," +
                        GameAppManager.instance.CurrentGameScore);
                    Debug.Log("GamePlayScore :"+i+" :" + GameAppManager.instance.GamePlayScore[i]);
                }
            }
        }
        //치매에방
        else if (GameAppManager.instance.GameKind.Equals("Dementia"))
        {
            Debug.Log("플레이 횟수!!!Dementia :" + data.Count);
            writer.WriteLine("횟수,운동1,운동2,운동3,운동4,운동5,운동6,운동7,운동8,총점");
            for (int i = 0; i <= data.Count + 1; i++)
            {
                //기존 갯수만큼 데이터 써준다
                if (i <= data.Count - 1)
                {
                    writer.WriteLine(data[i]["횟수"] + "," +
                        data[i]["운동1"] + "," +
                        data[i]["운동2"] + "," +
                        data[i]["운동3"] + "," +
                        data[i]["운동4"] + "," +
                        data[i]["운동5"] + "," +
                        data[i]["운동6"] + "," +
                        data[i]["운동7"] + "," +
                        data[i]["운동8"] + "," +
                        data[i]["총점"]);
                }
                //마지막 새로운 데이터를 써준다.
                else if (i.Equals(data.Count))
                {
                    writer.WriteLine((i + 1).ToString() + "," +
                        GameAppManager.instance.GamePlayScore[0] + "," +
                        GameAppManager.instance.GamePlayScore[1] + "," +
                        GameAppManager.instance.GamePlayScore[2] + "," +
                        GameAppManager.instance.GamePlayScore[3] + "," +
                        GameAppManager.instance.GamePlayScore[4] + "," +
                        GameAppManager.instance.GamePlayScore[5] + "," +
                        GameAppManager.instance.GamePlayScore[6] + "," +
                        GameAppManager.instance.GamePlayScore[7] + "," +
                        GameAppManager.instance.CurrentGameScore);
                }
            }
        }
        //손게임
        else if (GameAppManager.instance.GameKind.Equals("Real"))
        {
            writer.WriteLine("횟수,운동1,운동2,운동3,운동4,운동5,총점");
            for (int i = 0; i <= data.Count + 1; i++)
            {
                //기존 갯수만큼 데이터 써준다
                if (i <= data.Count - 1)
                {
                    writer.WriteLine(data[i]["횟수"] + "," +
                        data[i]["운동1"] + "," +
                        data[i]["운동2"] + "," +
                        data[i]["운동3"] + "," +
                        data[i]["운동4"] + "," +
                        data[i]["운동5"] + "," +
                        data[i]["총점"]);
                }
                //마지막 새로운 데이터를 써준다.
                else if (i.Equals(data.Count))
                {
                    writer.WriteLine((i + 1).ToString() + "," +
                    GameAppManager.instance.GamePlayScore[0] + "," +
                    GameAppManager.instance.GamePlayScore[1] + "," +
                    GameAppManager.instance.GamePlayScore[2] + "," +
                    GameAppManager.instance.GamePlayScore[3] + "," +
                    GameAppManager.instance.GamePlayScore[4] + "," +
                    GameAppManager.instance.CurrentGameScore);
                }
            }
        }
        writer.Flush();
        //This closes the file
        writer.Close();

        data = CSVReader.Read_("KinectExercise/" + name);
    }
    public void DayDataFileSave()
    {
        StartCoroutine(_DayDataFileSave());
    }

    IEnumerator _DayDataFileSave()
    {
        string name = 
              PlayerPrefs.GetString("EP_UserNAME") + "_" + PlayerPrefs.GetString("EP_UserBrithDay")+"_Day";
        string newFilePath = Application.persistentDataPath + "/Date/KinectExercise/" + name + ".csv";
        FileInfo fileinfo = new FileInfo(newFilePath);

        //파일 있는지 체크
        if (!fileinfo.Exists)
        {
            //파일 없음
            File.Create(newFilePath);   // 파일 생성
        }

        yield return new WaitForSeconds(3);

        List<Dictionary<string, object>> data;
        data = CSVReader.Read_("KinectExercise/" + name);

        StreamWriter writer = new StreamWriter(newFilePath);
        //
        Debug.Log("운동횟수 :" + data.Count);
        writer.WriteLine("운동횟수,운동일자");

        if (!data.Count.Equals(0))
        {
            for (int i = 0; i <= data.Count + 1; i++)
            {
                //기존 갯수만큼 데이터 써준다
                if (i <= data.Count - 1)
                {
                    writer.WriteLine(data[i]["운동횟수"] + "," +
                        data[i]["운동일자"]);
                }
                //마지막 새로운 데이터를 써준다.
                else if (i.Equals(data.Count))
                {
                    string Before_day_date = data[data.Count - 1]["운동일자"].ToString();
                    string Now_day_date = DateTime.Now.ToString("yyyy-MM-dd");
                    Debug.Log("운동일자 :" + Before_day_date + "  " + Now_day_date);
                    if (!Before_day_date.Equals(Now_day_date))
                    {
                        writer.WriteLine((i + 1).ToString() + "," +
                        DateTime.Now.ToString("yyyy-MM-dd"));
                    }

                }
            }
        }
        else
        {
            writer.WriteLine((data.Count + 1).ToString() + "," +
                  DateTime.Now.ToString("yyyy-MM-dd"));
        }
        writer.Flush();
        //This closes the file
        writer.Close();

        data = CSVReader.Read_("KinectExercise/" + name);
    }

    public void OnClick_Touch_BackHome()
    {
        StartCoroutine(_OnClick_BackHome());
    }
  
    public void OnClick_Leap_BackHome()
    {
        StartCoroutine(_OnClick_BackHome());
    }
    IEnumerator _OnClick_BackHome()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("8.ChooseGame");
    }
    public void Data_WriteToDay()
    {
        DateTime.Now.ToString("yyyy-MM-dd");
    }
}
