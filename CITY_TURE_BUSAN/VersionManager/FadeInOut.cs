using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [Header("Alarm")]
    public GameObject parentObj;
    public GameObject versionCheckObj;
    public Sprite[] logo;
    public float fadeTime = 1.5f;
    Tween _twBlink;
    Image logoImage;    
    
    private void Awake()
    {
        logoImage = GetComponent<Image>();        
    }

    private void OnEnable()
    {
        _twBlink = Blink(logoImage);
    }

    public void StartBlink()
    {        
        _twBlink = Blink(logoImage);
    }

    private void OnDisable()
    {
        _twBlink.Pause();
        _twBlink.Kill();
    }

    private void OnDestroy()
    {
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
                blink.sprite = logo[0];
                Color color;
                color = blink.color;
                color = Color.white;
                color.a = 0f;
                blink.color = color;
            })
            .Append(blink.DOFade(1f, fadeTime).SetEase(Ease.OutCubic))
            .Append(blink.DOFade(0f, fadeTime).SetEase(Ease.InCubic))
            .OnComplete(() =>
            {
                blink.sprite = logo[1];
                
                blink.DOFade(1f, fadeTime).SetEase(Ease.OutCubic)
                .SetAutoKill(false)
                .OnComplete(() => {
                    
                    blink.DOColor(Color.black, fadeTime).SetEase(Ease.InCubic)
                    .SetAutoKill(false)
                    .OnComplete(() => {
                        versionCheckObj.SetActive(true);
                        parentObj.SetActive(false);
                    });
                });
            });

        return twBlink;
    }
}
