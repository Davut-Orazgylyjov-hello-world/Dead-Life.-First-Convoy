using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Это главный скрипт коновойной тачки
//Тут механики взаимодействий и тд. Для каждой тачки свой скрипт а этот скрипт для взаимождейтсивй с ними
// как в Activate_Hit_player ток мы вместо bool используем сразу enum ID_Car
public class Convoy_Car : MonoBehaviour
{
    [Header("Какой атрибут требуется от людей")]
    public Car_attribute Need_Car_attribute;

    [Space]
    public ID_Car This_car_ID;

    [Header("Максимальное количество людей в машине")]
    public int[] Max_CharactersAtCar;

    [Header("ID_Людей в этой машине")]
    public int[] ID_CharactersInThisCar;

    [Header("Скрипты")]
    public New_Human_Character New_Human_Character_Script;



    [Header("Атрибуты персов в машине")]
    public int CharactersAtributs;

    [Header("Текущий уровень прокачки тачки")]
    public int Car_Num_upgraded;


    [Header("Фары")]
    public GameObject LightCar;

    //время в жизни
    private int Hours;

    public void Start()
    {
        //включаем фары если ночь
        {
            //получить время устройства
            {
                string HH = System.DateTime.UtcNow.ToLocalTime().ToString("HH");
                Hours = (Convert.ToInt32(HH)) + 1;

            }


            //включить фары если ночь
            if (Hours >= 22 || Hours < 6)
            {
                LightCar.SetActive(true);
            }
        }

    }
    


    //найти свободное место для человека
    public void Find_PlaceForWorkCharacter(int ID_PlaceCharacter)
    {


        //ищем свободное место
        for(int i = 0; i < Max_CharactersAtCar[(int)This_car_ID]; i++)
        {
            //если нашли место
            if (ID_CharactersInThisCar[i] <= 0)
            {
                //даем работу персу
                ID_CharactersInThisCar[i] = ID_PlaceCharacter;

                //обновляем общую количество атрибтов людей в машине
                Update_CharactersAtributes();

                //конец цикла
                i = 99;
              

            }

        }
    }



    //обновить общее количество атрибутов персов
    public void Update_CharactersAtributes()
    {
        //обнуляем счетчик
        CharactersAtributs = 0;


        //сбираем нужный нам атрибут со всех персов
        for (int i = 0; i < Max_CharactersAtCar[(int)This_car_ID]; i++)
        {
            //если есть чел в этом месте
            if (ID_CharactersInThisCar[i] != 0)
            {
                //если Strange
                if ((int)Need_Car_attribute == 1)
                {
                    CharactersAtributs += 
                        New_Human_Character_Script.
                        Prefabs_BaseDate[New_Human_Character_Script.ID_Character_BD_Scripts[ID_CharactersInThisCar[i] - 1]].Strength;
                }
                //если Agility
                if ((int)Need_Car_attribute == 2)
                {
                    CharactersAtributs += 
                        New_Human_Character_Script.
                        Prefabs_BaseDate[New_Human_Character_Script.ID_Character_BD_Scripts[ID_CharactersInThisCar[i] - 1]].Agility;
                }
                //если Intelect
                if ((int)Need_Car_attribute == 3)
                {
                    CharactersAtributs += 
                        New_Human_Character_Script.
                        Prefabs_BaseDate[New_Human_Character_Script.ID_Character_BD_Scripts[ID_CharactersInThisCar[i] - 1]].Intellect;
                }
                //если Charisma
                if ((int)Need_Car_attribute == 4)
                {
                    CharactersAtributs +=
                        New_Human_Character_Script.
                        Prefabs_BaseDate[New_Human_Character_Script.ID_Character_BD_Scripts[ID_CharactersInThisCar[i] - 1]].Сharisma;
                }
            }
        }
        
    }
}
