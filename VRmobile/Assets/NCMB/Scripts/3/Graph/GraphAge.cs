﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

public class GraphAge : MonoBehaviour {

    public Transform roulette;
    public GameObject plate;

    public GameObject[] text;
    public GameObject[] textRatio;

    /// <summary>
    /// グラフの色の個数
    /// </summary>
    private int num = 0;

    /// <summary>
    /// 割合
    /// </summary>
    public float[] ratio;

    private float[] sizeList;

    public int[] trg;

    void Awake() {
        Init();
        FetchAge();
    }

    public void Reset() {
        Init();
    }

    public void Show() {
        StartCoroutine(ShowAnim());
    }

    // 性別のグラフ
    void FetchAge() {

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
                    int s = System.Convert.ToInt32(obj["Age"]);
                    //Debug.Log(s);
                    num++;

                    switch (s) {
                        case 0:
                        trg[0]++;
                        break;

                        case 1:
                        trg[1]++;
                        break;

                        case 2:
                        trg[2]++;
                        break;

                        case 3:
                        trg[3]++;
                        break;

                        case 4:
                        trg[4]++;
                        break;

                        case 5:
                        trg[5]++;
                        break;

                        case 6:
                        trg[6]++;
                        break;

                        default:
                        break;
                    }
                    /*
                    Debug.Log("0は" + trg[0] + "回");
                    Debug.Log("1は" + trg[1] + "回");
                    Debug.Log("2は" + trg[2] + "回");
                    Debug.Log("3は" + trg[3] + "回");
                    Debug.Log("4は" + trg[4] + "回");
                    Debug.Log("5は" + trg[5] + "回");
                    Debug.Log("6は" + trg[6] + "回");
                    */
                }

                //Debug.Log(num + "回呼んだよ");

                float a  = (float)trg[0] / (float)num * (float)100;
                float b  = (float)trg[1] / (float)num * (float)100;
                float c  = (float)trg[2] / (float)num * (float)100;
                float d  = (float)trg[3] / (float)num * (float)100;
                float ee = (float)trg[4] / (float)num * (float)100;
                float f  = (float)trg[5] / (float)num * (float)100;
                float g  = (float)trg[6] / (float)num * (float)100;

                ratio[0] = a;
                ratio[1] = b;
                ratio[2] = c;
                ratio[3] = d;
                ratio[4] = ee;
                ratio[5] = f;
                ratio[6] = g;

                Array.Sort(ratio);
                Array.Reverse(ratio);

                Text val = text[0].GetComponent<Text>();
                Text val01 = text[1].GetComponent<Text>();
                Text val02 = text[2].GetComponent<Text>();
                Text val03 = text[3].GetComponent<Text>();
                Text val04 = text[4].GetComponent<Text>();
                Text val05 = text[5].GetComponent<Text>();
                Text val06 = text[6].GetComponent<Text>();

                Text ratio01 = textRatio[0].GetComponent<Text>();
                Text ratio02 = textRatio[1].GetComponent<Text>();
                Text ratio03 = textRatio[2].GetComponent<Text>();
                Text ratio04 = textRatio[3].GetComponent<Text>();
                Text ratio05 = textRatio[4].GetComponent<Text>();
                Text ratio06 = textRatio[5].GetComponent<Text>();
                Text ratio07 = textRatio[6].GetComponent<Text>();

                val.text = "" + trg[0];
                val01.text = "" + trg[1];
                val02.text = "" + trg[2];
                val03.text = "" + trg[3];
                val04.text = "" + trg[4];
                val05.text = "" + trg[5];
                val06.text = "" + trg[6];


                ratio01.text = "" + a + "%";
                ratio02.text = "" + b + "%";
                ratio03.text = "" + c + "%";
                ratio04.text = "" + d + "%";
                ratio05.text = "" + ee + "%";
                ratio06.text = "" + f + "%";
                ratio07.text = "" + g + "%";

            }
        });
    }

    // 円グラフが表示される演出
    private IEnumerator ShowAnim() {
        bool flg = true;
        roulette.GetComponent<Image>().fillAmount = 0;
        float speed = 0.05f;
        while (flg) {
            roulette.GetComponent<Image>().fillAmount += speed;
            if (roulette.GetComponent<Image>().fillAmount >= 1) flg = false;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void Init() {
        // 既に作成したグラフがあれば削除する
        foreach (Transform tran in roulette) {
            if (tran.name != "Plate") Destroy(tran.gameObject);
        }

        int kindCount = num;  // 最大の色の数
        sizeList = new float[kindCount];       // 割合のリスト
        float max = 100;                      // グラフの比率 100が最大　ここからどんどん引いていく

        for (int i = 0; i < kindCount; i++) {
            if (max <= 0) break;

            // Plateをコピーしてサイズなどを調節
            GameObject plateCopy = Instantiate(plate) as GameObject;
            plateCopy.transform.SetParent(roulette);
            plateCopy.transform.localPosition = Vector3.zero;
            plateCopy.transform.localScale = Vector3.one;

            sizeList[i] = ratio[i];

            // zの角度を設定
            plateCopy.transform.localEulerAngles = new Vector3(0 , 0 , (100f - (float)max) / 100f * -360f);

            // 円のサイズをfillAmountに設定
            plateCopy.GetComponent<Image>().fillAmount = (float)sizeList[i] / 100f;

            // 色をランダムに設定 明るめにしてます
            plateCopy.GetComponent<Image>().color = new Vector4(UnityEngine.Random.Range(0.6f , 1f) , UnityEngine.Random.Range(0.6f , 1f) , UnityEngine.Random.Range(0.6f , 1f) , 1); ;
            plateCopy.SetActive(true);
            max -= sizeList[i];

        }
        roulette.GetComponent<Image>().fillAmount = 1;
    }
}
