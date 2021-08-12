using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LootMagazine_BD : MonoBehaviour
{
    [Header("Наша BD")]
    public CityMagazin_BaseDate CityMagazin_BaseDate_This;

    [Header("Визуальные характеристики")]
    public Text LootBay;
    public Text LootBay_Cost;
    public Image LootBay_Image;
    public Image CurrencyCost_Image;

    [Header("Сам магазин")]
    public City_Magazine City_Magazine_Script;

    [Header("Все Спрайты, лута покупки")]
    public Sprite[] Squad_Search_WhotThis_Sprite;

    [Header("Все Спрайты, валюты покупки лута")]
    public Sprite[] Currency_This_Sprite;



    //получили инфу про товар (лут)
    public void LootMagazine_BD_Info(CityMagazin_BaseDate CityMagazin_BaseDate_Script)
    {
        //получение бд тачки сюда
        CityMagazin_BaseDate_This = CityMagazin_BaseDate_Script;

        //Инфа про тачку
        LootBay.text = "" + CityMagazin_BaseDate_Script.Currency_Bay;
        LootBay_Cost.text = "" + CityMagazin_BaseDate_Script.LootBay_Cost;


        //что покупаем (картинка)
        {
            // если вода 1
            if (CityMagazin_BaseDate_Script.ID_LootBay_This == ID_LootBay.Water)
            {
                //ставим картинку что покупаем
                LootBay_Image.sprite = Squad_Search_WhotThis_Sprite[0];

            }
            // если запчасти 2
            if (CityMagazin_BaseDate_Script.ID_LootBay_This == ID_LootBay.SpareParts)
            {
                //ставим картинку что покупаем
                LootBay_Image.sprite = Squad_Search_WhotThis_Sprite[1];

            }
            // если бензин 3
            if (CityMagazin_BaseDate_Script.ID_LootBay_This == ID_LootBay.Petrol)
            {

                //ставим картинку что покупаем
                LootBay_Image.sprite = Squad_Search_WhotThis_Sprite[2];
            }        
            // если золота 5
            if (CityMagazin_BaseDate_Script.ID_LootBay_This == ID_LootBay.Gold)
            {
                //ставим картинку что покупаем
                LootBay_Image.sprite = Squad_Search_WhotThis_Sprite[3];

            }
            // если еда 6
            if (CityMagazin_BaseDate_Script.ID_LootBay_This == ID_LootBay.Food)
            {
                //ставим картинку что покупаем
                LootBay_Image.sprite = Squad_Search_WhotThis_Sprite[4];

            }
            // если пули 7
            if (CityMagazin_BaseDate_Script.ID_LootBay_This == ID_LootBay.Bullet)
            {
                //ставим картинку что покупаем
                LootBay_Image.sprite = Squad_Search_WhotThis_Sprite[5];

            }

        }
        //валюта покупки картинка
        {
            //если запчасти
            if (CityMagazin_BaseDate_Script.ID_Currency_This == ID_Currency.SpareParts)
            {
                CurrencyCost_Image.sprite = Currency_This_Sprite[0];
            }

            //если пули
            if (CityMagazin_BaseDate_Script.ID_Currency_This == ID_Currency.Bullet)
            {
                CurrencyCost_Image.sprite = Currency_This_Sprite[1];
            }

            //если золота
            if (CityMagazin_BaseDate_Script.ID_Currency_This == ID_Currency.Gold)
            {
                CurrencyCost_Image.sprite = Currency_This_Sprite[2];
            }
        }
    }


    //отправляем запрос на покупку лута
    public void Loot_Bay()
    {
        //                                                   цена                                валюта цены                          номер лута покупки                 количество  покупки
        City_Magazine_Script.Check_Money(CityMagazin_BaseDate_This.LootBay_Cost, CityMagazin_BaseDate_This.ID_Currency_This, CityMagazin_BaseDate_This.ID_LootBay_This, CityMagazin_BaseDate_This.Currency_Bay);
    }
}
