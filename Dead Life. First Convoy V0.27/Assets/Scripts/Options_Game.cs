using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Options_Game : MonoBehaviour
{
    private void Awake()
    {
        int LanguageInt = PlayerPrefs.GetInt("Language");

        if (LanguageInt == 0)
            LocalisationSystem.language = LocalisationSystem.Language.Russian;
        if (LanguageInt == 1)
            LocalisationSystem.language = LocalisationSystem.Language.English;
        if (LanguageInt == 2)
            LocalisationSystem.language = LocalisationSystem.Language.French;
        if (LanguageInt == 3)
            LocalisationSystem.language = LocalisationSystem.Language.Japanese;
        if (LanguageInt == 4)
            LocalisationSystem.language = LocalisationSystem.Language.Italian;
        if (LanguageInt == 5)
            LocalisationSystem.language = LocalisationSystem.Language.Spanish;
        if (LanguageInt == 6)
            LocalisationSystem.language = LocalisationSystem.Language.Arab;
        if (LanguageInt == 7)
            LocalisationSystem.language = LocalisationSystem.Language.Ukrainian;
        if (LanguageInt == 8)
            LocalisationSystem.language = LocalisationSystem.Language.Vietnamese;
        if (LanguageInt == 9)
            LocalisationSystem.language = LocalisationSystem.Language.Danish;
        if (LanguageInt == 10)
            LocalisationSystem.language = LocalisationSystem.Language.Chinese_trad;
        if (LanguageInt == 11)
            LocalisationSystem.language = LocalisationSystem.Language.Korean;
        if (LanguageInt == 12)
            LocalisationSystem.language = LocalisationSystem.Language.Norwegian;
        if (LanguageInt == 13)
            LocalisationSystem.language = LocalisationSystem.Language.Portuguese;
        if (LanguageInt == 14)
            LocalisationSystem.language = LocalisationSystem.Language.Turkish;
        if (LanguageInt == 15)
            LocalisationSystem.language = LocalisationSystem.Language.Finnish;
        if (LanguageInt == 16)
            LocalisationSystem.language = LocalisationSystem.Language.Swedish;
        if (LanguageInt == 17)
            LocalisationSystem.language = LocalisationSystem.Language.German;
        if (LanguageInt == 18)
            LocalisationSystem.language = LocalisationSystem.Language.Bulgarian;
        if (LanguageInt == 19)
            LocalisationSystem.language = LocalisationSystem.Language.Hungarian;
        if (LanguageInt == 20)
            LocalisationSystem.language = LocalisationSystem.Language.Greek;
        if (LanguageInt == 21)
            LocalisationSystem.language = LocalisationSystem.Language.Czech;
        if (LanguageInt == 22)
            LocalisationSystem.language = LocalisationSystem.Language.Chinese_simpl;
        if (LanguageInt == 23)
            LocalisationSystem.language = LocalisationSystem.Language.Dutch;
        if (LanguageInt == 24)
            LocalisationSystem.language = LocalisationSystem.Language.Polish;
        if (LanguageInt == 25)
            LocalisationSystem.language = LocalisationSystem.Language.Romanian;
        if (LanguageInt == 26)
            LocalisationSystem.language = LocalisationSystem.Language.Thai;
    }

    [Header("Звук")]
    public AudioMixer Auido_Music;
    public AudioMixer Auido_Effects;
    public AudioMixer Auido_Voice;
    [Header("Экран")]
    Resolution[] Screen_resolutions;
    public Dropdown Resolutions_Dropdown;

    [Header("UI Настроек")]
    public Slider Auido_Music_Slider;
    public Slider Auido_Effects_Slider;
    public Slider Auido_Voice_Slider;
    public Toggle Fullscreen_Togle;
    public Dropdown Language_Dropdown;
    //public Dropdown Resolution_screen_Dropdown;
    public Dropdown Game_Quality_Dropdown;

    [Space]
    [Header("Mouse Sensetive")]
    public Slider OptionMouseSlider;

    [Header("презапуск игры")]
    public string[] Need_RestartGame;
    public string[] Ok_strig;
    public string[] Aply_strig;
    public Text Ok_Button_Text;
    public Text Info_Restart_Text;
    public Text Aply;

    [Header("изменения размера интерфейса CanvasScale")]
    public CanvasScaler[] CanvasScaler_Option;
    public Slider CanvasScaler_Option_Slider;
    //
    //звуки/музыка/голос
    public void SetVolue_Music(float volume)
    {
        Auido_Music.SetFloat("Music_SoundValue", Mathf.Log10 (volume)*20);
        //сохарнение
        PlayerPrefs.SetFloat("Music_SoundValue", volume);
        PlayerPrefs.Save();
    }
    public void SetVolue_SoundEffects(float volume)
    {      
        Auido_Effects.SetFloat("Effects_SoundValue", Mathf.Log10(volume) * 20);
        //сохарнение
        PlayerPrefs.SetFloat("Effects_SoundValue", volume);
        PlayerPrefs.Save();
    }
    public void SetVolue_SoundVoice(float volume)
    {
        Auido_Voice.SetFloat("Voice_SoundValue", Mathf.Log10(volume) * 20);
        //сохарнение
        PlayerPrefs.SetFloat("Voice_SoundValue", volume);
        PlayerPrefs.Save();
    }

    //качество игры
    public void Set_Game_Quality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Game_Quality_Dropdown_value", qualityIndex);
        PlayerPrefs.Save();
    }
    //
    //размер интерфейса
    public void Set_Game_InterfaceScale(float InterfaceScale)
    {
        PlayerPrefs.SetFloat("Game_Game_InterfaceScale_value", InterfaceScale);
        PlayerPrefs.Save();
        Change_Intarface_scale(InterfaceScale);
    }
    //вызов изменнеия размера интерфейса
    public void Change_Intarface_scale(float InterfaceScale)
    {

        if (CanvasScaler_Option != null)
        {
            for (int i = 0; i < CanvasScaler_Option.Length; i++)
            {
                CanvasScaler_Option[i].scaleFactor = InterfaceScale;
            }
        }
    }
    //вызов изменнеия размера интерфейса
    public void Change_Intarface_scaleStart()
    {

        if (CanvasScaler_Option != null)
        {
            for (int i = 0; i < CanvasScaler_Option.Length; i++)
            {
                //CanvasScaler_Option[i].scaleFactor = PlayerPrefs.GetFloat("Game_Game_InterfaceScale_value");
                CanvasScaler_Option[i].scaleFactor = CanvasScaler_Option_Slider.value;
            }
        }
    }

    //полный экран
    public void SetFullscreen(bool IsFullscreen)
    {
        Screen.fullScreen = IsFullscreen;

        if (IsFullscreen == true)
        {
            PlayerPrefs.SetInt("IsFullscreen", 0);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("IsFullscreen", 1);
            PlayerPrefs.Save();
        }
    }

    public void SetResolution(int resolutionIndex)
    {
       
        Resolution resolution = Screen_resolutions[resolutionIndex];

        if (resolution.width > 100 && resolution.height > 100)
        {
            Screen.SetResolution(resolution.width, resolution.height, Fullscreen_Togle.isOn);       
        }   
    }
    //язык
    public void Set_Game_Language(int LanguageInex)
    {
        PlayerPrefs.SetInt("Language", LanguageInex);
        PlayerPrefs.Save();
        ChangeLanguageInfo(LanguageInex);
    }
    //инормация для игрока
    public void ChangeLanguageInfo(int Language)
    {
        Ok_Button_Text.text = Ok_strig[Language];
        Info_Restart_Text.text = Need_RestartGame[Language];
        Aply.text = Aply_strig[Language];
    }
    //перезапуск игры
    public void Restart_Menu()
    {
        SceneManager.LoadScene(0);
    }

    //чувствительность мыши
    public void MouseSensevityOption(float Sens)
    {
        OptionMouseSlider.value = Sens;
        PlayerPrefs.SetFloat("MouseSensetives", Sens);
        PlayerPrefs.Save();
    }


    // Start is called before the first frame update
    void Start()
    {
       // PlayerPrefs.DeleteAll();
        //получаем все разрешения экрана
        {
            Screen_resolutions = Screen.resolutions;

            Resolutions_Dropdown.ClearOptions();

            List<string> Screen_Resolution_options = new List<string>();

            int currentResolutionIndex = 0;
            for (int i = 0; i < Screen_resolutions.Length; i++)
            {
                string option = Screen_resolutions[i].width + "x" + Screen_resolutions[i].height;
                Screen_Resolution_options.Add(option);

                if (Screen_resolutions[i].width == Screen.currentResolution.width &&
                   Screen_resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                  
                }            
            }

            Resolutions_Dropdown.AddOptions(Screen_Resolution_options);
           // Resolutions_Dropdown.value = PlayerPrefs.GetInt("Resolution_screen_Dropdown_value");
            Resolutions_Dropdown.value = currentResolutionIndex;
            Resolutions_Dropdown.RefreshShownValue();

           


        }
        //загрузка
        {
            //загрузка параметров звука
            {
                Auido_Music.SetFloat("Music_SoundValue", Mathf.Log10 (PlayerPrefs.GetFloat("Music_SoundValue")) * 20); 
                Auido_Effects.SetFloat("Effects_SoundValue", Mathf.Log10 (PlayerPrefs.GetFloat("Effects_SoundValue")) * 20);
                Auido_Voice.SetFloat("Voice_SoundValue", Mathf.Log10 (PlayerPrefs.GetFloat("Voice_SoundValue")) * 20);
            }

            //загрузка полного экрана
            {
                if (PlayerPrefs.GetInt("IsFullscreen") == 1)
                {
        //            Screen.fullScreen = false;
                    Fullscreen_Togle.isOn = false;
                }
                else
                {
        //            Screen.fullScreen = true;
                    Fullscreen_Togle.isOn = true;
                }
            }
            //загрузка детализации
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Game_Quality_Dropdown_value"));

            //загрузка параметров для UI настроек
            {
                //слайдеры звука
                {
                    Auido_Music_Slider.value = PlayerPrefs.GetFloat("Music_SoundValue");
                    Auido_Effects_Slider.value = PlayerPrefs.GetFloat("Effects_SoundValue");
                    Auido_Voice_Slider.value = PlayerPrefs.GetFloat("Voice_SoundValue");
                }

                {
                
                    Game_Quality_Dropdown.value = PlayerPrefs.GetInt("Game_Quality_Dropdown_value");
                    Language_Dropdown.value = PlayerPrefs.GetInt("Language");
                }
            }

            //загрузка чуствительность мыши при старте 
            if (PlayerPrefs.GetFloat("MouseSensetives") > 0)
            {
                OptionMouseSlider.value = PlayerPrefs.GetFloat("MouseSensetives");
            }
            else
            {
                OptionMouseSlider.value = 250;
                PlayerPrefs.SetFloat("MouseSensetives", 250);
                PlayerPrefs.Save();
            }

            //hазмер интерфейса
            if (PlayerPrefs.GetFloat("Game_Game_InterfaceScale_value") != 0)
            {

                CanvasScaler_Option_Slider.value = PlayerPrefs.GetFloat("Game_Game_InterfaceScale_value");
                Change_Intarface_scaleStart();
            }
            else
            {
                CanvasScaler_Option_Slider.value = 1;
                PlayerPrefs.SetFloat("Game_Game_InterfaceScale_value", 1);
                Change_Intarface_scaleStart();
            }
        }
    }

    

}
