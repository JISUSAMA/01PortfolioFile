using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainGame1_DataManager : MonoBehaviour
{
    [Header("BrainGame1-Find Word And Touch")]
    public BrainGame1_UIManager UIManager; 
    [SerializeField] public string[] BrainGame1_Word_list = { "호랑이", "금붕어", "원숭이", "나무늘보", "이야기", "학교", "대나무", "플로토늄", "육교", "기린", "나무", "대학교", "나이", "교육", "무지개" };
    public List<string> BrainGame1_Piece_str_list;
    public List<string> QusetionPieceWord_list;
    int PieceBtnCount_int =25;

    public bool playBool = false;
    public float AnswerTimer_f; //15초
    public static BrainGame1_DataManager instance { get; private set; }
    /// <summary>
    /// 5*5 표 안에 배열된 글자들 속에서 아래 예시에서 요구하는 단어를 순서대로 터치하여 완성하기
    /// 1) 잘못된 글자 터치하면 실패
    /// 2) 순서 틀리면 실패
    /// 3) 제한 시간 넘기면 실패
    /// </summary>
    void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
        BrainGame1_Piece_str_list = new List<string> { "호", "랑", "이", "금", "붕", "어", "원", "숭", "육", "나", "무", "늘", "보", "린", "야", "기", "학", "교", "대", "지", "개", "플", "로", "토", "늄" };
    }
    private void Start()
    {
        BrainGame1_Play();
    }
    //초기화
    void Initialization()
    {
        UIManager.Click_num = -1;
        UIManager.QuestionWord_text.text = "";
        UIManager.FindWord_text.text = "";
        QusetionPieceWord_list.Clear();
    }
    public void BrainGame1_Play()
    {
        StopCoroutine("_BrainGame1_Play");
        StartCoroutine("_BrainGame1_Play");
    }
    IEnumerator _BrainGame1_Play()
    {
        Initialization();
        Choose_Random_Word();
        yield return null;
    }

    public void Choose_Random_Word()
    {
        Touch_GameManager.instance.GetShuffleList(BrainGame1_Piece_str_list);
        int rand_num = Random.Range(0, BrainGame1_Word_list.Length);
        UIManager.QuestionWord_text.text = BrainGame1_Word_list[rand_num];
        for (int i = 0; i < UIManager.QuestionWord_text.text.Length; i++)
        {
            QusetionPieceWord_list.Add(UIManager.QuestionWord_text.text.Substring(0, i + 1));
        }
  
        for (int i = 0; i < PieceBtnCount_int; i++)
        {
            UIManager.Piece_btn_text[i].text = BrainGame1_Piece_str_list[i];
        }
        FindWord_sec_Timer(15);
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
