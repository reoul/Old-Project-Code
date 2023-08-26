using UnityEngine;

public class Item012 : Item
{
    public override void Active()
    {
        IBattleable PlayerBattleable = BattleManager.Instance.PlayerBattleable;
        IBattleable EnemyBattleable = BattleManager.Instance.EnemyBattleable;

        EnemyBattleable.ToDamage(Mathf.FloorToInt(PlayerBattleable.OffensivePower.FinalStatus * 1.5f));
    }
}
