using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class PauseButton : MonoBehaviour
{

    public KeyCode keyToPress;
    public UIManager Manager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            Manager.counter++;
            if (Manager.counter % 2 == 0)
            {
                Manager.isPaused = false;
                Manager.OnGamePause(Manager.isPaused);
                Manager.gameplayEventSystem.SetActive(true);
                Manager.pauseUI.SetActive(false);
                Manager.pauseEventSystem.SetActive(false);
            }
            else
            {
                Manager.isPaused = true;
                Manager.OnGamePause(Manager.isPaused);
                Manager.gameplayEventSystem.SetActive(false);
                Manager.pauseUI.SetActive(true);
                Manager.pauseEventSystem.SetActive(true);
            }

        }
    }
}
