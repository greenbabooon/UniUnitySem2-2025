using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int maxItems = 5;//all items other than ammo
    private int curItems = 0;
    private int ammoType1Count = 90;
    private int ammoType2Count = 90;
    private int ammoType3Count = 90;
    public List<GameObject> weapons = new List<GameObject>();
    private Dictionary<GameObject, int> invSlots = new Dictionary<GameObject, int>();
    private void Start()
    {
        InitializeInv();
    }
    private void InitializeInv()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            invSlots.Add(weapons[i], i);
        }

    }
    public GameObject GetItem(int index)
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


}
