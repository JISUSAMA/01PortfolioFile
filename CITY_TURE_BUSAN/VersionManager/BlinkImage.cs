using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlinkImage : MonoBehaviour, IBlink<Image>
{
    [Header("Alarm")]
    private Image touchImage;
    public float fadeTime = 1.3f;
    Tween _twBlink;

    private void Awake()
    {
        touchImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        Debug.Log("OnEn");
        _twBlink = Blink(touchImage).Play().SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        Debug.Log("OnDis");
        _twBlink.Pause();
        _twBlink.Kill();
    }

    private void OnDestroy()
    {
        Debug.Log("OnDis");
        _twBlink.Pause();
        _twBlink.Kill();
    }

    public Tween Blink(Image blink)
    {
        Tween twBlink = DOTween.Sequence()
            .SetAutoKill(false)
            .OnStart(() =>
            {
                Debug.Log("OnStart");
            })
            .Append(blink.DOFade(0f, fadeTime))
            .Append(blink.DOFade(1f, fadeTime))
            .OnComplete(() =>
            {
                Debug.Log("OnComplete");
            });

        return twBlink;
    }
}
