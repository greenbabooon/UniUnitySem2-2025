using UnityEngine;

public class Carry : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Interact()
    {
        // Logic for picking up the object
    }
    public string InteractionPrompt()
    {
        return "Press E to pick up " + gameObject.name;
    }
}
