using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExDis : MonoBehaviour {
    public GameObject ExtingSmoke;
    Vector3 FireExting;
    private void OnParticleCollision(GameObject other)
    {
        FireExting = this.transform.position;
        if (other.gameObject.tag == "FireExting")
        {
            this.gameObject.SetActive(false);
            Instantiate(ExtingSmoke, new Vector3(FireExting.x, FireExting.y, FireExting.z), Quaternion.identity);
            FindObjectOfType<FireExtinguisher>().hose.SetActive(false);
            FireExtinguisher.fireCnt++;
        }
    }
}
