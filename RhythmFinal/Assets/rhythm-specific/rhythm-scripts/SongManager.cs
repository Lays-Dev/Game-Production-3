using Unity.VisualScripting;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;


public class SongManager : MonoBehaviour
{
    public static SongManager instance;
    public AudioSource audioSource;
    public float songDelayInSeconds;
    public int inputDelayInMilliseconds;
    public double marginOfError; //Perfect
    public double marginOfError1; //Great
    public double marginOfError2; //Good
    public Lane[] lanes;
    public LaneMulti[] lanesMulti;
    public bool forBossFight;

    public string fileLocation;
    public float noteTime;
    public float noteSpawnX;
    public float noteTapX;
    //This next float automatically does math using a get command to figure out where to despawn notes to save up memory
    public float noteDespawnX
    { 
        get
            { return noteTapX - (noteSpawnX - noteTapX) + 3f; }
    }   
    //reference to the midifile
   public static MidiFile midiFile;

    private void Start()
    {
        instance = this;
        ReadFromFile();
    }

    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        getDataFromMidi();
    }

    public void getDataFromMidi()
    {
        // This is the function that actually looks inside of the midi file to figure out how many notes total there will be per song. It will then create an array using that amount of notes
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);
        //This starts the audio after certain amount of time
        Invoke(nameof(startSong), songDelayInSeconds);

        if (forBossFight == false)
        {
            foreach (var lane in lanes) lane.setTimeStamps(array);
        }

        else
        {
            foreach (var laneMulti in lanesMulti) laneMulti.setTimeStamps(array);
        }


    }
    public void startSong()
    {
        audioSource.Play();
    }
    //This is what figures out which second the audio is currently in
    public static double GetAudioSourceTime()
    {
        return (double) instance.audioSource.timeSamples / instance.audioSource.clip.frequency;
    }
}
