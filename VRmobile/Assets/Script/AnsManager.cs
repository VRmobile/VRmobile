using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnsManager : MonoBehaviour {

    public GameObject AgeAns;
    public GameObject SexAns;
    public GameObject VRAns;
    public GameObject DrunkAns;
    public GameObject GameAns;
    public GameObject GameTriAns;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ANSTEXT(AgeAns, FindObjectOfType<VariableSave>().age,3 );
        ANSTEXT(SexAns, FindObjectOfType<VariableSave>().sex, 1);
        ANSTEXT(VRAns, FindObjectOfType<VariableSave>().quesVR, 2);
        ANSTEXT(DrunkAns, FindObjectOfType<VariableSave>().quesDrunk, 2);
        ANSTEXT(GameAns, FindObjectOfType<VariableSave>().quesMove, 2);
        ANSTEXT(GameTriAns, FindObjectOfType<VariableSave>().quesMoveNot, 4);
    }

    void ANSTEXT(GameObject hei ,int quesNum ,int quesAns)
    {
        Text Text = hei.GetComponent<Text>();
        switch (quesNum)
        {
            case 1:
                if (quesAns == 1)
                {
                    Text.text = "男性";
                }
                else if(quesAns == 2)
                {
                    Text.text = "はい";
                }
                else if(quesAns == 3)
                {
                    Text.text = "7～9";
                }
                else
                {
                    Text.text = "移動操作";
                }
                break;
            case 2:
                if (quesAns == 1)
                {
                    Text.text = "女性";
                }
                else if(quesAns == 2)
                {
                    Text.text = "いいえ";
                }
                else if(quesAns == 3)
                {
                    Text.text = "10～12";
                }
                else
                {
                    Text.text = "消火器の使い方";
                }
                break;
            case 3:
                if(quesAns == 3)
                {
                    Text.text = "13～19";
                }
                else
                {
                    Text.text = "しゃがみの仕方";
                }
                break;
            case 4:
                if (quesAns == 3)
                {
                    Text.text = "20代";
                }
                else
                {
                    Text.text = "移動操作、消火器の使い方";
                }
                break;
            case 5:
                if (quesAns==3)
                {
                    Text.text = "30代";
                }
                else
                {
                    Text.text = "消火器の使い方、しゃがみの仕方";
                }
                break;
            case 6:
                if (quesAns == 3)
                {
                    Text.text = "40代";
                }
                else
                {
                    Text.text = "移動操作、しゃがみの仕方";
                }
                break;
            case 7:
                if (quesAns == 3)
                {
                    Text.text = "50代";
                }
                else
                {
                    Text.text = "移動操作、消火器の使い方、しゃがみの仕方";
                }
                break;
            case 8:
                Text.text = "60代以上";
                break;



        }
    }
}
