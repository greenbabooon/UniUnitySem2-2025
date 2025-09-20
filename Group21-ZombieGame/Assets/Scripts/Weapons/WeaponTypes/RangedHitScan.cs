using UnityEngine;

public class RangedHitScan : WeaponType, IAttackable
{
    TypeOfWeapon weaponType = TypeOfWeapon.rangedHitscan;
    int CurSpare = 0;
    bool isReloading = false;
    bool canShoot = true;
   
    void Awake()
    {
       
    }
    public override void AttackPressed()
    {
        if (weapon.isAutomatic == false)
        {
            fireProjectile();
        }
        else if (weapon.isAutomatic == true)
        {
            StartFiring();
        }
    }
    public override void AttackReleased()
    {
        if(weapon.isAutomatic == true)
        {
            StopFiring();
        }
    }
    public override void Reload()
    {

    }

    void fireProjectile()
    {
        if (canShoot && weapon.currentAmmo > 0 && !isReloading)
        {
            
            
        }       
    }
    void StartFiring()
    {

    }
    void StopFiring()
    {

    }
}


