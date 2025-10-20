using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxItems = 5;//all items other than ammo
    private int curItems = 0;
    public int ammoType1Count = 90;
    public int ammoType2Count = 90;
    public int ammoType3Count = 90;
    public List<Weapon> weapons = new List<Weapon>();
    private Dictionary<Weapon, int> invSlots = new Dictionary<Weapon, int>();
    List <GameObject> weaponObjs = new List<GameObject>();
    private void Start()
    {
        InitializeInv();
    }
    public void InitializeInv()
    {   
        invSlots.Clear();
        weaponObjs.Clear();
        for (int i = 0; i < weapons.Count; i++)
        {
            invSlots.Add(weapons[i], i);
            weaponObjs.Add(Instantiate(weapons[i].weaponPrefab));
            weaponObjs[i].SetActive(false);
            weapons[i].SetOwner(weaponObjs[i]);
            if (weapons[i].GetComponent<ObjPool>() == null&&weapons[i].WeaponTypeIndex==2)
            {
                weapons[i].weaponType.Initialize();
            }
        }
        PlayerController player = FindFirstObjectByType<PlayerController>();
        player.UpdateHotbarUI();

    }
    public GameObject GetWeaponObject(int index)
    {
        if (index < 0 || index >= weaponObjs.Count)
        {
            Debug.LogError("Index out of range");
            return null;
        }

        return weaponObjs[index];
    }
    public Weapon GetItem(int index)
    {
        if (index < 0 || index >= weapons.Count)
        {
            Debug.LogError("Index out of range");
            return null;
        }
        return weapons[index];
    }
    public int GetAmmoCount(int ammoType)
    {
        switch (ammoType)
        {
            case 1: return ammoType1Count;
            case 2: return ammoType2Count;
            case 3: return ammoType3Count;
            default: return 0;
        }
    }
    public int SetAmmoCount(int ammoType, int count)
    {
        switch (ammoType)
        {
            case 1: ammoType1Count = count; break;
            case 2: ammoType2Count = count; break;
            case 3: ammoType3Count = count; break;
        }
        return GetAmmoCount(ammoType);
    }

    public void addItem(Weapon item)
    {
        if (curItems < maxItems)
        {
            if (weapons.Contains(item))
            {
                print("testing duplicate prevention");
                Weapon newWeapon = ScriptableObject.Instantiate(item);
                int count = 1;
                while (weapons.Exists(w=>w.name==item.name+"("+count+")"))
                {
                    count++;
                }
                newWeapon.name = item.name + " (" + count + ")";
                weapons.Add(newWeapon);
            }else
            {
                print("adding new weapon");
                weapons.Add(item);
            }
            curItems++;
            InitializeInv();
        }
    }
}
