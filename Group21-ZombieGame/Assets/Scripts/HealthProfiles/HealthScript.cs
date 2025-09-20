using UnityEngine;

public interface IDamageable
{
    void damage(float damageAmount);
}
public class HealthScript : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;



}
