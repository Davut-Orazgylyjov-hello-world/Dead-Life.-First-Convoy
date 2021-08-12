using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarMagazine_BD : MonoBehaviour
{

    [Header("Наша BD")]
    public Bay_Car_BaseDate_Info Bay_Car_BaseDate_Info_This;

    [Header("Визуальные характеристики")]
    public Image Car_Image;
    //public Text Name;
    public TextLocalisationText TextLocalisationText_SCRIPT_Name;
    public Text Car_Cost;
    public Image ID_Squad_Search_WhotThis_Image;
    public Image ID_Currency_This_Image;

    [Header("Сам магазин")]
    public Bay_Car_Magazin Bay_Car_Magazin_Script;

    [Header("Все Спрайты, Что добывает машина")]
    public Sprite[] Squad_Search_WhotThis_Sprite;

    [Header("Все Спрайты, валюты покупки машиины")]
    public Sprite[] Currency_This_Sprite;

    //получили инфу про товар (машину)
    public void CarMagazine_BD_Info(Bay_Car_BaseDate_Info Bay_Car_BaseDate_Script)
    {
        //получение бд тачки сюда
        Bay_Car_BaseDate_Info_This = Bay_Car_BaseDate_Script;

        //Инфа про тачку
        Car_Image.sprite = Bay_Car_BaseDate_Script.Car_Sprite;
        TextLocalisationText_SCRIPT_Name.key = Bay_Car_BaseDate_Script.Name;
        //даем команду перевести
        TextLocalisationText_SCRIPT_Name.LocalizText();
        Car_Cost.text = ""+Bay_Car_BaseDate_Script.Car_Cost;
        //что добывают картинка
        {
            // если вода 1
            if (Bay_Car_BaseDate_Script.ID_Squad_Search_WhotThis == ID_Squad_Search_Whot.Squad_Search_Water)
            {
                //ставим картинку добычи
                ID_Squad_Search_WhotThis_Image.sprite = Squad_Search_WhotThis_Sprite[0];

            }
            // если запчасти 2
            if (Bay_Car_BaseDate_Script.ID_Squad_Search_WhotThis == ID_Squad_Search_Whot.Squad_Search_SpareParts)
            {
                //ставим картинку добычи
                ID_Squad_Search_WhotThis_Image.sprite = Squad_Search_WhotThis_Sprite[1];

            }
            // если бензин 3
            if (Bay_Car_BaseDate_Script.ID_Squad_Search_WhotThis == ID_Squad_Search_Whot.Squad_Search_Petrol)
            {

                //ставим картинку добычи
                ID_Squad_Search_WhotThis_Image.sprite = Squad_Search_WhotThis_Sprite[2];
            }
            // если люди 4
            if (Bay_Car_BaseDate_Script.ID_Squad_Search_WhotThis == ID_Squad_Search_Whot.Squad_Search_People)
            {

                //ставим картинку добычи
                ID_Squad_Search_WhotThis_Image.sprite = Squad_Search_WhotThis_Sprite[3];
            }
            // если золота 5
            if (Bay_Car_BaseDate_Script.ID_Squad_Search_WhotThis == ID_Squad_Search_Whot.Squad_Search_Gold)
            {
                //ставим картинку добычи
                ID_Squad_Search_WhotThis_Image.sprite = Squad_Search_WhotThis_Sprite[4];

            }
            // если еда 6
            if (Bay_Car_BaseDate_Script.ID_Squad_Search_WhotThis == ID_Squad_Search_Whot.Squad_Search_Food)
            {
                //ставим картинку добычи
                ID_Squad_Search_WhotThis_Image.sprite = Squad_Search_WhotThis_Sprite[5];

            }
            // если пули 7
            if (Bay_Car_BaseDate_Script.ID_Squad_Search_WhotThis == ID_Squad_Search_Whot.Squad_Search_Bullet)
            {
                //ставим картинку добычи
                ID_Squad_Search_WhotThis_Image.sprite = Squad_Search_WhotThis_Sprite[6];

            }
           
        }
        //валюта покупки картинка
        {
            //если запчасти
            if(Bay_Car_BaseDate_Script.ID_Currency_This == ID_Currency.SpareParts)
            {
                ID_Currency_This_Image.sprite = Currency_This_Sprite[0];
            }

            //если пули
            if (Bay_Car_BaseDate_Script.ID_Currency_This == ID_Currency.Bullet)
            {
                ID_Currency_This_Image.sprite = Currency_This_Sprite[1];
            }

            //если золота
            if (Bay_Car_BaseDate_Script.ID_Currency_This == ID_Currency.Gold)
            {
                ID_Currency_This_Image.sprite = Currency_This_Sprite[2];
            }
        }
    }


    //отправляем запрос на покупку тачки
    public void Bay_Car()
    {
        //                                                   цена                                валюта цены                          номер машины
        Bay_Car_Magazin_Script.Check_Money(Bay_Car_BaseDate_Info_This.Car_Cost, Bay_Car_BaseDate_Info_This.ID_Currency_This, Bay_Car_BaseDate_Info_This.ID_Car);
    }
}
