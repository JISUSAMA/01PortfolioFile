using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man_BicycleBakerCtrl : MonoBehaviour
{
    public static Man_BicycleBakerCtrl instance { get; private set; }


    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;

        DontDestroyOnLoad(this.gameObject);

    }
}
