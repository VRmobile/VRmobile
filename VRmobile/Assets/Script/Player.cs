using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Vector3 VREye;
    public VRTeleporter teleporter;
    public static bool squatFlg;                      //しゃがんでいるかどうか
    bool smokeFlg;                                    //煙範囲内にいるかどうか
    public bool floorSwitchFlg;

    // Use this for initialization
    void Start () {
        //初期化処理
        squatFlg = false;
        smokeFlg = false;
        floorSwitchFlg = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "smokeDamage")
        {//otherには入ってきたオブジェクトが渡されているのでtagを比較しています。
            smokeFlg = true;
            //Debug.Log("煙範囲");
        }

        if(other.tag == "floorSwitch")
        {
            floorSwitchFlg = true;
            MainDataManager.floor = 2;
        }

    }

    
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "smokeDamage")
        {//otherには入ってきたオブジェクトが渡されているのでtagを比較しています。
            smokeFlg = false;
            // Debug.Log("煙");
        }
    }



    // Update is called once per frame
    void Update () {

        VREye = this.transform.position;

        //移動処理
        if (Input.GetMouseButtonDown(0)|| VvrController.AppButtonDown())
        {
            teleporter.ToggleDisplay(true);
        }
        if (Input.GetMouseButtonUp(0) || VvrController.AppButtonUp())
        {
            teleporter.Teleport();
            teleporter.ToggleDisplay(false);
        }

        //しゃがむ処理部分
        if (Input.GetKeyDown(KeyCode.LeftArrow) && squatFlg == false|| VvrController.HomeButtonDown() && squatFlg == false)
        {
            this.transform.position = new Vector3(VREye.x, VREye.y - 2.0f, VREye.z);
            squatFlg = true;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)&& squatFlg == true || VvrController.HomeButtonDown() && squatFlg == true)
        {
            this.transform.position = new Vector3(VREye.x, VREye.y + 2.0f, VREye.z);
            squatFlg = false;
        }



        //けむりに当たってからの処理
        if (smokeFlg == true)
        {
            if (squatFlg == false)
            {
                FindObjectOfType<ImageEffectControl>().damageone = true;
                FindObjectOfType<ImageEffectControl>().recovery = false;
            }
            else
            {
                FindObjectOfType<ImageEffectControl>().damageone = false;
                FindObjectOfType<ImageEffectControl>().recovery = true;
                FindObjectOfType<ImageEffectControl>().deadTime = 0;
            }
        }
        else
        {
            FindObjectOfType<ImageEffectControl>().damageone = false;
            FindObjectOfType<ImageEffectControl>().recovery = true;
            FindObjectOfType<ImageEffectControl>().deadTime = 0;
            
        }
    }
}
