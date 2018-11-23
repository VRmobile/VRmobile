using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExDis : MonoBehaviour {
    public GameObject ExtingFlame;
    private void OnParticleCollision(GameObject other)
    {
       /*
        if (other.gameObject.tag == "FireExting")
        {
            Debug.Log("パーティクルです。" + other.gameObject.name + "とぶつかりました");
        }
        */
        if (other.gameObject.tag == "ExtingFlame")
        {
            Debug.Log("hei");
            DestroyObject(ExtingFlame.gameObject);
        }
    }
}
