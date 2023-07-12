using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopStarField : MonoBehaviour
{
    //[Header("Distance")]
    //public float distance;
    //[Header("Speed")]
    //public float speed;

    //private Vector3 TargetPos;
    //private Vector3 startPos;

    [SerializeField] [Range(0f, 10f)] private float speed;
    [SerializeField] [Range(0f, 10000)] private float radius;
    private float runningTime = 0;
    private Vector3 newPos = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        //startPos = transform.position;
        //TargetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance);
    }

    //Update is called once per frame
    void Update()
    {
        runningTime += Time.deltaTime * (speed + 0.0008f);  // 별 속도
        float x = radius * Mathf.Cos(runningTime);
        float z = radius * Mathf.Sin(runningTime);
        newPos = new Vector3(x, 0, z);
        this.transform.position = newPos;
        //Debug.Log(newPos);
    }
}
