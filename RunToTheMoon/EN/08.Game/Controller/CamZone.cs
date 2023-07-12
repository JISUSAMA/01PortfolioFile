using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CamZone : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private CinemachineVirtualCamera virtualCamera = null;

    #endregion

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("???????????");
        virtualCamera.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            virtualCamera.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            virtualCamera.enabled = false;
        }
    }

    private void OnValidate()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    #endregion
}
