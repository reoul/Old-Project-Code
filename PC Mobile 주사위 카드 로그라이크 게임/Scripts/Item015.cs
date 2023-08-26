using UnityEngine;

public class Item015 : Item
{
    public override void Active()
    {
        IBattleable EnemyBattleable = BattleManager.Instance.EnemyBattleable;

        EnemyBattleable.ToPiercingDamage(3);
    }
}
