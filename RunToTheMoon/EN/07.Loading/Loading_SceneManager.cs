using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading_SceneManager : MonoBehaviour
{

    public static string nextScene;
    public Game_DataManager moonDistance;
    public Image loading_img;
    public Sprite[] loading_sprites;
    int ImgeCount = 0;
    [SerializeField]
    Slider sliderBar;
    public Text LoadingText_Percent;
    private float percentComplete;
    public float downloadProgressInput;
    public float downloadSliderProgressInput;
    public bool LoadCompleteB = false;
    float timer = 0.0f;

    private void Start()
    {
        AddressablesManager.Instance.downloadProgressScript = this.gameObject.GetComponent<Loading_SceneManager>();
        // SoundManager.Instance.StopBGM();
        percentComplete = 0;
        sliderBar.value = 0;
      
        StartCoroutine(_LoadScene());        
    }

    private void OnEnable()
    {
        percentComplete = 0;
    }

    private void Update()
    {   
        //if (percentComplete != downloadProgressInput)
        //{
        //    Debug.LogError("여기들어오긴하니??????????????" + downloadProgressInput);
        //   LoadingText_Percent.text = "Loading ... " + downloadProgressInput.ToString("N0") + "%";

        //    percentComplete = downloadProgressInput;

        //    timer += Time.deltaTime * 0.009f;

        //    if (downloadSliderProgressInput < 0.9f)
        //    {
        //        sliderBar.value = Mathf.Lerp(sliderBar.value, downloadSliderProgressInput, timer);
        //        if (sliderBar.value >= downloadSliderProgressInput)
        //        {
        //            timer = 0f;
        //        }
        //    }
        //    else
        //    {
        //        sliderBar.value = Mathf.Lerp(sliderBar.value, 1f, timer);
        //        if (sliderBar.value >= 0.99999f)
        //        {
        //            //op.allowSceneActivation = true;
        //            sliderBar.value = 1f;
        //            //yield break;
        //        }
        //    }

        //    if (downloadProgressInput == 100)
        //    {
        //        sliderBar.value = 1f;
        //    }
        //}
    }
    public void ImgSprite_Change()
    {
        StartCoroutine(_ImgSprite_Change());
    }
    IEnumerator _ImgSprite_Change()
    {
       
        while (true)
        {
            sliderBar.value += Time.deltaTime * 0.5f;
            LoadingText_Percent.text = "Loading ... " + (sliderBar.value * 100).ToString("N0") + "%";
            float c = 0.0625f;
            
            //Debug.Log("sliderBar.value : " + sliderBar.value + " c  "+ c);
            if (ImgeCount < 16)
            {
                if (sliderBar.value <= c * 1 && ImgeCount.Equals(0))
                {
                    loading_img.sprite = loading_sprites[ImgeCount];
                    ImgeCount += 1;
                    Debug.Log("ImgeCount  1  " + ImgeCount);
                }
                else if (sliderBar.value > c * (ImgeCount - 1) && sliderBar.value <= c * (ImgeCount))
                {
                    loading_img.sprite = loading_sprites[ImgeCount];
                    ImgeCount += 1;
                    Debug.Log("ImgeCount 2   " + ImgeCount);
                }
            }

            if (sliderBar.value == 1f)
            {                
                ImgeCount = 15;
                loading_img.sprite = loading_sprites[ImgeCount++];
                LoadCompleteB = true;
                Debug.Log("ImgeCount : " + ImgeCount);
            }

            yield return null;
        }
    }

    public void LoadScene()
    {
        Debug.Log("nextScene" + nextScene);
    }
    IEnumerator _LoadScene()
    {
        int way = 1000 - (int)moonDistance.moonDis;
        int wayPoint = (way / 50) + 1; //맵차트 1,2,- 20
        if (way.Equals(1000)) nextScene = "HallofFame";
        else
        {
            if (wayPoint.Equals(1)) nextScene = "Game 1"; //1 0-50
            else if (wayPoint.Equals(2)) nextScene = "Game 2";//2 51-100
            else if (wayPoint.Equals(3)) nextScene = "Game 3";//3
            else if (wayPoint.Equals(4)) nextScene = "Game 4";//4
            else if (wayPoint.Equals(5)) nextScene = "Game 5"; //5
            else if (wayPoint.Equals(6)) nextScene = "Game 6"; //6
            else if (wayPoint.Equals(7)) nextScene = "Game 7"; //7
            else if (wayPoint.Equals(8)) nextScene = "Game 8"; //8
            else if (wayPoint.Equals(9)) nextScene = "Game 9"; //9
            else if (wayPoint.Equals(10)) nextScene = "Game 10";   //10
            else if (wayPoint.Equals(11)) nextScene = "Game 11";   //11
            else if (wayPoint.Equals(12)) nextScene = "Game 12"; //12
            else if (wayPoint.Equals(13)) nextScene = "Game 13";    //13
            else if (wayPoint.Equals(14)) nextScene = "Game 14";  //14
            else if (wayPoint.Equals(15)) nextScene = "Game 15";   //15
            else if (wayPoint.Equals(16)) nextScene = "Game 16";  //16
            else if (wayPoint.Equals(17)) nextScene = "Game 17"; //17
            else if (wayPoint.Equals(18)) nextScene = "Game 18";   //18
            else if (wayPoint.Equals(19)) nextScene = "Game 19";   //19
            else if (wayPoint.Equals(20)) nextScene = "Game 20"; //20
        }
     
        yield return null;

        // 거리 계산된 키값 설정
        string addressableKey;
        addressableKey = "Assets/00.Scenes/" + nextScene + ".unity";
        StartCoroutine(_ImgSprite_Change());
        yield return new WaitUntil(() => LoadCompleteB == true);
        // 키값을 넘김
        AddressablesManager.Instance.LoadAddressableLevel(addressableKey);

        //AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        //Debug.Log("nextScene" + nextScene);
        //LoadingText_Persent.text = "Loading ... 0%";

        ////LoadSceneAsync ("게임씬이름"); 입니다.
        //op.allowSceneActivation = false;
        ////allowSceneActivation 이 true가 되는 순간이 바로 다음 씬으로 넘어가는 시점입니다.
        //float timer = 0.0f;
        //yield return new WaitForSeconds(3f);
        //while (!op.isDone)
        //{
        //    timer += Time.deltaTime * 0.009f;
        //    //  Debug.Log("sliderBar.value / 8 * ImgeCount " + sliderBar.value / 8 * ImgeCount);
        //    LoadingText_Persent.text = "Loading ... " + (sliderBar.value * 100).ToString("N0") + "%";
        //    if (op.progress < 0.9f)
        //    {

        //        sliderBar.value = Mathf.Lerp(sliderBar.value, op.progress, timer);
        //        if (sliderBar.value >= op.progress)
        //        {
        //            timer = 0f;
        //        }
        //    }
        //    else
        //    {
        //        sliderBar.value = Mathf.Lerp(sliderBar.value, 1f, timer);
        //        if (sliderBar.value >= 0.99999f)
        //        {
        //            op.allowSceneActivation = true;
        //            yield break;
        //        }
        //    }
        //    yield return null;
        //    //   Debug.Log(timer);
        //}

    }

}
