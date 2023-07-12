using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewManCtrl : MonoBehaviour
{
    [Header("애니메이션")]
    public Animator man_BicycleAnim;  //자전거 애니
    public Animator man_bodyAnim;   //몸 애니


    public void Animator_Initialization()
    {
        //Debug.Log("_shoes " + _shoes);
        man_BicycleAnim.Rebind(); 
        man_bodyAnim.Rebind();
    }

    //속도 빠른 애니메이션
    public void Animator_Speed(bool _normalState, bool _speedState, float _speed)
    {
        man_BicycleAnim.SetFloat("Speed", _speed);
        man_BicycleAnim.SetBool("NormalStart", _normalState);
        man_BicycleAnim.SetBool("SpeedStart", _speedState);

        man_bodyAnim.SetFloat("Speed", _speed);
        man_bodyAnim.SetBool("NormalStart", _normalState);
        man_bodyAnim.SetBool("SpeedStart", _speedState);
    }

    public void AnimClear_Fail(float _speed, bool _normalState, bool _speedState, bool _clearState, bool _failState)
    {
        man_BicycleAnim.SetFloat("Speed", _speed);
        man_BicycleAnim.SetBool("NormalStart", _normalState);
        man_BicycleAnim.SetBool("SpeedStart", _speedState);
        man_BicycleAnim.SetBool("ClearStart", _clearState);
        man_BicycleAnim.SetBool("FailStart", _failState);

        man_bodyAnim.SetFloat("Speed", _speed);
        man_bodyAnim.SetBool("NormalStart", _normalState);
        man_bodyAnim.SetBool("SpeedStart", _speedState);
        man_bodyAnim.SetBool("ClearStart", _clearState);
        man_bodyAnim.SetBool("FailStart", _failState);
    }
}
