using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class VariableSave : MonoBehaviour {
    /// <summary>
    /// 0:未入力 1:男性 2:女性
    /// </summary>
    public int sex = 0;
    /// <summary>
    /// 0:未入力 1:10代未満 2:10代 3:20代 4:30代 5:40代 6:50代以上
    /// </summary>
    public int age = 0;
    /// <summary>
    /// クリアタイム
    /// </summary>
    public float time = 0f;
    /// <summary>
    /// 死んだ階層
    /// </summary>
    public int floor = 0;
    /// <summary>
    /// 死んだX座標
    /// </summary>
    public float deadPointX = 0f;
    /// <summary>
    /// 死んだZ座標
    /// </summary>
    public float deadPointZ = 0f;
    /// <summary>
    /// 火の死亡回数
    /// </summary>
    public int deadFire = 0;
    /// <summary>
    /// 煙の死亡回数
    /// </summary>
    public int deadSmoke = 0;
    /// <summary>
    /// ドラフトの死亡回数
    /// </summary>
    public int deadDraft = 0;
    /// <summary>
    /// クリアしたか
    /// </summary>
    public bool clear = false;
    /// <summary>
    /// アンケート01 VRについて知っていたか。
    /// </summary>
    public bool quesVR = false;
    /// <summary>
    /// アンケート02 移動方法は理解できたか。
    /// </summary>
    public bool quesMove = false;
    /// <summary>
    /// アンケート03 酔いは発生したか。
    /// </summary>
    public bool quesDrunk = false;

    // Use this for initialization
    void Start () {
        floor = DBGameOver.getFloor();
        deadFire = DBGameOver.GetFire();
        deadSmoke = DBGameOver.GetSmoke();
        deadDraft = DBGameOver.GetDraft();
	}


    public void SaveVariableData(int sex, int age, float time, int floor, float deadPointX, float deadPointZ, int deadFire, int deadSmoke, int deadDraft, bool clear, bool QuesVR, bool QuesMove, bool QuesDrunk) {

        NCMBObject obj = new NCMBObject("VariableData");

        obj.Add("Sex", sex);
        obj.Add("Age", age);
        obj.Add("Time", time);
        obj.Add("Floor", floor);
        obj.Add("DeadPointX", deadPointX);
        obj.Add("DeadPointZ" , deadPointZ);
        obj.Add("DeadFire" , deadFire);
        obj.Add("DeadSmoke" , deadSmoke);
        obj.Add("DeadDraft" , deadDraft);
        obj.Add("Clear", clear);
        obj.Add("QuesVR" , quesVR);
        obj.Add("QuesMove" , quesMove);
        obj.Add("QuesDrunk" , quesDrunk);

        obj.SaveAsync((NCMBException e) => {

            if (e != null) {
                //エラー処理
                Debug.Log("保存失敗 通信環境を確認してください。");
            }
            else {
                //成功時の処理
                Debug.Log("保存成功！");
            }
        });
    }

    public void Save() {
        SaveVariableData(sex , age , time , floor , deadPointX , deadPointZ , deadFire , deadSmoke , deadDraft , clear, quesVR, quesMove, quesDrunk);
        Debug.Log("セーブしたよ");
    }
}
