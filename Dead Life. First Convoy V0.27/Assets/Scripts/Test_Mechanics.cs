using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Mechanics : MonoBehaviour
{
    public ID_Car Load_Car;

    public New_ConvoyCar New_ConvoyCar_Script;
    public bool ResetProfile;

    public bool NewCar;

    public New_Human_Character New_Human_Character_Script;

    public GameMechanics_Quests GameMechanics_Quests_Script;
    public ID_Quests ID_Quests_Taked;
    public bool TakeQuest;
    [Header("Увелить деньги")]
    public int SpareParts;



    private void Start()
    {
        if (ResetProfile == true)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (SpareParts > 0)
        {
            PlayerPrefs.SetInt("SpareParts", SpareParts);
            PlayerPrefs.Save();
            SpareParts = 0;
        }

        //тест создание тачки
        {
            if (NewCar == true)
            {
                NewCar = false;
                New_ConvoyCar_Script.NewCar_CreatAndSave((int)Load_Car);
            }
        }

        //тест создание квеста
        {
            if (TakeQuest == true)
            {
                //получитьзадание
                CreateQuest();
                //чтоб не повторялся
                TakeQuest = false;
            }
        }

    }
    //создать тачку
    public void Create_Car()
    {
        New_ConvoyCar_Script.NewCar_CreatAndSave(Random.Range(1, 14));
    }

    //создать перса
    public void Create_Character()
    {
        int i = Random.Range(1, 1260);
        if (PlayerPrefs.GetInt("ID_Character" + i) == 0)
        {
            New_Human_Character_Script.NewCharacter_CreatAndSave(i);
        }
        //если этот чел у игрока уже есть то повторяем 
        else
        {
            Create_Character();
        }
    }

    //создать задание если еще нету 
    public void CreateQuest()
    {
        //выбранный город который дает задание
        int ID_City = (int)GameMechanics_Quests_Script.All_Quests_BD[(int)ID_Quests_Taked].From;

        //чекаем этот город не давал еще задание или нет
        {          
            //если не давал
            if (PlayerPrefs.GetInt("City_Quest" + ID_City) < 1)
            {
                //чекаем эта задание еще не активна
                if (PlayerPrefs.GetInt("Quest" + (int)ID_Quests_Taked) < 1)
                {
                    //если нет то даем команду на активацию квеста
                    GameMechanics_Quests_Script.Got_A_New_Quest(ID_Quests_Taked, ID_City);
                }
            }
            else
            {
                Debug.Log("Этот город уже давал задание");
            }
        }
    }
}
