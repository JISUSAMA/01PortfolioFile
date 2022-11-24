using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 계단이 사라질 때 반환
public class ObjectReturn : MonoBehaviour
{
    private ObjectPool objectPool;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
    }

    private void OnDisable()
    {
        if(objectPool!= null)
        {
            // Stage마다 그때그때 다름
            // 현재 Stair_1
            objectPool.ReturnGameObject(objectPool.pools[0].tag);
        }
            
    }
}