
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadScene : MonoBehaviour
{
    public static string nextScene;
    public Game_DataManager moonDistance;
    public Image loading_img;
    public Sprite[] loading_sprites;
    int ImgeCount = 0;
    [SerializeField]
    Slider sliderBar;
    public Text LoadingText_Persent;
    // Level Loading
    private bool clearPreviousScene = false;
    private SceneInstance previousLoadedScene;

    private void Start()
    {
        // SoundManager.Instance.StopBGM();
        sliderBar.value = 0;
        StartCoroutine(_LoadScene());
        StartCoroutine(_ImgSprite_Change());

    }
    IEnumerator _ImgSprite_Change()
    {
        while (true)
        {
            Debug.Log("ImgeCount  0  " + (1 / loading_sprites.Length));
            if (ImgeCount < 16)
            {
                if (sliderBar.value <= (0.0625) * 1 && ImgeCount.Equals(0))
                {
                    loading_img.sprite = loading_sprites[ImgeCount];
                    ImgeCount += 1;
                    Debug.Log("ImgeCount  1  " + ImgeCount);
                }
                else if (sliderBar.value > (0.0625) * (ImgeCount - 1) && sliderBar.value <= (0.0625) * (ImgeCount))
                {
                    loading_img.sprite = loading_sprites[ImgeCount];
                    ImgeCount += 1;
                    Debug.Log("ImgeCount 2   " + ImgeCount);
                }
            }
            yield return null;
        }
    }

    IEnumerator _LoadScene()
    {
        int way = 1000 - (int)moonDistance.moonDis;
        int wayPoint = (way / 50) + 1; //맵차트 1,2,- 20
        if (way.Equals(0)) nextScene = "HallofFame";
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

        // 대기 방식


        string strAdr = "Assets/00.Scenes/" + nextScene + ".unity";
        AsyncOperationHandle<GameObject> goHandle = Addressables.LoadAssetAsync<GameObject>(strAdr);

        if(nextScene.Equals("Game 1")) { LoadAddressableLevel("Assets/00.Scenes/Game 1.unity"); }
        else if (nextScene.Equals("Game 2")) { LoadAddressableLevel("Assets/00.Scenes/Game 2.unity"); }
        else if (nextScene.Equals("Game 3")) { LoadAddressableLevel("Assets/00.Scenes/Game 3.unity"); }
        else if (nextScene.Equals("Game 4")) { LoadAddressableLevel("Assets/00.Scenes/Game 4.unity"); }
        else if (nextScene.Equals("Game 5")) { LoadAddressableLevel("Assets/00.Scenes/Game 5.unity"); }
        else if (nextScene.Equals("Game 6")) { LoadAddressableLevel("Assets/00.Scenes/Game 6.unity"); }
        else if (nextScene.Equals("Game 7")) { LoadAddressableLevel("Assets/00.Scenes/Game 7.unity"); }
        else if (nextScene.Equals("Game 8")) { LoadAddressableLevel("Assets/00.Scenes/Game 8.unity"); }
        else if (nextScene.Equals("Game 9")) { LoadAddressableLevel("Assets/00.Scenes/Game 9.unity"); }
        else if (nextScene.Equals("Game 10")) { LoadAddressableLevel("Assets/00.Scenes/Game 10.unity"); }
        else if (nextScene.Equals("Game 11")) { LoadAddressableLevel("Assets/00.Scenes/Game 11.unity"); }
        else if (nextScene.Equals("Game 12")) { LoadAddressableLevel("Assets/00.Scenes/Game 12.unity"); }
        else if (nextScene.Equals("Game 13")) { LoadAddressableLevel("Assets/00.Scenes/Game 13.unity"); }
        else if (nextScene.Equals("Game 14")) { LoadAddressableLevel("Assets/00.Scenes/Game 14.unity"); }
        else if (nextScene.Equals("Game 15")) { LoadAddressableLevel("Assets/00.Scenes/Game 15.unity"); }
        else if (nextScene.Equals("Game 16")) { LoadAddressableLevel("Assets/00.Scenes/Game 16.unity"); }
        else if (nextScene.Equals("Game 17")) { LoadAddressableLevel("Assets/00.Scenes/Game 17.unity"); }
        else if (nextScene.Equals("Game 18")) { LoadAddressableLevel("Assets/00.Scenes/Game 18.unity"); }
        else if (nextScene.Equals("Game 19")) { LoadAddressableLevel("Assets/00.Scenes/Game 19.unity"); }
        else if (nextScene.Equals("Game 20")) { LoadAddressableLevel("Assets/00.Scenes/Game 20.unity"); }

        yield return null;

    }
    public void LoadAddressableLevel(string addressableKey)
    {
        if (clearPreviousScene)
        {
            Addressables.UnloadSceneAsync(previousLoadedScene).Completed += (asyncHandle) =>
            {
                clearPreviousScene = false;
                previousLoadedScene = new SceneInstance();
                Debug.Log("Unloaded scene " + addressableKey + "successfully");
            };
        }

        Addressables.LoadSceneAsync(addressableKey, LoadSceneMode.Single).Completed += (asyncHandle) =>
        {
            clearPreviousScene = true;
            previousLoadedScene = asyncHandle.Result;
            Debug.Log("Loaded scene " + addressableKey + "successfully");
        };


    }
    public void LoadPrefab(string str)
    {
        // 1안, 어드레서블 명으로 경로 얻기.
        Addressables.LoadResourceLocationsAsync("SomeAddrsaalbeName").Completed +=
            (handle) =>
            {
                var locations = handle.Result;

                Addressables.InstantiateAsync(locations[0]);
            };

    }

}
