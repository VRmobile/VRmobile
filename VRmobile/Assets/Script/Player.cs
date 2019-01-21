using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    private Vector3 VREye;
    public VRTeleporter teleporter;
    public Camera mainCamera;
    public GameObject circleGage;
    public static bool squatFlg;                      //しゃがんでいるかどうか
    bool smokeFlg;                                    //煙範囲内にいるかどうか
    public bool smokeTeleportFlg;

    // Use this for initialization
    void Start () {
        //初期化処理
        squatFlg = false;
        smokeFlg = false;
        smokeTeleportFlg = false;
        circleGage.SetActive(false);
        circleGage.GetComponent<Image>().fillAmount = 0;
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
            MainDataManager.floor = 1;
        }

        if(other.tag == "smokeTrue")
        {
            smokeTeleportFlg = true;
        }

        if(other.tag == "smokeFalse")
        {
            smokeTeleportFlg = false;
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
        float x = mainCamera.transform.eulerAngles.x;

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
        if (Input.GetKeyDown(KeyCode.LeftArrow) && squatFlg == false|| VvrController.HomeButtonDown() && squatFlg == false|| 70.0f < x && x < 90.0f && squatFlg == false)
        {
            //下を向くとサークルゲージが増加し最大になるとしゃがむ
            circleGage.SetActive(true);
            circleGage.GetComponent<Image>().fillAmount += 0.01f;
            if(circleGage.GetComponent<Image>().fillAmount == 1.00f) {
                this.transform.position = new Vector3(VREye.x , VREye.y - 2.0f , VREye.z);
                squatFlg = true;
            }
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)&& squatFlg == true || VvrController.HomeButtonDown() && squatFlg == true|| 320.0f < x && x < 340.0f && squatFlg == true)
        {
            this.transform.position = new Vector3(VREye.x, VREye.y + 2.0f, VREye.z);
            squatFlg = false;
        }
        else {
            circleGage.GetComponent<Image>().fillAmount = 0;
            circleGage.SetActive(true);
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
