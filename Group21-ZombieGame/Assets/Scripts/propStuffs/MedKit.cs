using UnityEngine;

public class MedKit : MonoBehaviour,IInteractable
{
    public bool used = false;
    string prompt = "Press E to to Heal";
    public int healAmount = 25;
    

    public void Interact()
    {
        if (used == false)
        {
            var temp = FindAnyObjectByType<PlayerController>();
            temp.healthScript.currentHealth += healAmount;
            if (temp.healthScript.currentHealth > 100) temp.healthScript.currentHealth = 100f;
            temp.UpdateHealthUI();
            prompt = "This Med Kit is used";
            used = true;
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
