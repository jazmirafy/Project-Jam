using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Melanchall.DryWetMidi.Interaction;

public class RobotController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerNoteAnimation(Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction)
    {
        //basically this says if its in the left most lane make the robot do the left animation, so on and so forth
        //since each lane is mapped to a piano note i just do the appropriate animation based on what lane the note came from
        switch (noteRestriction)
        {
            //heal note lane
            case Melanchall.DryWetMidi.MusicTheory.NoteName.FSharp:
                animator.SetTrigger("leftHeld");
                break;
            //left most lane
            case Melanchall.DryWetMidi.MusicTheory.NoteName.F:
                animator.SetTrigger("leftHeld");
                break;
            //inner left lane
            case Melanchall.DryWetMidi.MusicTheory.NoteName.G:
                animator.SetTrigger("downHeld");
                break;
            //inner right lane
            case Melanchall.DryWetMidi.MusicTheory.NoteName.A:
                animator.SetTrigger("upHeld");
                break;
            //rightmost lane
            case Melanchall.DryWetMidi.MusicTheory.NoteName.B:
                animator.SetTrigger("rightHeld");
                break;
            //damage note lane
            case Melanchall.DryWetMidi.MusicTheory.NoteName.ASharp:
                animator.SetTrigger("rightHeld");
                break;

        }
    }
    public void IdleAnimation()
    {
        animator.SetTrigger("idle");
    }
}
