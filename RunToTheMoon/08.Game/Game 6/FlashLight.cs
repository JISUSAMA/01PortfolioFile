using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    Vector3 point1 = new Vector3(0, 0, 0);
    Vector3 point2 = new Vector3(100, 0, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(point1, point2, Color.red);
    }
    IEnumerator _Solve_Show()
    {
        yield return null;
    }
}
