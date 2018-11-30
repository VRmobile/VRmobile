using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
                    Invoke("MainLoad", 1.5f);
                    break;
                }
            case 2:
                {
                    kemuri.SetActive(true);
                    Invoke("MainLoad", 1.5f);
                    break;
                }
            case 3:
                {
                    bakcdorahuto.SetActive(true);
                    Invoke("MainLoad", 1.5f);
                    break;
                }
        }
    }

    void MainLoad()
    {
        SceneManager.LoadScene("map1");
    }
}
