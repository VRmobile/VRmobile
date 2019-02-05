using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class VariableSave : MonoBehaviour {
    /// <summary>
    /// 0:未入力 1:7～9才 2:10～12才 3:13～19才 4:20代 5:30代 6:40代 7:50代 8:60代以上
    /// </summary>
    public int db_age = 0;
    /// <summary>
    /// 0:未入力 1:男性 2:女性
    /// </summary>
    public int db_sex = 0;
    /// <summary>
    /// クリアタイム
    /// </summary>
    public float db_time = 0f;
    /// <summary>
    /// 死んだ階層
    /// </summary>
    public int db_floor = 0;
    /// <summary>
    /// 死んだX座標
    /// </summary>
    public float db_deadPointX = 0f;
    /// <summary>
    /// 死んだZ座標
    /// </summary>
    public float db_deadPointZ = 0f;
    /// <summary>
    /// 火の死亡回数
    /// </summary>
    public  static int db_deadFire = 0;
    /// <summary>
    /// 煙の死亡回数
    /// </summary>
    public static int db_deadSmoke = 0;
    /// <summary>
    /// ドラフトの死亡回数
    /// </summary>
    public static int db_deadDraft = 0;
    /// <summary>
    /// クリアしたか
    /// </summary>
    public bool db_clear = false;
    /// <summary>
    /// アンケート01 VRを他にプレイしたことがあるか。
    /// 0:未入力 1:はい 2:いいえ
    /// </summary>
    public int db_quesVR = 0;
    /// <summary>
    /// アンケート02 ゲームをプレイして気持ち悪くなったか。 
    /// 0:未入力 1:はい 2:いいえ
    /// </summary>
    public int db_quesDrunk = 0;
    /// <summary>
    /// アンケート03 ゲームは全体を通して分かりやすかったか。
    /// 0:未入力 1:はい 2:いいえ
    /// </summary>
    public int db_quesMove = 0;
    /// <summary>
    /// アンケート03_1 何が分かりずらかったか。(3の質問でいいえを答えた人用)
    /// 0:未入力 1:移動操作 2:消火器の使い方 3:しゃがみの仕方 4:1+2 5:2+3 6:1+3 7:全部
    /// </summary>
    public int db_quesMoveNot = 0;

    public bool SOUSIN = false;


    // Use this for initialization
    void Start () {
        db_floor = MainDataManager.getFloor();
        db_deadFire = MainDataManager.getFireDead();
        db_deadSmoke = MainDataManager.getSmokeDead();
        db_deadDraft = MainDataManager.getDraftDead();
	}

    void Update() {
        Debug.Log("火" + db_deadFire);
        Debug.Log("煙" + db_deadSmoke);
        if (Input.GetKeyDown(KeyCode.S)) {
            SOUSIN = true;
        }

        if (SOUSIN) {
            Save();
            SOUSIN = false;
        }
    }


    public void SaveVariableData(int age , int sex , float time , int floor , float deadPointX, float deadPointZ, int deadFire , int deadSmoke , int deadDraft , bool clear, int QuesVR , int QuesDrunk , int QuesMove , int QuesMoveNot) {

        NCMBObject obj = new NCMBObject("VariableData");

        //VariableDataに項目を追加していく
        obj.Add("Age",          db_age);
        obj.Add("Sex",          db_sex);
        obj.Add("Time",         db_time);
        obj.Add("Floor",        db_floor);
        obj.Add("DeadPointX",   db_deadPointX);
        obj.Add("DeadPointZ" ,  db_deadPointZ);
        obj.Add("DeadFire" ,    db_deadFire);
        obj.Add("DeadSmoke" ,   db_deadSmoke);
        obj.Add("DeadDraft" ,   db_deadDraft);
        obj.Add("Clear",        db_clear);
        obj.Add("QuesVR" ,      db_quesVR);
        obj.Add("QuesDrunk" ,   db_quesDrunk);
        obj.Add("QuesMove" ,    db_quesMove);
        obj.Add("QuesMoveNot" , db_quesMoveNot);

        //追加した項目をセーブする
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
        SaveVariableData(db_age , db_sex , db_time , db_floor , db_deadPointX , db_deadPointZ , db_deadFire ,
                         db_deadSmoke , db_deadDraft , db_clear , db_quesVR , db_quesDrunk , db_quesMove , db_quesMoveNot);
        Debug.Log("セーブしたよ");
    }


    //データに関する各項目の取得するための関数
    public static int getFireDead()
    {
        return db_deadFire;
    }

    public static float getSmokeDead()
    {
        return db_deadSmoke;
    }

    public static float getDraftDead()
    {
        return db_deadDraft;
    }
}
