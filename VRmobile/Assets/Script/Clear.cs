using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Clear : MonoBehaviour {
    public static bool clearFlg;
	// Update is called once per frame
	void Update () {
        clearFlg = true;
        Invoke("ClearLoad", 1.5f);
	}


    void ClearLoad()
    {
        SceneManager.LoadScene("NCMB");
    }

    public static bool GetClear()
    {
        return clearFlg;
    }
}
