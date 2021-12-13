using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state = GameState.InicioJuego;
    static private GameObject servicios;    // El gameObject SERVICIOS d�nde est�n todos los servicios
    public GameObject customers;    // El gameObject Spawner donde est�n todos los clientes por atender
    public GameObject[] edificios;

    public static event System.Action<GameState> OnGameStateChanged;

    //  Inicio
    public Vector3 playerSpawnPositionBosque { get; set; }
    public Vector3 playerSpawnPositionPanader�a;
    public Vector3 playerSpawnPositionInicioJuego;
    public Vector3 playerSpawnPosition;
    private GameObject player;
    private AudioSource BG_music;
    private MifaCharacterDialogueManager mifa;
    private CinemachineVirtualCamera panaderia_cam;

    // variables para mantener durante el juego
    public int satisfacci�nAcumulada = 1;
   
    // Booleanos para aparicion desde otra escena
    private bool fromBosque;
    private bool fromPanader�a;

    // Anim panaderia
    private bool panaderia;

    // tutorial bosque
    private bool tutorialBosque;

    // tutorial cocina
    private bool tutorialCocina;
    private GameObject wallToPanader�a;
    private GameObject wallToTown;

    // clientes
    private bool spawned = false;
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
        playerSpawnPositionInicioJuego = new Vector3(-380, -128, 0);
        playerSpawnPosition = playerSpawnPositionInicioJuego;
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
        playerSpawnPositionInicioJuego = new Vector3(-380, -135, 0);
        playerSpawnPosition = playerSpawnPositionInicioJuego;
        playerSpawnPositionBosque = new Vector3(-290, -135, 0);
        playerSpawnPositionPanader�a = new Vector3(-534, -135, 0);

        // Musica de fondo
        BG_music = GameObject.Find("BG_Music").GetComponent<AudioSource>();
        // Mifa
        mifa = GameObject.Find("mifa").GetComponent<MifaCharacterDialogueManager>();

        panaderia_cam = GameObject.Find("Cam_Anim_panader�a").GetComponent<CinemachineVirtualCamera>();

        wallToPanader�a = GameObject.Find("WallToPanader�a");
        wallToPanader�a.SetActive(false);

        // wallToTown is gotten in the Bosque function (when it loads)

        // Booleanos
        panaderia = false; // animaci�n Panaderia

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
                // Activar animaci�n panader�a y hitboxes de cambio de escena
                AnimacionPanaderia();
                break;
            case GameState.ConversacionMifa:
                ConversacionMifa();
                break;
            case GameState.Tutorial: // Tutorial en el bosque
                Tutorial();
                break;
            case GameState.TutorialCocina:  // Despu�s de jugar a un minijuego en el bosque
                tutorialCocina = true;
                //GameObject wallToTown = GameObject.Find("WallToTown");
                wallToTown.SetActive(true);
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
        // Activar la animaci�n, desactivar los dialogos y llamar a la funci�n que termina la anim
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
            Debug.Log("EnBosque");
            Bosque();
        }
        else if (scene.name == "Bakery") 
        {
            Panader�a();
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
    void Pueblo() // Funci�n que se ejecuta al volver al pueblo nada despu�s de AWAKE y antes de START
    {
        // Volviendo a pueblo...
        //Debug.Log("Volviendo a pueblo...");

        // Activar edificios y mostrar la panader�a
        servicios.SetActive(true);
        DesbloquearEdificio(Edificios.Panaderia);
        if (tutorialCocina)
        {
            //wallToPanader�a.SetActive(true); No hacer esto porque en la escena ya est� puesto a true, y como cambiamos de escena se rompe (la referencia se pierde)
            // Poner nueva conversaci�n a Mifa
            mifa = GameObject.Find("mifa").GetComponent<MifaCharacterDialogueManager>();
            mifa.conversationIndex = 3;
        }
       
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
            Debug.Log("Viniendo del bosque...");
            playerSpawnPosition = playerSpawnPositionBosque;
            fromBosque = false;
        }
        else if (fromPanader�a)
        {
            customers.SetActive(false);
            Debug.Log("Viniendo de panader�a...");
            playerSpawnPosition = playerSpawnPositionPanader�a;
            fromPanader�a = false;
        }

    }

    void Bosque() // Funci�n que se ejecuta llegar al bosque
    {
        if (!tutorialCocina) // Si no se ha jugado a un minijuego, no se puede volver
        {
            wallToTown = GameObject.Find("WallToTown");
            wallToTown.SetActive(false);
        }
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
    void Panader�a() 
    {
        if (spawned)
            customers.SetActive(true);
        servicios.SetActive(false);
        playerSpawnPosition = new Vector3(-10, -2, 0);
        fromPanader�a = true;
        spawned = true;
    }

    public void SumarSatisfacci�n(int suma) // O resta si es negativo 
    {
        satisfacci�nAcumulada += suma;
        if (satisfacci�nAcumulada >= 25)
        {
            satisfacci�nAcumulada = 0;
            // Enviar se�al para spawnear cliente especial
            return;
        }
    }

    public void DesbloquearEdificio(Edificios indice) 
    {
        edificios[(int)indice].transform.GetChild(0).gameObject.SetActive(true);
    }
    public enum Edificios 
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