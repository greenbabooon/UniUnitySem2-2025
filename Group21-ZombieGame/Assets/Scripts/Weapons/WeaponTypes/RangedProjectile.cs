using Unity.Mathematics;
using UnityEngine;

public class RangedProjectile : WeaponType, IAttackable
{
    int CurSpare = 0;
    bool isReloading = false;
    bool canShoot = true;
    TypeOfWeapon weaponType = TypeOfWeapon.rangedProjectile;
    ObjPool objPooler;


    void Awake()
    {
        objPooler = gameObject.AddComponent<ObjPool>();
        objPooler.SetPooled(weapon.projectilePrefab, weapon.magazineCapacity);

    }
    void OnEnable()
    {

    }
    void OnDisable()
    {
        StopFiring();
        CancelReload();
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
        if (weapon.isAutomatic == true)
        {
            StopFiring();
        }
    }
    public override void Reload()
    {
        if (!isReloading && weapon.currentAmmo < weapon.magazineCapacity && playerOwned)
        {
            Invoke("ReloadWeapon", weapon.reloadTime);
            isReloading = true;
            if (playerOwned)
            {
                player.GetComponent<PlayerController>().reloadText.enabled = true;
            }
        }
    }

    void fireProjectile()
    {
        if (canShoot && weapon.currentAmmo > 0 && !isReloading)
        {
            GameObject curProj = objPooler.GetPooledObj();
            curProj.GetComponent<projectileScript>().SetDamage(weapon.damage);
            curProj.transform.position = firePoint.transform.position + firePoint.transform.forward * 0.75f;
            curProj.transform.rotation = firePoint.transform.rotation;
            Rigidbody rb = curProj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(firePoint.transform.forward * weapon.force, ForceMode.Impulse);
            }
            if (playerOwned)
            {
                //if the weapon is player owned minus one from the current ammo count else do nothing (ie enemy owned projectiles have infinite ammo)
                //maybe change this later to not take from ammo pool rather
                weapon.currentAmmo--;
                player.GetComponent<PlayerController>().UpdateAmmoUI();
            }

            canShoot = false;
            Invoke("enableShooting", weapon.fireRate);
        }
    }
    void StartFiring()
    {
        InvokeRepeating("fireProjectile", 0f, 0.01f);
    }
    void StopFiring()
    {
        CancelInvoke("fireProjectile");
    }
    void enableShooting()
    {
        canShoot = true;
    }
    void ReloadWeapon()
    {
        if (!playerOwned)
        {
            weapon.currentAmmo = weapon.magazineCapacity;
            isReloading = false;
        }
        else
        {
            CurSpare = player.GetComponent<Inventory>().GetAmmoCount(weapon.ammoType);
            int neededAmmo = weapon.magazineCapacity - weapon.currentAmmo;
            if (CurSpare >= neededAmmo)
            {
                weapon.currentAmmo += neededAmmo;
                player.GetComponent<Inventory>().SetAmmoCount(weapon.ammoType, neededAmmo);
            }
            else
            {
                weapon.currentAmmo += CurSpare;
                player.GetComponent<Inventory>().SetAmmoCount(weapon.ammoType, CurSpare);
            }
            isReloading = false;
            if (playerOwned)
            {
                player.GetComponent<PlayerController>().reloadText.enabled = false;
                player.GetComponent<PlayerController>().UpdateAmmoUI();
            }
        }
    }
    public override void CancelReload()
    {
        if (isReloading)
        {
            CancelInvoke("ReloadWeapon");
            isReloading = false;
            if (playerOwned)
            {
                FindFirstObjectByType<PlayerController>().reloadText.enabled = false;
            }
        }
    }
}
