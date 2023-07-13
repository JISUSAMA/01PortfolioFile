using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class MyInformation_DataManager : MonoBehaviour
{

    public static MyInformation_DataManager instance { get; private set; }

    List<Dictionary<string, object>> data;

    public GameObject gameEndPopup; //게임종료팝업

    public int coin_i;
    public bool isNickNameCheckProcessEnd = false;
    bool nullState, specialState;  // 널값, 특수문자 사용
    bool curseState;    //욕사용 여부

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        coin_i = PlayerPrefs.GetInt("AT_Player_Gold");   //코인
    }

    //숫자 콤마 찍는 함수
    public string CommaText(int _data)
    {
        if (_data != 0)
            return string.Format("{0:#,###}", _data);
        else
            return "0";
    }


    //코인
    public bool SetCoinSub(int _coinNum)
    {
        Debug.Log(" _coinNum  " + _coinNum + " coin_i " + coin_i);
        if (_coinNum <= coin_i)
        {
            //coin_i -= _coinNum;
            //PlayerPrefs.SetInt("Player_Gold", coin_i);
            return true;
        }
        else
        {
            //Debug.Log("코인 모자름");
            //PlayerPrefs.SetInt("Player_Gold", coin_i);
            return false;
        }
    }

    //닉네임 중복 확인
    public void NickName_OverLap_Check(string _nickname, Text _overlapText)
    {
        StartCoroutine(_NickName_OverLap_Check(_nickname, _overlapText));
    }

    IEnumerator _NickName_OverLap_Check(string _nickname, Text _overlapText)
    {
        //Debug.Log("_nickname " + _nickname + " _overlapText " + _overlapText.text);
        string nicknameStr = _nickname;
        nicknameStr = nicknameStr.Replace(" ", "");   //공백제거
        specialState = Special_Character_Check(nicknameStr);
        curseState = HangeulCurseCheck(nicknameStr);

        //닉네임이 공백일 때
        if (nicknameStr == "")
        {
            nullState = true;
            if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.English) {
                _overlapText.text = "Please enter your nickname.";
            } else if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.Korean) {
                _overlapText.text = "닉네임을 입력해주시길 바랍니다.";
            }
        }
        else if (nicknameStr != "")
        {
            nullState = false;

            //특수문자 사용
            if(curseState.Equals(true))
            {
                if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.English) {
                    _overlapText.text = "Used profanity. Please re-enter.";
                } else if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.Korean) {
                    _overlapText.text = "비속어를 사용했습니다. 다시 입력해주세요.";
                }                    
            }
            else if (specialState == true)
            {
                //Debug.Log("특수문자를 사용하지 말아주십시오");
                if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.English) {
                    _overlapText.text = "Please do not use special characters.";
                } else if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.Korean) {
                    _overlapText.text = "특수문자를 사용하지 말아주십시오";
                }                
            }
            else if (curseState.Equals(false) && specialState == false && nullState == false)
            {
                //Debug.Log("사용은 가능");

                ServerManager.Instance.NickNameDoubleCheck(nicknameStr);

                yield return new WaitUntil(() => ServerManager.Instance.isNickNameSearchCompleted);

                ServerManager.Instance.isNickNameSearchCompleted = false;

                if (ServerManager.Instance.isExistNickName)
                {
                    // 닉네임이 존재한다면
                    //Debug.Log("닉네임이 존재합니다.");
                    //Debug.Log("특수문자를 사용하지 말아주십시오");
                    if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.English) {
                        _overlapText.text = "Duplicate nickname.";
                    } else if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.Korean) {
                        _overlapText.text = "중복된 닉네임 입니다.";
                    }                    
                }
                else
                {
                    //중복체크하고, 특수문자 사용하지 않았고, 닉네임이 중복이지 않고 널값이 아니다
                    ServerManager.Instance.NickNameChange(nicknameStr);

                    yield return new WaitUntil(() => ServerManager.Instance.isNickNameChangeCompleted);

                    ServerManager.Instance.isNickNameChangeCompleted = false;

                    //Debug.Log("닉네임 변경 성공");
                    //PlayerPrefs.SetString("Player_NickName", _nickname);
                    if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.English) {
                        _overlapText.text = "Nickname change success.";
                    } else if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleManager.LanguageType.Korean) {
                        _overlapText.text = "닉네임 성공";
                    }                    
                }
            }
        }

        isNickNameCheckProcessEnd = true;
    }

    //닉네임 특수문자 사용 여부 확인 함수
    public bool Special_Character_Check(string _nickname)
    {
        string specialStr = @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]";

        Regex regex = new Regex(specialStr);

        //Debug.Log(regex.IsMatch(_nickname));
        //True가 나오면 특수문자를 사용한다
        specialState = regex.IsMatch(_nickname);
        return regex.IsMatch(_nickname);
    }

    //한글욕검사
    bool HangeulCurseCheck(string _nickname)
    {
        data = CSVReader.Read("Swearlist");
        bool isCheck = false;
        
        //for (int i = 0; i < data.Count; i++)
        //{
        //    if (data[i]["욕"].ToString().Equals(_nickname))
        //    {
        //        isCheck = true;
        //    }
        //}
        for (int i = 0; i < data.Count; i++)
        {
            //isCheck = data[i]["욕"].ToString().Contains(_nickname);
            isCheck = _nickname.Contains(data[i]["욕"].ToString());
            if (isCheck.Equals(true))
                return isCheck;
            Debug.Log("욕 ? " + isCheck);
        }

        return isCheck;
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
}
