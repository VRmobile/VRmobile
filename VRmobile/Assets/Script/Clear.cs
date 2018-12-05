using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Clear : MonoBehaviour {
	// Update is called once per frame
	void Update () {

        Invoke("ClearLoad", 1.5f);
	}


    void ClearLoad()
    {
        SceneManager.LoadScene("NCMB");
    }
}
