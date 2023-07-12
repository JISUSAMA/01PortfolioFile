using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public interface IBlink<T>
{
    Tween Blink(T blink);
}
