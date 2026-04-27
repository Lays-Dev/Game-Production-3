using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaneMulti : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public InputAction RhythmButton;
    public GameObject notePrefab;
    
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();
    
    public BossManager BossFight;
    





    public GameObject RhythmGame;
    public float noteAmount;
    


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
        noteAmount = timeStamps.Count;
        StartCoroutine(EndSong());
        GameObject questTestPrefab = GameObject.FindWithTag("UIQuestTitle");
        GameObject BackgroundMusic = GameObject.FindWithTag("BackgroundMusic");
        BackgroundMusic.GetComponent<AudioSource>().volume = 0.05f;
        
        questTestPrefab.GetComponent<Canvas>().enabled = false;
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
        if (spawnIndex >= timeStamps.Count)
        {
            StartCoroutine(EndSong());
        }

    }
    public System.Collections.IEnumerator EndSong()
    {
        yield return new WaitForSeconds(1f); // Wait for a short delay to avoid the very beginning of the song triggering the end condition
        yield return new WaitUntil(() => spawnIndex == timeStamps.Count); // wait until all notes have been spawned
        yield return new WaitForSeconds(6f);
        GameObject BackgroundMusic = GameObject.FindWithTag("BackgroundMusic");
        BackgroundMusic.GetComponent<AudioSource>().volume = 1f;
        GameObject Player = GameObject.FindWithTag("Player");
        Player.GetComponent<InspectObject>().enabled = true;
        GameObject WinScreen = GameObject.FindWithTag("WinScreen");
        WinScreen.GetComponent<ExitToMainMenu>().enabled = true;
        WinScreen.GetComponent<Canvas>().enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        Destroy(RhythmGame);
    }

}
