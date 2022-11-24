using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainGame8_DataManager : MonoBehaviour
{
    public Touch_TimerManager TimerManager_sc;
    public BrainGame8_UIManager UIManager;
    public bool playBool = false;
    public float AnswerTimer_f;
    public string QuestionAnswer; 
    /// <summary>
    /// 거울에 비친 시계가 있다. 이 시계가 현재 몇 시 몇 분인지 시간을 정확하게 적으면 성공이다.
    /// 시간을 적는 칸은 총 4칸으로 각 칸에 대해서 숫자를 넣어야 되고 1시의 경우 01시로 넣어야 한다
    /// </summary>

    public static BrainGame8_DataManager instance { get; private set; }
    public void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this; 
    }
    private void Start()
    {
        BrainGame8_Play();
    }
    public void BrainGame8_Play()
    {
        StopCoroutine("_BrainGame8_Play");
        StartCoroutine("_BrainGame8_Play");
    }
    IEnumerator _BrainGame8_Play()
    {
        Set_Question_Data();
        TimerManager_sc.FindWord_sec_Timer(15,10,5);
        yield return null;
    }
    public void Set_Question_Data()
    {
        int random = Random.Range(0, UIManager.QuestionSprite.Length);
        UIManager.QuestionImg.sprite = UIManager.QuestionSprite[random];
        string spriteName = UIManager.QuestionSprite[random].name;
       
        char sp = '_';
        string[] Answer_str = spriteName.Split(sp);
        string Answer_split = Answer_str[1];

        UIManager.Q_answer = new string[4];
        for (int i=0; i<Answer_str[1].Length; i++)
        {
            UIManager.Q_answer[i] = Answer_split[i].ToString();
        }
    }
 
}
