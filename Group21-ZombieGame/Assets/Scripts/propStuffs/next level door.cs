using UnityEngine;
using UnityEngine.SceneManagement;
public class nextleveldoor : MonoBehaviour, IInteractable
{

    //public Renderer rend1;
    // public Renderer rend2;
    // Renderer OriginalRend1=new Renderer();
    // Renderer OriginalRend2=new Renderer();
   //  bool isGlowing = false;
    void Awake()
    {
       /* OriginalRend1 = rend1;
        OriginalRend2 = rend2;
        Material[] ogMats = new Material[rend1.materials.Length + 1];
        rend1.materials.CopyTo(ogMats, 0);
        OriginalRend1.materials = ogMats;
        OriginalRend1.materials = gameObject.AddComponent<Renderer>().materials;
        if (rend2 != null)
        {
            Material[] ogMats2 = new Material[rend2.materials.Length + 1];
            rend2.materials.CopyTo(ogMats2, 0);
            OriginalRend2.materials = ogMats2;
            OriginalRend2.materials = gameObject.AddComponent<Renderer>().materials;
        }
        */
     }
    public void Interact()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public string InteractionPrompt()
    {
        return "press E to go to the next level";
    }
    public void MakeGlow(Material glowMaterial)
    {
      /*  if (isGlowing) return;
        Material[] newMats1 = new Material[rend1.materials.Length + 1];
        rend1.materials.CopyTo(newMats1, 0);
        newMats1[newMats1.Length - 1] = glowMaterial;
        rend1.materials = newMats1;
        if (rend2 != null)
        {
            Material[] newMats2 = new Material[rend2.materials.Length + 1];
            rend2.materials.CopyTo(newMats2, 0);
            newMats2[newMats2.Length - 1] = glowMaterial;
            rend2.materials = newMats2;
        }
        isGlowing = true;
        */
    }
    public void StopGlow()
    {/*
        if (!isGlowing) return;
        rend1.materials = OriginalRend1.materials;
        rend2.materials = OriginalRend2.materials;
        isGlowing = false;*/
    }
    
}
