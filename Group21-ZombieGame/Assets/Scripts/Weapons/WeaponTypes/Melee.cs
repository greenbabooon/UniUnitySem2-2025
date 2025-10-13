using UnityEngine;

public class Melee : WeaponType, IAttackable
{
    float cooldownTimer = 0f;
    float cooldownDuration = 1f;
    Transform weapontip;
    float meleeRange = 2f;
    float meleeCharge;
    float maxMeleeCharge = 2f;
    float MinSwingTime = 0.5f;
    bool isCharging = false;

    TypeOfWeapon weaponType = TypeOfWeapon.melee;
    public override void AttackPressed()
    {
        StartMeleeCharge();
    }

    public override void AttackReleased()
    {
        print("melee attack released");
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
    }
    void CancelMeleeAttack()
    {
        isCharging = false;
        meleeCharge = 0f;
        if (player != null)
        {
            player.GetComponent<PlayerController>().setCanChangeWeapon(true);
        }
    }
    
}
