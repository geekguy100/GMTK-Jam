/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/

using TMPro;
using UnityEngine;

public class TextSetter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;
    
    public void SetText(string txt)
    {
        displayText.text = txt;
    }
}