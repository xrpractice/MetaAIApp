using System.Collections;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GroqApiLibrary;
using Meta.WitAi.Dictation;
using Meta.WitAi.TTS.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LlamaController : MonoBehaviour
{
    private const string API_KEY = "your api key";

    private IGroqApiClient _groqApiClient ;
    string request ;
    
    [SerializeField] TMP_Text outputField;
    
    [SerializeField] private Toggle _toggle;

    [SerializeField] TMP_Text message;

    [SerializeField] TMP_InputField inputField;
    
    [SerializeField] private TTSSpeaker ttsSpeaker;
    
    [SerializeField] private DictationService _dictation;

    void Start()
    {
        _groqApiClient = new GroqApiClient(API_KEY);
        StartCoroutine(ProcessRequestAfterDelay());
    }
    
    IEnumerator ProcessRequestAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        SendChatCompletionRequest();
    }

    public void ButtonOnclick()
    {
        SendChatCompletionRequest();
    }

    public async Task SendChatCompletionRequest()
    {
        if (_toggle.isOn)
        {
            request = message.text;
            request = Regex.Replace(request, @"\s+", " ");
        }
            
        else
            request = inputField.text;

        if (string.IsNullOrEmpty(request))
            request = "What is AI";
        
        string result = await _groqApiClient.CreateChatCompletionAsync(request);
        outputField.text = result;
        PlayAudio(result);
    }
    
    public void ClearText()
    {
        message.text = "";
    }
    
    private void PlayAudio(string result)
    {
        if (_dictation.MicActive) 
            _dictation.Deactivate();
        ttsSpeaker.Speak(result);
    }
}