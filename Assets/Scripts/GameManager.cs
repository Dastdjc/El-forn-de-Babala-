using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state = GameState.InicioJuego;
    public GameObject[] edificios;
    private GameObject servicios;
    private GameObject newServicios;

    public static event System.Action<GameState> OnGameStateChanged;

    //  Inicio
    public Vector3 playerSpawnPositionBosque { get; set; }
    private GameObject player;
    private AudioSource BG_music;
    private MifaCharacterDialogueManager mifa;
    private CinemachineVirtualCamera panaderia_cam;


    // Anim panaderia
    private bool panaderia;

    // Siguientes estados

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this) 
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        //UpdateGameState(GameState.InicioJuego);

        player = GameObject.Find("Dore_player");
        playerSpawnPositionBosque = new Vector3(-290, -128, 0);

        // Los gameobjects de los edificios
        newServicios = GameObject.Find("SERVICIOS");
        if (servicios == null)
        {
            servicios = newServicios;
            DontDestroyOnLoad(servicios);
        }
        else if (newServicios != servicios)
            Destroy(newServicios);

        // Musica de fondo
        BG_music = GameObject.Find("BG_Music").GetComponent<AudioSource>();
        // Mifa
        mifa = GameObject.Find("mifa").GetComponent<MifaCharacterDialogueManager>();
        panaderia_cam = GameObject.Find("Anim_panadería").GetComponent<CinemachineVirtualCamera>();

        // Panaderia
        panaderia = false;

        UpdateGameState(state);
    }

    void Update()
    {
        /*if (state == GameState.InicioJuego)
        {
            if (mifa.conversationIndex == 1)
            {
                UpdateGameState(GameState.AnimacionPanaderia);
            }
        }*/
        //else if (state == GameState.AnimacionPanaderia) { 

        //}
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.InicioJuego:
                // Desactivar hitboxes para cambio de escena
                break;
            case GameState.AnimacionPanaderia:
                // Activar animación panadería y hitboxes de cambio de escena
                AnimacionPanaderia();
                break;
            case GameState.ConversacionMifa:
                ConversacionMifa();
                break;
            case GameState.Tutorial:
                break;
            case GameState.Bosque:
                Bosque();
                break;
            case GameState.Pueblo:
                Pueblo();
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    void AnimacionPanaderia() {
        // Activar la animación, desactivar los dialogos y llamar a la función que termina la anim
        Animator panaderia_anim = edificios[(int)Edificios.Panaderia].GetComponent<Animator>();
        panaderia_anim.SetTrigger("panaderia");

        mifa.enabled = false;   // Esto el dialogo de mifa
        
        panaderia_cam.Priority = 11;
        CameraShake.Instance.ShakeCamera(15f,5f);
        Invoke("FinAnimPanaderia", 3f);
    }

    void FinAnimPanaderia()
    {
        panaderia = true;
        mifa.enabled = true;
        panaderia_cam.Priority = 0;
        BG_music.Play();
    }

    void ConversacionMifa() 
    {
        if (!panaderia)
        {
            AnimacionPanaderia();
        }
        mifa.conversationIndex = 1;
    }

    void Pueblo() 
    {
        servicios.SetActive(true);
        BG_music.Play();
        player = GameObject.Find("Dore_player");
        player.transform.position = playerSpawnPositionBosque;
    }

    void Bosque() 
    {
        servicios.SetActive(false);
    }
    private enum Edificios 
    { 
        Panaderia,
        Gas,
        Tienda,
        Escuela,
        Hospital,
        Cine,
        Ayuntamiento
    }
    public enum GameState
    {
        InicioJuego,
        AnimacionPanaderia,
        ConversacionMifa,
        Tutorial,
        Bosque,
        Pueblo
    }
}