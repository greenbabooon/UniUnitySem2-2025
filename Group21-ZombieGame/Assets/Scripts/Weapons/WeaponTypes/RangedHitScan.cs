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
    public void AttackPressed()
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
    public void AttackReleased()
    {
        if(weapon.isAutomatic == true)
        {
            StopFiring();
        }
    }
    public void Reload()
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


