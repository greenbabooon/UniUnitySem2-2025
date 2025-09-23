using Unity.VisualScripting;
using UnityEngine;

public class Carry : MonoBehaviour, IInteractable
{
   // public Renderer rend;
   // Renderer OriginalRend=new Renderer();
    //bool isGlowing = false;

    public void Interact()
    {
       /* OriginalRend = rend;
        Material[] ogMats = new Material[rend.materials.Length + 1];
        rend.materials.CopyTo(ogMats, 0);
        OriginalRend.materials = gameObject.AddComponent<Renderer>().materials;*/

    }
    public string InteractionPrompt()
    {
        return "Press E to pick up " + gameObject.name;
    }
    public void MakeGlow(Material glowMat)
    {
      /*  if (isGlowing) return;

        Material[] newMats = new Material[rend.materials.Length + 1];
        rend.materials.CopyTo(newMats, 0);
        newMats[newMats.Length - 1] = glowMat;
        rend.materials = newMats;*/
    }
    public void StopGlow()
    {/*
        
        if (!isGlowing) return;
        rend.materials = OriginalRend.materials;
        isGlowing = false;*/
    }
}
