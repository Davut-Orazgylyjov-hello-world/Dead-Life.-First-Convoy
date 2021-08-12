using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Human_Character : MonoBehaviour
{
    [Header("Все BD Людей")]
    public Human_BaseDate_Info[] Prefabs_BaseDate;

    [Header("Id Людей что уже у игрока")]
    public int[] ID_Character_BD_Scripts;

    [Space]
    public int HumansInScene_INT;


    [Header("Скрипт с инфой для игрока")]
    public UI_Info_Game UI_Info_Game_Script;


    [Header("Хранилише персов в игры")]
    public Characters_InGameScene Characters_InGameScene_Script;

    [Header("Скрипты")]
    public New_ConvoyCar New_ConvoyCar_Script;
    public Loot_Storage Loot_Storage_Script;

    //еда задершка потребления
    private float Food_Delay;
    //вода задершка потребления
    private float Water_Delay;

    void Start()
    {

     
    }

    public void Update()
    {
        //--еда и вода
        {
            //если есть еда
            if (Loot_Storage_Script.Food>0)
            {
                
                if (Food_Delay >= 1)
                {
                    //сохроняем количество еды --
                    PlayerPrefs.SetInt("Food", PlayerPrefs.GetInt("Food")-1);
                    PlayerPrefs.Save();
                    //обновление количества еды
                    Loot_Storage_Script.Update_Int_Food();

                    //обнуляем 
                    Food_Delay = 0;
                }
                else
                {
                    Food_Delay += Time.deltaTime * (float)(0.03f * (HumansInScene_INT));
                }
            }

            //если есть вода
            if (Loot_Storage_Script.Water > 0)
            {
                if (Water_Delay >= 1)
                {
                    //сохроняем количество воды --
                    PlayerPrefs.SetInt("Water", PlayerPrefs.GetInt("Water") - 1);
                    PlayerPrefs.Save();
                    //обновление количества воды
                    Loot_Storage_Script.Update_Int_Water();
                    //обнуляем 
                    Water_Delay = 0;
                }
                else
                {
                    Water_Delay += Time.deltaTime * (float)(0.03f * (HumansInScene_INT));
                }
            }
        }

    }

    //Загрузить всех персов после того как все машину будут прогружены
    public void Loading_Characters()
    {
          //Узнать сколько у игрока есть людей + Загрузка их на сцену + загрузка на тачки
        for (int i = 0; i < 200 + 1; i++)
        {
            if (i > 0)
            {
                //если есть человек 
                if (PlayerPrefs.GetInt("ID_Character" + i) > 0)
                {
                    //Debug.Log("Место номер" + i+"ID character"+ PlayerPrefs.GetInt("ID_Character" + i));
                    //Загрузить людей
                    Characters_InGameScene_Script.Load_To_BD_Characater(i,
                        PlayerPrefs.GetInt("ID_Character" + i));
                    //обновление инфы скок у игрока людей                
                    HumansInScene_INT++;
                    UI_Info_Game_Script.Upadate_Info_CharactersInConvoy(HumansInScene_INT);

                    //ставим его на рабочее место если он до этого не работал
                    if (PlayerPrefs.GetInt("Character_WorkingAtCar" + i) > -1)
                    {
                        //Debug.Log("" + PlayerPrefs.GetInt("Character_WorkingAtCar" + i));
                       New_ConvoyCar_Script.Cars_InScene[PlayerPrefs.GetInt("Character_WorkingAtCar" + i)].GetComponent<Convoy_Car>().Find_PlaceForWorkCharacter(i);                           
                    }

                }

                //Debug.Log("i = " + i);
            }
        }
    }




    //создаем нового чела + сохраняем ее
    public void NewCharacter_CreatAndSave(int ID_Character)
    {
        //ищем свободное место 
        for (int i = 0; i < 200 + 1; i++)
        {
            if (i > 0)
            {

                //если место свободно то создаем чела
                if (PlayerPrefs.GetInt("ID_Character" + i) <= 0)
                {
                    //Debug.Log("Место номер" + i);

                    // создаем Character_BD
                    GameObject Character_BD = Instantiate<GameObject>(Characters_InGameScene_Script.Prefab_Character_BD);
                    //Character_BD.transform.parent = Characters_InGameScene_Script.Spawn.transform;


                    //Спавн на место
                    {
                        //ищем свободное место в спавнах
                        for (int j = 0; j < Characters_InGameScene_Script.CharacterList_INT.Length; j++)
                        {
                            //если свободно место
                            if (Characters_InGameScene_Script.CharacterList_INT[j] < 25)
                            {
                                Character_BD.transform.parent = Characters_InGameScene_Script.Spawn_CharacterList[j].transform;
                                //ставим что 1 место занято
                                Characters_InGameScene_Script.CharacterList_INT[j]++;

                                //ставим номер контент листа чтоб потом -- при удалении перса
                                Character_BD.GetComponent<Character_BD>().Num_ContentList = j;

                                //остонавливаем цикл
                                j = 999;
                            }
                        }
                    }

                    Character_BD.transform.localScale = new Vector3(1, 1, 1);
                    //даем инфу про перса ему
                    Character_BD.GetComponent<Character_BD>().Character_BD_Info(Prefabs_BaseDate[ID_Character]);
                    //добовляем в реестр чтоб потом с ним связаться
                    ID_Character_BD_Scripts[i - 1] = ID_Character;
                    //добавляем в массив тчтоб потом если нужно будет удалить мы его смогли найти и удалить
                    Characters_InGameScene_Script.Character_BD_List[i - 1] = Character_BD;
                    //даем его ID + место в сохронениии
                    Character_BD.GetComponent<Character_BD>().This_ID = ID_Character;
                    Character_BD.GetComponent<Character_BD>().Plase_this = i;
                    //даем скрипт Characters_InGameScene_Script чтоб если что он смог связаться
                    Character_BD.GetComponent<Character_BD>().Characters_InGameScene_Script = Characters_InGameScene_Script;
                    //даем инфу где работает чел
                    Character_BD.GetComponent<Character_BD>().Num_Car_INT = -1;
                    Characters_InGameScene_Script.ID_CarVearWorking_Character[i - 1] = -1;
                    //сохраняем место работы чела
                    //так как это новый перс то ставыим -1 так как у него еще нету работы в тачке, остальные числа это ID машины в которой работаетчел
                    PlayerPrefs.SetInt("Character_WorkingAtCar" + i, -1);
                    PlayerPrefs.Save();
                    //Даем скрипт конвоя машин
                    Character_BD.GetComponent<Character_BD>().New_ConvoyCar_Script = New_ConvoyCar_Script;
                    //сохранили чела
                    PlayerPrefs.SetInt("ID_Character" + i, ID_Character);
                    PlayerPrefs.Save();

                    //обновляем инфу о количестве людей в конвое для игрока             
                    HumansInScene_INT++;
                    UI_Info_Game_Script.Upadate_Info_CharactersInConvoy(HumansInScene_INT);

                    //Обновляем BD лист
                    Character_BD.GetComponent<Character_BD>().Working_AtCar();


                    //конец цикла
                    i = 200 + 1;

                }
            }

        }
    }
}
