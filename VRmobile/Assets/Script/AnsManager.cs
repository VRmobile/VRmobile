using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnsManager : MonoBehaviour {

    public GameObject ageAns;
    public GameObject sexAns;
    public GameObject VRAns;
    public GameObject DrunkAns;
    public GameObject GameAns;
    public GameObject GameTriAns;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Hei(GameObject hei ,int quesNum ,int quesAns)
    {
        Text Text = hei.GetComponent<Text>();
        switch (quesNum)
        {
            case 0:
                Text.text = "未回答";
                break;
            case 1:
                if (quesAns == 1)
                {
                    Text.text = "男性";
                }
                else
                {
                    Text.text = "はい";
                }
                break;
            case 2:
                if (quesAns == 1)
                {
                    Text.text = "男性";
                }
                else
                {
                    Text.text = "はい";
                }
                break;


        }
    }
}
