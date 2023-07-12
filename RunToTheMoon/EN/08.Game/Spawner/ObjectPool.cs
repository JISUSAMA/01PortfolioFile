using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public Transform transform;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();


    #region Singleton
    public static ObjectPool Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else Instance = this;
    }
    #endregion

    public GameObject spawnParent;
    
    private void Start()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject ob = Instantiate(pool.prefab, spawnParent.transform);
                ob.SetActive(false);
                objectPool.Enqueue(ob);
            }

            //ob.name = ob.name.Replace("(clone)", "").Trim();
            poolDictionary.Add(pool.tag, objectPool);
        }
        Debug.Log("poolDictionary.Count : " + poolDictionary.Count);    // 1개
        Debug.Log("Stair_1 Count : " + poolDictionary["Stair_1"].Count);    // 30개
    }

    // 생성
    public void SpawnFromPool(string _tag, Vector3 _position, Quaternion _rotation)
    {
        if (!poolDictionary.ContainsKey(_tag))
        {
            Debug.LogWarning("태그가 존재하지 않아요 : " + _tag);
        }

        if (poolDictionary[_tag].Count != 0)
        {
            // 시작부분에서 제거, Count 도 감소
            // pool에 있던 GameObject 를 가져온다.
            GameObject objectPool = poolDictionary[_tag].Dequeue(); 
            objectPool.SetActive(true);
            objectPool.transform.position = _position;
            objectPool.transform.rotation = _rotation;
        }

        // 계단이 하나 사라지면
        //if ()
        //{

        //}
    }

    public GameObject GetObject(string tag)
    {
        // poolDictionary 안에 Value를 얻어옴
        // Key : tag , Value : Queue<GameObject> objectPool 로 저장되어있음.
        if (poolDictionary.TryGetValue(tag, out Queue<GameObject> objectPool))
        {
            // 없으면 생성해서 오브젝트를 가져온다.
            if (objectPool.Count == 0)
                return CreateNewObject(gameObject);
            else
            {
                // 있으면 시작부분을 제거해서 오브젝트를 가져온다.
                GameObject _object = objectPool.Dequeue();
                _object.SetActive(true);
                return _object;
            }
        }
        else
            return CreateNewObject(gameObject); // 둘다 아니라도 오브젝트를 가져온다.

    }

    private GameObject CreateNewObject(GameObject gameObject)
    {
        //GameObject ob = Instantiate(pool.prefab, spawnParent.transform);
        //ob.SetActive(false);
        //objectPool.Enqueue(ob);

        GameObject newGO = Instantiate(gameObject);
        newGO.name = gameObject.name;
        return newGO;
    }

    // 계단이 사라지게(비활성화)되면
    public void ReturnGameObject(string _tag)
    {
        // 찾아지면
        if (poolDictionary.TryGetValue(_tag, out Queue<GameObject> objectPool))
        {
            objectPool.Enqueue(gameObject);
        }
        else
        {
            // 안찾아지면
            Queue<GameObject> newObjectQueue = new Queue<GameObject>();
            newObjectQueue.Enqueue(gameObject);
            poolDictionary.Add(gameObject.name, newObjectQueue);
        }

        gameObject.SetActive(false);

    }
}
