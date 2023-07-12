using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiceEvnet : MonoBehaviour
{
    [Header("Pice")]
    public GameObject ParticleOB;
    public GameObject BaseFx;
    public GameObject BaseFx_Flare;
    public GameObject ObjectFx;

    private void OnEnable()
    {
        StartCoroutine(_Start_DropPice());
    }
    IEnumerator _Start_DropPice()
    {
        ParticleOB.transform.parent = null; //부모랑 분리
        ParticleOB.SetActive(true);
        yield return new WaitForSeconds(1f);
        BaseFx_Flare.SetActive(true); //폭죽 터짐
        yield return new WaitForSeconds(0.3f);
        ObjectFx.SetActive(true);
        // StartCoroutine(_y_rotation());
        yield return null;
    }
}
