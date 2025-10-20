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
    public PlayerPersistentData playerData;
    public bool isPaused = false;
    public GameObject menuMusic;
    public bool isMainMenu = false;
    public bool isReload=false;
   

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            isMainMenu = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
           
        }
        else
        {
            isMainMenu = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
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
        }
        else
        {
            isMainMenu = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;           
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
        SceneManager.LoadScene("Main Menu");
    }
    public void StartGame()
    {
        print("trying to load scene index:" + playerData.lastLevel);
        SceneManager.LoadScene(playerData.lastLevel);
    }
    public void NextLevel()
    {
        print("test");
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName==playerData.lastLevel)
        {
            SceneManager.LoadScene(currentIndex+1);
        }
        else
        {
            SavePlayerData();
          SceneManager.LoadScene(currentIndex+1);  
        }
        
    }
    public void LoadPlayerdata()
    {
        Inventory temp = GameObject.FindFirstObjectByType<PlayerController>().GetComponent<Inventory>();
        temp = playerData.inv;
    }
    public void SavePlayerData()
    {
        Inventory temp = GameObject.FindFirstObjectByType<PlayerController>().GetComponent<Inventory>();
        playerData.inv = temp;   
    }

}
