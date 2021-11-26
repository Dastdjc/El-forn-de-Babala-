using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state;

    public static event System.Action<GameState> OnGameStateChanged;

    private GameObject mifa;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        UpdateGameState(GameState.InicioJuego);
        mifa = GameObject.Find("mifa");
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.AnimacionPanaderia) { 

        }
    }

    public void UpdateGameState(GameState newState) 
    {
        state = newState;

        switch (newState)
        {
            case GameState.InicioJuego:
                break;
            case GameState.AnimacionPanaderia:
                break;
            case GameState.ConversacionMifa:
                break;
            case GameState.Tutorial:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public enum GameState
{
    InicioJuego,
    AnimacionPanaderia,
    ConversacionMifa,
    Tutorial
}
}