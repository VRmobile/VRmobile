﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour
{
    float alfa;
    float speed = 0.01f;
    float red, green, blue;

    private bool ONE;
    private bool fadeIn;

    void Start()
    {
        ONE = true;
        fadeIn = true;
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
        alfa = GetComponent<Image>().color.a;

    }

    public void FeadIn()
    {
        if (alfa >=2.0f)
        {
            fadeIn = false;
            ONE = true;
            FadeOut(); 
            FindObjectOfType<ViewSelect>().MainView.SetActive(false);

            if(Ded.Dedpoint!= 0)
            {
                Invoke("Restart" , 10.0f);
            }
            else
            {
                SceneManager.LoadScene("NCMB");
            }
            
        }
        else
        {
            if (fadeIn) {
                GetComponent<Image>().color = new Color(red , green , blue , alfa);
                alfa += speed;
                Debug.Log("nda");

            }
        }
    }

    public void FadeOut() {
        if (ONE) {
            Debug.Log(alfa);
            if (alfa <= 2.5f) {
                GetComponent<Image>().color = new Color(red , green , blue , alfa);
                alfa -= 0.02f;
                Debug.Log("nda");
            }
            if(alfa <= 0){
                ONE = false;
            }
        }
    }


    public void Restart() {
        SceneManager.LoadScene("map1");
    }
}