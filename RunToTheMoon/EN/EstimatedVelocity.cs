using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 運動状態推定コンポーネント
/// </summary>
public class EstimatedVelocity : MonoBehaviour
{
    /// <summary>
    /// 推定速度 [m/s]
    /// </summary>
    [SerializeField]
    private Vector3 _estimatedVelocity = Vector3.zero;

    /// <summary>
    /// 推定角速度 [rad/s]
    /// </summary>
    [SerializeField]
    private Vector3 _estimatedAngularVelocity = Vector3.zero;

    /// <summary>
    /// 位置前回値
    /// </summary>
    private Vector3 _positionPrevious = Vector3.zero;

    /// <summary>
    /// 回転前回値
    /// </summary>
    private Quaternion _rotationPrevious = Quaternion.identity;


    /// <summary>
    /// 推定速度 [m/s]
    /// </summary>
    public Vector3 _EstimatedVelocity
    {
        get { return _estimatedVelocity; }
    }

    /// <summary>
    /// 推定角速度 [rad/s]
    /// </summary>
    public Vector3 EstimatedAngularVelocity
    {
        get { return _estimatedAngularVelocity; }
    }


    private void FixedUpdate()
    {
        // 位置変化量から速度 [m/s] を算出
        _estimatedVelocity = (transform.position - _positionPrevious) / Time.deltaTime;

        Debug.Log("속도 : "+ _estimatedVelocity);

        // 今回値を覚えておく
        _positionPrevious = transform.position;


        // 回転変化量を計算
        Quaternion deltaRotation = Quaternion.Inverse(_rotationPrevious) * transform.rotation;

        // 角度と回転軸に変換
        deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

        // 角速度 [rad/s] を算出
        float angularSpeed = (angle * Mathf.Deg2Rad) / Time.deltaTime;
        _estimatedAngularVelocity = axis * angularSpeed;

        Debug.Log("각속도 : " + _estimatedAngularVelocity);

        // 今回値を覚えておく
        _rotationPrevious = transform.rotation;
    }
}