using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{

    public GameObject fireExting;
    public GameObject VREye;
    public GameObject hose;
    public ParticleSystem fireExtingParticle;

    public static bool fireExtingTrg;

    private bool ONE; //一回処理のためのトリガー

    // Use this for initialization
    void Start()
    {
        fireExtingTrg = false;
        hose.SetActive(false);
        ONE = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fireExtingPos = fireExting.transform.position;
        Vector3 playerPos = VREye.transform.position;
        float dis = Vector3.Distance(fireExtingPos, playerPos);

        //範囲内に入ったら置いてある消火器を見えなくして手元にホースが現れるようにする
        if (dis <= 7 && fireExtingTrg == false)
        {
            fireExtingTrg = true;
            hose.SetActive(true);
            if (ONE)
            {
                fireExtingParticle.Stop();
                ONE = false;
            }
            fireExting.SetActive(false);
        }

        if (fireExtingTrg == true && VvrController.Trigger() || fireExtingTrg == true && Input.GetKey(KeyCode.RightArrow))
        {
            fireExtingParticle.Play();
            Invoke("fireExtingStop", 3.5f);
        }
    }

    private void fireExtingStop()
    {
        fireExtingParticle.Stop();
        hose.SetActive(false);
    }
}
