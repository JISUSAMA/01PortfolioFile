using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DementiaGame4_DataManager : MonoBehaviour
{
    public DementiaGame4_UIManager UIManager;

    public bool playBool = false;
    public float AnswerTimer_f; //15초

    public int AnswerQuestionCount = 0;
    public string[] Question_Kind_str = { "Pizza","Cake","Donuts","Color","Bar" };
    public string Kind_str;
    public List<int> QuestionNum;

    public List<int> Fraction_int;
    public List<int> Food_int;
    //public List<int> FoodShuffle_int;
    
    public static DementiaGame4_DataManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        else instance = this;
        DementiaGame4_play();
    }

    public void DementiaGame4_play()
    {
        StopCoroutine("_DementiaGame4_play");
        StartCoroutine("_DementiaGame4_play");
    }
    IEnumerator _DementiaGame4_play()
    {
        Set_Kind_Data();
        FindWord_sec_Timer(18);

        yield return null;
    }

    void Set_Kind_Data()
    {
     
        int Kind_rand = Random.Range(0, Question_Kind_str.Length);
        Kind_str = Question_Kind_str[Kind_rand].ToString();
        Debug.Log("Kind_rand" + Kind_rand + "Kind_str" + Kind_str);
        if (Kind_str.Equals("Pizza")) { Set_Kind_Img(UIManager.Fraction_kind1_img.Length); }
        else if (Kind_str.Equals("Cake")) { Set_Kind_Img(UIManager.Fraction_kind2_img.Length); }
        else if (Kind_str.Equals("Donuts")) { Set_Kind_Img(UIManager.Fraction_kind3_img.Length); }
        else if (Kind_str.Equals("Color")) { Set_Kind_Img(UIManager.Fraction_kind4_img.Length); }
        else if (Kind_str.Equals("Bar")) { Set_Kind_Img(UIManager.Fraction_kind5_img.Length); }

   
    }
    void Set_Kind_Img(int listLength)
    {
        Debug.Log("listLength" + listLength);
        StopCoroutine("_Set_Kind_Img");
        StartCoroutine("_Set_Kind_Img", listLength);
    }
    IEnumerator _Set_Kind_Img(int listLength)
    {
        int random;
        //출제할 문항 4개를 고름
        while (Fraction_int.Count < 4)
        {
            random= Random.Range(0, listLength);
            if (Fraction_int.Count!=0)
            {
                if (!Fraction_int.Contains(random))
                {
                    Fraction_int.Add(random);
                    Food_int.Add(random);
     
                }
            }
            else
            {
                Fraction_int.Add(random);
                Food_int.Add(random);
      
            }
            yield return null;
        }

        Touch_GameManager.instance.GetShuffleList(Food_int);
        //UI 이미지 적용
        for(int i =0; i<4; i++)
        {
            if (Kind_str.Equals("Pizza"))
            {
                UIManager.StartGrup_img[i].sprite = UIManager.Fraction_kind1_img[Fraction_int[i]];
                UIManager.EndGrup_img[i].sprite = UIManager.Food_kind1_img[Food_int[i]];
            }
            else if (Kind_str.Equals("Cake")) 
            { 
                UIManager.StartGrup_img[i].sprite = UIManager.Fraction_kind2_img[Fraction_int[i]]; 
                UIManager.EndGrup_img[i].sprite = UIManager.Food_kind2_img[Food_int[i]];
            }
            else if (Kind_str.Equals("Donuts")) 
            {
                UIManager.StartGrup_img[i].sprite = UIManager.Fraction_kind3_img[Fraction_int[i]];
                UIManager.EndGrup_img[i].sprite = UIManager.Food_kind3_img[Food_int[i]];
            }
            else if (Kind_str.Equals("Color")) 
            { 
                UIManager.StartGrup_img[i].sprite = UIManager.Fraction_kind4_img[Fraction_int[i]];
                UIManager.EndGrup_img[i].sprite = UIManager.Food_kind4_img[Food_int[i]];
            }
            else if (Kind_str.Equals("Bar")) 
            { 
                UIManager.StartGrup_img[i].sprite = UIManager.Fraction_kind5_img[Fraction_int[i]];
                UIManager.EndGrup_img[i].sprite = UIManager.Food_kind5_img[Food_int[i]];
            }

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
