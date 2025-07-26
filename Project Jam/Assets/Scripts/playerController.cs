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
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            animator.SetTrigger("leftHeld");
    
     

        // right
        if (Input.GetKeyDown(KeyCode.RightArrow))
            animator.SetTrigger("rightHeld");


        //up
        if (Input.GetKeyDown(KeyCode.UpArrow))
            animator.SetTrigger("upHeld");
    

        // down
        if (Input.GetKeyDown(KeyCode.DownArrow))
            animator.SetTrigger("downHeld");

        

        
    }
    //miss trigger
    public void missAnimation()
    {
        animator.SetTrigger("miss");
    }
}



