using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using Vvr.Internal;

// Vroom Controller for Unity
// Copyright 2017 WonderLeague Corporation.
// version 2.32

public enum VvrConnectionState
{
	Error = -1,
	Disconnected = 0,
	Disconnecting,
	Scanning,
	Connecting,
	Connected
};

public enum VvrConnectionStyle
{
	RightHandOnly = 1,
	RightHandAndLeftHand,

};

public enum VvrConnectionMode
{
	Auto = 0,
	Manual
};
	
public enum VvrJoystickAction
{
	Neutral = 0,
	UpLeft,
	Up,
	UpRight,
	Left,
	Right,
	DownLeft,
	Down,
	DownRight,
}

public enum VvrVolumeState
{
	TurnDown,
	None,
	TurnUp
}

public class VvrController : MonoBehaviour
{
	static VvrController controllerInstance;
	static VvrControllerManager instance;
	
	public VvrConnectionStyle ConnectionStyle = VvrConnectionStyle.RightHandOnly;
	public VvrConnectionMode ConnectionMode = VvrConnectionMode.Auto;

	public const int RightHand = 0;
	public const int LeftHand = 1;

	void Awake ()
	{
		if (controllerInstance == null) {
			controllerInstance = this;
			DontDestroyOnLoad(this.gameObject);
		} else {
			Destroy(this.gameObject);
		}
		if (instance == null) {
			instance = GetComponent<VvrControllerManager>();
			if (instance != null) {
				instance.connectionMode = ConnectionMode;
				instance.maxConnectableDevices = (int)ConnectionStyle;
			}
		}
	}

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
	}
	
	public static int maxConnectableDevices {
		get {
			return instance != null ? instance.maxConnectableDevices : 1;
		}
	}

	public static int connectionStyle {
		get {
			return instance != null ? instance.maxConnectableDevices : 1;
		}
	}

	public static VvrConnectionMode connectionMode {
		get {
			return instance != null ? instance.connectionMode : VvrConnectionMode.Auto;
		}
	}

	public static bool scanningState {
		get {
			return instance != null ? instance.scanningState : false;
		}
	}

	public static bool IsInitialized {
		get {
			return instance != null ? instance.IsInitialized : false;
		}
	}

	public static void StartScan () {
		if (instance != null) {	instance.StartScan (); }
	}

	public static void StopScan () {
		if (instance != null) { instance.StopScan(); }
	}

	public static void GetScanResult (out int size, out string [][] peripherals) {
		if (instance == null) {
			size = 0;
			peripherals = null;
		}
		instance.GetScanResult(out size, out peripherals);
	}

	public static bool ConnectRequest(int hand = RightHand, string peripheralID = null)
	{
		if (instance == null)
			return false;
		instance.ConnectRequest (hand, peripheralID);
		return true;
	}

	public static bool DisconnectRequest(int hand = RightHand)
	{
		if (instance == null)
			return false;
		instance.DisconnectRequest (hand);
		return true;
	}

	public static VvrConnectionState ConnectionState(int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].connectionState : VvrConnectionState.Disconnected;
	}

	public static bool IsConnected(int hand = RightHand) {
		if (instance == null)
			return false;
		
		if (instance.controllerState[hand].connectionState != VvrConnectionState.Connected)
			return false;
		
		return true;
	}

	/* Peripheral ID (Android:Hardware Address, iOS:UUID) */
	public static string PeripheralID(int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].peripheralID : string.Empty; 
	}

	/* (Advertisement) Shortened Device Name */
	public static string ShortenedDeviceName(int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].shortenedDeviceName : string.Empty; 		
	}

	/* (Advertisement) RSSI */
	public static string RSSI(int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].rssi : string.Empty; 		
	}

	/* (General Access Profile) Device Name */
	public static string DeviceName(int hand = RightHand)
	{
		if (instance == null) return string.Empty;
		string deviceName = instance.controllerState[hand].deviceName;
		if (deviceName == string.Empty)
			deviceName = instance.controllerState[hand].shortenedDeviceName;
		return deviceName;
	}
		
	/* Orientation (Quaternion) */
	public static Quaternion Orientation (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].orientation : Quaternion.identity;
	}

	/* Gyroscope */
	public static Vector3 Gyro (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].gyro : Vector3.zero;
	}

	/* Acceleration */
	public static Vector3 Accel (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].accel : Vector3.zero;
	}

	/* Joystick: Position */
	public static Vector2 Joystick (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].joystick.Position : Vector2.zero;
	}

	/* Joystick: Action */
	public static VvrJoystickAction JoystickAction (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].joystick.Action : VvrJoystickAction.Neutral;
	}

	/* Joystick: Center Button State */
	public static bool ClickButton (int hand = RightHand)
	{
		return instance != null ? instance.controllerState [hand].clickButton.ButtonState : false;
	}

	/* Joystick: Center Button Down */
	public static bool ClickButtonDown (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].clickButton.ButtonDown : false;
	}

	/* Joystick: Center Button Up */
	public static bool ClickButtonUp (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].clickButton.ButtonUp : false;
	}

	/* App Button: State */
	public static bool AppButton (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].appButton.ButtonState : false;
	}

	/* App Button: Down Event */
	public static bool AppButtonDown (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].appButton.ButtonDown : false;
	}

	/* App Button: Up Event */
	public static bool AppButtonUp (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].appButton.ButtonUp : false;
	}

	/* Home Button: State */
	public static bool HomeButton (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].homeButton.ButtonState : false;
	}

	/* Home Button: State */
	public static bool HomeButtonState (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].homeButton.ButtonState : false;
	}

	/* Home Button: Down Event */
	public static bool HomeButtonDown (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].homeButton.ButtonDown : false;
	}

	/* Home Button: Up Event */
	public static bool HomeButtonUp (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].homeButton.ButtonUp : false;
	}

	/* Trigger: State */
	public static bool Trigger (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].trigger.ButtonState : false;
	}

	/* Trigger: Down Event */
		public static bool TriggerDown (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].trigger.ButtonDown : false;
	}

	/* Trigger: Up Event */
	public static bool TriggerUp (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].trigger.ButtonUp : false;
	}

	/* Recenter State: Recentering */
	public static bool Recentering (int hand = RightHand) 
	{
		return instance != null ? instance.controllerState[hand].recentering : false;
	}

	/* Recenter State: Recentered */
	public static bool Recentered (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].recentered : false;
	}

	/* Volume State */
	public static VvrVolumeState Volume (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].volumeState.volumeState : VvrVolumeState.None;
	}

	public static VvrVolumeState VolumeState (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].volumeState.volumeState : VvrVolumeState.None;
	}

	public static bool VolumeUp (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].volumeState.volumeUp.ButtonState : false;
	}

	public static bool VolumeDown (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].volumeState.volumeDown.ButtonState : false;
	}
		
	/* Battery Service: Battery Level */
	public static int BatteryLevel (int hand = 0)
	{
		return instance != null ? instance.controllerState[hand].batteryLevel : -1;
	}

	/* Device Information Service: Manufacturer Name String */
	public static string ManufacturerName(int hand = 0)
	{
		return instance != null ? instance.controllerState[hand].manufacturerName : "Unknown";
	}		

	/* Device Information Service: Model Number String */
		public static string ModelNumber (int hand = 0)
	{
		return instance != null ? instance.controllerState[hand].modelNumber : "Unknown";
	}

	/* Device Information Service: Serial Number String */
	public static string SerialNumber (int hand = 0)
	{
		return instance != null ? instance.controllerState[hand].serialNumber : "Unknown";
	}

	/* Device Information Service: Software Revision String */
	public static string HardwareRevision (int hand = 0)
	{
		return instance != null ? instance.controllerState[hand].hardwareRevision : "Unknown";
	}

	/* Device Information Service: Firmware Revision String */
	public static string FirmwareRevision (int hand = 0)
	{
		return instance != null ? instance.controllerState[hand].firmwareRevision : "Unknown";
	}
		
	/* Device Information Service: Software Revision String */
	public static string SoftwareRevision (int hand = 0)
	{
		return instance != null ? instance.controllerState[hand].softwareRevision : "Unknown";
	}

	/* Touch Pad: Touch State (Emulation) */
	public static bool IsTouching (int hand = RightHand)
	{
		return instance != null ? instance.controllerState[hand].clickButton.ButtonState : false;
	}

	/* Touch Pad: Touch Down (Emulation) */
	public static bool TouchDown (int hand = 0)
	{
		return instance != null ? instance.controllerState[hand].clickButton.ButtonDown : false;
	}

	/* Touch Pad: Touch Up (Emulation) */
	public static bool TouchUp (int hand = 0)
	{
		return instance != null ? instance.controllerState[hand].clickButton.ButtonUp : false;
	}

	/* Touch Pad: Position (Emulation) */
	public static Vector2 TouchPos (int hand = 0)
	{
		return instance != null ? instance.controllerState[hand].joystick.Position : Vector2.zero;
	}

	/* 4-Directional Pad: State (Emulation) */
	/* 4-Directional Pad: Action (Emulation) */
	/* 4-Directional Pad: Up (Emulation) */
	/* 4-Directional Pad: Down (Emulation) */
	/* 4-Directional Pad: Left (Emulation) */
	/* 4-Directional Pad: Right (Emulation) */
}
