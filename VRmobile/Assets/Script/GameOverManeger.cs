using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManeger : MonoBehaviour
{

    public GameObject fire;
    public GameObject kemuri;
    public GameObject bakcdorahuto;

    // Use this for initialization
    void Start ()
    {
        fire.SetActive(false);
        kemuri.SetActive(false);
        bakcdorahuto.SetActive(false);

    }
	
	// Update is called once per frame
	void Update ()

    {
        int Clear = Ded.Dedpoint;

        switch (Clear)
        {
            case 1:
                {
                    fire.SetActive(true);

                    break;
                }
            case 2:
                {
                    kemuri.SetActive(true);

                    break;
                }
            case 3:
                {
                    bakcdorahuto.SetActive(true);

                    break;
                }





        }





    }
}
