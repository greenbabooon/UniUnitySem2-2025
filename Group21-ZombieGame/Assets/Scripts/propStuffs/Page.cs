using UnityEngine;
using UnityEngine.UI;

public class Page : MonoBehaviour, IInteractable
{
    PlayerController player;
    public Canvas pageCanvas;
    public Sprite pageContent;
    public string pageTitle;
    void Awake()
    {
        player = FindFirstObjectByType<PlayerController>();
    }
    public void Interact()
    {
        player.PauseGame();
        player.HideHUD();
        PageViewEnter();
    }
    void PageViewEnter()
    {
        pageCanvas.gameObject.SetActive(true);
        pageCanvas.GetComponentInChildren<Image>().sprite = pageContent;
    }
    public void OnPageViewExit()
    {
        pageCanvas.gameObject.SetActive(false);
        player.ResumeGame();
        player.ShowHUD();
        // add logic to add page to inventory
    }
    public string InteractionPrompt()
    {
        return "Press E to read the page: " + pageTitle;
    }
    
}
