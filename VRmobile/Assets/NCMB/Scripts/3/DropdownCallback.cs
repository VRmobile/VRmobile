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
            vs.db_sex = 0;
            break;

            case 1:
            vs.db_sex = 1;
            break;

            case 2:
            vs.db_sex = 2;
            break;

            default:
            break;
        }
    }

    public void OnValueChanged02(Dropdown dropdown) {
        switch (dropdown.value) {

            case 0:
            vs.db_age = 0;
            break;

            case 1:
            vs.db_age = 1;
            break;

            case 2:
            vs.db_age = 2;
            break;

            case 3:
            vs.db_age = 3;
            break;

            case 4:
            vs.db_age = 4;
            break;

            case 5:
            vs.db_age = 5;
            break;

            case 6:
            vs.db_age = 6;
            break;

            case 7:
            vs.db_age = 7;
            break;

            case 8:
            vs.db_age = 8;
            break;

            default:
            break;
        }
    }
}