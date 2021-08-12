using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls_ChengeCar : MonoBehaviour
{
    public New_ConvoyCar New_ConvoyCar_SCRIPT;

   
    public void Next_Car()
    {
        New_ConvoyCar_SCRIPT.Plus_TakedCar();
    }

    public void Back_Car()
    {
        New_ConvoyCar_SCRIPT.Minus_TakedCar();
    }
}
