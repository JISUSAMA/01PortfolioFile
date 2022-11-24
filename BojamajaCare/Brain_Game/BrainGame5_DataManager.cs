using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainGame5_DataManager : MonoBehaviour
{
    [SerializeField] List<Dictionary<string, string>> data;
    public List<string> Order_word; //비교하는 리스트
    public List<string> Order_Input_word; //입력받은 단어\
    public List<string> PieceStringList;

    public BrainGame5_UIManager UIManager;
    public string[] OrderHead;

    public bool playBool = false;
    public float AnswerTimer_f = 15; //15초
    public static BrainGame5_DataManager instance { get; private set; }
    /*
        25개 칸에 숫자가 무작위로 배열되어 있음
        숫자는 아라비아 숫자와 한글 숫자 두 가지 중 하나로 표기될 수 있으며 이를 순서대로 터치해서 25까지 완료하면 완성되는 게임
        1) 순서와 다른 숫자를 터치하면 실패
        2) 시간 내 완료하지 못하면 실패     
    */
    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        data = CSVReader.Read("NumberList"); //낱말찾기
        OrderHead = new string[] { "KR", "NUM" };

    }
    //초기화
    void Initialization()
    {
        UIManager.CurrnetOrderNum = -1;
        Order_word.Clear(); //랜덤으로 글자 또는 수를 다시 넣어줌
        PieceStringList.Clear();
        for(int i =0; i<UIManager.NumberBtn.Length; i++) { UIManager.NumberBtn[i].interactable = true; }

    }
    private void Start()
    {
        BrainGame5_Play();
    }
    public void BrainGame5_Play()
    {
        StopCoroutine("_BrainGame5_Play");
        StartCoroutine("_BrainGame5_Play");
    }
    IEnumerator _BrainGame5_Play()
    {
        Initialization();
        Choose_Question_Word();
        FindWord_sec_Timer(30);
        //게임끝
        yield return null;
    }
    void Choose_Question_Word()
    {
        for (int i = 0; i < 25; i++)
        {
            string str = OrderHead[Random.Range(0, 2)];
            Order_word.Add(data[i][str]);
        }

        while (PieceStringList.Count < 25)
        {
            string rand_piecee = Order_word[Random.Range(0, Order_word.Count)];
            if (PieceStringList.Count != 0)
            {
                if (!PieceStringList.Contains(rand_piecee))
                {
                    PieceStringList.Add(rand_piecee);
                }
            }
            else
            {
                PieceStringList.Add(rand_piecee);
            }
        }
        for (int i = 0; i < UIManager.NumberBtn_text.Length; i++)
        {
            UIManager.NumberBtn_text[i].text = PieceStringList[i];
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
        if (AnswerTimer_f > 25) { Touch_GameManager.instance.CurrentGameScore = 20; }
        else if (AnswerTimer_f <= 25 && AnswerTimer_f > 20) { Touch_GameManager.instance.CurrentGameScore = 10; }
        else if (AnswerTimer_f <= 10 && AnswerTimer_f > 0) { Touch_GameManager.instance.CurrentGameScore = 5; }

        yield return null;
    }

}

