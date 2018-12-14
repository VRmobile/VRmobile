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

    //ラジオボタンで操作がしやすいを指定
    public void MoveTrue()
    {
        FindObjectOfType<VariableSave>().quesMove = 1;

    }
    //ラジオボタンで操作がしにくいを指定
    public void MoveFalse()
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
}