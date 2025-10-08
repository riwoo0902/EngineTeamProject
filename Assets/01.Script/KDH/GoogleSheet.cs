using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheet : MonoBehaviour
{
    const string URL = "https://docs.google.com/spreadsheets/d/1zVeF4OT7lxJPJqBD75ok5iTlkjky9TK7xGys4N2dnYc/export?format=csv";

    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        print(data);
    }
}
