using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour {
    public GameObject nomal;
    public GameObject miniGame;

    private bool stageFlg;
	// Use this for initialization
	void Start () {
        stageFlg = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(VvrController.VolumeUp()|| Input.GetKeyDown(KeyCode.LeftArrow))
        {
            stageFlg = true;
        }

        if(VvrController.VolumeDown() || Input.GetKeyDown(KeyCode.RightArrow))
        {
            stageFlg = false;
        }
        if(stageFlg == true)
        {
            nomal.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
            miniGame.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            if (VvrController.AppButtonDown() || Input.GetKeyDown(KeyCode.Space))
            {

                DBGameOver.floor = 0;
                DBGameOver.fire = 0;
                DBGameOver.smoke = 0;
                DBGameOver.draft = 0;
                Clear.clearFlg = false;

                Ded.deadFloor = 0;
                Ded.fireDed = 0;
                Ded.smokeDed = 0;
                Ded.backDed = 0;
                SceneManager.LoadScene("Tutorial");
            }
        }
        else
        {
            miniGame.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
            nomal.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            if (VvrController.AppButtonDown() || Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Tutorial2");
            }
        }
	}
}
