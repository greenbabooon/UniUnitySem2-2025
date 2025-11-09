using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void restart()
    {
        GameManager.gameManager.PlayLastLevel();
    }
    public void Quit()
    {
        GameManager.gameManager.MainMenu();
    }
}
