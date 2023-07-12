using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private float delta_y = 0.1f;
    [SerializeField] private float delta_z = 0.5f;

    private float plusWidth = 0f;
    private float plusHeight = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Spawn("Stair_1");
        //Create_LinearValue();
    }

    public void Spawn(string _stage)
    {
        // 갯수 가져와야하고, 갯수 * 처음위치
        // 처음과 끝만 사라지고 생기게
        if (ObjectPool.Instance.poolDictionary[_stage].Count != 0)
        {
            //Create_LinearValue();            
            plusWidth = delta_y * ObjectPool.Instance.poolDictionary[_stage].Count;
            plusHeight = delta_z * ObjectPool.Instance.poolDictionary[_stage].Count;
            ObjectPool.Instance.SpawnFromPool(_stage, new Vector3(startPos.position.x + Create_LinearValue(), startPos.position.y + plusWidth, startPos.position.z + plusHeight), Quaternion.identity);
        }
    }

    [SerializeField] GameObject sphere;
    [SerializeField] [Range(0f, 10f)] private float speed = 1f;
    [SerializeField] [Range(0f, 10f)] private float length = 1f;

    private float runningTime = 0f;
    private float retValue = 0f;
    private float xPos = 0f;

    Vector3 retVector;

    //public int speed = 1;
    private float degree = 0;

    private float Create_LinearValue()
    {
        runningTime += Time.deltaTime * speed;

        //Debug.Log("runningTime : " + runningTime);
        retValue = Mathf.Sin(runningTime) * length;
        //xPos = Mathf.Cos(runningTime) * length;
        //Debug.Log("yPos : " + yPos);
        sphere.transform.position = new Vector3(retValue, 0, 0);

        //degree += speed;
        //float radian = degree * Mathf.PI / 180; //라디안값

        //Debug.Log("radian : " + radian);
        //retVector.x += 0.1f * Mathf.Cos(radian);
        //retVector.y += 0.1f * Mathf.Sin(radian);

        //sphere.transform.position = retVector;
        Debug.Log("retValue : " + retValue);

        return retValue;
    }
}
