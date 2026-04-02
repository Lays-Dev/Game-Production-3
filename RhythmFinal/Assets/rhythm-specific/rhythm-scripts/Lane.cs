using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System;
using TMPro;

public class Lane : MonoBehaviour

{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public InputAction RhythmButton;
    public GameObject notePrefab;
    List<Note> notes = new List<Note>();
    public Player playerObject;
    public List<double> timeStamps = new List<double>();
    
    
    
    public GameObject RhythmGame;
    public float noteAmount;
    public TextMeshProUGUI endText;
    

    public int spawnIndex = 0;
    public int inputIndex = 0;


    // This function is what goes through the midi file and gets the time stamps of each note and then adds it to a list. It also filters through the notes to figure out which note is which for multi track games
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
        noteAmount = timeStamps.Count;
        
        


    }
    public void Awake()
    {
        playerObject = GameObject.FindFirstObjectByType < Player > ();
            Debug.Log("Found!");
        playerObject.GetComponent<Player>();
    }
    // This is the main function that spawns the notes and also checks for input. It checks if the note should be spawned and then spawns it. It also checks if the player has hit the note and if they have, it destroys the note instantly.
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
            double marginOfError1 = SongManager.instance.marginOfError1;
            double marginOfError2 = SongManager.instance.marginOfError2;

            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.instance.inputDelayInMilliseconds / 1000.0);

            // HARDCODED KEYBOARD INPUT BAD. WILL CHANGE ONCE PLAYER CONTROLS ARE IN PLACE
            if (Keyboard.current.jKey.wasPressedThisFrame)
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError) //  Margin of error is the amount of time that the player can be off by and still have it count as a hit. we will use multiple margins of error to create our PERFECT,GOOD,OKAY, effects
                {
                    Hit();
                    Debug.Log("Perfect");
                    // I will be changing this to not destroy it but instead make it invisible, freeze it in place, and then use particle effects to make things feel more impactful. later.
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else if (Math.Abs(audioTime - timeStamp) < marginOfError1) //  Margin of error is the amount of time that the player can be off by and still have it count as a hit. we will use multiple margins of error to create our PERFECT,GOOD,OKAY, effects
                {
                    Hit();
                    Debug.Log("Great");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else if (Math.Abs(audioTime - timeStamp) < marginOfError2) //  Margin of error is the amount of time that the player can be off by and still have it count as a hit. we will use multiple margins of error to create our PERFECT,GOOD,OKAY, effects
                {
                    Hit();
                    Debug.Log("Good");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else
                {
                    Debug.Log("Inaccurate");
                }

            }
            // This is what checks if notes are missed completely
            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                Debug.Log("miss");
                inputIndex++;

            }
        }
    }

    //This checks if the song is over and then deactivates the rhythm game and reactivates the player. I will be changing this to a results screen later on.
    private System.Collections.IEnumerator EndSong()
    {
        
        yield return new WaitForSeconds(2f);
        if (ScoreManager.instance.hitAmount > timeStamps.Count / 1.4)
        {
            endText.text = "You win!";
        }
        else
        {
            endText.text = "You lose!";
        }
        yield return new WaitForSeconds(2f);
        playerObject.gameObject.SetActive(true);
        playerObject.inRhythmGame = false;
        playerObject.controlLock = false;
        
        Destroy(RhythmGame);

    }
    private void Hit()
    {
        ScoreManager.hit();
    }    
    private void Miss()
    {
        ScoreManager.miss();
    }
    }
