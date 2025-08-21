using UnityEngine;

[CreateAssetMenu(fileName = "NewAmmo", menuName = "Inventory/Ammo")]
public class ammoScript : ScriptableObject
{
    public int ammoType = 0;
    public int ammoAmount = 0;
    public string ammoName = "Default Ammo";
    public Sprite ammoIcon;
    public Material mat;
}
