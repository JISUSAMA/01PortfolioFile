using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiceRoation : MonoBehaviour
{
    public GameObject ParticleOB;
    public GameObject BaseFx;
    public GameObject BaseFx_Flare;
    public GameObject ObjectFx;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("rail"))
        {
            StartCoroutine(_Start_DropPice());
        }
        if (collision.gameObject.name.Equals("Player"))
        {
            Destroy(this.gameObject);
        }
    }
    private void Awake()
    {
        BaseFx.SetActive(true);
    }
    IEnumerator _y_rotation()
    {
        while (true)
        {
            transform.Rotate(new Vector3(0f, 30f, 0f) * Time.deltaTime, Space.World);
            yield return null;
        }
    }
    IEnumerator _Start_DropPice()
    {
        BaseFx_Flare.SetActive(true); //폭죽 터짐
        yield return new WaitForSeconds(0.3f);
        ObjectFx.SetActive(true);
        StartCoroutine(_y_rotation());
        yield return null;
    }
}
