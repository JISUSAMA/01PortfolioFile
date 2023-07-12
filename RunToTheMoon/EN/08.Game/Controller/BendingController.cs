using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmazingAssets.CurvedWorld;
using TunnelEffect;

[System.Serializable]
public class Preset
{
    public string pos;
    public float curatureSize;
    public float curatureOffSet;
    public float horizonSize;
    public float horizonOffSet;
    public float verticalSize;
    public float verticalOffSet;
    public float rot_X;
    public float rot_Y;
    public float rot_Z;
}

public class BendingController : MonoBehaviour
{
    // Curved World Controller
    CurvedWorldController curvedWorldController;
    // Tunnel Rot
    [Header("[Tunnel Group]")]
    [SerializeField]
    GameObject tunnelObject;
   
    [Header("[Cureved Position]")] [SerializeField] private List<Preset> preset;
    [Header("[CurevedTile SpawnPostion By 5Km]")] [Range(0, 5)] [SerializeField] private List<float> spawnPos;

    // 랜덤값
    int rand_idx = 5;
    List<int> randValues = new List<int>();

    // 정거장
    float spaceStationDis = 0f;

    // 달
    public GameObject moonObj;
    // Start is called before the first frame update
    void Start()
    {
        // 커브 컨트롤러
        curvedWorldController = GetComponent<CurvedWorldController>();

        // 1. 현재 휴게소까지 남은 거리 저장
        spaceStationDis = Game_DataManager.instance.spaceStationDis;
       
        // 2. 랜덤 커브 설정
        RandomValues();

        // 3. 5km 단위 안에 커브 제어 : 일단 3개
        Bending();
    

    }

    private void Bending()
    {
        StartCoroutine(AutoCurve());
    }

    IEnumerator AutoCurve()
    {
        float deltaDis;
        int idx = 0;
        rand_idx = randValues[idx];
        //Debug.Log("moonObj--1 : " + moonObj.transform.localPosition);

        // 게임 중 일 때
        while (Game_DataManager.instance.gamePlaying)
        {
            //Debug.Log("moonObj--2 : " + moonObj.transform.localPosition);
            // 이동거리
            deltaDis = spaceStationDis - Game_DataManager.instance.spaceStationDis;

            // 정거장 위치에 따른 커브 변경
            // spawnPos[0], spawnPos[1], spawnPos[2] 현재 3개 설정
            if (deltaDis < spawnPos[idx])
            {
                //Debug.Log("moonObj--3 : " + moonObj.transform.localPosition);
                // 달의 남은 거리 20km 남았으면 N 방향
                if (Game_DataManager.instance.moonDis <= 20f /*&& !moonObj.activeSelf*/)
                {
                    // 20km 남았을 때
                    rand_idx = 1;
                    moonObj.SetActive(true);
                    //Debug.Log("moonObj : " + moonObj.transform.localPosition);

                    if (Game_DataManager.instance.moonDis <= 15f)
                    {
                        //Debug.Log("moonObj--4 : " + moonObj.transform.localPosition);
                        // 10km 남았을 때
                        // 위치
                        moonObj.transform.localPosition = new Vector3(0, 0, 0.23f);

                        if (Game_DataManager.instance.moonDis <= 10f)
                        {
                            // 5km 남았을 때
                            // 위치w
                            moonObj.transform.localPosition = new Vector3(0, 0, 0.18f);
                            if(Game_DataManager.instance.moonDis <= 5f)
                            {
                             //   moonObj.SetActive(false);
                       //         Game_UIManager.instance.cameraOb[1].enabled = true; //측면 뷰


                                //Game_UIManager.instance.cameraOb[0].enabled = false;//정면 뷰


                                moonObj.transform.localPosition = new Vector3(0, 0, 0.02342f);
                               
                            }
                        }
                    }
                }

                Change_BendingSetting();
                //Debug.Log("moonObj--6 : " + moonObj.transform.localPosition);
            }
      
            else if (deltaDis > 5f)
            {
                // 휴게소 주기
                spaceStationDis = Game_DataManager.instance.spaceStationDis;
                idx = 0;
                RandomValues();
            }
            else
            {
                if (spawnPos.Count-1 > idx)
                {
                    idx++;
                    rand_idx = randValues[idx];
                }
            }

            yield return null;
        }

        yield return null;
    }

