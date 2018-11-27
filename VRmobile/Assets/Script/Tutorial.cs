using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        //ヴルームコントローラのトリガーが引かれたら画面遷移
        if (VvrController.Trigger() || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("map1");
        }

    }
}
