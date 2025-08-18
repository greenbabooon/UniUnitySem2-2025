using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
           Destroy(gameObject);
        }
    }
}
