using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas optionsMenu;
    private bool paused;

    public static GameManager instance;
    public GameObject player;
    public int extraJumpsForPlayer;

    public bool deathAllowed;

    public TMP_Text introText;
    public TMP_Text outroText;

    private bool levelOver;

    public bool canSkip;

    // Start is called before the first frame update
    void Awake()
    {
        optionsMenu = GameObject.Find("PauseMenu").GetComponent<Canvas>();
        player = GameObject.Find("Player");
        instance = this;
        player.GetComponent<PlayerController>().enabled = false;
        Invoke("WakePlayer", 5f);
        introText.GetComponent<TextFade>().StartFades();
    }
    private void Start()
    {
        canSkip = GameObject.Find("MusicPlayer").GetComponent<MusicController>().allowLevelSkip;
    }

    void WakePlayer()
    {
        player.GetComponent<PlayerController>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        canSkip = GameObject.Find("MusicPlayer").GetComponent<MusicController>().allowLevelSkip;
        Debug.Log("canSkip returns: " + canSkip.ToString());
        if(player.GetComponent<PlayerController>().isGoal && !levelOver)
        {
            outroText.GetComponent<TextFade>().StartFades();
            StartCoroutine("LoadNextLevel");
            levelOver = true;
        }

        if (player.transform.position.y < -20)
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }

        if (Input.GetKeyDown(KeyCode.N) )
        {
            if (canSkip)
            {
                int currentScene = SceneManager.GetActiveScene().buildIndex;
                if (currentScene < SceneManager.sceneCountInBuildSettings - 1)
                {
                    SceneManager.LoadScene(currentScene + 1);
                }
            }
            
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if(canSkip)
            {
                int currentScene = SceneManager.GetActiveScene().buildIndex;
                if (currentScene < SceneManager.sceneCountInBuildSettings - 1)
                {
                    SceneManager.LoadScene(currentScene - 1);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            if (paused)
            {
                Time.timeScale = 0;
                optionsMenu.enabled = true;
            }
            else if (!paused)
            {
                Time.timeScale = 1;
                optionsMenu.enabled = false;
            }
            
        }
    }

    public IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(6f);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if(currentScene < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
        
    }
}
