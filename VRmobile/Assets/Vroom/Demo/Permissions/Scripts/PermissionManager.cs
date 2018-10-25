using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PermissionManager : MonoBehaviour {

	[SerializeField]
	GameObject controllerSelectPanel;

	[SerializeField]
	GameObject permissionGrantedPanel;

	[SerializeField]
	GameObject permissionDeniedPanel;

	[SerializeField]
	GameObject permissionNoAskAgainPanel;

	[SerializeField]
	GameObject controllerConnectedPanel;

	[SerializeField]
	GameObject controllerNotUsedPanel;

	[SerializeField]
	GameObject controllerPrefab;

	VvrPermissionRequester requester;

	WaitForEndOfFrame waitForEndOfFrame;
	bool permissionResult = false;
	bool permissionGranted = false;

	// Use this for initialization
	void Start () {
		requester = VvrPermissionRequester.Instance;
		waitForEndOfFrame = new WaitForEndOfFrame();

		controllerSelectPanel.SetActive(true);
		permissionGrantedPanel.SetActive(false);
		permissionDeniedPanel.SetActive(false);
		permissionNoAskAgainPanel.SetActive(false);
		controllerConnectedPanel.SetActive(false);
		controllerNotUsedPanel.SetActive(false);
	}

	public void OnClickControllerEnabled () {
		if (requester.IsPermissionGranted()) {
			// 許可されている場合
			Debug.Log("OnClickControllerEnabled: [permission:Granted]");
			StartCoroutine(PermissionGranted());
		} else {
			// 許可されていない場合
			Debug.Log("OnClickControllerEnabled: [permission:Denied]");
			StartCoroutine(PermissionResult());
		}
	}

	IEnumerator PermissionResult () {
		// パーミッションの許可リクエストを行う。結果はコールバックとして返される。
		requester.RequestPermission(
			(VvrPermissionRequester.PermissionStatus result) => {
				// Androidプラグインからのコールバック。メインスレッドではないので、
				// UI周りの変更はできない。
				permissionResult = true;
				permissionGranted = result.Granted;
				Debug.Log("Results of Request Permission: [permission:" + result.Name + " granted:" + result.Granted + "]");
			});

		yield return new WaitUntil(() => permissionResult);
		if (permissionGranted) {
			// 現状、1本持ち・自動接続なら以下のようにシーンを移動しなくても接続までもっていけます。		
			yield return PermissionGranted();
		} else {
			// 拒否した場合
			controllerSelectPanel.SetActive(false);
			if (requester.ShouldShowRequestPermissionRationale()) { 
				permissionDeniedPanel.SetActive(true);
			} else {
				// 「今後、確認しない」をチェックして拒否した場合はこちらになる。
				permissionNoAskAgainPanel.SetActive(true);
				yield return new WaitForSecondsRealtime(3.0F);
				// アプリケーションの設定画面を開きます。
				requester.OpenApplicationSettings();
			}
		}
	}

	IEnumerator PermissionGranted () {
		controllerSelectPanel.SetActive(false);
		permissionGrantedPanel.SetActive(true);
		yield return ConnectController();
		permissionGrantedPanel.SetActive(false);
		controllerConnectedPanel.SetActive(true);
	}

	IEnumerator ConnectController () {
		GameObject controller = GameObject.Instantiate(controllerPrefab, Vector3.zero, Quaternion.identity);
		controller.name = controllerPrefab.name;
		while(true) {
			if (VvrController.ConnectionState() == VvrConnectionState.Connected) {
				break;
			}
			yield return waitForEndOfFrame;
		}
	}

	public void OnClickControllerDisabled () {
		controllerSelectPanel.SetActive(false);
		controllerNotUsedPanel.SetActive(true);
	}

}
