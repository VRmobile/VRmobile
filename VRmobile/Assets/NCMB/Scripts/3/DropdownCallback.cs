using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DropdownCallback : MonoBehaviour {

    VariableSave vs;

    void Start() {
        vs = GetComponent<VariableSave>();
    }

    public void OnValueChanged(Dropdown dropdown) {
        switch (dropdown.value) {

            case 0:
            Debug.Log("未入力");
            vs.sex = 0;
            Debug.Log(vs.sex);
            break;

            case 1:
            Debug.Log("男");
            vs.sex = 1;
            Debug.Log(vs.sex);
            break;

            case 2:
            Debug.Log("女");
            vs.sex = 2;
            Debug.Log(vs.sex);
            break;

            default:
            break;
        }
    }

    public void OnValueChanged02(Dropdown dropdown) {
        switch (dropdown.value) {

            case 0:
            Debug.Log("未入力");
            vs.age = 0;
            Debug.Log(vs.age);
            break;

            case 1:
            Debug.Log("7～9才");
            vs.age = 1;
            Debug.Log(vs.age);
            break;

            case 2:
            Debug.Log("10～12才");
            vs.age = 2;
            Debug.Log(vs.age);
            break;

            case 3:
            Debug.Log("13～19才");
            vs.age = 3;
            Debug.Log(vs.age);
            break;

            case 4:
            Debug.Log("20代");
            vs.age = 4;
            Debug.Log(vs.age);
            break;

            case 5:
            Debug.Log("30代");
            vs.age = 5;
            Debug.Log(vs.age);
            break;

            case 6:
            Debug.Log("40代");
            vs.age = 6;
            Debug.Log(vs.age);
            break;

            case 7:
            Debug.Log("50代");
            vs.age = 7;
            Debug.Log(vs.age);
            break;

            case 8:
            Debug.Log("60代以上");
            vs.age = 8;
            Debug.Log(vs.age);
            break;

            default:
            break;
        }
    }
}