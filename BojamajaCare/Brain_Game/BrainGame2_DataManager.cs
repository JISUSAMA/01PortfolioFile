using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainGame2_DataManager : MonoBehaviour
{
    public BrainGame2_UIManager UIManager;
    public List<string> menu;
    public List<int> menu_price;
    public List<string> problem_list;
    public List<string> QuestionValue_list;

    public int Problem_number; //문제 번호 
    public int Question_number; //문제 번호 
    public bool playBool = false;
    public float AnswerTimer_f = 20; //20초
    public static BrainGame2_DataManager instance { get; private set; }
    /*
        식당에 방문한 당신
        메뉴판이 걸려 있다.
        당신과 친구가 먹은 음식의 값을 계산해서 얼마인지 입력하라
        1) 제한 시간 초과 시 실패
        2) 잘못 된 숫자 입력 시 실패
        3) 숫자 수정 가능
    */
    void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }
    private void Start()
    {
        BrainGame2_Play();
    }
    void Initialization()
    {
        menu.Clear();
        menu_price.Clear();
        problem_list.Clear();
        QuestionValue_list.Clear();
        UIManager.AnswerValue = "0";
        UIManager.AnswerNum.text = "0";
    }
    public void BrainGame2_Play()
    {
        StopCoroutine("_BrainGame2_Play");
        StartCoroutine("_BrainGame2_Play");
    }
    IEnumerator _BrainGame2_Play()
    {
        Initialization();
 
        //문제 번호 고르기
        GetMenuList(); //메뉴 리스트
        UIManager.SetTextDataText(); 
        FindWord_sec_Timer(20);
        //게임끝
        yield return null;
    }
    public void GetMenuList()
    {
        Problem_number = Random.Range(0, 3);
        if (Problem_number.Equals(0))
        {
            menu = new List<string> { "된장찌개", "김치찌개", "계란 말이", "매운탕", "공기밥" };
            menu_price = new List<int> { 5000, 5500, 2500, 7000, 1000 };
            problem_list = new List<string>
            { "김치찌개 2 + 공기밥 2", "매운탕 1 + 공기밥 2 + 계란말이 1", "된장찌개 2 + 공기밥 3", "된장찌개 3 + 계란말이 2 + 공기밥 1" ,"된장찌개 1 + 계란말이 1 + 공기밥 1"};
            QuestionValue_list = new List<string> { "13000", "11500", "13000", "21000", "8500" };
        }
        else if (Problem_number.Equals(1))
        {
            menu = new List<string> { "김밥", "떡볶이", "순대", "튀김", "라면" };
            menu_price = new List<int> { 3000, 2500, 4000, 1000, 1000 };
            problem_list = new List<string>
            { "깁밥 2 + 순대 1 + 튀김 1", "떡볶이 2 + 순대 1 + 튀김 1", "라면 2 + 김밥 2 + 튀김 1", "라면 1 + 순대 3 + 김밥 1" ,"깁밥 3 + 라면 2 + 튀김 1"};
            QuestionValue_list = new List<string> { "11000", "10000", "11000", "17000", "14000" };
        }
        else if (Problem_number.Equals(2))
        {
            menu = new List<string> { "광어초밥", "계란초밥", "연어알 초밥", "유부초밥", "우동" };
            menu_price = new List<int> { 3200, 2500, 3400, 2700, 3000 };
            problem_list = new List<string>
            { "광어초밥 2 + 계란초밥 3", "연어알 초밥 5 + 우동", "우동 2 + 유부초밥 2", "광어초밥 1 + 계란초밥 1 + 우동 1" ,"유부초밥 2 + 광어초밥 2"};
            QuestionValue_list = new List<string> { "26700", "20000", "11000", "17400", "17200" };
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
        if (AnswerTimer_f > 15) { Touch_GameManager.instance.CurrentGameScore = 20; }
        else if (AnswerTimer_f <= 15 && AnswerTimer_f > 10) { Touch_GameManager.instance.CurrentGameScore = 10; }
        else if (AnswerTimer_f <= 10 && AnswerTimer_f > 0) { Touch_GameManager.instance.CurrentGameScore = 5; }
        yield return null;
    }

}
