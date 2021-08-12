using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chenge_LocationsWord_AndGameplayWorld : MonoBehaviour
{
    [Header("Локации")]
    public GameObject[] LocationWorld;

    [Header("Мы сейчас в городе?")]
    public bool WeAreInLocation;

    public int ID_WorldMapLocation_Now;

    [Header("Интерфейс города")]
    public GameObject City_UI;

    [Header("Скрипты")]
    public GameMechanics_Quests GameMechanics_Quests_Script;
    public Quests_InGameScene Quests_InGameScene_Script;
    public Loot_Storage Loot_Storage_Script;
    public Convoy_Controller Convoy_Controller_Script;
    public City_Relations City_Relations_Scripts; 

    [Header("Кнопка Дать награду")]
    public GameObject Get_Reward;
    [Header("Кнопка Дать квест")]
    public GameObject Give_Quest;
    [Header("Кнопка Получить посылку")]
    public GameObject Complete_The_Quest;

    [Header("ID Задание которое игрок может получить")]
    public int ID_QuestPlayerCanGet;
    public Quest_BD_Info Quest_BD_Info_INFO;

    [Header("квест инфа показать")]
    public GameObject VisualQuest;

    [Header("Визуальные характеристики")]
    public Image Revard_Icon;
    //public Text QuestName;
    public TextLocalisationText TextLocalisationText_Name;
    //public Text QuestInfo;
    public TextLocalisationText TextLocalisationText_Info;
    public Text QuestReward;

    [Header("Иконки наград")]
    public Sprite[] RewardSprite;


    //ID квеста послыку которую нужно получить
    private int Id_Quest;
    //скрипт квеста послыку которую нужно получить
    private Quest_BD Quest_BD_Script;
    
    //ID квеста награду которое надо дать игроку
    private int ID_QuestReward;
    //префаб квеста в списке квестов чтобы удалить после выполнения
    private GameObject BD_List_Quest;

    //зайти в город
    public void GoToCity(int NumCity)
    {
        //вкл саму локацию
        LocationWorld[NumCity].SetActive(true);

        //ставим что уже в городе
        WeAreInLocation = true;

        //сохроняем номер локации
        ID_WorldMapLocation_Now = NumCity;

        //вкл интерфейса
        City_UI.SetActive(true);







        //чекаем город дал уже квест или нет
        if (PlayerPrefs.GetInt("City_Quest" + ID_WorldMapLocation_Now) > 0)
        {
            //если дал то выкл кнопку
            Give_Quest.SetActive(false);
            Debug.Log("Уже давали квест");
        }
        //если не дал то выбираем задание заранее потом игрок может взять ее
        else
        {
            //вкл кнопку
            Give_Quest.SetActive(true);

            //если это город - The_Grand_Harbor
            if (ID_WorldMapLocation_Now == (int)ID_WorldMapLocation.The_Grand_Harbor)
            {
                //выбираем рандомную задание
                ID_QuestPlayerCanGet = Random.Range(Random.Range(0,3), Random.Range(4,6));
            }

            //если это город - Longstand
            if (ID_WorldMapLocation_Now == (int)ID_WorldMapLocation.Longstand)
            {
                //выбираем рандомную задание
                ID_QuestPlayerCanGet = Random.Range(Random.Range(7,10), Random.Range(11,13));
            }

            //если это город - Whitehand
            if (ID_WorldMapLocation_Now == (int)ID_WorldMapLocation.Whitehand)
            {
                //выбираем рандомную задание
                ID_QuestPlayerCanGet = Random.Range(Random.Range(14,17), Random.Range(18,20));
            }

            //если это город - Jadewing
            if (ID_WorldMapLocation_Now == (int)ID_WorldMapLocation.Jadewing)
            {
                //выбираем рандомную задание
                ID_QuestPlayerCanGet = Random.Range(Random.Range(21,24), Random.Range(25,27));
            }

            //если это город - Wolfland
            if (ID_WorldMapLocation_Now == (int)ID_WorldMapLocation.Wolfland)
            {
                //выбираем рандомную задание
                ID_QuestPlayerCanGet = Random.Range(Random.Range(28,31), Random.Range(32,34) );
            }

            //если это город - Frostblade_Sanctuary
            if (ID_WorldMapLocation_Now == (int)ID_WorldMapLocation.Frostblade_Sanctuary)
            {
                //выбираем рандомную задание
                ID_QuestPlayerCanGet = Random.Range(Random.Range(35,38), Random.Range(39,41));
            }

            //если это город - The_Tempest_Lair
            if (ID_WorldMapLocation_Now == (int)ID_WorldMapLocation.The_Tempest_Lair)
            {
                //выбираем рандомную задание
                ID_QuestPlayerCanGet = Random.Range(Random.Range(42,45), Random.Range(46,48) );
            }

            //если это город - Renegade_Hideaway
            if (ID_WorldMapLocation_Now == (int)ID_WorldMapLocation.Renegade_Hideaway)
            {
                //выбираем рандомную задание
                ID_QuestPlayerCanGet = Random.Range(Random.Range(49,52), Random.Range(53,55));
            }

            //берем его BD_info
            Quest_BD_Info_INFO = GameMechanics_Quests_Script.All_Quests_BD[ID_QuestPlayerCanGet];
        }




        //чекаем город должен получить доставку или нет
        {
            //это чтобы проверить должен ли игрок привезти сюда посылку
            bool Must_CompleteQuest = false;


            //Ищем среди активных заданий задание где нужно привезти послыку в этот город
            for (int i = 0; i < Quests_InGameScene_Script.Quests_BD_List.Length; i++)
            {
                //если не пустой
                if (Quests_InGameScene_Script.Quests_BD_List[i]!=null) 
                {
                    //если пунк назначения совпадает с нынешней локацией
                    if ((ID_WorldMapLocation)ID_WorldMapLocation_Now ==
                    Quests_InGameScene_Script.Quests_BD_List[i].GetComponent<Quest_BD>().Quest_BaseDate_Info_INFO.Where)
                    {
                        //если послыку еще не доставили
                        if (PlayerPrefs.GetInt("Quest" + i) != 2)
                        {
                            //ставим что нужно привезти сюда послыку
                            Must_CompleteQuest = true;
                            //Получаем ID квеста который выполняем
                            Id_Quest = Quests_InGameScene_Script.Quests_BD_List[i].GetComponent<Quest_BD>().This_ID;
                            //Получаем скрипт BD квеста который выполняем
                            Quest_BD_Script = Quests_InGameScene_Script.Quests_BD_List[i].GetComponent<Quest_BD>();

                            //Debug.LogError("Сюда доставить посылку");
                        }
                    }
                }
            }

            //если должен 
            if(Must_CompleteQuest == false)
            {
                //вкл кнопку
                Complete_The_Quest.SetActive(false);
            }
            else
            {           
                //вкл кнопку
                Complete_The_Quest.SetActive(true);
            }
        }

        //чекаем город должен дать награду или нет
        {
            //это чтобы проверить должен ли игрок привезти сюда посылку
            bool Must_ReawrdQuest = false;


            //Ищем среди активных заданий задание где нужно привезти из этого города
            for (int i = 0; i < Quests_InGameScene_Script.Quests_BD_List.Length; i++)
            {
                //если не пустой
                if (Quests_InGameScene_Script.Quests_BD_List[i] != null)
                {
                    //если пунк назначения совпадает с нынешней локацией
                    if ((ID_WorldMapLocation)ID_WorldMapLocation_Now ==
                    Quests_InGameScene_Script.Quests_BD_List[i].GetComponent<Quest_BD>().Quest_BaseDate_Info_INFO.From)
                    {
                        //если послыку уже доставили
                        if (PlayerPrefs.GetInt("Quest" + i) == 2)
                        {
                            //ставим что нужно получить награду
                            Must_ReawrdQuest = true;

                            //Получаем ID квеста который выполнили
                            ID_QuestReward = Quests_InGameScene_Script.Quests_BD_List[i].GetComponent<Quest_BD>().This_ID;
                            //Получаем OBJ BD квеста который выполняем
                            BD_List_Quest = Quests_InGameScene_Script.Quests_BD_List[i];

                            //Debug.LogError("Здесь получить награду");
                        }
                    }
                }
            }

            //если не должен 
            if (Must_ReawrdQuest == false)
            {
                //если нет то выкл кнопку
                Get_Reward.SetActive(false);
            }
            else
            {
                //вкл кнопку
                Get_Reward.SetActive(true);
            }

        }
    }

    //Выйти из города
    public void Exit_From_City()
    {
        //выкл саму локацию
        LocationWorld[ID_WorldMapLocation_Now].SetActive(false);

        //ставим что уже НЕ в городе
        WeAreInLocation = false;

        //выкл интерфейса
        City_UI.SetActive(false);
    }




    //выполнить задание
    public void Complete_Quest_COMAND()
    {
        //выполнили квест
        {
            Debug.Log("Выпонили квест");
            //сохроняем задание что уже дсотавили посылку
            PlayerPrefs.SetInt("Quest" + Id_Quest, 2);
            PlayerPrefs.Save();
            //Debug.Log("Id_Quest"+Id_Quest);
            //Обновляем инфу о активности квеста в списке BD
            Quest_BD_Script.Active_QuestUpdate();
        }
     
            //чекаем город должен получить доставку или нет
            {
                //это чтобы проверить должен ли игрок привезти сюда посылку
                bool Must_CompleteQuest = false;


            //Ищем среди активных заданий задание где нужно привезти послыку в этот город
            for (int i = 0; i < Quests_InGameScene_Script.Quests_BD_List.Length; i++)
            {
                //если не пустой
                if (Quests_InGameScene_Script.Quests_BD_List[i] != null)
                {
                    //если пунк назначения совпадает с нынешней локацией
                    if ((ID_WorldMapLocation)ID_WorldMapLocation_Now ==
                    Quests_InGameScene_Script.Quests_BD_List[i].GetComponent<Quest_BD>().Quest_BaseDate_Info_INFO.Where)
                    {
                        //если послыку еще не доставили
                        if (PlayerPrefs.GetInt("Quest" + i) != 2)
                        {
                            Debug.Log("Id_Quest" + i);
                            //ставим что нужно привезти сюда послыку
                            Must_CompleteQuest = true;
                            //Получаем ID квеста который выполняем
                            Id_Quest = Quests_InGameScene_Script.Quests_BD_List[i].GetComponent<Quest_BD>().This_ID;
                            //Получаем скрипт BD квеста который выполняем
                            Quest_BD_Script = Quests_InGameScene_Script.Quests_BD_List[i].GetComponent<Quest_BD>();
                        }
                    }
                }
            }

                //если должен 
                if (Must_CompleteQuest == false)
                {
                    //вкл кнопку
                    Complete_The_Quest.SetActive(false);
                }
                else
                {
                    //вкл кнопку
                    Complete_The_Quest.SetActive(true);
                }
            }
    }


    //дать игроку награду
    public void Give_Reward_COMAND()
    {

        //Получили награду за квест
        {
            //получили скрипт BD задания
            Quest_BD Quest_BDScript_ListBd = BD_List_Quest.GetComponent<Quest_BD>();
            //получили BD_Info задания
            Quest_BD_Info Quest_BD_Info_INFO_BD_QUEST = Quest_BDScript_ListBd.Quest_BaseDate_Info_INFO;

            //Сколько получили награду
            int HowMuchLoot = Quest_BD_Info_INFO_BD_QUEST.Reward;

            //если в награду
            if (Quest_BD_Info_INFO_BD_QUEST.What_reward == ID_LootBay.Bullet)
            {
                //сохроняем количество добычи
                PlayerPrefs.SetInt("Bullet", PlayerPrefs.GetInt("Bullet") + HowMuchLoot);
                PlayerPrefs.Save();

                //Меняем инфу про количество Пуль
                Loot_Storage_Script.Update_Int_Bullet();

                Debug.Log("Пули" + HowMuchLoot);
            }
            //если в награду
            if (Quest_BD_Info_INFO_BD_QUEST.What_reward == ID_LootBay.Food)
            {
                //сохроняем количество добычи
                PlayerPrefs.SetInt("Food", PlayerPrefs.GetInt("Food") + HowMuchLoot);
                PlayerPrefs.Save();

                //Меняем инфу про количество запчестей
                Loot_Storage_Script.Update_Int_Food();

                Debug.Log("Еда" + HowMuchLoot);
            }
            //если в награду
            if (Quest_BD_Info_INFO_BD_QUEST.What_reward == ID_LootBay.Gold)
            {
                //сохроняем количество добычи
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + HowMuchLoot);
                PlayerPrefs.Save();

                //Меняем инфу про количество Пуль
                Loot_Storage_Script.Update_Int_Gold();

                Debug.Log("Золото" + HowMuchLoot);
            }
            //если в награду
            if (Quest_BD_Info_INFO_BD_QUEST.What_reward == ID_LootBay.Petrol)
            {
                //Изменяем переменную о количестве бензина
                Convoy_Controller_Script.Petrol += HowMuchLoot;


                //сохроняем количество добычи
                PlayerPrefs.SetInt("Petrol", PlayerPrefs.GetInt("Petrol") + HowMuchLoot);
                PlayerPrefs.Save();

                Debug.Log("Бензин" + HowMuchLoot);
            }
            //если в награду
            if (Quest_BD_Info_INFO_BD_QUEST.What_reward == ID_LootBay.SpareParts)
            {

                //сохроняем количество добычи
                PlayerPrefs.SetInt("SpareParts", PlayerPrefs.GetInt("SpareParts") + HowMuchLoot);
                PlayerPrefs.Save();

                //Меняем инфу про количество запчестей
                Loot_Storage_Script.Update_Int_SpareParts();

                Debug.Log("Запчасти" + HowMuchLoot);
            }
            //если в награду
            if (Quest_BD_Info_INFO_BD_QUEST.What_reward == ID_LootBay.Water)
            {

                //сохроняем количество добычи
                PlayerPrefs.SetInt("Water", PlayerPrefs.GetInt("Water") + HowMuchLoot);
                PlayerPrefs.Save();

                //Меняем инфу про количество Воды
                Loot_Storage_Script.Update_Int_Water();

                Debug.Log("Вода" + HowMuchLoot);
            }


            //улучшаем отношение с получаетелем  //скок - отношения   //город нанимателя - отношения
            City_Relations_Scripts.Plus_Relation(5, (int)Quest_BD_Info_INFO_BD_QUEST.From);

            //улучшаем отношение с нанемателем //скок - отношения   //город получателя - отношения
            City_Relations_Scripts.Plus_Relation(5, (int)Quest_BD_Info_INFO_BD_QUEST.Where);
        }

        Debug.Log("Выпонили квест");

        //сохроняем что задание больше не активна
        PlayerPrefs.SetInt("Quest" + ID_QuestReward, 0);
        PlayerPrefs.Save();

        //Сохроняем что город не давал задание
        PlayerPrefs.SetInt("City_Quest" + ID_WorldMapLocation_Now, 0);
        PlayerPrefs.Save();


        //Удаляем задание из списка
        Destroy(BD_List_Quest);

        //Открлючаем кнопку получить награду
        Get_Reward.SetActive(false);

        //Включаем кнопку получить задание и выбираем задание
        {
            //вкл кнопку
            Give_Quest.SetActive(true);

            //если это город - The_Grand_Harbor
            if (ID_WorldMapLocation_Now == (int)ID_WorldMapLocation.The_Grand_Harbor)
            {
                //выбираем рандомную задание
                ID_QuestPlayerCanGet = Random.Range(Random.Range(0, 3), Random.Range(4, 6));
            }

            //если это город - Longstand
            if (ID_WorldMapLocation_Now == (int)ID_WorldMapLocation.Longstand)
            {
                //выбираем рандомную задание
                ID_QuestPlayerCanGet = Random.Range(Random.Range(7, 10), Random.Range(11, 13));
            }

            //если это город - Whitehand
            if (ID_WorldMapLocation_Now == (int)ID_WorldMapLocation.Whitehand)
            {
                //выбираем рандомную задание
                ID_QuestPlayerCanGet = Random.Range(Random.Range(14, 17), Random.Range(18, 20));
            }

            //если это город - Jadewing
            if (ID_WorldMapLocation_Now == (int)ID_WorldMapLocation.Jadewing)
            {
                //выбираем рандомную задание
                ID_QuestPlayerCanGet = Random.Range(Random.Range(21, 24), Random.Range(25, 27));
            }

            //если это город - Wolfland
            if (ID_WorldMapLocation_Now == (int)ID_WorldMapLocation.Wolfland)
            {
                //выбираем рандомную задание
                ID_QuestPlayerCanGet = Random.Range(Random.Range(28, 31), Random.Range(32, 34));
            }

            //если это город - Frostblade_Sanctuary
            if (ID_WorldMapLocation_Now == (int)ID_WorldMapLocation.Frostblade_Sanctuary)
            {
                //выбираем рандомную задание
                ID_QuestPlayerCanGet = Random.Range(Random.Range(35, 38), Random.Range(39, 41));
            }

            //если это город - The_Tempest_Lair
            if (ID_WorldMapLocation_Now == (int)ID_WorldMapLocation.The_Tempest_Lair)
            {
                //выбираем рандомную задание
                ID_QuestPlayerCanGet = Random.Range(Random.Range(42, 45), Random.Range(46, 48));
            }

            //если это город - Renegade_Hideaway
            if (ID_WorldMapLocation_Now == (int)ID_WorldMapLocation.Renegade_Hideaway)
            {
                //выбираем рандомную задание
                ID_QuestPlayerCanGet = Random.Range(Random.Range(49, 52), Random.Range(53, 55));
            }

            //берем его BD_info
            Quest_BD_Info_INFO = GameMechanics_Quests_Script.All_Quests_BD[ID_QuestPlayerCanGet];
        }
    }


    //дать задание
    public void Give_Quest_COMAND()
    {
        //Ставим инфу о задании     
        {
            //иконка награды
            {
                //если пули
                if (Quest_BD_Info_INFO.What_reward == ID_LootBay.Bullet)
                {
                    Revard_Icon.sprite = RewardSprite[0];
                }
                //если еда
                if (Quest_BD_Info_INFO.What_reward == ID_LootBay.Food)
                {
                    Revard_Icon.sprite = RewardSprite[1];
                }
                //если золота
                if (Quest_BD_Info_INFO.What_reward == ID_LootBay.Gold)
                {
                    Revard_Icon.sprite = RewardSprite[2];
                }
                //если бензин
                if (Quest_BD_Info_INFO.What_reward == ID_LootBay.Petrol)
                {
                    Revard_Icon.sprite = RewardSprite[3];
                }
                //если запччасти
                if (Quest_BD_Info_INFO.What_reward == ID_LootBay.SpareParts)
                {
                    Revard_Icon.sprite = RewardSprite[4];
                }
                //если вода
                if (Quest_BD_Info_INFO.What_reward == ID_LootBay.Water)
                {
                    Revard_Icon.sprite = RewardSprite[5];
                }

            }

            //Тип задания
            {
                //если это доставка
                if (Quest_BD_Info_INFO.Quest_Type == ID_Quest_Type.delivery)
                {
                    TextLocalisationText_Name.key = "delivery";
                    //даем команду перевести
                    TextLocalisationText_Name.LocalizText();
                }
            }


            //Инфопрмация по заданию
            TextLocalisationText_Info.key = Quest_BD_Info_INFO.Quest_Info;
            //даем команду перевести
            TextLocalisationText_Info.LocalizText();

            //Награда за задание
            QuestReward.text = "" + Quest_BD_Info_INFO.Reward;

        }


        //Выводим на экран предложение задания
        VisualQuest.SetActive(true);
    }

    //принять квест
    public void Take_Quest_Yes()
    {
        //вызываем комнду получения квеста
        GameMechanics_Quests_Script.Got_A_New_Quest((ID_Quests)ID_QuestPlayerCanGet, ID_WorldMapLocation_Now);

        //отключение кнопки получить квест
        Give_Quest.SetActive(false);
    }
}
