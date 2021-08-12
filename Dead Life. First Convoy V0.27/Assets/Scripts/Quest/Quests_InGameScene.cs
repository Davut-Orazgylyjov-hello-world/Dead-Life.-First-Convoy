using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests_InGameScene : MonoBehaviour
{
    [Header("таблица квестов")]
    public GameObject Prefab_Quest_BD;
    public GameObject Spawn;

    [Header("квест в списке")]
    public GameObject[] Quests_BD_List;//если надо будет удалить задание из списка 

    //это для удалении задания
    private int CityGiveQuest_ID_Info;
    private int This_ID_Info;
    [Header("Вы уверены что хотите отказаться от задания?")]
    public GameObject AreYouShure;


    //списко квестов
    public GameObject QuestsList;

    [Header("Скрипты")]
    public City_Relations City_Relations_Scripts;
    public GameMechanics_Quests GameMechanics_Quests_Scipts;

    //Загрузка инфы про квест в списке 
    public void Load_To_BD_Quests(Quest_BD_Info Quest_BD_Info_INFO,int ID_Quest_int)
    {

        //создаем BD задание в список идаем ему инфу
        {
            // создаем квест
            GameObject Quest_BD = Instantiate<GameObject>(Prefab_Quest_BD);
            Quest_BD.transform.parent = Spawn.transform;
            Quest_BD.transform.localScale = new Vector3(1, 1, 1);
            //даем инфу про квест ему
            Quest_BD.GetComponent<Quest_BD>().Quests_BD_Info(Quest_BD_Info_INFO);
            //даем его ID + Город который дал квест
            Quest_BD.GetComponent<Quest_BD>().This_ID = ID_Quest_int;
            Quest_BD.GetComponent<Quest_BD>().CityGiveQuest_ID = (int)Quest_BD_Info_INFO.From;
            //даем скрипт Quests_InGameScene чтоб если что он смог связаться
            Quest_BD.GetComponent<Quest_BD>().Quests_InGameScene_Script = this;

            //сохроняем обект квеста из листа чтоб потом связаться
            Quests_BD_List[ID_Quest_int] = Quest_BD;
        }

    }


    //уверены что хотите удалить?
    public void DoYouWnotDeleteQuest(int CityGiveQuest_ID, int This_ID)
    {
        //Включаем кнопку вы увернеы?
        AreYouShure.SetActive(true);

        //получаем инфу задания
        This_ID_Info = This_ID;//id квеста
        CityGiveQuest_ID_Info = CityGiveQuest_ID;//id города что дал квест

    }

    //удалить задание + минус отношение к тому кто дал задание
    public void YesIamShere_DelletQuest()
    {
        Debug.Log("Удалили задание");
        //удалить с сохранения его что нету кветса
        {
            //сохроняем что нету этого квеста
            PlayerPrefs.SetInt("Quest" + This_ID_Info, 0);
            PlayerPrefs.Save();

            //сохроняем что город не дал квест
            PlayerPrefs.SetInt("City_Quest" + CityGiveQuest_ID_Info, 0);
            PlayerPrefs.Save();
        }
        //ухудшить отношение с нанимателем   //скок - отношения   //город нанимателя - отношения
        City_Relations_Scripts.Minus_Relation(10, CityGiveQuest_ID_Info);
        //ухудшить отношение с получаетелем //скок - отношения   //город получателя - отношения
        City_Relations_Scripts.Minus_Relation(10, (int)GameMechanics_Quests_Scipts.All_Quests_BD[This_ID_Info].Where);
        
        //удалить задание из списка
        Destroy(Quests_BD_List[This_ID_Info]);


    }

    //закрыть список
    public void Off_CharacterList()
    {
        QuestsList.SetActive(false);
    }
}
