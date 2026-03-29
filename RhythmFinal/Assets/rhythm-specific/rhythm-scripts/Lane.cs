using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public InputAction RhythmButton;
    public GameObject notePrefab;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();
    public GameObject player;
    public GameObject RhythmGame;

    public int spawnIndex = 0;
    public int inputIndex = 0;
    public void setTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            //This filters through the notes and is for multi track games. Figures out which note is which and also gets the tempo of the song
            if (note.NoteName == noteRestriction)
            {
                var MetricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)MetricTimeSpan.Minutes * 60f + MetricTimeSpan.Seconds + (double)MetricTimeSpan.Milliseconds / 1000f);
            }
        }
    }


    public void Start()
    {
        RhythmButton = InputSystem.actions.FindAction("RhythmButton");
    }
    void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.instance.noteTime)
            {
                var note = Instantiate(notePrefab, transform);
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;
            }
        }

        if (spawnIndex == timeStamps.Count)
        {
            StartCoroutine(EndSong());
        }
        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.instance.inputDelayInMilliseconds / 1000.0);

            if (Keyboard.current.jKey.wasPressedThisFrame)
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit();
                    Debug.Log("hit");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else
                {
                    Debug.Log("Inaccurate");
                }

            }
            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                Debug.Log("miss");
                inputIndex++;

            }
        }
    }

    private System.Collections.IEnumerator EndSong()
    {
        yield return new WaitForSeconds(3f);
        player.gameObject.SetActive(true);
        RhythmGame.SetActive(false);

    }
    private void Hit()
    {
        ScoreManager.hit();
    }    
    private void Miss()
    {

    }
    }
