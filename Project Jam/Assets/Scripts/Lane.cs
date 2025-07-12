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
    public GameObject button;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>(); //the scheduled time the note is supposed to be hit/expected hit time of a note
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    int spawnIndex = 0;
    int inputIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }
    // Update is called once per frame
    void Update()
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
            double timeStamp = timeStamps[inputIndex]; //the time the note is supposed to be hit
            double marginOfError = SongManager.Instance.marginOfError; //basically like the interval of time where the note still counts as a hit/how much leeway the hit has
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0); //the current time it is in the song

            if (Input.GetKeyDown(input))
            {
                //so we take the differences between the audio time and the time stamp (the current time it is in the song vs when the note is supposed to be hit)
                //this determines the difference in accuracy of the hit
                //if the player accuracy is outside of the margin of error, the note is missed
                //the conditions are as follows;
                //if you hit the note within 1/4th of the given leeway interval, you have a perfect hit
                if (Math.Abs(audioTime - timeStamp) < marginOfError / 4)
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, button.transform.position, perfectEffect.transform.rotation);
                    print($"Hit on {inputIndex} note");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                //if you hit the note within 1/2 of the given leeway interval, you have a good hit
                else if (Math.Abs(audioTime - timeStamp) < marginOfError / 2)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, button.transform.position, goodEffect.transform.rotation);
                    inputIndex++;
                }
                //if you hit the note within the leeway interval but not within 1/4th or 1/2 of it, you just have a normal hit
                else
                {
                    Debug.Log("Normal");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, button.transform.position, hitEffect.transform.rotation);
                    print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                    inputIndex++;
                }
            }
            //if you hit the note outside of the leeway interval, you have missed the note (or it you did hit the note/press the button period)
            if (timeStamp + marginOfError <= audioTime)
            {
                GameManager.instance.NoteMissed();
                Instantiate(missEffect, button.transform.position, missEffect.transform.rotation);
                print($"Missed {inputIndex} note");
                inputIndex++;
            }
            /*if (Mathf.Abs(transform.position.y) > 0.25)
            {
                Debug.Log("Normal");
                GameManager.instance.NormalHit();
                Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
            }
            else if (Mathf.Abs(transform.position.y) > 0.05f)
            {
                Debug.Log("Good");
                GameManager.instance.GoodHit();
                Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
            }
            else
            {
                Debug.Log("Perfect");
                GameManager.instance.PerfectHit();
                Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
            }*/

            
        }       
    
    }
    /*private void Hit()
    {
        ScoreManager.Hit();
    }
    private void Miss()
    {
        ScoreManager.Miss();
    }*/
}