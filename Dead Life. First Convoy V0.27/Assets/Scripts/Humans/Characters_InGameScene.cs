using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//это главный скрипт персов, через этот скрипт идет апгрейд смерть, улчшения и тд персов
public class Characters_InGameScene : MonoBehaviour
{
    [Header("В какой машине работает чел, 0 значит нигде")]
    public int[] ID_CarVearWorking_Character;
    //это если в будующем захотим сделать прокачку персов + нстроение персов
    //[Header("Скриптовые характеристики")]
    //public int[] Character_LvL;
    //public int[] Character_Strange;
    //public int[] Character_Agility;
    //public int[] Character_Intelect;
    //public int[] Character_Charisma;
    //public int[] Character_Num_Mood;

    [Header("Это для инфы")]
    public UI_Info_Game UI_Info_Game_Script;

    [Header("таблица людей")]
    public GameObject Prefab_Character_BD;
    public GameObject[] Spawn_CharacterList;
    public ScrollRect ScrollRect_ContentScroll;
    [Header("Проверка свободных мест в списке")]
    public int[] CharacterList_INT;

    [Header("скрипт")]
    public New_Human_Character New_Human_Character_Script;
    public GameObject[] Character_BD_List;//если надо будет удалить перса 
    public New_ConvoyCar New_ConvoyCar_Script;

    //это для удалении персов
    private int Plase_this_Info;
    private int This_ID_Info;
    [Header("Вы уверены что хотите удалить перса?")]
    public GameObject AreYouShure;


    //списко людей
    public GameObject CharacterList;

    //Загрузка инфы про людей в списке 
    public void Load_To_BD_Characater(int ID_Character_Place, int ID_Character)
    {
        // создаем Character_BD
        GameObject Character_BD = Instantiate<GameObject>(Prefab_Character_BD);
        //Character_BD.transform.parent = Spawn.transform;

        //Спавн на место
        {
            //ищем свободное место в спавнах
            for (int i = 0; i < CharacterList_INT.Length; i++)
            {
                //если свободно место
                if (CharacterList_INT[i] < 25)
                {
                    Character_BD.transform.SetParent(Spawn_CharacterList[i].transform);
                    //ставим что 1 место занято
                    CharacterList_INT[i]++;

                    //ставим номер контент листа чтоб потом -- при удалении перса
                    Character_BD.GetComponent<Character_BD>().Num_ContentList = i;

                    //остонавливаем цикл
                    i = 999;
                }
            } 
        }
        Character_BD.transform.localScale = new Vector3(1, 1, 1);
        //даем инфу про перса ему
        Character_BD.GetComponent<Character_BD>().Character_BD_Info(New_Human_Character_Script.Prefabs_BaseDate[ID_Character]);
        //даем его ID + место в сохронениии
        Character_BD.GetComponent<Character_BD>().This_ID = ID_Character;
        Character_BD.GetComponent<Character_BD>().Plase_this = ID_Character_Place;
        //даем наш скрипт чтоб если что он смог связаться
        Character_BD.GetComponent<Character_BD>().Characters_InGameScene_Script = this;
        //даем инфу где работает чел
        Character_BD.GetComponent<Character_BD>().Num_Car_INT = PlayerPrefs.GetInt("Character_WorkingAtCar" + ID_Character_Place);
        ID_CarVearWorking_Character[ID_Character_Place - 1] = PlayerPrefs.GetInt("Character_WorkingAtCar" + ID_Character_Place);
        //обновляем инфу про работу
        Character_BD.GetComponent<Character_BD>().Working_AtCar();
        //Даем скрипт конвоя машин
        Character_BD.GetComponent<Character_BD>().New_ConvoyCar_Script = New_ConvoyCar_Script;

        //добовляем в реестр чтоб потом с ним связаться
        New_Human_Character_Script.ID_Character_BD_Scripts[ID_Character_Place - 1] = ID_Character;
        //добавляем в массив тчтоб потом если нужно будет удалить мы его смогли найти и удалить
        Character_BD_List[ID_Character_Place - 1] = Character_BD;
    }


    //уверены что хотите удалить?
    public void DoYouWnotDeleteCharacter(int Plase_this, int This_ID)
    {
        //Включаем кнопку вы увернеы?
        AreYouShure.SetActive(true);

        //получаем инфу проперса
        This_ID_Info = This_ID;
        Plase_this_Info = Plase_this;

    }


    //уведомление что персонаж покинул конвой
    public void Character_Has_Left_Convoy()
    {
        Yes_Iam_Shere();
    }

    //информация о персе что покинул конвой
    public void CharacterINFO_LeftConvoy(int Plase_this, int This_ID)
    {
        //получаем инфу проперса
        This_ID_Info = This_ID;
        Plase_this_Info = Plase_this;
    }

