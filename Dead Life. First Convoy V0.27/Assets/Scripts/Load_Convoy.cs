using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Convoy : MonoBehaviour
{
    [Header("это спавны для конвоя")]
    public GameObject[] Spawns;//макс 50, чтобы  больше 50 тачек небыло

    [Header("Скрипт с префабами и инфой конвоя")]
    public New_ConvoyCar New_ConvoyCar_Scritp;

    //загружаем конвой + позиция
    public void LoadCar_Convoy(int ID_Convoy_Place, int ID_Car)
    {
        // создаем тачку в конвой
        GameObject LoadCar = Instantiate<GameObject>(New_ConvoyCar_Scritp.Cars[ID_Car]);
        LoadCar.transform.position = Spawns[ID_Convoy_Place - 1].transform.position;
        LoadCar.transform.rotation = Spawns[ID_Convoy_Place - 1].transform.rotation;

        //добовляем в реестр чтоб потом с ним связаться
        New_ConvoyCar_Scritp.Cars_InScene[ID_Convoy_Place - 1] = LoadCar;

        //загружаем уровень прокачки тачки
        LoadCar.GetComponent<Convoy_Car>().Car_Num_upgraded = PlayerPrefs.GetInt("Car_Num_upgraded" + ID_Convoy_Place);
        //Ставим агрейды 
        LoadCar.GetComponent<Upgrade_Load>().Activate_Upgrades();
    }
}
