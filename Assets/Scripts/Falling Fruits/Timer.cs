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

    public GameObject spawner;

    public GameObject player;

    public GameObject final;

    public GameObject minijuego;

    public TextMeshProUGUI textoFinal;
    void Start()
    {
        startTime = Time.time;

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
            spawner.SetActive(false);
            player.SetActive(false);
            final.SetActive(true);
        }
    }

    void Finish()
    {
        finished = true;
        timerText.color = Color.red;
    }

    public void Salir()
    {
        Destroy(minijuego);
    }
}
