using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDataManager : MonoBehaviour {

    //VariableSaveに送る変数
    public static int fireDead;
    public static int smokeDead;
    public static int draftDead;
    public static int floor=2;
    public static bool clear;
    public static float time;


    void Start()
    {
        floor = 2;
        fireDead = VariableSave.getFireDead();
        smokeDead = VariableSave.getSmokeDead();
        draftDead = VariableSave.getDraftDead();
        clear = false;
    }
    // Update is called once per frame
    void Update () {
        

	}



    public static int getFireDead()
    {
        return fireDead;
    }

    public static int getSmokeDead()
    {
        return smokeDead;
    }

    public static int getDraftDead()
    {
        return draftDead;
    }

    public static int getFloor()
    {
        return floor;
    }

    public static bool getClear()
    {
        return clear;
    }

    public static float getTime()
    {
        return time;
    }
}
