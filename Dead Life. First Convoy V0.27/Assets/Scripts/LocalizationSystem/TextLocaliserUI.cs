using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//mesh pro text
[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocaliserUI : MonoBehaviour
{
    TextMeshProUGUI textField;

    public string key;

    // Start is called before the first frame update
    void Start()
    {
        LocalizText();
    }

    private void LocalizText()
    {
        textField = GetComponent<TextMeshProUGUI>();
        string value = LocalisationSystem.GetLocalisedValue(key);
        textField.text = value;
    }
}