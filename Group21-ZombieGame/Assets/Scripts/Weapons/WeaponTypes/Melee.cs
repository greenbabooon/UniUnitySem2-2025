using UnityEngine;

public class Melee : WeaponType, IAttackable
{
    TypeOfWeapon weaponType = TypeOfWeapon.melee;

    public override void AttackPressed()
    {
        print("melee attack pressed");
    }

    public override void AttackReleased()
    {
        print("melee attack released");
    }

    public override void Reload()
    {
        print("melee reload pressed");
    }
}
