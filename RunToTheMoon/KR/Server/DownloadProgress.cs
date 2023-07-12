using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadProgress : MonoBehaviour
{
    public float downloadProgressInput;
    public float downloadSliderProgressInput;
    private float cachedDownloadProgressInput;
    public float downloadProgressOutput;
    // Start is called before the first frame update
    void Start()
    {
        downloadProgressOutput = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cachedDownloadProgressInput != downloadProgressInput)
        {
            downloadProgressOutput = downloadProgressInput;
            cachedDownloadProgressInput = downloadProgressInput;
        }
    }
}
