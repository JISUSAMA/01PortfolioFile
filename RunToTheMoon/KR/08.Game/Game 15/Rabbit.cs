using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{

    public GameObject RabbitTrack; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("RabbitSetOff"))
        {
            Debug.LogError("토끼사라지나????????????????");
            RabbitTrack.SetActive(false);
        }
    }
}
