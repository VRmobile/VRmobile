using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Vroom Controller for Unity
// Copyright 2017 WonderLeague Corporation.
// version 2.32

/// <summary>
/// This class requests runtime permission for Vroom controller. This class is required android only,
/// </summary>
public class VvrPermissionRequester {

	AndroidJavaObject permissionFragment = null;

	const string FRAGMENT_CLASSNAME = "jp.co.wonderleague.vroom.controller.PermissionFragment";
	const string CALLBACK_CLASSNAME = FRAGMENT_CLASSNAME + "$PermissionCallback"; 
	const string PERMISSION_NAME = "android.permission.ACCESS_COARSE_LOCATION";

	static VvrPermissionRequester theInstance = null;

	/// <summary>
	/// Get a Instance of VvrPermissionRequester
	/// </summary>
	/// <returns>VvrPermissionRequester</returns>
	public static VvrPermissionRequester Instance {
		get {
			if (theInstance == null) {
				theInstance = new VvrPermissionRequester();
				if (!theInstance.InitializeFragment()) {
					Debug.LogError("Unable to initialize PermissionFragment.");
					theInstance = null;
				}
			}
			return theInstance;
		}
	}

#if !UNITY_EDITOR && UNITY_ANDROID
	bool InitializeFragment() {
		try {
			using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			   	using (AndroidJavaObject currentActivity = jc.GetStatic<AndroidJavaObject>("currentActivity")) {
					AndroidJavaObject ajc = new AndroidJavaClass(FRAGMENT_CLASSNAME);
					permissionFragment = ajc.CallStatic<AndroidJavaObject>("getInstance", currentActivity);
				}
			}
		} catch (Exception e) {
			Debug.LogWarning("Exception:" + e.Message);
		}
		return permissionFragment != null && permissionFragment.GetRawObject() != IntPtr.Zero;	
  	}

	/// <summary>
	/// Determine whether you have been granted the ACCESS_COARSE_LOCATION permission.
	/// </summary>
	/// <returns>Returns true if you have the permission, or false if not.</returns>
	public bool IsPermissionGranted() {
		return permissionFragment.Call<bool>("checkPermission", PERMISSION_NAME);
	}	

	/// <summary>
	/// Gets whether you should show UI with rationale for requesting a permission. You should do this only 
	/// if you do not have the permission and the context in which the permission is requested does not 
	/// clearly communicate to the user what would be the benefit from granting this permission.
	/// </summary>
	/// <returns>Whether you can show permission rationale UI.</returns>
	public bool ShouldShowRequestPermissionRationale () {
		return permissionFragment.Call<bool>("shouldShowRequestPermissionRationale", PERMISSION_NAME);
	}

	/// <summary>
	/// Requests permission to be granted to this application.
	/// </summary>
	/// <param name="callback">Callback of the result of the requested permission.</param>
	public void RequestPermission (Action<PermissionStatus> callback) {
	    PermissionCallback cb = new PermissionCallback(PERMISSION_NAME, callback);
		permissionFragment.Call("requestPermission", PERMISSION_NAME, cb);
	}

	/// <summary>
	/// Show screen of details about a particular application.
	/// </summary>
	public void OpenApplicationSettings () {
		permissionFragment.Call("openSettings");
	}
#else 
	bool InitializeFragment() {
		Debug.Log("[VvrPermissionRequester]: InitializeFragment");
		return true;		
	}
	
	public bool IsPermissionGranted() {
		Debug.Log("[VvrPermissionRequester]: IsPermissionGranted");
		return false;
	}	

	public bool ShouldShowRequestPermissionRationale () {
		Debug.Log("[VvrPermissionRequester]: ShouldShowRequestPermissionRationale");
		return true;
	}

	public void RequestPermission (Action<PermissionStatus> callback) {
		Debug.Log("[VvrPermissionRequester]: RequestPermission");
		callback(new PermissionStatus(PERMISSION_NAME, true));
	}

	public void OpenApplicationSettings () {
	}
#endif

	public class PermissionStatus {
    	public PermissionStatus(string name, bool granted) {
	    	Name = name;
    		Granted = granted;
    	}
    
		public string Name {
    		get;
    		set;
    	}

    	public bool Granted {
    		get;
    		set;
    	}
	}

#if UNITY_ANDROID
	class PermissionCallback : AndroidJavaProxy {
		string permissionName;
		Action<PermissionStatus> callback;
		
		internal PermissionCallback (string requestedPermission, 
												Action<PermissionStatus> calback) 
												: base(CALLBACK_CLASSNAME) {
			permissionName = requestedPermission;
			this.callback = calback;
		}

		public void onRequestPermissionResult(string permission, bool grantResult) {
			if (permissionName.Equals(permission)) {
				callback(new PermissionStatus(permission, grantResult));
			}
		}
	}
#endif
}
