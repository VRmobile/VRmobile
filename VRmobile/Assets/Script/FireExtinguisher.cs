using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FireExtinguisher : MonoBehaviour
{
    [SerializeField]
    private GameObject fireExting;
    [SerializeField]
    private GameObject VREye;
    [SerializeField]
    public GameObject hose;
    [SerializeField]
    private ParticleSystem fireExtingParticle;
    [SerializeField]
    private bool gameModeFireExting;

    public static int fireCnt;               //火が消えたのをカウントする変数
    public static bool fireExtingTrg;        //消火器から煙を噴出するかのトリガ

    private bool ONE; //一回処理のためのトリガー

    // Use this for initialization
    void Start()
    {
        fireCnt = 0;
        fireExtingTrg = false;
        fireExting.SetActive(true);
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
        if (fireExtingTrg == true && VvrController.TriggerDown() || fireExtingTrg == true && Input.GetMouseButtonDown(1))
        {
            fireExtingParticle.Play();
            //Invoke("fireExtingStop", 3.5f);
        }
        else if (fireExtingTrg == true && VvrController.TriggerUp() || fireExtingTrg == true && Input.GetMouseButtonUp(1))
        {
            fireExtingParticle.Stop();
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
        if (fireExtingTrg == true && VvrController.TriggerDown() || fireExtingTrg == true && Input.GetMouseButtonDown(0))
        {
            fireExtingParticle.Play();
            //Invoke("fireExtingStop", 3.5f);
        }
        else if(fireExtingTrg == true && VvrController.TriggerUp() || fireExtingTrg == true && Input.GetMouseButtonUp(0))
        {
            fireExtingParticle.Stop();
        }
        if(fireCnt == 7)
        {
            SceneManager.LoadScene("FireExtingClear");
        }
    }
}
