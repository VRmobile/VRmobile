using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocationRenderer : MonoBehaviour
{
    public LocationUpdater updater;
    public Text text;

    void Update()
    {
        text.text = updater.Status.ToString()
                  + "\n" + "lat:" + updater.Location.latitude.ToString()
                  + "\n" + "lng:" + updater.Location.longitude.ToString()
                  + "\n" + "高度" + updater.Location.altitude.ToString();

    }
}