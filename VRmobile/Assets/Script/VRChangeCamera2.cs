﻿using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class VRChangeCamera2 : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadCardBoard());
    }

    IEnumerator LoadCardBoard()
    {
        XRSettings.LoadDeviceByName("Cardboard");
        yield return null;
        XRSettings.enabled = true;
    }

}