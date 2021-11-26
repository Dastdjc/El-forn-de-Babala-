using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state;
    public GameObject[] edificios;

    public static event System.Action<GameState> OnGameStateChanged;

    private MifaCharacterDialogueManager mifa;
    private CinemachineVirtualCamera panaderia_cam;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        UpdateGameState(GameState.InicioJuego);

        mifa = GameObject.Find("mifa").GetComponent<MifaCharacterDialogueManager>();
        panaderia_cam = GameObject.Find("Anim_panadería").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
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
                break;
            case GameState.Tutorial:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    void AnimacionPanaderia() {
        // Activar la animación, desactivar los dialogos y llamar a la función que termina la anim
        Animator panaderia_anim = edificios[(int)Edificios.Panaderia].GetComponent<Animator>();
        panaderia_anim.SetTrigger("panaderia");

        mifa.enabled = false;   // Esto el el dialogo de mifa
        
        panaderia_cam.Priority = 11;
        CameraShake.Instance.ShakeCamera(15f,5f);
        Invoke("FinAnimPanaderia", 3f);
    }

    void FinAnimPanaderia()
    {
        mifa.enabled = true;
        panaderia_cam.Priority = 0;
        GameObject.Find("BG_Music").GetComponent<AudioSource>().Play();
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
        Tutorial
    }
}