using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f; //put time back to normal speed
        SoundManager.PlayBackgroundMusic(SoundManager.instance.mainMenuMusic, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
