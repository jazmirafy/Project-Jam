using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class UIManager : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject startScreenUI;
    public GameObject levelSelectUI;
    public KeyCode input;
    int counter = 0;

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(input))
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

}
