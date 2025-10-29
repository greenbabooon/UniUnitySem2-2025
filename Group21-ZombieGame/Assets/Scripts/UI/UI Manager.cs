using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject mainMenu;
    public float delay = 9f;

    private void Start()
    {
        bool hasSeenTitle = PlayerPrefs.GetInt("HasSeenTitle", 0) == 1;
        if (hasSeenTitle)
        {
            titleScreen.SetActive(false);
            mainMenu.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("HasSeenTitle", 1);
            PlayerPrefs.Save();
            titleScreen.SetActive(true);
            mainMenu.SetActive(false);
            StartCoroutine(SwitchToMainMenu());
        }
    }

    private IEnumerator SwitchToMainMenu()
    {
        yield return new WaitForSeconds(delay);
        titleScreen.SetActive(false);
        mainMenu.SetActive(true);
    }
}