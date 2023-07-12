using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading_SceneManager : MonoBehaviour
{
    private BusanMapStringClass stringClass;
    public static string nextScene;
   // private string BUSAN_MAP_COURSE_NOR1 = "Normal-1";
   // private string BUSAN_MAP_COURSE_NOR2 = "Normal-2";
   // private string BUSAN_MAP_COURSE_HARD1 = "Hard-1";
   // private string BUSAN_MAP_COURSE_HARD2 = "Hard-2";
   //
   // private string BUSAN_RED_LINE = "RED_LINE";
   // private string BUSAN_GREEN_LINE = "GREEN_LINE";
    [SerializeField]
    Slider sliderBar;

    AsyncOperation op;


    private void Start()
    {
        stringClass = new BusanMapStringClass(); 
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
        if (PlayerPrefs.GetString("Busan_CurrentMap_Name").Equals(stringClass.BUSAN_RED_LINE))
        {
            if (PlayerPrefs.GetString("Busan_MapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_NOR1) 
                || PlayerPrefs.GetString("Busan_MapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_HARD1))
                op = SceneManager.LoadSceneAsync("BusanMapMorning"); //LoadSceneAsync ("게임씬이름"); 입니다.
            else if (PlayerPrefs.GetString("Busan_MapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_NOR2) 
                || PlayerPrefs.GetString("Busan_MapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_HARD2))
                op = SceneManager.LoadSceneAsync("BusanMapNight"); //LoadSceneAsync ("게임씬이름"); 입니다.

            //SceneManager.GetActiveScene().name.Equals("BusanMapNight")

        }
        if (PlayerPrefs.GetString("Busan_CurrentMap_Name").Equals(stringClass.BUSAN_GREEN_LINE))
        {
            if (PlayerPrefs.GetString("Busan_GreenMapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_NOR1) 
                || PlayerPrefs.GetString("Busan_GreenMapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_HARD1))
                op = SceneManager.LoadSceneAsync("BusanGreenLineMorning"); //LoadSceneAsync ("게임씬이름"); 입니다.
            else if (PlayerPrefs.GetString("Busan_GreenMapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_NOR2) 
                || PlayerPrefs.GetString("Busan_GreenMapCourseName").Equals(stringClass.BUSAN_MAP_COURSE_HARD2))
                op = SceneManager.LoadSceneAsync("BusanGreenLineMorning"); //LoadSceneAsync ("게임씬이름"); 입니다.

            //SceneManager.GetActiveScene().name.Equals("BusanMapNight")

        }



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

