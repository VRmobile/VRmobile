using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyButton : MonoBehaviour {
    public enum State { Open, Close, UnUsed }

    public State state { get; private set; }
    public TweenScale open, close;

    /// ボタンをクリックした時の処理
    public void OnFalse() {
        FindObjectOfType<VariableSave>().db_clear = false;
    }

    public void OnTrue() {
        FindObjectOfType<VariableSave>().db_clear = true;
    }

    public void OnVRFalse() {
        FindObjectOfType<VariableSave>().db_quesVR = 2;
    }

    public void OnVRTrue() {
        FindObjectOfType<VariableSave>().db_quesVR = 1;
    }

    public void OnMoveFalse() {
        FindObjectOfType<VariableSave>().db_quesMove = 2;
    }

    public void OnMoveTrue() {
        FindObjectOfType<VariableSave>().db_quesMove = 1;
    }

    public void OnDrunkFalse() {
        FindObjectOfType<VariableSave>().db_quesDrunk = 2;
    }

    public void OnDrunkTrue() {
        FindObjectOfType<VariableSave>().db_quesDrunk = 1;
    }

    public void OnSave() {
        FindObjectOfType<VariableSave>().Save();
        Invoke("TitleLoad", 1.5f);
    }


    void TitleLoad()
    {
        SceneManager.LoadScene("Title");
    }
}
