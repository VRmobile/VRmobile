using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class VRChangeCamera : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(LoadCardBoard());
    }

    IEnumerator LoadCardBoard()
    {
        UnityEngine.XR.XRSettings.LoadDeviceByName("cardboard");
        yield return null;
        UnityEngine.XR.XRSettings.enabled = false;
    }
}
