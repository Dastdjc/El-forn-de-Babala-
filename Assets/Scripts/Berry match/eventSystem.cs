using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class eventSystem : MonoBehaviour
{
    public TextMeshProUGUI textPuntuacion;
    public GameObject pantallaFinal;
    [HideInInspector] public int puntuacion = 0;
    public GameObject spawner;
    public GameObject pantallaTutorial;

    public bool playing = false;
    private bool finish = false;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
        MostrarTutorial();
    }

    void Update()
    {

        if (!finish && playing)
        {
            float t = Time.time - startTime;
            
            if (puntuacion < 0)
            {
                puntuacion = 0;
            }

            textPuntuacion.text = "Puntuacion: " + puntuacion.ToString();
            if (t > 30.00)
            {
                finish = true;
                playing = false;
            }
        }
        else if (finish)
        {
            Acabar();
            finish = false;
        }
        
    }
    void MostrarTutorial()
    {
        Animator anim_pantallaTutorial = pantallaTutorial.GetComponent<Animator>();
        pantallaTutorial.SetActive(true);
        anim_pantallaTutorial.SetTrigger("aparicion");

    }
    public void EsconderTutorial() 
    {
        Animator anim_pantallaTutorial = pantallaTutorial.GetComponent<Animator>();
        anim_pantallaTutorial.SetTrigger("desaparicion");
        playing = true;
    }
    private void Acabar()
    {
        textPuntuacion.gameObject.SetActive(false);
        spawner.SetActive(false);
        Destroy(GameObject.FindWithTag("Azul"));
        Destroy(GameObject.FindWithTag("Negra"));
        Destroy(GameObject.FindWithTag("Verde"));
        Destroy(GameObject.FindWithTag("Rojo"));

        pantallaFinal.SetActive(true);
        pantallaFinal.GetComponent<Animator>().SetTrigger("aparicion");
        GameObject.Find("MatchingBerries(Clone)/Canvas/PantallaFinalMinijuegoMatchingBerries/Puntuación").GetComponent<TextMeshProUGUI>().text = "Puntuación: " + puntuacion.ToString();
    }
}
