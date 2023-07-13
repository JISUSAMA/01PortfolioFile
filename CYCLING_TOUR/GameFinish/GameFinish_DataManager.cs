using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameFinish_DataManager : MonoBehaviour
{
    public static GameFinish_DataManager instance { get; private set; }


    string[] openTime;
    string[] asia_courseTime;   //아시아 노멀맵 시간

    public Button[] coinBoxBtn;
    public GameObject[] openVfx;    //오픈 파티클
    public Button checkBtn;   //코인오픈팝업 버튼

    public Sprite[] openBoxImg; //동은금
    public GameObject rewardPanel;
    public Text[] rewardCoinText;
    
    public Button[] replayBtn;    //다시하기버튼
    public Button[] playEndBtn;   //종료하기버튼

    public GameObject[] questObj; //퀘스트 리스트 항목
    public Text[] questTitleText;   //퀘스트 제목
    public GameObject[] questObj_fail;  //실패 리스트 항목
    public Text[] questTitleText_fail;  //실패 퀘스트 제목
    public Sprite failSprite;   //실패 이미지

    public GameObject[] baseItemImg;    //기존 아이템 이미지(코인경험치)
    public GameObject[] itemUseImg; //아이템 사용 이미지

    public GameObject questRewardPanel; //퀘스트보상 판넬
    public GameObject gameEndPopup; //게임종료팝업


    public int reward; //보상 금액
    float courseTime; //골인 시간

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    

    void Start()
    {
        //Debug.Log("값 : " + PlayerPrefs.GetString("Asia Normal 1Course"));
    }

    private void Update()
    {
        if (Application.platform.Equals(RuntimePlatform.Android))
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                gameEndPopup.SetActive(true);
            }
        }
    }

    //코스별 시간 기록 가져오는 함수

    public string CourseOpenTimeState(string _data)
    {
        //Debug.Log(" === " + _data);
        
        if (_data != "")
        {
            string courseData = _data;    //예 : 2021/12/10:00

            char sp = '/';
            openTime = courseData.Split(sp);   //2021,12,10:00 따로 저장

            char bar = ':';
            string[] _openTime = openTime[1].Split(bar); //10:00 분해해서 10,00 따로
            courseTime = (int.Parse(_openTime[0]) * 60 * 60) + int.Parse(_openTime[1]) * 60 + float.Parse(_openTime[2]);

            //Debug.Log(courseTime + " `````````````````````````````" + openTime[1]);
            //게임한 시간 따로 저장 - 시간제한 퀘스트 때문에 / 껐다켰을때 최고 좋은 점수가 서버에서 들고오기 때문에 퀘스트 미션이 진행이 안됨.
            if(PlayerPrefs.GetString("AT_MapCourseName").Equals("Normal-1"))
                PlayerPrefs.SetString("AT_CurrPlayTime1", "Normal-1/" + openTime[1]);
            else if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Normal-2"))
                PlayerPrefs.SetString("AT_CurrPlayTime2", "Normal-2/" + openTime[1]);
            else if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Normal-3"))
                PlayerPrefs.SetString("AT_CurrPlayTime3", "Normal-3/" + openTime[1]);
            else if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Hard-1"))
                PlayerPrefs.SetString("AT_CurrPlayTime4", "Hard-1/" + openTime[1]);
            else if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Hard-2"))
                PlayerPrefs.SetString("AT_CurrPlayTime5", "Hard-2/" + openTime[1]);
            else if (PlayerPrefs.GetString("AT_MapCourseName").Equals("Hard-3"))
                PlayerPrefs.SetString("AT_CurrPlayTime6", "Hard-3/" + openTime[1]);
        }
        //Debug.Log(" -- 놀고싶다 -- : " + PlayerPrefs.GetString("AT_CurrPlayTime3"));
        return openTime[1];
    }

    //다음 코스 오픈위한 시간타임
    public string NextCourseOpenTimeMission()
    {
        int time = PlayerPrefs.GetInt("AT_MapCourseOpenTime");
        time = time / 60;
        //_timeText.text = time.ToString() + "완주달성";

        return time.ToString();
    }

    //보상 랜덤 코인 박스
    public void CoinBoxOpen()
    {
        PlayerPrefs.SetString("AT_PlayQuestState", "Yes");  //보상받기위한 게임 플레이를했다.
        MapQuestCourseFinishAmount();   //완주 횟수 저장

        //Debug.Log("코인 이름 " + EventSystem.current.currentSelectedGameObject.name);
        if (EventSystem.current.currentSelectedGameObject.name == "CoinButton1")
        {
            rewardCoinText[0].text = RandomCoin();
            rewardCoinText[1].text = rewardCoinText[0].text;

            CoinImageChange("CoinButton1");  //획득에 따른 코인 박스 변경
            openVfx[0].SetActive(true); //파티클 오픈

            coinBoxBtn[0].interactable = false;
            coinBoxBtn[1].interactable = false;
            coinBoxBtn[2].interactable = false;


            Invoke("RewardTextShow", 1f);
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "CoinButton2")
        {
            rewardCoinText[0].text = RandomCoin();
            rewardCoinText[1].text = rewardCoinText[0].text;

            CoinImageChange("CoinButton2");//획득에 따른 코인 박스 변경
            openVfx[1].SetActive(true); //파티클 오픈

            coinBoxBtn[0].interactable = false;
            coinBoxBtn[1].interactable = false;
            coinBoxBtn[2].interactable = false;

            Invoke("RewardTextShow", 1f);
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "CoinButton3")
        {
            rewardCoinText[0].text = RandomCoin();
            rewardCoinText[1].text = rewardCoinText[0].text;

            CoinImageChange("CoinButton3");//획득에 따른 코인 박스 변경
            openVfx[2].SetActive(true); //파티클 오픈

            coinBoxBtn[0].interactable = false;
            coinBoxBtn[1].interactable = false;
            coinBoxBtn[2].interactable = false;

            Invoke("RewardTextShow", 1f);
        }
    }

    //획득 코인에 따른 코인박스 변경
    void CoinImageChange(string _btnName)
    {
        int _index = 0;
        if (_btnName.Equals("CoinButton1"))
            _index = 0;
        else if (_btnName.Equals("CoinButton2"))
            _index = 1;
        else if (_btnName.Equals("CoinButton3"))
            _index = 2;

        if (reward >= 100 && reward <= 600)
            coinBoxBtn[_index].gameObject.GetComponent<Image>().sprite = openBoxImg[0];
        else if (reward >= 700 && reward <= 900)
            coinBoxBtn[_index].gameObject.GetComponent<Image>().sprite = openBoxImg[1];
        else if (reward >= 1000)
            coinBoxBtn[_index].gameObject.GetComponent<Image>().sprite = openBoxImg[2];
    }

    //획득한 코인 프리팹에 저장
    void RewardTextShow()
    {
        rewardPanel.SetActive(true);
        checkBtn.interactable = true;    //버튼 활성화

        string rewardStr = rewardCoinText[0].text.Replace(",", "");

        if (PlayerPrefs.GetString("AT_UseItemCoin").Equals("Coin"))
            PlayerPrefs.SetInt("AT_Player_Gold", PlayerPrefs.GetInt("AT_Player_Gold") + int.Parse(rewardStr) *2);  //코인 토탈 저장
        else
            PlayerPrefs.SetInt("AT_Player_Gold", PlayerPrefs.GetInt("AT_Player_Gold") + int.Parse(rewardStr));  //코인 토탈 저장

        //코인 토탈 저장
        ServerManager.Instance.Update_Gold_Data(int.Parse(rewardStr));
    }

    //숫자 콤마 찍는 함수
    public string CommaText(int _data)
    {
        if (_data != 0)
            return string.Format("{0:#,###}", _data);
        else
            return "0";
    }


    //아이템사용 exp 이미지 표시
    public void ItemUseExpImageShow()
    {
        if (PlayerPrefs.GetString("AT_UseItemExp").Equals("Exp"))
        {
            baseItemImg[1].SetActive(false);
            itemUseImg[1].SetActive(true);
        }
        else
        {
            baseItemImg[1].SetActive(true);
            itemUseImg[1].SetActive(false);
        }  
    }

    //아이템사용 coin 이미지 표시
    public void ItemUseCoinImageShow()
    {
        //코인2배 아이템 사용했으면 이미지 보여줌.
        if (PlayerPrefs.GetString("AT_UseItemCoin").Equals("Coin"))
        {
            baseItemImg[0].SetActive(false);
            itemUseImg[0].SetActive(true);
        }
        else
        {
            baseItemImg[0].SetActive(true);
            itemUseImg[0].SetActive(false);
        }
    }

    //랜덤경험치 부여
    public int RandomExp()
    {
        string mapName = PlayerPrefs.GetString("AT_MapCourseName");
        int expRandom = 0;
        //성공했을 시 - 노멀(61~80) 하드(81~100)
        if (PlayerPrefs.GetString("AT_FinishState") == "Clear")
        {
            if (mapName.Equals("Normal-1") || mapName.Equals("Normal-2") || mapName.Equals("Normal-3"))
            {
                
                expRandom = Random.Range(61, 81);
                //Debug.Log("expRandom " + expRandom);
            }
            else if(mapName.Equals("Hard-1") || mapName.Equals("Hard-2") || mapName.Equals("Hard-3"))
            {
                expRandom = Random.Range(81, 101);
            }
        }
        //실패했을 시 30~40
        else if (PlayerPrefs.GetString("AT_FinishState") == "Fail")
        {
           expRandom = Random.Range(30, 40);
        }
        return expRandom;
    }

    //코인 보상 0:500 ~ 5:1000
    string RandomCoin()
    {
        int coinRandom = Random.Range(0, 6);

        if (PlayerPrefs.GetString("AT_FinishState") == "Clear")
        {
            if (coinRandom == 0)
                reward = 500;
            else if (coinRandom == 1)
                reward = 600;
            else if (coinRandom == 2)
                reward = 700;
            else if (coinRandom == 3)
                reward = 800;
            else if (coinRandom == 4)
                reward = 900;
            else if (coinRandom == 5)
                reward = 1000;
        }
        else if (PlayerPrefs.GetString("AT_FinishState") == "Fail")
        {
            if (coinRandom == 0)
                reward = 500 / 5;
            else if (coinRandom == 1)
                reward = 600 / 5;
            else if (coinRandom == 2)
                reward = 700 / 5;
            else if (coinRandom == 3)
                reward = 800 / 5;
            else if (coinRandom == 4)
                reward = 900 / 5;
            else if (coinRandom == 5)
                reward = 1000 / 5;
        }
        return CommaText(reward);
    }

    //다시학, 완주종료 버튼 -interactable 상태
    public void RePlay_PlayEnd(bool _btnState)
    {
        if (PlayerPrefs.GetString("AT_FinishState") == "Clear")
        {
            replayBtn[0].interactable = _btnState;
            playEndBtn[0].interactable = _btnState;
        }
        else if (PlayerPrefs.GetString("AT_FinishState") == "Fail")
        {
            replayBtn[1].interactable = _btnState;
            playEndBtn[1].interactable = _btnState;
        }
    }



    //////////----결과화면에 나오는 퀘스트리스트에 결과 보여주기 위한 함수들 ---------------------
    //칼로리소모 미션 확인 함수
    public void TodayMapQuestKcalBurnUp()
    {
        int kcalNum = PlayerPrefs.GetInt("AT_TodayQuest4");    //칼로리 소모

        if (kcalNum.Equals(0))
        {
            //questTitleText[0].text = "칼로리 소모: 300kcal이상";
            _TodayMapQuestKcalBurnUp(300);
        }
        else if (kcalNum.Equals(1))
        {
            _TodayMapQuestKcalBurnUp(400);
        }
        else if (kcalNum.Equals(2))
        {
            _TodayMapQuestKcalBurnUp(500);
        }
        else if (kcalNum.Equals(3))
        {
            _TodayMapQuestKcalBurnUp(600);
        }
    }

    //칼로리 퀘스트 결과
    void _TodayMapQuestKcalBurnUp(int _kcal)
    {
        string before = PlayerPrefs.GetString("AT_BeforeQuestKcal");   //아시아맵에서 시작하자마자 상태
        string after = PlayerPrefs.GetString("AT_AfterQuestKcal"); //아시아맵에서 끝났을때 상태


        if (PlayerPrefs.GetString("AT_FinishState") == "Clear")
        {
            if (before.Equals("No"))
            {
                questTitleText[0].text = "칼로리 소모: " + _kcal + "kcal이상";

                if (after.Equals("No"))
                {
                    questObj[0].GetComponent<Image>().sprite = failSprite;  //실패 이미지
                }
                else
                {
                    questRewardPanel.SetActive(true);   //퀘스트보상알림 판넬 활성화
                }
            }
            else if (before.Equals("Yes"))
            {
                questObj[0].SetActive(false);   //이전에 끝낸 퀘스트라서 비활성화
            }
        }
        else if(PlayerPrefs.GetString("AT_FinishState").Equals("Fail"))
        {
            if (before.Equals("No"))
            {
                questTitleText_fail[0].text = "칼로리 소모: " + _kcal + "kcal이상";

                if (after.Equals("No"))
                {
                    questObj_fail[0].GetComponent<Image>().sprite = failSprite;  //실패 이미지
                }
                else
                {
                    questRewardPanel.SetActive(true);   //퀘스트보상알림 판넬 활성화
                }
            }
            else if (before.Equals("Yes"))
            {
                questObj_fail[0].SetActive(false);   //이전에 끝낸 퀘스트라서 비활성화
            }
        }
    }


    //최대속도 미션 확인 함수
    public void TodayMapQuestMaxSpeed()
    {
        int maxSpeedNum = PlayerPrefs.GetInt("AT_TodayQuest5");    //최고속도
        float todayMaxSpeed = PlayerPrefs.GetFloat("AT_TodayMaxSpeed");    //오늘 최고속도
        //Debug.Log("최대속도 미션 : " + maxSpeedNum);

        if (maxSpeedNum.Equals(0))
        {
            //questTitleText[1].text = "오늘의 속도: 25km이상";
            _TodayMapQuestMaxSpeed(25);
        }
        else if (maxSpeedNum.Equals(1))
        {
            _TodayMapQuestMaxSpeed(30);
        }
        else if (maxSpeedNum.Equals(2))
        {
            _TodayMapQuestMaxSpeed(35);
        }
        else if (maxSpeedNum.Equals(3))
        {
            _TodayMapQuestMaxSpeed(40);
        }
        else if (maxSpeedNum.Equals(4))
        {
            _TodayMapQuestMaxSpeed(45);
        }
        else if (maxSpeedNum.Equals(5))
        {
            _TodayMapQuestMaxSpeed(50);
        }
        else if (maxSpeedNum.Equals(6))
        {
            _TodayMapQuestMaxSpeed(55);
        }
    }

    //오늘의 최고 스피드 결과
    void _TodayMapQuestMaxSpeed(int _speed)
    {
        string before = PlayerPrefs.GetString("AT_BeforeQuestMaxSpeed");   //아시아맵에서 시작하자마자 상태
        string after = PlayerPrefs.GetString("AT_AfterQuestMaxSpeed"); //아시아맵에서 끝낫을때 상태 

        if (PlayerPrefs.GetString("AT_FinishState") == "Clear")
        {
            if (before.Equals("No"))
            {
                questTitleText[1].text = "오늘의 속도: " + _speed + "km이상";

                if (after.Equals("No"))
                {
                    questObj[1].GetComponent<Image>().sprite = failSprite;  //실패 이미지
                }
                else
                {
                    questRewardPanel.SetActive(true);   //퀘스트보상알림 판넬 활성화
                }
            }
            else if (before.Equals("Yes"))
            {
                questObj[1].SetActive(false);   //이전에 끝낸 퀘스트라서 비활성화
            }
        }
        else if(PlayerPrefs.GetString("AT_FinishState") == "Fail")
        {
            if (before.Equals("No"))
            {
                questTitleText_fail[1].text = "오늘의 속도: " + _speed + "km이상";

                if (after.Equals("No"))
                {
                    questObj_fail[1].GetComponent<Image>().sprite = failSprite;  //실패 이미지
                }
                else
                {
                    questRewardPanel.SetActive(true);   //퀘스트보상알림 판넬 활성화
                }
            }
            else if (before.Equals("Yes"))
            {
                questObj_fail[1].SetActive(false);   //이전에 끝낸 퀘스트라서 비활성화
            }
        }
    }

    //코스별 시간제한 퀘스트 미션
    public void CourseTimeLimitQuest()
    {
        if (PlayerPrefs.GetString("AT_FinishState") == "Clear")
        {
            _CourseTimeLimitQuest(questTitleText[2], questObj[2]);
        }
        else if(PlayerPrefs.GetString("AT_FinishState") == "Fail")
        {
            _CourseTimeLimitQuest(questTitleText_fail[2], questObj_fail[2]);
        }
    }

    void _CourseTimeLimitQuest(Text _questTitleText, GameObject _questObj)
    {
        string courseName = PlayerPrefs.GetString("AT_MapCourseName");
        string timeLimitCourseNumber = "";
        //Debug.Log("timeLimitCourseNumber " + PlayerPrefs.GetInt(timeLimitCourseNumber));

        if (courseName.Equals("Normal-1"))
        {
            timeLimitCourseNumber = "AT_AsiaNormalTimeLimitFinish1";
            CourseOpenTimeState(PlayerPrefs.GetString("AT_Asia Normal 1Course"));
        }
        else if (courseName.Equals("Normal-2"))
        {
            CourseOpenTimeState(PlayerPrefs.GetString("AT_Asia Normal 2Course"));
            timeLimitCourseNumber = "AT_AsiaNormalTimeLimitFinish2";
        } 
        else if (courseName.Equals("Normal-3"))
        {
            CourseOpenTimeState(PlayerPrefs.GetString("AT_Asia Normal 3Course"));
            timeLimitCourseNumber = "AT_AsiaNormalTimeLimitFinish3";
        }
        else if (courseName.Equals("Hard-1"))
        {
            CourseOpenTimeState(PlayerPrefs.GetString("AT_Asia Hard 1Course"));
            timeLimitCourseNumber = "AT_AsiaHardTimeLimitFinish1";
        }
        else if (courseName.Equals("Hard-2"))
        {
            CourseOpenTimeState(PlayerPrefs.GetString("AT_Asia Hard 2Course"));
            timeLimitCourseNumber = "AT_AsiaHardTimeLimitFinish2";
        }
        else if (courseName.Equals("Hard-3"))
        {
            CourseOpenTimeState(PlayerPrefs.GetString("AT_Asia Hard 3Course"));
            timeLimitCourseNumber = "AT_AsiaHardTimeLimitFinish3";
        }

        //Debug.LogError("========== " + PlayerPrefs.GetInt(timeLimitCourseNumber));
        if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(0))
        {
            //Debug.Log("여기 들어옴" + courseTime);
            _questTitleText.text = "도전:5분 내로 완주!";

            if(courseTime <= 300)
            {
                _questObj.SetActive(true);  //클리어 활성화
                questRewardPanel.SetActive(true);   //퀘스트보상알림 판넬 활성화
            }
            else if(courseTime > 300)
                _questObj.GetComponent<Image>().sprite = failSprite;  //실패 이미지
        }
        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(1))
        {
            //Debug.Log("2여기 들어옴" + courseTime); 
            _questTitleText.text = "도전:4분40초 내로 완주!";
            if (courseTime <= 280)
            {
                _questObj.SetActive(true);  //클리어 활성화
                questRewardPanel.SetActive(true);   //퀘스트보상알림 판넬 활성화
            }
            else if (courseTime > 280)
                _questObj.GetComponent<Image>().sprite = failSprite;  //실패 이미지
        }
        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(2))
        {
            _questTitleText.text = "도전:4분20초 내로 완주!";
            //Debug.Log("????????? " + courseTime);
            if (courseTime <= 260)
            {
                _questObj.SetActive(true);  //클리어 활성화
                questRewardPanel.SetActive(true);   //퀘스트보상알림 판넬 활성화
            }
            else if (courseTime > 260)
                _questObj.GetComponent<Image>().sprite = failSprite;  //실패 이미지
        }
        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(3))
        {
            _questTitleText.text = "4분 내로 완주!";
            if (courseTime <= 240)
            {
                _questObj.SetActive(true);  //클리어 활성화
                questRewardPanel.SetActive(true);   //퀘스트보상알림 판넬 활성화
            }
            else if (courseTime > 240)
                _questObj.GetComponent<Image>().sprite = failSprite;  //실패 이미지
        }
        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(4))
        {
            _questTitleText.text = "도전:3분40초 내로 완주!";
            if (courseTime <= 220)
            {
                _questObj.SetActive(true);  //클리어 활성화
                questRewardPanel.SetActive(true);   //퀘스트보상알림 판넬 활성화
            }
            else if (courseTime > 220)
                _questObj.GetComponent<Image>().sprite = failSprite;  //실패 이미지
        }
        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(5))
        {
            _questTitleText.text = "도전:3분20초 내로 완주!";
            if (courseTime <= 200)
            {
                _questObj.SetActive(true);  //클리어 활성화
                questRewardPanel.SetActive(true);   //퀘스트보상알림 판넬 활성화
            }
            else if (courseTime > 200)
                _questObj.GetComponent<Image>().sprite = failSprite;  //실패 이미지
        }
        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(6))
        {
            _questTitleText.text = "도전:3분 내로 완주!";
            if (courseTime <= 180)
            {
                _questObj.SetActive(true);  //클리어 활성화
                questRewardPanel.SetActive(true);   //퀘스트보상알림 판넬 활성화
            }
            else if (courseTime > 180)
                _questObj.GetComponent<Image>().sprite = failSprite;  //실패 이미지
        }
        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(7))
        {
            _questTitleText.text = "도전:2분40초 내로 완주!";
            if (courseTime <= 160)
            {
                _questObj.SetActive(true);  //클리어 활성화
                questRewardPanel.SetActive(true);   //퀘스트보상알림 판넬 활성화
            }
            else if (courseTime > 160)
                _questObj.GetComponent<Image>().sprite = failSprite;  //실패 이미지
        }
        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(8))
        {
            _questTitleText.text = "도전:2분20초 내로 완주!";
            if (courseTime <= 140)
            {
                _questObj.SetActive(true);  //클리어 활성화
                questRewardPanel.SetActive(true);   //퀘스트보상알림 판넬 활성화
            }
            else if (courseTime > 140)
                _questObj.GetComponent<Image>().sprite = failSprite;  //실패 이미지
        }
        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(9))
        {
            _questTitleText.text = "도전:2분 내로 완주!";
            if (courseTime <= 120)
            {
                _questObj.SetActive(true);  //클리어 활성화
                questRewardPanel.SetActive(true);   //퀘스트보상알림 판넬 활성화
            }
            else if (courseTime > 120)
                _questObj.GetComponent<Image>().sprite = failSprite;  //실패 이미지
        }
        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(10))
        {
            _questTitleText.text = "도전:1분40초 내로 완주!";
            if (courseTime <= 100)
            {
                _questObj.SetActive(true);  //클리어 활성화
                questRewardPanel.SetActive(true);   //퀘스트보상알림 판넬 활성화
            }  
            else if (courseTime > 100)
                _questObj.GetComponent<Image>().sprite = failSprite;  //실패 이미지
        }
        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(11))
        {
            _questTitleText.text = "도전:1분20초 내로 완주!";
            if (courseTime <= 80)
            {
                _questObj.SetActive(true);  //클리어 활성화
                questRewardPanel.SetActive(true);   //퀘스트보상알림 판넬 활성화
            }
            else if (courseTime > 80)
                _questObj.GetComponent<Image>().sprite = failSprite;  //실패 이미지
        }
        else if (PlayerPrefs.GetInt(timeLimitCourseNumber).Equals(12))
        {
            _questTitleText.text = "도전:1분20초 내로 완주!";
            _questObj.SetActive(true);  //클리어 활성화
        }
    }


    //======================================================================



    ///////-------------퀘스트 확인 함수 --------------------
    //맵퀘스트 각 코스 완주 횟수 저장 함수
    public void MapQuestCourseFinishAmount()
    {
        int courseAmount = 0;
        if(PlayerPrefs.GetString("AT_MapCourseName") == "Normal-1")
        {
            courseAmount = PlayerPrefs.GetInt("AT_AsiaNormal1FinishAmount") + 1;
            PlayerPrefs.SetInt("AT_AsiaNormal1FinishAmount", courseAmount);

            ServerManager.Instance.Update_MapQuest("AsiaNormal1Finish", -1, courseAmount);

            //Debug.Log("AsiaNormal1FinishAmount " + courseAmount);
        }
        else if (PlayerPrefs.GetString("AT_MapCourseName") == "Normal-2")
        {
            courseAmount = PlayerPrefs.GetInt("AT_AsiaNormal2FinishAmount") + 1;
            PlayerPrefs.SetInt("AT_AsiaNormal2FinishAmount", courseAmount);

            ServerManager.Instance.Update_MapQuest("AsiaNormal2Finish", -1, courseAmount);
        }
        else if (PlayerPrefs.GetString("AT_MapCourseName") == "Normal-3")
        {
            courseAmount = PlayerPrefs.GetInt("AT_AsiaNormal3FinishAmount") + 1;
            PlayerPrefs.SetInt("AT_AsiaNormal3FinishAmount", courseAmount);

            ServerManager.Instance.Update_MapQuest("AsiaNormal3Finish", -1, courseAmount);
        }
        else if (PlayerPrefs.GetString("AT_MapCourseName") == "Hard-1")
        {
            courseAmount = PlayerPrefs.GetInt("AT_AsiaHard1FinishAmount") + 1;
            PlayerPrefs.SetInt("AT_AsiaHard1FinishAmount", courseAmount);

            ServerManager.Instance.Update_MapQuest("AsiaHard1Finish", -1, courseAmount);
        }
        else if (PlayerPrefs.GetString("AT_MapCourseName") == "Hard-2")
        {
            courseAmount = PlayerPrefs.GetInt("AT_AsiaHard2FinishAmount") + 1;
            PlayerPrefs.SetInt("AT_AsiaHard2FinishAmount", courseAmount);

            ServerManager.Instance.Update_MapQuest("AsiaHard2Finish", -1, courseAmount);
        }
        else if (PlayerPrefs.GetString("AT_MapCourseName") == "Hard-3")
        {
            courseAmount = PlayerPrefs.GetInt("AT_AsiaHard3FinishAmount") + 1;
            PlayerPrefs.SetInt("AT_AsiaHard3FinishAmount", courseAmount);

            ServerManager.Instance.Update_MapQuest("AsiaHard3Finish", -1, courseAmount);
        }
    }


    



    //=================================================================

    public void AgainPlayButtonOn()
    {
        SceneManager.LoadScene("Loading");
    }

    public void EndPlayButtonOn()
    {
        StartCoroutine(_EndPlayButtonOn(PlayerPrefs.GetString("AT_Player_ID")));
    }

    IEnumerator _EndPlayButtonOn(string _userID)
    {
        ServerManager.Instance.GetCharacterInfo(_userID);
        yield return new WaitUntil(() => ServerManager.Instance.isCharacterDataStackCompleted);
        ServerManager.Instance.isCharacterDataStackCompleted = false;

        SceneManager.LoadScene("MapChoice");
    }

    //게임 종료
    public void GameEndButtonOn()
    {
        Application.Quit(); //게임 종료
    }


}
