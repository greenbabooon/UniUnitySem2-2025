using UnityEngine;

public class Door : MonoBehaviour,IInteractable
{
    public void Interact()
    {
        // Logic for opening the doors
    }
    public string InteractionPrompt()
    {
        return "Press E to open the door";
    }
}
