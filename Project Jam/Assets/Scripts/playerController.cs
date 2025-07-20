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
        // ---- LEFT ----
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            animator.SetTrigger("leftHeld");
    
     

        // ---- RIGHT ----
        if (Input.GetKeyDown(KeyCode.RightArrow))
            animator.SetTrigger("rightHeld");


        // ---- UP ----
        if (Input.GetKeyDown(KeyCode.UpArrow))
            animator.SetTrigger("upHeld");
    

        // ---- DOWN ----
        if (Input.GetKeyDown(KeyCode.DownArrow))
            animator.SetTrigger("downHeld");

        // miss

        
    }
    public void missAnimation()
    {
        animator.SetTrigger("miss");
    }
}



