using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script that modifies the texts depending on the active language.
/// </summary>
public class TextManager : MonoBehaviour
{
    [SerializeField] string english = null;
    [SerializeField] string spanish = null;

    [SerializeField] bool menuText = false;

    private void OnEnable()
    {
        MultiLanguage.OnLanguageChange += LanguageChange;

        if (menuText && MultiLanguage.multiLanguage)
        {
            LanguageChange();
        }
    }

    private void OnDisable()
    {
        MultiLanguage.OnLanguageChange -= LanguageChange;
    }

    private void Start()
    {
        LanguageChange();
    }

    /// <summary>
    /// Function that is called every time the language is changed from the options menu.
    /// </summary>
    void LanguageChange()
    {
        Text textToChange = GetComponent<Text>();

        if (MultiLanguage.multiLanguage.activeLanguage == 0)
        {
            textToChange.text = english.Replace("\\n", "\n");
        }

        else if (MultiLanguage.multiLanguage.activeLanguage == 1)
        {
            textToChange.text = spanish.Replace("\\n", "\n");
        }
    }
}
