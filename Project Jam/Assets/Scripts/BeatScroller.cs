using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo; //how fast the notes move down
    public bool hasStarted; //checking if the player clicked a button to start the song(the beat scrolling)
    // Start is called before the first frame update
    void Start()
    {
        beatTempo = beatTempo / 60f; //how fast the beat scrolls should move per second
    }

    // Update is called once per frame
    void Update()
    {
        //basically saying if the beat hasnt started yet, and the if the user clicked any button, we know the song started/scrolling starts now
        if (!hasStarted)
        {
            /*if (Input.anyKeyDown)
            {
                hasStarted = true;
            }*/
        }
        /*if the song has started, start moving the notes down
        keep x and z zero bc we only want to affect the vertical movement(y)
        note to self: when you make the defend phase where beats have to move upward instead of
        downward, take this same line but put += instead of -= */
        else
        {
            //basically saying every frame move the note object down based on how fast the beat is and  scale it by time so it moves down consistently (so it moves per second instead of per frame)
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);         }
    }
}
