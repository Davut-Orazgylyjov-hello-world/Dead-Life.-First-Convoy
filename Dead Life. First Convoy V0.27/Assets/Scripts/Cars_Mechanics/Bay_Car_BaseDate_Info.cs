using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CarBay_BaseDate_Info", menuName = "CarBay_BaseDate_Info")]
public class Bay_Car_BaseDate_Info : ScriptableObject
{

    [Header("ID_Тачки")]
    public ID_Car ID_Car;

    [Header("Имя машины")]
    public string Name;

    [Header("Изображение Тачки")]
    public Sprite Car_Sprite;

    [Header("Что добывает")]
    public ID_Squad_Search_Whot ID_Squad_Search_WhotThis;

    [Header("Цена")]
    public int Car_Cost;

    [Header("Валюта покупки")]
    public ID_Currency ID_Currency_This;


}
