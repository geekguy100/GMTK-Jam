/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/

using TMPro;
using UnityEngine;

public class TextSetter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private TextMeshProUGUI staminaText;
    
    public void SetText(string txt)
    {
        displayText.text = txt;
    }

    public void SetStaminaText(string text)
    {
        staminaText.text = text;
    }
}