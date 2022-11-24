using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DementiaGame5_DataManager : MonoBehaviour
{
    public DementiaGame5_UIManager UIManager;

    public bool playBool = false;
    public float AnswerTimer_f; //15초
    public static DementiaGame5_DataManager instance { get; private set; }
    /// <summary>
    /// 그림 속에 숨겨진 사물 5개를 정해진 시간 안에 찾는 게임
    /// 1) 시간 내 완료하지 못하면 실패
    /// </summary>
    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        else instance = this; 
    }
    private void Start()
    {
     
        DementiaGame5_Play();
    }
    public void DementiaGame5_Play()
    {
        StopCoroutine("_DementiaGame5_Play");
        StartCoroutine("_DementiaGame5_Play");
    }
    IEnumerator _DementiaGame5_Play()
    {
        UIManager.PaintGrup[Random.Range(0, UIManager.PaintGrup.Length)].gameObject.SetActive(true);
        FindWord_sec_Timer(30);
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
        if (AnswerTimer_f > 20) { Touch_GameManager.instance.CurrentGameScore = 15; }
        else if (AnswerTimer_f <= 20 && AnswerTimer_f > 10) { Touch_GameManager.instance.CurrentGameScore = 10; }
        else if (AnswerTimer_f <= 10 && AnswerTimer_f > 0) { Touch_GameManager.instance.CurrentGameScore = 5; }

        yield return null;
    }
}
