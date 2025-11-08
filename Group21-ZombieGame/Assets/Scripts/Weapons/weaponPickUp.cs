using UnityEngine;

public class weaponPickUp : MonoBehaviour, IInteractable
{
    public Weapon weapon; // Reference to the weapon scriptable object
    GameObject player;
    void Awake()
    {
        player = GameObject.FindFirstObjectByType<PlayerController>().gameObject.gameObject;

    }
    public void Interact()
    {
        Inventory inv = player.GetComponent<Inventory>();
        inv.addItem(weapon);
        gameObject.SetActive(false);
    }
    public string InteractionPrompt()
    {

        return "Press E to pick up " + weapon.weaponName;
    }
    public void MakeGlow(Material glowMat)
    {/*
        if (isGlowing) return;
        Material[] newMats = new Material[rend.materials.Length + 1];
        rend.materials.CopyTo(newMats, 0);
        newMats[newMats.Length - 1] = glowMat;
        rend.materials = newMats;
        isGlowing = true;*/
    }
    public void StopGlow()
    {
      /*  if (!isGlowing) return;
        rend.materials = OriginalRend.materials;
        isGlowing = false;*/
    }

}
