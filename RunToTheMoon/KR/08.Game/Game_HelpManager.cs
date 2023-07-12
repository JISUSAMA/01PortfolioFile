using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_HelpManager : MonoBehaviour
{
    public GameObject GameTutorialGrup;
    public GameObject[] GameTutorial_Grup;
    public Button[] GameTutorial;
    void Start()
    {
        GameTutorial[0].onClick.AddListener(() => GameTutorial_SetOff(0));
        GameTutorial[1].onClick.AddListener(() => GameTutorial_SetOff(1));
        GameTutorial[2].onClick.AddListener(() => GameTutorial_SetOff(2));
        GameTutorial[3].onClick.AddListener(() => GameTutorial_SetOff(3));
        GameTutorial[4].onClick.AddListener(() => GameTutorial_SetOff(4));
        GameTutorial[5].onClick.AddListener(() => GameTutorial_SetOff(5));
        GameTutorial[6].onClick.AddListener(() => GameTutorial_SetOff(6));
        GameTutorial[7].onClick.AddListener(() => GameTutorial_SetOff(7));
        GameTutorial[8].onClick.AddListener(() => GameTutorial_SetOff(8));
        GameTutorial[9].onClick.AddListener(() => Finish_Tutorial()); //마지막 버튼

      //  PlayerPrefs.DeleteKey("DoGameTutorial");
        if (!PlayerPrefs.HasKey("DoGameTutorial"))
        {
            First_GameTutorial(); //처음 게임 플레이 했을 경우,튜토리얼 시작
        }
    }
    void GameTutorial_SetOff(int num)
    {
        SoundFunction.Instance.ButtonClick_Sound();
        GameTutorial_Grup[num].SetActive(false);
        GameTutorial_Grup[num + 1].SetActive(true);
    }
    public void First_GameTutorial()
    {
        RunnerPlayer1.instance.StoryEventing = true;
        SoundFunction.Instance.ItemDescription_Sound();
        GameTutorialGrup.SetActive(true);
        GameTutorial_Grup[0].SetActive(true);
        Time.timeScale = 0;
    }
    public void Finish_Tutorial()
    {
        GameTutorial_Grup[9].SetActive(false);
        RunnerPlayer1.instance.StoryEventing = false;
        PlayerPrefs.SetString("DoGameTutorial", "true");
        Time.timeScale = 1;
    }
}
