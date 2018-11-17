using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    float alfa;
    float speed = 0.03f;
    float red, green, blue;
    public GameObject VReye;
    float Dedpoint;

    void Start()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
    }

    public void fede()
    {

            GetComponent<Image>().color = new Color(red, green, blue, alfa);
            alfa += speed;
     }
    }