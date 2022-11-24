using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BrainGame4_DrawCtrl : MonoBehaviour
{
    //public GameObject currentLine;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> fingerPositions;

    [Header("MultyTouch")]
    private Vector3 touchedPos;
    private bool touchOn;
    private Touch tempTouchs;
    bool DrawLine_GameRun;

    public int index;
    private void Awake()
    {
        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        edgeCollider = this.gameObject.GetComponent<EdgeCollider2D>();
    }
    private void Start()
    {
        DrawLine_Start();
    }
    public void DrawLine_Start()
    {
        StopCoroutine(_DrawLine_Start());
        StartCoroutine(_DrawLine_Start());
    }
    IEnumerator _DrawLine_Start()
    {
        DrawLine_GameRun = true;
        while (DrawLine_GameRun)
        {
            //#if UNITY_EDITOR_WIN
            //            if (Input.GetMouseButtonDown(0))
            //            {
            //                fingerPositions.Clear();
            //                CreateLine();
            //            }
            //            if (Input.GetMouseButtonUp(0))
            //            {
            //                fingerPositions.Clear();
            //                DrawLine_GameRun = false;
            //            }
            //            if (Input.GetMouseButton(0))
            //            {
            //                Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //                transform.position = tempFingerPos;
            //                if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f)
            //                {
            //                    UpdateLine(tempFingerPos);
            //                }
            //            }
            //#endif
#if UNITY_ANDROID
            //터치좌표
            Touch t = Input.GetTouch(index);
            //  Vector2 pos = t.position;
            //  float x, y, dx, dy;
            //  x = pos.x;
            //  y = pos.y;
            //  dx = pos.x - BrainGame4_UIManager.instance.delta[index].x;
            //  dy = pos.y - BrainGame4_UIManager.instance.delta[index].y;


            if (t.phase == TouchPhase.Began)
            {
                fingerPositions.Clear();
                CreateLine();
            }
            else if (t.phase == TouchPhase.Moved)
            {
                Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(t.position);
                transform.position = tempFingerPos;
                if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f)
                {
                    UpdateLine(tempFingerPos);
                }
            }
            else if (t.phase == TouchPhase.Ended)
            {
                this.gameObject.GetComponent<LineRenderer>().positionCount = 0;
                fingerPositions.Clear();

                //  DrawLine_GameRun = false;
            }
            yield return null;
        }
#endif 
    }


    //선그리기
    void CreateLine()
    {
        //#if UNITY_EDITOR_WIN
        //        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        //        edgeCollider = this.gameObject.GetComponent<EdgeCollider2D>();
        //        fingerPositions.Clear();
        //
        //        Vector3 line_StartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        fingerPositions.Add(line_StartPos);
        //        fingerPositions.Add(line_StartPos);
        //        lineRenderer.SetPosition(0, fingerPositions[0]);
        //        lineRenderer.SetPosition(1, fingerPositions[1]);
        //        edgeCollider.points = fingerPositions.ToArray();
        //#endif
#if UNITY_ANDROID
        Touch t = Input.GetTouch(index);
        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        edgeCollider = this.gameObject.GetComponent<EdgeCollider2D>();
        fingerPositions.Clear();

        Vector3 line_StartPos_android = Camera.main.ScreenToWorldPoint(t.position);
        fingerPositions.Add(line_StartPos_android);
        fingerPositions.Add(line_StartPos_android);
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
        edgeCollider.points = fingerPositions.ToArray();

#endif
    }
    public void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
        edgeCollider.points = fingerPositions.ToArray();
    }
  
}

