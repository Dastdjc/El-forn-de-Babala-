using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashImages : MonoBehaviour
{
    public Canvas UI;

    public Image headphones;
    public Image skyfrogLogo;
    public Animator transition;
    public Image blackBG;
    void Start()
    {
        UI.gameObject.SetActive(false);
        MostrarSkyfrog();
    }


    void MostrarSkyfrog() // Se activa el logo y se hace fadeout de la pantalla negra
    {
        skyfrogLogo.gameObject.SetActive(true);
        transition.SetTrigger("fadeOut");
        Invoke("FadeIn", 4f);
        Invoke("MostrarHeadphones", 7f);
    }

    void FadeIn() // Se se empieza a mostrar la pantalla negra
    {
        transition.SetTrigger("fadeIn");
    }

    void MostrarHeadphones() 
    {
        skyfrogLogo.gameObject.SetActive(false);
        headphones.gameObject.SetActive(true);
        transition.SetTrigger("fadeOut");
        Invoke("FadeIn", 4f);
        Invoke("FinalSplash", 7f);
    }
    void FinalSplash() 
    {
        headphones.gameObject.SetActive(false);
        UI.gameObject.SetActive(true);
        blackBG.gameObject.SetActive(false);
        transition.gameObject.SetActive(false);
    }
}
