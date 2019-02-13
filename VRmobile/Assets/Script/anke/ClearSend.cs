using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ClearSend : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Invoke("TitleLoad" , 3.0f);
	}

    void TitleLoad() {
        SceneManager.LoadScene("Title");
    }
}
