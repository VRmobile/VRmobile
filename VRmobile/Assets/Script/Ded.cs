using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ded : MonoBehaviour
{
    public static int Dedpoint = 0;

    public GameObject panel;

    FadeScript fede;

    private bool ONE;
    //アンケート集計用
    void OnTriggerStay(Collider other)
    {
        /*
        //スモーク
        if (other.tag == "smokeDamage")
        {
            Dedpoint = 1;
            smokeDed++;

            //Debug.Log(Dedpoint);
        }*/
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
        ONE = true;
        Dedpoint = 0;
        fede = panel.GetComponent<FadeScript>();
    }




    // Update is called once per frame
    void Update()
    {
        switch (Dedpoint)
        {
            case 1:
                fede.Fead();
                if (ONE)
                {
                    MainDataManager.smokeDead++;
                    ONE = false;
                }
                
                break;
            case 2:
                fede.Fead();
                if (ONE)
                {
                    MainDataManager.fireDead++;
                    ONE = false;
                }
                
                break;
            //バックドラフト
            case 3:
                fede.Fead();
                if (ONE)
                {
                    MainDataManager.draftDead++;
                    ONE = false;
                }     
                
                break;
        };
    }
}
       

        
        
         