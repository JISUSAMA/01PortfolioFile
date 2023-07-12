using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWomanCtrl : MonoBehaviour
{
    [Header("애니메이션")]
    public Animator woman_BodyAnim;   //동서흑 순서 몸 애니(서양 애니는 기본이라서 기본으로 깔고 가야함)
    public Animator woman_BicycleAnim;  //자전거 애니

    public void Animator_Initialization()
    {
        woman_BicycleAnim.Rebind();
        woman_BodyAnim.Rebind();
    }

    //속도 빠른 애니메이션
    public void Animator_Speed(bool _normalState, bool _speedStat, float _speed)
    {
        woman_BicycleAnim.SetFloat("Speed", _speed);
        woman_BicycleAnim.SetBool("NormalStart", _normalState);
        woman_BicycleAnim.SetBool("SpeedStart", _speedStat);

        woman_BodyAnim.SetFloat("Speed", _speed);
        woman_BodyAnim.SetBool("NormalStart", _normalState);
        woman_BodyAnim.SetBool("SpeedStart", _speedStat);
    }

    public void AnimClear_Fail(float _speed, bool _normalState, bool _speedState, bool _clearState, bool _failState)
    {
        woman_BicycleAnim.SetFloat("Speed", _speed);
        woman_BicycleAnim.SetBool("NormalStart", _normalState);
        woman_BicycleAnim.SetBool("SpeedStart", _speedState);
        woman_BicycleAnim.SetBool("ClearStart", _clearState);
        woman_BicycleAnim.SetBool("FailStart", _failState);

        woman_BodyAnim.SetFloat("Speed", _speed);
        woman_BodyAnim.SetBool("NormalStart", _normalState);
        woman_BodyAnim.SetBool("SpeedStart", _speedState);
        woman_BodyAnim.SetBool("ClearStart", _clearState);
        woman_BodyAnim.SetBool("FailStart", _failState);
    }
}
