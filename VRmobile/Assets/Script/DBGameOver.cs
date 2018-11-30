using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBGameOver : MonoBehaviour {
    public static int floor;
    public static int fire;
    public static int smoke;
    public static int draft;
    // Use this for initialization
    void Start () {
        floor = Ded.GetDeadFloor();
        fire = Ded.GetFireDed();
        smoke = Ded.GetSmokeDed();
        draft = Ded.GetDraftDed();
	}
	
    public static int getFloor()
    {
        return floor;
    }
    public static int GetFire()
    {
        return fire;
    }

    public static int GetSmoke()
    {
        return smoke;
    }

    public static int GetDraft()
    {
        return draft;
    }
}

