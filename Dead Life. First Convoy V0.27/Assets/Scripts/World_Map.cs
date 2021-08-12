using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Map : MonoBehaviour
{
    [Header("Матка игрока на карте")]
    public GameObject PlayerPosInMap;

    [Header("Цель Куда едем")]
    public GameObject TargetToWay;

    [Header("Дистанция до цели в КМ")]
    public float Distantion_ToTarget_KM;

    [Header("Все локации на карте")]
    public GameObject[] WorldMap_Locations;

    [Header("Отношение города к вам 0-100")]
    public int[] Mood_Of_The_City_To_You;


    [Header("Скрипты")]
    public Convoy_Controller Convoy_Controller_Script;
    public Chenge_LocationsWord_AndGameplayWorld Chenge_LocationsWord_AndGameplayWorld_Script;

    [Header("Кнопка войти в город")]
    public GameObject Buttom_GoToCity;

    [Header("Задершка до сохранения игрока")]
    public float Delay_PlayrWorldMapSave = 5;



    // Start is called before the first frame update
    void Start()
    {
        //загружаем какая была последняя цель карте
        TargetToWay = WorldMap_Locations[PlayerPrefs.GetInt("Last_Map_Target")];

        //загружаем позицию игрока на карте
        PlayerPosInMap.transform.localPosition = new Vector3(PlayerPrefs.GetFloat("WorldMap_Player_X"), PlayerPrefs.GetFloat("WorldMap_Player_Y"), PlayerPrefs.GetFloat("WorldMap_Player_Z"));
    }

    // Update is called once per frame
    void Update()
    {
        //Дистанция до цели
        {
            //если цель есть
            if (TargetToWay != null)
            {
                Distantion_ToTarget_KM = Vector2.Distance(PlayerPosInMap.transform.position, TargetToWay.transform.position);

                //если мы уже дошли до цели
                {
                    //вкл зайти в город или локацию или что там
                    if (Distantion_ToTarget_KM<=0)
                    {
                        Buttom_GoToCity.SetActive(true);
                    }
                    //если не дошли то выкл
                    else
                    {
                        Buttom_GoToCity.SetActive(false);
                    }
                }
            }
        }

        //если мы едем
        if (Convoy_Controller_Script.Going==true)
        {
            //то приближаем игрока к цели
            PlayerPosInMap.transform.localPosition = Vector3.MoveTowards(PlayerPosInMap.transform.localPosition, TargetToWay.transform.localPosition, Time.deltaTime * 5);

            //если уже доехили то останавливаемся
            if (Distantion_ToTarget_KM <= 0)
            {
                Convoy_Controller_Script.Stop_Convoy();
            }
        }

        //сохранение позиции игрока на карте
        {

            if (Delay_PlayrWorldMapSave > 0)
            {
                Delay_PlayrWorldMapSave -= Time.deltaTime * 1;
            }
            else
            {
                Delay_PlayrWorldMapSave = 1.5f;
                //сохранение позции игрока
                {
                  
                    PlayerPrefs.SetFloat("WorldMap_Player_X", PlayerPosInMap.transform.localPosition.x);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("WorldMap_Player_Y", PlayerPosInMap.transform.localPosition.y);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetFloat("WorldMap_Player_Z", PlayerPosInMap.transform.localPosition.z);
                    PlayerPrefs.Save();
                }
            }
        }

    }


    //Ставим новую цель куда ехать
    public void NewTargetToWay(int ID_Taget)
    {
        //ставим саму цель
        TargetToWay = WorldMap_Locations[(int)ID_Taget];
        Debug.Log("Новая цель езды епта");

        //сохраняем цель куда едем
        PlayerPrefs.SetInt("Last_Map_Target", (int)ID_Taget);
        PlayerPrefs.Save();
    }


    //войти в город
    public void GoToCity()
    {
        //команда на вход в город
        Chenge_LocationsWord_AndGameplayWorld_Script.GoToCity(PlayerPrefs.GetInt("Last_Map_Target"));

        //Debug.Log("Вошли в город/локацию/убежище и тд");
    }
}
