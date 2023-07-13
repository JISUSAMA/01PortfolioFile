using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class RankingJon_UIManager : MonoBehaviour
{
    public GameObject gameEndPopup; //게임종료팝업

    public GameObject[] rankingPanel;   //랭킹판넬
    public Text panelNameText;  //코스 이름
    public Dropdown mapNameDrop;
    public Dropdown modeDrop;
    public Dropdown corseDrop;

    public Text expText;
    public Text coinText;


    string[] mapNameStr;    //판넬 이름들
    float currTime = 3f;    //판넬 넘어가는 시간
    int count;  //판넬 넘어가는 횟수
    int exp, coin;

    GameObject itemDataBase;
    GameObject sensorManager;   //센서
    Transform woman_player;   //여자모델링
    Transform man_player;   //남자

    void Start()
    {
        Initialization();
        StartCoroutine(RankingPanelMoving());
    }

    void Initialization()
    {
        exp = PlayerPrefs.GetInt("AT_Player_CurrExp");
        coin = PlayerPrefs.GetInt("AT_Player_Gold");
        expText.text = exp.ToString();

        if (coin != 0)
            coinText.text = string.Format("{0:#,###}", coin);
        else if (coin.Equals(0))
            coinText.text = "0";

        //내 정보 - 모델링 위치 초기화
        if (PlayerPrefs.GetString("AT_Player_Sex") == "Woman")
        {
            woman_player = GameObject.Find("Woman").GetComponent<Transform>();
            //woman_player.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetString("AT_Player_Sex") == "Man")
        {
            man_player = GameObject.Find("Man").GetComponent<Transform>();
            //man_player.gameObject.SetActive(false);
        }


        itemDataBase = GameObject.Find("DataManager");
        sensorManager = GameObject.Find("SensorManager");
        //sensorManager.SetActive(false);

        //판넬별 이름 초기화
        mapNameStr = new string[6];
        mapNameStr[0] = "Asia Normal 제 1코스";
        mapNameStr[1] = "Asia Normal 제 2코스";
        mapNameStr[2] = "Asia Normal 제 3코스";
        mapNameStr[3] = "Asia Hard 제 1코스";
        mapNameStr[4] = "Asia Hard 제 2코스";
        mapNameStr[5] = "Asia Hard 제 3코스";

        // 분류 순서 Map > Mode > Corse
        // DB에서 불러올 때 China > Busan > ..
        // China - Corse 123.. > Mode Normal-Hard.. > Map변경..
        // 서버에서 한번에 다 들고오기? 바뀔때 마다 들고오기? ..
        //ServerManager.Instance.Get_China_RankingZoneData(10, "Normal", "제1코스");

        count = 0;  //판넬 넘기는 횟수 초기화
    }

    //판넬 넘기는 타이머 3초마다
    IEnumerator RankingPanelMoving()
    {
        count++;
        //Debug.Log(count);

        // 서버 데이터 불러올 때 마다 딜레이 주기 :)
        //3초가 지나갈때마다 해당 판넬 이름이 변경, 판넬 변경
        if (count % 6 == 1)
        {
            ServerManager.Instance.Get_Asia_RankingZoneData(10, "Normal", "제1코스");

            yield return new WaitUntil(() => ServerManager.Instance.isRankingSearchCompleted);

            ServerManager.Instance.isRankingSearchCompleted = false;

            RankingDisplay("Normal", 1);   // 1코스

            panelNameText.text = mapNameStr[0];
            RankingCorsePanel_ActionShow(true, false, false, false, false, false);
        }
        else if (count % 6 == 2)
        {
            ServerManager.Instance.Get_Asia_RankingZoneData(10, "Normal", "제2코스");

            yield return new WaitUntil(() => ServerManager.Instance.isRankingSearchCompleted);

            ServerManager.Instance.isRankingSearchCompleted = false;

            RankingDisplay("Normal", 2);   // 2코스

            panelNameText.text = mapNameStr[1];
            RankingCorsePanel_ActionShow(false, true, false, false, false, false);
        }
        else if (count % 6 == 3)
        {
            ServerManager.Instance.Get_Asia_RankingZoneData(10, "Normal", "제3코스");

            yield return new WaitUntil(() => ServerManager.Instance.isRankingSearchCompleted);

            ServerManager.Instance.isRankingSearchCompleted = false;

            RankingDisplay("Normal", 3);   // 3코스

            panelNameText.text = mapNameStr[2];
            RankingCorsePanel_ActionShow(false, false, true, false, false, false);
        }
        else if (count % 6 == 4)
        {
            ServerManager.Instance.Get_Asia_RankingZoneData(10, "Hard", "제1코스");

            yield return new WaitUntil(() => ServerManager.Instance.isRankingSearchCompleted);

            ServerManager.Instance.isRankingSearchCompleted = false;

            RankingDisplay("Hard", 1);

            panelNameText.text = mapNameStr[3];
            RankingCorsePanel_ActionShow(false, false, false, true, false, false);
        }
        else if (count % 6 == 5)
        {
            ServerManager.Instance.Get_Asia_RankingZoneData(10, "Hard", "제2코스");

            yield return new WaitUntil(() => ServerManager.Instance.isRankingSearchCompleted);

            ServerManager.Instance.isRankingSearchCompleted = false;

            RankingDisplay("Hard", 2);

            panelNameText.text = mapNameStr[4];
            RankingCorsePanel_ActionShow(false, false, false, false, true, false);
        }
        else if (count % 6 == 0)
        {
            ServerManager.Instance.Get_Asia_RankingZoneData(10, "Hard", "제3코스");

            yield return new WaitUntil(() => ServerManager.Instance.isRankingSearchCompleted);

            ServerManager.Instance.isRankingSearchCompleted = false;

            RankingDisplay("Hard", 3);

            panelNameText.text = mapNameStr[5];
            RankingCorsePanel_ActionShow(false, false, false, false, false, true);
        }


        //3초 카운트
        while (currTime > 0f)
        {
            currTime -= Time.deltaTime;
            //Debug.Log(currTime);
            yield return new WaitForEndOfFrame();
        }

        if (currTime <= 0)
            currTime = 3f;

        //반복되어야하기에 코루틴 안에 코루틴
        StartCoroutine(RankingPanelMoving());
    }

    private void RankingDisplay(string _mode, int _corse)
    {
        if (_mode.Equals("Normal"))
        {
            if (_corse.Equals(1))
            {
                for (int j = 0; j < ServerManager.Instance.Ranking.Count; j++)     // 노말 1코스 콘텐츠 10개 / ChinaNomalCorsePanel1 안에 자식
                {
                    for (int i = 0; i < rankingPanel[_corse - 1].transform.GetChild(j).childCount; i++)   // 노말 1코스 Content1~10 안에 7개
                    {
                        if (rankingPanel[_corse - 1].transform.GetChild(j).transform.GetChild(i).name.Equals("ModeImage"))
                        {
                            // 아무고토 안함
                        }
                        else if (rankingPanel[_corse - 1].transform.GetChild(j).transform.GetChild(i).name.Equals("Rank"))
                        {
                            rankingPanel[_corse - 1].transform.GetChild(j).
                                transform.GetChild(i).
                                transform.GetChild(0).
                                transform.GetChild(0).GetComponent<Text>().text = ServerManager.Instance.Ranking[j].ranking.ToString();
                        }
                        else
                        {

                            rankingPanel[_corse - 1].
                               transform.GetChild(j).
                                    transform.GetChild(i).GetComponent<Text>().text = Input_RankingValue(j, rankingPanel[_corse - 1].transform.GetChild(j).transform.GetChild(i).name);
                        }
                    }
                }
            }
            else if (_corse.Equals(2))
            {
                for (int j = 0; j < ServerManager.Instance.Ranking.Count; j++)     // 노말 1코스 콘텐츠 10개 / ChinaNomalCorsePanel1 안에 자식
                {
                    for (int i = 0; i < rankingPanel[_corse - 1].transform.GetChild(j).childCount; i++)   // 노말 1코스 Content1~10 안에 7개
                    {
                        if (rankingPanel[_corse - 1].transform.GetChild(j).transform.GetChild(i).name.Equals("ModeImage"))
                        {
                            // 아무고토 안함
                        }
                        else if (rankingPanel[_corse - 1].transform.GetChild(j).transform.GetChild(i).name.Equals("Rank"))
                        {
                            rankingPanel[_corse - 1].transform.GetChild(j).
                                transform.GetChild(i).
                                transform.GetChild(0).
                                transform.GetChild(0).GetComponent<Text>().text = ServerManager.Instance.Ranking[j].ranking.ToString();
                        }
                        else
                        {

                            rankingPanel[_corse - 1].
                               transform.GetChild(j).
                                    transform.GetChild(i).GetComponent<Text>().text = Input_RankingValue(j, rankingPanel[_corse - 1].transform.GetChild(j).transform.GetChild(i).name);

                        }
                    }
                }
            }
            else if (_corse.Equals(3))
            {
                for (int j = 0; j < ServerManager.Instance.Ranking.Count; j++)     // 노말 1코스 콘텐츠 10개 / ChinaNomalCorsePanel1 안에 자식
                {
                    for (int i = 0; i < rankingPanel[_corse - 1].transform.GetChild(j).childCount; i++)   // 노말 1코스 Content1~10 안에 7개
                    {
                        if (rankingPanel[_corse - 1].transform.GetChild(j).transform.GetChild(i).name.Equals("ModeImage"))
                        {
                            // 아무고토 안함
                        }
                        else if (rankingPanel[_corse - 1].transform.GetChild(j).transform.GetChild(i).name.Equals("Rank"))
                        {
                            rankingPanel[_corse - 1].transform.GetChild(j).
                                transform.GetChild(i).
                                transform.GetChild(0).
                                transform.GetChild(0).GetComponent<Text>().text = ServerManager.Instance.Ranking[j].ranking.ToString();
                        }
                        else
                        {

                            rankingPanel[_corse - 1].
                               transform.GetChild(j).
                                    transform.GetChild(i).GetComponent<Text>().text = Input_RankingValue(j, rankingPanel[_corse - 1].transform.GetChild(j).transform.GetChild(i).name);

                        }
                    }
                }
            }
        }
        else
        {   // Hard
            if (_corse.Equals(1))
            {
                for (int j = 0; j < ServerManager.Instance.Ranking.Count; j++)     // 노말 1코스 콘텐츠 10개 / ChinaNomalCorsePanel1 안에 자식
                {
                    for (int i = 0; i < rankingPanel[_corse + 2].transform.GetChild(j).childCount; i++)   // 노말 1코스 Content1~10 안에 7개
                    {
                        if (rankingPanel[_corse + 2].transform.GetChild(j).transform.GetChild(i).name.Equals("ModeImage"))
                        {
                            // 아무고토안함
                        }
                        else if (rankingPanel[_corse + 2].transform.GetChild(j).transform.GetChild(i).name.Equals("Rank"))
                        {
                            rankingPanel[_corse + 2].transform.GetChild(j).
                                transform.GetChild(i).
                                transform.GetChild(0).
                                transform.GetChild(0).GetComponent<Text>().text = ServerManager.Instance.Ranking[j].ranking.ToString();
                        }
                        else
                        {

                            rankingPanel[_corse + 2].
                               transform.GetChild(j).
                                    transform.GetChild(i).GetComponent<Text>().text = Input_RankingValue(j, rankingPanel[_corse + 2].transform.GetChild(j).transform.GetChild(i).name);
                        }
                    }
                }
            }
            else if (_corse.Equals(2))
            {
                for (int j = 0; j < ServerManager.Instance.Ranking.Count; j++)     // 노말 1코스 콘텐츠 10개 / ChinaNomalCorsePanel1 안에 자식
                {
                    for (int i = 0; i < rankingPanel[_corse + 2].transform.GetChild(j).childCount; i++)   // 노말 1코스 Content1~10 안에 7개
                    {
                        if (rankingPanel[_corse + 2].transform.GetChild(j).transform.GetChild(i).name.Equals("ModeImage"))
                        {
                        }
                        else if (rankingPanel[_corse + 2].transform.GetChild(j).transform.GetChild(i).name.Equals("Rank"))
                        {
                            rankingPanel[_corse + 2].transform.GetChild(j).
                                transform.GetChild(i).
                                transform.GetChild(0).
                                transform.GetChild(0).GetComponent<Text>().text = ServerManager.Instance.Ranking[j].ranking.ToString();
                        }
                        else
                        {

                            rankingPanel[_corse + 2].
                               transform.GetChild(j).
                                    transform.GetChild(i).GetComponent<Text>().text = Input_RankingValue(j, rankingPanel[_corse + 2].transform.GetChild(j).transform.GetChild(i).name);
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < ServerManager.Instance.Ranking.Count; j++)     // 노말 1코스 콘텐츠 10개 / ChinaNomalCorsePanel1 안에 자식
                {
                    for (int i = 0; i < rankingPanel[_corse + 2].transform.GetChild(j).childCount; i++)   // 노말 1코스 Content1~10 안에 7개
                    {
                        if (rankingPanel[_corse + 2].transform.GetChild(j).transform.GetChild(i).name.Equals("ModeImage"))
                        {
                        }
                        else if (rankingPanel[_corse + 2].transform.GetChild(j).transform.GetChild(i).name.Equals("Rank"))
                        {
                            rankingPanel[_corse + 2].transform.GetChild(j).
                                transform.GetChild(i).
                                transform.GetChild(0).
                                transform.GetChild(0).GetComponent<Text>().text = ServerManager.Instance.Ranking[j].ranking.ToString();
                        }
                        else
                        {

                            rankingPanel[_corse + 2].
                               transform.GetChild(j).
                                    transform.GetChild(i).GetComponent<Text>().text = Input_RankingValue(j, rankingPanel[_corse + 2].transform.GetChild(j).transform.GetChild(i).name);
                        }
                    }
                }
            }
        }
    }

    string Input_RankingValue(int _rank, string _name)
    {
        switch (_name)
        {
            case "NameText":
                return ServerManager.Instance.Ranking[_rank].nickname;
                break;
            case "TimeText":
                return ServerManager.Instance.Ranking[_rank].best_time;
                break;
            case "BikeText":
                return ServerManager.Instance.Ranking[_rank].bike;
                break;
            case "MapText":
                return ServerManager.Instance.Ranking[_rank].map;
                break;
            case "CorseText":
                return "제 " + ServerManager.Instance.Ranking[_rank].corse + "코스";
                break;
            default:
                return "";
                break;
        }
    }

    //코스별 순위판 활성화-비활성화
    void RankingCorsePanel_ActionShow(bool _index1, bool _index2, bool _index3, bool _index4, bool _index5, bool _index6)
    {
        rankingPanel[0].SetActive(_index1);
        rankingPanel[1].SetActive(_index2);
        rankingPanel[2].SetActive(_index3);
        rankingPanel[3].SetActive(_index4);
        rankingPanel[4].SetActive(_index5);
        rankingPanel[5].SetActive(_index6);
    }

    //필터를 이용해 검색 이벤트 함수
    public void FilterSearchButtonOn()
    {
        StopAllCoroutines();    //모든 코루핀 정지

        if (modeDrop.options[modeDrop.value].text == "노멀")
        {
            if (corseDrop.options[corseDrop.value].text == "제 1코스")
            {
                RankingCorsePanel_ActionShow(true, false, false, false, false, false);
                panelNameText.text = mapNameStr[0]; //노말 제 1코스
            }

            else if (corseDrop.options[corseDrop.value].text == "제 2코스")
            {
                RankingCorsePanel_ActionShow(false, true, false, false, false, false);
                panelNameText.text = mapNameStr[1]; //노말 제 2코스
            }

            else if (corseDrop.options[corseDrop.value].text == "제 3코스")
            {
                RankingCorsePanel_ActionShow(false, false, true, false, false, false);
                panelNameText.text = mapNameStr[2]; //노말 제 1코스
            }

        }
        else if (modeDrop.options[modeDrop.value].text == "하드")
        {
            if (corseDrop.options[corseDrop.value].text == "제 1코스")
            {
                RankingCorsePanel_ActionShow(false, false, false, true, false, false);
                panelNameText.text = mapNameStr[3]; //하드 제 1코스
            }
            else if (corseDrop.options[corseDrop.value].text == "제 2코스")
            {
                RankingCorsePanel_ActionShow(false, false, false, false, true, false);
                panelNameText.text = mapNameStr[4]; //하드 제 2코스
            }
            else if (corseDrop.options[corseDrop.value].text == "제 3코스")
            {
                RankingCorsePanel_ActionShow(false, false, false, false, false, true);
                panelNameText.text = mapNameStr[5]; //하드 제 3코스
            }
        }
    }

    //백버튼 이벤트 - 로비 씬 이동
    public void BackButtonOn()
    {
        SoundMaixerManager.instance.OutGameBGMPlay();
        SceneManager.LoadScene("Lobby");
    }


    public void ConnectButtonOn()
    {
        GameObject sensor = GameObject.Find("SensorManager");
        ArduinoHM10Test2 sensor_Script = sensor.GetComponent<ArduinoHM10Test2>();

        sensor_Script.StartProcess();
    }

    //게임 종료
    public void GameEndButtonOn()
    {
        Application.Quit(); //게임 종료
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

    //BGM 사운드
    public void BGM_SliderSound()
    {
        SoundMaixerManager.instance.AudioControl();
    }

    //효과음 사운드
    public void Effect_SliderSound()
    {
        SoundMaixerManager.instance.SFXAudioControl();
    }
}