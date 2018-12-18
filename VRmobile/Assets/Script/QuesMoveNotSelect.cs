using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuesMoveNotSelect : MonoBehaviour {

    //選択を受け取る変数
    public bool[] sel;

    public int send = 0;

	// Use this for initialization
	void Start () {
		
        //開幕初期化
        for(int i = 0; i <= sel.Length; i++) {
            sel[i] = false;
        }

	}
	
	// Update is called once per frame
	void Update () {

        SendAnswer();
	}

    void SendAnswer() {

        //移動操作だけ
        if (sel[0]) {
            send = 1;
        }
        //消火器だけ
        if (sel[1]) {
            send = 2;
        }
        //しゃがみだけ
        if (sel[2]) {
            send = 3;
        }
        //移動操作と消火器
        if (sel[0] && sel[1]) {
            send = 4;
        }
        //消火器としゃがみ
        if (sel[1] && sel[2]) {
            send = 5;
        }
        //移動操作としゃがみ
        if (sel[0] && sel[2]) {
            send = 6;
        }
        //全部
        if (sel[0] && sel[1] && sel[2]) {
            send = 7;
        }

        switch (send) {
            case 1:
            FindObjectOfType<VariableSave>().quesMoveNot = 1;
            break;
            case 2:
            FindObjectOfType<VariableSave>().quesMoveNot = 2;
            break;
            case 3:
            FindObjectOfType<VariableSave>().quesMoveNot = 3;
            break;
            case 4:
            FindObjectOfType<VariableSave>().quesMoveNot = 4;
            break;
            case 5:
            FindObjectOfType<VariableSave>().quesMoveNot = 5;
            break;
            case 6:
            FindObjectOfType<VariableSave>().quesMoveNot = 6;
            break;
            case 7:
            FindObjectOfType<VariableSave>().quesMoveNot = 7;
            break;
        }
    }
}
