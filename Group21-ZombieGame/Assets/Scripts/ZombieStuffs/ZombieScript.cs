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
    public float cooldownTime = 1f;
    public float attackRange = 2f;
    public float spottingRange = 10f;
    bool canAttack = true;
    int delayedUpdate = 0;

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
    void FixedUpdate()
    {
        delayedUpdate++;
        if (healthCanvas.enabled == true)
        {
            canvasRotation.LookAt(Camera.main.transform.position);

        }
        if (TargetInSpottingRange)
        {
            if (delayedUpdate>15)
            {
            agent.SetDestination(GameObject.FindFirstObjectByType<PlayerController>().transform.position);
            delayedUpdate = 0;
            }
            
        }
        if (Vector3.Distance(transform.position, GameObject.FindFirstObjectByType<PlayerController>().transform.position) < spottingRange)
        {
            TargetSpotted();
        }
        else
        {
            TargetInSpottingRange = false;
            TargetDetected = false;
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, GameObject.FindFirstObjectByType<PlayerController>().transform.position - transform.position, out hit, 2f)&&TargetInSpottingRange)
        {
            if (hit.collider.gameObject.GetComponent<PlayerController>() != null && canAttack)
            {
                hit.collider.gameObject.GetComponent<PlayerController>().damage(10f);
                canAttack = false;
                Invoke("CanAttack", cooldownTime);
            }
        }

    }
    private void TargetSpotted()
    {//will update just testing
        TargetInSpottingRange = true;
        TargetDetected = true;

    }

    void CanAttack()
    {
        canAttack = true;
    }   


    
    
}
