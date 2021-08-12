using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//этот скрипт для зугрски улучшений этой тачки
public class Upgrade_Load : MonoBehaviour
{
    [Header("улучшения")]
    public GameObject[] Upgrades;

    [Header("Стоимость улучшния*на уровень прокачки")]
    public int Upgrade_Cost;

    //вкл улучшение
    public void Activate_Upgrades()
    {
        //вкл все апгрейды
        for(int i = 0; i < this.GetComponent<Convoy_Car>().Car_Num_upgraded; i++)
        {
            //если есть апгрейд
            if (this.GetComponent<Convoy_Car>().Car_Num_upgraded > -1)
            {
                //вкл его
                Upgrades[i].SetActive(true);
            }
        }
     
    }
}
