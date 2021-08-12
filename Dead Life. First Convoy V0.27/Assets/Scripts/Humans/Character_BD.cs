using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_BD : MonoBehaviour
{
    [Header("Визуальные характеристики")]
    public Image Avatar;
    public Text LvL;
    public Text Name_Surname;
    public Text Strange;
    public Text Agility;
    public Text Intelect;
    public Text Charisma;

    [Header("Хранилища инфы всех людей у игрока")]
    public Characters_InGameScene Characters_InGameScene_Script;
    public int This_ID;
    public int Plase_this;

    [Header("В какой машине работает")]
    public int Num_Car_INT;
    public GameObject Buttom_Find;
    public GameObject Buttom_Work;


    [Header("Именно наш чел")]
    public Human_BaseDate_Info Human_BaseDate_Info_INFO;

    [Header("Скрипты")]
    public New_ConvoyCar New_ConvoyCar_Script;

    [Header("Номер в списке контента")]
    public int Num_ContentList;

    //получаем инфу про перса
    public void Character_BD_Info(Human_BaseDate_Info Character_BD_Script)
    {
        Human_BaseDate_Info_INFO = Character_BD_Script;

        //ID машины в каторой работаем
        Num_Car_INT = PlayerPrefs.GetInt("Character_WorkingAtCar" + Plase_this);

        //грузим наши характеристики
        {
            Avatar.sprite = Human_BaseDate_Info_INFO.Avatar;

            Name_Surname.text = Human_BaseDate_Info_INFO.Name +
                " " + Human_BaseDate_Info_INFO.Surname;

            LvL.text = "" + Human_BaseDate_Info_INFO.LvL;

            Strange.text = "" + Human_BaseDate_Info_INFO.Strength;

            Agility.text = "" + Human_BaseDate_Info_INFO.Agility;

            Intelect.text = "" + Human_BaseDate_Info_INFO.Intellect;

            Charisma.text = "" + Human_BaseDate_Info_INFO.Сharisma;

        }

        ////получаем инфу работает ли чел гдето
        //Working_AtCar();
    }


    //работаем ли мы в тачке или нет
    public void Working_AtCar()
    {

        //если гдето работаем то вкл кнопки переключение на тачку в которой перс находится
        if (Num_Car_INT > -1)
        {
            Buttom_Find.SetActive(true);
            Buttom_Work.SetActive(false);//кнопка найти работу
           // Debug.Log("Работает в машине" + Num_Car_INT);
        }
        else
        {
            Buttom_Find.SetActive(false);
            Buttom_Work.SetActive(true);//кнопка найти работу
           // Debug.Log("НЕ Работает"+ Num_Car_INT);
        }

    }

    //Назначить на работу
    public void SetCharacter_CarWork()
    {
        Characters_InGameScene_Script.SetCharacterWork_InCar_IDPlaceCharacter(Plase_this, this);
    }


    //отправить запрос на удаление перса
    public void DoYouwontDelete_ThisCharacter()
    {
        Characters_InGameScene_Script.DoYouWnotDeleteCharacter(Plase_this, This_ID);
    }

    //Найти человека в машину
    public void Find_CharacterAtScene()
    {
        //назначаем номер машины которой мы выбраны
        New_ConvoyCar_Script.Change_TakedCar(Num_Car_INT);
        //выключить список
        Characters_InGameScene_Script.Off_CharacterList();

    }





    //вкл переменной что работаем уже
    public void ChengeBool_Work_True(int ID_CarWorking)
    {
        //сохроняем что рабортаем в машине ID
        PlayerPrefs.SetInt("Character_WorkingAtCar" + Plase_this, ID_CarWorking);
        PlayerPrefs.Save();

        //обновляем инфу о работе
        Working_AtCar();
    }

  
}
