using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRChangeCamera : MonoBehaviour {

    void Start() {
        StartCoroutine(LoadCardBoard());
    }

    IEnumerator LoadCardBoard() {
        XRSettings.LoadDeviceByName("Cardboard");
        yield return null;
        XRSettings.enabled = false;
    }
}
