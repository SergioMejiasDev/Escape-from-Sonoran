using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that controls the main functions of the main menu.
/// </summary>
public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject enemy = null;
    [SerializeField] Transform generationPoint = null;
    [SerializeField] GameObject panelLanguage, panelMenu;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("ActiveLanguage"))
        {
            panelMenu.SetActive(true);
        }
        else
        {
            panelLanguage.SetActive(true);
        }
    }

    void Start()
    {
        GameManager.gameManager.InitialFade();
        StartCoroutine(Generate());
    }

    /// <summary>
    /// Function that is called to generate enemies.
    /// </summary>
    void GenerateEnemy()
    {
        Destroy(Instantiate(enemy, generationPoint.position, enemy.transform.rotation), 15);
    }

    /// <summary>
    /// Coroutine that calls the function to generate enemies after a few seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator Generate()
    {
        while (true)
        {
            GenerateEnemy();
            yield return new WaitForSeconds(Random.Range(3, 15));
        }
    }
}
