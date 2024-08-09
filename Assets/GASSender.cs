using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GASSender : MonoBehaviour
{
    [SerializeField] string deployId; // Inspector����ݒ�\��
    [SerializeField] string questionText; // Inspector����ݒ肷�鎿��e�L�X�g
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
            Debug.LogError("�G���[: " + request.error);
        }
        else
        {
            Debug.Log("���X�|���X: " + request.downloadHandler.text);
        }
    }

}