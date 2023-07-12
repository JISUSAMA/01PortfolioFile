using Coffee.UIExtensions;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableLandMark : MonoBehaviour {

    // Parent Canvas
    [SerializeField] private Canvas canvas;

    //[Header("Show Move Values")]
    //public Vector2 originPosition;
    
    //public float duration;
    //public Ease showEase;

    [Header("Zoom Values")]
    public Vector3 _extendedScale;
    public Vector2 zoomInPosition;
    public Vector2 zoomOutPosition;
    public float zoomInOutDuration;
    public Ease zoomEase;

    [Header("Slide Values")]
    public float _xOffset;

    private ShinyEffectForUGUI effect;
    private RectTransform rect;

    float _xClickedPos;
    bool OnDragging = false;
    bool OnZooming = false;
    bool OnZoomOut;
    IEnumerator loopShiny = null;

    Camera cam;
    RectTransform CanvasRect;

    private void Awake() {
        effect = transform.GetComponent<ShinyEffectForUGUI>();
        rect = transform.GetComponent<RectTransform>();
        CanvasRect = canvas.GetComponent<RectTransform>();
        cam = Camera.main;
    }

    private void OnEnable() {
        loopShiny = LoopShiny(2f, 3.5f);
        StartCoroutine(loopShiny);
    }

    private void OnDisable() {
        if (loopShiny != null) {
            StopCoroutine(loopShiny);
        }
    }

    IEnumerator LoopShiny(float duration, float time) {
        if (!OnZoomOut) { yield break; }    // ZoomOut State 에서만 동작
        while (true) {
            effect.Play(duration);
            yield return new WaitForSeconds(time);
        }
    }

    public void ZoomInOut() {
        if (!OnDragging) {
            if (OnZoomOut) {
                ZoomIn();
            } else {
                ZoomOut();
            }
        }
    }

    public void ZoomIn() {

        Sequence mySequence = DOTween.Sequence()
            .OnStart(() =>
            {
                OnZooming = true;
                StopCoroutine(loopShiny);
            })
            .Append(transform.DOScale(_extendedScale, zoomInOutDuration).SetEase(zoomEase))
            .Join(rect.DOAnchorPos(zoomInPosition, zoomInOutDuration).SetEase(zoomEase))
            .OnComplete(() => {
                OnZoomOut = false;
                OnZooming = false;
            });
    }

    public void ZoomOut() {
        Sequence mySequence = DOTween.Sequence()
            .OnStart(() =>
            {
                OnZooming = true;
            })
            .Append(transform.DOScale(1f, zoomInOutDuration).SetEase(zoomEase))
            .Join(rect.DOAnchorPos(zoomOutPosition, zoomInOutDuration).SetEase(zoomEase))
            .OnComplete(() => {
                
                OnZoomOut = true;
                OnZooming = false;

                loopShiny = LoopShiny(2f, 3.5f);
                StartCoroutine(loopShiny);
            });
    }

    public void BeginDrag(BaseEventData baseEventData) {

        PointerEventData pointerEventData = baseEventData as PointerEventData;

        Vector2 position;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerEventData.position,
            canvas.worldCamera,
            out position);

        Vector3 worldPosition = canvas.transform.TransformPoint(position);
        Vector2 ViewportPosition = cam.WorldToViewportPoint(worldPosition);
        
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        _xClickedPos = WorldObject_ScreenPosition.x;
    }

    public void Dragging(BaseEventData baseEventData) {

        if (OnZooming) { return; }

        OnDragging = true;

        PointerEventData pointerEventData = baseEventData as PointerEventData;

        Vector2 position;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerEventData.position,
            canvas.worldCamera,
            out position);

        // worldPosition in Canvas localTransform
        // 캔버스상에 GameObject의 WorldPosition 좌표와 같다. => Screen Space - Camera 일 경우
        Vector3 worldPosition = canvas.transform.TransformPoint(position);
        
        // 그 월드포지션을 화면상 Viewport 로 변환
        Vector2 ViewportPosition = cam.WorldToViewportPoint(worldPosition);

        // 캔버스 비율을 통해서 앵커위치 스크린포지션으로 변환        
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));


        rect.anchoredPosition = new Vector2(WorldObject_ScreenPosition.x, rect.anchoredPosition.y);
        if (_xClickedPos + _xOffset < rect.anchoredPosition.x) { // Right
            if (!OnZoomOut) {
                ZoomOut();
            } else {
                IntroducingLandMarks.Instance.Hide();
            }
        } else if (_xClickedPos - _xOffset > rect.anchoredPosition.x) { // Left            
            ZoomIn();
        }
    }

    public void EndDrag() {
        OnDragging = false;
    }
}
