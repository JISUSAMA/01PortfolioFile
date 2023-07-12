using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair_rep : MonoBehaviour
{
    private Vector3 TargetPos;
    private bool once;
    private void OnDisable()
    {
        // 사라질 때
        // 기준높이 이하 일 때
    }
    // Start is called before the first frame update
    void Start()
    {
        once = false;
        TargetPos = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z - 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance.isEnableStairMove)
        {
            Debug.Log("이동 중");
            transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * 3f);
        }
        else if (once)
        {
            once = false;
            CurrentPos();
        }
    }

    private void CurrentPos()
    {
        TargetPos = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z - 0.5f);
        
    }
}
