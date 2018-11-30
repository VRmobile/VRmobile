using UnityEngine;
using System.Collections;
using LitJson;

public class TestPHP : MonoBehaviour {

    public string url = "http://sample.local/index03.php";

    void Start() {
        StartCoroutine(SetUserTest());
    }

    IEnumerator SetUserTest() {
        DBUsers sendData = new DBUsers();
        sendData.id = 1;
        sendData.name = "superman";
        sendData.score = 100;
        WWWForm form = new WWWForm();
        form.AddField("user" , JsonMapper.ToJson(sendData));
        using (WWW www = new WWW(url , form)) {
            yield return www;
            if (!string.IsNullOrEmpty(www.error)) {
                Debug.Log("error:" + www.error);
                yield break;
            }
            Debug.Log("text:" + www.text);
            DBUsers user = JsonMapper.ToObject<DBUsers>(www.text);
            Debug.Log("id:" + user.id + ", name:" + user.name + ", score:" + user.score);
        }
    }
}
