using UnityEngine;
using System.Collections;
//OnGUIの部分は今回は削除
public class CameraController : MonoBehaviour
{
    private GUIStyle labelStyle;
    Quaternion start_gyro;
    Quaternion gyro;

    void Start()
    {

    }

    void Update()
    {
        Input.gyro.enabled = true;
        if (Input.gyro.enabled)
        {
            gyro = Input.gyro.attitude;
            gyro = Quaternion.Euler(-180, 0, 0) * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
            this.transform.localRotation = Quaternion.Inverse(StartCameraController.ini_gyro) * gyro;
        }
    }
}