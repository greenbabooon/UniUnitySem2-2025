using Unity.VisualScripting;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    public float damageMultiplier = 1f;
    private float gunDamage;
    bool hasDealtDamage = false;
    void Awake()
    {
        foreach (projectileScript proj in GameObject.FindObjectsByType<projectileScript>(FindObjectsSortMode.None))
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), proj.GetComponent<Collider>());
        }  
    }
    private void OnEnable()
    {
        hasDealtDamage = false;
        Invoke("KillProjectile", 5f);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<HealthScript>() != null && !hasDealtDamage)
        {
            HealthScript health = collision.gameObject.GetComponent<HealthScript>();
            health.currentHealth -= gunDamage * damageMultiplier;
            print("Projectile hit " + collision.gameObject.name + "! Damage dealt: " + (gunDamage * damageMultiplier));
            if (collision.gameObject.GetComponent<ZombieScript>() != null)
            {
                collision.gameObject.GetComponent<ZombieScript>().dmgUpdate();
            }  
            hasDealtDamage = true;
        }
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        Invoke("KillProjectile", 0.1f);
    }
    private void KillProjectile()
    {
        //play impact effect here later
        gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
    public void SetDamage(float damage)
    {
        gunDamage = damage;
    }
}
