using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

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
    public GameObject menuMusic;
    public bool isMainMenu = false;
    bool hasSpawned = false;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        hasSpawned = false;
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            isMainMenu = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            player.SetActive(false);
        }
        else
        {
            
            player.transform.position = GameObject.Find("spawnPoint").transform.position+Vector3.up*2;
            isMainMenu = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            player.SetActive(true);
            player.GetComponentInChildren<Inventory>().InitializeInv();
        }    
    }
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
            if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            isMainMenu = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            player.SetActive(false);
        }
        else
        {
            isMainMenu = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            player.SetActive(true);
            
        }
        if (gameManager == null)
        {
            gameManager = this;
           // gameManager.startPnt = transform.position;
            //gameManager.gameObject.transform.position = transform.position;
            DontDestroyOnLoad(gameObject);  
        }
        else if (gameManager != this)
        {
           
            Destroy(gameObject);
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
            optionsMenu.GetComponent<OptionsMenu>().OpenOptions();
            Debug.Log("Open Options");
        }
    }

    public void CloseOptionsMenu()
    {
        if (optionsMenu.GetComponent<OptionsMenu>() != null)
        {
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
    public void MainMenu()
    {
        GameManager.gameManager.player.transform.position = GameObject.Find("spawnPoint").transform.position+Vector3.up*2;
        SceneManager.LoadScene("Main Menu");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level-1-Prom-Hall");
    }

}
