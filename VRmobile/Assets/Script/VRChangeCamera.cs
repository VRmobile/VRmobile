﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
public class VRChangeCamera : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadCardBoard());
    }

    IEnumerator LoadCardBoard()
    {
        UnityEngine.XR.XRSettings.LoadDeviceByName("Cardboard");
        yield return null;
        UnityEngine.XR.XRSettings.enabled = false;

    }
}
