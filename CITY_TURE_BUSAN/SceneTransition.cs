using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

public class SceneTransition : MonoBehaviour {
        
    [SerializeField] private CanvasGroup _canvasGroup;
    public async UniTask StartTransition() {

        Tween tween = DOTween.Sequence()
            .SetAutoKill(false)
            .OnStart(() =>
            {
                _canvasGroup.gameObject.SetActive(true);
                _canvasGroup.alpha = 0f;
            })
            .Append(_canvasGroup.DOFade(1, 0.7f))
            .OnComplete(() =>
            {
                Debug.Log("OnCompleted");
            });

        await UniTask.Yield();
    }

    //IEnumerator _StartTransition() {

    //    Tween tween = DOTween.Sequence()            
    //        .SetAutoKill(false)
    //        .OnStart(() =>
    //        {
    //            isTransitioning = true;
    //            _canvasGroup.alpha = 0f;
    //        })
    //        .Append(_canvasGroup.DOFade(1, 0.7f))
    //        .OnComplete(() =>
    //        {
    //            isTransitioning = false;
    //        });

    //    yield return tween.WaitForCompletion();
    //}

    public async UniTask EndTransition() {
        
        Tween tween = DOTween.Sequence()
            .SetAutoKill(false)
            .OnStart(() =>
            {                
                _canvasGroup.alpha = 1f;
            })
            .Append(_canvasGroup.DOFade(0, 0.7f))
            .OnComplete(() =>
            {
                _canvasGroup.gameObject.SetActive(false);
            });

        await UniTask.Yield();
    }

    //IEnumerator _EndTransition() {        
    //    Tween tween = DOTween.Sequence()
    //        .SetAutoKill(false)
    //        .OnStart(() =>
    //        {
    //            isTransitioning = true;
    //            _canvasGroup.alpha = 1f;
    //        })
    //        .Append(_canvasGroup.DOFade(0, 0.7f))
    //        .OnComplete(() =>
    //        {
    //            isTransitioning = false;                
    //        });

    //    yield return tween.WaitForCompletion();
    //}
}