using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class VariableSave : MonoBehaviour {
    /// <summary>
    /// 0:未入力 1:7～9才 2:10～12才 3:13～19才 4:20代 5:30代 6:40代 7:50代 8:60代以上
    /// </summary>
    public int age = 0;
    /// <summary>
    /// 0:未入力 1:男性 2:女性
    /// </summary>
    public int sex = 0;
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
    public  static int deadFire = 0;
    /// <summary>
    /// 煙の死亡回数
    /// </summary>
    public static int deadSmoke = 0;
    /// <summary>
    /// ドラフトの死亡回数
    /// </summary>
    public static int deadDraft = 0;
    /// <summary>
    /// クリアしたか
    /// </summary>
    public bool clear = false;
    /// <summary>
    /// アンケート01 VRを他にプレイしたことがあるか。
    /// 0:未入力 1:はい 2:いいえ
    /// </summary>
    public int quesVR = 0;
    /// <summary>
    /// アンケート02 ゲームをプレイして気持ち悪くなったか。 
    /// 0:未入力 1:はい 2:いいえ
    /// </summary>
    public int quesDrunk = 0;
    /// <summary>
    /// アンケート03 ゲームは全体を通して分かりやすかったか。
    /// 0:未入力 1:はい 2:いいえ
    /// </summary>
    public int quesMove = 0;
    /// <summary>
    /// アンケート03_1 何が分かりずらかったか。(3の質問でいいえを答えた人用)
    /// 0:未入力 1:移動操作 2:消火器の使い方 3:しゃがみの仕方 4:1+2 5:2+3 6:1+3 7:全部
    /// </summary>
    public int quesMoveNot = 0;

    public bool SOUSIN = false;


    // Use this for initialization
    void Start () {
        floor = MainDataManager.getFloor();
        deadFire = MainDataManager.getFireDead();
        deadSmoke = MainDataManager.getSmokeDead();
        deadDraft = MainDataManager.getDraftDead();
	}

    void Update() {
        Debug.Log("火" + deadFire);
        Debug.Log("煙" + deadSmoke);
        if (Input.GetKeyDown(KeyCode.S)) {
            SOUSIN = true;
        }

        if (SOUSIN) {
            Save();
            SOUSIN = false;
        }
    }


    public void SaveVariableData(int age, int sex, float time, int floor, float deadPointX, float deadPointZ, int deadFire, int deadSmoke, int deadDraft, bool clear, int QuesVR, int QuesDrunk, int QuesMove, int QuesMoveNot) {

        NCMBObject obj = new NCMBObject("VariableData");

        obj.Add("Age", age);
        obj.Add("Sex", sex);
        obj.Add("Time", time);
        obj.Add("Floor", floor);
        obj.Add("DeadPointX", deadPointX);
        obj.Add("DeadPointZ" , deadPointZ);
        obj.Add("DeadFire" , deadFire);
        obj.Add("DeadSmoke" , deadSmoke);
        obj.Add("DeadDraft" , deadDraft);
        obj.Add("Clear", clear);
        obj.Add("QuesVR" , quesVR);
        obj.Add("QuesDrunk" , quesDrunk);
        obj.Add("QuesMove" , quesMove);
        obj.Add("QuesMoveNot" , quesMoveNot);

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
        SaveVariableData(age , sex , time , floor , deadPointX , deadPointZ , deadFire , deadSmoke , deadDraft , clear, quesVR, quesDrunk, quesMove, quesMoveNot);
        Debug.Log("セーブしたよ");
    }


    //データに関する各項目の取得するための関数
    public static int getFireDead()
    {
        return deadFire;
    }

    public static int getSmokeDead()
    {
        return deadSmoke;
    }

    public static int getDraftDead()
    {
        return deadDraft;
    }
}
