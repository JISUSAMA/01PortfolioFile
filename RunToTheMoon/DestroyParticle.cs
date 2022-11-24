using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    private void Awake()
    {
        Destroy(this.gameObject, 0.3f);
    }

}
