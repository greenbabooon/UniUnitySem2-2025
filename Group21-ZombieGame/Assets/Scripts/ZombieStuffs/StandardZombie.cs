using UnityEngine;

public class StandardZombie : ZombieScript
{
    protected override void Attack()
    {
        anim.SetTrigger("Attack 0");
    }
    public void OnAnimationEnded()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, attackRange, layerMask))
        {
            print(hit.collider.gameObject.name);
            if (hit.collider.gameObject.GetComponent<PlayerController>() != null)
            {
                hit.collider.gameObject.GetComponent<PlayerController>().damage(dmg);
            }
        }
    }

}
