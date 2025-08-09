using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.Windows;
using JetBrains.Annotations;

public class UIManager : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject startScreenUI;
    public GameObject levelSelectUI;

    public GameObject startHoverImage;
    public GameObject quitHoverImage;

    public GameObject gameOverImage;

    public GameObject tutorialImage;

    //public List<GameObject> popUpList;
    //private GameObject currentPopUp;
    //private float timer = 0;
    public float popUpTime;
    //private int i = 0;


    public KeyCode inputForPauseButton;
    int counter = 0;

    public bool isPaused = true;

    private void Start()
    {
        PauseGame();
    }


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
        ResumeGame(); // put the audio listener back on and sets time back to the normal scale
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("time scale =" + Time.timeScale);
        
    }

    public void OnGameLevelSelectPress()
    {
        bringToLevelSelect(levelSelectUI, startScreenUI, 5f);
        SceneManager.LoadScene("MenuScene");

    }

    public IEnumerator bringToLevelSelect(GameObject canvasOn, GameObject canvasOff, float waitLength)
    { 
        
        yield return new WaitForSeconds(waitLength);
        canvasOn.gameObject.SetActive(true);
        canvasOff.gameObject.SetActive(false); 
        
    }

    public void OnGameQuitPress()
    {
        ResumeGame(); // puts audio listener back on and puts time back at the normal scale
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
        if (!firstBackground.activeSelf && secondBackground.activeSelf)
        {
            firstBackground.gameObject.SetActive(true); //enables the first background
            secondBackground.gameObject.SetActive(false); //disables the second background
        }
    }

    //play the confirm button sfx
    public void PlayConfirmSFX()
    {
        SoundManager.PlaySound(SoundType.ConfirmSFX);
    }

    //play the cancel sfx
    public void PlayCancelSFX()
    {
        SoundManager.PlaySound(SoundType.CancelSFX);
    }
    //stops game when the player health = 0
    public void onGameOver()
    {
        PauseGame();
        gameOverImage.SetActive(true);
    }


    //pausing and resuming game

    public void PauseGame()
    {
        Time.timeScale = 0f; // stop game time
        AudioListener.pause = true; // stop audio
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // continue game time
        AudioListener.pause = false; // resume audio
        isPaused = false;
        tutorialImage.SetActive(false);
    }

}
