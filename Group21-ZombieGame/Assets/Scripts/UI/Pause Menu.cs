using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private Canvas pauseMenu;
    public bool isPaused;

    private void Awake()
    {
        pauseMenu =gameObject.GetComponentInParent<Canvas>();
        pauseMenu.enabled = false;
    }

    void Start()
    {
        pauseMenu.enabled = false;
    }

    public void PauseGame()
    {
        pauseMenu.enabled = true;
        Time.timeScale = 0f;
        isPaused = true;
        if(GameManager.gameManager != null && GameManager.gameManager.isMainMenu)
        {
            return;
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        pauseMenu.enabled = false;
        Time.timeScale = 1f;
        isPaused = false;
        if(GameManager.gameManager != null && GameManager.gameManager.isMainMenu)
        {
            return;
        }
        Cursor.visible = false;
        Cursor.lockState= CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
