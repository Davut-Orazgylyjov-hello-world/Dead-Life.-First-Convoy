using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMechanics_Quests : MonoBehaviour
{
    [Header("Все задания вообще")]
    public Quest_BD_Info[] All_Quests_BD;

    //[Header("Все активные задания")]
    //public Quest_BD_Info[] Active_Quests_BD;

    //[Header("Активность задания")]
    //public int[] Quest_Activity;//0 нету /1 выполняется/2 забрать вознограждение


    [Header("Скрипты")]
    public Quests_InGameScene Quests_InGameScene_Script;

    // Start is called before the first frame update
    void Start()
    {
        //проверяем были ли активные задания у игрока
        {
            for (int i = 0; i < All_Quests_BD.Length; i++)
            {
                //если есть активное задание
                if (PlayerPrefs.GetInt("Quest" + i) > 0)
                {
                    ////грузим BD в активные задание
                    //Active_Quests_BD[i] = All_Quests_BD[i];

                    ////ставим активность
                    //Quest_Activity[i] = PlayerPrefs.GetInt("Quest" + i);

                    //загружаем в список заданий
                    Quests_InGameScene_Script.Load_To_BD_Quests(All_Quests_BD[i],i);
                }
            }
        }
    }

    //получили новый квест
    public void Got_A_New_Quest(ID_Quests ID_Quest, int ID_City_GiveQuest)
    {
        //ID задания в инт переводим
        int ID_QuestINT = (int)ID_Quest;

        //создаем BD задание в список идаем ему инфу
        {
            // создаем квест
            GameObject Quest_BD = Instantiate<GameObject>(Quests_InGameScene_Script.Prefab_Quest_BD);
            Quest_BD.transform.parent = Quests_InGameScene_Script.Spawn.transform;
            Quest_BD.transform.localScale = new Vector3(1, 1, 1);
            //даем инфу про квест ему
            Quest_BD.GetComponent<Quest_BD>().Quests_BD_Info(All_Quests_BD[ID_QuestINT]);
            //даем его ID + Город который дал квест
            Quest_BD.GetComponent<Quest_BD>().This_ID = ID_QuestINT;
            Quest_BD.GetComponent<Quest_BD>().CityGiveQuest_ID = ID_City_GiveQuest;
            //даем скрипт Quests_InGameScene чтоб если что он смог связаться
            Quest_BD.GetComponent<Quest_BD>().Quests_InGameScene_Script = Quests_InGameScene_Script;

            //сохроняем обект квеста из листа чтоб потом связаться
            Quests_InGameScene_Script.Quests_BD_List[ID_QuestINT] = Quest_BD;
        }

        ////ставим его BD в активные задание
        //Active_Quests_BD[ID_QuestINT] = All_Quests_BD[ID_QuestINT];
        ////меняем на активен
        //Quest_Activity[ID_QuestINT] = 1;

        //сохроняем активность квеста
        PlayerPrefs.SetInt("Quest" + ID_QuestINT, 1);
        PlayerPrefs.Save();

        //сохроняем что город дал квест
        PlayerPrefs.SetInt("City_Quest" + ID_City_GiveQuest,1);
        PlayerPrefs.Save();

        Debug.Log("Получили задание");

    }
}
