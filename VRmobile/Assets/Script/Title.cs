using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {

    void Start()
    {
        
        
    }
	// Update is called once per frame
	void Update () {

        //ヴルームコントローラのトリガーが引かれたら画面遷移
        if (VvrController.Trigger()|| Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<VariableSave>().floor = 0;
            FindObjectOfType<VariableSave>().deadFire = 0;
            FindObjectOfType<VariableSave>().deadSmoke = 0;
            FindObjectOfType<VariableSave>().deadDraft = 0;

            DBGameOver.floor = 0;
            DBGameOver.fire = 0;
            DBGameOver.smoke = 0;
            DBGameOver.draft = 0;

            Ded.deadFloor = 0;
            Ded.fireDed = 0;
            Ded.smokeDed = 0;
            Ded.backDed = 0;
            Debug.Log("aaaaaaa" + FindObjectOfType<VariableSave>().deadFire);
            SceneManager.LoadScene("Tutorial");
        }
		
	}
}
