using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    public Camera subCamera;
    


    public float rotSpeed = 50f;
    bool backRotaionState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(subCamera.gameObject.activeSelf == false)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            if (transform.rotation.y <= 0.95f)
            {
                if (backRotaionState == false)
                {
                    transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
                    if (transform.rotation.y >= 0.95f)
                        backRotaionState = true;
                    Debug.Log(transform.rotation.y);
                }
                else
                {
                    transform.Rotate(new Vector3(0, -(rotSpeed * Time.deltaTime), 0));
                    if (transform.rotation.y <= 0)
                        backRotaionState = false;
                    Debug.Log("흠" + transform.rotation.y);
                }

            }
            else if (transform.rotation.y >= 0.95f && backRotaionState == true)
            {
                //transform.rotation = Quaternion.Euler(0,0,0);
                transform.Rotate(new Vector3(0, -(rotSpeed * Time.deltaTime), 0));
                if (transform.rotation.y <= 0)
                    backRotaionState = false;
                Debug.Log("흠" + transform.rotation.y);
            }
        }
        
    }
}
