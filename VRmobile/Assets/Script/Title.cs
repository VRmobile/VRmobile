using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {

	// Update is called once per frame
	void Update () {

        //ヴルームコントローラのトリガーが引かれたら画面遷移
        if (VvrController.Trigger()|| Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Tutorial");
        }
		
	}
}
