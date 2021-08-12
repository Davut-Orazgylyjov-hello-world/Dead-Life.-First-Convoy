using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Info_Game : MonoBehaviour
{
    //инфа что под каким номером
    public enum UI_Info
    {
        Text_population = 0,
        Text_food = 1,
        Text_Mood = 2,
        Text_water = 3,
        Text_Cars_in_the_convoy = 4,
        Text_Bullet = 5,
        Text_gold = 6,
        Text_spare_parts = 7,
        Text_mileage_of_the_convoy = 8,
        Text_Petrol = 9
    }

    [Header("Информация счетчиков")]
    public Text[] Info_Text;


    //обновить инфу про количество машин в конвое
    public void Upadate_Info_CarsInConvoy(int CarsConvoy)
    {
        Info_Text[(int)UI_Info.Text_Cars_in_the_convoy].text = ("" + CarsConvoy);
    }


    //обновить инфу про количество Людей
    public void Upadate_Info_CharactersInConvoy(int ChractersConvoy)
    {
        Info_Text[(int)UI_Info.Text_population].text = ("" + ChractersConvoy);
    }

    //обновить инфу про количество галонов бензина
    public void Update_Info_Petrol(int PetrolConvoy)
    {
        Info_Text[(int)UI_Info.Text_Petrol].text = ("" + PetrolConvoy);
    }

    //обновить инфу про количество Mood
    public void Update_Info_Mood(int Mood)
    {
        Info_Text[(int)UI_Info.Text_Mood].text = ("" + Mood);
    }



    //обновить инфу про количество Water
    public void Update_Info_Water(int Water)
    {
        Info_Text[(int)UI_Info.Text_water].text = ("" + Water);
    }
    //обновить инфу про количество SpareParts
    public void Update_Info_SpareParts(int SpareParts)
    {
        Info_Text[(int)UI_Info.Text_spare_parts].text = ("" + SpareParts);
    }
    //обновить инфу про количество Gold
    public void Update_Info_Gold(int Gold)
    {
        Info_Text[(int)UI_Info.Text_gold].text = ("" + Gold);
    }

    //обновить инфу про количество Food
    public void Update_Info_Food(int Food)
    {
        Info_Text[(int)UI_Info.Text_food].text = ("" + Food);
    }

    //обновить инфу про количество Bullet
    public void Update_Info_Bullet(int Bullet)
    {
        Info_Text[(int)UI_Info.Text_Bullet].text = ("" + Bullet);
    }
}
