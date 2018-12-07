using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour
{
    float alfa;
    float speed = 0.01f;
    float red, green, blue;

    void Start()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
    }

    public void Fead()
    {
        if (alfa >=2.0f)
        {
           // FindObjectOfType<ViewSelect>().MainView.SetActive(false);

            if(Ded.Dedpoint!= 0)
            {
               // SceneManager.LoadScene("map1");
            }
            else
            {
                SceneManager.LoadScene("NCMB");
            }
            
        }
        else
        {
            GetComponent<Image>().color = new Color(red, green, blue, alfa);
            alfa += speed;
            Debug.Log("nda");
        }
    }
}