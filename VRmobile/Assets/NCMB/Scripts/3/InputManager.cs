using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    InputField inputField;
    VariableSave vs;

    void Start() {

        inputField = GetComponent<InputField>();
        vs = GetComponent<VariableSave>();

        InitInputField();
    }

    //Timeの数値を入力
    public void InputTime() {

        if (Input.GetKeyDown(KeyCode.Return)) {

            string inputValue = inputField.text;

            //string型をfloat型に変換してる
            FindObjectOfType<VariableSave>().time = float.Parse(inputValue);
            Debug.Log(FindObjectOfType<VariableSave>().time);

            InitInputField();
        }
    }

    //Floorの数値を入力
    public void InputFloor() {

        if (Input.GetKeyDown(KeyCode.Return)) {

            string inputValue = inputField.text;

            //string型をint型に変換してる
            FindObjectOfType<VariableSave>().floor = int.Parse(inputValue);
            Debug.Log(FindObjectOfType<VariableSave>().floor);

            InitInputField();
        }
    }

    //DeadPointXの数値を入力
    public void InputDPX() {

        if (Input.GetKeyDown(KeyCode.Return)) {

            string inputValue = inputField.text;

            //string型をfloat型に変換してる
            FindObjectOfType<VariableSave>().deadPointX = float.Parse(inputValue);
            Debug.Log(FindObjectOfType<VariableSave>().deadPointX);

            InitInputField();
        }
    }

    //DeadPointZの数値を入力
    public void InputDPZ() {

        if (Input.GetKeyDown(KeyCode.Return)) {

            string inputValue = inputField.text;

            //string型をfloat型に変換してる
            FindObjectOfType<VariableSave>().deadPointZ = float.Parse(inputValue);
            Debug.Log(FindObjectOfType<VariableSave>().deadPointZ);

            InitInputField();
        }
    }

    //DeadCaseFireの数値を入力
    public void InputDCF() {

        if (Input.GetKeyDown(KeyCode.Return)) {

            string inputValue = inputField.text;

            //string型をint型に変換してる
            //FindObjectOfType<VariableSave>().deadFire = int.Parse(inputValue);
           // Debug.Log(FindObjectOfType<VariableSave>().deadFire);

            InitInputField();
        }
    }

    //DeadCaseSmokeの数値を入力
    public void InputDCS() {

        if (Input.GetKeyDown(KeyCode.Return)) {

            string inputValue = inputField.text;

            //string型をint型に変換してる
           // FindObjectOfType<VariableSave>().deadSmoke = int.Parse(inputValue);
           // Debug.Log(FindObjectOfType<VariableSave>().deadSmoke);

            InitInputField();
        }
    }

    //DeadCaseDraftの数値を入力
    public void InputDCD() {

        if (Input.GetKeyDown(KeyCode.Return)) {

            string inputValue = inputField.text;

            //string型をint型に変換してる
           // FindObjectOfType<VariableSave>().deadDraft = int.Parse(inputValue);
           // Debug.Log(FindObjectOfType<VariableSave>().deadDraft);

            InitInputField();
        }
    }

    /// <summary>
    /// InputFieldの初期化用メソッド
    /// 入力値をリセットして、フィールドにフォーカスする
    /// </summary>


    void InitInputField() {

        // 値をリセット
        //inputField.text = "";

        // フォーカス
        inputField.ActivateInputField();
    }

}