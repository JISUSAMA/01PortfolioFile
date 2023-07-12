using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [Header("17Scene_Door")]
    public Animator Door_ani; //문 열리는 애니메이션

    public void OnTriggerEnter(Collider other)
    {
        GameObject ob = other.gameObject;
        Debug.Log(ob.name);
        if (ob.name.Equals("Player"))
        {
            if (this.gameObject.name.Equals("OpenDoor"))
            {
                Door_ani.SetTrigger("BasicDoor");
            }
            else if (this.gameObject.name.Equals("PinkOpenDoor"))
            {
                Door_ani.SetTrigger("PinkDoor");
            }
            else if (this.gameObject.name.Equals("GateOpenDoor"))
            {
                if (Door_ani.name.Equals("LeftDoor"))
                {
                    Door_ani.SetTrigger("LeftDoor");
                }
                else if (Door_ani.name.Equals("RightDoor"))
                {
                    Door_ani.SetTrigger("RightDoor");
                }

            }
        }

    }
}
