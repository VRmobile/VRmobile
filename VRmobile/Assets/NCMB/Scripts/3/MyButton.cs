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
        FindObjectOfType<VariableSave>().quesVR = 2;
    }

    public void OnVRTrue() {
        FindObjectOfType<VariableSave>().quesVR = 1;
    }

    public void OnMoveFalse() {
        FindObjectOfType<VariableSave>().quesMove = 2;
    }

    public void OnMoveTrue() {
        FindObjectOfType<VariableSave>().quesMove = 1;
    }

    public void OnDrunkFalse() {
        FindObjectOfType<VariableSave>().quesDrunk = 2;
    }

    public void OnDrunkTrue() {
        FindObjectOfType<VariableSave>().quesDrunk = 1;
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
