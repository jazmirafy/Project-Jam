using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public Melanchall.DryWetMidi.MusicTheory.NoteName healNote;
    public Melanchall.DryWetMidi.MusicTheory.NoteName damageNote;
    public KeyCode keyboardInput;
    public KeyCode controllerInput;
    public playerController playerController;
    public RobotController robotController;
    public UIManager UIManager;
    public GameObject notePrefab;
    public GameObject button;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>(); //the scheduled time the note is supposed to be hit/expected hit time of a note
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    int spawnIndex = 0;
    int inputIndex = 0;
    int robotAnimIndex = 0;
    public static bool onAttackPhase;
    public GameObject attackImage;
    public GameObject defendImage;

    public GameObject attackBackground;
    public GameObject defendBackground;


    // Start is called before the first frame update
    void Start()
    {

        //the attack phase goes first in the song
        onAttackPhase = true;
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
        // Debug.Log("Connected Joysticks: " + Input.GetJoystickNames().Length);
        // foreach (var name in Input.GetJoystickNames())
        // {
        //     Debug.Log("Joystick detected: " + name);
        // }
        // Debug.Log($"{controllerInput}: {Input.GetAxisRaw(controllerInput)}");
        //     for (int i = 1; i <= 20; i++)
        // {
        //     float value = Input.GetAxisRaw("Axis " + i);
        //     if (Mathf.Abs(value) > 0.1f)
        //         Debug.Log($"Axis {i}: {value}");
        // }
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
        

        double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0); //the current time it is in the song
        if (robotAnimIndex < timeStamps.Count)
        {
            double robotTimestamp = timeStamps[robotAnimIndex];
        

            // if the audio time has just passed this timestamp
            if (audioTime >= robotTimestamp)
            {
                //only animate the robot directional poses if we are on the defend phase
                if (!onAttackPhase)
                {
                    robotController.TriggerNoteAnimation(noteRestriction);
                }

                robotAnimIndex++; // move to the next note for animation
            }
        }
        if (GameManager.instance.transitionIndex < GameManager.instance.transitionTimes.Length && audioTime >= GameManager.instance.currentTransitionTime) //if the variable we are using to track the elapsed time in the song = GameManager.instance.currentTransitionTime
        //i put >= to give the float a little leeway since it may not catch exactly on the time's decimal places
        //also check the transition index and the transition time array length so stuff doesnt go out of bound
        {
            if (GameManager.instance.transitionIndex % 2 == 0) //if the transition index is even transition to defend since we start the song attacking, defend transitions will always be on an even transition index
            {
                TransitionToDefend();
            }
            else
            {
                TransitionToAttack();
            }
        }
        if (audioTime >= GameManager.instance.gameEndTime)
        {
            GameManager.instance.gameEnded = true;
        }

        if (inputIndex < timeStamps.Count)
        {

            double timeStamp = timeStamps[inputIndex]; //the time the note is supposed to be hit
            double marginOfError = SongManager.Instance.marginOfError; //basically like the interval of time where the note still counts as a hit/how much leeway the hit has

            //if u tapped a note thats not a damage note
            if ((Input.GetKeyDown(keyboardInput) || Input.GetKeyDown(controllerInput)) && noteRestriction != damageNote)
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
                    SoundManager.PlaySound(SoundType.PerfectNote, .4f);
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
                    SoundManager.PlaySound(SoundType.HitNote, .4f);
                    Instantiate(goodEffect, button.transform.position, goodEffect.transform.rotation);
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;

                }
                //if you hit the note within the leeway interval but not within 1/4th or 1/2 of it, you just have a normal hit
                else if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Debug.Log("Normal");
                    GameManager.instance.NormalHit();
                    SoundManager.PlaySound(SoundType.HitNote, .4f);
                    Instantiate(hitEffect, button.transform.position, hitEffect.transform.rotation);
                    print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;

                }
                //if the note they hit was a healing note
                if (noteRestriction == healNote)
                {
                    //triple the players health by healing an extra health here (double the health here and one more time in the game manager script)
                    GameManager.instance.healthManager.PlayerHeal(GameManager.instance.healAmount * 2);
                }
            }
            //if u hit a note and its a damage note u basically get the consequences of missing a note
            else if ((Input.GetKeyDown(keyboardInput) || Input.GetKeyDown(controllerInput)) && Math.Abs(audioTime - timeStamp) < marginOfError && noteRestriction == damageNote)
            {
                GameManager.instance.NoteMissed();
                SoundManager.PlaySound(SoundType.MissNote, .4f);
                Instantiate(missEffect, button.transform.position, missEffect.transform.rotation);
                playerController.missAnimation();
                print($"Missed {inputIndex} note");
                inputIndex++;
            }
            //if you hit the note outside of the leeway interval, you have missed the note (or if you didnt hit the note/press the button period)
            //basically if the current time that the audio is at right now is greaterthan or equal to the time the note way supposed to be hit 
            //plus the margin+ of error (leeway time) the user took too long and you have missed the note
            if (audioTime >= timeStamp + marginOfError)
            {

                //if you dodge the damage note, count it as doing a perfect note and show perfect
                if (noteRestriction == damageNote)
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    SoundManager.PlaySound(SoundType.PerfectNote, .4f);
                    Instantiate(perfectEffect, button.transform.position, perfectEffect.transform.rotation);
                    print($"Hit on {inputIndex} note");
                    inputIndex++;
                }
                else
                {
                    GameManager.instance.NoteMissed();
                    Instantiate(missEffect, button.transform.position, missEffect.transform.rotation);
                    SoundManager.PlaySound(SoundType.MissNote, .4f);
                    playerController.missAnimation();
                    print($"Missed {inputIndex} note");
                    inputIndex++;
                }
            }




        }

    }
        public void TransitionToAttack()
    {
        //UI transition pop up
        StartCoroutine(UIManager.ShowPopUp(attackImage, 3f));
        //UI transition background
        UIManager.ShowNewBackground(attackBackground, defendBackground);
        ///switch note tap and note spawn values
        onAttackPhase = true;
        SongManager.Instance.noteSpawnY = SongManager.Instance.attackNoteSpawnY;
        SongManager.Instance.noteTapY = SongManager.Instance.attackNoteTapY;
        
        /// change the position to the buttons to the note tap value
       GameManager.instance.tapButtons.transform.position = new Vector3(GameManager.instance.tapButtons.transform.position.x, SongManager.Instance.noteTapY, GameManager.instance.tapButtons.transform.position.z);
       // GameManager.instance.onAttackPhase = true; // this will trigger the change in note drirections
        GameManager.instance.transitionIndex += 1;//go to the next transition time
        Debug.Log("we incremente the transition index by caling transition to attack" + GameManager.instance);
        //note to future self: put a check before u do this so transition index doesnt go out of bounds
        if (GameManager.instance.transitionIndex < GameManager.instance.transitionTimes.Length)
        {
            GameManager.instance.currentTransitionTime = GameManager.instance.transitionTimes[GameManager.instance.transitionIndex];//set that next transition time as the current transition time

        }
        
    }
    public void TransitionToDefend()
    {
        //UI transition pop up
        StartCoroutine(UIManager.ShowPopUp(defendImage, 3f));
        //UI transition background
        UIManager.ShowNewBackground(defendBackground, attackBackground);

        onAttackPhase = false;
        ///switch note tap and note spawn values
        SongManager.Instance.noteSpawnY = SongManager.Instance.defendNoteSpawnY;
        SongManager.Instance.noteTapY = SongManager.Instance.defendNoteTapY;
        
        /// change the y position of the buttons to the new note tap value 
        GameManager.instance.tapButtons.transform.position = new Vector3(GameManager.instance.tapButtons.transform.position.x, SongManager.Instance.noteTapY, GameManager.instance.tapButtons.transform.position.z);
        //GameManager.instance.onAttackPhase = false; //this will trigger the change in note direction
        GameManager.instance.transitionIndex += 1; //go to the next transition time
        //this makes sure we dont go out of bounds
        if (GameManager.instance.transitionIndex < GameManager.instance.transitionTimes.Length)
        {
            GameManager.instance.currentTransitionTime = GameManager.instance.transitionTimes[GameManager.instance.transitionIndex];//set that next transition time as the current transition time

        }


        
    }
}