using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class DoorController : MonoBehaviour
{
    public GameObject door;
    public GameObject VREye;
    public ParticleSystem draftParticle;
    public float openSpeed; //ドアオープンスピード
    public bool doorOpen; //ドアチェック
    private float yDegree; //ドア回転角

    public GameObject panel;
    FadeScript fede;

    [SerializeField]
    private bool backDraftTrg = false;
    [SerializeField]
    public bool clearFlg = false;
    [SerializeField]
    private bool doorOpenFlg = true;

    void Start()
    {
        doorOpen = false;
        yDegree = 0.0F;
        draftParticle.Stop();
        fede = panel.GetComponent<FadeScript>();
    }
    void Update()
    {
        Vector3 doorPos = door.transform.position;
        Vector3 playerPos = VREye.transform.position;
        float dis = Vector3.Distance(doorPos, playerPos);

        if (dis <= 13 && VvrController.Trigger()&&doorOpenFlg == true || dis <= 13 && Input.GetKey(KeyCode.Space) && doorOpenFlg == true)
        {
            doorOpen = true;
        }

        if (doorOpen)
        {
            if (clearFlg == true)
            {
                if (yDegree < 90.0F)
                {
                    yDegree += openSpeed * Time.deltaTime;
                    door.transform.Rotate(0, openSpeed * Time.deltaTime, 0);
                    Debug.Log(yDegree);
                    
                }
                if(yDegree > 90.0F)
                {
                    MainDataManager.clear = true;
                    fede.FeadIn();
                    Invoke("Clear" , 2.5f);
                }

            }
            else if (backDraftTrg == false)
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

    void Clear()
    {
        FindObjectOfType<ViewSelect>().ClearView.SetActive(true);
    }
}
