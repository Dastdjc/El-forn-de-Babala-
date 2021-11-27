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

    public float score;

    public static MidiFile midiFile;

    private bool finished;
    private int mantequilla;
    private int leche;
    private int requeson;
    private int huevos;

    void Start()
    {
        Instance = this;
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
            score = ScoreManager.maxCombo;
            CalcularRecompensa();
            finished = true;
            // Mostrar lo conseguido
            MostrarPantallaFinal();
        }
    }
    void CalcularRecompensa() 
    {
        Debug.Log(score);
        float multiplicador = score / 96;
        mantequilla = (int)(multiplicador * 3 * UnityEngine.Random.Range(0.5f, 1f));
        Debug.Log(mantequilla);
        leche = (int)(multiplicador * 8 * UnityEngine.Random.Range(0.5f, 1f));
        requeson = (int)(multiplicador * 4 * UnityEngine.Random.Range(0.5f, 1f));
        huevos = (int)(multiplicador * 15 * UnityEngine.Random.Range(0.5f, 1f));
    }
    void MostrarPantallaFinal() 
    {
        Animator anim_pantallaFinal = pantallaFinal.GetComponent<Animator>();
        // Poner los datos de los alimentos
        TextMeshProUGUI cant_mantequilla = GameObject.Find("CM vcam1/Musical Bees(Clone)/Canvas/PantallaFinalMinijuego/cant_mantequilla").GetComponent<TextMeshProUGUI>();
        Debug.Log(cant_mantequilla);
        TextMeshProUGUI cant_leche = GameObject.Find("CM vcam1/Musical Bees(Clone)/Canvas/PantallaFinalMinijuego/cant_leche").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cant_requeson = GameObject.Find("CM vcam1/Musical Bees(Clone)/Canvas/PantallaFinalMinijuego/cant_requeson").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cant_huevos = GameObject.Find("CM vcam1/Musical Bees(Clone)/Canvas/PantallaFinalMinijuego/cant_huevos").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI combo = GameObject.Find("CM vcam1/Musical Bees(Clone)/Canvas/PantallaFinalMinijuego/Combo").GetComponent<TextMeshProUGUI>();

        cant_mantequilla.text = mantequilla.ToString();
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

        Invoke("StartSong", songDelayInSeconds);
    }
    public void StartSong()
    {
        audioSource.Play();
        playing = true;
        finished = false;
    }
    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }
}
