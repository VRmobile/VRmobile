using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
public class VRChangeCamera : MonoBehaviour
{
    [SerializeField]
    private bool cameraFlg = true;           //trueなら複眼表示、falseなら単眼表示

    void Start()
    {
        StartCoroutine(LoadCardBoard());
    }

    IEnumerator LoadCardBoard()
    {
        UnityEngine.XR.XRSettings.LoadDeviceByName("Cardboard");
        yield return null;
        if (cameraFlg)
        {
            UnityEngine.XR.XRSettings.enabled = true;
        }
        else
        {
            UnityEngine.XR.XRSettings.enabled = false;
        }
    }
}
