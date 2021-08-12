using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//этот скрипт предназначен на случай если это первый запуск игры
public class First_Launch_Of_Game : MonoBehaviour
{
    [Header("Первый запуск")]
    public GameObject CheangeLanguage;//выбор языка
    public GameObject AllOptionFirstStart;
    [Header("Язык игры")]
    public Dropdown Language_Dropdown;

    [Header("Стартовый набор людей")]
    public int[] ID_Character_CreateAtStart;

    [Header("Стартовый набор машин")]
    public ID_Car[] ID_Car_FirstStart;

    [Header("Скрипты")]
    public New_Human_Character New_Human_Character_Script;
    public New_ConvoyCar New_ConvoyCar_Script;

    void Awake()
    {
        //если это первый запуск
        if (PlayerPrefs.GetInt("FirstStartGame") != 1)
        {
            //вкл настройки первого запуска
            AllOptionFirstStart.SetActive(true);

            //выбор языка
            CheangeLanguage.SetActive(true);
            
            //ставим звук на макс в настройках
            {
                //сохарнение
                PlayerPrefs.SetFloat("Music_SoundValue", 1);
                PlayerPrefs.Save();


                //сохарнение
                PlayerPrefs.SetFloat("Effects_SoundValue", 1);
                PlayerPrefs.Save();

                //сохарнение
                PlayerPrefs.SetFloat("Voice_SoundValue", 1);
                PlayerPrefs.Save();
            }

            //Даем игроку стартовый набор 
            {
                //еда вода и тд
                {
                    //еда
                    PlayerPrefs.SetInt("Food", 25);
                    PlayerPrefs.Save();
                    //вода
                    PlayerPrefs.SetInt("Water", 25);
                    PlayerPrefs.Save();
                    //Запчасти
                    PlayerPrefs.SetInt("SpareParts", 500);
                    PlayerPrefs.Save();
                    //Бензина
                    PlayerPrefs.SetInt("Food", 10);
                    PlayerPrefs.Save();
                    //золота
                    PlayerPrefs.SetInt("Gold", 0);
                    PlayerPrefs.Save();
                    //патронов
                    PlayerPrefs.SetInt("Bullet", 50);
                    PlayerPrefs.Save();
                    //Настроение людей
                    PlayerPrefs.SetInt("Characters_Mood", 100);
                    PlayerPrefs.Save();
                }
                //Люди
                {
                    //создаем стартовый набор людей
                    for(int i = 0;i< ID_Character_CreateAtStart.Length; i++)
                    {
                        CreateCharacter(ID_Character_CreateAtStart[i]);
                    }
                }
                //Машины
                {
                    //создаем стартовый набор машинами
                    for (int i = 0; i < ID_Car_FirstStart.Length; i++)
                    {
                        CreateCar(ID_Car_FirstStart[i]);
                    }
                }
            }
        }
        //иначе убираем возможность настроек первого запуска
        else
        {
            Destroy(AllOptionFirstStart.gameObject);
        }


        //если первый запуск
        if (AllOptionFirstStart != null)
        {
            //язык
            Language_Dropdown.value = PlayerPrefs.GetInt("Language");
        }
    }

    //когда игрок нажмет принять сохроняем чтобы некст раз оно не показалась
    public void SaveFirstStart_ChengeLanguage()
    {
        PlayerPrefs.SetInt("FirstStartGame", 1);
        PlayerPrefs.Save();
    }


    //Рестарт Игры
    public void Restart_Game()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //создать стартового персонажа
    public void CreateCharacter(int ID_Character)
    {
        New_Human_Character_Script.NewCharacter_CreatAndSave(ID_Character);
    }

    //создать стартовую тачку
    public void CreateCar(ID_Car ID_Car_Create)
    {
        New_ConvoyCar_Script.NewCar_CreatAndSave((int)ID_Car_Create);
    }
}


