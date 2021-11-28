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
    public Vector3 playerSpawnPosition;
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
        // scenes
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Crear un singleton de todos los edifcios, de forma que al cambiar de escena no se destruyan
        servicios = GameObject.Find("SERVICIOS");
        DontDestroyOnLoad(servicios);

        // referencia al jugador y sus spawns
        player = GameObject.Find("Dore_player");
        playerSpawnPositionBosque = new Vector3(-290, -128, 0);

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
                Tutorial();
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

    void Tutorial() 
    {
        GameObject.Find("InitialCollider").SetActive(false);
    }
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
        // Los gameobjects de los edificios
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
    void Pueblo() // Función que se ejecuta al vovler al pueblo nada después de AWAKE y antes de START
    {
        // Activar edificios y dejar la panadería
        servicios.SetActive(true);
        Animator panaderia_anim = edificios[(int)Edificios.Panaderia].GetComponent<Animator>();
        panaderia_anim.SetTrigger("panaderia2");

        // Desactivar el hitbox que note deja pasar al bosque
        GameObject.Find("InitialCollider").SetActive(false);

        // Play BG music
        BG_music = GameObject.Find("BG_Music").GetComponent<AudioSource>();
        BG_music.Play();
        
        // Update Spawn position
        playerSpawnPosition = playerSpawnPositionBosque;
        //player.transform.position = playerSpawnPositionBosque; HAD TO USE LOCAL POSITION BECAUSE IT WAS A CHILD 
    }

    void Bosque() // Función que se ejecuta llegar al bosque
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