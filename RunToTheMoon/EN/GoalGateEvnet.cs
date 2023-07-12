using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalGateEvnet : MonoBehaviour
{
    public GameObject SpaceStation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            SpaceStation.SetActive(true);
        }
            
    }
}
