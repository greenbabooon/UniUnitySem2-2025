using Unity.Mathematics;
using UnityEngine;

public class RangedProjectile : WeaponType, IAttackable
{
    int CurSpare = 0;
    bool isReloading = false;
    bool canShoot = true;
    TypeOfWeapon weaponType = TypeOfWeapon.rangedProjectile;
    ObjPool objPooler = new ObjPool();
    GameObject firePoint = new GameObject("firePoint");
    void Awake()
    {
        objPooler.SetPooled(weapon.projectilePrefab, weapon.magazineCapacity);
        //sets a default fire point
        firePoint.transform.position = gameObject.transform.position + gameObject.transform.forward;
        firePoint.transform.rotation = gameObject.transform.rotation;
        firePoint.transform.parent = gameObject.transform;
    }
    public void setFirePoint(GameObject obj)
    {
        firePoint.transform.position = obj.transform.position + obj.transform.forward;
        firePoint.transform.rotation = obj.transform.rotation;
        firePoint.transform.parent = obj.transform;
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
        if (weapon.isAutomatic == true)
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
            GameObject curProj = objPooler.GetPooledObj();
            curProj.transform.position = firePoint.transform.position;
            curProj.transform.rotation = firePoint.transform.rotation;
            Rigidbody rb = curProj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(firePoint.transform.forward * weapon.force);
            }
            weapon.currentAmmo--;
            canShoot = false;
            Invoke("enableShooting", weapon.fireRate);
        }
    }
    void StartFiring()
    {

    }
    void StopFiring()
    {

    }
    void enableShooting()
    {
        canShoot = true;
    }
    
}
