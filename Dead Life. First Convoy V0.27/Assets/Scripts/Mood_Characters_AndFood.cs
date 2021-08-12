using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mood_Characters_AndFood : MonoBehaviour
{
    [Header("Настроение людей")]
    public float Mood_Characters;

    [Header("Скрипты")]
    public Loot_Storage Loot_Storage_Script;
    public UI_Info_Game UI_Info_Game_Script;
    public Characters_InGameScene Characters_InGameScene_Script;


    //задершка до сохранение настроение людей
    private float Delay_Save_Mood = 3;


    //задершка удаления рандомнога перса
    private float Delay_Dellet_RR_Character = 30;

    [Header("Уведомление что перс покинул конвой")]
    public GameObject Character_LeftConvoy;


    [Header("Цвет настроения смайлкиа")]
    public Color[] Color_Smile;

    [Header("Иконкаи смайликов")]
    public Sprite[] Icons_Smile_Hoods;

    [Header("Картинка иконки настрения людей")]
    public Image Hood_Image;

    // Start is called before the first frame update
    void Start()
    {
        //загружаем скок было настрение
        Mood_Characters = PlayerPrefs.GetInt("Characters_Mood");

        //обновление UI
        UI_Info_Game_Script.Update_Info_Mood((int)Mood_Characters);

        //обновить визуально настроение людей
        UpdateIcon_HoodChracters();
    }

    // Update is called once per frame
    void Update()
    {
        //Настроение людей
        {
            //сохранение настроения + обновление UI
            {
                if (Delay_Save_Mood > 0)
                {
                    Delay_Save_Mood -= Time.deltaTime * 1;
                }
                else
                {
                    Delay_Save_Mood = 3;
               
                    //сохраняем настроение
                    PlayerPrefs.SetInt("Characters_Mood", (int)Mood_Characters);
                    PlayerPrefs.Save();

                    //обновление UI
                    UI_Info_Game_Script.Update_Info_Mood((int)Mood_Characters);

                    //обновить визуально настроение людей
                    UpdateIcon_HoodChracters();
                }
            }

            //--настроение
            {
                //если нет еды
                if (Loot_Storage_Script.Food <= 0)
                {
                    //если еще не 0 настроение
                    if (Mood_Characters > 0)
                    {
                        //--настроение
                        Mood_Characters -= Time.deltaTime * 0.1f;
                    }
                }
                //если нет воды
                if (Loot_Storage_Script.Water <= 0)
                {
                    //если еще не 0 настроение
                    if (Mood_Characters > 0)
                    {
                        //--настроение
                        Mood_Characters -= Time.deltaTime * 0.25f;
                    }
                }
            }



            //если есть еда и вода
            if (Loot_Storage_Script.Food > 0 && Loot_Storage_Script.Water > 0)
            {
                //если еще не макс настроение
                if (Mood_Characters < 100)
                {
                    //++ настроение людей
                    Mood_Characters += Time.deltaTime * 0.1f;
                }


            }

            //еслинастрой людей меньше 10 то начинаем таймердо удаления перса
            if (Mood_Characters < 10)
            {
                //Удаление перса
                {
                    if (Delay_Dellet_RR_Character > 0)
                    {
                        Delay_Dellet_RR_Character -= Time.deltaTime * 1;
                    }
                    else
                    {
                        Delay_Dellet_RR_Character = 30;

                        //даем команду на удаление рандмного перса
                        Dellet_RRandomCharacter();
                    }
                }
            }

            //это чтоб < 0 небыло
            if (Mood_Characters < 0)
            {
                Mood_Characters = 0;

                //сохраняем настроение
                PlayerPrefs.SetInt("Characters_Mood", 0);
                PlayerPrefs.Save();

            }
        }
    }


    //удалить случайного персонажа
    public void Dellet_RRandomCharacter()
    {

        //выбираем рандмоный перс
        int i = Random.Range(0, Characters_InGameScene_Script.Character_BD_List.Length);

        //если не пустой то даем команду на удаление
        if (Characters_InGameScene_Script.Character_BD_List[i] != null)
        {
            //пулчаем скрипт
            Character_BD Character_BD_Script = Characters_InGameScene_Script.Character_BD_List[i].GetComponent<Character_BD>();

            Character_LeftYourConvoy(Character_BD_Script.Plase_this, Character_BD_Script.This_ID);

              Debug.Log("Удалили");

        }
    }


    //Говорим игроку что один из персонажей персонажей покинул конвой (настрой людей понизился)
    public void Character_LeftYourConvoy(int Plase_this, int This_ID)
    {
        //Включае кнопку с надписью (один из персонажей персонажей покинул конвой (настрой людей понизился) )
        Character_LeftConvoy.SetActive(true);

        //лаем инфу про перса что покунул конвой
        Characters_InGameScene_Script.CharacterINFO_LeftConvoy(Plase_this, This_ID);

        //понижаем настрой людей на 5
        Mood_Characters -= 5;
        //и обнов инфы
        Delay_Save_Mood = 0;
    }

    //обновить настроение людей 
    public void UpdateIcon_HoodChracters()
    {
        //Иконка настроение + цвет
        {
            if (Mood_Characters < 5)
            {
                

                Hood_Image.color = Color_Smile[9];
                Hood_Image.sprite = Icons_Smile_Hoods[6];
            }
            if (Mood_Characters > 4 && Mood_Characters < 19)
            {
           

                Hood_Image.color = Color_Smile[8];
                Hood_Image.sprite = Icons_Smile_Hoods[6];
            }
            if (Mood_Characters > 20 && Mood_Characters < 29)
            {
               

                Hood_Image.color = Color_Smile[7];
                Hood_Image.sprite = Icons_Smile_Hoods[5];
            }
            if (Mood_Characters > 30 && Mood_Characters < 39)
            {
              

                Hood_Image.color = Color_Smile[6];
                Hood_Image.sprite = Icons_Smile_Hoods[5];
            }
            if (Mood_Characters > 40 && Mood_Characters < 49)
            {
                

                Hood_Image.color = Color_Smile[5];
                Hood_Image.sprite = Icons_Smile_Hoods[4];
            }
            if (Mood_Characters > 50 && Mood_Characters < 59)
            {
                Hood_Image.color = Color_Smile[4];
                Hood_Image.sprite = Icons_Smile_Hoods[4];
            }
            if (Mood_Characters > 60 && Mood_Characters < 69)
            {
                Hood_Image.color = Color_Smile[3];
                Hood_Image.sprite = Icons_Smile_Hoods[3];
            }
            if (Mood_Characters > 70 && Mood_Characters < 79)
            {
                Hood_Image.color = Color_Smile[2];
                Hood_Image.sprite = Icons_Smile_Hoods[2];
            }
            if (Mood_Characters > 80 && Mood_Characters < 89)
            {
                Hood_Image.color = Color_Smile[1];
                Hood_Image.sprite = Icons_Smile_Hoods[1];
            }
            if (Mood_Characters > 90)
            {
                Hood_Image.color = Color_Smile[0];
                Hood_Image.sprite = Icons_Smile_Hoods[0];
            }
        }
    }
}
