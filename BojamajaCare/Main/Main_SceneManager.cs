using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_SceneManager : MonoBehaviour
{
    public void OnClick_BrainGame()
    {
        Touch_GameManager.instance.QuestionStringList.Clear();
        Touch_GameManager.instance.CurrentGameScore =0;
        Touch_GameManager.instance.game_kind ="Brain";
        while (Touch_GameManager.instance.QuestionStringList.Count < Touch_GameManager.instance.Brain_QusetionCount)
        {
            int rand_input = Random.Range(0, Touch_GameManager.instance.Brain_QusetionCount);
            if (Touch_GameManager.instance.QuestionStringList.Count != 0)
            {
                if (!Touch_GameManager.instance.QuestionStringList.Contains(Touch_GameManager.instance.Brain_gameName[rand_input]))
                {
                    Touch_GameManager.instance.QuestionStringList.Add(Touch_GameManager.instance.Brain_gameName[rand_input]);
                }
            }
            else
            {
                Touch_GameManager.instance.QuestionStringList.Add(Touch_GameManager.instance.Brain_gameName[rand_input]);
            }
        }
        SceneManager.LoadScene(Touch_GameManager.instance.QuestionStringList[Touch_GameManager.instance.CurrentQusetionNumber]);
    }
    public void OnClick_Dementia()
    {
        Touch_GameManager.instance.QuestionStringList.Clear();
        Touch_GameManager.instance.CurrentGameScore = 0;
        Touch_GameManager.instance.game_kind = "Dementia";
        while (Touch_GameManager.instance.QuestionStringList.Count < Touch_GameManager.instance.Dementia_QusetionCount)
        {
            int rand_input = Random.Range(0, Touch_GameManager.instance.Dementia_QusetionCount);
            if (Touch_GameManager.instance.QuestionStringList.Count != 0)
            {
                if (!Touch_GameManager.instance.QuestionStringList.Contains(Touch_GameManager.instance.Dementia_gameName[rand_input]))
                {
                    Touch_GameManager.instance.QuestionStringList.Add(Touch_GameManager.instance.Dementia_gameName[rand_input]);
                }
            }
            else
            {
                Touch_GameManager.instance.QuestionStringList.Add(Touch_GameManager.instance.Dementia_gameName[rand_input]);
            }
        }
        SceneManager.LoadScene(Touch_GameManager.instance.QuestionStringList[Touch_GameManager.instance.CurrentQusetionNumber]);
    }
}
