using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ded : MonoBehaviour
{
    public static int Dedpoint = 0;

    public GameObject panel;

    FadeScript fede;

    //アンケート集計用
    public static int fireDed;

    public static int smokeDed;

    public static int backDed;

    public static int deadFloor;

    private bool switchFloorFlg;
    //アンケート集計用
    void OnTriggerStay(Collider other)
    {
        /*
        //スモーク
        if (other.tag == "smokeDamage")
        {
            Dedpoint = 1;
            smokeDed++;

            //Debug.Log(Dedpoint);
        }*/
        //炎
        if (other.tag == "fire")
        {
            Debug.Log("hei" + fireDed);
            Dedpoint = 2;
            Debug.Log(Dedpoint);

        }
        //バックドラフト
        if (other.tag == "BackDraft")
        {
            Dedpoint = 3;
            backDed++;
            Debug.Log(Dedpoint);

        }
    }
         public static int getDedpoint()
    {
        return Dedpoint;
    }

    void Start()
    {
        fireDed = 0;
        smokeDed = 0;
        backDed = 0;
        Dedpoint = 0;
        fede = panel.GetComponent<FadeScript>();
    }




    // Update is called once per frame
    void Update()
    {
        switchFloorFlg = Player.getFloorSwitch();
        Debug.Log(switchFloorFlg);
        switch (Dedpoint)
        {
            case 1:
                smokeDed++;
                fede.Fead();
                DBFloorSwitch();
                break;
            case 2:
                fireDed++;
                fede.Fead();
                DBFloorSwitch();
                break;
            //バックドラフト
            case 3:
                backDed++;
                fede.Fead();
                DBFloorSwitch();
                break;
        };
    }

    void DBFloorSwitch()
    {
        //FindObjectOfType<VariableSave>().clear = false;
        Debug.Log("sd" + switchFloorFlg);
        if (switchFloorFlg)
        {
            deadFloor = 1;
        }
        else
        {
            deadFloor = 2;
        }
    }

    public static int GetDeadFloor()
    {
        return deadFloor;
    }

    public static int GetFireDed()
    {
        return fireDed;
    }

    public static int GetSmokeDed()
    {
        return smokeDed;
    }

    public static int GetDraftDed()
    {
        return backDed;
    }
}
       

        
        
         