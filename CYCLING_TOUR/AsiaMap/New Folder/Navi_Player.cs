using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class Navi_Player : MonoBehaviour
{
    GameObject obj;
    NavMeshAgent nav;


    void Start()
    {
        obj = GameObject.FindGameObjectWithTag("End");
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(obj.transform.position);
    }
}
