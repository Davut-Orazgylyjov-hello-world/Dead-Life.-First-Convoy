using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewCityMagazin_BaseDate", menuName = "CityMagazin_BaseDate")]
public class CityMagazin_BaseDate : ScriptableObject
{

    [Header("ID того что покупаем")]
    public ID_LootBay ID_LootBay_This;

    [Header("количество покупки валюты")]
    public int Currency_Bay;

    [Header("Цена")]
    public int LootBay_Cost;

    [Header("Валюта покупки")]
    public ID_Currency ID_Currency_This;

}
