using UnityEngine;
using System;
using UnityEngine.UI;

public class Squad_Search : MonoBehaviour
{
    [Header("Что эта машина ищет?")]
    public ID_Squad_Search_Whot Squad_Searching;

    [Header("Длительность поиска")]
    public int Delay_Searching;

    [Header("Остальось длительности посика")]
    public float Left_Delay_Searhing;

    [Header("Ускоренный поиск за валюту")]
    public bool Fust_Seraching_Payed;

    [Header("Все нашли, ждем когда игрок соберет")]
    public bool Wheiting_Collect_Loot;

    [Header("Ускоренный Рискованный поиск")]
    public bool Fust_Dungereus_Searching;

    [Header("Количество добычи")]
    public int How_Many_Found;

    [Header("Людей в машине")]
    public int CharectersAtCar;

    [Header("Копка собрать лут")]
    public GameObject TakeLoot_Buttom;
    public Vector3 offset;
    private Camera cam;
    public Sprite[] TakeLootImage;
    public Image LootImage;

    [Header("Скрипты")]
    public Convoy_Car Convoy_Car_Script;
    public Loot_Storage Loot_Storage_Script;
    public Convoy_Controller Convoy_Controller_Script;
    public New_Human_Character New_Human_Character_Script;

    //количество лута
    private int HowMuchLoot;


    public void Start()
    {
        //получаем камеру основную
        cam = Camera.main;

        //что добывают картинка
        {
            // если вода 1
            if (Squad_Searching == ID_Squad_Search_Whot.Squad_Search_Water)
            {
                //ставим картинку добычи
                LootImage.sprite = TakeLootImage[0];

            }
            // если запчасти 2
            if (Squad_Searching == ID_Squad_Search_Whot.Squad_Search_SpareParts)
            {
                //ставим картинку добычи
                LootImage.sprite = TakeLootImage[1];

            }
            // если бензин 3
            if (Squad_Searching == ID_Squad_Search_Whot.Squad_Search_Petrol)
            {

                //ставим картинку добычи
                LootImage.sprite = TakeLootImage[2];
            }
            // если люди 4
            if (Squad_Searching == ID_Squad_Search_Whot.Squad_Search_People)
            {

                //ставим картинку добычи
                LootImage.sprite = TakeLootImage[3];
            }
            // если золота 5
            if (Squad_Searching == ID_Squad_Search_Whot.Squad_Search_Gold)
            {
                //ставим картинку добычи
                LootImage.sprite = TakeLootImage[4];

            }
            // если еда 6
            if (Squad_Searching == ID_Squad_Search_Whot.Squad_Search_Food)
            {
                //ставим картинку добычи
                LootImage.sprite = TakeLootImage[5];

            }
            // если пули 7
            if (Squad_Searching == ID_Squad_Search_Whot.Squad_Search_Bullet)
            {
                //ставим картинку добычи
                LootImage.sprite = TakeLootImage[6];

            }

        }
    }
    void Update()
    {
        //если еще не наши добычу то ищем
        if (Wheiting_Collect_Loot == false)
        {
            //если в машине есть хотябы 1 атрибут, то начинаем искать добычу
            if (GetComponent<Convoy_Car>().CharactersAtributs > 0)
            {
                //людей в машине
                CharectersAtCar = GetComponent<Convoy_Car>().Max_CharactersAtCar[(int)GetComponent<Convoy_Car>().This_car_ID];

                //++ к времени до сбора добычи               
               Left_Delay_Searhing += Time.deltaTime * ((float)GetComponent<Convoy_Car>().CharactersAtributs / (CharectersAtCar * 10));
       

            }

            //если уже можно собрать добычу
            if (Left_Delay_Searhing >= Delay_Searching)
            {
                //говорим что пора собирать добычу
                Wheiting_Collect_Loot = true;
            }

            //выключаем кнопку собрать лут
            TakeLoot_Buttom.SetActive(false);
        }
        //иначе включим кнопку собрать лут
        else
        {
            //вкл кнопки собрать лут
            TakeLoot_Buttom.SetActive(true);

            //ставим кнопку на позицию тачки
            {
                Vector3 pos = cam.WorldToScreenPoint(this.transform.position + offset);

                if (TakeLoot_Buttom.transform.position != pos)
                    TakeLoot_Buttom.transform.position = pos;
            }
        }
    }

    //собрать лут
    public void Car_Collect_Loot_Buttom()
    {
        //сохроняем собранный лут + собираем лут
        {

            //если есть прокачка тачки
            if (Convoy_Car_Script.Car_Num_upgraded > 0)
            {
                //если есть прокачка тачки
                HowMuchLoot = (int)(How_Many_Found * ((float)Math.Pow(1.5f, Convoy_Car_Script.Car_Num_upgraded)));
            }
            //иначе просто добыча
            else
            {
                HowMuchLoot = How_Many_Found;
            }



            //вода
            if ((int)Squad_Searching == 1)
            {

                //сохроняем количество добычи
                PlayerPrefs.SetInt("Water", PlayerPrefs.GetInt("Water") + HowMuchLoot);
                PlayerPrefs.Save();

                //Меняем инфу про количество Воды
                Loot_Storage_Script.Update_Int_Water();

                Debug.Log("Вода" + HowMuchLoot);
            }
            //Запчасти
            if ((int)Squad_Searching == 2)
            {
                //сохроняем количество добычи
                PlayerPrefs.SetInt("SpareParts", PlayerPrefs.GetInt("SpareParts") + HowMuchLoot);
                PlayerPrefs.Save();

                //Меняем инфу про количество запчестей
                Loot_Storage_Script.Update_Int_SpareParts();

                Debug.Log("Запчасти" + HowMuchLoot);
            }
            //Бензин
            if ((int)Squad_Searching == 3)
            {

                //Изменяем переменную о количестве бензина
                Convoy_Controller_Script.Petrol += HowMuchLoot;


                //сохроняем количество добычи
                PlayerPrefs.SetInt("Petrol", PlayerPrefs.GetInt("Petrol") + HowMuchLoot);
                PlayerPrefs.Save();

                Debug.Log("Бензин" + HowMuchLoot);
            }
            //Люди
            if ((int)Squad_Searching == 4)
            {
                //Команда создания перса
                Create_Character();

                Debug.Log("Люди" + HowMuchLoot);
            }
            //Золото
            if ((int)Squad_Searching == 5)
            {
                //сохроняем количество добычи
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + HowMuchLoot);
                PlayerPrefs.Save();

                //Меняем инфу про количество Пуль
                Loot_Storage_Script.Update_Int_Gold();

                Debug.Log("Золото" + HowMuchLoot);
            }
            //Еда
            if ((int)Squad_Searching == 6)
            {
                //сохроняем количество добычи
                PlayerPrefs.SetInt("Food", PlayerPrefs.GetInt("Food") + HowMuchLoot);
                PlayerPrefs.Save();

                //Меняем инфу про количество запчестей
                Loot_Storage_Script.Update_Int_Food();

                Debug.Log("Еда" + HowMuchLoot);
            }
            //Пули
            if ((int)Squad_Searching == 7)
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
            Left_Delay_Searhing = 0;
            //говорим что уже собран
            Wheiting_Collect_Loot = false;


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
