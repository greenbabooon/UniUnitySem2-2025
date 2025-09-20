using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour, IDamageable
{
    public HealthScript health;
    public Canvas healthCanvas;
    public UnityEngine.UI.Image[] healthIcons;
    private Transform canvasRotation;
    private NavMeshAgent agent;
    bool idle = true;
    bool patrolling = false;
    bool TargetInSpottingRange = false;
    bool TargetDetected = false;
    bool TargetInAttackRange = false;
    float currentCoolDownTime;
    public float cooldownTime = 1f;
    public float attackRange = 2f;
    public float spottingRange = 10f;

    public void damage(float damageAmount)
    {
        health.currentHealth -= damageAmount;
        dmgUpdate();
    }   

    public void dmgUpdate()
    {
        print("Health: " + health.currentHealth + " / " + health.maxHealth);
        if (healthCanvas.enabled == false)
        {
            healthCanvas.enabled = true;
        }
        for (int i = 0; i < healthIcons.Length; i++)
        {
            if (i < health.currentHealth / (health.maxHealth / (healthIcons.Length)))
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
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (healthCanvas.enabled == true)
        {
            canvasRotation.LookAt(Camera.main.transform.position);

        }
        if (TargetInSpottingRange)
        {

        }
        agent.SetDestination(GameObject.FindFirstObjectByType<PlayerController>().transform.position);

    }
    private void TargetSpotted()
    {//will update just testing
        TargetInSpottingRange = true;
        TargetDetected = true;

    }
    void OnCollision(Collision col)
    {

    }


    
    
}
