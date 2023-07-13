using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SelfSetNativeSize : MonoBehaviour {

    private enum LocaleType {
        English,
        Korean,
    }

    [SerializeField] private Vector3 enVector3;
    [SerializeField] private Vector3 koVector3;
    
    private void OnEnable() {
        SelfSetSize();
    }

    private void SetTransformLocalPosition() {
        if (PlayerPrefs.GetInt("LocaleKey") == (int)LocaleType.English) {
            transform.localPosition = enVector3;
        } else {
            transform.localPosition = koVector3;            
        }
    }

    public void SelfSetSize() {
        // changing original size 
        if (transform.TryGetComponent(out Image image)) {
            image.SetNativeSize();
        }
        // changing transform position by locale
        SetTransformLocalPosition();
    }
}