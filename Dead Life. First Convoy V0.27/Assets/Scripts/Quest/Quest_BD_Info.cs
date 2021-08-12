using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest_BD_Info", menuName = "Quest_BD_Info")]
public class Quest_BD_Info : ScriptableObject
{
    [Header("ID локации откуда везти")]
    public ID_WorldMapLocation From;

    [Header("ID локации куда везти")]
    public ID_WorldMapLocation Where;

    [Header("Что в награду получем")]
    public ID_LootBay What_reward;

    [Header("Количество вознограждения получим")]
    public int Reward;

    [Header("Описание задания")]
    public string Quest_Info;

    [Header("Тип задания")]
    public ID_Quest_Type Quest_Type;
}
