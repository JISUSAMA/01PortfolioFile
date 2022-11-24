using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainGame7_DataManager : MonoBehaviour
{
    public BrainGame7_UIManager UIManager;
    public List<int> QuestionNumberList; 
    public string[] Answer_StrList; 

    public bool playBool = false;
    public float AnswerTimer_f = 15; //15초
    public static BrainGame7_DataManager instance { get; private set; }
    public void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }
    public void Start()
    {
        BrainGame7_Play();
    }
    public void BrainGame7_Play()
    {
        StopCoroutine("_BrainGame7_Play");
        StartCoroutine("_BrainGame7_Play");
    }
    IEnumerator _BrainGame7_Play()
    {
        Choose_Random_Question();
        FindWord_sec_Timer(15f);
        yield return null;
    }
    public void Choose_Random_Question()
    {
        StopCoroutine("_Choose_Random_Question");
        StartCoroutine("_Choose_Random_Question");
    }
    IEnumerator _Choose_Random_Question()
    {
        while (QuestionNumberList.Count < 6)
        {
            int randomNum = UnityEngine.Random.Range(0, 40);
            if (QuestionNumberList.Count != 0)
            {
                if (!QuestionNumberList.Contains(randomNum))
                {
                    QuestionNumberList.Add(randomNum);
                } 
            }
            else{  QuestionNumberList.Add(randomNum); }
            yield return null;
        }
        //데이터 값에 따라서 이미지를 뿌려줌
        for(int i =0; i<5; i++)
        {
            UIManager.Question_img_left[i].sprite = UIManager.LeftIMG_sprite[QuestionNumberList[i]];
            UIManager.Question_img_right[i].sprite = UIManager.RightIMG_sprite[QuestionNumberList[i]];
        }
        AnswerList();
    }
    public void AnswerList()
    {
        Answer_StrList = new string[5];
        for (int i =0; i < 5; i++)
        {
            string spriteName = UIManager.Question_img_right[i].sprite.name;
           // Debug.Log("spriteName" + spriteName);
            char sp = '_';
            string[] Answer_str = spriteName.Split(sp);
            Answer_StrList[i] =Answer_str[1];
          //  Debug.Log("Answer_str" + Answer_BoolList[i]);
        }
      
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
        if (AnswerTimer_f > 10) { Touch_GameManager.instance.CurrentGameScore = 20; }
        else if (AnswerTimer_f <= 10 && AnswerTimer_f > 5) { Touch_GameManager.instance.CurrentGameScore = 10; }
        else if (AnswerTimer_f <= 5 && AnswerTimer_f > 0) { Touch_GameManager.instance.CurrentGameScore = 5; }

        yield return null;
    }
}
