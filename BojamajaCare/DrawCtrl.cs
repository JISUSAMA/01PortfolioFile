using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawCtrl : MonoBehaviour
{
    public GameObject[] dot; //1.dot 2.mid 3.end
    public bool checkPoint1, checkPoint2 = false;

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("MidPoint")){
            checkPoint1 = true; 
        }
        if (collision.gameObject.name.Equals("EndPoint"))
        { 
            if(checkPoint1)checkPoint2 = true; 
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.tag.Equals("Shape"))
        {
            BrainGame4_UIManager.instance.DrawLine_GameRun = false;
        }
     
    }
}
