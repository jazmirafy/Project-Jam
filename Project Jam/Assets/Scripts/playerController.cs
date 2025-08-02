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
                for (int i = 0; i < 20; i++) {
            if (Input.GetKeyDown("joystick button " + i)) {
                Debug.Log("Button " + i + " was pressed");
            }
        }
        //left
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.JoystickButton6) )
            animator.SetTrigger("leftHeld");



        // right
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.JoystickButton5) )
            animator.SetTrigger("rightHeld");


        //up
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.JoystickButton4))
            animator.SetTrigger("upHeld");


        // down
        if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.JoystickButton7))
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



