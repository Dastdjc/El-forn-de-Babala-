using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;
using TMPro;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public AudioSource audioSource;
    public int bpm = 92;
    public Lane[] lanes;
    public float songDelayInSeconds;
    public int midiDelayInMilliseconds;
    public double marginOfError; // in seconds
    public bool playing = false;
    
    public int inputDelayInMilliseconds;

    public string fileLocation;
    public float noteTime;
    public float noteSpawnScale = 3f;
    public float noteTapScale = 1f;
    public float noteDespawnScale = 0f;

    public GameObject pantallaFinal;
    public GameObject pantallaTutorial;

    public float score;

    public static MidiFile midiFile;

    private bool finished;
    private int agua;
    private int leche;
    private int requeson;
    private int huevos;

    void Start()
    {
        Instance = this;
        MostrarTutorial();
        if (Application.streamingAssetsPath.StartsWith("http://") || Application.streamingAssetsPath.StartsWith("https://"))
        {
            StartCoroutine(ReadFromWebsite());
        }
        else
        {
            ReadFromFile();
        }
        finished = true;
    }

    private void Update()
    {
        if (!audioSource.isPlaying && !finished) 
        {
            score = ScoreManager.score;
            CalcularRecompensa();
            RecompensaAInventario();
            finished = true;
            // Mostrar lo conseguido
            MostrarPantallaFinal();
        }
    }
    void CalcularRecompensa() 
    {
        Debug.Log(score);
        float multiplicador = score / 1500;   // Max score = 9700
        agua = (int)(multiplicador * 1 * UnityEngine.Random.Range(0.8f, 1f));
        leche = (int)(multiplicador * 3 * UnityEngine.Random.Range(0.8f, 1f));
        requeson = (int)(multiplicador * 3 * UnityEngine.Random.Range(0.8f, 1f));
        huevos = (int)(multiplicador * 4 * UnityEngine.Random.Range(0.8f, 1f));
    }

    void RecompensaAInventario() 
    {
        Inventory inventario = GameObject.Find("INVENTORY/Inventory").GetComponent<Inventory>();

        Items itemAgua = ScriptableObject.CreateInstance<Items>();
        itemAgua.amount = agua;
        itemAgua.type = "Agua";
        inventario.AddIngrItem(itemAgua);

        Items itemLeche = ScriptableObject.CreateInstance<Items>();
        itemLeche.amount = leche;
        itemLeche.type = "Leche";
        inventario.AddIngrItem(itemLeche);

        Items itemRequeson = ScriptableObject.CreateInstance<Items>();
        itemRequeson.amount = requeson;
        itemRequeson.type = "Requesón";
        inventario.AddIngrItem(itemRequeson);

        Items itemHuevos = ScriptableObject.CreateInstance<Items>();
        itemHuevos.amount = huevos;
        itemHuevos.type = "Huevos";
        inventario.AddIngrItem(itemHuevos);
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
        Invoke("StartSong", songDelayInSeconds);

    }
    void MostrarPantallaFinal() 
    {
        Animator anim_pantallaFinal = pantallaFinal.GetComponent<Animator>();
        // Poner los datos de los alimentos
        TextMeshProUGUI cant_agua = GameObject.Find("CM vcam1/Musical Bees(Clone)/Canvas/PantallaFinalMinijuegoMB/cant_agua").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cant_leche = GameObject.Find("CM vcam1/Musical Bees(Clone)/Canvas/PantallaFinalMinijuegoMB/cant_leche").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cant_requeson = GameObject.Find("CM vcam1/Musical Bees(Clone)/Canvas/PantallaFinalMinijuegoMB/cant_requeson").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cant_huevos = GameObject.Find("CM vcam1/Musical Bees(Clone)/Canvas/PantallaFinalMinijuegoMB/cant_huevos").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI combo = GameObject.Find("CM vcam1/Musical Bees(Clone)/Canvas/PantallaFinalMinijuegoMB/Combo").GetComponent<TextMeshProUGUI>();

        cant_agua.text = agua.ToString();
        cant_leche.text = leche.ToString();
        cant_requeson.text = requeson.ToString();
        cant_huevos.text = huevos.ToString();
        if (score >= 96)
           combo.text = "PERFECT SCORE: " + score.ToString();
        combo.text = "Max combo: " + score.ToString();

        pantallaFinal.SetActive(true);
        anim_pantallaFinal.SetTrigger("aparicion");
    }

    private IEnumerator ReadFromWebsite()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + fileLocation))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ProtocolError) 
            {
                Debug.LogError(www.error);
            }
            else
            {
                byte[] results = www.downloadHandler.data;
                using (var stream = new MemoryStream(results))
                {
                    midiFile = MidiFile.Read(stream);
                    GetDataFromMidi();
                }
            }
        }
    }

    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }
    public void GetDataFromMidi()
    {
        midiFile.ReplaceTempoMap(TempoMap.Create(Tempo.FromBeatsPerMinute(bpm)));    // Cambiar el tempoMap por el BPM
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);

        foreach (var lane in lanes) lane.SetTimeStamps(array);

        //Invoke("StartSong", songDelayInSeconds);
    }
    public void StartSong()
    {
        pantallaTutorial.SetActive(false);
        audioSource.Play();
        playing = true;
        finished = false;
    }
    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }
}
