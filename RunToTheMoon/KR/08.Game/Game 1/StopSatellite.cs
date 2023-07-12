using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSatellite : MonoBehaviour
{
    Rigidbody rigidbody;
    float leftTime;
    bool touch;
    void Start()
    {
        leftTime = 0f;
        touch = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (TryGetComponent(out rigidbody) && touch)
        {
            leftTime += Time.deltaTime;
            Debug.Log("rigidbody.velocity  : " + rigidbody.velocity);

            if (leftTime > 8.0f)
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
    
                // √ ±‚»≠
                leftTime = 0f;
                touch = false;
            }
            Debug.Log("touch : " + touch);
        }

        //Debug.Log("touch : " + touch);
    }

    private void OnTriggerEnter(Collider other)
    {
        leftTime = 0f;
        touch = true;
    }
}
