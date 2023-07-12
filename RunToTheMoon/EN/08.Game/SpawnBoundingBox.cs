using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

//[System.Serializable]
//public class SpawnEdit
//{
//    public AssetReference assRef;
//    public int spawn_amount;
//    public SpawnEdit() { /*초기화*/ }
//}

public class SpawnBoundingBox : MonoBehaviour
{
    [SerializeField] GameObject parentObj;
    [SerializeField] AssetReference[] assRef;
    GameObject go;
    Bounds bounds;

    void Start()
    {
        bounds = GetComponent<BoxCollider>().bounds;
        //assRef.InstantiateAsync(parentObj.transform);
        Snack_RandPosSpawn();
    }

    void OnDisable()
    {
        ReleaseDestroy();
    }

    Vector3 GetRandPos()
    {
        float offsetX = Random.Range(bounds.min.x, bounds.max.x);
        //float offsetY = Random.Range(-bounds.extents.y, bounds.extents.y);
        float offsetZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(offsetX,0.5f, offsetZ);
    }

    void Snack_RandPosSpawn()
    {
        for (int i = 0; i < assRef.Length; i++)
        {
            assRef[i].InstantiateAsync(GetRandPos(), Quaternion.Euler(new Vector3(-75,0,-90)) ,parentObj.transform).Completed +=
            (AsyncOperationHandle<GameObject> obj) =>
            {
                go = obj.Result;
                go.SetActive(true);
            };
        }
    }

    void ReleaseDestroy()
    {
        for (int i = 0; i < assRef.Length; i++)
        {
            assRef[i].ReleaseInstance(go);
        }
    }

}
