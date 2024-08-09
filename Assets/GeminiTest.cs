using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GeminiCommunicator : MonoBehaviour
{
    [SerializeField] string deployId; // Inspector����ݒ�\��
    string _responseText;

    public void Start()
    {
        // �������Ȃǂ��K�v�ȏꍇ�͂����ɏ���
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
            Debug.LogError("�G���[: " + request.error);
            callback?.Invoke("�G���[: " + request.error); // �G���[���̃R�[���o�b�N
        }
        else
        {
            _responseText = request.downloadHandler.text;
            // �K�v�ɉ����ă��X�|���X�̉�͂��s���A��b���e�ȊO���폜
            // ���Ƀ��X�|���X��JSON�`���Ȃ�A��͂��ĕK�v�ȕ������������o��
            string parsedResponse = ParseResponse(_responseText);
            callback?.Invoke(parsedResponse); // �������̃R�[���o�b�N
        }
    }

    private string ParseResponse(string responseText)
    {
        // �����Ń��X�|���X����͂��A��b���e�ȊO����菜���������s��
        // ���Ƀ��X�|���X��JSON�`���� { "answer": "xxx" } �̂悤�ɂȂ��Ă���ꍇ:
        string answer = responseText; // JSON��͂Ȃǂ��s���K�v������΂����ōs��
        return answer;
    }
}
