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
            nomal.transform.localScale = new Vector3(75.0f, 75.0f, 75.0f);
            miniGame.transform.localScale = new Vector3(50.0f, 50.0f, 50.0f);
            if (VvrController.AppButtonDown())
            {

                MainDataManager.floor = 2;
                MainDataManager.fireDead = 0;
                MainDataManager.smokeDead = 0;
                MainDataManager.draftDead = 0;
                SceneManager.LoadScene("Tutorial");
            }
        }
        else
        {
            nomal.transform.localScale = new Vector3(50.0f , 50.0f , 50.0f);
            miniGame.transform.localScale = new Vector3(75.0f , 75.0f , 75.0f);
            if (VvrController.AppButtonDown())
            {
                SceneManager.LoadScene("Tutorial2");
            }
        }
	}
}
