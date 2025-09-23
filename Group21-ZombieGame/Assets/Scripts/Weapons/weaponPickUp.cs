using UnityEngine;

public class weaponPickUp : MonoBehaviour, IInteractable
{
    //bool beingLookedAt = false;
    //public Renderer rend;
   // Renderer OriginalRend;
    public Weapon weapon; // Reference to the weapon scriptable object
    //bool isGlowing = false;
    void Awake()
    {
      /*  if (rend == null) print("no rend assigned to " + gameObject.name);
        OriginalRend = rend;
        Material[] ogMats = new Material[rend.materials.Length + 1];
        rend.materials.CopyTo(ogMats, 0);
        OriginalRend.materials = gameObject.AddComponent<Renderer>().materials;*/

    }
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
