using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_Night_Time : MonoBehaviour
{
    public int Hours;

    [Header("Вкл Выкл Света")]
    public Light Day;
    public Light Night;

    [Header("Цвет солнца и луны")]
    public Color[] Color_Sun_Night;


    void Start()
    {
        //получить время устройства
        {
            string HH = System.DateTime.UtcNow.ToLocalTime().ToString("HH");
            Hours = (Convert.ToInt32(HH))+1;

        }
        //Изменить положение Солнца
        {

            float i = (14.4f * Hours);
            this.transform.rotation = Quaternion.Euler(i - 110, 0, 0);
            //Смена света в зависимости от времени
            {

                //день
                if (Hours >= 6 && Hours < 22)
                {
                    Day.enabled = true;
                    Night.enabled = false;
                    //солнце цвет света
                    Day.color = Color_Sun_Night[Hours];

                }
                //ночь
                if (Hours >= 22 || Hours < 6)
                {
                    Day.enabled = false;
                    Night.enabled = true;
                    //луна цвет света
                    Night.color = Color_Sun_Night[Hours];
                }
            }
        }
    }
}
