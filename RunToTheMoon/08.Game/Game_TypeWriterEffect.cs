using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Game_TypeWriterEffect : MonoBehaviour
{
    public static Game_TypeWriterEffect instance { get; private set; }
    public GameObject Story_ob;
    public Animator Story_ani; //대화 이벤트 애니메이션
    //public Text m_StoryTypingText;  //대화 이벤트 텍스트
    public TMP_Text m_StoryTypingTex_mesh;  //대화 이벤트 텍스트
    //public InputField m_StoryTyping_field;  //대화 이벤트 텍스트
    public Game_Narration narration_sc;

    public string m_Message;

    public Text m_TitleTypingText; //타이틀 텍스트
    public Animator Title_ani;
    public string TitleText;

    float m_Speed = 0.09111111f;
    public float colorCount = 0;
    private float deltaDis;
    private float deltaDis_radio;
    private float spaceStationDis;
    private float spaceStationDis_radio;
    int way;
    public int wayPoint;
    //
    public int storyPosition = 0;

    public string[] EventText; //이벤트로 나오는 텍스트
    public string[] RandomText; //랜덤으로 나오는 텍스트
    public string[] EventRadioText; //이벤트로 나오는 라디오 텍스트
    public string[] RadioText; //라디오에서 나오는 텍스트

    public bool RadioEventAble = true;
    public bool RadioEvent_ing = false;
    public bool Event_ing = false; //내용이 다 
    public bool isReadyTextEvent = false;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        storyPosition = 0;
    }
    // Start is called before the first frame update 
    void Start()
    {
        isReadyTextEvent = false;
        spaceStationDis = Game_DataManager.instance.spaceStationDis;
        spaceStationDis_radio = Game_DataManager.instance.spaceStationDis;
   
        Show_TileText(); //시작할때 타이틀 !
    }
    public void Show_EventStoryText(int num)
    {
        Debug.Log("Show_EventStoryText() : " + num);
        Debug.Log("EventText : " + EventText.Length);
        if (EventText.Length != 0)
        {
            m_Message = EventText[num - 1];
            StartCoroutine(Typing_Story(m_StoryTypingTex_mesh, m_Message));
            Debug.Log("Show_EventStoryText()");
        }
    }

    public void Show_RadioStoryText()
    {
        if (EventRadioText.Length != 0)
        {
            Debug.Log("Show_RadioStoryText()");
            SoundFunction.Instance.Show_Radio_narration();
            RadioEvent_ing = true; //라디오 이벤트 진행중 , 텍스트 컬러 변경
            m_Message = EventRadioText[storyPosition];
            StartCoroutine(Typing_Story(m_StoryTypingTex_mesh, m_Message));
        }
    }
    public void Show_RandomStoryText()
    {
        if (RandomText.Length != 0)
        {
            Debug.Log("Show_RadioStoryText()");
            m_Message = RandomText[Random.Range(0, RandomText.Length)];
            StartCoroutine(Typing_Story(m_StoryTypingTex_mesh, m_Message));
        }
    }
    public void Show_RandomRadioStoryText()
    {
        if (RadioText.Length != 0)
        {
            Debug.Log("Show_RandomRadioStoryText()");
            SoundFunction.Instance.Show_Radio_narration();
            RadioEvent_ing = true; //라디오 이벤트 진행중 , 텍스트 컬러 변경
            m_Message = RadioText[Random.Range(0, RadioText.Length)];
            StartCoroutine(Typing_Story(m_StoryTypingTex_mesh, m_Message));
        }
    }
    //원하는 메세지 직접 입력
    public void Show_TypingText(string str)
    {
        m_Message = str;
        StartCoroutine(Typing_Story(m_StoryTypingTex_mesh, str));
    }
    IEnumerator Typing_Story(TMP_Text typingText, string message)
    {
        if (Event_ing)
        {
            yield return new WaitUntil(() => Event_ing == false);
        }
        Event_ing = true;
        typingText.text = " ";
        m_StoryTypingTex_mesh.color = Color.white; //텍스트 생상 초기화
        Story_ani.SetTrigger("open");
        yield return new WaitForSeconds(1.4f);
        typingText.text = message;
        for (int i = 0; i < message.Length; i++)
        {
           // Debug.LogError(colorCount);
            if (message.Substring(i,1).Equals("/"))
            {
                typingText.text = message.Substring(0, i + 1);
                if (colorCount.Equals(0))
                {
             //       Debug.LogError(colorCount + "---------------1");
                    m_StoryTypingTex_mesh.color = new Color32(155, 242, 255,255); //민트
                    message = message.Remove(i,1);
                    colorCount += 1;
                }
                else
                {
               //     Debug.LogError(colorCount + "---------------2");
                    m_StoryTypingTex_mesh.color = Color.white;
                    message = message.Remove(i, 1);
                    colorCount = 0;
                }
            }
            else if (RadioEvent_ing.Equals(true))
            {
                m_StoryTypingTex_mesh.color = new Color32(155, 242, 255, 255); //민트
            }
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(m_Speed);
         //   Debug.Log(typingText.text);
        }
        yield return new WaitForSeconds(1.4f);
        Story_ani.SetTrigger("close");
        Event_ing = false;
        RadioEvent_ing = false;
        m_Speed = 0.09111111f; //속도 초기화
    }
    //텍스트 나오는거 테스트하는중!
   // IEnumerator Typing_Story(TMP_Text typingText, string message)
   // {
   //     if (Event_ing)
   //     {
   //         yield return new WaitUntil(() => Event_ing == false);
   //     }
   //     Event_ing = true;
   //     typingText.text = " ";
   //     m_StoryTypingTex_mesh.color = Color.white; //텍스트 생상 초기화
   //     Story_ani.SetTrigger("open");
   //     yield return new WaitForSeconds(1.4f);
   //     typingText.text = message;
   //     if (message.Substring(i, 1).Equals("["))
   //     {
   //
   //     }
   //         yield return new WaitForSeconds(5f);
   //     Story_ani.SetTrigger("close");
   //     Event_ing = false;
   //     RadioEvent_ing = false;
   //     m_Speed = 0.09111111f; //속도 초기화
   // }
    //터치 할 수록 타이핑 속도가 빨라짐
    public void TypingBtn_speedUP()
    {
        m_Speed = 0f;
    }
    public void Show_TileText()
    {
        MapChartList();
    }
    IEnumerator Typing_Title(Text typingText, string message)
    {
        Event_ing = true;
        Title_ani.SetTrigger("TitleOpen");
        yield return new WaitForSeconds(1.4f);
        for (int i = 0; i < message.Length; i++)
        {
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(m_Speed);
        }
        yield return new WaitForSeconds(1f);
        Title_ani.SetTrigger("TitleClose");
        yield return new WaitForSeconds(1.4f);
        Title_ani.gameObject.SetActive(false);
        Event_ing = false;

        //Game_DataManager.instance.gamePlaying = true;     // 게임 시작
    }
    void MapChartList()
    {
        way = 1000 - (int)Game_DataManager.instance.moonDis;
        //1000m - 남은 달까지의 거리의 나머지가 0일 떄, 
        if (Game_DataManager.instance.moonDis % 50 == 0)
        {
            Title_ani.gameObject.SetActive(true);
            way = 1000 - (int)Game_DataManager.instance.moonDis;
            wayPoint = (way / 50) + 1; //맵차트 1,2,- 20
            if (wayPoint.Equals(1)) TitleText = " 여정의 시작"; //1 0-50
            else if (wayPoint.Equals(2)) TitleText = " 목적없는 발걸음";//2 51-100
            else if (wayPoint.Equals(3)) TitleText = " 달의 비밀"; //3
            else if (wayPoint.Equals(4)) TitleText = " 희망의 끈"; //4
            else if (wayPoint.Equals(5)) TitleText = " 길을 잃은 아기별"; //5
            else if (wayPoint.Equals(6)) TitleText = " 맴도는 공허함"; //6
            else if (wayPoint.Equals(7)) TitleText = " 빛의 무리"; //7
            else if (wayPoint.Equals(8)) TitleText = " 수상한 빛"; //8
            else if (wayPoint.Equals(9)) TitleText = "  나를 도와줘"; //9
            else if (wayPoint.Equals(10)) TitleText = " 불꽃놀이";   //10
            else if (wayPoint.Equals(11)) TitleText = " 소원석";    //11
            else if (wayPoint.Equals(12)) TitleText = " 발버둥 치는 마음";  //12
            else if (wayPoint.Equals(13)) TitleText = " 우주를 떠도는 영혼";    //13
            else if (wayPoint.Equals(14)) TitleText = " 함께하는 여정";    //14
            else if (wayPoint.Equals(15)) TitleText = " 목걸이의 주인";    //15
            else if (wayPoint.Equals(16)) TitleText = " 안녕, 별자리";    //16
            else if (wayPoint.Equals(17)) TitleText = " 몽환의 세계"; //17
            else if (wayPoint.Equals(18)) TitleText = " 점점 더 가까이";   //18
            else if (wayPoint.Equals(19)) TitleText = " 색을 잃은 별";    //19
            else if (wayPoint.Equals(20)) TitleText = " 달의 신전";  //20

            StartCoroutine(Typing_Title(m_TitleTypingText, TitleText));
        }
        else
        {
            if (0 <= way && way < 50) wayPoint = 1;
            else if (0 < way && way < 100) wayPoint = 2;
            else if (100 <= way && way < 150) wayPoint = 3;
            else if (150 <= way && way < 200) wayPoint = 4;
            else if (200 <= way && way < 250) wayPoint = 5;
            else if (250 <= way && way < 300) wayPoint = 6;
            else if (300 <= way && way < 350) wayPoint = 7;
            else if (350 <= way && way < 400) wayPoint = 8;
            else if (400 <= way && way < 450) wayPoint = 9;
            else if (450 <= way && way < 500) wayPoint = 10;
            else if (500 <= way && way < 550) wayPoint = 11;
            else if (550 <= way && way < 600) wayPoint = 12;
            else if (600 <= way && way < 650) wayPoint = 13;
            else if (650 <= way && way < 700) wayPoint = 14;
            else if (700 <= way && way < 750) wayPoint = 15;
            else if (750 <= way && way < 800) wayPoint = 16;
            else if (800 <= way && way < 850) wayPoint = 17;
            else if (850 <= way && way < 900) wayPoint = 18;
            else if (900 <= way && way < 950) wayPoint = 19;
            else if (950 <= way && way < 1000) wayPoint = 20;

            //Game_DataManager.instance.gamePlaying = true;     // 게임 시작
        }
        narration_sc.EventTextList(wayPoint);
        narration_sc.EventRandomTextList(wayPoint);
        narration_sc.EventTextRadio(wayPoint);
        narration_sc.EventRandomTextRadio(wayPoint);

        StartCoroutine(_Show_RandomStoryText());

        // 이후에 게임 시작 진행
        isReadyTextEvent = true;        
    }
    
    IEnumerator _Show_RandomStoryText()
    {
        if(!wayPoint.Equals(16) || !wayPoint.Equals(17))
        {
            float rand_StoryT = 80f;
            while (Game_DataManager.instance.gamePlaying)
            {
                if (!Event_ing)
                {
                    rand_StoryT -= Time.deltaTime;
                    deltaDis = spaceStationDis - Game_DataManager.instance.spaceStationDis;
                    deltaDis_radio = spaceStationDis_radio - Game_DataManager.instance.spaceStationDis;
                   //  Debug.Log(" deltaDis :" + deltaDis + " deltaDis_radio :"+ deltaDis_radio);
                   
                    if (rand_StoryT < 0f)
                    {
                        Debug.Log(" Show_RandomStoryText()");
                        if (!Event_ing)
                            Show_RandomStoryText();
                        spaceStationDis = Game_DataManager.instance.spaceStationDis;
                        rand_StoryT = 80f;
                    }
                    if (deltaDis_radio > 0.3f)
                    {
                         // Debug.Log("deltaDis" + deltaDis);

                        if (!Event_ing && RadioEventAble.Equals(true))
                        {
                            //필수 라디오 이벤트 
                            if (storyPosition < EventRadioText.Length)
                            {
                                Debug.Log("Show_RadioStoryText();");
                                Show_RadioStoryText();
                                storyPosition += 1;
                            }
                            else
                            {
                                Debug.Log("Show_RandomRadioStoryText();");
                                Show_RandomRadioStoryText();

                            }
                            deltaDis_radio = 0f;
                            spaceStationDis_radio = Game_DataManager.instance.spaceStationDis;
                        }

                    }
                    yield return null;
                }
                yield return null;
            }
        }

    }
    //특정구간에서 라디오가 나오지 않게 되면 코루틴 멈추기
    public void Stop_RadioEvnet()
    {
        StopCoroutine(_Show_RandomStoryText());
    }

}