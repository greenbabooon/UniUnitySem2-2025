using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour, IDamageable
{
    public HealthScript health;
    protected int layerMask = 1 << 7;
    public Canvas healthCanvas;
    public UnityEngine.UI.Image[] healthIcons;
    protected Transform canvasRotation;
    protected NavMeshAgent agent;
    protected bool TargetInSpottingRange = false;
    protected bool TargetInAttackRange = false;
    public float cooldownTime = 1f;
    public float attackRange = 2f;
    public float spottingRange = 10f;
    protected bool canAttack = true;
    protected int delayedUpdate = 0;
    protected Animator anim;
    public float dmg = 10f;
    protected float distance;
    protected GameObject player;
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
        if (GetComponentInChildren<Animator>() != null)
        {
            anim = GetComponentInChildren<Animator>();
        }
        GetComponent<Collider>();
        player = FindAnyObjectByType<PlayerController>().gameObject;
    }
    void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, GameObject.FindFirstObjectByType<PlayerController>().transform.position);
        if (TargetInSpottingRange==false)
        {
            anim.SetBool("isWalking", false);
        }else if (TargetInSpottingRange)
        {
            anim.SetBool("isWalking", true);   
        }
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
        if (distance < spottingRange)
        {
            TargetSpotted();
        }
        if (distance>spottingRange+5)
        {
            TargetLost();
        }
        if(distance<attackRange)
        {
            InAttackRange();   
        }
    }
    private void TargetSpotted()
    {
        TargetInSpottingRange = true;
    }
    void TargetLost()
    {
        TargetInSpottingRange = false;
        agent.SetDestination(transform.position);
    }

    public void CanAttack()
    {
        canAttack = true;
    }
    void InAttackRange()
    {
        transform.LookAt(player.transform);
        if (canAttack) Attack();
    }
    protected virtual void Attack()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, attackRange,layerMask))
        {
            print(hit.collider.gameObject.name);
            if (hit.collider.gameObject.GetComponent<PlayerController>() != null)
            {
                hit.collider.gameObject.GetComponent<PlayerController>().damage(dmg);
                Invoke("CanAttack", cooldownTime);
                canAttack = false;
            }
        }

    }
    void Alert()
    {
        
    }
}
