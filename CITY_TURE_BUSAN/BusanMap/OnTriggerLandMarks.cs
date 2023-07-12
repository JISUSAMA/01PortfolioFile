using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnTriggerLandMarks : MonoBehaviour {
    private const string PLAYER = "Player";
    private const string DISABLE = "Disable";
    

    private void OnTriggerEnter(Collider other) {
        if (transform.name == DISABLE) {
            IntroducingLandMarks.Instance.Hide();
        } else if(other.gameObject.layer == LayerMask.NameToLayer(PLAYER)) {
            IntroducingLandMarks.Instance.Show(transform.name);
        }
    }
}