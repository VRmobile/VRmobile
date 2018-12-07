using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControllerSample_SingleHand : MonoBehaviour
{

    public Transform target;

    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (VvrController.Recentered() == true)
        {
            target.localRotation = VvrController.Orientation(VvrController.RightHand);
            target.rotation = Quaternion.identity;
        }
        else
        {
            target.rotation = VvrController.Orientation(VvrController.RightHand);
        }
    }
}
