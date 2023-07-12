using Coffee.UIExtensions;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class IntroducingLandMarks : MonoBehaviour {

    public static IntroducingLandMarks Instance { get; private set; }

    // Show Landmarks
    public RectTransform[] landMarks;

    [Header("Show & Hide Move Values")]
    public Vector2 originPosition;
    public Vector2 showPosition;
    public float duration;
    public Ease showEase;

    //GUIStyle style = new GUIStyle();

    //float rect_pos_x = 5f;
    //float rect_pos_y = 5f;
    //float screenWidth = Screen.width;
    //float screenHeight = Screen.height;
    //float h = 55f;
    //Camera cam;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }

        //style.normal.textColor = Color.red;
        //style.fontSize = 40;

        //cam = Camera.main;
    }


    public void Show(string landMarksName) {
        for (int i = 0; i < landMarks.Length; i++) {
            if (landMarks[i].name == landMarksName) {
                if (landMarks[i].gameObject.activeSelf) { break; }  // duplicated Show Call block                
                landMarks[i].gameObject.SetActive(true);                
                landMarks[i].DOAnchorPos(showPosition, duration).SetEase(showEase);
                break;
            }
        }
    }

    public void Hide() {
        for (int i = 0; i < landMarks.Length; i++) {
            if (landMarks[i].gameObject.activeSelf) {
                landMarks[i].DOAnchorPos(originPosition, duration).SetEase(showEase).OnComplete(() => OnCompletedLandmarkPanel(i));
                break;
            }
        }
    }

    private void OnCompletedLandmarkPanel(int landMarkIndex) {
        landMarks[landMarkIndex].transform.localScale = Vector3.one;
        StopAllCoroutines();
        landMarks[landMarkIndex].gameObject.SetActive(false);
    }

    //void OnGUI() {
    //    Vector2 point = new Vector2();
    //    Event currentEvent = Event.current;
    //    Vector2 mousePos = new Vector2();

    //    // Get the mouse position from Event.
    //    // Note that the y position from Event is inverted.
    //    mousePos.x = currentEvent.mousePosition.x;
    //    mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

    //    point = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
    //    //point = cam.ScreenToWorldPoint(Input.mousePosition);
    //    //point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
    //    //point = new Vector3(mousePos.x, mousePos.y, 2f);

    //    GUI.Label(new Rect(rect_pos_x, rect_pos_y + h * 2, screenWidth, h), "CAM pixels: " + cam.pixelWidth + ":" + cam.pixelHeight, style);
    //    GUI.Label(new Rect(rect_pos_x, rect_pos_y + h * 3, screenWidth, h), "Screen pixels: " + screenWidth + ":" + screenHeight, style);        
    //    GUI.Label(new Rect(rect_pos_x, rect_pos_y + h * 4, screenWidth, h), "anchor position: " + point.ToString("F3"), style);
    //}
}