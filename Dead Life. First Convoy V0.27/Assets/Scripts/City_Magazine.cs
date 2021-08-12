using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City_Magazine : MonoBehaviour
{
    [Header("Все Товары в магазине")]
    public CityMagazin_BaseDate[] CityMagazin_BaseDate_INFO;

    [Header("Все скрипты товаров в магазине")]
    public LootMagazine_BD[] LootMagazine_BD_Script;

    [Header("Префаб товара в магазине")]
    public GameObject Prefab_Loot_BD;

    [Header("Спацвн куда все это заспавнить")]
    public GameObject Spawn;


    [Header("Скрипты")]
    public Loot_Storage Loot_Storage_Script;
    public Convoy_Controller Convoy_Controller_Script;

    public void Start()
    {
        //создаем все товары (машины) в магазине
        for (int i = 0; i < CityMagazin_BaseDate_INFO.Length; i++)
        {
            Create_Loot_InMagazine(CityMagazin_BaseDate_INFO[i]);
        }

    }

    //Создаем товар (лут) в магазине 
    public void Create_Loot_InMagazine(CityMagazin_BaseDate Bay_Loot_BaseDate)
    {
        //создаем товар (лут) в магизне
        GameObject Loot_InMagazine = Instantiate<GameObject>(Prefab_Loot_BD);
        Loot_InMagazine.transform.SetParent(Spawn.transform);
        Loot_InMagazine.transform.localScale = new Vector3(1, 1, 1);
        //даем инфу про Лут ему
        Loot_InMagazine.GetComponent<LootMagazine_BD>().LootMagazine_BD_Info(Bay_Loot_BaseDate);
        //даем ему свой скрипт
        Loot_InMagazine.GetComponent<LootMagazine_BD>().City_Magazine_Script = this;
        //Ставим его в реестр чтоб потом если что свезаться
        {
            //ищем свободное место
            for (int i = 0; i < LootMagazine_BD_Script.Length; i++)
            {
                //если место свободное то ставим
                if (LootMagazine_BD_Script[i] == null)
                {
                    LootMagazine_BD_Script[i] = Loot_InMagazine.GetComponent<LootMagazine_BD>();

                    //конец цикла
                    i = LootMagazine_BD_Script.Length;
                }
            }

        }
    }


    //проверить хватает ли (валюты) на покупку
    public void Check_Money(int LootCost, ID_Currency ID_Currency_Loot, ID_LootBay ID_Loot_Bay_Whot, int Currency_BayLoot)
    {
        bool HawMany_ToBay = false;
        //проверка хватает лм этой валюты для покупки
        {
            //если в пулях
            if (ID_Currency_Loot == ID_Currency.Bullet)
            {
                if (PlayerPrefs.GetInt("Bullet") >= LootCost)
                {
                    HawMany_ToBay = true;


                    //Забираем плату за покупку тачки
                    PlayerPrefs.SetInt("Bullet", PlayerPrefs.GetInt("Bullet") - LootCost);
                    PlayerPrefs.Save();

                    //Меняем инфу про количество пуль
                    Loot_Storage_Script.Update_Int_Bullet();

                }
            }

            //если в золоте
            if (ID_Currency_Loot == ID_Currency.Gold)
            {
                if (PlayerPrefs.GetInt("Gold") >= LootCost)
                {
                    HawMany_ToBay = true;

                    //Забираем плату за покупку тачки
                    PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") - LootCost);
                    PlayerPrefs.Save();

                    //Меняем инфу про количество золота
                    Loot_Storage_Script.Update_Int_Gold();
                }
            }

            //если в запчастях
            if (ID_Currency_Loot == ID_Currency.SpareParts)
            {
                if (PlayerPrefs.GetInt("SpareParts") >= LootCost)
                {
                    HawMany_ToBay = true;

                    //Забираем плату за покупку тачки
                    PlayerPrefs.SetInt("SpareParts", PlayerPrefs.GetInt("SpareParts") - LootCost);
                    PlayerPrefs.Save();

                    //Меняем инфу про количество запчестей
                    Loot_Storage_Script.Update_Int_SpareParts();
                }
            }
        }


        //если да то покупаем
        if (HawMany_ToBay == true)
        {
            Bought_Loot(ID_Loot_Bay_Whot, Currency_BayLoot);
        }
        else
        //иначе говорим что нет денег
        {
            Debug.Log("Нет Хватает денег милорд");
        }
    }


    //Купили Loot                      что именно              количество
    public void Bought_Loot(ID_LootBay ID_LootBay_NOW, int HowMuch_LootBay)
    {
        //сохроняем + обновляем количество лута 
        {
            //если Water
            if (ID_LootBay_NOW == ID_LootBay.Water)
            {
                //сохроняем
                PlayerPrefs.SetInt("Water", PlayerPrefs.GetInt("Water")+ HowMuch_LootBay);
                PlayerPrefs.Save();

    
                //Обновляем UI об количестве воды
                Loot_Storage_Script.Update_Int_Water();
            }
            //если SpareParts
            if (ID_LootBay_NOW == ID_LootBay.SpareParts)
            {
                //сохроняем
                PlayerPrefs.SetInt("SpareParts", PlayerPrefs.GetInt("SpareParts") + HowMuch_LootBay);
                PlayerPrefs.Save();

                //Обновляем UI об количестве запчастей
                Loot_Storage_Script.Update_Int_SpareParts();
            }
            //если Food
            if (ID_LootBay_NOW == ID_LootBay.Food)
            {
                //сохроняем
                PlayerPrefs.SetInt("Food", PlayerPrefs.GetInt("Food") + HowMuch_LootBay);
                PlayerPrefs.Save();


                //Обновляем UI об количестве еды
                Loot_Storage_Script.Update_Int_Food();
            }
            //если Bullet
            if (ID_LootBay_NOW == ID_LootBay.Bullet)
            {
                //сохроняем
                PlayerPrefs.SetInt("Bullet", PlayerPrefs.GetInt("Bullet") + HowMuch_LootBay);
                PlayerPrefs.Save();

                //Обновляем UI об количестве пуль
                Loot_Storage_Script.Update_Int_Bullet();
            }
            //если Petrol
            if (ID_LootBay_NOW == ID_LootBay.Petrol)
            {
                //сохроняем
                PlayerPrefs.SetInt("Petrol", PlayerPrefs.GetInt("Petrol") + HowMuch_LootBay);
                PlayerPrefs.Save();

                //Изменяем переменную о количестве бензина
                Convoy_Controller_Script.Petrol += HowMuch_LootBay;
            }
            //если Gold
            if (ID_LootBay_NOW == ID_LootBay.Gold)
            {
                //сохроняем
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + HowMuch_LootBay);
                PlayerPrefs.Save();

                //Обновляем UI об количестве золота
                Loot_Storage_Script.Update_Int_Gold();
            }
        }

    }

}
