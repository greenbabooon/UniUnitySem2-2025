using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Camera mainCamera;
    public float rotationSpeed = 1f;
    private bool isRotating = false;
    public GameObject[] UIElements;
    public GameObject initialButton;

    public void StartGame()
    {
        GameManager.gameManager.StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
