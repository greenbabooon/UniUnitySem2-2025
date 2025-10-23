
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Weapon", menuName = "WeaponsData/Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName = "hands";
    public int damage = 10;
    public bool isRanged = false;
    public float fireRate = 1;
    public float force = 0f;//only used for projectiles
    public int magazineCapacity = 0;//only used for projectiles
    public int currentAmmo = 0;//only used for projectiles
    public float reloadTime = 0;//only used for projectiles
    public Sprite WeaponIcon;
    public bool isAutomatic = false;//only used for projectiles
    public int ammoType = 0;//0:melee,1,2,3 ect
    public GameObject projectilePrefab;//only used for projectiles
    public GameObject weaponPrefab;//game Object with weapon model
    public WeaponType weaponType;//reference to weapon type script
    public int WeaponTypeIndex;//0:melee,1:ranged hitscan,2:ranged projectile
    private GameObject Owner;
    public void SetOwner(GameObject obj)
    {
        Owner = obj;
       if (WeaponTypeIndex == 0)
        {
            weaponType = Owner.AddComponent<Melee>();
        }
        else if (WeaponTypeIndex == 1)
        {
            weaponType = Owner.AddComponent<RangedHitScan>();
        }
        else if (WeaponTypeIndex == 2)
        {
            weaponType = Owner.AddComponent<RangedProjectile>();
        }
        weaponType.SetWeapon(this);
        weaponType.Initialize();
    }
    void OnStart()
    {
 
    }
}
