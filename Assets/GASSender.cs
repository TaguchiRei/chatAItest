using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GASSender : MonoBehaviour
{
    [SerializeField] string deployId; // Inspectorから設定可能に
    [SerializeField] string questionText; // Inspectorから設定する質問テキスト
    string _displayText;

    void Start()
    {
        StartCoroutine(CallGoogleAppsScript());
    }

    IEnumerator CallGoogleAppsScript()
    {
        string url = $"https://script.google.com/macros/s/{deployId}/exec?question=" + UnityWebRequest.EscapeURL(questionText);
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("エラー: " + request.error);
        }
        else
        {
            Debug.Log("レスポンス: " + request.downloadHandler.text);
        }
    }

}