using UnityEngine;
using UnityEngine.UI;

public class Quest_BD : MonoBehaviour
{
    [Header("Визуальные характеристики")]
    public Image Revard_Icon;
    //public Text QuestName;
    public TextLocalisationText TextLocalisationText_SCRIPT_Name;
    //public Text QuestInfo;
    public TextLocalisationText TextLocalisationText_SCRIPT_Info;
    public Text QuestReward;
    public Image Where_LocationLogoSprite;
    public Image Frome_LocationLogoSprite;

    [Header("Хранилища инфы всех заданий у игрока")]
    public Quests_InGameScene Quests_InGameScene_Script;
    public int This_ID;
    public int CityGiveQuest_ID;

    [Header("Именно наша задание")]
    public Quest_BD_Info Quest_BaseDate_Info_INFO;

    [Header("Скрипты")]
    public New_ConvoyCar New_ConvoyCar_Script;

    [Header("Иконки наград")]
    public Sprite[] RewardSprite;

    [Header("Надпись Не выполнили")]
    public GameObject Text_NotComleted;
    [Header("Надпись Получите награду")]
    public GameObject Text_TakeReward;

    [Header("Иконки локаций")]
    public Sprite[] LocationLogoSprite;

    [Header("Стрелка куда везти")]
    public GameObject Where_to_take_Quest;

    [Header("Стрелка откуда везти")]
    public GameObject Frome_to_take_Quest;

    //получаем инфу про перса
    public void Quests_BD_Info(Quest_BD_Info Quest_BD_Script)
    {
        //это задание
        Quest_BaseDate_Info_INFO = Quest_BD_Script;

        //грузим наши характеристики
        {
            //иконка награды
            {
                //если пули
                if (Quest_BaseDate_Info_INFO.What_reward == ID_LootBay.Bullet)
                {
                    Revard_Icon.sprite = RewardSprite[0];
                }
                //если еда
                if (Quest_BaseDate_Info_INFO.What_reward == ID_LootBay.Food)
                {
                    Revard_Icon.sprite = RewardSprite[1];
                }
                //если золота
                if (Quest_BaseDate_Info_INFO.What_reward == ID_LootBay.Gold)
                {
                    Revard_Icon.sprite = RewardSprite[2];
                }
                //если бензин
                if (Quest_BaseDate_Info_INFO.What_reward == ID_LootBay.Petrol)
                {
                    Revard_Icon.sprite = RewardSprite[3];
                }
                //если запччасти
                if (Quest_BaseDate_Info_INFO.What_reward == ID_LootBay.SpareParts)
                {
                    Revard_Icon.sprite = RewardSprite[4];
                }
                //если вода
                if (Quest_BaseDate_Info_INFO.What_reward == ID_LootBay.Water)
                {
                    Revard_Icon.sprite = RewardSprite[5];
                }
           
            }

            //Тип задания
            {
                //если это доставка
                if (Quest_BaseDate_Info_INFO.Quest_Type == ID_Quest_Type.delivery)
                {
                    TextLocalisationText_SCRIPT_Name.key = "delivery";

                    //даем команду перевести
                    TextLocalisationText_SCRIPT_Name.LocalizText();
                }
            }


            //Инфопрмация по заданию
            TextLocalisationText_SCRIPT_Info.key = Quest_BaseDate_Info_INFO.Quest_Info;
            //даем команду перевести
            TextLocalisationText_SCRIPT_Info.LocalizText();


            //Награда за задание
            QuestReward.text = "" + Quest_BaseDate_Info_INFO.Reward;

            //Иконка куда везти
            Where_LocationLogoSprite.sprite = LocationLogoSprite[(int)Quest_BaseDate_Info_INFO.Where];

            //Иконка откда везти
            Frome_LocationLogoSprite.sprite = LocationLogoSprite[(int)Quest_BaseDate_Info_INFO.From];

        }
    }


    //отправить запрос на удаление задания
    public void DoYouwontDelete_ThisQuest()
    {
        Quests_InGameScene_Script.DoYouWnotDeleteQuest(CityGiveQuest_ID, This_ID);
    }


    public void Start()
    {
        //чекаем активность квеста
        {
            //Чекаем квест еще не доставлен
            if (PlayerPrefs.GetInt("Quest" + This_ID) == 1)
            {
                //Вкл напись не выполнили и выкл надпись получить награду
                Text_NotComleted.SetActive(true);
                Text_TakeReward.SetActive(false);

                //Иконка куда везти и откуда везти
                Where_to_take_Quest.SetActive(true);
                Frome_to_take_Quest.SetActive(false);
            }

            //Чекаем квест ждет получения награды
            if (PlayerPrefs.GetInt("Quest" + This_ID) == 2)
            {
                //Выкл напись не выполнили и вкл надпись получить награду
                Text_NotComleted.SetActive(false);
                Text_TakeReward.SetActive(true);


                //Иконка куда везти и откуда везти
                Where_to_take_Quest.SetActive(false);
                Frome_to_take_Quest.SetActive(true);
            }
        }

    }


    //Обновить информацию об активности квеста
    public void Active_QuestUpdate()
    {
        //чекаем активность квеста
        {
            //Чекаем квест еще не доставлен
            if (PlayerPrefs.GetInt("Quest" + This_ID) == 1)
            {
                //Вкл напись не выполнили и выкл надпись получить награду
                Text_NotComleted.SetActive(true);
                Text_TakeReward.SetActive(false);

                //Иконка куда везти и откуда везти
                Where_to_take_Quest.SetActive(true);
                Frome_to_take_Quest.SetActive(false);
            }

            //Чекаем квест ждет получения награды
            if (PlayerPrefs.GetInt("Quest" + This_ID) == 2)
            {
                //Выкл напись не выполнили и вкл надпись получить награду
                Text_NotComleted.SetActive(false);
                Text_TakeReward.SetActive(true);

                //Иконка куда везти и откуда везти
                Where_to_take_Quest.SetActive(false);
                Frome_to_take_Quest.SetActive(true);
            }
        }
    }
}
