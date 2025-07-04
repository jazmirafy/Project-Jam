using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    private bool canBePressed;
    public KeyCode keyToPress;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    private bool hasBeenHit;
    //note to future self but when u make the damage notes check first if the note type is damage not and if so
    //if a damage note has been hit call hitDamageNote() (we are gonna make that its gonna be like missed note but deal more damage)
    //same for if we hit a note that replenishes u(healing notes) check the note type first and call hitHealNote which is gonna be like note hit but more replenishing
    //basically asking have i entered our button area and if i have is the correct button being pressed
    //basically checking if the note entered the button area
    //if it has we are basically saying okay you can press the button now
    //if the button is pressed in time we remove the note (object disappears if u press on time)
    //if not we basically say of you cant press it anymore
    void Update()
    {
        //if the bind corresponding to the note was pressed during the time the note and button thingy are overlapped, make the note disappear
        //this is nice bc the notes that dont disappear are an indicator to the player they missed the note
        //also tell the game manager we hit or missed the note

        if (Input.GetKeyDown(keyToPress) && canBePressed)
        {
            hasBeenHit = true;
            //note for future self if i make an aspect of the game where i reuse note imma have to make sure i reset this back to false
            gameObject.SetActive(false);
            //GameManager.instance.NoteHit();
            //measure how closely aligned the center of the colliders of the note and button are to see how accurate the hit is
            //take the absolute value in case the player hits late and the note hits a little more below center
            if (Mathf.Abs(transform.position.y) > 0.25)
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
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && !hasBeenHit)
        {
            canBePressed = false;
            GameManager.instance.NoteMissed();
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            //if the player hits the note on time the game object gets deactivated
            //therefore if the note has a chance to exit the trigger zone, that means it was a missed note (hypothetically but imma add a boolean flag justin case)
        }
    }
}
