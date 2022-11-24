using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DementiaGame8_DataManager : MonoBehaviour
{

    public DementiaGame8_UIManager UIManager;
    public bool playBool = false;
    public float AnswerTimer_f; //15초
    public List<string> Q1_str, Q2_str, Q3_str, Q4_str;
    [SerializeField] List<Dictionary<string, string>> data;
    public static DementiaGame8_DataManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
        data = CSVReader.Read("CalculationList"); //낱말찾기
    }
    void Start()
    {
        DementiaGame8_Play();
    }
    public void DementiaGame8_Play()
    {
        StopCoroutine(_DementiaGame8_Play());
        StartCoroutine(_DementiaGame8_Play());
    }
    IEnumerator _DementiaGame8_Play()
    {
        Set_Question_Data();
        FindWord_sec_Timer(15);
        yield return null;
    }

    public void Set_Question_Data()
    {
      int rand = Random.Range(0, data.Count);
        UIManager.Q1.text = data[rand]["Q1"];
        UIManager.Q2_answer = data[rand]["Q2"];
        UIManager.Q3.text = data[rand]["Q3"];
        UIManager.Q4_answer = data[rand]["Q4"];
    }
    public void Question_Success()
    {
        StopCoroutine("_Question_Success");
        StartCoroutine("_Question_Success");
    }
    IEnumerator _Question_Success()
    {
        UIManager.AnswerOb.SetActive(true);
        UIManager.AnswerImg.sprite = UIManager.AnswesrImg_sprite[0];
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
        UIManager.AnswerImg.sprite = UIManager.AnswesrImg_sprite[1];
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
        if (AnswerTimer_f > 10) { Touch_GameManager.instance.CurrentGameScore = 15; }
        else if (AnswerTimer_f <= 10 && AnswerTimer_f > 5) { Touch_GameManager.instance.CurrentGameScore = 10; }
        else if (AnswerTimer_f <= 5 && AnswerTimer_f > 0) { Touch_GameManager.instance.CurrentGameScore = 5; }
  

        yield return null;
    }
}
