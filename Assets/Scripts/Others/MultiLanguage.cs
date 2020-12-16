using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script used to define the active language within the game.
/// </summary>
public class MultiLanguage : MonoBehaviour
{
    public static MultiLanguage multiLanguage;
    public delegate void LanguageDelegate();
    public static event LanguageDelegate OnLanguageChange;
    public int activeLanguage;

    void Awake()
    {
        multiLanguage = this;

        if (PlayerPrefs.HasKey("ActiveLanguage"))
        {
            activeLanguage = PlayerPrefs.GetInt("ActiveLanguage");
        }
        else
        {
            activeLanguage = 0;
        }
    }

    /// <summary>
    /// Function that we call when we change the language from the options menu.
    /// </summary>
    public void ChangeLanguage()
    {
        if (activeLanguage == 0)
        {
            activeLanguage = 1;
        }
        
        else if (activeLanguage == 1)
        {
            activeLanguage = 0;
        }

        if (OnLanguageChange != null)
        {
            OnLanguageChange();
        }
    }

    public void StartLanguage(int language)
    {
        activeLanguage = language;

        if (OnLanguageChange != null)
        {
            OnLanguageChange();
        }
    }

    /// <summary>
    /// Function called to save the selected language in the PlayePrefs.
    /// </summary>
    public void SaveOptions()
    {
        PlayerPrefs.SetInt("ActiveLanguage", activeLanguage);
        PlayerPrefs.Save();
    }
}
