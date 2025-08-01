using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.Windows;

public class UIManager : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject startScreenUI;
    public GameObject levelSelectUI;

    public GameObject startHoverImage;
    public GameObject quitHoverImage;

    

    //public List<GameObject> popUpList;
    //private GameObject currentPopUp;
    //private float timer = 0;
    public float popUpTime;
    //private int i = 0;


    public KeyCode inputForPauseButton;
    int counter = 0;

    public bool isPaused = false;



    // BUTTONS!!


    //pause button UI
    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(inputForPauseButton))
        {
            counter++;
            if (counter % 2 == 0)
            {
                isPaused = false;
                OnGamePause(isPaused);
                pauseUI.SetActive(false);
            }
            else
            {
                isPaused = true;
                OnGamePause(isPaused);
                pauseUI.SetActive(true);
            }
        }
    }

    //pausing the actual game
    public void OnGamePause(bool pauseStatus)
    {
        if (pauseStatus) // if app is paused
        {
            PauseGame();
        }
        else // App resumes
        {
            ResumeGame();
        }
    }

    //Main Menu Buttons
    public void OnRestartPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnGameLevelSelectPress()
    {
        
    }

    public void OnGameQuitPress()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnGameStartPress()
    {
        startScreenUI.SetActive(false);
        levelSelectUI.SetActive(true);
    }

    public void OnLevelSelectBackPress()
    {
        startScreenUI.SetActive(true);
        levelSelectUI.SetActive(false);
    }

    public void BringtoHipHop()
    {
        SceneManager.LoadScene("HipHopLevelScene");
    }
    public void BringtoJazz()
    {
        SceneManager.LoadScene("JazzLevelScene");
    }

    public void BringtoPunk()
    {
        SceneManager.LoadScene("PunkLevelScene");
    }



    //image toggles in the main menu (when hovering over a button)

    public void startHover()
    {
        startHoverImage.SetActive(true);
    }

    public void quitHover()
    {
        quitHoverImage.SetActive(true);
    }

    public void stopStartHover()
    {
        startHoverImage.SetActive(false);
    }

    public void stopQuitHover()
    {
        quitHoverImage.SetActive(false);
    }



    //pop up script draft
    //public void popupFunc()
    //{
    //    currentPopUp = popUpList[i % popUpList.Count];
    //    currentPopUp.SetActive(true);

    //    if (timer < popUpTime)
    //    {
    //        timer += Time.deltaTime;
    //    }
    //    else
    //    {
    //        currentPopUp.SetActive(false);
    //        timer = 0;
    //        i++;
    //    }
    //}



    //pop up any image method
    public IEnumerator ShowPopUp(GameObject popUpImage, float popUpLength)
    {
        popUpImage.gameObject.SetActive(true); //enables the pop up
        Debug.Log("POP UP SHOWN");
        yield return new WaitForSeconds(popUpLength);
        popUpImage.gameObject.SetActive(false); //disables the pop up 
        Debug.Log("POP UP DEACTIVATED");
    }


    //Switch current background
    public void ShowNewBackground(GameObject firstBackground, GameObject secondBackground)
    {
        if (!firstBackground.activeSelf && secondBackground.activeSelf) {
            firstBackground.gameObject.SetActive(true); //enables the first background
            secondBackground.gameObject.SetActive(false); //disables the second background
        }
    }



    //pausing and resuming game

    void PauseGame()
    {
        Time.timeScale = 0f; // stop game time
        AudioListener.pause = true; // stop audio
        isPaused = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // continue game time
        AudioListener.pause = false; // resume audio
        isPaused = false;
    }
}
