using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Когда игрок купил или создал или нашел новую тачку и теперь нужно сохранить ее
public class New_ConvoyCar : MonoBehaviour
{
    [Header("Все тачки")]
    public GameObject[] Cars;

    [Header("Тачки на сцене")]
    public GameObject[] Cars_InScene;
    public int CarsInScene_INT;

    [Header("Скрипт создания и загрузки конвоя")]
    public Load_Convoy Load_Convoy_Script;
    [Header("Скрипт с инфой для игрока")]
    public UI_Info_Game UI_Info_Game_Script;
    [Header("Скрипт для управляение камерой")]
    public Camera_Control Camera_Control_Script;
    [Header("Выбранная тачка в конвое")]
    public int Taked_Object;

    [Header("Переключатели выбраной тачки")]
    public GameObject Buttom_PlusCar;
    public GameObject Buttom_MinusCar;

    [Header("Скрипты")]
    public New_Human_Character New_Human_Character_Script;
    public Characters_InGameScene Characters_InGameScene_Script;

    void Start()
    {
       
        //Узнать сколько у игрока есть тачек + Загрузка их на сцену
        for(int i = 0; i < Load_Convoy_Script.Spawns.Length+1; i++)
        {
       
            //если есть тачка 
            if (PlayerPrefs.GetInt("ID_Car" + i)>0)
            {
                //Загрузка тачки
                Load_Convoy_Script.LoadCar_Convoy(i,
                    PlayerPrefs.GetInt("ID_Car" + i));
                //обновление инфы скок у игрока тачек
                UI_Info_Game_Script.Upadate_Info_CarsInConvoy(i);
                CarsInScene_INT = i;
               // Debug.Log("Машина"+ i);
            }         
        }

        //грузим какая тачка была последней выбранной
        {
            Taked_Object = PlayerPrefs.GetInt("Num_Taked_CarInConvoy");
            Camera_Control_Script.TakedCar = Taked_Object;
            //чек кнопки до последнего переключение
            Buttoms_UpadteCheeck();
        }

        //даем команду на загрузку всех персов
        {
            New_Human_Character_Script.Loading_Characters();
        }
    }

    //создаем новую машину + сохраняем ее
    public void NewCar_CreatAndSave(int ID_Car)
    {
        //ищем свободное место 
        for (int i = 0; i < Load_Convoy_Script.Spawns.Length+1; i++) 
        {
     
            if (i > 0)
            {
                //если место свободно то создаем тачку
                if (PlayerPrefs.GetInt("ID_Car" + i) <= 0)
                {
                    //Создали тачку
                    GameObject NewCar = Instantiate<GameObject>(Cars[ID_Car]);
                    NewCar.transform.position = Load_Convoy_Script.Spawns[i - 1].transform.position;
                    NewCar.transform.rotation = Load_Convoy_Script.Spawns[i - 1].transform.rotation;

                    //добовляем в реестр чтоб потом с ним связаться
                    Cars_InScene[i - 1] = NewCar;

                    //сохранили тачку
                    PlayerPrefs.SetInt("ID_Car" + i, ID_Car);
                    PlayerPrefs.Save();
                    //обновляем инфу о количестве машин в конвое для игрока
                    UI_Info_Game_Script.Upadate_Info_CarsInConvoy(i);
                    CarsInScene_INT = i;

                    //конец цикла
                    i = Load_Convoy_Script.Spawns.Length + 1;

                    //чек кнопки до последнего переключение
                    Buttoms_UpadteCheeck();
                }
            }
          
        }
    }

