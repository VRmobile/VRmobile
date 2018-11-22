using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ded : MonoBehaviour
{
    public static int Dedpoint = 0;

    public GameObject panel;

    FadeScript fede;

    //アンケート集計用
    public int fireDed;

    public int smokeDed;

    public int backDed;
    //アンケート集計用
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
       

        
        
         