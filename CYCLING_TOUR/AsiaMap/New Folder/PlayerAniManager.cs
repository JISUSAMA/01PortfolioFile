using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class PlayerAniManager : MonoBehaviour
{
    public Animator animator;
    public float Anispeed = 1;
    RaycastHit hit;
    Vector3 theRay;

    public LayerMask terainMask;

    private void Update()
    {
        Align();

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("R_return") 
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            animator.SetBool("R_return_b", false);
        }
    }
    private void Align()
    {
        theRay = -transform.up;
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), theRay * 3, Color.red);
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z),
            theRay, out hit, 3, terainMask))
        {
            GameObject hitOB = hit.collider.gameObject;
            Quaternion targetRotationT = Quaternion.Inverse(hitOB.transform.rotation);

            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.parent.rotation;

            //Quaternion targetRotation = Quaternion.FromToRotation(Vector3.down, hit.normal) * transform.rotation;
            Debug.Log("targetRotation" + targetRotation);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);
            // transform.rotation = targetRotation;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        if (col.name.Equals("R_right"))
        {
            Debug.Log("Right");
            // animator.SetFloat("right",Anispeed);
            animator.SetTrigger("R_right_t");
            animator.SetBool("R_return_b",false);
        }
        else if (col.name.Equals("R_return"))
        {
            Debug.Log("return");
            //animator.SetFloat("return",Anispeed);
            animator.SetTrigger("R_return_t");
            animator.SetBool("R_return_b", true);
        }
        /////////////////////////////////////////////////////////
        if (col.name.Equals("L_left"))
        {
            Debug.Log("L_Left");
            // animator.SetFloat("right",Anispeed);
            animator.SetTrigger("L_left_t");
        }
        else if (col.name.Equals("L_return"))
        {
            Debug.Log("L_return");
            //animator.SetFloat("return",Anispeed);
            animator.SetTrigger("L_return_t");
        }
    }
}
