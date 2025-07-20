using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using Melanchall.DryWetMidi.MusicTheory;
using UnityEngine.Events;
using System.Runtime.CompilerServices;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance; // access song manager from other classes
    public AudioSource audioSource; // our music goes here
    public float songDelayInSeconds; // delay our song after a certain amount of time
    public int inputDelayInMilliseconds; // in case there's a keyboard issue and we have input delay
    public string fileLocation; // place in StreamingAssets where the MIDI file is kept
    public float noteTime; // how much time the note will be on screen
    public float noteSpawnY, defendNoteSpawnY, attackNoteSpawnY; // where the notes spawn
    public float noteTapY, defendNoteTapY, attackNoteTapY; // where the notes need to be tapped
    public Lane[] lanes; // your custom class for input lanes
    public double marginOfError; // in seconds

    public float noteDespawnY
    // calculates the despawn Y position by mirroring the spawn-tap distance on the other side of the tap point
    // so this works for both up and down scrolling directions because it uses actual spawn and tap Y positions (so it adjusts for both the attack and defend phase)

    {
        get
        {
            return noteTapY - (noteSpawnY - noteTapY);
        }
    }
    public static MidiFile midiFile; // where the midi file is loaded into memory
    public float bpm;
    public Intervals[] intervals;


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

    }
    void Update()
    {
        foreach (Intervals interval in intervals)
        {
            //time that we are elapsed divided by intervals
            float sampledTime = audioSource.timeSamples / (audioSource.clip.frequency * interval.GetIntervalLength(bpm));
            interval.CheckForNewInterval(sampledTime);
        }
    }


    private IEnumerator ReadFromWebsite()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + fileLocation))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
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
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);

        foreach (var lane in lanes)
        {
            lane.SetTimeStamps(array); // this method will live in your Lane.cs
        }

        Invoke(nameof(StartSong), songDelayInSeconds);
    }

    public void StartSong()
    {
        audioSource.Play();
    }

    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }


}
[System.Serializable]
public class Intervals
{
        public float steps;
        public UnityEvent trigger;


        private int lastInterval;

        //length of our current beat we are trying to track
        public float GetIntervalLength(float bpm)
        {
            return 60f / (bpm * steps);
        }
        //check if we crossed into a new beat or not 
        public void CheckForNewInterval(float interval)
        {
            //round down to the nearest whole number and if we have a change in whole number we know we have another beat
            if (Mathf.FloorToInt(interval) != lastInterval)
            {
                lastInterval = Mathf.FloorToInt(interval);
                trigger.Invoke();
            }
        }
        
}
  
