using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public GameObject notePrefab;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();

    int spawnIndex = 0;
    int inputIndex = 0;
    private ParticleSystem hitParticle;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.transform.parent.gameObject.GetComponent<Animator>();
        hitParticle = GetComponentInChildren<ParticleSystem>();
    }
    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f + SongManager.Instance.midiDelayInMilliseconds / 1000f);
            }
        }
    }

    void Update()
    {
        if (SongManager.Instance.playing)
        {
            if (spawnIndex < timeStamps.Count)
            {
                if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
                {
                    var note = Instantiate(notePrefab, transform);
                    notes.Add(note.GetComponent<Note>());
                    note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                    spawnIndex++;
                }
            }

            if (inputIndex < timeStamps.Count)
            {
                double timeStamp = timeStamps[inputIndex];
                double marginOfError = SongManager.Instance.marginOfError;
                double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

                if (Input.GetKeyDown(input))
                {
                    PlayPressedAnimation();
                    if (Math.Abs(audioTime - timeStamp) < marginOfError)    // Dada a tiempo
                    {
                        hitParticle.Play();
                        Hit();
                        //print($"Hit on {inputIndex} note");
                        Destroy(notes[inputIndex].gameObject);
                        inputIndex++;
                    }
                    // else inaccurate (podr?a poner m?s tipos de puntuaci?n XD)
                }
                else if (Input.GetKeyUp(input))
                    PlayReleasedAnimation();
                if (timeStamp + marginOfError <= audioTime) // Si se pierde la nota
                {
                    Miss();
                    //print($"Missed {inputIndex} note");
                    inputIndex++;
                }
            }
        }
    }
    private void PlayPressedAnimation() 
    {
        anim.SetTrigger("pressed");
    }
    private void PlayReleasedAnimation() 
    {
        anim.SetTrigger("released");
    }
    private void Hit()
    {
        ScoreManager.Hit();
    }
    private void Miss()
    {
        ScoreManager.Miss();
    }
}