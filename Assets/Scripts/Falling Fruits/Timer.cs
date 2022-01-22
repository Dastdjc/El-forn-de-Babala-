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

    public GameObject pantallaTutorial;
    public GameObject player;

    public GameObject pantallaFinal;

    public TextMeshProUGUI textoFinal;
    private Collider2D colliderPlayer;

    private bool playing = true;
    void Start()
    {
        spawner.SetActive(false);
        timerText.enabled = false;
        minijuego = FindObjectOfType<MinijuegoBee>();
        MostrarTutorial();
        colliderPlayer = player.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (playing && !finished)
        {
            float t = Time.time - startTime;


            string seconds = (t % 60).ToString("f2");
            if (seconds == "30,00")
            {
                Finish();
            }

            timerText.text = seconds;
        }
        else if (finished && !playing)
        {
            RecompensaAInventario();
            MostrarPantallaFinal();
            spawner.SetActive(false);
            player.SetActive(false);
            finished = false;
            //final.SetActive(true);
            //textoFinal.text = "Conseguiste:\n Huevos:" + "\nCalabaza:" + "\nHarina:";
        }
    }
    void RecompensaAInventario()
    {
        // Aquí se hace referencia al INVENTARIO
        Inventory Inventario = Inventory.Instance;

        minijuegoMovement ingredientes = player.GetComponent<minijuegoMovement>();

        Items Calabaza = ScriptableObject.CreateInstance<Items>();
        Calabaza.type = "Calabaza";
        Calabaza.amount = ingredientes.calabaza;
        Debug.Log(ingredientes.calabaza);
        Inventario.AddIngrItem(Calabaza);

        Items Almendra = ScriptableObject.CreateInstance<Items>();
        Almendra.type = "Almendra";
        Almendra.amount = ingredientes.almendra;
        Inventario.AddIngrItem(Almendra);

        Items Limon = ScriptableObject.CreateInstance<Items>();
        Limon.type = "Limón";
        Limon.amount = ingredientes.limon;
        Inventario.AddIngrItem(Limon);

        Items Boniato = ScriptableObject.CreateInstance<Items>();
        Boniato.type = "Boniato";
        Boniato.amount = ingredientes.boniato;
        Inventario.AddIngrItem(Boniato);

    }
    void MostrarPantallaFinal()
    {
        Animator anim_pantallaFinal = pantallaFinal.GetComponent<Animator>();

        // Poner los datos de los alimentos
        TextMeshProUGUI cant_limon = GameObject.Find("CM vcam1/Falling Fruits(Clone)/Canvas/PantallaFinalMinijuegoFF/cant_limon").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cant_almendra = GameObject.Find("CM vcam1/Falling Fruits(Clone)/Canvas/PantallaFinalMinijuegoFF/cant_almendra").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cant_boniato = GameObject.Find("CM vcam1/Falling Fruits(Clone)/Canvas/PantallaFinalMinijuegoFF/cant_boniato").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cant_calabaza = GameObject.Find("CM vcam1/Falling Fruits(Clone)/Canvas/PantallaFinalMinijuegoFF/cant_calabaza").GetComponent<TextMeshProUGUI>();

        minijuegoMovement ingredientes = player.GetComponent<minijuegoMovement>();

        cant_limon.text = ingredientes.limon.ToString();
        cant_almendra.text = ingredientes.almendra.ToString();
        cant_boniato.text = ingredientes.boniato.ToString();
        cant_calabaza.text = ingredientes.calabaza.ToString();

        pantallaFinal.SetActive(true);
        anim_pantallaFinal.SetTrigger("aparicion");
    }

    void Finish()
    {
        finished = true;
        timerText.color = Color.red;
        playing = false;
    }

    public void Salir()
    {
        Destroy(minijuego.currentTask);
        Destroy(minijuego2);
        minijuego.hitbox.SetActive(true);
        minijuego.rb.bodyType = RigidbodyType2D.Dynamic;
        minijuego.BGmusic.mute = false;
        Inventory.Instance.inMinigame = false;
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
        spawner.SetActive(true);
        startTime = Time.time;
        timerText.enabled = true;
        colliderPlayer.isTrigger = false;

    }
}
