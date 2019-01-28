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
        //bool act = tgl.isOn;
    }

    //ラジオボタンで性別男性に指定
    public void Man()
    {
        FindObjectOfType<VariableSave>().db_sex = 1;
    }
    //ラジオボタンで性別女性に指定
    public void Woman()
    {
        FindObjectOfType<VariableSave>().db_sex = 2;
    }

    //ラジオボタンでVRを知っているを指定
    public void VRKnow()
    {
        FindObjectOfType<VariableSave>().db_quesVR = 1;
    }
    //ラジオボタンでVRを知らないを指定
    public void VRnotKnow()
    {
        FindObjectOfType<VariableSave>().db_quesVR = 2;
    }

    //ラジオボタンで分かりやすいを指定
    public void GameTrue()
    {
        FindObjectOfType<VariableSave>().db_quesMove = 1;
    }
    //ラジオボタンで分かりにくいを指定
    public void GameFalse()
    {
        FindObjectOfType<VariableSave>().db_quesMove = 2;
        FindObjectOfType<ErrorManager>().NotFlg = true;
        FindObjectOfType<ErrorManager>().GameNotTri.SetActive(true);
    }
    //ラジオボタンで酔ったと指定
    public void DrunkTrue()
    {
        FindObjectOfType<VariableSave>().db_quesDrunk = 1;
    }

    //ラジオボタンで酔わないと指定
    public void DrunkFalse()
    {
        FindObjectOfType<VariableSave>().db_quesDrunk = 2;
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