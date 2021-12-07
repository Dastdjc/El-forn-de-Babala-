using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state = GameState.InicioJuego;
    public GameObject[] edificios;
    static private GameObject servicios;

    public static event System.Action<GameState> OnGameStateChanged;

    //  Inicio
    public Vector3 playerSpawnPositionBosque { get; set; }
    public Vector3 playerSpawnPositionPanadería;
    public Vector3 playerSpawnPositionInicioJuego;
    public Vector3 playerSpawnPosition;
    private GameObject player;
    private AudioSource BG_music;
    private MifaCharacterDialogueManager mifa;
    private CinemachineVirtualCamera panaderia_cam;


    // Booleanos para aparicion desde otra escena
    private bool fromBosque;
    private bool fromPanadería;

    // Anim panaderia
    private bool panaderia;

    // tutorial bosque
    private bool tutorialBosque;

    // tutorial cocina
    private bool tutorialCocina;

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
        // scenes
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Crear un singleton de todos los edifcios, de forma que al cambiar de escena no se destruyan
        servicios = GameObject.Find("SERVICIOS");
        DontDestroyOnLoad(servicios);

        // referencia al jugador y sus spawns
        player = GameObject.Find("Dore_player");
        playerSpawnPositionInicioJuego = new Vector3(-380, -128, 0);
        playerSpawnPositionBosque = new Vector3(-290, -128, 0);
        playerSpawnPositionPanadería = new Vector3(-345, -128, 0);

        // Musica de fondo
        BG_music = GameObject.Find("BG_Music").GetComponent<AudioSource>();
        // Mifa
        mifa = GameObject.Find("mifa").GetComponent<MifaCharacterDialogueManager>();
        panaderia_cam = GameObject.Find("Anim_panadería").GetComponent<CinemachineVirtualCamera>();

        // Booleanos
        // animación Panaderia
        panaderia = false;

        tutorialBosque = false;
        tutorialCocina = false;

        UpdateGameState(state);
    }

    void Update()
    {
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.InicioJuego:
                // No se permite pasar al jugador hacia el bosque
                break;
            case GameState.AnimacionPanaderia:
                // Activar animación panadería y hitboxes de cambio de escena
                AnimacionPanaderia();
                break;
            case GameState.ConversacionMifa:
                ConversacionMifa();
                break;
            case GameState.Tutorial: // Tutorial en el bosque
                Tutorial();
                break;
            case GameState.TutorialCocina:
                tutorialCocina = true;
                break;

            case GameState.Bosque:
                //Bosque();
                break;
            case GameState.Pueblo:
                //Pueblo();
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
        GameObject.Find("PanaderiaSound").GetComponent<AudioSource>().Play();
        Invoke("FinAnimPanaderia", 4f);
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

    void Tutorial() // Tutorial del bosque
    {
        if (!panaderia)
        {
            AnimacionPanaderia();
        }
        mifa.conversationIndex = 2;
        GameObject.Find("InitialCollider").SetActive(false);
    }

    // Cuando se cargan las escenas, vigilar las cosas que haga falta
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        if (scene.name == "Pueblo_Final")
        {
            Pueblo();
        }
        else if (scene.name == "MarcParallax_Dash") 
        {
            Bosque();
        }
        else if (scene.name == "Bakery") 
        {
            Panadería();
        }
        // Los gameobjects de los edificios. Hacer que sea un singleton
        foreach (GameObject newServicios in GameObject.FindGameObjectsWithTag("Servicios"))
        {
            if (newServicios == null)
            {
                servicios = newServicios;
                DontDestroyOnLoad(servicios);
            }
            else if (newServicios != servicios)
                Destroy(newServicios);
        }
    }
    void Pueblo() // Función que se ejecuta al volver al pueblo nada después de AWAKE y antes de START
    {
        if (tutorialCocina)
            GameObject.Find("WallToPanadería").SetActive(true);
        // Activar edificios y mostrar la panadería
        servicios.SetActive(true);
        Animator panaderia_anim = edificios[(int)Edificios.Panaderia].GetComponent<Animator>();
        panaderia_anim.SetTrigger("panaderia2");

        // Desactivar el hitbox que note deja pasar al bosque
        GameObject.Find("InitialCollider").SetActive(false);

        // Play BG music
        BG_music = GameObject.Find("BG_Music").GetComponent<AudioSource>();
        BG_music.Play();

        // Update Spawn position
        if (state == GameState.InicioJuego)
            playerSpawnPosition = playerSpawnPositionInicioJuego; //player.transform.position = playerSpawnPositionBosque; HAD TO USE LOCAL POSITION BECAUSE IT WAS A CHILD 
        else if (fromBosque)
        {
            playerSpawnPosition = playerSpawnPositionBosque;
            fromBosque = false;
        }
        else if (fromPanadería)
        {
            playerSpawnPosition = playerSpawnPositionPanadería;
            fromPanadería = false;
        }

    }

    void Bosque() // Función que se ejecuta llegar al bosque
    {
        if (!tutorialCocina)    // First time in the bosque
            GameObject.Find("WallToTown").SetActive(false);

        servicios.SetActive(false);
        playerSpawnPosition = new Vector3(-95, -12);
        if (!tutorialBosque) 
        {
            Debug.Log("Tutorial");
            GameObject.Find("TutorialBosque").GetComponent<TutorialCharacterDialogueManager>().tutorialActivated = true;
            tutorialBosque = true;
        }
        fromBosque = true;
    }
    void Panadería() 
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
        TutorialCocina,
        Bosque,
        Pueblo
    }
}