using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas optionsCanvas;

    private void Awake()
    {
        mainMenuCanvas = GameObject.Find("MainMenuCanvas").GetComponent<Canvas>();
        Time.timeScale = 1;
        DontDestroyOnLoad(optionsCanvas.gameObject);
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (mainMenuCanvas == null)
            {
                mainMenuCanvas = GameObject.Find("MainMenuCanvas").GetComponent<Canvas>();
            }
        }
    }
        


    public void ToggleOptions()
    {
        optionsCanvas = GameObject.Find("OptionsCanvas").GetComponent<Canvas>();
        mainMenuCanvas.enabled = !mainMenuCanvas.enabled;
        optionsCanvas.enabled = !optionsCanvas.enabled;
        Debug.Log("Options");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Play");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

}
