using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDataManager : MonoBehaviour {

    //VariableSaveに送る変数
    public static int fireDead;
    public static int smokeDead;
    public static int draftDead;
    public static int floor=2;
    public static int clear;
    public static float time;

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

    public static int getClear()
    {
        return clear;
    }

    public static float getTime()
    {
        return time;
    }
}
