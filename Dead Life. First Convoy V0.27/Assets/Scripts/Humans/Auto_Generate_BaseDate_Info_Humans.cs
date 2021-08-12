using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Auto_Generate_BaseDate_Info_Humans : MonoBehaviour
{
    public bool OnlyName_Rename;
    [Header("Хранилища баз даенных")]
    public Human_BaseDate_Info[] Base_Dates;

    [Header("Мужик")]
    public string[] MAN_Name;
    public Sprite[] MAN_Icons_Avatars;

    [Header("Баба")]
    public Sprite[] Female_Icons_Avatars;
    public string[] Female_Name;
	//фамилия
	public string[] MF_Surname;

    //Распокавать все если нужнот сделать базу данных людей и затем обранто запокавать
    
    //void Start()
    //{
    //    //создание случаных людей
    //    for (int i = 0; i < Base_Dates.Length; i++)
    //    {
    //        //если не только поменять имя
    //        if (OnlyName_Rename == false)
    //        {
    //            int a = 0;

    //            //пол
    //            if (a == 0)
    //            {
    //                int R = Random.Range(0, 2);
    //                if (R == 0)
    //                {
    //                    Base_Dates[i].Man = false;
    //                }
    //                else
    //                {
    //                    Base_Dates[i].Man = true;
    //                }
    //                a++;

    //            }

    //            // имя /фамилия/ аватар
    //            if (a == 1)
    //            {
    //                //Мужик
    //                if (Base_Dates[i].Man == true)
    //                {
    //                    //имя
    //                    Base_Dates[i].Name = MAN_Name[Random.Range(0, MAN_Name.Length - 1)];
    //                    //фамилия
    //                    Base_Dates[i].Surname = MF_Surname[Random.Range(0, MF_Surname.Length - 1)];
    //                    //Автарка
    //                    Base_Dates[i].Avatar = MAN_Icons_Avatars[Random.Range(0, MAN_Icons_Avatars.Length - 1)];

    //                    //меняем название файла В РЕДАКТОРЕ!!!
    //                    //Base_Dates[i].name = ("" + Base_Dates[i].Name + "_"+Base_Dates[i].Surname);
    //                    {
    //                        string assetPath = AssetDatabase.GetAssetPath(Base_Dates[i].GetInstanceID());
    //                        AssetDatabase.RenameAsset(assetPath, "" + Base_Dates[i].Name + "_" + Base_Dates[i].Surname);
    //                        AssetDatabase.SaveAssets();
    //                    }
    //                }
    //                //женщина
    //                if (Base_Dates[i].Man == false)
    //                {
    //                    //имя
    //                    Base_Dates[i].Name = Female_Name[Random.Range(0, Female_Name.Length - 1)];
    //                    //фамилия
    //                    Base_Dates[i].Surname = MF_Surname[Random.Range(0, MF_Surname.Length - 1)];
    //                    //Автарка
    //                    Base_Dates[i].Avatar = Female_Icons_Avatars[Random.Range(0, Female_Icons_Avatars.Length - 1)];
    //                }
    //                a++;
    //                Debug.Log("" + a);

    //            }

    //            //уровень 
    //            if (a == 2)
    //            {
    //                //уровень
    //                Base_Dates[i].LvL = Random.Range(1, 10);
    //                a++;

    //            }
    //            //Характристики
    //            if (a == 3)
    //            {
    //                Base_Dates[i].Strength = 0;
    //                Base_Dates[i].Agility = 0;
    //                Base_Dates[i].Intellect = 0;
    //                Base_Dates[i].Сharisma = 0;

    //                //распределяем характеристики случайно
    //                for (int H = 0; H < Base_Dates[i].LvL; H++)
    //                {
    //                    //рандом S=0 A=1 I=2 C=3
    //                    int RR = Random.Range(0, 3 + 1);

    //                    if (RR == 0)
    //                    {
    //                        Base_Dates[i].Strength++;
    //                    }
    //                    if (RR == 1)
    //                    {
    //                        Base_Dates[i].Agility++;
    //                    }
    //                    if (RR == 2)
    //                    {
    //                        Base_Dates[i].Intellect++;
    //                    }
    //                    if (RR == 3)
    //                    {
    //                        Base_Dates[i].Сharisma++;
    //                    }
    //                }

    //                //Добавляем к каждой характеристике +1 чтобы не было ==0             
    //                Base_Dates[i].Strength++;
    //                Base_Dates[i].Agility++;
    //                Base_Dates[i].Intellect++;
    //                Base_Dates[i].Сharisma++;

    //                a++;

    //            }
    //        }
    //        //иначе просто меняем имя в папке с базой данных
    //        else
    //        {
    //            //меняем название файла В РЕДАКТОРЕ!!!
    //            //Base_Dates[i].name = ("" + Base_Dates[i].Name + "_"+Base_Dates[i].Surname);
    //            {
    //                string assetPath = AssetDatabase.GetAssetPath(Base_Dates[i].GetInstanceID());
    //                AssetDatabase.RenameAsset(assetPath, "" + Base_Dates[i].Name + "_" + Base_Dates[i].Surname);
    //                AssetDatabase.SaveAssets();
    //            }
    //        }
    //    }
    //}
}
