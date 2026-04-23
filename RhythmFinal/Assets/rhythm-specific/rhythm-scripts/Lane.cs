using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System;
using TMPro;
using System.Collections;

public class Lane : MonoBehaviour

{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public InputAction RhythmButton;
    public GameObject notePrefab;
    List<Note> notes = new List<Note>();
    public Player playerObject;
    public List<double> timeStamps = new List<double>();
    public TextMeshProUGUI accuracyText;
    public bool hasBeenHurt;
    public int LaneID;

    public GameObject questTestPrefab;

    public bool hasBeenCollected;



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
        noteAmount = timeStamps.Count;
        StartCoroutine(EndSong());
        GameObject questTestPrefab = GameObject.FindWithTag("UIQuestTitle");
        GameObject BackgroundMusic = GameObject.FindWithTag("BackgroundMusic");
        BackgroundMusic.GetComponent<AudioSource>().volume = 0.05f ;
       
        questTestPrefab.GetComponent<Canvas>().enabled = false;

    }
    public void Awake()
    {
        playerObject = GameObject.FindFirstObjectByType<Player>();
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

            // changed from hardcoded keys to input system
            if (playerObject != null && playerObject.hitNotePressed)
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError) //  Margin of error is the amount of time that the player can be off by and still have it count as a hit. we will use multiple margins of error to create our PERFECT,GOOD,OKAY, effects
                {
                    Hit();
                    Debug.Log("Perfect");

                    accuracyText.text = "Perfect!";
                    accuracyText.color = new Color(1f, 1f, 0f, 1f); //
                    accuracyText.CrossFadeAlpha(1f, 0f, false); // Reset alpha to 1 before fading out
                    accuracyText.CrossFadeAlpha(0f, 0.5f, false); // Fades out the text over 0.5 seconds              
                    // I will be changing this to not destroy it but instead make it invisible, freeze it in place, and then use particle effects to make things feel more impactful. later.
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else if (Math.Abs(audioTime - timeStamp) < marginOfError1)
                {
                    Hit();
                    Debug.Log("Great");
                    accuracyText.text = "Great";
                    accuracyText.color = new Color(0f, 0f, 1f, 1f); // 
                    accuracyText.CrossFadeAlpha(1f, 0f, false); // Reset alpha to 1 before fading out
                    accuracyText.CrossFadeAlpha(0f, 0.5f, false); // Fades out the text over 0.5 seconds
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else if (Math.Abs(audioTime - timeStamp) < marginOfError2)
                {
                    Hit();
                    Debug.Log("Good");
                    accuracyText.text = "Good";
                    accuracyText.color = new Color(0f, 1f, 0f, 1f); //
                    accuracyText.CrossFadeAlpha(1f, 0f, false); // Reset alpha to 1 before fading out
                    accuracyText.CrossFadeAlpha(0f, 0.5f, false); // Fades out the text over 0.5 seconds
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else
                {
                    Debug.Log("Inaccurate");
                    StartCoroutine(InaccurateDamage());
                }

            }
            // This is what checks if notes are missed completely
            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                Debug.Log("miss");
                StartCoroutine(InaccurateDamage());
                inputIndex++;

            }
        }
    }

    private IEnumerator InaccurateDamage()
    {
        GameObject healthTracking = GameObject.FindWithTag("HealthTracker");
        if (hasBeenHurt == false)
        {
            StartCoroutine(healthTracking.GetComponent<HealthTracking>().TakeSmallDamage());
            hasBeenHurt = true;
        }
        yield return new WaitForSeconds(.1f);
        hasBeenHurt = false;
    }


    //This checks if the song is over and then deactivates the rhythm game and reactivates the player. I will be changing this to a results screen later on.
    private System.Collections.IEnumerator EndSong()
    {
        
        yield return new WaitForSeconds(1f); // Wait for a short delay to avoid the very beginning of the song triggering the end condition
        yield return new WaitUntil(() => spawnIndex == timeStamps.Count); // wait until all notes have been spawned
        yield return new WaitForSeconds(2f); // Wait for a short delay to ensure the last note has been processed
        GameObject questTestPrefab = GameObject.FindWithTag("UIQuestTitle");
        GameObject healthTracking = GameObject.FindWithTag("HealthTracker");

        {


            if (ScoreManager.instance.hitAmount > timeStamps.Count / 1.4)
            {
                endText.text = "Item Collected!";
                if (hasBeenCollected == false)
                {
                    StartCoroutine(questTestPrefab.GetComponent<QuestTest>().collectItem());
                    hasBeenCollected = true;
                    GameObject.FindWithTag("Song").GetComponent<Items>().gameComplete();
                }
            }
            else
            {
                endText.text = "Collection Failed";
                if (hasBeenHurt == false)
                {
                    StartCoroutine(healthTracking.GetComponent<HealthTracking>().TakeDamage());
                    hasBeenHurt = true;
                }

            }
            yield return new WaitForSeconds(2f);
            GameObject BackgroundMusic = GameObject.FindWithTag("BackgroundMusic");
            BackgroundMusic.GetComponent<AudioSource>().volume = 1f;
            playerObject.gameObject.SetActive(true);
            hasBeenCollected = false;
            questTestPrefab.GetComponent<Canvas>().enabled = true;
            playerObject.inRhythmGame = false;
            playerObject.controlLock = false;
            StartCoroutine(healthTracking.GetComponent<HealthTracking>().RefillHealth());
            Destroy(RhythmGame);
            
        }
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