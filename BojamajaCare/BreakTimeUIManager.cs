using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BreakTimeUIManager : MonoBehaviour
{
    public Image FillSlider;
    float coolTime = 5f;
    float updateTime = 1f;
    public Text TimerText;

    public Text TitleText;
    public Text GameTitle; //게임 타이틀
    public Image MarkImg;

    public Sprite[] MarkSprite;

    public Text ClearTimeText;

    public GameObject GameExitPopup;
    public Button YesBTN;
    public Button NoBTN;

    private void Awake()
    {
        YesBTN.onClick.AddListener(() => OnClick_ExitYes());
        NoBTN.onClick.AddListener(() => OnClick_ExitNo());
    }
    private void Start()
    {
        string state = PlayerPrefs.GetString("GameClearState");
        switch (GameAppManager.instance.GameKind)
        {
            case "Brain":
                if (state.Equals("true"))
                {
                    TitleText.text = "훈련종료";
                    GameTitle.text
                        = GameAppManager.instance.BrainName[GameAppManager.instance.rand_GameNumber[GameAppManager.instance.CurrentQusetionNumber - 1]].ToString();
                    ClearTimeText.text = GameAppManager.instance.GamePlayTime[GameAppManager.instance.CurrentQusetionNumber - 1].ToString("N0")+ "초";

                }
                else if (state.Equals("false"))
                {
                    TitleText.text = "훈련실패";
                    GameTitle.text
                          = GameAppManager.instance.BrainName[GameAppManager.instance.rand_GameNumber[GameAppManager.instance.CurrentQusetionNumber - 1]].ToString();
                    ClearTimeText.text = "다음기회에..";
                }
                break;
            case "Dementia":
                if (state.Equals("true"))
                {
                    TitleText.text = "훈련종료";
                    GameTitle.text
                        = GameAppManager.instance.DementiaName[GameAppManager.instance.rand_GameNumber[GameAppManager.instance.CurrentQusetionNumber - 1]].ToString();
                    ClearTimeText.text = GameAppManager.instance.GamePlayTime[GameAppManager.instance.CurrentQusetionNumber - 1].ToString("N0") + "초";

                }
                else if (state.Equals("false"))
                {
                    TitleText.text = "훈련실패";
                    GameTitle.text
                          = GameAppManager.instance.DementiaName[GameAppManager.instance.rand_GameNumber[GameAppManager.instance.CurrentQusetionNumber - 1]].ToString();
                    ClearTimeText.text = "다음기회에..";
                }
                break;
            case "Real":
                if (state.Equals("true"))
                {
                    TitleText.text = "훈련종료";
                    GameTitle.text
                        = GameAppManager.instance.LeapName[GameAppManager.instance.LeapMotion_PlayNumList[GameAppManager.instance.CurrentQusetionNumber - 1]].ToString();
                    ClearTimeText.text = GameAppManager.instance.GamePlayScore[GameAppManager.instance.CurrentQusetionNumber - 1].ToString() + "점";
                  //  ClearTimeText.text = " ";

                }
                else if (state.Equals("false"))
                {
                    TitleText.text = "훈련실패";
                    GameTitle.text
                          = GameAppManager.instance.LeapName[GameAppManager.instance.LeapMotion_PlayNumList[GameAppManager.instance.CurrentQusetionNumber - 1]].ToString();
                    ClearTimeText.text = "다음기회에..";
                   // ClearTimeText.text = " ";
                }
                break;

        }
        Waiting_SecTime();
    }
    public void OnClick_ExitBTN()
    {
        Time.timeScale = 0;
        GameExitPopup.SetActive(true);
    }
    public void OnClick_ExitYes()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("8.ChooseGame");
    }
    public void OnClick_ExitNo()
    {
        Time.timeScale = 1;
        GameExitPopup.SetActive(false);
    }
    public void Waiting_SecTime()
    {
        coolTime = 5f;
        StartCoroutine("_Waiting_SecTime");
    }
    IEnumerator _Waiting_SecTime()
    {
        while (coolTime > updateTime)
        {
            updateTime += Time.deltaTime;
            FillSlider.fillAmount = 1 - (Mathf.Lerp(0, 100, (updateTime / coolTime) / 100));
            TimerText.text = (coolTime - updateTime).ToString("N0");
            yield return null;
        }

        updateTime = 1f;
        FillSlider.fillAmount = 0.0f;

        yield return new WaitForSeconds(1);
        GameAppManager.instance.GameLoadScene();
    }
}
