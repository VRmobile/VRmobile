using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ded : MonoBehaviour
{
    public static int Dedpoint = 0;

    public GameObject panel;

    FadeScript fede;

    void OnTriggerStay(Collider other)
    {
        //スモーク
        if (other.tag == "smokeDamage")
        {
            Dedpoint = 1;

            Debug.Log(Dedpoint);
        }
        //炎
        if (other.tag == "fire")
        {
            Dedpoint = 2;
            Debug.Log(Dedpoint);

        }
        //バックドラフト
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

    void Start()
    {

        fede = panel.GetComponent<FadeScript>();
    }




    // Update is called once per frame
    void Update()
    {
        switch (Dedpoint)
        {
            //煙
            case 1:
                fede.Fead();
                break;
            //炎
            case 2:
                fede.Fead();
                break;
            //バックドラフト
            case 3:
                fede.Fead();
                break;



        }
        ;
    }
}
       

        
        
         