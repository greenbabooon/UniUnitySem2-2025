using UnityEngine;

public class BoomBoxScript : MonoBehaviour, IInteractable
{
    public bool used = false;
    string prompt = "Press E to get ammo for cassette gun";
    public int ammoType;
    public int ammoAmount;
    Inventory inventory;
    PlayerController playerController;
    void Start()
    {
        playerController = GameObject.FindAnyObjectByType<PlayerController>();
        inventory = playerController.GetComponent<Inventory>();
    }

    public void Interact()
    {
        if (used == false)
        {
            inventory.SetAmmoCount(ammoType, ammoAmount + inventory.GetAmmoCount(ammoType));
            playerController.UpdateAmmoUI();
            prompt = "no more ammo here";
            used = true;
        }
        else
        {
         //play nothing happening sfx   
        }
    }
    public string InteractionPrompt()
    {
        return prompt;
    }
    
}
