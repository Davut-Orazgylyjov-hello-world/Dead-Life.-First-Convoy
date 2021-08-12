using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Convoy_Controller : MonoBehaviour
{
    [Header("Запуск + Остановить конвой")]
    public GameObject Buttom_Stop;
    public GameObject Buttom_Start;

    [Header("Аниамция Мира езда")]
    public Animator InfitinityRoad_Anim;

    [Header("Анимации колес")]
    public Animator[] Wheels;

    [Header("Скорость мира")]
    public float WorldSpeed;
    public float MaxWorldSpeed;

    [Header("мы Едим?")]
    public bool Going;

    [Header("Бензин")]
    public float Petrol;

    [Header("Скрипт с инфой")]
    public New_ConvoyCar New_ConvoyCar_Script;


    [Header("Это для инфы")]
    public UI_Info_Game UI_Info_Game_Script;

    [Header("Карта Визуал")]
    public GameObject MapVisual_OBJ;


    [Header("Скрипты")]
    public World_Map World_Map_Script;

    [Header("Одометр")]
    public Text Odometer;
    public int Odometer_Int;
    public float Delay_OdometrKM;

    // Start is called before the first frame update
    void Start()
    {
        //узнаем перед выходом мы ехали или стояли + сколько у нас бело бензина
        {
            //скок было бензина
            Petrol = PlayerPrefs.GetInt("Petrol");

            //ехали или стояли
            if (PlayerPrefs.GetInt("Going") == 1)
            {
                if (Petrol > 0)
                {
                    Going = true;
                    //ставим скорость мира сразу на макс
                    WorldSpeed = MaxWorldSpeed;
                }
                else
                {
                    Going = false;
                }
            }
            else
            {
                Going = false;
            }

       
            //обновляем активность кнопок
            ChengeButtom();
        }

        //Загружаем одометр инфу
        Odometer_Int = PlayerPrefs.GetInt("Odometer");
        //обновялем счетчикинфу
        Odometer.text = "" + Odometer_Int;
    }



    //++ колес анимации
    public void Animator_Wheels(Animator Animator_Wheals)
    {
        //ищем свободное место для тачки
        for (int i = 0; i < Wheels.Length; i++)
        {
            //если нашли свободное место
            if (Wheels[i] == null)
            {
                //ставим туда анмаитор
                Wheels[i] = Animator_Wheals;

                //конец цикла
                i = Wheels.Length;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
      
        //скорость мира 
        {
            //если еще не поменяли анимацию то меняем
            if (InfitinityRoad_Anim.speed!= WorldSpeed)
            {
              
                //аним колес
                for (int i = 0; i < Wheels.Length; i++)
                {

                    //если есть аниматор то меняем его скорость анимации
                    if (Wheels[i] != null)
                    {
                        Wheels[i].speed = WorldSpeed;
                    }
                }

                //аним скорость мира бесконечного
                InfitinityRoad_Anim.speed = WorldSpeed;
            }


            //если едим
            if (Going == true)
            {
                //меняем скорость мира ++
                {
                    if (WorldSpeed < MaxWorldSpeed)
                    {
                        WorldSpeed += Time.deltaTime * 0.3f;
                    }
                    if (WorldSpeed > MaxWorldSpeed)
                    {
                        WorldSpeed = MaxWorldSpeed;
                    }
                }
            }
            else
            //если стоим
            {
                //меняем скорость мира --
                {
                    if (WorldSpeed > 0)
                    {
                        WorldSpeed -= Time.deltaTime * 0.5f;
                    }
                    if (WorldSpeed < 0)
                    {
                        WorldSpeed = 0;
                    }
                }
            }
        }

        //Бензин
        {
            //если нет бензина то остонавливаем конвой
            if (Petrol <= 0)
            {
                Going = false;
                //обновляем активность кнопок
                ChengeButtom();
              //  Debug.Log("Бак пуст милорд");
            }

            if (Petrol < 0)
            {
                Petrol = 0;
            }


            //-- бензин во время езды
            {
                //если мы едим
                if (Going == true)
                {
                    //бензин - бензин в минуту * количество машин
                    Petrol -= (0.016f * (float)New_ConvoyCar_Script.CarsInScene_INT) * Time.deltaTime;

                    //++ проехали км 60 сек = 1км
                    {
                        //+=0.016f;
                    }     
                }
            }

            //Сохарнить инфу про количество бензина
            {
                PlayerPrefs.SetInt("Petrol", (int)Petrol);
                PlayerPrefs.Save();
            
            }

            //Обновить инфу про количество бензина
            {
                UI_Info_Game_Script.Update_Info_Petrol((int)Petrol);
            }


        }

        //Одометр
        {
            //если едут
            if (Going==true) 
            {
                if (Delay_OdometrKM > 0)
                {
                    Delay_OdometrKM -= Time.deltaTime * 1;
                }
                else
                {
         

                    //+1 проехали км в одометр
                    Odometer_Update();

                    Delay_OdometrKM = 5;
                }
            }

           
        }
    }

    //Остановить конвой
    public void Stop_Convoy()
    {

        Going = false;
        //обновляем активность кнопок
        ChengeButtom();

    }


    //Запустить конвой
    public void Start_Convoy()
    {

        //если хватает бензина
        if (Petrol > 0)
        {
            //если есть цель куда ехать + мы еще не доехали
            if (World_Map_Script.TargetToWay != null && World_Map_Script.Distantion_ToTarget_KM>0)
            {
                //то едим
                Going = true;
                //обновляем активность кнопок
                ChengeButtom();
            }
            //иначе открываем карту
            else
            {
                MapVisual_OBJ.SetActive(true);
            }
        }
        else
        //иначе говорим нету бензина
        {
            Debug.Log("Нет бензина милорд");
        }

    }

    public void ChengeButtom()
    {
        //переключение активностей кнопок
        {
            if (Going == true)
            {
                //ставим активность кнопки
                Buttom_Stop.SetActive(true);
                Buttom_Start.SetActive(false);
                //сохраняем инфу
                PlayerPrefs.SetInt("Going", 1);
                PlayerPrefs.Save();
            }
            else
            {
                //ставим активность кнопки
                Buttom_Start.SetActive(true);
                Buttom_Stop.SetActive(false);
                //сохраняем инфу
                PlayerPrefs.SetInt("Going", 0);
                PlayerPrefs.Save();
            }
        }
    }

    //Обновляение одометра 
    public void Odometer_Update()
    {
        //Сохроняем инфу о колчиестве пройденного расстояния  //+1 проехали км
        PlayerPrefs.SetInt("Odometer", PlayerPrefs.GetInt("Odometer")+1);

        //Загружаем одометр инфу
        Odometer_Int = PlayerPrefs.GetInt("Odometer");

        //обновляем счетчик инфу
        Odometer.text = "" + Odometer_Int;

    }

}
