using Unity.VisualScripting;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    private void Awake()
    {
        Invoke("KillProjectile", 30f); 
    }
    private void OnCollisionEnter(Collision collision)
    {
        //put damage dealing code here before killing the projectile
        KillProjectile();
    }
    private void KillProjectile()
    {
        Destroy(gameObject);
    }
}
