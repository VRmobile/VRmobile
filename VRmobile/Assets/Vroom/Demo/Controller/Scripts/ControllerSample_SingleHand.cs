using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControllerSample_SingleHand : MonoBehaviour
{

	public Transform target;
	public Text quatText;

	public Text joystickText;
	public Text clickText;
	public Text padText;
	public Text appBtnText;
	public Text homeBtnText;
	public Text gyroText;
	public Text accelText;
	public Text triggerText;
	public Text volumeText;

	public Text deviceStatusText;

	public Text batteryLevelText;
	public Text deviceInformationText;

	public GameObject connectBtn;


	// Use this for initialization
	void Start ()
	{
		Application.targetFrameRate = 60;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (VvrController.Recentered() == true) {
			target.localRotation = VvrController.Orientation (VvrController.RightHand);
			//target.rotation = Quaternion.identity;
		} else {
			target.rotation = VvrController.Orientation (VvrController.RightHand);
		}
		//quatText.text = VvrController.Orientation ().ToString ();
		quatText.text = "(" +
			VvrController.Orientation ().x.ToString ("f3") + ", " +
			VvrController.Orientation ().y.ToString ("f3") + ", " +
			VvrController.Orientation ().z.ToString ("f3") + ", " +
			VvrController.Orientation ().w.ToString ("f3") + ")";

		Vector3 gyro = VvrController.Gyro ();
		gyroText.text = "Gyro: (" +
			gyro.x.ToString ("f2") + ", " +
			gyro.y.ToString ("f2") + ", " +
			gyro.z.ToString ("f2") + ")";

		Vector3 accel = VvrController.Accel ();
		accelText.text = "Accel: (" +
			accel.x.ToString ("f2") + ", " +
			accel.y.ToString ("f2") + ", " +
			accel.z.ToString ("f2") + ")";

		joystickText.text = "Joystick: (" +
			VvrController.Joystick ().x.ToString ("f2") + ", " +
			VvrController.Joystick ().y.ToString ("f2") + ")";

		padText.text = "Action: " + VvrController.JoystickAction().ToString();

		appBtnText.text = "App: " + VvrController.AppButton ().ToString (); 

		string recenter = null;
		if (VvrController.Recentering ()) {
			recenter = "(Recentering)";
		} else if (VvrController.Recentered ()) {
			recenter = "(Recentered)";
		}
		homeBtnText.text = "Home: " + VvrController.HomeButton ().ToString () + " " + recenter;

		clickText.text = "Click: " + VvrController.ClickButton().ToString();

		triggerText.text = "Trigger: " + VvrController.Trigger ().ToString ();

		volumeText.text = "Volume: " + VvrController.Volume ().ToString ();

		deviceStatusText.text = "DeviceStatus: " + 
			VvrController.ConnectionState ().ToString ();

		batteryLevelText.text = "Battery: " + VvrController.BatteryLevel ().ToString ();

		deviceInformationText.text = "Manufacturer:[" + VvrController.ManufacturerName () + "]\n" +
		"Model:[" + VvrController.ModelNumber () + "] Serial:[" + VvrController.SerialNumber () + "]\n" +
		"HW Rev.:[" + VvrController.HardwareRevision () + "] SW Rev.:[" + VvrController.SoftwareRevision () +
		"] FW Rev.:[" + VvrController.FirmwareRevision () + "]";
	}
}
