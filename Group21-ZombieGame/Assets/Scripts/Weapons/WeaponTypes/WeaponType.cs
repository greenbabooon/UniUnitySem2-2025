using UnityEngine;

public interface IAttackable
{
    void AttackPressed();
    void AttackReleased();
    void Reload();

}
public class WeaponType : MonoBehaviour
{
    public enum TypeOfWeapon
    {
        rangedProjectile,
        rangedHitscan,
        melee
    }
    protected Weapon weapon;

    public void SetWeapon(Weapon w)
    {
        weapon = w;
    }
}
