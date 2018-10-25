using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectButton : MonoBehaviour {

	Text buttonText;
	// Use this for initialization
	void Start () {
		buttonText = gameObject.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		switch(VvrController.ConnectionState()) {
			case VvrConnectionState.Connected:
				buttonText.text = "Disconnect";
				break;
			case VvrConnectionState.Connecting:
				buttonText.text = "Connecting";
				break;
			case VvrConnectionState.Disconnecting:
				buttonText.text = "Disconnecting";
				break;
			case VvrConnectionState.Disconnected:
				buttonText.text = "Connect";
				break;
			default:
				break;
		}
	}

	public void OnClick () {
		
		if (VvrController.ConnectionState() == VvrConnectionState.Connected) {
			VvrController.DisconnectRequest ();
		} else if (VvrController.ConnectionState() == VvrConnectionState.Disconnected) {
			VvrController.ConnectRequest ();
		}
	}
}
