using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ded : MonoBehaviour
{
    public static int Dedpoint = 0;

    public GameObject VReye;

    void OnTriggerStay(Collider other)
    {

        if (other.tag == "smokeDamage")
        {
            Dedpoint = 1;

            Debug.Log(Dedpoint);
        }
        if (other.tag == "fire")
        {
            Dedpoint = 2;
            Debug.Log(Dedpoint);

        }
        if (other.tag == "BackDraft")
        {
            Dedpoint = 3;
            Debug.Log(Dedpoint);

        }
    }
         public static int getDedpoint()
    {
        return Dedpoint;
    }



    // Update is called once per frame
    void Update()
    {
        switch (Dedpoint)
        {
            case 1:
                break;
            case 2:

                break;
            case 3:

                break;



        }
        ;
    }
}
       

        
        
         