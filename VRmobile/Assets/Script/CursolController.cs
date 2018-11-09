using UnityEngine;
using System.Collections;

public class CursolController : MonoBehaviour
{
	public static bool trgFlg = false;


    void Update()
    {
        //Vector3 player = GameObject.Find("VREye").transform.position;
        VvrController.ConnectRequest();

        Ray ray = new Ray(Camera.main.transform.position,
            Camera.main.transform.rotation * Vector3.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.name == "Floor")
            {
                transform.position = hit.point + new Vector3(0, 0.1f, 0);
            }
        }

            if (Input.GetMouseButtonDown(0)|| VvrController.AppButton())
        {
        	Vector3 pos = transform.position;

            if(Player.squatFlg == true)
            {
                GameObject.Find("VREye").transform.position = new Vector3(pos.x, 1, pos.z);
            }
            else
            {
                GameObject.Find("VREye").transform.position = new Vector3(pos.x, 6, pos.z);
            }
        	
        }
    }
}
