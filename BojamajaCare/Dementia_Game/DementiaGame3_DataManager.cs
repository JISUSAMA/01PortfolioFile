using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DementiaGame3_DataManager : MonoBehaviour
{
    public DementiaGame3_UIManager UIManager;
    public bool playBool = false;
    public float AnswerTimer_f; 

    public List<Dictionary<string,string>> object_Data;
    public List<string> Object_Data_SuffleNum;

    public List<string> Object_Data_OB; //물체 이름 
    public List<string> Object_Data_Num;
    public int AnswerQuestionCount = 0; //맞춘 정답의 갯수
    /// <summary>
    /// 사물을 나타내는 그림과 해당 글자를 연결하는 게임. 총 네 개의 그림과 글자 쌍이 주어진다.
    /// 1) 시간 내 완료하지 못하면 실패
    /// 2) 잘못된 항목 연결하면 실패
    /// </summary>
    /// 
    public static DementiaGame3_DataManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        else instance = this;
        object_Data = CSVReader.Read("ObjectList"); //낱말찾기
    }
    private void Start()
    {
        DementiaGame3_Play();
    }
    public void DementiaGame3_Play()
    {
        StopCoroutine("_DementiaGame3_Play");
        StartCoroutine("_DementiaGame3_Play");
    }
    IEnumerator _DementiaGame3_Play()
    {
        Set_ObjectDataList();
        UIManager.DrawLine_Start();
        FindWord_sec_Timer(20);
        yield return null;
    }

    void Set_ObjectDataList()
    {
        StopCoroutine("_Set_ObjectDataList");
        StartCoroutine("_Set_ObjectDataList");
    }
    IEnumerator _Set_ObjectDataList()
    {
        while (Object_Data_Num.Count < 4)
        {
            int randomNum = UnityEngine.Random.Range(0, object_Data.Count - 1);
            string randomNum_str = object_Data[randomNum]["number"];
            if (!Object_Data_Num.Equals(0))
            {
                if (!Object_Data_Num.Contains(randomNum_str))
                {
                    Object_Data_Num.Add(object_Data[randomNum]["number"]);
                    Object_Data_OB.Add(object_Data[randomNum]["object"]);
                    Object_Data_SuffleNum.Add(object_Data[randomNum]["number"]);
                }
            }
            else
            {
                Object_Data_Num.Add(object_Data[randomNum]["number"]);
                Object_Data_OB.Add(object_Data[randomNum]["object"]);
                Object_Data_SuffleNum.Add(object_Data[randomNum]["number"]);
            }
            yield return null;
        }
        Touch_GameManager.instance.GetShuffleList(Object_Data_SuffleNum);
        for(int i =0; i<4; i++)
        {
            int Num_i =Int32.Parse(Object_Data_SuffleNum[i]);
            UIManager.ObjectImage[i].sprite = UIManager.ObjectSpriteKinds[Num_i-1];
            UIManager.ObjectText[i].text = Object_Data_OB[i];
        }
        yield return null;
    }
    public void Question_Success()
    {
        StopCoroutine("_Question_Success");
        StartCoroutine("_Question_Success");
    }
    IEnumerator _Question_Success()
    {
        UIManager.AnswerOb.SetActive(true);
        UIManager.AnswerImg.sprite = UIManager.AnswerImg_sprite[0];
        playBool = false;
        UIManager.DrawLine_GameRun = false;
        yield return new WaitForSeconds(1.5f);
        UIManager.AnswerOb.SetActive(false);
        Touch_GameManager.instance.CurrentQusetionNumber += 1;
        Touch_GameManager.instance.Game_LoadScene();
        yield return null;
    }
    public void Question_Fail()
    {
        StopCoroutine("_Question_Fail");
        StartCoroutine("_Question_Fail");
    }
    IEnumerator _Question_Fail()
    {
        UIManager.AnswerOb.SetActive(true);
        UIManager.AnswerImg.sprite = UIManager.AnswerImg_sprite[1];
        playBool = false;
        UIManager.DrawLine_GameRun = false;
        yield return new WaitForSeconds(1.5f);
        UIManager.AnswerOb.SetActive(false);
        Touch_GameManager.instance.CurrentQusetionNumber += 1;
        Touch_GameManager.instance.Game_LoadScene();
        yield return null;
    }
    public void FindWord_sec_Timer(float time)
    {
        AnswerTimer_f = time;
        playBool = true;
        StopCoroutine("_FindWord_sec_Timer");
        StartCoroutine("_FindWord_sec_Timer");
    }
    IEnumerator _FindWord_sec_Timer()
    {
        UIManager.AnswerTimer_slider.maxValue = AnswerTimer_f;
        UIManager.AnswerTimer_slider.value = AnswerTimer_f;
        while (playBool)
        {
            yield return new WaitForSeconds(0.5f);
            while (AnswerTimer_f > 0)
            {
                AnswerTimer_f -= Time.deltaTime;
                UIManager.AnswerTimer_slider.value = AnswerTimer_f;
                UIManager.AnswerTimer_text.text = AnswerTimer_f.ToString("N0") + " 초";
                if (!playBool) break;
                yield return null;
            }
            if (AnswerTimer_f <= 0)
            {
                AnswerTimer_f = 0;
                UIManager.AnswerTimer_text.text = " ";
                Question_Fail();
            }
        }
        //점수 Set
        if (AnswerTimer_f > 15) { Touch_GameManager.instance.CurrentGameScore = 15; }
        else if (AnswerTimer_f <= 15 && AnswerTimer_f > 10) { Touch_GameManager.instance.CurrentGameScore = 10; }
        else if (AnswerTimer_f <= 10 && AnswerTimer_f > 0) { Touch_GameManager.instance.CurrentGameScore = 5; }

        yield return null;
    }
}
