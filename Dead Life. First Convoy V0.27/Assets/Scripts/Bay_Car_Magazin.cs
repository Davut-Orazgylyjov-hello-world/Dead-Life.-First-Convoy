using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bay_Car_Magazin : MonoBehaviour
{
    [Header("Все машины в магазине")]
    public Bay_Car_BaseDate_Info[] Bay_Car_BaseDate_Info_MagazinCars;

    [Header("Все скрипты товаров в магазине")]
    public CarMagazine_BD[] CarMagazine_BD_Script;

    [Header("Префаб товара в магазине")]
    public GameObject Prefab_Car_BD;

    [Header("Спацвн куда все это заспавнить")]
    public GameObject Spawn;


    [Header("Скрипты")]
    public New_ConvoyCar New_ConvoyCar_Script;
    public Loot_Storage Loot_Storage_Script;

    public void Start()
    {
        //создаем все товары (машины) в магазине
        for(int i = 0; i< Bay_Car_BaseDate_Info_MagazinCars.Length; i++)
        {
            Create_Car_InMagazine(Bay_Car_BaseDate_Info_MagazinCars[i]);
        }

    }

    //Создаем товар (машину) в магазине 
    public void Create_Car_InMagazine(Bay_Car_BaseDate_Info Bay_Car_BaseDate)
    {
        //создаем товар (машину) в магизне
        GameObject Car_InMagazine = Instantiate<GameObject>(Prefab_Car_BD);
        Car_InMagazine.transform.SetParent(Spawn.transform);
        Car_InMagazine.transform.localScale = new Vector3(1, 1, 1);
        //даем инфу про тачк ему
        Car_InMagazine.GetComponent<CarMagazine_BD>().CarMagazine_BD_Info(Bay_Car_BaseDate);
        //даем ему свой скрипт
        Car_InMagazine.GetComponent<CarMagazine_BD>().Bay_Car_Magazin_Script=this;
        //Ставим его в реестр чтоб потом если что свезаться
        {
            //ищем свободное место
            for(int i = 0; i < CarMagazine_BD_Script.Length; i++)
            {
                //если место свободное то ставим
                if (CarMagazine_BD_Script[i] == null)
                {
                    CarMagazine_BD_Script[i] = Car_InMagazine.GetComponent<CarMagazine_BD>();

                    //конец цикла
                    i = CarMagazine_BD_Script.Length;
                }
            }
       
        }
    }


    //проверить хватает ли (валюты) на покупку
    public void Check_Money(int CarCost, ID_Currency ID_Currency_Car, ID_Car ID_Car_Bay)
    {
        bool HawMany_ToBay = false;
        //проверка хватает лм этой валюты для покупки
        {
            //если в пулях
            if (ID_Currency_Car == ID_Currency.Bullet)
            {             
                if (PlayerPrefs.GetInt("Bullet") >= CarCost)
                {
                    HawMany_ToBay = true;


                    //Забираем плату за покупку тачки
                    PlayerPrefs.SetInt("Bullet", PlayerPrefs.GetInt("Bullet") - CarCost);
                    PlayerPrefs.Save();

                    //Меняем инфу про количество пуль
                    Loot_Storage_Script.Update_Int_Bullet();

                }
            }

            //если в золоте
            if (ID_Currency_Car == ID_Currency.Gold)
            {
                if (PlayerPrefs.GetInt("Gold") >= CarCost)
                {
                    HawMany_ToBay = true;

                    //Забираем плату за покупку тачки
                    PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - CarCost);
                    PlayerPrefs.Save();

                    //Меняем инфу про количество золота
                    Loot_Storage_Script.Update_Int_Gold();
                }
            }

            //если в запчастях
            if (ID_Currency_Car == ID_Currency.SpareParts)
            {
                if (PlayerPrefs.GetInt("SpareParts") >= CarCost)
                {
                    HawMany_ToBay = true;

                    //Забираем плату за покупку тачки
                    PlayerPrefs.SetInt("SpareParts", PlayerPrefs.GetInt("SpareParts") - CarCost);
                    PlayerPrefs.Save();

                    //Меняем инфу про количество запчестей
                    Loot_Storage_Script.Update_Int_SpareParts();
                }
            }
        }


        //если да то покупаем
        if (HawMany_ToBay == true)
        {
            Bought_Car(ID_Car_Bay);
        }
        else
        //иначе говорим что нет денег
        {
            Debug.Log("Нет Хватает денег милорд");
        }
    }


    //Купили машину
    public void Bought_Car(ID_Car ID_Car_Bay_NOW)
    {
        New_ConvoyCar_Script.NewCar_CreatAndSave((int)ID_Car_Bay_NOW);
    }



}
