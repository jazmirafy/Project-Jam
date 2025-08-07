using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    double timeInstantiated;
    public float assignedTime;
    void Start()
    {
        timeInstantiated = SongManager.GetAudioSourceTime();
    }

    // Update is called once per frame
    void Update()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2));

        
        if (t > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            // lerp moves the note from spawn to despawn smoothly over time (t from 0 to 1)
            // spawn and despawn positions are dynamically calculated based on attack/defend mode
            // vector3.up * y converts a y-value into a Vector3 in the vertical direction (0,1,0) (reminds me when we have ijk in linear algebra is the basis j vector)
            transform.localPosition = Vector3.Lerp(Vector3.up * SongManager.Instance.noteSpawnY, Vector3.up * SongManager.Instance.noteDespawnY, t); 
            GetComponent<SpriteRenderer>().enabled = true;

            //
            if (!Lane.onAttackPhase && gameObject.tag != "damageNote")
            {
                Vector3 currentScale = transform.localScale;
                transform.localScale = new Vector3(currentScale.x, -Mathf.Abs(currentScale.y), currentScale.z);
            }
        }
    }
}
