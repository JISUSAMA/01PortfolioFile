using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_HelpManager : MonoBehaviour
{
    public GameObject LobbyTutorialGrup;
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
        GameTutorial[9].onClick.AddListener(() => GameTutorial_SetOff(9));
        GameTutorial[10].onClick.AddListener(() => GameTutorial_SetOff(10));
        GameTutorial[11].onClick.AddListener(() => GameTutorial_SetOff(11));
        GameTutorial[12].onClick.AddListener(() => GameTutorial_SetOff(12));
        GameTutorial[13].onClick.AddListener(() => GameTutorial_Grup[13].SetActive(false)); //마지막 버튼
    }
    void GameTutorial_SetOff(int num)
    {
        SoundFunction.Instance.ButtonClick_Sound();
            GameTutorial_Grup[num].SetActive(false);
            GameTutorial_Grup[num + 1].SetActive(true);
    }

    public void First_LobbyTutorial()
    {
        SoundFunction.Instance.ItemDescription_Sound();
        LobbyTutorialGrup.SetActive(true);
        GameTutorial_Grup[0].SetActive(true);
    }
}
