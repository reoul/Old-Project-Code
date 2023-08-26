using UnityEngine;

public class Item017 : Item
{
    public override void Active()
    {
        IBattleable PlayerBattleable = BattleManager.Instance.PlayerBattleable;
        IBattleable EnemyBattleable = BattleManager.Instance.EnemyBattleable;

        EnemyBattleable.DefensivePower.ItemStatus += 5;
        //EnemyBattleable.OwnerObj.GetComponent<Enemy>().ValueUpdater.AddVal(5, ValueUpdater.valType.def);

        PlayerBattleable.PiercingDamage.ItemStatus += 10;
        PlayerBattleable.OwnerObj.GetComponent<Player>().ValueUpdater.AddVal(10, ValueUpdater.valType.piercing, false);
    }
}
