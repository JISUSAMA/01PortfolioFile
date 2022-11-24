using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DementiaGame2_DataManager : MonoBehaviour
{
    public DementiaGame2_UIManager UIManager;

    public bool playBool = false;
    public float AnswerTimer_f; //15초
    public List<Dictionary<string, string>> proverb_data;
    public List<string> Proverb_List; //속담 문제 4개 
    public List<string> ProverbNum_List; //문제 번호 4개 ->해당 속담의 고유 번호를 담음
    public List<string> ProverbNum_Shuffle; //섞인 문제의 번호
     
    public int AnswerQuestionCount = 0;
    public static DementiaGame2_DataManager instance { get; private set; }
    /// <summary>
    /// 속담이 주어지고 주어진 속담과 관련 있는 그림과 연결하는 게임으로 한 페이지 당 4개의 속담과 그림이 주어 진다.
    /// 알맞은 속담과 글을 라인으로 연결하는 게임
    /// 1) 시간 내 완료하지 못하면 실패
    /// 2) 잘못된 항목 연결하면 실패
    /// </summary>
    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        else instance = this;


        proverb_data = CSVReader.Read("ProverbList"); //낱말찾기
        DementiaGame2_Play();
    }


    public void DementiaGame2_Play()
    {
        StopCoroutine("_DementiaGame2_Play");
        StartCoroutine("_DementiaGame2_Play");
    }
    IEnumerator _DementiaGame2_Play()
    {
        SetProverb();//랜덤으로 4개의 문제 선택 하고 리스트 안에 넣어줌
        FindWord_sec_Timer(20);
        yield return null;
    }

    void SetProverb()
    {
        StopCoroutine("_Set_ProverbList");
        StartCoroutine("_Set_ProverbList");
    }
    IEnumerator _Set_ProverbList()
    {
        //랜덤으로 4문제를 뽑아서 숫자리스트와 속담리스트 안에 넣어줌
        while (Proverb_List.Count < 4)
        {
            int randomNum = UnityEngine.Random.Range(0, proverb_data.Count - 1);
            string RandVerb_str = proverb_data[randomNum]["proverb"];
            if (Proverb_List.Count != 0)
            {
                if (!Proverb_List.Contains(RandVerb_str))
                {
                    Proverb_List.Add(proverb_data[randomNum]["proverb"]);
                    ProverbNum_List.Add(proverb_data[randomNum]["number"]);
                    ProverbNum_Shuffle.Add(proverb_data[randomNum]["number"]);
                }
            }
            else
            {
                Proverb_List.Add(proverb_data[randomNum]["proverb"]);
                ProverbNum_List.Add(proverb_data[randomNum]["number"]);
                ProverbNum_Shuffle.Add(proverb_data[randomNum]["number"]);
            }
            yield return null;
        }
        Touch_GameManager.instance.GetShuffleList(ProverbNum_Shuffle); //뽑은 속담 관련 숫자 리스트를 랜덤으로 섞음 
        //UI상에 뿌려줌
        for (int j = 0; j < 4; j++)
        {
            int Num_i = Int32.Parse(ProverbNum_List[j]);
            UIManager.ProverbText[j].text = Proverb_List[j];//속담을 넣어줌
            UIManager.ProverbImage[j].sprite = UIManager.ProverbSpriteKinds[j];//속담에 맞는 이미지를 넣어줌
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
