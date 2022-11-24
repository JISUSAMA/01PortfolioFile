using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainGame4_UIManager : MonoBehaviour
{
    public GameObject[] LeftGrup; //도형이 담긴 그룹
    public GameObject[] RigthGrup;//도형이 담긴 그룹

    public bool DrawLine_GameRun, DrawLine_Click;
    [Header("DrawLine")]
    public GameObject handle;
    public GameObject linePrefab;

    [Header("정답 이미지")]
    public GameObject AnswerOb;
    public Image AnswerImg;
    public Sprite[] AnswerImg_sprite;
    public Slider AnswerTimer_slider; //타이머
    public Text AnswerTimer_text;

    public Text DebugText_0;
    public Text DebugText_1;

    public List<TouchLocation> touches = new List<TouchLocation>();
    public List<GameObject> touches_ob = new List<GameObject>();
    public GameObject currentLine;

    public Vector2[] delta = new Vector2[5];

  //  public GameObject[] dot; //1.dot 2.mid 3.end
    public bool checkPoint1, checkPoint2 , checkPoint3,checkPoint4 = false;
    public static BrainGame4_UIManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }
    public void DrawLine_Start()
    {
        StopCoroutine("_DrawLine_Start");
        StartCoroutine("_DrawLine_Start");
    }
    IEnumerator _DrawLine_Start()
    {
        DrawLine_GameRun = true;
        while (DrawLine_GameRun)
        {
         
            int count = Input.touchCount;

            for(int i =0; i<count; i++)
            {
                    Touch touch = Input.GetTouch(i);
                    int id = touch.fingerId;

                    //터치좌표
                    Vector2 pos = touch.position;
                    //begin이라면 무조건 delta에 넣어줌
                    if (touch.phase == TouchPhase.Began)
                    {
                        GameObject ob = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
                        ob.GetComponent<BrainGame4_DrawCtrl>().index = id;
                        delta[id] = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        if (checkPoint1 && checkPoint2 && checkPoint3 && checkPoint4)
                        {
                            BrainGame4_DataManager.instance.Question_Success();
                            DrawLine_GameRun = false;
                        }
                        else
                        {
                            BrainGame4_DataManager.instance.Question_Fail();
                          //  DrawLine_GameRun = false;
                        }
                    }

                    DebugText_0.text = " checkPoint1 :  " + checkPoint1 + " checkPoint2 : " + checkPoint2
                    + " checkPoint3 :  " + checkPoint3 + " checkPoint4 : " + checkPoint4;
           

            }

            //     if (Input.GetMouseButtonDown(0))
            //     {
            //         GameObject ob = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
            //         touches_ob.Add(ob);
            //     }
            //
          
            yield return null;
        }

    }
    //    public GameObject circle;
    //  public List touches = new List();

    // Update is called once per frame
    //void Update () {
    //    int i = 0;
    //     
    //    while(i < Input.touchCount){
    //        Touch t = Input.GetTouch(i);
    //        if(t.phase == TouchPhase.Began){
    //            Debug.Log("touch began");
    //            touches.Add(new TouchLocation(t.fingerId, createCircle(t)));
    //        }else if(t.phase == TouchPhase.Ended){
    //            Debug.Log("touch ended");
    //            TouchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);
    //            Destroy(thisTouch.currentLine);
    //            touches.RemoveAt(touches.IndexOf(thisTouch));
    //        }else if(t.phase == TouchPhase.Moved){
    //            Debug.Log("touch is moving");
    //            TouchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);
    //            thisTouch.currentLine.transform.position = getTouchPosition(t.position);
    //        }
    //        ++i;
    //    }
    //}
    //Vector2 getTouchPosition(Vector2 touchPosition){
    //    return cam.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    //}
    //GameObject createCircle(Touch t){
    //    GameObject c = Instantiate(circle) as GameObject;
    //    c.name = "Touch" + t.fingerId;
    //    c.transform.position = getTouchPosition(t.position);
    //    return c;
    //}

}

