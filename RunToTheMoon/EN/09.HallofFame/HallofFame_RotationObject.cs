using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallofFame_RotationObject : MonoBehaviour
{

    Vector3 pos; //현재위치
    float delta = 0.01f; // 좌(우)로 이동가능한 (x)최대값
    float speed = 2.0f; // 이동속도
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.name.Equals("BallMove"))
        {
            pos = transform.position;
            StartCoroutine(_Move_up_down());
        }
        else if (this.gameObject.name.Equals("StonRotation"))
        {
            StartCoroutine(_y_rotation());
        }
        else if (this.gameObject.name.Equals("Fountain"))
        {
            pos = this.gameObject.transform.position;
            speed = Random.Range(0.5f, 0.6f);
            delta = Random.Range(0.01f, 0.2f);
            StartCoroutine(_Move_up_down());
        }
        else if (this.gameObject.name.Equals("Constellation"))
        {
            pos = this.gameObject.transform.position;
            speed = Random.Range(0.1f, 0.2f);
            delta = Random.Range(1f, 2f);
            StartCoroutine(_Move_up_down());
        }
        else if (this.gameObject.name.Equals("Item"))
        {
            pos = this.gameObject.transform.position;
            speed = 1.5f;
            delta = 1f;
            StartCoroutine(_Move_up_down());
        }
        else if (this.gameObject.name.Equals("WishSton"))
        {
            pos = this.gameObject.transform.position;
            speed = 2f;
            delta = 0.05f;
            StartCoroutine(_Move_up_down());
        }
        else if (this.gameObject.name.Equals("Sky_island"))
        {
            pos = this.gameObject.transform.position;
            speed = Random.Range(0.1f, 0.2f);
            delta = Random.Range(1f, 2f);
            StartCoroutine(_Move_up_down());
        }
    }
    private void OnDisable()
    {
        StopCoroutine(_Move_up_down());
        StopCoroutine(_y_rotation());

    }
    IEnumerator _y_rotation()
    {
        while (true)
        {
            transform.Rotate(new Vector3(0f, 30f, 0f) * Time.deltaTime, Space.World);
            yield return null;
        }
    }
    IEnumerator _Move_up_down()
    {
        while (true)
        {
         //   Debug.LogError(pos);
            Vector3 v = pos;
            v.y += delta * Mathf.Sin(Time.time * speed);
            transform.position = v;
            yield return null;
        }
        yield return null;
    }
}
