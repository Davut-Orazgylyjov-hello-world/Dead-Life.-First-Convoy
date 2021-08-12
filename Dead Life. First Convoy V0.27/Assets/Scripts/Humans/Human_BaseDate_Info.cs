using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Human_BaseDate_Info", menuName = "Human_BaseDate_Info")]
public class Human_BaseDate_Info : ScriptableObject
{
    [Header("Пол Мужской?")]
    public bool Man;

    [Header("Имя")]
    public string Name;
    [Header("Фамилия")] 
    public string Surname;

    [Header("Иконка перса")]
    public Sprite Avatar;

    //чем выше уровень тем выше стоимость и редкость перса
    [Header("Уровень")]
    public int LvL;

    //от уровня зависит характеристика. LvL рандомных характеристик 
    [Header("Характиеристики")]
    public int Strength;//запас здоровье, урон
    public int Agility;//шанс уворачиваться,скорость рейдов
    public int Intellect;//скорость создание чего либо, скорость взлома компов
    public int Сharisma;//Увеличивает шанс найти людей, и скорость радио вызова людей

}
