using UnityEngine;

public class Item018 : Item
{
    public override void Active()
    {
        IBattleable PlayerBattleable = BattleManager.Instance.PlayerBattleable;
        IBattleable EnemyBattleable = BattleManager.Instance.EnemyBattleable;

        if (PlayerBattleable.DefensivePower.FinalStatus > EnemyBattleable.DefensivePower.FinalStatus)
        {
            PlayerBattleable.PiercingDamage.ItemStatus += 5;
            PlayerBattleable.OwnerObj.GetComponent<Player>().ValueUpdater.AddVal(5, ValueUpdater.valType.piercing, false);
        }
    }
}
