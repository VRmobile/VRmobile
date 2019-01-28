using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCtrl : MonoBehaviour {
    public bool animSuquat = false;
	// Use this for initialization
	void Start () {
        animSuquat = false;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(animSuquat);
	}

    public void Suquat() {
        animSuquat = true;
    }
}
