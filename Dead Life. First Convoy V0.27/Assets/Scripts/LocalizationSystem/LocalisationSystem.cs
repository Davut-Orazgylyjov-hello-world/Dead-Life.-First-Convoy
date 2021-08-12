using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalisationSystem
{

    public enum Language
    {
        English,
        French,
        Russian,
        Japanese,
        Italian,
        Spanish,
        Arab,
        Vietnamese,
        Danish,
        Chinese_trad,
        Korean,
        Norwegian,
        Portuguese,
        Turkish,
        Finnish,
        Swedish,
        German,
        Bulgarian,
        Hungarian,
        Greek,
        Chinese_simpl,
        Dutch,
        Polish,
        Romanian,
        Thai,
        Ukrainian,
        Czech
    }

    public static Language language = Language.Russian;

    private static Dictionary<string, string> localised_English;
    private static Dictionary<string, string> localised_French;
    private static Dictionary<string, string> localised_Russian;
    private static Dictionary<string, string> localised_Japanese;
    private static Dictionary<string, string> localised_Italian;
    private static Dictionary<string, string> localised_Spanish;
    private static Dictionary<string, string> localised_Arab;
    private static Dictionary<string, string> localised_Vietnamese;
    private static Dictionary<string, string> localised_Danish;
    private static Dictionary<string, string> localised_Chinese_trad;
    private static Dictionary<string, string> localised_Korean;
    private static Dictionary<string, string> localised_Norwegian;
    private static Dictionary<string, string> localised_Portuguese;
    private static Dictionary<string, string> localised_Turkish;
    private static Dictionary<string, string> localised_Finnish;
    private static Dictionary<string, string> localised_Swedish;
    private static Dictionary<string, string> localised_German;
    private static Dictionary<string, string> localised_Bulgarian;
    private static Dictionary<string, string> localised_Hungarian;
    private static Dictionary<string, string> localised_Greek;
    private static Dictionary<string, string> localised_Chinese_simpl;
    private static Dictionary<string, string> localised_Dutch;
    private static Dictionary<string, string> localised_Polish;
    private static Dictionary<string, string> localised_Romanian;
    private static Dictionary<string, string> localised_Thai;
    private static Dictionary<string, string> localised_Ukrainian;
    private static Dictionary<string, string> localised_Czech;
    public static bool isInit;

    //public static CSVLoader csvLoader;

    public static void Init()
    {
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        localised_English = csvLoader.GetDictionaryValues("English");
        localised_French = csvLoader.GetDictionaryValues("French");
        localised_Russian = csvLoader.GetDictionaryValues("Russian");
        localised_Japanese = csvLoader.GetDictionaryValues("Japanese");
        localised_Italian = csvLoader.GetDictionaryValues("Italian");
        localised_Spanish = csvLoader.GetDictionaryValues("Spanish");
        localised_Arab = csvLoader.GetDictionaryValues("Arab");
        localised_Vietnamese = csvLoader.GetDictionaryValues("Vietnamese");
        localised_Danish = csvLoader.GetDictionaryValues("Danish");
        localised_Chinese_trad = csvLoader.GetDictionaryValues("Chinese_trad");
        localised_Korean = csvLoader.GetDictionaryValues("Korean");
        localised_Norwegian = csvLoader.GetDictionaryValues("Norwegian");
        localised_Portuguese = csvLoader.GetDictionaryValues("Portuguese");
        localised_Turkish = csvLoader.GetDictionaryValues("Turkish");
        localised_Finnish = csvLoader.GetDictionaryValues("Finnish");
        localised_Swedish = csvLoader.GetDictionaryValues("Swedish");
        localised_German = csvLoader.GetDictionaryValues("German");
        localised_Bulgarian = csvLoader.GetDictionaryValues("Bulgarian");
        localised_Hungarian = csvLoader.GetDictionaryValues("Hungarian");
        localised_Greek = csvLoader.GetDictionaryValues("Greek");
        localised_Chinese_simpl = csvLoader.GetDictionaryValues("Chinese_simpl");
        localised_Dutch = csvLoader.GetDictionaryValues("Dutch");
        localised_Polish = csvLoader.GetDictionaryValues("Polish");
        localised_Romanian = csvLoader.GetDictionaryValues("Romanian");
        localised_Thai = csvLoader.GetDictionaryValues("Thai");
        localised_Ukrainian = csvLoader.GetDictionaryValues("Ukrainian");
        localised_Czech = csvLoader.GetDictionaryValues("Czech");
        isInit = true;

    }


    public static string GetLocalisedValue(string key)
    {
        if (!isInit) { Init(); }

        string value = key;

        switch (language)
        {
            case Language.English:
                localised_English.TryGetValue(key, out value);
                break;
            case Language.French:
                localised_French.TryGetValue(key, out value);
                break;
            case Language.Russian:
                localised_Russian.TryGetValue(key, out value);
                break;
            case Language.Japanese:
                localised_Japanese.TryGetValue(key, out value);
                break;
            case Language.Italian:
                localised_Italian.TryGetValue(key, out value);
                break;
            case Language.Spanish:
                localised_Spanish.TryGetValue(key, out value);
                break;
            case Language.Arab:
                localised_Arab.TryGetValue(key, out value);
                break;
            case Language.Vietnamese:
                localised_Vietnamese.TryGetValue(key, out value);
                break;
            case Language.Danish:
                localised_Danish.TryGetValue(key, out value);
                break;
            case Language.Chinese_trad:
                localised_Chinese_trad.TryGetValue(key, out value);
                break;
            case Language.Korean:
                localised_Korean.TryGetValue(key, out value);
                break;
            case Language.Norwegian:
                localised_Norwegian.TryGetValue(key, out value);
                break;
            case Language.Portuguese:
                localised_Portuguese.TryGetValue(key, out value);
                break;
            case Language.Turkish:
                localised_Turkish.TryGetValue(key, out value);
                break;
            case Language.Finnish:
                localised_Finnish.TryGetValue(key, out value);
                break;
            case Language.Swedish:
                localised_Swedish.TryGetValue(key, out value);
                break;
            case Language.German:
                localised_German.TryGetValue(key, out value);
                break;
            case Language.Bulgarian:
                localised_Bulgarian.TryGetValue(key, out value);
                break;
            case Language.Hungarian:
                localised_Hungarian.TryGetValue(key, out value);
                break;
            case Language.Greek:
                localised_Greek.TryGetValue(key, out value);
                break;
            case Language.Chinese_simpl:
                localised_Chinese_simpl.TryGetValue(key, out value);
                break;
            case Language.Dutch:
                localised_Dutch.TryGetValue(key, out value);
                break;
            case Language.Polish:
                localised_Polish.TryGetValue(key, out value);
                break;
            case Language.Romanian:
                localised_Romanian.TryGetValue(key, out value);
                break;
            case Language.Thai:
                localised_Thai.TryGetValue(key, out value);
                break;
            case Language.Ukrainian:
                localised_Ukrainian.TryGetValue(key, out value);
                break;
            case Language.Czech:
                localised_Czech.TryGetValue(key, out value);
                break;
        }

        return value;
    }
}
