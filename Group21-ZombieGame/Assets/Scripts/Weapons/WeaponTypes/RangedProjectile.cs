using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class RangedProjectile : WeaponType, IAttackable
{
    int CurSpare = 0;
    public AudioClip shootNoise;
    private playerAnimController playerAnim;
    bool isReloading = false;
    bool canShoot = true;
    TypeOfWeapon weaponType = TypeOfWeapon.rangedProjectile;
    ObjPool objPooler;
    public override void Initialize()
    {
    if (objPooler==null)objPooler = gameObject.AddComponent<ObjPool>();
    objPooler.SetPooled(weapon.projectilePrefab);
        objPooler = gameObject.AddComponent<ObjPool>();
        objPooler.SetPooled(weapon.projectilePrefab, weapon.magazineCapacity);

        if (playerOwned && player != null)
        {
            playerAnim = GameObject.FindFirstObjectByType<playerAnimController>().gameObject.GetComponent<playerAnimController>();
        }

    }
    void OnEnable()
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
                 player.GetComponent<PlayerController>().reloadText.color= new Color(1,1,1,1);
                 player.GetComponent<PlayerController>().reloadText.gameObject.GetComponentInChildren<Animator>().SetBool("isReloading",true);
            }
        }
    }

    void fireProjectile()
    {
        if (canShoot && weapon.currentAmmo > 0 && !isReloading)
        {
            GameObject curProj = objPooler.GetPooledObj();
            curProj.GetComponent<projectileScript>().SetDamage(weapon.damage);
            curProj.transform.position = firePoint.transform.position;
            curProj.transform.rotation = firePoint.transform.rotation;
            curProj.SetActive(true);
            Rigidbody rb = curProj.GetComponent<Rigidbody>();

            GetComponent<AudioSource>().Play();
            if (playerAnim != null)
            playerAnim.shootSFX();

            if (rb != null)
            {
                rb.AddForce(firePoint.transform.forward * 3f * weapon.force, ForceMode.Impulse);
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
                player.GetComponent<Inventory>().SetAmmoCount(weapon.ammoType, CurSpare - neededAmmo);
            }
            else
            {
                weapon.currentAmmo += CurSpare;
                player.GetComponent<Inventory>().SetAmmoCount(weapon.ammoType, 0);
            }
            isReloading = false;
            if (playerOwned)
            {
                player.GetComponent<PlayerController>().reloadText.color= new Color(1,1,1,0);
                 player.GetComponent<PlayerController>().reloadText.GetComponentInChildren<Animator>().SetBool("isReloading",false);
                player.GetComponent<PlayerController>().UpdateAmmoUI();
            }
        }
    }
    public override void CancelReload()
    {
            CancelInvoke("ReloadWeapon");
            
                FindFirstObjectByType<PlayerController>().reloadText.color= new Color(1,1,1,0);
                player.GetComponent<PlayerController>().reloadText.gameObject.GetComponentInChildren<Animator>().SetBool("isReloading", false);
                player.GetComponent<PlayerController>().UpdateAmmoUI();
                isReloading = false;
    }
}
