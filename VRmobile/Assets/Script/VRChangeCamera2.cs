using System.Collections;
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

    /*
    /// <summary>
    /// VRデバイス名。
    /// </summary>
    static string DeviceCardboard = "Cardboard";
    static string DeviceNone = "None";

    /// <summary>
    /// Update.
    /// </summary>
    void Update()
    {
        // 画面タッチで切り替え
        if (Input.GetMouseButtonDown(0))
        {
            ToggleXR();
        }
    }

    /// <summary>
    /// XR切り替え。
    /// </summary>
    void ToggleXR()
    {
        if (XRSettings.loadedDeviceName.ToLower() == VRChangeCamera2.DeviceCardboard.ToLower())
        {
            StartCoroutine(LoadDevice(VRChangeCamera2.DeviceNone));
        }
        else
        {
            StartCoroutine(LoadDevice(VRChangeCamera2.DeviceCardboard));
        }
    }

    /// <summary>
    /// VRデバイスのロード。
    /// </summary>
    IEnumerator LoadDevice(string newDevice)
    {
        bool vrMode = newDevice != VRChangeCamera2.DeviceNone;

        // VRの際にはここで切り替え
        if (vrMode)
            Screen.orientation = ScreenOrientation.LandscapeLeft;

        XRSettings.LoadDeviceByName(newDevice);
        yield return null;

        XRSettings.enabled = vrMode;

        // ノーマルの際はここで切り替え
        if (!vrMode)
            Screen.orientation = ScreenOrientation.Portrait;
    }*/
}