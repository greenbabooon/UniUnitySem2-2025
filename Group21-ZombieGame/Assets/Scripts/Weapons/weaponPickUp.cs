using UnityEngine;

public class weaponPickUp : MonoBehaviour, IInteractable
{
   // public Material highlightMat;
   // Material defaultMat;
    bool beingLookedAt = false;
    void Awake()
    {
        //defaultMat = GetComponent<Renderer>().material;
    }
    public Weapon weapon; // Reference to the weapon scriptable object
    public void Interact()
    {
        Inventory inv = FindFirstObjectByType<Inventory>();
        inv.addItem(weapon);
        Destroy(gameObject);
    }
    public string InteractionPrompt()
    {
        
        return "Press E to pick up " + weapon.weaponName;
    }

}
