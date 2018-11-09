using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Vector3 VREye;
    public static bool syagami = false;
	// Use this for initialization
	void Start () {
        syagami = false;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 Pos = this.transform.position;

        if (Input.GetKeyDown(KeyCode.LeftArrow) && syagami == false)
        {
            this.transform.position = new Vector3(Pos.x, 1, Pos.z);
            syagami = true;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)&& syagami == true)
        {
            this.transform.position = new Vector3(Pos.x, 6, Pos.z);
            syagami = false;
        }

	}
}
