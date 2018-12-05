using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSelect : MonoBehaviour {

    public GameObject MainView;

    public GameObject DeadFireView;

    public GameObject DeadSmokeView;

    public GameObject DeadDraftView;

    public GameObject ClearView;

    // Use this for initialization
    void Start () {
        DeadFireView.SetActive(false);
        DeadSmokeView.SetActive(false);
        DeadDraftView.SetActive(false);
        ClearView.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if(Ded.Dedpoint != 0)
        {
            Invoke("View", 2.0f);
        }
		
	}

    void View()
    {
        switch(Ded.Dedpoint)
        {
            case 1:
                DeadSmokeView.SetActive(true);
                break;
            case 2:
                DeadFireView.SetActive(true);
                break;
            case 3:
                DeadDraftView.SetActive(true);
                break;
        }
    }
}
