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
        inventory = gameObject.GetComponentInParent<Inventory>();
        playerController = gameObject.GetComponentInParent<PlayerController>();
    }

    public void Interact()
    {
        if (used == false)
        {
            inventory.SetAmmoCount(ammoType, ammoAmount + inventory.GetAmmoCount(ammoType));
            prompt = "no more ammo here";
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
