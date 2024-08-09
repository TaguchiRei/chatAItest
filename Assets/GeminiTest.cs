using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GeminiCommunicator : MonoBehaviour
{
    [SerializeField] string deployId; // Inspectorから設定可能に
    string _responseText;

    public void Start()
    {
        // 初期化などが必要な場合はここに書く
    }
    private void Update()
    {

    }

    public IEnumerator TalkToGemini(string talkText, System.Action<string> callback)
    {
        string url = $"https://script.google.com/macros/s/{deployId}/exec?question=" + UnityWebRequest.EscapeURL(talkText);
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("エラー: " + request.error);
            callback?.Invoke("エラー: " + request.error); // エラー時のコールバック
        }
        else
        {
            _responseText = request.downloadHandler.text;
            // 必要に応じてレスポンスの解析を行い、会話内容以外を削除
            // 仮にレスポンスがJSON形式なら、解析して必要な部分だけを取り出す
            string parsedResponse = ParseResponse(_responseText);
            callback?.Invoke(parsedResponse); // 成功時のコールバック
        }
    }

    private string ParseResponse(string responseText)
    {
        // ここでレスポンスを解析し、会話内容以外を取り除く処理を行う
        // 仮にレスポンスがJSON形式で { "answer": "xxx" } のようになっている場合:
        string answer = responseText; // JSON解析などを行う必要があればここで行う
        return answer;
    }
}
