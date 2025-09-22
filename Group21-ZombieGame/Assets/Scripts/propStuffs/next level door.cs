using UnityEngine;
using UnityEngine.SceneManagement;
public class nextleveldoor : MonoBehaviour, IInteractable
{   
    public void Interact()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public string InteractionPrompt()
    {
       return "press E to go to the next level";
    }
}
