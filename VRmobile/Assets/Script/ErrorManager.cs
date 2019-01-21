using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorManager : MonoBehaviour {
    public GameObject[] Error;
    public GameObject GameNotTri;
    public bool Flg;
    public bool NotFlg;
	// Use this for initialization
	void Start () {
		for(int i = 0; i <= Error.Length-1; i++)
        {
            Error[i].SetActive(false);
        }
        NotFlg = false;
        Flg = false;
        GameNotTri.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Flg)//最初はエラー表示させない。
        {
            //年齢が入力されていない
            if (FindObjectOfType<VariableSave>().age == 0)
            {
                Error[0].SetActive(true);
            }
            else
            {
                Error[0].SetActive(false);
            }
            //性別が入力されていない
            if (FindObjectOfType<VariableSave>().sex == 0)
            {
                Error[1].SetActive(true);
            }
            else
            {
                Error[1].SetActive(false);
            }
            //VRを他にプレイしたかが入力されていない
            if (FindObjectOfType<VariableSave>().quesVR == 0)
            {
                Error[2].SetActive(true);
            }
            else
            {
                Error[2].SetActive(false);
            }
            //酔ったかが入力されていない
            if (FindObjectOfType<VariableSave>().quesDrunk == 0)
            {
                Error[3].SetActive(true);
            }
            else
            {
                Error[3].SetActive(false);
            }
            //全体を通して分かりやすかったかが入力されていない
            if (FindObjectOfType<VariableSave>().quesMove == 0)
            {
                Error[4].SetActive(true);
            }
            else
            {
                Error[4].SetActive(false);
            }

            //いいえと答えた人への質問が入力されていない
            if (FindObjectOfType<VariableSave>().quesMoveNot == 0)
            {
                Error[5].SetActive(true);
            }
            else
            {
                Error[5].SetActive(false);
            }

        }
        
    }
}
