using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{

    public GameObject fireExting;
    public GameObject VREye;
    public GameObject hose;
    public ParticleSystem fireExtingParticle;
    [SerializeField]
    private bool gameModeFireExting;
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
        //ゲームモード切替　真：消火器ミニゲーム　偽：脱出ゲーム
        if(gameModeFireExting == true)
        {
            GameModeFireExting();
        }
        else
        {
            GameModeNomal();
        }
    }

    private void GameModeNomal()
    {
        Vector3 fireExtingPos = fireExting.transform.position;
        Vector3 playerPos = VREye.transform.position;
        float dis = Vector3.Distance(fireExtingPos, playerPos); //消火器とプレイヤーで二点間の距離

        //範囲内に入ったら置いてある消火器を見えなくして手元にホースが現れるようにする
        if (dis <= 7 && fireExtingTrg == false)
        {
            fireExtingTrg = true;
            hose.SetActive(true);
            fireExting.SetActive(false);
            if (ONE)
            {
                fireExtingParticle.Stop();
                ONE = false;
            }
            
        }
        if (fireExtingTrg == true && VvrController.Trigger() || fireExtingTrg == true && Input.GetKey(KeyCode.RightArrow))
        {
            fireExtingParticle.Play();
            Invoke("fireExtingStop", 3.5f);
        }
    }
    
    private void GameModeFireExting()
    {
        fireExtingTrg = true;
        hose.SetActive(true);
        if (ONE)
        {
            fireExtingParticle.Stop();
            ONE = false;
        }
        if (fireExtingTrg == true && VvrController.TriggerDown() || fireExtingTrg == true && Input.GetKeyDown(KeyCode.RightArrow))
        {
            fireExtingParticle.Play();
            //Invoke("fireExtingStop", 3.5f);
        }
        else if(fireExtingTrg == true && VvrController.TriggerUp() || fireExtingTrg == true && Input.GetKeyUp(KeyCode.RightArrow))
        {
            fireExtingParticle.Stop();
        }
    }
    private void fireExtingStop()
    {
        fireExtingParticle.Stop();
        hose.SetActive(false);
    }
}
