using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainGame4_DataManager : MonoBehaviour
{
    public DrawCtrl[] ShapeGrup;
    public int QuestionLeft_Num, QuestionRight_Num;
   // public BrainGame4_UIManager UIManager;
    bool Left_clear, Right_clear, GameEnd = false;

    public bool playBool = false;
    public float AnswerTimer_f = 15; //15초
    public static BrainGame4_DataManager instance { get; private set; }
    /*
        동그라미, 세모, 네모 이 세가지 도형이 양쪽에 다르게 그려져 있고 제한 시간 내에 양손으로 두 가지 다른 도형의 선을 따라 그림을 완성하면 되는 게임
        1) 시간 내 완료하지 못하면 실패
        2) 움직임을 가이드하는 원 밖을 벗어나면 실패
   */
    void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
        BrainGame4_Play();
    }
    void Initialization()
    {
        GameEnd = false;
    }

    public void BrainGame4_Play()
    {
        StopCoroutine("_BrainGame4_Play");
        StartCoroutine("_BrainGame4_Play");
    }
    IEnumerator _BrainGame4_Play()
    {
        BrainGame4_UIManager.instance.DrawLine_Start();
        FindWord_sec_Timer(15);
        //게임끝
        yield return null;
    }

    public void Question_Success()
    {
        StopCoroutine("_Question_Success");
        StartCoroutine("_Question_Success");
    }
    IEnumerator _Question_Success()
    {
        BrainGame4_UIManager.instance.AnswerOb.SetActive(true);
        BrainGame4_UIManager.instance.AnswerImg.sprite = BrainGame4_UIManager.instance.AnswerImg_sprite[0];
        playBool = false;
        yield return new WaitForSeconds(1.5f);
        BrainGame4_UIManager.instance.AnswerOb.SetActive(false);
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
        BrainGame4_UIManager.instance.AnswerOb.SetActive(true);
        BrainGame4_UIManager.instance.AnswerImg.sprite = BrainGame4_UIManager.instance.AnswerImg_sprite[1];
        playBool = false;
        yield return new WaitForSeconds(1.5f);
        BrainGame4_UIManager.instance.AnswerOb.SetActive(false);
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
        BrainGame4_UIManager.instance.AnswerTimer_slider.maxValue = AnswerTimer_f;
        BrainGame4_UIManager.instance.AnswerTimer_slider.value = AnswerTimer_f;
        while (playBool)
        {
            yield return new WaitForSeconds(0.5f);
            while (AnswerTimer_f > 0)
            {
                AnswerTimer_f -= Time.deltaTime;
                BrainGame4_UIManager.instance.AnswerTimer_slider.value = AnswerTimer_f;
                BrainGame4_UIManager.instance.AnswerTimer_text.text = AnswerTimer_f.ToString("N0") + " 초";
                if (!playBool) break;
                yield return null;
            }
            if (AnswerTimer_f <= 0)
            {
                AnswerTimer_f = 0;
                BrainGame4_UIManager.instance.AnswerTimer_text.text = " ";
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
