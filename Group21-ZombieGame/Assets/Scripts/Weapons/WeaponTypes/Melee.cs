using UnityEngine;

public class Melee : WeaponType, IAttackable
{
    float cooldownTimer = 0f;
    float cooldownDuration = 1f;
    Transform weapontip;
    float meleeRange = 2f;
    float meleeCharge;
    float maxMeleeCharge = 2f;
    float SwingTime = 0f;
    bool isCharging = false;
    bool isSwinging = true;
    bool onCooldown = false;

  /*  TypeOfWeapon weaponType = TypeOfWeapon.melee;
    public override void AttackPressed()
    {
        if (onCooldown) return;
        if (isCharging) return;
        StartMeleeCharge();
    }

    public override void AttackReleased()
    {
        if (isCharging)
        {   
           // animator.SetTrigger("swing");
            Debug.Log("Swinging with charge: " + meleeCharge);
            isSwinging = true;
            isCharging = false;
        }
    }

    public override void Reload()
    {
        CancelMeleeAttack();
    }
    void StartMeleeCharge()
    {
        isCharging = true;
        if (player != null)
        {
            player.GetComponent<PlayerController>().setCanChangeWeapon(false);
            Debug.Log("Charging melee attack");
           // animator.SetTrigger("charge");
        }
    }
    void FixedUpdate()
    {
        if (isCharging)
        {
            meleeCharge += 0.02f;
            if (meleeCharge >= maxMeleeCharge)
            {
                meleeCharge = maxMeleeCharge;
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("swing") && SwingTime > 0.5&&!isCharging)//add improved stateMachine function look at yt saved
        {

            Physics.Raycast(player.transform.position, weapontip.position, out RaycastHit hit, meleeRange);
            Debug.DrawRay(player.transform.position, weapontip.position * meleeRange, Color.red, meleeRange);
            if (hit.collider != null)
            {

                if (hit.collider.gameObject.GetComponent<ZombieScript>() != null)
                {
                    hit.collider.gameObject.GetComponent<IDamageable>().damage(weapon.damage * (1 + (meleeCharge / maxMeleeCharge)));
                    onCooldown = true;
                    isSwinging = false;

                }

            }
            SwingTime += 0.02f;
            print("swing time" + SwingTime);
        }
        else
            isSwinging = false;
       // animator.SetTrigger("idle");
        SwingTime = 0;
        if (onCooldown)
        {
            cooldownTimer += 0.02f;
            if (cooldownTimer >= cooldownDuration + (meleeCharge / maxMeleeCharge))
            {
                onCooldown = false;
                cooldownTimer = 0f;
                CancelMeleeAttack();
            }
        }
    }
    void CancelMeleeAttack()
    {
        isCharging = false;
        if (player != null)
        {
            player.GetComponent<PlayerController>().setCanChangeWeapon(true);
        }
        //animator.SetTrigger("idle");
    }
    void Awake()
    {
        weapontip = GetComponentInChildren<Transform>();
       // animator = GetComponent<Animator>();
    }*/
}