    private void Change_BendingSetting()
    {
        //rand = Random.Range(0, 9);

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //curvedWorldController.bendCurvatureSize = Mathf.Lerp(curvedWorldController.bendCurvatureSize, preset[rand_idx].curatureSize, Time.deltaTime * 0.05f);
                //curvedWorldController.bendCurvatureOffset = Mathf.Lerp(curvedWorldController.bendCurvatureOffset, preset[rand_idx].curatureOffSet, Time.deltaTime * 0.06f);
                curvedWorldController.bendHorizontalSize = Mathf.Lerp(curvedWorldController.bendHorizontalSize, preset[rand_idx].horizonSize, Time.deltaTime * 0.05f);
                curvedWorldController.bendHorizontalOffset = Mathf.Lerp(curvedWorldController.bendHorizontalOffset, preset[rand_idx].horizonOffSet, Time.deltaTime * 0.05f);
                curvedWorldController.bendVerticalSize = Mathf.Lerp(curvedWorldController.bendVerticalSize, preset[rand_idx].verticalSize, Time.deltaTime * 0.05f);
                curvedWorldController.bendVerticalOffset = Mathf.Lerp(curvedWorldController.bendVerticalOffset, preset[rand_idx].verticalOffSet, Time.deltaTime * 0.05f);

                // Tunnel Rot
                Quaternion currentRotation = tunnelObject.transform.rotation;
                Quaternion wantedRotation = Quaternion.Euler(preset[rand_idx].rot_X, preset[rand_idx].rot_Y+90f, preset[rand_idx].rot_Z);
                tunnelObject.transform.rotation = Quaternion.Lerp(currentRotation, wantedRotation, Time.deltaTime * 0.07f);
            }
            else
            {
                //curvedWorldController.bendCurvatureSize = Mathf.Lerp(curvedWorldController.bendCurvatureSize, preset[rand_idx].curatureSize, Time.deltaTime * 0.005f);
                //curvedWorldController.bendCurvatureOffset = Mathf.Lerp(curvedWorldController.bendCurvatureOffset, preset[rand_idx].curatureOffSet, Time.deltaTime * 0.005f);
                curvedWorldController.bendHorizontalSize = Mathf.Lerp(curvedWorldController.bendHorizontalSize, preset[rand_idx].horizonSize, Time.deltaTime * 0.04f);
                curvedWorldController.bendHorizontalOffset = Mathf.Lerp(curvedWorldController.bendHorizontalOffset, preset[rand_idx].horizonOffSet, Time.deltaTime * 0.04f);
                curvedWorldController.bendVerticalSize = Mathf.Lerp(curvedWorldController.bendVerticalSize, preset[rand_idx].verticalSize, Time.deltaTime * 0.04f);
                curvedWorldController.bendVerticalOffset = Mathf.Lerp(curvedWorldController.bendVerticalOffset, preset[rand_idx].verticalOffSet, Time.deltaTime * 0.04f);

                // Tunnel Rot
                Quaternion currentRotation = tunnelObject.transform.rotation;
                Quaternion wantedRotation = Quaternion.Euler(preset[rand_idx].rot_X, preset[rand_idx].rot_Y+90f, preset[rand_idx].rot_Z);
                tunnelObject.transform.rotation = Quaternion.Lerp(currentRotation, wantedRotation, Time.deltaTime * 0.07f);
            }
        }
    }

    private void RandomValues()
    {
        randValues.Clear();
        for (int i = 0; i < spawnPos.Count; i++)
        {
            randValues.Add(Random.Range(0, 6));
        }

        //Debug.Log("0 : "+randValues[0]);
        //Debug.Log("1 : "+randValues[1]);
        //Debug.Log("2 : "+randValues[2]);
        
    }
}
