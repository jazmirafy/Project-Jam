using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo; //how fast the notes move down
    
                            // Start is called before the first frame update
    
    
    public GameObject attackNoteHolder, defendNoteHolder, attackButtons, defendButtons;
    void Start()
    {
        beatTempo = beatTempo / 60f; //how fast the beat scrolls should move per second
        GameManager.instance.currentTransitionTime = GameManager.instance.transitionTimes[GameManager.instance.transitionIndex];
    }

    // Update is called once per frame
    void Update()
    {
        //basically saying if the beat hasnt started yet, and the if the user clicked any button, we know the song started/scrolling starts now
        if (!GameManager.instance.hasStarted)
        {
            /*if (Input.anyKeyDown)
            {
                GameManager.instance.currentTransitionTime) = true;
            }*/
        }
        /*if the song has started, start moving the notes down
        keep x and z zero bc we only want to affect the vertical movement(y)
        note to self: when you make the defend phase where beats have to move upward instead of
        downward, take this same line but put += instead of -= */
        else if (GameManager.instance.onAttackPhase) //during attack
        {
            //basically saying every frame move the note object down based on how fast the beat is and  scale it by time so it moves down consistently (so it moves per second instead of per frame)
            transform.position += new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
        else if (!GameManager.instance.onAttackPhase) //during defend
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f); //changes notes to go up at a consistent rate instead of down
        }
        if (GameManager.instance.transitionIndex < GameManager.instance.transitionTimes.Length && GameManager.instance.elapsedMusicTime >= GameManager.instance.currentTransitionTime) //if the variable we are using to track the elapsed time in the song = GameManager.instance.currentTransitionTime
        //i put >= to give the float a little leeway since it may not catch exactly on the time's decimal places
        //also check the transition index and the transition time array length so stuff doesnt go out of bound
        {
            if (GameManager.instance.transitionIndex % 2 == 0) //if the transition index is even
            {
                TransitionToDefend();
            }
            else
            {
                TransitionToAttack();
            }
        }

    }
    public void TransitionToAttack()
    {
        //deactivate defend notes + buttons
        defendNoteHolder.SetActive(false);
        defendButtons.SetActive(false);
        //activate attack notes + buttons
        attackNoteHolder.SetActive(true);
        attackButtons.SetActive(true);
        GameManager.instance.onAttackPhase = true; // this will trigger the change in note drirections
        GameManager.instance.transitionIndex += 1;//go to the next transition time
        //note to future self: put a check before u do this so transition index doesnt go out of bounds
        if (GameManager.instance.transitionIndex < GameManager.instance.transitionTimes.Length)
        {
            GameManager.instance.currentTransitionTime = GameManager.instance.transitionTimes[GameManager.instance.transitionIndex];//set that next transition time as the current transition time

        }
        
    }
    public void TransitionToDefend()
    {
        attackNoteHolder.SetActive(false);//deactivate attack notes + buttons
        attackButtons.SetActive(false);
        //activate defend notes + buttons
        defendNoteHolder.SetActive(true);
        defendButtons.SetActive(true);
        GameManager.instance.onAttackPhase = false; //this will trigger the change in note direction
        GameManager.instance.transitionIndex += 1; //go to the next transition time
        //this makes sure we dont go out of bounds
        if (GameManager.instance.transitionIndex < GameManager.instance.transitionTimes.Length)
        {
            GameManager.instance.currentTransitionTime = GameManager.instance.transitionTimes[GameManager.instance.transitionIndex];//set that next transition time as the current transition time

        }


        
    }
    
}
