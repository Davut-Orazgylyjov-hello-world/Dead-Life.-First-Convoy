using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City_Relations : MonoBehaviour
{
    [Header("Список Городов")]
    public int[] ID_City_Realaion;
    public Text[] City_Relation_TEXT;
    public int[] City_Relation_INT;



    void Start()
    {
        //Загружаем инфу отношений локаций
        for( int i = 0; i < ID_City_Realaion.Length; i++)
        {
            //загружаем инфу отношений
            City_Relation_INT[i] = PlayerPrefs.GetInt("City_Relation" + ID_City_Realaion[i]);
            //ставим инфу 
            City_Relation_TEXT[i].text = "" + City_Relation_INT[i];
            //Debug.Log("Город"+ ID_City_Realaion[i]+"Отношения"+ City_Relation_INT[i]);
        }
    }

    //-- отношения
    public void Minus_Relation(int MinusRel_INT, int ID_PlaceLocation)
    {
        //сохроняем уровень отношений с гиродом
        PlayerPrefs.SetInt("City_Relation" + ID_PlaceLocation,
            (PlayerPrefs.GetInt("City_Relation" + ID_PlaceLocation)- MinusRel_INT));
        //загружаем инфу отношений
        City_Relation_INT[ID_PlaceLocation] = PlayerPrefs.GetInt("City_Relation" + ID_PlaceLocation);
        //меняем инфу про задание
        City_Relation_TEXT[ID_PlaceLocation].text = "" + City_Relation_INT[ID_PlaceLocation];

        ////Если < -100 то ставим -100
        //if (PlayerPrefs.GetInt("City_Relation" + ID_PlaceLocation)<-100)
        //{
        //    PlayerPrefs.SetInt("City_Relation" + ID_PlaceLocation,-100);
        //}

        ////Если > 100 то ставим 100
        //if (PlayerPrefs.GetInt("City_Relation" + ID_PlaceLocation) > 100)
        //{
        //    PlayerPrefs.SetInt("City_Relation" + ID_PlaceLocation, 100);
        //}
    }

    //++ отношения
    public void Plus_Relation(int PlusRel_INT, int ID_PlaceLocation)
    {
        //сохроняем уровень отношений с гиродом
        PlayerPrefs.SetInt("City_Relation" + ID_PlaceLocation,
            (PlayerPrefs.GetInt("City_Relation" + ID_PlaceLocation) + PlusRel_INT));
        //загружаем инфу отношений
        City_Relation_INT[ID_PlaceLocation] = PlayerPrefs.GetInt("City_Relation" + ID_PlaceLocation);
        //меняем инфу про задание
        City_Relation_TEXT[ID_PlaceLocation].text = "" + City_Relation_INT[ID_PlaceLocation];
    }

}
