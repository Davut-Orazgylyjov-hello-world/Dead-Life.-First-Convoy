using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot_Storage : MonoBehaviour
{
    [Header("Воды")]
    public int Water;
    [Header("Запчастей")]
    public int SpareParts;
    [Header("Золота")]
    public int Gold;
    [Header("Еды")]
    public int Food;
    [Header("Пуль")]
    public int Bullet;

    [Header("Скрипты")]
    public UI_Info_Game UI_Info_Game_Script;


    private void Awake()
    {
        //при запуске игры мы получаем весь лут что был у игрока
        {
            //вода
            Water = PlayerPrefs.GetInt("Water");
            //Запчасти
            SpareParts = PlayerPrefs.GetInt("SpareParts");
            //Золото
            Gold = PlayerPrefs.GetInt("Gold");
            //Еда
            Food = PlayerPrefs.GetInt("Food");
            //Пули
            Bullet = PlayerPrefs.GetInt("Bullet");   
        }

        //Затем даем инфу игроку об их количестве
        {
            //Воды UI
            UI_Info_Game_Script.Update_Info_Water(Water);
            //Запчастей UI
            UI_Info_Game_Script.Update_Info_SpareParts(SpareParts);
            //Золота UI
            UI_Info_Game_Script.Update_Info_Gold(Gold);
            //Еды UI
            UI_Info_Game_Script.Update_Info_Food(Food);
            //Пуль UI
            UI_Info_Game_Script.Update_Info_Bullet(Bullet);
        }
    }

    //Обновить сколько у нас есть воды
    public void Update_Int_Water()
    {
        //вода
        Water = PlayerPrefs.GetInt("Water");
        //Воды UI
        UI_Info_Game_Script.Update_Info_Water(Water);
    }
    //Обновить сколько у нас есть запчестей
    public void Update_Int_SpareParts()
    {
        //Запчасти
        SpareParts = PlayerPrefs.GetInt("SpareParts");
        //Запчастей UI
        UI_Info_Game_Script.Update_Info_SpareParts(SpareParts);
      

    }
    //Обновить сколько у нас есть золота
    public void Update_Int_Gold()
    {
        //Золото
        Gold = PlayerPrefs.GetInt("Gold");
        //Золота UI
        UI_Info_Game_Script.Update_Info_Gold(Gold);
     


    }
    //Обновить сколько у нас есть еды
    public void Update_Int_Food()
    {
        //Еда
        Food = PlayerPrefs.GetInt("Food");

        //Еды UI
        UI_Info_Game_Script.Update_Info_Food(Food);


    }
    //Обновить сколько у нас есть пуль
    public void Update_Int_Bullet()
    {
        //Пули
        Bullet = PlayerPrefs.GetInt("Bullet");
        //Пуль UI
        UI_Info_Game_Script.Update_Info_Bullet(Bullet);
    }

}
