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

    private bool ONE;
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
            Debug.Log(Dedpoint);

        }
    }
         public static int getDedpoint()
    {
        return Dedpoint;
    }

    void Start()
    {
        ONE = true;
        fireDed = DBGameOver.GetFire();
        smokeDed = DBGameOver.GetSmoke();
        backDed = DBGameOver.GetDraft();
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
                fede.Fead();
                DBFloorSwitch();
                if (ONE)
                {
                    smokeDed++;
                    ONE = false;
                }
                
                break;
            case 2:
                fede.Fead();
                DBFloorSwitch();
                if (ONE)
                {
                    fireDed++;
                    ONE = false;
                }
                
                break;
            //バックドラフト
            case 3:
                fede.Fead();
                DBFloorSwitch();
                if (ONE)
                {
                    backDed++;
                    ONE = false;
                }     
                
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
       

        
        
         