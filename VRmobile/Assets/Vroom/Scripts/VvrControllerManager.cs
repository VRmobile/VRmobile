using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

// Vroom Controller for Unity
// Copyright 2017 WonderLeague Corporation.
// version 2.32

namespace Vvr.Internal
{
	public class VvrControllerManager : MonoBehaviour
	{
#if UNITY_ANDROID
		static AndroidJavaObject m_plugin = null;
#endif

#if UNITY_IOS
    	[DllImport ("__Internal")]
		private static extern void vroomBluetoothLELog (string message);

		[DllImport ("__Internal")]
		private static extern void vroomBluetoothLEInitialize (string unityObject, string unityCallback);
	
		[DllImport ("__Internal")]
		private static extern void vroomBluetoothLEDeinitialize();
	
		[DllImport ("__Internal")]
		private static extern void vroomBluetoothLEStartScan (bool clearPeripheralList);
	
		[DllImport ("__Internal")]
		private static extern void vroomBluetoothLEStopScan ();

		[DllImport ("__Internal")]
		private static extern void vroomBluetoothLERetrieveListOfPeripheralsWithServices (string serviceUUIDString);

		[DllImport ("__Internal")]
		private static extern void vroomBluetoothLEConnectToPeripheral (string name);

		[DllImport ("__Internal")]
		private static extern void vroomBluetoothLEDisconnectAll ();

		[DllImport ("__Internal")]
		private static extern void vroomBluetoothLEDisconnectPeripheral (string name);

		[DllImport ("__Internal")]
		private static extern void vroomBluetoothLEGetBatteryService(string name);

		[DllImport ("__Internal")]
		private static extern void vroomBluetoothLEGetDeviceInformationService(string name);

		[DllImport ("__Internal")]
		private static extern void vroomBluetoothLEGetVroomControllerStatus(string name);

		[DllImport ("__Internal")]
		private static extern void vroomBluetoothLESubscribeCharacteristic (string name);

		[DllImport ("__Internal")]
		private static extern void vroomBluetoothLEUnsubscribeCharacteristic (string name);

		[DllImport ("__Internal")]
		private static extern int vroomBluetoothLEGetNotificationValue(string name, out IntPtr bytes, out int size);
		//private static extern int vroomBluetoothLEGetNotificationValue(string name, byte[] bytes);
#endif

		private const string VROOM_CONTROLLER_SERVICE = "500C0001-164A-4D7A-A6CC-57301B115071";
		private const string VROOM_CONTROLLER_DATA_CHARACTERISTIC = "500C0002-164A-4D7A-A6CC-57301B115071";
		private const string VROOM_CONTROLLER_STATUS_CHARACTERISTIC = "500C0003-164A-4D7A-A6CC-57301B115071";

		internal bool initialized  = false;

		internal bool scanningState = false;
		static Dictionary<string, string[]> discoveredPeripherals = new Dictionary<string, string[]>();

		internal VvrConnectionMode connectionMode = VvrConnectionMode.Auto;
		internal int maxConnectableDevices = Enum.GetNames(typeof(VvrConnectionStyle)).Length;
		internal ControllerState[] controllerState;


		void Awake() {
			Initialize();
		}

