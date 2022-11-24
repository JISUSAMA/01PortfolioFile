using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DementiaGame7_DrawCtrl : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Line"))
        {
            DementiaGame7_DataManager.instance.playBool = false;
        }
        Debug.Log(collision.gameObject.name);
    }
}
