using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class UIManager : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject startScreenUI;
    public GameObject levelSelectUI;

    public GameObject startHoverImage;
    public GameObject quitHoverImage;

    public List <GameObject> popUpList;
    private GameObject currentPopUp;
    private float timer = 0;
    public float popUpTime;
    private int i = 0;

    public KeyCode inputForPauseButton;
    int counter = 0;



    // BUTTONS!!


    //pause button
    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(inputForPauseButton))
        {
            counter++;
            if (counter % 2 == 0)
            {
                pauseUI.SetActive(false);
            }
            else
            {
                pauseUI.SetActive(true);
            }
        }
    }

    //Main Menu Buttons
    public void OnRestartPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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



    //pop up script
    public void popupFunc()
    {
        currentPopUp = popUpList[i % popUpList.Count];
        currentPopUp.SetActive(true);

        if (timer < popUpTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            currentPopUp.SetActive(false);
            timer = 0;
            i++;
        }
    }
}
