using UnityEngine;
using UnityEngine.SceneManagement;
public class nextleveldoor : MonoBehaviour, IInteractable
{
    //public Material highlightMat;
   // Material defaultMat;
    void Awake()
    {
       // defaultMat = GetComponent<Renderer>().material;
    }
    public void Interact()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public string InteractionPrompt()
    {
        return "press E to go to the next level";
    }
    
}
