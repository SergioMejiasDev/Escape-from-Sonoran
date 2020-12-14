using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script que se encarga de la escena de los créditos.
/// </summary>
public class Credits : MonoBehaviour
{
    float speed = 50; //Velocidad a la que se mueven los créditos.
    [SerializeField] RectTransform creditsTransform = null; //Transform del panel de los créditos.
    [SerializeField] Text thanksText = null;
    float finalPosition = 1500;

    void Start()
    {
        Cursor.visible = false; //Desactivamos el cursor.
        GameManager.gameManager.InitialFade();
        GameManager.gameManager.ActivateMusic();
        StartCoroutine(CreditsMovement());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            StartCoroutine(CloseCredits());
        }
    }

    IEnumerator CreditsMovement()
    {
        while (creditsTransform.anchoredPosition.y < finalPosition)
        {
            creditsTransform.anchoredPosition += new Vector2(0, speed * Time.deltaTime);   // + hacia arriba | - hacia abajo
            yield return null;
        }

        Color levelColor = thanksText.color; //Creamos una variable Color con los datos de colores del texto.
        float alphaValue; //Creamos una variable para referirnos al valor que tendrá el color alpha (transparencia).

        while (thanksText.color.a < 1) //Mientra el valor de alpha sea inferior a 1.
        {
            alphaValue = levelColor.a + (0.5f * Time.deltaTime); //El valor de alpha sube gradualmente según el tiempo que le hayamos indicado.
            levelColor = new Color(levelColor.r, levelColor.g, levelColor.b, alphaValue);
            thanksText.color = new Color(levelColor.r, levelColor.g, levelColor.b, alphaValue);
            yield return null; //El bucle se repite sin esperas.
        }

        yield return new WaitForSeconds(3); //Espera de 3 segundos.

        while (thanksText.color.a > 0) //Mientra el valor de alpha sea inferior a 1.
        {
            alphaValue = levelColor.a - (0.5f * Time.deltaTime); //El valor de alpha sube gradualmente según el tiempo que le hayamos indicado.
            levelColor = new Color(levelColor.r, levelColor.g, levelColor.b, alphaValue);
            thanksText.color = new Color(levelColor.r, levelColor.g, levelColor.b, alphaValue);
            yield return null; //El bucle se repite sin esperas.
        }

        yield return new WaitForSeconds(4);
        StartCoroutine(CloseCredits());
    }

    IEnumerator CloseCredits()
    {
        GameManager.gameManager.FinalFade();
        yield return new WaitForSeconds(2);
        GameManager.gameManager.DeactivateMusic();
        yield return new WaitForSeconds(3);
        Cursor.visible = true; //Activamos el cursor.
        GameManager.gameManager.LoadScene(0);
    }
}
