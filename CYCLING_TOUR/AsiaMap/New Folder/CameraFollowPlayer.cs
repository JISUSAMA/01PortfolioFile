using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    // public Transform target;
    public float distance = 10.0f;
    public float height = 2.0f;
    [SerializeField] private float rotationDamping = 3.0f;
    [SerializeField] private float heightDamping = 2.0f;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    private void LateUpdate()
    {
       //  Refresh();
          if (!target)

              return;

          var wantedRotationAngle = target.eulerAngles.y;
          var wantedHeight = target.position.y + height;

          var currentRotationAngle = transform.eulerAngles.y;
          var currentHeight = transform.position.y;


          currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

          currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

          var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

          transform.position = target.position;
          transform.position -= currentRotation * Vector3.forward * distance;

          transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

          transform.LookAt(target);
        


    }
    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);
            return;
        }
        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = target.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = target.position + offsetPosition;
        }
        // compute rotation
        if (lookAt)
        {
            transform.LookAt(target);
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }
}
