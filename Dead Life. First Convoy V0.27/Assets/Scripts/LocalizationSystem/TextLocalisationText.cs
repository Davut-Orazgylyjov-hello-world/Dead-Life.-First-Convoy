using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//обычный текст
[RequireComponent(typeof(Text))]
public class TextLocalisationText : MonoBehaviour
{
    Text textField;

    public string key;


    [Header("шрифт текста")]
    public Fonts_SytemPack_AllLanguages FontStylePack;
    // Start is called before the first frame update
    void Start()
    {
        LocalizText();
    }

    public void LocalizText()
    {
        textField = GetComponent<Text>();
        string value = LocalisationSystem.GetLocalisedValue(key);
        textField.text = value;

        ChangeFontStyle();
     
    }

    //смена шрифта
    private void ChangeFontStyle()
    {
        //textField.font = FontStylePack.;
        int LanguageInt = PlayerPrefs.GetInt("Language");

        if (LanguageInt == 0)
            textField.font = FontStylePack.Russian;
        if (LanguageInt == 1)
            textField.font = FontStylePack.English;
        if (LanguageInt == 2)
            textField.font = FontStylePack.French;
        if (LanguageInt == 3)
            textField.font = FontStylePack.Japanese;
        if (LanguageInt == 4)
            textField.font = FontStylePack.Italian;
        if (LanguageInt == 5)
            textField.font = FontStylePack.Spanish;
        if (LanguageInt == 6)
            textField.font = FontStylePack.Arab;
        if (LanguageInt == 7)
            textField.font = FontStylePack.Ukrainian;
        if (LanguageInt == 8)
            textField.font = FontStylePack.Vietnamese;
        if (LanguageInt == 9)
            textField.font = FontStylePack.Danish;
        if (LanguageInt == 10)
            textField.font = FontStylePack.Chinese_trad;
        if (LanguageInt == 11)
            textField.font = FontStylePack.Korean;
        if (LanguageInt == 12)
            textField.font = FontStylePack.Norwegian;
        if (LanguageInt == 13)
            textField.font = FontStylePack.Portuguese;
        if (LanguageInt == 14)
            textField.font = FontStylePack.Turkish;
        if (LanguageInt == 15)
            textField.font = FontStylePack.Finnish;
        if (LanguageInt == 16)
            textField.font = FontStylePack.Swedish;
        if (LanguageInt == 17)
            textField.font = FontStylePack.German;
        if (LanguageInt == 18)
            textField.font = FontStylePack.Bulgarian;
        if (LanguageInt == 19)
            textField.font = FontStylePack.Hungarian;
        if (LanguageInt == 20)
            textField.font = FontStylePack.Greek;
        if (LanguageInt == 21)
            textField.font = FontStylePack.Czech;
        if (LanguageInt == 22)
            textField.font = FontStylePack.Chinese_simpl;
        if (LanguageInt == 23)
            textField.font = FontStylePack.Dutch;
        if (LanguageInt == 24)
            textField.font = FontStylePack.Polish;
        if (LanguageInt == 25)
            textField.font = FontStylePack.Romanian;
        if (LanguageInt == 26)
            textField.font = FontStylePack.Thai;     
    }
}