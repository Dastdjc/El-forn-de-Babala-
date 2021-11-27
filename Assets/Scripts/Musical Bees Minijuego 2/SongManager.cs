using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;

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
            finished = true;
            // Mostrar lo conseguido
            Debug.Log("Finished");
            MostrarPantallaFinal();
        }
    }

    void MostrarPantallaFinal() 
    {
        Animator anim_pantallaFinal = pantallaFinal.GetComponent<Animator>();
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
