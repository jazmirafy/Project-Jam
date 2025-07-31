using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class playerController : MonoBehaviour
{

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //left
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.JoystickButton10))
            animator.SetTrigger("leftHeld");



        // right
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.JoystickButton8))
            animator.SetTrigger("rightHeld");


        //up
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.JoystickButton9))
            animator.SetTrigger("upHeld");


        // down
        if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.JoystickButton11))
            animator.SetTrigger("downHeld");




    }
    //miss trigger
    public void missAnimation()
    {
        animator.SetTrigger("miss");
    }
    public void IdleAnimation()
    {
        animator.SetTrigger("idle");
    }
}



