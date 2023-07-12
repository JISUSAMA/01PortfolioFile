using GPUInstancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsGenerator : MonoBehaviour
{
    [Range(0, 200000)]
    public int count = 50000;

    public List<GPUInstancerPrefab> asteroidObjects = new List<GPUInstancerPrefab>();
    public GPUInstancerPrefabManager prefabManager;
    public Transform centerTransform;

    private List<GPUInstancerPrefab> asteroidInstances = new List<GPUInstancerPrefab>();
    private int instantiatedCount;
    private Vector3 center;
    private Vector3 allocatedPos;
    private Quaternion allocatedRot;
    private Vector3 allocatedLocalEulerRot;
    private Vector3 allocatedLocalScale;
    private GPUInstancerPrefab allocatedGO;
    private GameObject goParent;
    private float allocatedLocalScaleFactor;
    private int columnSize;
    private int columnSpace = 3;

    //List<GameObject> _pool = new List<GameObject>();
    BoxCollider _collider;
    void Awake()
    {
        instantiatedCount = 0;

        _collider = GetComponent<BoxCollider>();

        center = centerTransform.position;

        allocatedPos = Vector3.zero;
        allocatedRot = Quaternion.identity;
        allocatedLocalEulerRot = Vector3.zero;
        allocatedLocalScale = Vector3.one;
        allocatedLocalScaleFactor = 1f;

        // 부모 설정
        goParent = new GameObject("Astroids");
        goParent.transform.position = center;
        goParent.transform.parent = gameObject.transform;

        columnSize = count < 5000 ? 1 : count / 2500;

        Debug.Log("columnSize : " + columnSize);

        int firstPassColumnSize = count % columnSize > 0 ? columnSize - 1 : columnSize;

        asteroidInstances.Clear();

        Debug.Log("firstPassColumnSize : " + firstPassColumnSize);
        
        for (int h = 0; h < firstPassColumnSize; h++)
        {
            for (int i = 0; i < Mathf.FloorToInt((float)count / columnSize); i++)
            {
                //asteroidInstances.Add(InstantiateInBound(center, h));
                asteroidInstances.Add(InstantiateInBound(center));
            }
        }

        if (firstPassColumnSize != columnSize)
        {
            for (int i = 0; i < count - (Mathf.FloorToInt((float)count / columnSize) * firstPassColumnSize); i++)
            {
                //asteroidInstances.Add(InstantiateInBound(center, columnSize));
                asteroidInstances.Add(InstantiateInBound(center));
            }
        }
       // Debug.Log("Instantiated " + instantiatedCount + " objects.");
    }
    private void Start()
    {
        if (prefabManager != null && prefabManager.gameObject.activeSelf && prefabManager.enabled)
        {
            GPUInstancerAPI.RegisterPrefabInstanceList(prefabManager, asteroidInstances);
            GPUInstancerAPI.InitializeGPUInstancer(prefabManager);
        }

        _collider.enabled = false;
    }

    //private void SetRandomPosInCircle(Vector3 center, int column, float radius)
    //{
    //    float ang = Random.value * 360;

    //    allocatedPos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
    //    allocatedPos.y = center.y - (column * (float)columnSpace / 2) + (column * columnSpace) + Random.Range(0f, 1f);
    //    allocatedPos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
    //}

    private GPUInstancerPrefab InstantiateInBound(Vector3 center)
    {
        // 초기 생성 위치 방향 끝
        allocatedPos = GetRandomPointInCollider();
        allocatedRot = Quaternion.FromToRotation(Vector3.forward, center - allocatedPos);
        allocatedGO = Instantiate(asteroidObjects[Random.Range(0, asteroidObjects.Count)], allocatedPos, allocatedRot);
        allocatedGO.transform.parent = goParent.transform;

        // 각각의 방향 랜덤
        allocatedLocalEulerRot.x = Random.Range(-180f, 180f);
        allocatedLocalEulerRot.y = Random.Range(-180f, 180f);
        allocatedLocalEulerRot.z = Random.Range(-180f, 180f);
        allocatedGO.transform.localRotation = Quaternion.Euler(allocatedLocalEulerRot);

        // 스케일 랜덤
        allocatedLocalScaleFactor = Random.Range(10f, 20f);
        allocatedLocalScale.x = allocatedLocalScaleFactor;
        allocatedLocalScale.y = allocatedLocalScaleFactor;
        allocatedLocalScale.z = allocatedLocalScaleFactor;
        allocatedGO.transform.localScale = allocatedLocalScale;

        instantiatedCount++;

        return allocatedGO;
    }

    //public void RemoveFromPool(GameObject obj) => _pool.Remove(obj);
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