    //удалить перса
    public void Yes_Iam_Shere()
    {
        //если перс работал то уволить
        if (ID_CarVearWorking_Character[Plase_this_Info - 1] > 0)
        {
            //получаем скрипт нужной тачки
            Convoy_Car Convoy_Car_Script = New_ConvoyCar_Script.Cars_InScene
                [PlayerPrefs.GetInt("Character_WorkingAtCar" + Plase_this_Info)].GetComponent<Convoy_Car>();
            //и его макс персов
            int maxCharacterAtCar = Convoy_Car_Script.Max_CharactersAtCar[(int)Convoy_Car_Script.This_car_ID];
            //то ищем его в машине по ID
            for (int i = 0; i < maxCharacterAtCar; i++)
            {

                //если ID перса совпадает то увольняем с работы
                if (Convoy_Car_Script.ID_CharactersInThisCar[i] == Plase_this_Info)
                {
                    //Debug.LogError("В тачке" + Convoy_Car_Script.ID_CharactersInThisCar[i] + "в реале" + Plase_this_Info);
                    Convoy_Car_Script.ID_CharactersInThisCar[i] = 0;


                    //обновляем общую количество атрибтов людей
                    Convoy_Car_Script.Update_CharactersAtributes();

                    //конец цикла
                    i = maxCharacterAtCar;
                }
            }
        }

        //-- из списка свободных мест контент списке
        CharacterList_INT[Character_BD_List[Plase_this_Info - 1].GetComponent<Character_BD>().Num_ContentList]--;



        //удалить чела из списка
        Destroy(Character_BD_List[Plase_this_Info - 1]);

        //+ удалиь его из массива персов что уже есть у игрока
        New_Human_Character_Script.ID_Character_BD_Scripts[Plase_this_Info - 1] = 0;

        //удалить чела из сохранении
        PlayerPrefs.SetInt("ID_Character" + Plase_this_Info, 0);
        PlayerPrefs.Save();

        //удалить место работы чела
        PlayerPrefs.SetInt("Character_WorkingAtCar" + Plase_this_Info, -1);
        PlayerPrefs.Save();


        //-1 из количество людей вообще
        New_Human_Character_Script.HumansInScene_INT--;
        //меняем инфу количество людей
        UI_Info_Game_Script.Upadate_Info_CharactersInConvoy(New_Human_Character_Script.HumansInScene_INT);
    }

    //Ставим актиным скрол контент
    public void ScrollContet_Chenge(int Num)
    {
        //Включаем наш контент скролинг
        for(int i = 0;i< Spawn_CharacterList.Length;i++)
        {
            //включаем нужный переключатель остальные выключаем
            if (i != Num)
            {
                Spawn_CharacterList[i].SetActive(false);
            }
            else
            {
                Spawn_CharacterList[i].SetActive(true);
            }
        }

        //ставим активным скролинг контента листа
        ScrollRect_ContentScroll.content = Spawn_CharacterList[Num].GetComponent<RectTransform>();
    }

    //даем персу работу в машине
    public void SetCharacterWork_InCar_IDPlaceCharacter(int Id_CharacterPlace, Character_BD Character_BD_Scritp)
    {
        
        //выбранная машина
        int ID_TakedCar = New_ConvoyCar_Script.Taked_Object;

        //макс людей в машине
        int MaxPlaceAtCar = New_ConvoyCar_Script.Cars_InScene[ID_TakedCar].GetComponent<Convoy_Car>().
            Max_CharactersAtCar[(int)New_ConvoyCar_Script.Cars_InScene[ID_TakedCar].GetComponent<Convoy_Car>().This_car_ID];
        //проверяем есть ли место 
        {
            for (int i = 0; i < MaxPlaceAtCar; i++)
            {
           
                //проверяем место свободное или нет
                if (New_ConvoyCar_Script.Cars_InScene[ID_TakedCar].GetComponent<Convoy_Car>().ID_CharactersInThisCar[i] < 1)
                {
                    //если свободно ставим перса в это место
                    New_ConvoyCar_Script.Cars_InScene[ID_TakedCar].GetComponent<Convoy_Car>().ID_CharactersInThisCar[i] = Id_CharacterPlace;


                    //обновляем общую количество атрибтов людей в машине
                    New_ConvoyCar_Script.Cars_InScene[ID_TakedCar].GetComponent<Convoy_Car>().Update_CharactersAtributes();

                    //+ сохраняем это 
                    PlayerPrefs.SetInt("Character_WorkingAtCar" + Id_CharacterPlace, ID_TakedCar);//сохроняем в ID тачки 
                    PlayerPrefs.Save();

                    //В самом списке персов меняем перменную этого перса на занят и даем номер тачки в которой работает
                    Character_BD_Scritp.ChengeBool_Work_True(ID_TakedCar);

                    //Даем скрипт конвоя машин
                    Character_BD_Scritp.New_ConvoyCar_Script = New_ConvoyCar_Script;

                    //даем инфу где работает чел
                    Character_BD_Scritp.Num_Car_INT = PlayerPrefs.GetInt("Character_WorkingAtCar" + Id_CharacterPlace);
                    ID_CarVearWorking_Character[Id_CharacterPlace - 1] = PlayerPrefs.GetInt("Character_WorkingAtCar" + Id_CharacterPlace);

                    //Обновляем BD лист
                    Character_BD_Scritp.Working_AtCar();

                    //конец цикла
                    i = 80;
                }


                // говорим  игроку нет места в машине
                if (i == MaxPlaceAtCar - 1)
                {
                    Debug.Log("Нет мест в машине");
                }
            }
        }

    }

    //закрыть список
    public void Off_CharacterList()
    {
        CharacterList.SetActive(false);
    }
}
