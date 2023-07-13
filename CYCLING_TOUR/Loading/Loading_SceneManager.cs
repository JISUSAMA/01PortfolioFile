using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading_SceneManager : MonoBehaviour
{

    public static string nextScene;

    [SerializeField]
    Slider sliderBar;


    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string name)
    {
        nextScene = name;
        SceneManager.LoadScene(nextScene);
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync("AsiaMap 3"); //LoadSceneAsync ("게임씬이름"); 입니다.

        //allowSceneActivation 이 true가 되는 순간이 바로 다음 씬으로 넘어가는 시점입니다.
        op.allowSceneActivation = false;
        
        float timer = 0.0f;
        yield return new WaitForSeconds(0.3f);

        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime * 0.0009f;

            if (op.progress < 0.9f)
            {
                sliderBar.value = Mathf.Lerp(sliderBar.value, op.progress, timer);
                if (sliderBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                sliderBar.value = Mathf.Lerp(sliderBar.value, 1f, timer);
                if (sliderBar.value >= 0.99f) //0.99999
                {
                    op.allowSceneActivation = true;

                    yield break;
                }
            }
            //Debug.Log(timer);
        }
    }
}

