using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    [Header("кнопки персонажей")]
    public Image[] Avatar_Image;
    public GameObject[] Avatar_Image_GameObject;

    [Header("кнопки для назначения персонажа на машину")]
    public GameObject[] SetCharacterAtCar;

    [Header("Атрибуты персов в тачке")]
    public Text[] Text_NumAtributes;
    public Image[] BG_Image_NumAtributes;
    [Header("Атрибут требуемой тачки")]
    public GameObject[] Whot_Atribute_CarIs;
    public Image BG_Image_Atribute;
    private Car_attribute Car_attribute_INFO;
    public Color[] Atribute_Color;


    [Header("Скрипты")]
    public New_Human_Character New_Human_Character_Script;
    public New_ConvoyCar New_ConvoyCar_Script;


    //машина которую мы открыли для контролли
    private GameObject Car_Controlling;
    private Convoy_Car Convoy_Car_Script;
    private Squad_Search Squad_Search_Script;
    private Upgrade_Load Upgrade_Load_Script;

    [Header("UI CarController")]
    public GameObject UI_carController;

    [Header("Скрипты")]
    public Characters_InGameScene Characters_InGameScene_Script;
    public Convoy_Controller Convoy_Controller_Script;
    public Loot_Storage Loot_Storage_Script;

    [Header("Осталосб до сбора лута")]
    public Text Delay_Loot;
    public GameObject Collect_Loot;
    public float DelayTimer;
    private int HowMuchLoot;
    public GameObject Text_DElay_Loot;

    [Header("кнопка апгрейда")]
    public GameObject UpgradeButtom;

    [Header("Цена запчастей на апгрейда")]
    public Text UpradeCost_TEXT;

    [Header("Сколько запчастей на апгрейд")]
    public Text HowMuschHaveLoot;

    [Header("Иконка того что собираем")]
    public Sprite[] Sprite_LootTake;
    [Header("Изображение того что собираем")]
    public Image LootTake_Image;

    [Header("Сколько Соберем ресурсов")]
    public Text HowMuchTake_Loot;

    [Header("Уровень прокачки такчи")]
    public Text Car_LVLUpgrade;


    [Header("Инфа про то что и скок добываем")]
    public Text HowMuchLootTakeInfo_TEXT;
    public Image LootTakeInfo_Image;


    //при открытии UI контроллера машины
    public void InfoCar_AndControll()
    {

        //получаем выбраную машину  + его скрипт
        Car_Controlling = New_ConvoyCar_Script.Cars_InScene[New_ConvoyCar_Script.Taked_Object];
        Convoy_Car_Script = Car_Controlling.GetComponent<Convoy_Car>();
        Squad_Search_Script = Car_Controlling.GetComponent<Squad_Search>();
        Upgrade_Load_Script = Car_Controlling.GetComponent<Upgrade_Load>();

        //макс мест в машине
        int CarCharactersMAX = Convoy_Car_Script.Max_CharactersAtCar[(int)Convoy_Car_Script.This_car_ID];

        //что добываем
        LootTakeInfo_Image.sprite = Sprite_LootTake[(int)Squad_Search_Script.Squad_Searching - 1];

        //сколько соберем лута
        {
            //если есть прокачка тачки
            if (Convoy_Car_Script.Car_Num_upgraded > 0)
            {
                //если есть прокачка тачки
                HowMuchLoot = (int)(Squad_Search_Script.How_Many_Found * ((float)Math.Pow(1.5f, Convoy_Car_Script.Car_Num_upgraded)));
                HowMuchLootTakeInfo_TEXT.text = "" + HowMuchLoot;
            }
            //иначе просто добыча
            else
            {
                HowMuchLoot = Squad_Search_Script.How_Many_Found;
                HowMuchLootTakeInfo_TEXT.text = "" + HowMuchLoot;
            }
        }

        //меняем активный атрибут на тот что у машины
        {
            //атрибут тачки самой
            Car_attribute_INFO = Convoy_Car_Script.Need_Car_attribute;

            //цвет атрибута тачки
            BG_Image_Atribute.color = Atribute_Color[(int)Car_attribute_INFO - 1];

            //вкл текст нужный от атрибута + остальные вырубаем
            for (int i = 0; i < Whot_Atribute_CarIs.Length; i++)
            {
                if (i == (int)Car_attribute_INFO - 1)
                {
                    Whot_Atribute_CarIs[i].SetActive(true);
                }
                else
                {
                    Whot_Atribute_CarIs[i].SetActive(false);
                }
            }

        }

        //обновляем активность кнопок в зависимости от макс мест в маишине
        {
            //Вкл всех кнопок
            {
                for (int i = 0; i < Avatar_Image_GameObject.Length; i++)
                {
                    Avatar_Image_GameObject[i].SetActive(true);
                    SetCharacterAtCar[i].SetActive(true);
                }
            }
            //Выкл кнопок == макс мест в машине
            {
                for (int i = CarCharactersMAX; i < Avatar_Image_GameObject.Length; i++)
                {
                    Avatar_Image_GameObject[i].SetActive(false);
                    SetCharacterAtCar[i].SetActive(false);
                }
            }
        }


        //меняем Кнопки персов+ Инфу машины в UI 
        for (int i = 0; i < CarCharactersMAX; i++)
        {
            //вкл выкл лишник кнопок персов
            {
                //если есть чел то вкл
                if (Convoy_Car_Script.ID_CharactersInThisCar[i] > 0)
                {
                    Avatar_Image_GameObject[i].SetActive(true);

                    //меняем Аватар персов для UI
                    Avatar_Image[i].sprite =
                        New_Human_Character_Script.Prefabs_BaseDate[New_Human_Character_Script.ID_Character_BD_Scripts[(Convoy_Car_Script.ID_CharactersInThisCar[i]) - 1]].Avatar;

                    //меняем атрибуты на те что у персов
                    {

                        //если Strange
                        if ((int)Car_attribute_INFO == 1)
                        {
                            Text_NumAtributes[i].text =
                          "" + New_Human_Character_Script.Prefabs_BaseDate[New_Human_Character_Script.ID_Character_BD_Scripts[(Convoy_Car_Script.ID_CharactersInThisCar[i]) - 1]].Strength;
                            //цвет
                            BG_Image_NumAtributes[i].color = Atribute_Color[0];
                        }
                        //если Agility
                        if ((int)Car_attribute_INFO == 2)
                        {
                            Text_NumAtributes[i].text =
                          "" + New_Human_Character_Script.Prefabs_BaseDate[New_Human_Character_Script.ID_Character_BD_Scripts[(Convoy_Car_Script.ID_CharactersInThisCar[i]) - 1]].Agility;
                            //цвет
                            BG_Image_NumAtributes[i].color = Atribute_Color[1];
                        }
                        //если Intelect
                        if ((int)Car_attribute_INFO == 3)
                        {
                            Text_NumAtributes[i].text =
                          "" + New_Human_Character_Script.Prefabs_BaseDate[New_Human_Character_Script.ID_Character_BD_Scripts[(Convoy_Car_Script.ID_CharactersInThisCar[i]) - 1]].Intellect;
                            //цвет
                            BG_Image_NumAtributes[i].color = Atribute_Color[2];
                        }
                        //если Charisma
                        if ((int)Car_attribute_INFO == 4)
                        {
                            Text_NumAtributes[i].text =
                          "" + New_Human_Character_Script.Prefabs_BaseDate[New_Human_Character_Script.ID_Character_BD_Scripts[(Convoy_Car_Script.ID_CharactersInThisCar[i]) - 1]].Сharisma;
                            //цвет
                            BG_Image_NumAtributes[i].color = Atribute_Color[3];
                        }

                    }


                    //Выкл кнопки назначения чела на тачку
                    {
                        SetCharacterAtCar[i].SetActive(false);
                    }
                }
                //иначе выкл
                else
                {
                    Avatar_Image_GameObject[i].SetActive(false);
                    //вкл кнопки для назначения чела
                    {
                        SetCharacterAtCar[i].SetActive(true);
                    }
                }
            }


        }

        //Если можно собрать лут то вкл кнопки иначе выкл
        {
            if (Squad_Search_Script.Wheiting_Collect_Loot==true)
            {
                //ставим иконку того что собираем
                LootTake_Image.sprite = Sprite_LootTake[(int)Squad_Search_Script.Squad_Searching-1];

                //сколько соберем лута
                {
                    //если есть прокачка тачки
                    if (Convoy_Car_Script.Car_Num_upgraded > 0)
                    {
                        //если есть прокачка тачки
                        HowMuchLoot = (int)(Squad_Search_Script.How_Many_Found * ((float)Math.Pow(1.5f, Convoy_Car_Script.Car_Num_upgraded)));
                        HowMuchTake_Loot.text = "" + HowMuchLoot;
                    }
                    //иначе просто добыча
                    else
                    {
                        HowMuchLoot = Squad_Search_Script.How_Many_Found;
                        HowMuchTake_Loot.text = "" + HowMuchLoot;
                    }
                }

                Collect_Loot.SetActive(true);
            }
            else
            {
                Collect_Loot.SetActive(false);
            }
        }

        //если есть атрибуты в тачке
        if (Convoy_Car_Script.CharactersAtributs>0) 
        {
            //Осталось до сбора лута
            DelayTimer = (Squad_Search_Script.Delay_Searching - Squad_Search_Script.Left_Delay_Searhing) * 10;
            Delay_Loot.text = "" + (int)DelayTimer;

            //если уэе готов собрать то выкл 
            if (DelayTimer <= 0)
            {
                Text_DElay_Loot.SetActive(false);
            }
            else
            {
                Text_DElay_Loot.SetActive(true);
            }
        }
        //иначе нет добычи 
        else
        {
            Delay_Loot.text = "" + 999;
        }

        //Проверяем это макс уровень прокачки
        {
            //если нет то вкл кнопку апгрейда 
            if (Convoy_Car_Script.Car_Num_upgraded<Upgrade_Load_Script.Upgrades.Length)
            {
                //цена апгрейда
                UpradeCost_TEXT.text = ""+ Upgrade_Load_Script.Upgrade_Cost * (Convoy_Car_Script.Car_Num_upgraded + 1);

                //сколько есть запчастей на апгрейд
                HowMuschHaveLoot.text = ""+ PlayerPrefs.GetInt("SpareParts");

                UpgradeButtom.SetActive(true);
            }
            //иначе откл кнопку апгрейда
            else
            {
                UpgradeButtom.SetActive(false);
            }
        }

        //Уровень прокачки тачки
        Car_LVLUpgrade.text = "LVL " + Convoy_Car_Script.Car_Num_upgraded+1;

        //Сделать UI контроллера над машиной видимой
        UI_carController.SetActive(true);
    }

    public void Update()
    {
        //UI Активен
        if (UI_carController.activeInHierarchy==true)
        {
            //если есть атрибуты в тачке
            if (Convoy_Car_Script.CharactersAtributs > 0)
            {
                //Осталось до сбора лута
                DelayTimer = (Squad_Search_Script.Delay_Searching - Squad_Search_Script.Left_Delay_Searhing) 
                    / ((float)Convoy_Car_Script.CharactersAtributs / (Squad_Search_Script.CharectersAtCar * 10));
                Delay_Loot.text = "" + ((int)DelayTimer);


                //если уэе готов собрать то выкл 
                if (DelayTimer <= 0)
                {
                    Text_DElay_Loot.SetActive(false);
                }
                else
                {
                    Text_DElay_Loot.SetActive(true);
                }
            }


            //Если можно собрать лут то вкл кнопки иначе выкл
            {
                if (Squad_Search_Script.Wheiting_Collect_Loot == true)
                { 
                    //ставим иконку того что собираем
                    LootTake_Image.sprite = Sprite_LootTake[(int)Squad_Search_Script.Squad_Searching - 1];

                    //сколько соберем лута
                    {
                        //если есть прокачка тачки
                        if (Convoy_Car_Script.Car_Num_upgraded > 0)
                        {
                            //если есть прокачка тачки
                            HowMuchLoot = (int)(Squad_Search_Script.How_Many_Found * ((float)Math.Pow(1.5f, Convoy_Car_Script.Car_Num_upgraded)));
                            HowMuchTake_Loot.text = "" + HowMuchLoot;
                        }
                        //иначе просто добыча
                        else
                        {
                            HowMuchLoot = Squad_Search_Script.How_Many_Found;
                            HowMuchTake_Loot.text = "" + HowMuchLoot;
                        }
                    }

                    Collect_Loot.SetActive(true);
                }
                else
                {
                    Collect_Loot.SetActive(false);
                }
            }
        }

     
    }

    //при нажатии на кнопку персонажа
    public void Character_Buttom(int NumButtom)
    {
        Debug.Log("Персонаж" + Convoy_Car_Script.ID_CharactersInThisCar[NumButtom - 1] + "Чтото сделал");
    }


    //Уволить персонажа
    public void Fire_Character(int NumButtom)
    {
        //место перса 
        int Place_Character = Convoy_Car_Script.ID_CharactersInThisCar[NumButtom - 1];



        //Debug.Log("" + PlayerPrefs.GetInt("Character_WorkingAtCar" + Place_Character));



        //обновляем инфу в самой машине
        {
            //получаем скрипт нужной тачки
            Convoy_Car Convoy_Car_Script_Taked = New_ConvoyCar_Script.Cars_InScene
                [PlayerPrefs.GetInt("Character_WorkingAtCar" + Place_Character)].GetComponent<Convoy_Car>();

            //и его макс персов
            int maxCharacterAtCar = Convoy_Car_Script_Taked.Max_CharactersAtCar[(int)Convoy_Car_Script_Taked.This_car_ID];

            //ищем его в машине по ID
            for (int i = 0; i < maxCharacterAtCar; i++)
            {

                //если ID перса совпадает то увольняем с работы
                if (Convoy_Car_Script_Taked.ID_CharactersInThisCar[i] == Place_Character)
                {
                    Convoy_Car_Script_Taked.ID_CharactersInThisCar[i] = 0;


                    //обновляем общую количество атрибтов людей в машине
                    Convoy_Car_Script_Taked.Update_CharactersAtributes();

                    //конец цикла
                    i = maxCharacterAtCar;
                }
            }

        }


        //сохроняем что не работаем
        PlayerPrefs.SetInt("Character_WorkingAtCar" + Place_Character, -1);
        PlayerPrefs.Save();

        //обновляем инфу о работе
        Characters_InGameScene_Script.Character_BD_List[Place_Character - 1].GetComponent<Character_BD>().Num_Car_INT = -1;
        Characters_InGameScene_Script.Character_BD_List[Place_Character - 1].GetComponent<Character_BD>().Working_AtCar();


        //обновляем UI car controller
        InfoCar_AndControll();

    }


    //Улучшить машину
    public void UpgradeCar()
    {
        //Если хватает запчестей
        if (PlayerPrefs.GetInt("SpareParts") >= Upgrade_Load_Script.Upgrade_Cost * (Convoy_Car_Script.Car_Num_upgraded + 1))
        {
            //то сохроняем уровень улучшения тачки
            PlayerPrefs.SetInt("Car_Num_upgraded" + (New_ConvoyCar_Script.Taked_Object+1), Convoy_Car_Script.Car_Num_upgraded + 1);
            PlayerPrefs.Save();

            //ставим уровень прокачки тачки
            Convoy_Car_Script.Car_Num_upgraded = PlayerPrefs.GetInt("Car_Num_upgraded" + (New_ConvoyCar_Script.Taked_Object+1));
            //Ставим агрейды 
            Car_Controlling.GetComponent<Upgrade_Load>().Activate_Upgrades();




            //Забираем плату за прокачку тачки
            PlayerPrefs.SetInt("SpareParts", PlayerPrefs.GetInt("SpareParts") - Upgrade_Load_Script.Upgrade_Cost * Convoy_Car_Script.Car_Num_upgraded);
            PlayerPrefs.Save();

            //Меняем инфу про количество запчестей
            Loot_Storage_Script.Update_Int_SpareParts();

            //обновляем скок заробатываем лута
            {
                //если есть прокачка тачки
                if (Convoy_Car_Script.Car_Num_upgraded > 0)
                {
                    //если есть прокачка тачки
                    HowMuchLoot = (int)(Squad_Search_Script.How_Many_Found * ((float)Math.Pow(1.5f, Convoy_Car_Script.Car_Num_upgraded)));
                    HowMuchTake_Loot.text = "" + HowMuchLoot;
                    HowMuchLootTakeInfo_TEXT.text = "" + HowMuchLoot;
                }
                //иначе просто добыча
                else
                {
                    HowMuchLoot = Squad_Search_Script.How_Many_Found;
                    HowMuchTake_Loot.text = "" + HowMuchLoot;
                    HowMuchLootTakeInfo_TEXT.text = "" + HowMuchLoot;
                }
            }




            //Проверяем это макс уровень прокачки
            {
                //если нет то вкл кнопку апгрейда 
                if (Convoy_Car_Script.Car_Num_upgraded < Upgrade_Load_Script.Upgrades.Length)
                {
                    //цена апгрейда
                    UpradeCost_TEXT.text = "" + Upgrade_Load_Script.Upgrade_Cost * (Convoy_Car_Script.Car_Num_upgraded + 1);

                    //сколько есть запчастей на апгрейд
                    HowMuschHaveLoot.text = "" + PlayerPrefs.GetInt("SpareParts");

                    UpgradeButtom.SetActive(true);
                }
                //иначе откл кнопку апгрейда
                else
                {
                    UpgradeButtom.SetActive(false);
                }
            }

            //Уровень прокачки тачки
            Car_LVLUpgrade.text = "LVL " + Convoy_Car_Script.Car_Num_upgraded+1;


        }
        //иначе говорим что не хватает то-то то-то
        else
        {
            Debug.Log("Не хватает Запчестей");
        }
    }
    


    //собрать лут
    public void Collect_Loot_Buttom()
    {


        //сохроняем собранный лут + собираем лут
        {

            //если есть прокачка тачки
            if (Convoy_Car_Script.Car_Num_upgraded > 0)
            {
                //если есть прокачка тачки
                HowMuchLoot = (int)(Squad_Search_Script.How_Many_Found * ((float)Math.Pow(1.5f,Convoy_Car_Script.Car_Num_upgraded)));
            }
            //иначе просто добыча
            else
            {
                 HowMuchLoot = Squad_Search_Script.How_Many_Found;
            }



            //вода
            if ((int)Squad_Search_Script.Squad_Searching == 1)
            {

                //сохроняем количество добычи
                PlayerPrefs.SetInt("Water", PlayerPrefs.GetInt("Water") + HowMuchLoot);
                PlayerPrefs.Save();

                //Меняем инфу про количество Воды
                Loot_Storage_Script.Update_Int_Water();

                Debug.Log("Вода" + HowMuchLoot);
            }
            //Запчасти
            if ((int)Squad_Search_Script.Squad_Searching == 2)
            {
                //сохроняем количество добычи
                PlayerPrefs.SetInt("SpareParts", PlayerPrefs.GetInt("SpareParts") + HowMuchLoot);
                PlayerPrefs.Save();

                //Меняем инфу про количество запчестей
                Loot_Storage_Script.Update_Int_SpareParts();

                Debug.Log("Запчасти" + HowMuchLoot);

                //сколько есть запчастей на апгрейд
                {

                    HowMuschHaveLoot.text = "" + PlayerPrefs.GetInt("SpareParts");
                }
            }
            //Бензин
            if ((int)Squad_Search_Script.Squad_Searching == 3)
            {

                //Изменяем переменную о количестве бензина
                Convoy_Controller_Script.Petrol += HowMuchLoot;


                //сохроняем количество добычи
                PlayerPrefs.SetInt("Petrol", PlayerPrefs.GetInt("Petrol") + HowMuchLoot);
                PlayerPrefs.Save();

                Debug.Log("Бензин" + HowMuchLoot);
            }
            //Люди
            if ((int)Squad_Search_Script.Squad_Searching == 4)
            {
                //Команда создания перса
                Create_Character();

                Debug.Log("Люди" + HowMuchLoot);
            }
            //Золото
            if ((int)Squad_Search_Script.Squad_Searching == 5)
            {
                //сохроняем количество добычи
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + HowMuchLoot);
                PlayerPrefs.Save();

                //Меняем инфу про количество Пуль
                Loot_Storage_Script.Update_Int_Gold();

                Debug.Log("Золото" + HowMuchLoot);
            }
            //Еда
            if ((int)Squad_Search_Script.Squad_Searching == 6)
            {
                //сохроняем количество добычи
                PlayerPrefs.SetInt("Food", PlayerPrefs.GetInt("Food") + HowMuchLoot);
                PlayerPrefs.Save();

                //Меняем инфу про количество запчестей
                Loot_Storage_Script.Update_Int_Food();

                Debug.Log("Еда" + HowMuchLoot);
            }
            //Пули
            if ((int)Squad_Search_Script.Squad_Searching == 7)
            {
                //сохроняем количество добычи
                PlayerPrefs.SetInt("Bullet", PlayerPrefs.GetInt("Bullet") + HowMuchLoot);
                PlayerPrefs.Save();

                //Меняем инфу про количество Пуль
                Loot_Storage_Script.Update_Int_Bullet();

                Debug.Log("Пули" + HowMuchLoot);
            }
        }

        //говорим тачке что уже собрали лут
        {
            //обнуляем счетчик сбора
            Squad_Search_Script.Left_Delay_Searhing = 0;
            //говорим что уже собран
            Squad_Search_Script.Wheiting_Collect_Loot = false;

          
        }
      
    }

    //создать перса
    public void Create_Character()
    {
        int i = UnityEngine.Random.Range(1, 1260);
        if (PlayerPrefs.GetInt("ID_Character" + i) == 0)
        {
            New_Human_Character_Script.NewCharacter_CreatAndSave(UnityEngine.Random.Range(1, 1260));
        }
        //если этот чел у игрока уже есть то повторяем 
        else
        {
            Create_Character();
        }
    }
}
