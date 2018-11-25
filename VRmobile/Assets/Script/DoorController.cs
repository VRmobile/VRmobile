using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public GameObject door;
    public GameObject VREye;
    public ParticleSystem draftParticle;
    public float openSpeed; //ドアオープンスピード
    private bool doorOpen; //ドアチェック
    private float yDegree; //ドア回転角

    [SerializeField]
    private bool backDraftTrg = false;

    void Start()
    {
        doorOpen = false;
        yDegree = 0.0F;
        draftParticle.Stop();
    }
    void Update()
    {
        Vector3 doorPos = door.transform.position;
        Vector3 playerPos = VREye.transform.position;
        float dis = Vector3.Distance(doorPos, playerPos);

        if (dis <= 13 && VvrController.Trigger() || dis <= 13 && Input.GetKey(KeyCode.Space))
        {
            doorOpen = true;
        }

        if (doorOpen)
        {
            if (backDraftTrg == false)
            {
                if (yDegree < 90.0F)
                {
                    yDegree += openSpeed * Time.deltaTime;

                    door.transform.Rotate(0, openSpeed * Time.deltaTime, 0);
                }
            }
            else
            {
                if (yDegree < 90.0F)
                {
                    draftParticle.Play();
                    yDegree += openSpeed * Time.deltaTime;

                    door.transform.Rotate(0, openSpeed * Time.deltaTime, 0);
                }

            }
        }
        else
        {
            if (yDegree > 0.0F)
            {
                yDegree -= openSpeed * Time.deltaTime;
                transform.Rotate(0, -openSpeed * Time.deltaTime, 0);
            }
        }
    }
}
