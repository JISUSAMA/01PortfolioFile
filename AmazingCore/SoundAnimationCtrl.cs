using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAnimationCtrl : MonoBehaviour
{
    /////////////////// ���������� ���� //////////////////////
    public void DeliveryMan_Bicycle_sound()
    {
        SoundManager.instance.PlaySFX_OneShot("bicycle-bell-ring");
    }
    /////////////////// ���̾��ũ ���� //////////////////////
    public void WireWalking_shaking()
    {
        SoundManager.instance.PlaySFX_OneShot("moving");
    }

}
