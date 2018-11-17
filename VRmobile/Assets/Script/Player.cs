using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Vector3 VREye;

    public static bool squatFlg;                      //しゃがんでいるかどうか
    bool smokeFlg;                                    //煙範囲内にいるかどうか

    // Use this for initialization
    void Start () {
        //初期化処理
        squatFlg = false;
        smokeFlg = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "smokeDamage")
        {//otherには入ってきたオブジェクトが渡されているのでtagを比較しています。
            smokeFlg = true;
            Debug.Log("煙範囲");
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

        //しゃがむ処理部分
        if (Input.GetKeyDown(KeyCode.LeftArrow) && squatFlg == false)
        {
            this.transform.position = new Vector3(VREye.x, 1, VREye.z);
            squatFlg = true;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)&& squatFlg == true)
        {
            this.transform.position = new Vector3(VREye.x, 6, VREye.z);
            squatFlg = false;
        }



        //けむりに当たってからの処理
        if (smokeFlg == true)
        {
            if (VREye.y >= 4)
            {
                FindObjectOfType<ImageEffectControl>().damageone = true;
                FindObjectOfType<ImageEffectControl>().recovery = false;
            }
            else
            {
                FindObjectOfType<ImageEffectControl>().damageone = false;
                FindObjectOfType<ImageEffectControl>().recovery = true;
            }
        }
        else
        {
            Debug.Log("回避");
        }



    }

}
