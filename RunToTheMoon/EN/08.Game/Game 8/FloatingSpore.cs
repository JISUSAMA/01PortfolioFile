using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSpore : MonoBehaviour
{
    public float flySpeed = 15f;
    public float floatSpeed = 3f;
    public float floatAmplitude = 0.01f;
    Rigidbody _rigidbody;
    Collider _collider;
    FloatingSporeSpawn _spawner;
    Vector3 _scale;
    Vector3 _destination;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _spawner = transform.parent.GetComponent<FloatingSporeSpawn>();
            StartCoroutine(Fly());
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += new Vector3(0f, Mathf.Sin(Time.timeSinceLevelLoad * floatSpeed) * floatAmplitude, 0f);
        transform.Rotate(new Vector3(0f, 30f, 0f) * Time.deltaTime, Space.World);
    }
    IEnumerator Fly()
    {
        _destination = _spawner.GetRandomPointInCollider();

        Vector3 dir = _destination - transform.position; // get dir vector
        //dir = new Vector3(dir.x, 0f, dir.z); // remove y
        //Quaternion targetDir = Quaternion.LookRotation(dir, Vector3.up); // actual rotation (now Z faces the direction, however our model's forward axis is X)
        //targetDir = Quaternion.AngleAxis(-90f, Vector3.up) * targetDir; // our dust model's axis is not correct, we rotate so that X axis faces destination

        while (Vector3.Distance(transform.position, _destination) > 1f)
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, targetDir, Time.deltaTime);
            _rigidbody.AddForce((_destination - transform.position).normalized * flySpeed, ForceMode.Force);

            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Idle());
    }

    IEnumerator Idle()
    {
        float duration = Random.Range(1f, 2f);

        while (duration > 0f)
        {
            duration -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Fly());
    }
}
