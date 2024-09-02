using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class VirtualKeyboard : MonoBehaviour
{
    private TouchScreenKeyboard _keyboard;
    private TMP_InputField _inputField;

    void Start()
    {
        _inputField = GetComponent<TMP_InputField>();
        _inputField.onSelect.AddListener(e => OpenKeyboard());
    }

    private async void OpenKeyboard()
    {
        await Task.Delay(500);
        if (!TouchScreenKeyboard.visible)
        {
            _inputField.ActivateInputField();
            TouchScreenKeyboard.Open(_inputField.text, TouchScreenKeyboardType.ASCIICapable);
        }
    }
}
