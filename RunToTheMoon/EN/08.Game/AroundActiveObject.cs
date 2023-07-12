using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundActiveObject : MonoBehaviour
{
    public GameObject[] EventObject;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")) 
        {
            GoAroundOnce();      
        }

    }
    void GoAroundOnce()
    {
        for(int i =0; i< EventObject.Length; i++)
        {
            EventObject[i].SetActive(true);
        }
    }
}
