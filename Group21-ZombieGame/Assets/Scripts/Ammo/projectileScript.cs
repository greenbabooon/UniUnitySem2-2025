using Unity.VisualScripting;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    public float damageMultiplier = 1f;
    private float gunDamage;
    private void Awake()
    {
        Invoke("KillProjectile", 30f);


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<HealthScript>() != null)
        {
            HealthScript health = collision.gameObject.GetComponent<HealthScript>();
            health.currentHealth -= gunDamage * damageMultiplier;
            print("Projectile hit " + collision.gameObject.name + "! Damage dealt: " + (gunDamage * damageMultiplier));
            if (collision.gameObject.GetComponent<ZombieScript>() != null)
            {
                collision.gameObject.GetComponent<ZombieScript>().dmgUpdate();
            }  
        }
        KillProjectile();
    }
    private void KillProjectile()
    {
        Destroy(gameObject);
    }
    public void SetDamage(float damage)
    {
        gunDamage = damage;
    }
}