		// Use this for initialization
		void Start()
		{
			DebugLogFormat("VvrControllerManager.Start():[mode:{0} style:{1}]", connectionMode, maxConnectableDevices);

#if !UNITY_EDITOR && UNITY_ANDROID
			try {
				using (AndroidJavaClass activityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {
					using (AndroidJavaObject activityObject = activityClass.GetStatic<AndroidJavaObject> ("currentActivity")) {
						AndroidJavaClass pluginClass = new AndroidJavaClass ("jp.co.wonderleague.vroom.controller.BluetoothLE");
						m_plugin = pluginClass.CallStatic<AndroidJavaObject> ("GetInstance");
						bool isSupport = m_plugin.Call<bool> ("Initialize", activityObject, gameObject.name, "OnBleCallback");
						if (!isSupport) m_plugin = null;
					}
				}
			} catch (Exception e) {
				Debug.LogWarning ("Exception: " + e.Message);
				m_plugin = null;
			}

			if (m_plugin == null) {
				Debug.LogError ("Android BLE Initialize falied.");
			}
#endif
#if !UNITY_EDITOR && UNITY_IOS
	        vroomBluetoothLEInitialize(gameObject.name, "OnBleCallback");
#endif
		}

	    // Update is called once per frame
    	void Update()
		{
			if (!initialized)
				return;
			
    	    GetNotificationValue();
		}

		// Suspend (pauseStatus:true) or Resume (pauseStatus:false)
		void OnApplicationPause (bool pauseStatus)
		{
			if (!initialized)
				return;

			if (pauseStatus) {
				DebugLog ("OnPause() called");
		
				for (int i = 0; i < maxConnectableDevices; i++) {
					if (controllerState[i].connectionState == VvrConnectionState.Connected &&
						controllerState[i].notifyState) {
#if !UNITY_EDITOR && UNITY_ANDROID
						m_plugin.Call("UnsubscribeCharacteristic", controllerState[i].peripheralID);
#endif
#if !UNITY_EDITOR && UNITY_IOS
						vroomBluetoothLEUnsubscribeCharacteristic(controllerState[i].peripheralID);
#endif
					}
				}
			} else {
				DebugLog ("OnResume() called");
				for (int i = 0; i < maxConnectableDevices; i++) {
					if (controllerState[i].connectionState == VvrConnectionState.Connected) {
#if !UNITY_EDITOR && UNITY_ANDROID
						m_plugin.Call("SubscribeCharacteristic", controllerState[i].peripheralID);
#endif
#if !UNITY_EDITOR && UNITY_IOS
						vroomBluetoothLESubscribeCharacteristic(controllerState[i].peripheralID);
#endif
					}
				}
			}
		}

		public void OnApplicationQuit() 
		{
			DebugLog("OnApplicationQuit() called");
			if (!initialized) return;

#if !UNITY_EDITOR && UNITY_ANDROID
			m_plugin.Call ("Close");
			m_plugin = null;
#endif
#if !UNITY_EDITOR && UNITY_IOS
			vroomBluetoothLEDisconnectAll();
			vroomBluetoothLEDeinitialize();
#endif
		}

		public bool IsInitialized {
			get { return initialized; }
		}

		public bool StartScan () 
		{
			if (scanningState)
				return true;

			scanningState = true;

			discoveredPeripherals.Clear();

			for (int i = 0; i < maxConnectableDevices; i++) {
				if (controllerState[i].connectionState == VvrConnectionState.Disconnected) {
					controllerState[i].connectionState = VvrConnectionState.Scanning;
				}
			}

			DebugLog("StartScan");

#if !UNITY_EDITOR && UNITY_ANDROID
			m_plugin.Call("StartScan", true);
#endif
#if !UNITY_EDITOR && UNITY_IOS
			vroomBluetoothLEStartScan(true);
#endif
			StartCoroutine(ScanHandler());
			return true;
		}

		public void StopScan ()
		{
			DebugLog("StopScan");

#if !UNITY_EDITOR && UNITY_ANDROID
			m_plugin.Call("StopScan");
#endif
#if !UNITY_EDITOR && UNITY_IOS
			vroomBluetoothLEStopScan();
#endif

			for (int i = 0; i < maxConnectableDevices; i++) {
				if (controllerState[i].connectionState == VvrConnectionState.Scanning) {
					controllerState[i].connectionState = VvrConnectionState.Disconnected;
				}
			}
			scanningState = false;
		}

		public void GetScanResult(out int size, out string[][] peripherals) {
			size = discoveredPeripherals.Count;
			if (size == 0)
				peripherals = null;
			
			int count = 0;
			peripherals = new string[size][];  
			foreach (string[] value in discoveredPeripherals.Values) {
				peripherals[count] = new string[3]{value[0], value[1], value[2]};
				count++;
			}
		}

		public bool ConnectRequest(int number, string peripheral)
		{
			ControllerState state = controllerState[number];
			if (state.connectionState == VvrConnectionState.Disconnecting) {
				if (state.peripheralID == peripheral) {
					// Do something
				} else {
					// Do something
				}
				Debug.LogWarningFormat("[warn]connect request:[number:{0} peripheral:{1} state:{2}({3})]",
										number, peripheral, state.connectionState.ToString(), state.peripheralID);
				return false;
			}
			
			if (state.connectionState == VvrConnectionState.Connecting ||
				state.connectionState == VvrConnectionState.Connected) {
				if (state.peripheralID != peripheral) {
					Debug.LogWarningFormat("[warn]connect request:[number:{0} peripheral:{1} state:{2}({3})]",
											number, peripheral, state.connectionState.ToString(), state.peripheralID);
					return false;
				}
				Debug.LogFormat("[info]connect request:[number:{0} peripheral:{1} state:{2}({3})]",
								number, peripheral, state.connectionState.ToString(), state.peripheralID);
			}

			if (state.connectionState == VvrConnectionState.Scanning ||
				state.connectionState == VvrConnectionState.Disconnected) {
				if (peripheral != null)
					state.peripheralID = peripheral;

				if (state.peripheralID == string.Empty) {
					if (connectionMode == VvrConnectionMode.Auto) {
						Debug.LogFormat("[info]start scanning:[number:{0} peripheral:null state:{1}({2})]",
								number, state.connectionState.ToString(), state.peripheralID);
						StartScan();
					} else {
						Debug.LogWarningFormat("[warn]connect request:[number:{0} peripheral:null state:{1}({2})]",
										number, state.connectionState.ToString(), state.peripheralID);
						return false;
					}
				} else {
					Debug.LogFormat("[info]connect request:[number:{0} peripheral:{1} state:{2}({3})]", 
										number, peripheral, state.connectionState.ToString(), state.peripheralID);
					
					if (discoveredPeripherals.ContainsKey(state.peripheralID)) {
						string[] scanData = discoveredPeripherals[state.peripheralID];
						state.shortenedDeviceName = scanData[1]; 
						state.rssi = scanData[2];
					}

					state.connectionState = VvrConnectionState.Connecting;
					StartCoroutine(CheckConnectionHandler(number));
#if !UNITY_EDITOR && UNITY_ANDROID
					if (! m_plugin.Call<bool>("ConnectRequest", state.peripheralID)) {
						Debug.LogFormat("[warn]failed to connect request:[number:{0} peripheral:{1} state:{2}({3})]", 
										number, peripheral, state.connectionState.ToString(), state.peripheralID);
						state.connectionState = VvrConnectionState.Disconnected;
					}
#endif
#if !UNITY_EDITOR && UNITY_IOS
					vroomBluetoothLEConnectToPeripheral(state.peripheralID);
#endif					
				}
			}
			return true;
		}
		
		public void DisconnectRequest(int number)
		{
			ControllerState state = controllerState[number];
			if (state.connectionState == VvrConnectionState.Connecting ||
				state.connectionState == VvrConnectionState.Connected) {
				state.connectionState = VvrConnectionState.Disconnecting;
				Debug.LogFormat("[info]disconnect request:[number:{0} state:{1}({2})]", 
									number, state.connectionState.ToString(), state.peripheralID);
#if !UNITY_EDITOR && UNITY_ANDROID
				m_plugin.Call<bool>("DisconnectRequest", state.peripheralID);
#endif
#if !UNITY_EDITOR && UNITY_IOS
				vroomBluetoothLEDisconnectPeripheral(state.peripheralID);
#endif
			}
		}

		private void OnBleCallback(string message)
		{
			if (message == null)
				return;

			char[] delim = new char[] { '~' };
			string[] tokens = message.Split(delim);
			if (tokens[0].Length == 0) 
				return;

			DebugLog(message);

			switch (tokens[0])
			{
				case "Initialized":
					// token: [0:Message Header]
					OnInitialized ();
					break;
				case "Deinitialized":
					// token: [0:Message Header]
					break;
				case "StartScan":
					scanningState = true;
					break;
				case "StopScan":
					for (int i = 0; i < maxConnectableDevices; i++) {
						if (controllerState[i].connectionState == VvrConnectionState.Scanning) {
							controllerState[i].connectionState = VvrConnectionState.Disconnected;
						}
					}
					scanningState = false;
					break;
				case "DiscoveredPeripheral":
					// token: [0:Header, 1:Peripheral, 2:DeviceName(Shortened), 3:RSSI, 4:ManufacturerData]
					OnScanResult(tokens[1], tokens[2], (tokens.Length == 4 ? tokens[3] : string.Empty));
					if (connectionMode == VvrConnectionMode.Auto) {
						ConnectToPeripheral(tokens[1]);
					}
					break;
				case "ConnectedPeripheral":
					// tokens: [0:Header 1:Peripheral 2:DeviceName]
					OnConnectedPeripheral (tokens [1], tokens[2]);
					break;
				case "ReconnectPeripheral":
					// tokens: [0:Header 1:Peripheral 2:Reason]
					OnReconnetPeripheral (tokens [1], tokens [2]);
					break;
				case "DisconnectedPeripheral":
					// tokens: [0:Header 1:Peripheral]
					OnDisconnectedPeripheral (tokens [1]);
					break;
				case "DiscoveredService":
					// tokens: [0:Header 1:Peripheral 2:Service UUID]
					break;
				case "DiscoveredCharacteristic":
					// tokens: [0:Header 1:Peripheral 2:Service UUID 3:Characteristic]
					break;
				case "CharacteristicRead":
					// tokens: [0:Header 1:Peripheral 2:Characteristic 3:Value]
					OnUpdateNotificationValue(tokens[1], tokens[2], tokens[3]);
					break;
				case "DidUpdateValueForCharacteristic":
					// tokens: [0:Header 1:Peripheral 2:Characteristic 3:Value]
					OnUpdateNotificationValue(tokens[1], tokens[2], tokens[3]);
					break;
				case "DidUpdateNotificationStateForCharacteristic":
					// tokens: [0:Header 1:Peripheral 2:Characteristic 3:Subscribe]
					OnUpdateNotificationState(tokens[1], tokens[2], tokens[3]);
					break;
				case "Error":
					OnError(tokens);
					Debug.LogError ("OnBleCallback: " + message);
					break;
				default:
					Debug.LogWarning("OnBleCallback: UnknownMessage~" + message);
					break;
			}
		}
			
	    private void GetNotificationValue()
    	{
#if !UNITY_EDITOR && UNITY_ANDROID
			if (m_plugin == null) return;
#endif
			for (int i = 0; i < maxConnectableDevices; i++) {
				if (controllerState[i].connectionState == VvrConnectionState.Connected && controllerState[i].notifyState) {
#if !UNITY_EDITOR && UNITY_ANDROID
					string data = m_plugin.Call<string> ("GetNotificationValue", controllerState[i].peripheralID);
					if (data == null)
						continue;

					char[] delim = new char[] { '~' };
					string[] tokens = data.Split(delim);
					if (tokens[0].Length > 0) {
						// Vroom Controller Measurement Data
						byte[] bytes = Convert.FromBase64String(tokens[0]);
						//DebugLog("NotificationValue:(" + i + "): Controller:" + BitConverter.ToString(bytes).Replace ("-", String.Empty));
						controllerState[i].SetControllerMeasurementData(bytes);
					}
					if (tokens[1].Length > 0) {
						// Battery Level
						byte[] bytes = Convert.FromBase64String(tokens[1]);
						//DebugLog("NotificationValue:(" + i + "): Battery:" + BitConverter.ToString(bytes).Replace ("-", String.Empty));
						controllerState[i].SetBatteryLevel(bytes);
					}
#endif
#if !UNITY_EDITOR && UNITY_IOS
					IntPtr bytePtr = IntPtr.Zero;
					int size = 0;
					if (vroomBluetoothLEGetNotificationValue(controllerState[i].peripheralID, out bytePtr, out size) != 0) {
						//DebugLog("NotificationValue:(" + i + "):" + BitConverter.ToString(bytes).Replace ("-", String.Empty));
						byte[] bytes = new byte[size];
						Marshal.Copy(bytePtr, bytes, 0, size);
						controllerState[i].SetControllerMeasurementData(bytes);
						Marshal.FreeHGlobal(bytePtr);
					}
#endif
				}
        	}
    	}

	    private void Initialize()
	    {
			// 最大接続可能数分、初期化する
			int length = Enum.GetNames(typeof(VvrConnectionStyle)).Length;
			Debug.Log("[VvrControllerManager] Initialize:[max:" + length + "]");
			controllerState = new ControllerState[length];
			for (int i = 0; i < length; i++) {
				controllerState[i] = new ControllerState();
			}
    	}

		private bool ConnectToPeripheral(string peripheral)
		{
			bool result = false;
			for (int i = 0; i < maxConnectableDevices; i++) {
				result = ConnectRequest(i, peripheral);
				if (result) break;
			}
			return result;
		}

		public void OnScanResult (string peripheral, string deviceName, string RSSI)
		{
			if (! discoveredPeripherals.ContainsKey(peripheral)) {
				string[] scanDevice = {peripheral, deviceName, RSSI};
				discoveredPeripherals[peripheral] = scanDevice;
			}
		}

		// BLE Plugin Callback: Initialized
		void OnInitialized() 
		{
			if (!initialized) {
				initialized = true;
				Debug.Log("connectionMode:" + connectionMode);
				if (connectionMode == VvrConnectionMode.Auto)
					StartScan();
			}
		}

		// BLE Plugin Callback: Connected Peripheral
    	void OnConnectedPeripheral(string peripheral, string deviceName)
    	{
			ControllerState state;
			int number;
			if (foundPeripheral(peripheral, out number, out state)) {
				state.deviceName = deviceName;
            	state.connectionState = VvrConnectionState.Connected;
			}

			if (connectionMode == VvrConnectionMode.Auto) {
				if (scanningState) {
					int connected = 0;
					for (int i = 0; i < maxConnectableDevices; i++) {
						ControllerState con = controllerState[i];
						if (con.connectionState == VvrConnectionState.Connected) {
							connected++;
						}
					}
					if (connected >= maxConnectableDevices) {
						StopScan();
					}
				}
			}
		}

		// BLE Plugin Callback: Reconnect Peripheral
		void OnReconnetPeripheral(string peripheral, string status = null)
		{
			ControllerState state;
			int number;
			if (foundPeripheral(peripheral, out number, out state)) {			
				state.connectionState = VvrConnectionState.Connecting;
			}
		}

		// BLE Plugin Callback: Disconnected Peripheral
		void OnDisconnectedPeripheral(string peripheral)
    	{
			ControllerState state;
			int number;
			if (foundPeripheral(peripheral, out number, out state)) {
				bool reconnect = false;
				if (state.connectionState != VvrConnectionState.Disconnecting)
					reconnect = true;
				state.notifyState = false;
				state.connectionState = VvrConnectionState.Disconnected;
				if (reconnect)
					ConnectRequest(number, peripheral);
			}
		}

		void OnUpdateNotificationValue (string peripheral, string characteristic, string encodedData)
		{
			ControllerState state;
			int number;
			if (foundPeripheral(peripheral, out number, out state)) {
				byte[] bytes = Convert.FromBase64String (encodedData);
				if ("Battery Level".Equals(characteristic)) {
					state.SetBatteryLevel(bytes);
					//DebugLog ("CharacteristicRead: " + peripheral + ", " + characteristic + ", " + controllerState[i].batteryLevel);
				} else {
					string value = System.Text.Encoding.UTF8.GetString(bytes);
					switch (characteristic) {
						case "Manufacturer Name String":
							state.manufacturerName = value;
							break;
						case "Model Number String":
							state.modelNumber = value;
							break;
						case "Serial Number String":
							state.serialNumber = value;
							break;
						case "Hardware Revision String":
							state.hardwareRevision = value;
							break;
						case "Firmware Revision String":
							state.firmwareRevision = value;
							break;
						case "Software Revision String":
							state.softwareRevision = value;
							break;
						default:
							Debug.LogWarningFormat ("[warn]unknown data received: [peripheral:{0} characteristic:{1} value:{2}]",
								peripheral, characteristic, value);
							break;
					}
					//DebugLog ("CharacteristicRead: " + peripheral + ", " + characteristic + ", " + value);
				}
			}
		}

		void OnUpdateNotificationState(string peripheral, string characteristic, string subscribe)
		{
			ControllerState state;
			int number;
			if (foundPeripheral(peripheral, out number, out state)) {
				if (subscribe.Equals ("Subscribe")) {
					state.notifyState = true;
				} else if (subscribe.Equals ("Unsubscribe")) {
					state.notifyState = false;
				}
				DebugLog ("UpdateNotificationState: " + peripheral + ", " + characteristic + ", " + subscribe);
			}
		}

		void OnError (string[] tokens) {
			// tokens[0]  : "Error"
			// tokens[1]  : Message
			// tokens[2]~ : Parameter 
			if (tokens[1].Equals("ConnectionStateChange")) {
				ControllerState state;
				int number;
				if (foundPeripheral(tokens[2], out number, out state)) {
					state.connectionState = VvrConnectionState.Error;
					state.connectionState = VvrConnectionState.Disconnected;
					ConnectRequest(number, state.peripheralID);
				}
				DebugLog("Error:" + tokens[1] + " [address:" + tokens[2] + ", state:" + tokens[3] + ", newState:" + tokens[4] + "]");
			}
		}

		bool foundPeripheral(string peripheral, out int number, out ControllerState state) {

			number = -1;			
			for (int i = 0; i < maxConnectableDevices; i++) {
				if (controllerState[i].peripheralID == peripheral) {
					number = i;
					break;
				}
			}
			
			if (number < 0) {
				state = new ControllerState();
				return false;
			}
			state = controllerState[number];
			return true;
		}

		IEnumerator ScanHandler () {
			for (int i = 0; i < 20; i++ ) {
				yield return new WaitForSeconds(1.0f);
				if (VvrController.scanningState == false) {
					yield break;
				}
			}
			StopScan();
		}

    	IEnumerator CheckConnectionHandler(int number) 
		{
			float startTime = Time.time;
	        while (controllerState[number].connectionState == VvrConnectionState.Connecting) {
            	yield return new WaitForSeconds(2.0f);
				float checkTime = Time.time;
				if ((checkTime - startTime) >= 30.0f) {
					DisconnectRequest(number);
					break;
				}
        	}
    	}

		[System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		private void DebugLog(string message)
		{
	        Debug.Log(message);
		}

		[System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		private void DebugLogFormat(string format, params object[] args)
		{
	        Debug.LogFormat(format, args);
		}
	}
}
