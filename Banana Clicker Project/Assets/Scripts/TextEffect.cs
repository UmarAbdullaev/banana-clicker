using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    public void Set(string text)
    {
        textComponent.text = text;
    }
}
