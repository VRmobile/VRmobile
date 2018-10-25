using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// Vroom Controller for Unity
// Copyright 2017 WonderLeague Corporation.
// version 2.32

namespace Vvr.Internal
{
	public class ControllerState {
		private const int VROOM_CONTROLLER_DATA_LENGTH = 20;

		public string peripheralID = string.Empty;
		public string deviceName = string.Empty;
		public string shortenedDeviceName = string.Empty;
		public string rssi = string.Empty;

		public VvrConnectionState connectionState = VvrConnectionState.Disconnected;
		public bool notifyState = false;
		
		public string manufacturerName = string.Empty;
		public string modelNumber = string.Empty;
		public string serialNumber = string.Empty;
		public string hardwareRevision = string.Empty;
		public string softwareRevision = string.Empty;
		public string firmwareRevision = string.Empty;
		
		public int batteryLevel = 0;
		
		public Quaternion orientation = Quaternion.identity;
		public Vector3 accel = Vector3.zero;
		public Vector3 gyro = Vector3.zero;
		public Joystick joystick = new Joystick();
		public Button clickButton = new Button();
		public Button appButton = new Button();
		public Button homeButton = new Button();
		public Button trigger = new Button();
		public VolumeState volumeState = new VolumeState();
		public bool recentering = false;
		public bool recentered = false; 

		public void SetBatteryLevel (byte[] bytes) {
			batteryLevel = (short)bytes[0];
		}

		public void SetControllerMeasurementData (byte[] bytes) {

			if (bytes.Length != VROOM_CONTROLLER_DATA_LENGTH)
				return;

			// Joystick (Vector2)
			SetJoystick(bytes);

			// BUttons
			// 0 (1)  : Home Button
			// 1 (2)  : App Button
			// 2 (4)  : Joystick Click
			// 3 (8)  : Trigger
			// 4 (16) : Volume-
			// 5 (32) : Volume+
			// 6 (64) : Recentered
			// 7 (128): Reserved
			homeButton.SetState(((bytes[17] & 0x1) == 1) ? true : false);
			appButton.SetState(((bytes[17] >> 1) & 0x1) == 1 ? true : false);
			trigger.SetState(((bytes[17] >> 3) & 0x1) == 1 ? true : false);

			volumeState.SetState((bytes [17] >> 4) & 0x3);

			bool recenterState = ((bytes [17] >> 6) & 0x1) == 1 ? true : false;  

			if (recenterState) {
				recentered  = false;
				recentering = true;
			} else {
				// Recenter State
				recentered  = recentering;
				recentering = false;

				// Quaternion (Quaternion)
				SetQuaternion(bytes);

				// Accelerometer (Vector3)
				SetAccel(bytes);

				// Gyro (Vector3)
				SetGyro(bytes);
			}
		}

    	private void SetQuaternion(byte[] bytes)
	    {
        	short qx, qy, qz, qw;
    	    const double scale = (1.0 / (1 << 14));
	        qw = (short)((bytes[0] << 8) | bytes[1]);
	        qx = (short)((bytes[2] << 8) | bytes[3]);
	        qy = (short)((bytes[4] << 8) | bytes[5]);
	        qz = (short)((bytes[6] << 8) | bytes[7]);
	        // 軸調整はqx,qy,qzを入れ替える。方向が逆なら-1.0fを掛けること
			orientation.Set((float)(-1.0f * scale * qx), (float)(-1.0f * scale * qz), (float)(-1.0f * scale * qy), (float)scale * qw);
	    }

    	private void SetAccel(byte[] bytes)
    	{
        	short ax, ay, az;
			// BOSCH Sensortec: BNO055
        	// Accelerometer (m/s2)
			//const double scale = (1.0 / 100.0);
        	// Accelerometer (G)
        	//const double scale = 1.0 / (100.0 * 9.80665);
			// TDK Invensense: icm-20608-g (16g)
			// Accelerometer (G)
			const double scale = (1.0 / 2048.0);
        	ax = (short)((bytes[8] << 8) | (bytes[9] & 0xf0));
        	ay = (short)(((bytes[9] & 0x0f) << 12) | (bytes[10] << 4));
        	az = (short)((bytes[11] << 8) | bytes[12] & 0xf0);
			accel = new Vector3((float)scale * ax, (float)scale * ay, (float)scale * az);
	    }

    	private void SetGyro(byte[] bytes)
	    {
        	short gx, gy, gz;
			// BOSCH Sensortec: BNO055
			// Gyroscope (deg/s)
			//const double scale = (1.0 / 100.0);
			// TDK Invensense: icm-20608-g (2000deg/s)
			// Gyroscope (deg/s)
        	const double scale = (1.0 / 16.4);
        	gx = (short)(((bytes[12] & 0x0f) << 12) | (bytes[13] << 4));
        	gy = (short)((bytes[14] << 8) | bytes[15] & 0xf0);
       		gz = (short)(((bytes[15] & 0x0f) << 12) | (bytes[16] << 4));
			gyro = new Vector3((float)scale * gx, (float)scale * gy, (float)scale * gz);
	    }

		private void SetJoystick(byte[] bytes)
		{
			short jx, jy;
			jx = (short)(bytes[18] << 2);
			jy = (short)(bytes[19] << 2);
			joystick.SetPosition(((float)jx), ((float)jy));
			clickButton.SetState (((bytes [17] >> 2) & 0x1) == 1 ? true : false);
		}
	}
}