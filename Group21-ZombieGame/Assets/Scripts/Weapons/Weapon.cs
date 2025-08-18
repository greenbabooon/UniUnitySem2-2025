using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    string weaponName="hands";
    public int damage=10;
    public bool isRanged=false;
    public float fireRate=1;
    public float force=0f;//only used for projectiles
    public int magazineCapacity=0;//only used for projectiles
    public int currentAmmo=0;//only used for projectiles
    public float reloadTime=0;//only used for projectiles
    public Sprite WeaponIcon;
    public bool isAutomatic=false;//only used for projectiles
    public int ammoType=0;//0:melee,1,2,3 ect
    public GameObject projectilePrefab;//only used for projectiles
    public string GetWeaponName()
    {
        return weaponName;
    }

    public int GetDamage()
    {
        return damage;
    }

    public bool IsRanged()
    {
        return isRanged;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public float GetForce()
    {
        return force;
    }

    public int GetMagazineCapacity()
    {
        return magazineCapacity;
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public float GetReloadTime()
    {
        return reloadTime;
    }
    public Sprite GetWeaponIcon()
    {
        return WeaponIcon;
    }
    public bool IsAutomatic()
    {
        return isAutomatic;
    }
    public int GetAmmoType()
    {
        return ammoType;
    }
    public GameObject GetProjectile()
    {
        return projectilePrefab;
    }
}
