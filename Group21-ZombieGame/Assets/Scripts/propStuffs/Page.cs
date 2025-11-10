using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Page : MonoBehaviour, IInteractable
{
    PlayerController player;
    public Canvas pageCanvas;
    public Sprite pageContent;
    public string pageTitle;
    public int pageNum;
    //public Renderer rend;
    //Renderer OriginalRend=new Renderer();
    //bool isGlowing = false;
    void Awake()
    {
        player = FindFirstObjectByType<PlayerController>();
        /*
        OriginalRend = rend;
        
        pageCanvas.gameObject.SetActive(false);
        OriginalRend = rend;
        Material[] ogMats = new Material[rend.materials.Length + 1];
        rend.materials.CopyTo(ogMats, 0);
        OriginalRend.materials = gameObject.AddComponent<Renderer>().materials;
*/
    }
    public void Interact()
    {
        player.PauseGame();
        player.HideHUD();
        PageViewEnter();
    }
    void PageViewEnter()
    {   player.isViewingPage = true;
        pageCanvas.gameObject.SetActive(true);
        pageCanvas.GetComponentInChildren<Image>().sprite = pageContent;
        
    }
    public void OnPageViewExit()
    {   player.isViewingPage = false;
        pageCanvas.gameObject.SetActive(false);
        GameObject.FindAnyObjectByType<JournalScript>().SetUnlocked(pageNum);
        player.ResumeGame();
        player.ShowHUD();
        gameObject.SetActive(false);
        
        // add logic to add page to inventory
    }
    public string InteractionPrompt()
    {
        return "Press E to read the page: " + pageTitle;
    }
    public void MakeGlow(Material glowMaterial)
    {
       /* if (isGlowing) return;

        Material[] newMats = new Material[rend.materials.Length + 1];
        rend.materials.CopyTo(newMats, 0);
        newMats[newMats.Length - 1] = glowMaterial;
        rend.materials = newMats;
        isGlowing = true;*/
    }
    public void StopGlow()
    {
       /* if (!isGlowing) return;
        rend.materials = OriginalRend.materials;
        isGlowing = false;*/
    }
    
}
