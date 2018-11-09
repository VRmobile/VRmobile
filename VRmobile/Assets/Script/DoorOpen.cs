using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour {
    public GameObject door;
    public GameObject VREye;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Vector3 doorPos = door.transform.position;
        Vector3 playerPos = VREye.transform.position;
        float dis = Vector3.Distance(doorPos, playerPos);
        if (dis <= 13 && VvrController.Trigger()|| dis <= 13 && Input.GetKey(KeyCode.Space))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }
}
