using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    Quaternion start_gyro;
    void Start()
    {
        Input.gyro.enabled = true;
        start_gyro = StartCameraController.ini_gyro;
    }
    void Update()
    {
        if (Input.gyro.enabled)
        {
            Quaternion gyro = Input.gyro.attitude;
            this.transform.localRotation = Quaternion.Euler(0, -start_gyro.y, 0) * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
        }
    }
}