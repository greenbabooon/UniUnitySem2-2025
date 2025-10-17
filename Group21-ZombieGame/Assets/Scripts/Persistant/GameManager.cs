using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public int currentLevel = 0;
    public int maxLevels = 3;
    public Dictionary<int, GameObject> Pages = new Dictionary<int, GameObject>();
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject player;
    public bool isPaused = false;
    public AudioClip menuMusic;
    public bool isMainMenu = false;
    


    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(pauseMenu);
            DontDestroyOnLoad(optionsMenu);
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            isMainMenu = true;
        }
        else
        {
            isMainMenu = false;
        }
    }
    public void OpenPauseMenu()
    {
        if (pauseMenu.GetComponent<PauseMenu>() != null)
        {
            isPaused = true;
            pauseMenu.GetComponent<PauseMenu>().PauseGame();
        }
        else
        {
            Debug.LogError("PauseMenu component not found on pauseMenu GameObject.");
        }

    }
    public void ClosePauseMenu()
    {
        if (pauseMenu.GetComponent<PauseMenu>() != null)
        {
            isPaused = false;
            pauseMenu.GetComponent<PauseMenu>().ResumeGame();
        }
        else
        {
            Debug.LogError("PauseMenu component not found on pauseMenu GameObject.");
        }
    }


        public void OpenOptionsMenu()
    {
        if (optionsMenu.GetComponent<OptionsMenu>() != null)
        {
            isPaused = true;
            optionsMenu.GetComponent<OptionsMenu>().OpenOptions();
            Debug.Log("Open Options");
        }
    }

    public void CloseOptionsMenu()
    {
        if (optionsMenu.GetComponent<OptionsMenu>() != null)
        {
            isPaused = false;
            optionsMenu.GetComponent<OptionsMenu>().CloseOptions();
            Debug.Log("close test");
        }
    }
    public void Pause()
    {
     if (!isPaused)
        {
            OpenPauseMenu();
        }
        else
        {
            ClosePauseMenu();
        }   
    }
}
