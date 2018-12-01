using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FireExtingClear : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Invoke("ClearFLoad", 5.0f);
    }


    void ClearFLoad()
    {
        SceneManager.LoadScene("Title");
    }

}