using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class DataFetch : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //データベースから受信する
        Fetch();

    }

    void Fetch() {

        //複数のNCMBObjectを取得するクエリを作成//
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("VariableData");

        query.FindAsync((List<NCMBObject> objList , NCMBException e) => {

            if (e != null) {
                //エラー処理
                Debug.Log("接続失敗");
            }
            else {
                //成功時の処理
                foreach (NCMBObject obj in objList) {
                    Debug.Log(
                        ", Age : " + obj["Age"] +
                        ", Sex : " + obj["Sex"] +
                        ", Time : " + obj["Time"] +
                        ", Floor : " + obj["Floor"] +
                        ", DeadPointX : " + obj["DeadPointX"] +
                        ", DeadPointZ : " + obj["DeadPointZ"] +
                        ", DeadFire : " + obj["DeadFire"] +
                        ", DeadSmoke : " + obj["DeadSmoke"] +
                        ", DeadDraft : " + obj["DeadDraft"] +
                        ", Clear : " + obj["Clear"] +
                        ", QuesVR : " + obj["QuesVR"] +
                        ", QuesDrunk : " + obj["QuesDrunk"] +
                        ", QuesMove : " + obj["QuesMove"] +
                        ", QuesMoveNot : " + obj["QuesMoveNot"]
                        );
                }
            }
        });
    }
}
