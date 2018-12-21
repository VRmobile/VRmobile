using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

public class GraphQuesVR : MonoBehaviour {

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
        FetchFloor();
    }

    public void Reset() {
        Init();
    }

    public void Show() {
        StartCoroutine(ShowAnim());
    }

    // 性別のグラフ
    void FetchFloor() {

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
                    int s = System.Convert.ToInt32(obj["QuesVR"]);

                    Debug.Log(s);
                    num++;

                    switch (s) {
                        case 1:
                        trg[0]++;
                        break;

                        case 2:
                        trg[1]++;
                        break;

                        default:
                        break;
                    }
                    
                    //Debug.Log("VRtrueは" + trg[0] + "回");
                    //Debug.Log("VRfalseは" + trg[1] + "回");
                    
                }

                //Debug.Log(num + "回呼んだよ");

                float a = (float)trg[0] / (float)num * (float)100;
                float b = (float)trg[1] / (float)num * (float)100;

                ratio[0] = a;
                ratio[1] = b;

                Array.Sort(ratio);
                Array.Reverse(ratio);

                Text val = text[0].GetComponent<Text>();
                Text val01 = text[1].GetComponent<Text>();

                Text ratio01 = textRatio[0].GetComponent<Text>();
                Text ratio02 = textRatio[1].GetComponent<Text>();

                val.text = "" + trg[0];
                val01.text = "" + trg[1];

                ratio01.text = "" + a + "%";
                ratio02.text = "" + b + "%";

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
