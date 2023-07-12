using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSporeSpawn : MonoBehaviour
{
    public int poolLimit = 30;
    //public int minPerBatch = 1;
    //public int maxPerBatch = 5;
    //public float minInterval = 0.5f;
    public GameObject[] toSpawn;

    List<GameObject> _pool = new List<GameObject>();
    BoxCollider _collider;

    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        StartSpawner();
    }

    public void StartSpawner()
    {
        StopCoroutine("Spawner");
        StartCoroutine("Spawner");
    }
    //먼지 생성
    public void StopSpawner()
    {
        StopCoroutine("Spawner");
    }

    IEnumerator Spawner()
    {
        GameObject obj;
        Vector3 randPos;
      
        for (int i = 0; i < poolLimit; i++ )
        {
            randPos = GetRandomPointInCollider();
            obj = Instantiate(toSpawn[Random.Range(0, toSpawn.Length)], randPos, Quaternion.Euler(-90f, 0f, 0f)); //랜덤한 위치에서 랜덤한 오브젝트 생성
            obj.transform.SetParent(this.transform, true);
            _pool.Add(obj);
        }

        yield return null;
    }

    public void RemoveFromPool(GameObject obj) => _pool.Remove(obj);
    public Vector3 GetRandomPointInCollider()
    {
        Bounds bounds = _collider.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
