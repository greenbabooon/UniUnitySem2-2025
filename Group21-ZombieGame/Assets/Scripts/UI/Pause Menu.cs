using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseMenu;
    public bool isPaused;

    private void Awake()
    {
        pauseMenu.enabled = false;
    }

    void Start()
    {
        pauseMenu.enabled = false;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (isPaused && context.performed)
        {
            ResumeGame();
        }
        else if (! isPaused && context.performed)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.enabled = true;
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        pauseMenu.enabled = false;
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState= CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