    //удалить выбранную машину + уволить всех кто в ней работал
    public void Dellet_TakedCar()
    {
        //тачка что удаляем
        GameObject Car_Deletting = Cars_InScene[Taked_Object];
        //скрипт тачки что удаляем
        Convoy_Car CarDelletong_Script = Car_Deletting.GetComponent<Convoy_Car>();
        //и его макс персов
        int maxCharacterAtCar = CarDelletong_Script.Max_CharactersAtCar[(int)CarDelletong_Script.This_car_ID];

        //Увольняем всех персов в этой тачке
        for (int i = 0; i < maxCharacterAtCar; i++)
        {
            //если место не пустое 
            if (CarDelletong_Script.ID_CharactersInThisCar[i] > 0)
            {

                //сохроняем что не работает чел
                PlayerPrefs.SetInt("Character_WorkingAtCar" + CarDelletong_Script.ID_CharactersInThisCar[i], -1);
                PlayerPrefs.Save();

                //обновляем инфу о работе чела
                Characters_InGameScene_Script.Character_BD_List[CarDelletong_Script.ID_CharactersInThisCar[i] - 1].GetComponent<Character_BD>().Num_Car_INT = -1;
                Characters_InGameScene_Script.Character_BD_List[CarDelletong_Script.ID_CharactersInThisCar[i] - 1].GetComponent<Character_BD>().Working_AtCar();
            }
        }


        //Удаляем тачку из сохранения
        PlayerPrefs.SetInt("ID_Car" + (Taked_Object+1), 0);
        PlayerPrefs.Save();

        //удаляем апгрейды этой тачки
        PlayerPrefs.SetInt("Car_Num_upgraded" + (Taked_Object + 1), 0);
        PlayerPrefs.Save();

        //Удаляем тачку из сцены
        Destroy(Cars_InScene[Taked_Object]);

        //обновляем инфу о количестве машин в конвое для игрока      
        CarsInScene_INT--;
        UI_Info_Game_Script.Upadate_Info_CarsInConvoy(CarsInScene_INT);

    }

    //++ к выбранной тачке
    public void Plus_TakedCar()
    {
        //ищем ближайшую ++ тачку
        for (int i = Taked_Object+1; i < Cars_InScene.Length; i++)
        {
            //если есть тачка
            if (Cars_InScene[i] != null)
            {
                //ставим его как выбранную тачку
                Taked_Object = i;

                //конец цикла
                i = 999;
            }
        
        }
        Camera_Control_Script.TakedCar = Taked_Object;
        PlayerPrefs.SetInt("Num_Taked_CarInConvoy", Taked_Object);
        PlayerPrefs.Save();
        //чек кнопки до последнего переключение
        Buttoms_UpadteCheeck();
    }
    //-- к выбранной тачке
    public void Minus_TakedCar()
    {
        //ищем ближайшую -- тачку
        for (int i = Taked_Object - 1; i > -1; i--)
        {
            //если есть тачка
            if (Cars_InScene[i] != null)
            {
                //ставим его как выбранную тачку
                Taked_Object = i;

                //конец цикла
                i = -1;
            }

        }
        Camera_Control_Script.TakedCar = Taked_Object;
        PlayerPrefs.SetInt("Num_Taked_CarInConvoy", Taked_Object);
        PlayerPrefs.Save();

        //чек кнопки до последнего переключение
        Buttoms_UpadteCheeck();
    }

    //Назначить номер выбранной машины
    public void Change_TakedCar(int ChangeTakedCar)
    {
        Taked_Object = ChangeTakedCar;
        Camera_Control_Script.TakedCar = Taked_Object;
        PlayerPrefs.SetInt("Num_Taked_CarInConvoy", Taked_Object);
        PlayerPrefs.Save();

        //чек кнопки до последнего переключение
        Buttoms_UpadteCheeck();
    }



    //чекаем какая кнопка до последней тачки
    public void Buttoms_UpadteCheeck()
    {
        //проверяем есть ли -- тачка
        {
            //ищем ближайшую -- тачку
         
            for (int i = Taked_Object - 1; i > -2; i--)
            {
                if (i == -1)
                {
                    Buttom_PlusCar.SetActive(false);

                    //конец цикла
                    i = -999;
                }

                if (i > -1)
                {
                    //если есть тачка
                    if (Cars_InScene[i] != null)
                    {
                        Buttom_PlusCar.SetActive(true);

                        //конец цикла
                        i = -999;
                    }
                }

            }
        }

        //проверяем есть ли ++ тачка
        {
            //ищем ближайшую ++ тачку
            for (int i = Taked_Object + 1; i < Cars_InScene.Length+1; i++)
            {
                if (i == Cars_InScene.Length)
                {
                    Buttom_MinusCar.SetActive(false);

                    //конец цикла
                    i = 999;
                }

                if (i < Cars_InScene.Length)
                {
                    //если есть тачка
                    if (Cars_InScene[i] != null)
                    {
                        Buttom_MinusCar.SetActive(true);

                        //конец цикла
                        i = 999;
                    }
                }

            }
        }
    }
}
