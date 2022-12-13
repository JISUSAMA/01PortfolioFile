using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseGame_UIManager : MonoBehaviour
{

    //뇌 훈련 게임 버튼 클릭
    public void OnClick_BrainGame()
    {
        GameAppManager.instance.Initialization();
        GameAppManager.instance.GameKind = "Brain";
        while (GameAppManager.instance.QuestionStringList.Count < GameAppManager.instance.Brain_QusetionCount)
        {
            int rand_input = UnityEngine.Random.Range(0, GameAppManager.instance.Brain_QusetionCount);
            if (GameAppManager.instance.QuestionStringList.Count != 0)
            {
                if (!GameAppManager.instance.QuestionStringList.Contains(GameAppManager.instance.Brain_gameName[rand_input]))
                {
                    GameAppManager.instance.rand_GameNumber.Add(rand_input);
                    GameAppManager.instance.QuestionStringList.Add(GameAppManager.instance.Brain_gameName[rand_input]);

                }
            }
            else
            {
                GameAppManager.instance.rand_GameNumber.Add(rand_input);
                GameAppManager.instance.QuestionStringList.Add(GameAppManager.instance.Brain_gameName[rand_input]);
            }
        }
        AppSoundManager.Instance.PlayBGM("MainBGM");
        GameAppManager.instance.GameLoadScene();
    }
    //치매 예방 게임 버튼 클릭
    public void OnClick_Dementia()
    {
       
        GameAppManager.instance.Initialization();
        GameAppManager.instance.GameKind = "Dementia";
        while (GameAppManager.instance.QuestionStringList.Count < GameAppManager.instance.Dementia_QusetionCount)
        {
            int rand_input = UnityEngine.Random.Range(0, GameAppManager.instance.Dementia_QusetionCount);
            if (GameAppManager.instance.QuestionStringList.Count != 0)
            {
                if (!GameAppManager.instance.QuestionStringList.Contains(GameAppManager.instance.Dementia_gameName[rand_input]))
                {
                    GameAppManager.instance.rand_GameNumber.Add(rand_input);
                    GameAppManager.instance.QuestionStringList.Add(GameAppManager.instance.Dementia_gameName[rand_input]);
                }
            }
            else
            {
                GameAppManager.instance.rand_GameNumber.Add(rand_input);
                GameAppManager.instance.QuestionStringList.Add(GameAppManager.instance.Dementia_gameName[rand_input]);
            }
        }
        AppSoundManager.Instance.PlayBGM("MainBGM");
        GameAppManager.instance.GameLoadScene();
    }
    //치매 예방 체조 버튼 클릭
    public void GymnasticsGame()
    {
        AppSoundManager.Instance.AudioMixer_Volume("BGM", -20f);
        SceneManager.LoadScene("8.GymnasticsGame");
    }


    public void LoadSceneName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void BackButton_ForChooseGame()
    {
        //게스트가 아닌경우, 데이터 저장!
        if (PlayerPrefs.GetString("CARE_PlayMode") != "GuestMode")
        {
            SceneManager.LoadScene("5.UserExercise");
        }
        else
        {
            SceneManager.LoadScene("0.Main");
        }
    }
    public void SystemCloseBtnClickOn()
    {
        Application.Quit();
    }

    public void OnClick_RestVideo(string url) 
    {
       AppSoundManager.Instance.StopBGM();
        Application.OpenURL(url); 
    }
}
