using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private float startTime;
    private bool finished = false;

    //private MinijuegoBee minijuego;

    private MinijuegoBee minijuego;

    public GameObject minijuego2;
    public GameObject spawner;

    public GameObject player;

    public GameObject pantallaFinal;

    public TextMeshProUGUI textoFinal;
    void Start()
    {
        startTime = Time.time;
        minijuego = FindObjectOfType<MinijuegoBee>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            float t = Time.time - startTime;


            string seconds = (t % 60).ToString("f2");
            if (seconds == "30,00")
            {
                Finish();
            }

            timerText.text = seconds;
        }
        else if (finished)
        {
            MostrarPantallaFinal();
            spawner.SetActive(false);
            player.SetActive(false);
            //final.SetActive(true);
            //textoFinal.text = "Conseguiste:\n Huevos:" + "\nCalabaza:" + "\nHarina:";
        }
    }

    void MostrarPantallaFinal()
    {
        Animator anim_pantallaFinal = pantallaFinal.GetComponent<Animator>();
        pantallaFinal.SetActive(true);
        anim_pantallaFinal.SetTrigger("aparicion");
    }

    void Finish()
    {
        finished = true;
        timerText.color = Color.red;
    }

    public void Salir()
    {
        Destroy(minijuego.currentTask);
        Destroy(minijuego2);
        minijuego.hitbox.SetActive(true);
        minijuego.rb.bodyType = RigidbodyType2D.Dynamic;
        minijuego.BGmusic.mute = false;
    }
}
