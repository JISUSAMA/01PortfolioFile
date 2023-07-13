using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniManager2 : MonoBehaviour
{
    public Animator animator;
    public Animator tireAnimator;
    public Animator cameraAni;
    RaycastHit hit;
    Vector3 theRay;
    public LayerMask terainMask;
    public GameObject player;
    public GameObject playerPos;

    public Camera mainCamera;
    public Camera subCamera;

    public float rotSpeed = 50f;
    bool backRotaionState;


    void Start()
    {
        
    }

    private void Update()
    {
        Align();

        //if (transform.rotation.y <= 0.95f)
        //{
        //    if (backRotaionState == false)
        //    {
        //        transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
        //        if (transform.rotation.y >= 0.95f)
        //            backRotaionState = true;
        //        Debug.Log(transform.rotation.y);
        //    }
        //    else
        //    {
        //        transform.Rotate(new Vector3(0, -(rotSpeed * Time.deltaTime), 0));
        //        if (transform.rotation.y <= 0)
        //            backRotaionState = false;
        //        Debug.Log("흠" + transform.rotation.y);
        //    }

        //}
        //else if (transform.rotation.y >= 0.95f && backRotaionState == true)
        //{
        //    //transform.rotation = Quaternion.Euler(0,0,0);
        //    transform.Rotate(new Vector3(0, -(rotSpeed * Time.deltaTime), 0));
        //    if (transform.rotation.y <= 0)
        //        backRotaionState = false;
        //    Debug.Log("흠" + transform.rotation.y);
        //}
    }

    private void Align()
    {
        theRay = -transform.up;
        Debug.DrawRay(new Vector3(playerPos.transform.position.x, playerPos.transform.position.y, playerPos.transform.position.z), theRay * 3, Color.red);
        if (Physics.Raycast(new Vector3(playerPos.transform.position.x, playerPos.transform.position.y, playerPos.transform.position.z),
            theRay, out hit, 3, terainMask))
        {
            GameObject hitOB = hit.collider.gameObject;
            Quaternion targetRotationT = Quaternion.Inverse(hitOB.transform.rotation);

            Quaternion targetRotation = Quaternion.FromToRotation(player.transform.up, hit.normal) * player.transform.parent.rotation;

            //Quaternion targetRotation = Quaternion.FromToRotation(Vector3.down, hit.normal) * transform.rotation;
            Debug.Log("targetRotation" + targetRotation);
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, targetRotation, Time.deltaTime);
            // transform.rotation = targetRotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Left"))
        {
            cameraAni.Rebind();
            Debug.Log("Left");
            animator.SetBool("Left", true);
            animator.SetBool("R_Left", false);

            tireAnimator.SetBool("Left", true);
            tireAnimator.SetBool("R_Left", false);

            CameraActive(false, true);
            cameraAni.SetBool("Right", true);
        }
        else if(other.CompareTag("R_Left"))
        {
            Debug.Log("R_Left");
            animator.SetBool("Left", false);
            animator.SetBool("R_Left", true);

            tireAnimator.SetBool("Left", false);
            tireAnimator.SetBool("R_Left", true);

            CameraActive(true, false);
            cameraAni.SetBool("Right", false);
        }
        else if(other.CompareTag("Right"))
        {
            cameraAni.Rebind();
            Debug.Log("Right");
            animator.SetBool("Right", true);
            animator.SetBool("R_Right", false);

            tireAnimator.SetBool("Right", true);
            tireAnimator.SetBool("R_Right", false);

            CameraActive(false, true);
            cameraAni.SetBool("Left", true);
        }
        else if(other.CompareTag("R_Right"))
        {
            Debug.Log("R_Right");
            animator.SetBool("Right", false);
            animator.SetBool("R_Right", true);

            tireAnimator.SetBool("Right", false);
            tireAnimator.SetBool("R_Right", true);

            CameraActive(true, false);
            cameraAni.SetBool("Left", false);
        }
    }

    void CameraActive(bool _main, bool _sub)
    {
        mainCamera.enabled = _main;
        subCamera.enabled = _sub;
        //mainCamera.gameObject.SetActive(_main);
        //subCamera.gameObject.SetActive(_sub);
    }

    


    private void OnTriggerExit(Collider other)
    { 
    }
}
