using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyButton : MonoBehaviour {

    /// ボタンをクリックした時の処理
    public void OnFalse() {
        FindObjectOfType<VariableSave>().clear = false;
    }

    public void OnTrue() {
        FindObjectOfType<VariableSave>().clear = true;
    }

    public void OnVRFalse() {
        FindObjectOfType<VariableSave>().quesVR = false;
    }

    public void OnVRTrue() {
        FindObjectOfType<VariableSave>().quesVR = true;
    }

    public void OnMoveFalse() {
        FindObjectOfType<VariableSave>().quesMove = false;
    }

    public void OnMoveTrue() {
        FindObjectOfType<VariableSave>().quesMove = true;
    }

    public void OnDrunkFalse() {
        FindObjectOfType<VariableSave>().quesDrunk = false;
    }

    public void OnDrunkTrue() {
        FindObjectOfType<VariableSave>().quesDrunk = true;
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
