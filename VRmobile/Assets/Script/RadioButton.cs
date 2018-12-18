using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RadioButton : MonoBehaviour
{

    void Start()
    {


    }

    void Update()
    {
        Toggle tgl = GetComponent<Toggle>();
        bool act = tgl.isOn;
        Debug.Log(act);
    }

    //ラジオボタンで性別男性に指定
    public void Man()
    {
        FindObjectOfType<VariableSave>().sex = 1;
    }
    //ラジオボタンで性別女性に指定
    public void Woman()
    {
        FindObjectOfType<VariableSave>().sex = 2;
    }

    //ラジオボタンでVRを知っているを指定
    public void VRKnow()
    {
        FindObjectOfType<VariableSave>().quesVR = 1;
    }
    //ラジオボタンでVRを知らないを指定
    public void VRnotKnow()
    {
        FindObjectOfType<VariableSave>().quesVR = 2;
    }

    //ラジオボタンで分かりやすいを指定
    public void GameTrue()
    {
        FindObjectOfType<VariableSave>().quesMove = 1;

    }
    //ラジオボタンで分かりにくいを指定
    public void GameFalse()
    {
        FindObjectOfType<VariableSave>().quesMove = 2;

    }
    //ラジオボタンで酔ったと指定
    public void DrunkTrue()
    {
        FindObjectOfType<VariableSave>().quesDrunk = 1;
    }

    //ラジオボタンで酔わないと指定
    public void DrunkFalse()
    {
        FindObjectOfType<VariableSave>().quesDrunk = 2;
    }

    //操作がしにくいと指定
    public void NotMove()
    {
        if (FindObjectOfType<QuesMoveNotSelect>().sel[0] == false) {
            FindObjectOfType<QuesMoveNotSelect>().sel[0] = true;
        }else {
            FindObjectOfType<QuesMoveNotSelect>().sel[0] = false;
        }
    }
    //消火器が使いにくかったと指定
    public void NotFireExting()
    {
        if (FindObjectOfType<QuesMoveNotSelect>().sel[1] == false) {
            FindObjectOfType<QuesMoveNotSelect>().sel[1] = true;
        }
        else {
            FindObjectOfType<QuesMoveNotSelect>().sel[1] = false;
        }
    }
    //しゃがみがしにくいと指定
    public void NotSquat()
    {
        if (FindObjectOfType<QuesMoveNotSelect>().sel[2] == false) {
            FindObjectOfType<QuesMoveNotSelect>().sel[2] = true;
        }
        else {
            FindObjectOfType<QuesMoveNotSelect>().sel[2] = false;
        }
    }

}