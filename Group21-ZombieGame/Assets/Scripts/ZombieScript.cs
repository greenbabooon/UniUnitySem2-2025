using System.Diagnostics;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public HealthScript health;
    public Canvas healthCanvas;
    public UnityEngine.UI.Image[] healthIcons;
    private Transform canvasRotation;
    public void dmgUpdate()
    {
        print("Health: " + health.currentHealth + " / " + health.maxHealth);
        if (healthCanvas.enabled == false)
        {
            healthCanvas.enabled = true;
        }
        for (int i = 0; i < healthIcons.Length; i++)
        {
            if (i < health.currentHealth / (health.maxHealth / (healthIcons.Length )))
            {
                healthIcons[i].enabled = true;
            }
            else
            {
                healthIcons[i].enabled = false;
            }

        }
        if (health.currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        //play death animation and drop loot
        Destroy(gameObject);
    }
    void Awake()
    {
        healthCanvas.enabled = false;
        canvasRotation = healthCanvas.GetComponent<Transform>();
    }
    void Update()
    {
        if (healthCanvas.enabled == true)
        {
            canvasRotation.LookAt(Camera.main.transform.position);
        }
    }
    
}
