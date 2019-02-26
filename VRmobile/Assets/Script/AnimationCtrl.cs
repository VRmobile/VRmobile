using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCtrl : MonoBehaviour {
    public bool animStart = false;
	// Use this for initialization
	void Start () {
        animStart = false;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(animStart);
	}

    public void AnimStart() {
        animStart = true;
    }

}
