using UnityEngine;

public interface IAttackable
{
    void AttackPressed();
    void AttackReleased();
    void Reload();

}
public class WeaponType : MonoBehaviour, IAttackable
{
    protected GameObject player;
    protected bool playerOwned = false;
    public enum TypeOfWeapon
    {
        rangedProjectile,
        rangedHitscan,
        melee

    }
    protected Weapon weapon;
    protected GameObject firePoint;
    public void SetWeapon(Weapon w)
    {
        weapon = w;
    }
    public virtual void AttackPressed() { print("attack pressed"); }
    public virtual void AttackReleased() { print("attack released"); }
    public virtual void Reload() { print("reload pressed"); }

    public void SetFirePoint(GameObject obj)
    {
        firePoint = obj;
    }
    public void SetPlayerOwned(bool val)
    {
        playerOwned = val;
        if (val)
        {
            player = FindFirstObjectByType<PlayerController>().gameObject;
        }
    }
    public virtual void CancelReload() { print("cancel reload"); }
    public virtual void Initialize(){ print("Initialized weapon"); }
}
